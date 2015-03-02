using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace LibEditSpatial.Model
{
  public class DmpModel : ICloneable
  {
    public DmpModel(int cols, int rows)
    {
      Palette = DmpPalette.Default;

      Columns = cols;
      MaxX = cols;
      Rows = rows;
      MaxY = rows;
      Data = new double[cols, rows];
      Min = 0;
      Max = 10;
    }

    /// <summary>
    /// Flag indicating whether the current model needs to be saved
    /// </summary>
    public bool Dirty { get; set; }

    /// <summary>
    /// Current Palette
    /// </summary>
    public DmpPalette Palette { get; set; }
  
    /// <summary>
    /// Filename for the model
    /// </summary>
    public string FileName { get; set; }
    
    /// <summary>
    /// Number of Columns
    /// </summary>
    public int Columns { get; set; }

    /// <summary>
    /// Returns all unique values in this model
    /// </summary>
    public List<double> Range
    {
      get
      {
        var numbers = Data;
        var unique = Enumerable.Range(0, numbers.GetUpperBound(0) + 1)
          .SelectMany(x => Enumerable.Range(0, numbers.GetUpperBound(1) + 1)
            .Select(y => numbers[x, y])).Distinct().ToList();
        unique.Sort();
        return unique;
      }
    }

    /// <summary>
    /// X range
    /// </summary>
    public double XRange {
      get { return MaxX - MinX; }
    }

    /// <summary>
    /// Y range
    /// </summary>
    public double YRange
    {
      get { return MaxY - MinY; }
    }

    /// <summary>
    /// data range
    /// </summary>
    public double DataRange
    {
      get { return Max - Min; }
    }
  
    /// <summary>
    /// number of rows
    /// </summary>
    public int Rows { get; set; }

    /// <summary>
    ///   Saves data in form of [columns, rows]
    /// </summary>
    public double[,] Data { get; set; }

    public double MinX { get; set; }
    public double MaxX { get; set; }
    public double MinY { get; set; }
    public double MaxY { get; set; }
    public double Min { get; set; }
    public double Max { get; set; }
    public bool IssueEvents { get; set; }

    /// <summary>
    ///   Access the element at the given index position
    /// </summary>
    /// <param name="x">index from 0...Columns</param>
    /// <param name="y">index from 0...Rows</param>
    /// <returns>value at given index position</returns>
    public double this[int x, int y]
    {
      get { return Data[x, y]; }
      set
      {
        Data[x, y] = value;
        Min = Math.Min(Min, value);
        Max = Math.Max(Max, value);
        OnModelChanged();
      }
    }

    public object Clone()
    {
      return new DmpModel(Columns, Rows)
      {
        MinX = MinX,
        MaxX = MaxX,
        MinY = MinY,
        MaxY = MaxY,
        Min = Min,
        Max = Max,
        Data = (double[,]) Data.Clone()
      };
    }

    //public double this[double x, double y]
    //{
    //  get
    //  {
    //    if (x < MinX)
    //      x = MinX;
    //    if (x > MaxX)
    //      x = MaxX;
    //    if (y < MinY)
    //      y = MinY;
    //    if (y > MaxY)
    //      y = MaxY;

    //    int posX = (int)Math.Round((x - MinX)) ;
    //    int posY = 0;


    //    return Data[posX, posY]; 
    //  }
    //  set
    //  {
    //    Data[x, y] = value;
    //    Min = Math.Min(Min, value);
    //    Max = Math.Max(Max, value);
    //    OnModelChanged();
    //  }
    //}

    public event EventHandler<DmpModel> ModelChanged;

    public void Invert()
    {
      var range = Range;
      var count = range.Count/2;
      for (var i = 0; i < count; i++)
      {
        var first = range[i];
        var last = range[range.Count - 1 - i];
        Transform(
          x =>
          {
            if (Math.Abs(x - first) < 1e-10) return last;
            if (Math.Abs(x - last) < 1e-10) return first;
            return x;
          }
          );
      }
    }

    public void OnModelChanged()
    {
      Dirty = true;
      if (ModelChanged != null && IssueEvents)
      {
        ModelChanged(this, this);
      }
    }

    public void RotateRight()
    {
      Data = RotateMatrixCounterClockwise(Data);
      Columns = Data.GetLength(0);
      Rows = Data.GetLength(1);
    }

    private static T[,] RotateMatrixCounterClockwise<T>(T[,] oldMatrix)
    {
      var newMatrix = new T[oldMatrix.GetLength(1), oldMatrix.GetLength(0)];
      var newRow = 0;
      for (var oldColumn = oldMatrix.GetLength(1) - 1; oldColumn >= 0; oldColumn--)
      {
        var newColumn = 0;
        for (var oldRow = 0; oldRow < oldMatrix.GetLength(0); oldRow++)
        {
          newMatrix[newRow, newColumn] = oldMatrix[oldRow, oldColumn];
          newColumn++;
        }
        newRow++;
      }
      return newMatrix;
    }

    protected T[,] ResizeArray<T>(T[,] original, int x, int y)
    {
      var newArray = new T[x, y];
      var minX = Math.Min(original.GetLength(0), newArray.GetLength(0));
      var minY = Math.Min(original.GetLength(1), newArray.GetLength(1));

      for (var i = 0; i < minY; ++i)
        for (var j = 0; j < minX; ++j)
          newArray[j, i] = original[j, i];

      return newArray;
    }

    public void Resize(int cols, int rows, bool scaleExisting = false)
    {
      if (scaleExisting)
      { 
        ResizeByScaling(cols, rows);
        return;
      }

      Data = ResizeArray(Data, cols, rows);
      Rows = rows;
      Columns = cols;
      Dirty = true;
    }

    private static DmpPalette GetRedPaletteOrDefault()
    {
      try
      {
        string file = Path.Combine(
                            Path.GetDirectoryName(Assembly.GetEntryAssembly().Location),
                            "Palettes/black-red.txt");
        if (File.Exists(file))
          return DmpPalette.FromFile(
                        file
                        );
      }
      catch
      {

      }
      return DmpPalette.Default;
    }

    private void ResizeByScalingWithPalette(int cols, int rows, DmpPalette palette)
    {
      var image = ToImage(palette);
      var bitmap = new Bitmap(cols, rows);
      using (var g = Graphics.FromImage(bitmap))
      {
        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
        g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
        g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighSpeed;
        g.DrawImage(image, 0, 0, cols, rows);
        g.Flush(System.Drawing.Drawing2D.FlushIntention.Flush);
      }

      var dmp = FromImage(bitmap, (c) =>
      {
        var index = palette.GetIndex(c);
        if (index == -1)
          return 0;
        return MapFromUnit((double)index / (double)palette.Colors.Count);
      });

      Data = dmp.Data;
      Columns = dmp.Columns;
      Rows = dmp.Rows;
    }
    /// <summary>
    /// Creates a new image with required resolution, 
    /// and samples it again. 
    /// </summary>
    /// <param name="cols"></param>
    /// <param name="rows"></param>
    public void ResizeByScaling(int cols, int rows)
    {
      DmpPalette palette = GetRedPaletteOrDefault();
      ResizeByScalingWithPalette(cols, rows, palette);

    }

    public void SaveAs(string fileName)
    {
      var builder = new StringBuilder();

      builder.AppendFormat("{0} {1}{2}", Columns, Rows, Environment.NewLine);
      builder.AppendFormat("{0} {1} {2} {3} {4}", MinX, MaxX, MinY, MaxY, Environment.NewLine);

      for (var y = 0; y < Rows; y++)
      {
        for (var x = 0; x < Columns; x++)

        {
          builder.Append(Data[x, y]);
          if (x + 1 < Columns)
            builder.Append(" ");
        }
        builder.AppendLine();
      }

      File.WriteAllText(fileName, builder.ToString());
      FileName = fileName;
      Dirty = false;
    }

    public Color GetColor(double val)
    {
      return Palette.GetColor(MapToUnit(val));
    }

    public Bitmap ToImage(DmpPalette palette)
    {
      if (Columns == 0 && Rows == 0)
        return new Bitmap(1, 1);

      var result = new Bitmap(Columns, Rows);
      for (var x = 0; x < Columns; x++)
        for (var y = 0; y < Rows; y++)
          result.SetPixel(x, y, palette.GetColor(MapToUnit(Data[x, y])));

      return result;
    }

    public Bitmap ToImage()
    {
      if (Columns == 0 && Rows == 0)
        return new Bitmap(1, 1);

      var result = new Bitmap(Columns, Rows);
      for (var x = 0; x < Columns; x++)
        for (var y = 0; y < Rows; y++)
          result.SetPixel(x, y, GetColor(Data[x, y]));

      return result;
    }

    public static bool IsImageFile(string fileName)
    {
      var ext = Path.GetExtension(fileName).ToLowerInvariant();
      return ext.EndsWith("tif") || ext.EndsWith("tiff") || ext.EndsWith("png") || ext.EndsWith("jpg") ||
             ext.EndsWith("jpeg") || ext.EndsWith("bmp");
    }

    public static DmpModel FromFile(string filename)
    {
      if (IsImageFile(filename))
        return FromImage(filename);          


      var lines = File.ReadAllLines(filename);
      var rows = 0;
      var cols = 0;
      if (lines.Length > 0)
      {
        var dims = lines[0].Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);
        if (dims.Length > 1)
        {
          int.TryParse(dims[0], out cols);
          int.TryParse(dims[1], out rows);
        }
      }

      var model = new DmpModel(cols, rows)
      {
        Min = double.MaxValue,
        Max = double.MinValue,
        FileName = filename
      };

      var start = 2;
      if (lines.Length > 1)
      {
        var dims = lines[1].Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);
        if (dims.Length == 4)
        {
          double temp;
          if (double.TryParse(dims[0], out temp))
            model.MinX = temp;
          if (double.TryParse(dims[1], out temp))
            model.MaxX = temp;
          if (double.TryParse(dims[2], out temp))
            model.MinY = temp;
          if (double.TryParse(dims[3], out temp))
            model.MaxY = temp;
        }
        else
        {
          // old format
          model.MinX = 0;
          model.MinY = 0;
          model.MaxX = model.Columns;
          model.MaxY = model.Rows;

          start = 1;
        }
      }

      for (var y = start; y < Math.Min(rows + start, lines.Length); y++)
      {
        var entries = lines[y].Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);
        for (var x = 0; x < Math.Min(cols, entries.Length); x++)
        {
          double current;
          if (double.TryParse(entries[x], out current))
          {
            model[x, y - start] = current;
          }
        }
      }

      if (Math.Abs(model.Min - model.Max) < 1e-10)
      {
        model.Max = model.Min + 10;
      }

      model.Dirty = false;
      return model;
    }

    public static DmpModel FromImage(Bitmap bmp, Func<Color,double> mapper = null)
    {
      if (bmp == null) return null;
      //var image = mapper == null ? ToGrayScale(bmp) : bmp;
      var image =  bmp;
      var dmp = new DmpModel(image.Width, image.Height);
      for (var y = 0; y < dmp.Rows; ++y)
        for (var x = 0; x < dmp.Columns; ++x)
          if (mapper == null)
            dmp[x, y] = image.GetPixel(x, y).R;
          else
            dmp[x, y] = mapper(image.GetPixel(x, y));
      dmp.Dirty = false;
      return dmp;
    }

    public static DmpModel FromImage(string filename)
    {
      var bmp = Image.FromFile(filename) as Bitmap;
      return FromImage(bmp);
    }

    public double MapToUnit(double value)
    {
      if (value < Min) return 0;
      if (value > Max) return 1;

      if (Math.Abs(DataRange) < 1e-10) return 0;

      return (value-Min)/DataRange;
    }

    public double MapFromUnit(double value)
    {
      return value*DataRange + Min;
    }

    private Rectangle FindDimensions()
    {
      var minX = Columns;
      var maxX = 0;
      var minY = Rows;
      var maxY = 0;

      for (var y = 0; y < Rows; ++y)
        for (var x = 0; x < Columns; ++x)
        {
          var current = Data[x, y];
          if (Math.Abs(current) > 1e-10)
          {
            if (x < minX)
              minX = x;
            if (x > maxX)
              maxX = x;
            if (y < minY)
              minY = y;
            if (y > maxY)
              maxY = y;
          }
        }

      return new Rectangle(minX, minY, maxX, maxY);
    }

    public void Center()
    {
      // find dimensions of non-zero entries
      var used = FindDimensions();
      //Data[used.Y, used.X] = 3;
      //Data[used.Height, used.Width] = 5;
      var total = new Rectangle(0, 0, Columns, Rows);
      var endDiff = new Size(total.Width - used.Width, total.Height - used.Height);
      var newStart = new Size(
        (int) Math.Floor((endDiff.Width + used.X)/2.0),
        (int) Math.Floor((endDiff.Height + used.Y)/2.0));
      //Data[newStart.Height, newStart.Width] = 7;
      var difference = new Size(newStart.Width - used.X, newStart.Height - used.Y);
      // Data[difference.Width, difference.Height] = 7;
      var data = new double[Columns, Rows];
      for (var y = used.Y; y <= used.Height; y++)
        for (var x = used.X; x <= used.Width; x++)
          data[x + difference.Width, y + difference.Height] = Data[x, y];
      Data = data;
    }

    public void Transform(Func<double, double> transformation)
    {
      if (transformation == null) return;

      for (var y = 0; y < Rows; ++y)
        for (var x = 0; x < Columns; ++x)
          Data[x, y] = transformation(Data[x, y]);
    }

    public static DmpModel operator +(DmpModel model, double value)
    {
      var tmp = (DmpModel) model.Clone();
      tmp.Transform(x => x + value);
      return tmp;
    }

    public static DmpModel operator *(DmpModel model, double value)
    {
      var tmp = (DmpModel) model.Clone();
      tmp.Transform(x => x*value);
      return tmp;
    }

    public void MaskWith(DmpModel file)
    {
      file.Resize(Columns, Rows);

      for (var y = 0; y < Rows; ++y)
        for (var x = 0; x < Columns; ++x)
          this[x, y] = file[x,y] * Data[x, y];      
    }


    /// <summary>
    /// Convert image to grayscale, from: 
    /// 
    /// http://tech.pro/tutorial/660/csharp-tutorial-convert-a-color-image-to-grayscale
    /// 
    /// </summary>
    /// <param name="original"></param>
    /// <returns></returns>
    public static Bitmap ToGrayScale(Bitmap original)
    {
      //create a blank bitmap the same size as original
      Bitmap newBitmap = new Bitmap(original.Width, original.Height);

      //get a graphics object from the new image
      Graphics g = Graphics.FromImage(newBitmap);

      //create the grayscale ColorMatrix
      ColorMatrix colorMatrix = new ColorMatrix(
         new float[][] 
      {
         new float[] {.3f, .3f, .3f, 0, 0},
         new float[] {.59f, .59f, .59f, 0, 0},
         new float[] {.11f, .11f, .11f, 0, 0},
         new float[] {0, 0, 0, 1, 0},
         new float[] {0, 0, 0, 0, 1}
      });

      //create some image attributes
      ImageAttributes attributes = new ImageAttributes();

      //set the color matrix attribute
      attributes.SetColorMatrix(colorMatrix);

      //draw the original image on the new image
      //using the grayscale color matrix
      g.DrawImage(original, new Rectangle(0, 0, original.Width, original.Height),
         0, 0, original.Width, original.Height, GraphicsUnit.Pixel, attributes);

      //dispose the Graphics object
      g.Dispose();
      return newBitmap;
    }
  }
}
