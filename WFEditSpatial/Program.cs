using System;
using System.IO;
using System.Windows.Forms;
using SBW;

namespace EditSpatial
{
  internal static class Program
  {
    private static object module;

    /// <summary>
    ///   The main entry point for the application.
    /// </summary>
    [STAThread]
    private static void Main(string[] args)
    {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      var mainForm = new MainForm();
      try
      {
        module = mainForm;
        DefaultModule.EnableServices(ref module,
          "EditSpatial", // module name
          "Edit Spatial", // display name
          "Creates / Edits spatial models",
          LowLevel.ModuleManagementType.UniqueModule,
          "analysis", // service name
          "Create/Edit Spatial Description", // service display name
          "/Analysis", // category
          "Creates / Edits spatial models", //
          args
          );
      }
      catch
      {
      }

      if (args.Length == 1 && File.Exists(args[0]))
        mainForm.OpenFile(args[0]);

      Application.Run(mainForm);
    }
  }
}