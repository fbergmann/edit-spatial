using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibEditSpatial
{
  public static class Util
  {
    public static string ToCygwin(string dir)
    {
      dir = dir.Replace("\\", "/");
      int separator = dir.IndexOf(':');
      if (separator == -1) return dir;
      var drive = dir.Substring(0, separator);
      var sub = dir.Substring(separator + 1);
      return "/cygdrive/" + drive + sub;
    }
  }
}
