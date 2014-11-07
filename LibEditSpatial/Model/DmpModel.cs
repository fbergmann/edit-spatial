using System;
using System.Drawing;
using System.IO;
using System.Text;

namespace LibEditSpatial.Model
{
  public class DmpModel
  {
    public DmpModel(int rows, int cols)
    {
      Palette = DmpPalette.Default;

      Columns = cols;
      Rows = rows;
      Data = new double[rows, cols];
      Min = 0;
      Max = 10;
    }

    public DmpPalette Palette { get; set; }

    public string FileName { get; set; }

    public int Columns { get; set; }
    public int Rows { get; set; }
    public double[,] Data { get; set; }

    public double Min { get; set; }
    public double Max { get; set; }

    public bool IssueEvents { get; set; }

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
      Rows = Data.GetLength(0);
      Columns = Data.GetLength(1);
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
        Array.Copy(original, i*original.GetLength(0), newArray, i*newArray.GetLength(0), minX);

      return newArray;
    }

    public void Resize(int rows, int cols)
    {
      Data = ResizeArray(Data, cols, rows);
      Rows = rows;
      Columns = cols;
    }

    public void SaveAs(string fileName)
    {
      var builder = new StringBuilder();

      builder.AppendFormat("{0} {1}{2}", Rows, Columns, Environment.NewLine);
      for (int i = 0; i < Rows; i++)
      {
        for (int j = 0; j < Columns; j++)
        {
          builder.Append(Data[i, j]);
          if (j + 1 < Columns)
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
      var result = new Bitmap(Rows, Columns);
      for (int i = 0; i < Rows; i++)
        for (int j = 0; j < Columns; j++)
          result.SetPixel(i, j, GetColor(Data[i, j]));

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
          int.TryParse(dims[0], out rows);
          int.TryParse(dims[1], out cols);
        }
      }

      var model = new DmpModel(rows, cols) {Min = double.MaxValue, Max = double.MinValue};
      for (int i = 1; i < Math.Min(rows + 1, lines.Length); i++)
      {
        string[] entries = lines[i].Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);
        for (int j = 0; j < Math.Min(cols, entries.Length); j++)
        {
          double current;
          if (double.TryParse(entries[j], out current))
          {
            model[i - 1, j] = current;
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

      for(int i = 0; i < Rows; ++i)
        for (int j = 0; j < Columns; ++j)
        { 
          var current = Data[i, j];
          if (current != 0)
          {
            if (j < minX)
              minX = j;
            if (j > maxX)
              maxX = j;
            if (i < minY)
              minY = i;
            if (i > maxY)
              maxY = i;
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
      for (int i = used.Y; i <= used.Height; i++)
        for (int j = used.X; j <= used.Width; j++)
          data[i + difference.Height, j + difference.Width] = Data[i, j];
      Data = data;
    }
  }
}