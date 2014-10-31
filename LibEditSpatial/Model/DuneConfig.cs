﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace WFDuneRunner.Model
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

    public Dictionary<string, Dictionary<string, string>> Entries { get; set; }

    public GlobalConfig GlobalConfig
    {
      get
      {
        if (_GlobalConfig == null)
          _GlobalConfig = GlobalConfig.FromDict(Entries[STR_GLOBAL]);
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
          _NewtonConfig = NewtonConfig.FromDict(Entries["Newton"]);
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
          _DomainConfig = DomainConfig.FromDict(Entries["Domain"]);
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
          _TimeConfig = TimeLoopConfig.FromDict(Entries["Timeloop"]);
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

    private void WriteSection(StringBuilder builder, string key, Dictionary<string, string> map)
    {
      if (!string.IsNullOrWhiteSpace(key))
        builder.AppendFormat("[{0}]{1}", key, Environment.NewLine);

      foreach (var item in map)
        builder.AppendFormat("{0} = {1}{2}", item.Key, item.Value, Environment.NewLine);

      builder.AppendLine();
    }

    public void SaveAs(string fileName)
    {
      var builder = new StringBuilder();
      builder.AppendLine("# Dune Config file");
      builder.AppendLine("# written: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
      builder.AppendLine();

      WriteSection(builder, "", GlobalConfig.ToDict());
      WriteSection(builder, "Newton", NewtonConfig.ToDict());
      if (!Entries.ContainsKey("Verbosity"))
      {
        var tmp = new Dictionary<string, string>();
        tmp["verbosity"] = "0";
        tmp["Instationary"] = "0";
        Entries.Add("Verbosity", tmp);
      }

      WriteSection(builder, "Verbosity", Entries["Verbosity"]);
      WriteSection(builder, "Timeloop", TimeConfig.ToDict());
      WriteSection(builder, "Domain", DomainConfig.ToDict());
      WriteSection(builder, "Reaction", Entries["Reaction"]);
      foreach (string item in GetVariableKeys())
        WriteSection(builder, item, Entries[item]);
      if (Entries.ContainsKey("Data"))
        WriteSection(builder, "Data", Entries["Data"]);

      File.WriteAllText(fileName, builder.ToString());
    }


    public static DuneConfig FromFile(string filename)
    {
      string[] lines = File.ReadAllLines(filename);

      var result = new DuneConfig();

      string current = STR_GLOBAL;
      var entries = new Dictionary<string, string>();

      foreach (string line in lines)
      {
        // skip comments
        string trimmed = line.Trim();

        // remove comment at the end
        int commentIndex = trimmed.IndexOf("#");
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

        string[] pair = trimmed.Split(new[] {'='}, StringSplitOptions.RemoveEmptyEntries);
        if (pair.Length > 0)
        {
          string key = pair[0].Trim();
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