using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WFDuneRunner
{
  static class Program
  {
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main(string[] args)
    {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      MainForm form = new MainForm();

      foreach (var file in args)
      {
        if (File.Exists(file))
          form.LoadFile(file);
      }

      Application.Run(form);
    }
  }
}
