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

      foreach (var item in args)
        if (File.Exists(item))
        {
          form.OpenFile(item);
          break;
        }

      Application.Run(form);
    }
  }
}