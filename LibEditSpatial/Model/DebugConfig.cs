using System.Collections.Generic;

namespace LibEditSpatial.Model
{
  public class DebugConfig
  {
    public int Verbosity { get; set; }
    public int Instationary { get; set; }

    public static DebugConfig FromDict(Dictionary<string, string> dict)
    {
      var result = new DebugConfig
      {
        Verbosity = dict.Get<int>("verbosity"),
        Instationary = dict.Get<int>("Instationary")
      };
      return result;
    }

    public Dictionary<string, string> ToDict()
    {
      var result = new Dictionary<string, string>();
      result["verbosity"] = Verbosity.ToString();
      result["Instationary"] = Instationary.ToString();
      return result;
    }
  }
}