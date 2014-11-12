using System;
using System.Drawing;
using System.IO;
using System.Text;

namespace LibEditSpatial.Model
{
  public class DmpModel
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

    public DmpPalette Palette { get; set; }

    public string FileName { get; set; }

    public int Columns { get; set; }
    public int Rows { get; set; }

    /// <summary>
    /// Saves data in form of [columns, rows]
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
    /// Access the element at the given index position
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

    public void OnModelChanged()
    {
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
      int newRow = 0;
      for (int oldColumn = oldMatrix.GetLength(1) - 1; oldColumn >= 0; oldColumn--)
      {
        int newColumn = 0;
        for (int oldRow = 0; oldRow < oldMatrix.GetLength(0); oldRow++)
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
      int minX = Math.Min(original.GetLength(0), newArray.GetLength(0));
      int minY = Math.Min(original.GetLength(1), newArray.GetLength(1));

      for (int i = 0; i < minY; ++i)
        for (int j = 0; j < minX; ++j)
          newArray[j, i] = original[j, i];

      return newArray;
    }

    public void Resize(int cols, int rows)
    {
      Data = ResizeArray(Data, cols, rows);
      Rows = rows;
      Columns = cols;
    }

    public void SaveAs(string fileName)
    {
      var builder = new StringBuilder();

      builder.AppendFormat("{0} {1}{2}", Columns, Rows, Environment.NewLine);
      builder.AppendFormat("{0} {1} {2} {3} {4}", MinX, MaxX, MinY, MaxY, Environment.NewLine);

      for (int y = 0; y < Rows; y++)
      {
        for (int x = 0; x < Columns; x++)
          
        {
          builder.Append(Data[x, y]);
          if (x + 1 < Columns)
            builder.Append(" ");
        }
        builder.AppendLine();
      }


      File.WriteAllText(fileName, builder.ToString());
    }

    private Color GetColor(double val)
    {
      return Palette.GetColor(Max == 0 ? 0 : (val - Min)/Max);
    }


    public Bitmap ToImage()
    {
      var result = new Bitmap(Columns, Rows);
        for (int j = 0; j < Columns; j++)
          for (int i = 0; i < Rows; i++)
            result.SetPixel(j, i, GetColor(Data[j, i]));

      return result;
    }

    public static DmpModel FromFile(string filename)
    {
      string[] lines = File.ReadAllLines(filename);
      int rows = 0;
      int cols = 0;
      if (lines.Length > 0)
      {
        string[] dims = lines[0].Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);
        if (dims.Length > 1)
        {
          int.TryParse(dims[0], out cols);
          int.TryParse(dims[1], out rows);
        }
      }

      var model = new DmpModel(cols, rows) {Min = double.MaxValue, Max = double.MinValue};
      int start = 2;
      if (lines.Length > 1)
      {
        string[] dims = lines[1].Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
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

      for (int y = start; y < Math.Min(rows + start, lines.Length); y++)
      {
        string[] entries = lines[y].Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);
        for (int x = 0; x < Math.Min(cols, entries.Length); x++)
        {
          double current;
          if (double.TryParse(entries[x], out current))
          {
            model[x, y - start] = current;
          }
        }
      }

      if (model.Min == model.Max)
      {
        model.Max = model.Min + 10;
      }

      return model;
    }

    private Rectangle FindDimensions()
    {
      int minX = Columns;
      int maxX = 0;
      int minY = Rows;
      int maxY = 0;

      for(int y = 0; y < Rows; ++y)
        for (int x = 0; x < Columns; ++x)
        { 
          var current = Data[x, y];
          if (current != 0)
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
      var endDiff = new Size(total.Width - used.Width, total.Height- used.Height);
      var newStart = new Size(
        (int)Math.Floor(((double)(endDiff.Width + used.X))/2.0),
        (int)Math.Floor(((double)(endDiff.Height + used.Y)) / 2.0));
      //Data[newStart.Height, newStart.Width] = 7;
      var difference = new Size(newStart.Width - used.X, newStart.Height - used.Y);
     // Data[difference.Width, difference.Height] = 7;
      var data = new double[Columns, Rows];
      for (int y = used.Y; y <= used.Height; y++)
        for (int x = used.X; x <= used.Width; x++)
          data[x + difference.Width, y + difference.Height] = Data[x, y];
      Data = data;
    }
  }
}