using System;
using System.IO;
using System.Windows.Forms;

namespace WFDuneRunner
{
  internal static class Program
  {
    /// <summary>
    ///   The main entry point for the application.
    /// </summary>
    [STAThread]
    private static void Main(string[] args)
    {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      var form = new MainForm();

      foreach (var file in args)
      {
        if (File.Exists(file))
        {
          form.OpenFile(file);
          break;
        }
      }

      Application.Run(form);
    }
  }
}