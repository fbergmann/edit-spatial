using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using EditSpatial.Converter;
using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;
using libsbmlcs;
using SysBio.MathKGI;
using Image = System.Drawing.Image;

namespace EditSpatial.Model
{
  public class EditSpatialSettings
  {

    public string LastDir { get; set; }

    public string CygwinDir { get; set; }
    public string DuneDir { get; set; }
    public string ParaViewDir { get; set; }
    public string DefaultDir { get; set; }

    public bool IgnoreMultiCompartments { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }

    public static EditSpatialSettings GetDefault(Size current)
    {
        var key = Application.UserAppDataRegistry;
        if (key == null) return new EditSpatialSettings();
        var result = new EditSpatialSettings
        {
          LastDir = (string)key.GetValue("lastDir", ""),
          CygwinDir = (string)key.GetValue("cygwinDir", ""),
          DuneDir= (string)key.GetValue("duneDir", ""),
          ParaViewDir= (string)key.GetValue("paraviewDir", ""),
          DefaultDir= (string)key.GetValue("defaultDir", ""),
          IgnoreMultiCompartments = (int)key.GetValue("ignoreComps", 0) == 1,
          Height = (int)key.GetValue("height", current.Height), 
          Width = (int)key.GetValue("width", current.Width)
        };
        return result;
    }

    public void Save()
    {
      var key = Application.UserAppDataRegistry;
      if (key == null) return;

      key.SetValue("lastDir", LastDir);
      key.SetValue("cygwinDir", CygwinDir);
      key.SetValue("duneDir", DuneDir);
      key.SetValue("defaultDir", DefaultDir);
      key.SetValue("paraviewDir", ParaViewDir);
      key.SetValue("ignoreComps", IgnoreMultiCompartments ? 1 : 0);
      key.SetValue("height", Height);
      key.SetValue("width", Width);
    }

    public EditSpatialSettings()
    {

    }
  }
}
