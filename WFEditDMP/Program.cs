using System;
using System.IO;
using System.Windows.Forms;

namespace WFEditDMP
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

      foreach (string item in args)
        if (File.Exists(item))
          form.OpenFile(item);

      Application.Run(form);
    }
  }
}