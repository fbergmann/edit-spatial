using System;
using System.Drawing;
using System.IO;
using System.Text;

namespace WFEditDMP.Model
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
      int newColumn, newRow = 0;
      for (int oldColumn = oldMatrix.GetLength(1) - 1; oldColumn >= 0; oldColumn--)
      {
        newColumn = 0;
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
      return Palette.GetColor((val - Min)/Max);

      //switch ((int)val)
      //{
      //  case 0: return Color.Transparent;
      //  case 1: return Color.Red;
      //  case 2: return Color.Blue;
      //  case 3: return Color.Green;
      //  case 4: return Color.Yellow;
      //  case 5: return Color.Tomato;
      //  case 6: return Color.AliceBlue;
      //  case 7: return Color.YellowGreen;
      //  case 8: return Color.DarkTurquoise;
      //  case 9: return Color.Khaki;
      //  case 10: return Color.MediumSeaGreen;
      //  case 11: return Color.Purple;
      //  default: return Color.Black;
      //}
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
      if (lines != null && lines.Length > 0)
      {
        string[] dims = lines[0].Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);
        if (dims.Length > 1)
        {
          int.TryParse(dims[0], out rows);
          int.TryParse(dims[1], out cols);
        }
      }

      var model = new DmpModel(rows, cols) {Min = double.MaxValue, Max = double.MinValue};
      double current = 0;
      for (int i = 1; i < Math.Min(rows + 1, lines.Length); i++)
      {
        string[] entries = lines[i].Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);
        for (int j = 0; j < Math.Min(cols, entries.Length); j++)
        {
          if (double.TryParse(entries[j], out current))
          {
            model[i - 1, j] = current;
          }
        }
      }
      return model;
    }
  }
}