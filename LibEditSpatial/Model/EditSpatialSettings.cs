using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;

namespace LibEditSpatial.Model
{
  [Serializable]
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

    private static EditSpatialSettings _Instance;
    public static EditSpatialSettings Instance
    {
      get
      {
        if (_Instance == null)
          _Instance = GetDefault(new Size(1000, 600));
        return _Instance;
      }
      set
      {
        _Instance = value;
        _Instance.Save();
      }
    }

    public static string ConfigFile
    {
      get
      {
        return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "spatial_tools.xml");
      }
    }

    public void WriteXmlToFile(string fileName)
    {
      var serializer = new System.Xml.Serialization.XmlSerializer(this.GetType());
      using (var stream = new StringWriter())
      {
        serializer.Serialize(stream, this);
        stream.Flush();
        File.WriteAllText(fileName, stream.ToString());
      }
    }


    public static EditSpatialSettings FromXmlFile(string fileName)
    {
      try
      {
        var serializer = new System.Xml.Serialization.XmlSerializer(typeof(EditSpatialSettings));
        var text = File.ReadAllText(fileName);
        using (var stringReader = new StringReader(text))
        {
          return (EditSpatialSettings)serializer.Deserialize(stringReader);
        }
      }
      catch (Exception ex)
      {
        Debug.WriteLine("Could not load settings: " + ex.Message + "\n\n" + ex.StackTrace);
        return null;
      }
    }

    public static EditSpatialSettings GetDefault(Size current)
    {
      var key = FromXmlFile(ConfigFile);
      if (key == null)
        return new EditSpatialSettings
        {
          Height = current.Height,
          Width = current.Width
        };
      //var result = new EditSpatialSettings
      //{
      //  LastDir = (string) key.GetValue("lastDir", ""),
      //  CygwinDir = (string) key.GetValue("cygwinDir", ""),
      //  DuneDir = (string) key.GetValue("duneDir", ""),
      //  ParaViewDir = (string) key.GetValue("paraviewDir", ""),
      //  DefaultDir = (string) key.GetValue("defaultDir", ""),
      //  IgnoreMultiCompartments = (int) key.GetValue("ignoreComps", 0) == 1,
      //  Height = (int) key.GetValue("height", current.Height),
      //  Width = (int) key.GetValue("width", current.Width)
      //};
      return key;
    }

    public void Save()
    {
      WriteXmlToFile(ConfigFile);
      //var key = Application.UserAppDataRegistry;
      //if (key == null) return;
      //
      //key.SetValue("lastDir", LastDir);
      //key.SetValue("cygwinDir", CygwinDir);
      //key.SetValue("duneDir", DuneDir);
      //key.SetValue("defaultDir", DefaultDir);
      //key.SetValue("paraviewDir", ParaViewDir);
      //key.SetValue("ignoreComps", IgnoreMultiCompartments ? 1 : 0);
      //key.SetValue("height", Height);
      //key.SetValue("width", Width);
    }
  }
}