namespace LibEditSpatial
{
  public static class Util
  {
    public static string ToCygwin(string dir)
    {
      dir = dir.Replace("\\", "/");
      var separator = dir.IndexOf(':');
      if (separator == -1) return dir;
      var drive = dir.Substring(0, separator);
      var sub = dir.Substring(separator + 1);
      return string.Format("/cygdrive/{0}{1}", drive, sub);
    }
  }
}