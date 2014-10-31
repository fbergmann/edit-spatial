using System.Collections.Generic;

namespace LibEditSpatial.Model
{
  public static class StaticHelper
  {
    public static bool GetBool(this Dictionary<string, string> dict, string option)
    {
      if (!dict.ContainsKey(option)) return false;
      switch (dict[option].ToLowerInvariant())
      {
        case "yes":
        case "on":
          return true;
        default:
          return false;
      }
    }

    public static string GetString(this Dictionary<string, string> dict, string option)
    {
      if (!dict.ContainsKey(option)) return "";
      return dict[option];
    }


    public static T Get<T>(this Dictionary<string, string> dict, string option, T defaultValue = default(T))
    {
      if (!dict.ContainsKey(option)) return defaultValue;

      switch (typeof (T).ToString())
      {
        case "System.Boolean":
          return (T) (object) GetBool(dict, option);

        case "System.Int16":
        case "System.Int32":
        case "System.Int64":
        {
          int temp;
          if (int.TryParse(dict[option], out temp))
            return (T) (object) temp;
          break;
        }

        case "System.Single":
        {
          float temp;
          if (float.TryParse(dict[option], out temp))
            return (T) (object) temp;
          break;
        }

        case "System.Double":
        {
          double temp;
          if (double.TryParse(dict[option], out temp))
            return (T) (object) temp;
          break;
        }
        case "System.String":
        {
          return (T) (object) dict[option];
        }
        default:
          return defaultValue;
      }
      return defaultValue;
    }
  }
}