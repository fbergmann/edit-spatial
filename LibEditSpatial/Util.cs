using System.Windows.Forms;
using Ookii.Dialogs;

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

    public static string GetDir(string baseDir)
    {
      using (var dlg = new VistaFolderBrowserDialog { SelectedPath = baseDir, Description = "Open Directory" })
      {
        if (dlg.ShowDialog() == DialogResult.OK)
          return dlg.SelectedPath;
      }
      return baseDir;
    }
  }
}