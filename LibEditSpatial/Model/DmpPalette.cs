using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;

namespace LibEditSpatial.Model
{
  public class DmpPalette
  {
    private static DmpPalette _Default;

    public DmpPalette()
    {
      Colors = new List<Color>();
    }

    public static DmpPalette Default
    {
      get
      {
        if (_Default == null)
          _Default = new DmpPalette
          {
            IsFirstTransparent = true,
            Colors = new List<Color>
            {
              Color.Transparent
              ,
              Color.Red
              ,
              Color.Blue
              ,
              Color.Green
              ,
              Color.Yellow
              ,
              Color.Tomato
              ,
              Color.DarkSalmon
              ,
              Color.YellowGreen
              ,
              Color.DarkTurquoise
              ,
              Color.Khaki
              ,
              Color.MediumSeaGreen
              ,
              Color.Purple
              ,
              Color.Black
            }
          };
        return _Default;
      }
      set { _Default = value; }
    }

    public List<Color> Colors { get; set; }
    public bool IsFirstTransparent { get; set; }

    public Color GetColor(double val)
    {
      var index = Math.Max(Math.Min((int) Math.Round(val*Colors.Count), Colors.Count - 1), 0);
      return Colors[index];
    }

    public Bitmap ToImage(Size? size = null)
    {
      if (size == null)
        size = new Size {Width = 100, Height = 30};

      var image = new Bitmap(size.Value.Width, size.Value.Height);
      using (var graphics = Graphics.FromImage(image))
      {
        var length = size.Value.Width/(float) Colors.Count;
        for (var i = 0; i < Colors.Count; ++i)
        {
          graphics.FillRectangle(new SolidBrush(Colors[i]), length*i, 0f, length, size.Value.Height);
        }
      }
      return image;
    }

    public void SaveAs(string filename)
    {
      var builder = new StringBuilder();
      foreach (var color in Colors)
      {
        builder.AppendLine(ToArgbString(color));
      }
      File.WriteAllText(filename, builder.ToString());
    }

    public static string ToArgbString(Color oColor)
    {
      return ByteToHex(oColor.A) +
             ByteToHex(oColor.R) +
             ByteToHex(oColor.G) +
             ByteToHex(oColor.B);
    }

    public static string ByteToHex(byte oByte)
    {
      if (oByte == 0)
        return "00";
      var sResult = Convert.ToString(oByte, 16);
      if (sResult.Length == 1)
        sResult = "0" + sResult;
      return sResult;
    }

    public static Color ParseARGB(String argbString)
    {
      var oResult = Color.Empty;
      if (argbString == String.Empty) return Color.Empty;

      try
      {
        if (argbString.Length == 8)
        {
          int nAlpha = Convert.ToInt16(argbString.Substring(0, 2), 16);
          int nRed = Convert.ToInt16(argbString.Substring(2, 2), 16);
          int nGreen = Convert.ToInt16(argbString.Substring(4, 2), 16);
          int nBlue = Convert.ToInt16(argbString.Substring(6, 2), 16);
          oResult = Color.FromArgb(nAlpha, nRed, nGreen, nBlue);
        }
        else if (argbString.Length == 6)
        {
          int nRed = Convert.ToInt16(argbString.Substring(0, 2), 16);
          int nGreen = Convert.ToInt16(argbString.Substring(2, 2), 16);
          int nBlue = Convert.ToInt16(argbString.Substring(4, 2), 16);
          oResult = Color.FromArgb(255, nRed, nGreen, nBlue);
        }
        else
        {
          throw new Exception("The Color string has to be in ARGB");
        }
      }
      catch (Exception)
      {
      }
      return oResult;
    }

    public static DmpPalette FromFile(string fileName)
    {
      var lines = File.ReadAllLines(fileName);
      var result = new DmpPalette();
      foreach (var line in lines)
      {
        try
        {
          var color = ParseARGB(line.Replace("#", ""));
          result.Colors.Add(color);
        }
        catch
        {
        }
      }
      return result;
    }
  }
}