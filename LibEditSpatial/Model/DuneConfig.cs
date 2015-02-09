using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LibEditSpatial.Model
{
  public class DuneConfig
  {
    internal const string STR_GLOBAL = "Global";
    internal DomainConfig _DomainConfig;
    internal GlobalConfig _GlobalConfig;
    internal NewtonConfig _NewtonConfig;
    internal TimeLoopConfig _TimeConfig;

    /// <summary>
    ///   Initializes a new instance of the DuneConfig class.
    /// </summary>
    public DuneConfig()
    {
      Entries = new Dictionary<string, Dictionary<string, string>>();
    }

    public string FileName { get; set; }
    public Dictionary<string, Dictionary<string, string>> Entries { get; set; }

    public GlobalConfig GlobalConfig
    {
      get
      {
        if (_GlobalConfig == null)
        {
          if (Entries.ContainsKey(STR_GLOBAL))
            _GlobalConfig = GlobalConfig.FromDict(Entries[STR_GLOBAL]);
          else
            _GlobalConfig = GlobalConfig.Default;
        }

        return _GlobalConfig;
      }
      set
      {
        _GlobalConfig = value;
        Entries.Remove(STR_GLOBAL);
        Entries.Add(STR_GLOBAL, value.ToDict());
      }
    }

    public NewtonConfig NewtonConfig
    {
      get
      {
        if (_NewtonConfig == null)
        {
          if (Entries.ContainsKey("Newton"))
            _NewtonConfig = NewtonConfig.FromDict(Entries["Newton"]);
          else
            _NewtonConfig = NewtonConfig.Default;
        }

        return _NewtonConfig;
      }
      set
      {
        _NewtonConfig = value;
        Entries.Remove("Newton");
        Entries.Add("Newton", value.ToDict());
      }
    }

    public DomainConfig DomainConfig
    {
      get
      {
        if (_DomainConfig == null)
        {
          if (Entries.ContainsKey("Domain"))
            _DomainConfig = DomainConfig.FromDict(Entries["Domain"]);
          else
            _DomainConfig = DomainConfig.Default;
        }

        return _DomainConfig;
      }
      set
      {
        _DomainConfig = value;
        Entries.Remove("Domain");
        Entries.Add("Domain", value.ToDict());
      }
    }

    public TimeLoopConfig TimeConfig
    {
      get
      {
        if (_TimeConfig == null)
        {
          if (Entries.ContainsKey("Timeloop"))
            _TimeConfig = TimeLoopConfig.FromDict(Entries["Timeloop"]);
          else
            _TimeConfig = TimeLoopConfig.Default;
        }

        return _TimeConfig;
      }
      set
      {
        _TimeConfig = value;
        Entries.Remove("Timeloop");
        Entries.Add("Timeloop", value.ToDict());
      }
    }

    public Dictionary<string, string> this[string key]
    {
      get
      {
        if (!Entries.ContainsKey(key))
          Entries[key] = new Dictionary<string, string>();
        return Entries[key];
      }
    }

    public List<string> GetVariableKeys()
    {
      var result = new List<string>(Entries.Keys);
      result.Remove(STR_GLOBAL);
      result.Remove("Timeloop");
      result.Remove("Verbosity");
      result.Remove("Newton");
      result.Remove("Data");
      result.Remove("Reaction");
      result.Remove("Domain");
      return result;
    }

    private void WriteSection(StringBuilder builder, string key, Dictionary<string, string> map, string basedir = null)
    {
      if (!string.IsNullOrWhiteSpace(key))
        builder.AppendFormat("[{0}]{1}", key, Environment.NewLine);

      foreach (var item in map)
      {
        var val = item.Value.Trim();
        if (!string.IsNullOrWhiteSpace(basedir) && val.StartsWith(basedir))
          val = val.Replace(basedir, "");

        builder.AppendFormat("{0} = {1}{2}", item.Key, val, Environment.NewLine);
      }

      builder.AppendLine();
    }

    public void SaveAs(string fileName)
    {
      var baseDir = Path.GetDirectoryName(fileName);

      var builder = new StringBuilder();
      builder.AppendLine("# Dune Config file");
      builder.AppendLine("# written: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
      builder.AppendLine();

      WriteSection(builder, "", GlobalConfig.ToDict(), baseDir);
      WriteSection(builder, "Newton", NewtonConfig.ToDict(), baseDir);
      if (!Entries.ContainsKey("Verbosity"))
      {
        var tmp = new Dictionary<string, string>();
        tmp["verbosity"] = "0";
        tmp["Instationary"] = "0";
        Entries.Add("Verbosity", tmp);
      }

      WriteSection(builder, "Verbosity", Entries["Verbosity"], baseDir);
      WriteSection(builder, "Timeloop", TimeConfig.ToDict(), baseDir);
      WriteSection(builder, "Domain", DomainConfig.ToDict(), baseDir);
      WriteSection(builder, "Reaction", Entries["Reaction"], baseDir);
      foreach (var item in GetVariableKeys())
        WriteSection(builder, item, Entries[item], baseDir);
      if (Entries.ContainsKey("Data"))
        WriteSection(builder, "Data", Entries["Data"], baseDir);

      File.WriteAllText(fileName, builder.ToString());

      FileName = fileName;
    }

    public static DuneConfig FromFile(string filename)
    {
      var lines = File.ReadAllLines(filename);

      var result = new DuneConfig {FileName = filename};

      var current = STR_GLOBAL;
      var entries = new Dictionary<string, string>();

      foreach (var line in lines)
      {
        // skip comments
        var trimmed = line.Trim();

        // remove comment at the end
        var commentIndex = trimmed.IndexOf("#");
        if (commentIndex != -1)
        {
          trimmed = trimmed.Substring(0, commentIndex).Trim();
        }

        if (trimmed.StartsWith("#")) continue;

        if (trimmed.StartsWith("[") && trimmed.EndsWith("]"))
        {
          result.Entries.Add(current, entries);
          current = trimmed.Replace("[", "").Replace("]", "");
          entries = new Dictionary<string, string>();
          continue;
        }

        var pair = trimmed.Split(new[] {'='}, StringSplitOptions.RemoveEmptyEntries);
        if (pair.Length > 0)
        {
          var key = pair[0].Trim();
          if (!string.IsNullOrWhiteSpace(key))
            if (pair.Length >= 2)
              entries[key] = pair[1].Trim();
            else
              entries[key] = "";
        }
      }

      result.Entries.Add(current, entries);

      return result;
    }
  }
}