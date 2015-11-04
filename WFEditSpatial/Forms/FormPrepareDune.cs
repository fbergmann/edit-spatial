using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using EditSpatial.Model;
using LibEditSpatial.Dialogs;
using LibEditSpatial.Model;

namespace EditSpatial.Forms
{
  public partial class FormPrepareDune : Form
  {
    private bool closing;
    private Process process;

    public FormPrepareDune()
    {
      InitializeComponent();
    }

    public string BuildDir
    {
      get { return Path.Combine(Target, "build"); }
    }

    public string Target
    {
      get
      {
        return Path.Combine(TargetDir, ModuleName);
      }
    }

    public string TargetDir
    {
      get { return txtTargetDir.Text; }
      set { txtTargetDir.Text = value; }
    }

    public string ModuleName
    {
      get { return txtName.Text; }
      set { txtName.Text = value; }
    }

    public EditSpatialSettings Settings { get; set; }
    public SpatialModel Model { get; set; }

    private void OnBrowseClicke(object sender, EventArgs e)
    {
      txtTargetDir.Text = LibEditSpatial.Util.GetDir(txtTargetDir.Text);
    }

    private void DoCreateHost()
    {
      Util.UnzipArchive(
        Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "host.zip"), Target);
      txtResult.Text = "Host created in: " + Target;
    }

    private void OnCreateHostClick(object sender, EventArgs e)
    {
      if (Directory.Exists(Target))
      {
        var result = MessageBox.Show(
          string.Format(
            "The target directory '{0}' already consists if you continue, do you want to delete the directory before continuing?",
            Target),
          "Target dir exists", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

        if (result != DialogResult.Yes)
          return;

        try
        {
          Directory.Delete(Target, true);
        }
        catch
        {
        }
      }
      DoCreateHost();
    }

    private async Task RunCMake(string buildDir)
    {
      if (process != null) return;

      var info = new ProcessStartInfo
      {
        FileName = Path.Combine(Settings.CygwinDir, "bash"),
        Arguments = string.Format(
          "-c \"cmake -DCMAKE_BUILD_TYPE=Release -DCMAKE_INSTALL_PREFIX={0} {1}\"",
          LibEditSpatial.Util.ToCygwin(Settings.DuneDir),
          LibEditSpatial.Util.ToCygwin(Target)
          ),
        WorkingDirectory = buildDir,
        RedirectStandardError = true,
        RedirectStandardOutput = true,
        UseShellExecute = false,
        CreateNoWindow = true
      };

#pragma warning disable 4014
      await StartProcess(info);
#pragma warning restore 4014
    }

    private async Task RunMake(string buildDir)
    {
      if (process != null) return;

      var info = new ProcessStartInfo
      {
        FileName = Path.Combine(Settings.CygwinDir, "bash"),
        Arguments =
          "-c make",
        WorkingDirectory = buildDir,
        RedirectStandardError = true,
        RedirectStandardOutput = true,
        UseShellExecute = false,
        CreateNoWindow = true
      };


#pragma warning disable 4014
      await StartProcess(info);
#pragma warning restore 4014

      DlgRun.WriteBatchFiles(buildDir, Settings.CygwinDir);

    }

    private void ReEnableUI()
    {
      process = null;
    }

    private void OnAddString(string data)
    {
      if (closing) return;
      if (string.IsNullOrEmpty(data)) return;

      if (InvokeRequired)
      {
        Invoke(new VoidStringDelegate(OnAddString), data);
        return;
      }
      txtResult.AppendText(data.Replace("\n", Environment.NewLine) + Environment.NewLine);
    }

    public async Task StartProcess(ProcessStartInfo info)
    {
      if (!string.IsNullOrEmpty(Settings.CygwinDir))
        info.EnvironmentVariables["PATH"] = Settings.CygwinDir + ";" + info.EnvironmentVariables["PATH"];

      process = new Process {StartInfo = info, EnableRaisingEvents = true};
      process.Exited += (o, e1) => ReEnableUI();
      process.OutputDataReceived += (o, e2) => OnAddString(e2.Data);
      process.ErrorDataReceived += (o, e3) => OnAddString(e3.Data);
      process.Start();
      Thread.Sleep(100);
      process.BeginOutputReadLine();
      process.BeginErrorReadLine();

      await Task.Run(() => process.WaitForExit());
    }

    private void DoCompile()
    {
      // create build dir
      if (!Directory.Exists(BuildDir))
        Directory.CreateDirectory(BuildDir);
      // run cmake
      
      Task.Factory.StartNew(() => RunCMake(BuildDir)
        .ContinueWith(prevTask => RunMake(BuildDir)));
    }

    private void OnCompileModel(object sender, EventArgs e)
    {
      if (!Directory.Exists(Target))
      {
        MessageBox.Show("Please create hosting application first", "Hosting application not present.",
          MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }
      txtResult.Text = "Compiling Model";
      DoCompile();
    }

    private void DoExport()
    {
      // export model into host/src
      Model.ExportToDune(Path.Combine(Path.Combine(Target, "src"), "host.cc"));
      txtResult.Text = "Dune model exported into: " + Path.Combine(Target, "src");
    }

    private void OnExportModel(object sender, EventArgs e)
    {
      if (!Directory.Exists(Target))
      {
        MessageBox.Show("Please create hosting application first", "Hosting application not present.",
          MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }
      DoExport();
    }

    private void OnFormClosing(object sender, FormClosingEventArgs e)
    {
      closing = true;
    }

    private DuneConfig GetConfigFromDir(string directory)
    {
      if (!Directory.Exists(directory)) return null;
      var files = Directory.GetFiles(directory, "*.conf", SearchOption.TopDirectoryOnly);
      if (files.Length > 0)
      {
        var file = files.FirstOrDefault(f => f.Contains("temp.conf"));
        if (string.IsNullOrEmpty(file))
          file = files[0];
        return DuneConfig.FromFile(file);
      }
      return null;
    }

    private void OnRunClick(object sender, EventArgs e)
    {
      if (!Directory.Exists(Target))
      {
        MessageBox.Show("Please create hosting application first", "Hosting application not present.",
          MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }
      if (!Directory.Exists(BuildDir))
      {
        MessageBox.Show("Please compile application first", "Application not compiled.",
          MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }

      var runDir = Path.Combine(BuildDir, "src");
      var config = GetConfigFromDir(runDir);
      if (config != null)
        using (var dlg = new DlgRun
        {
          CygwinDir = Settings.CygwinDir,
          ParaViewDir = Settings.ParaViewDir,
          Config = config,
          FileName = Directory.GetFiles(runDir, "*.exe", SearchOption.TopDirectoryOnly).FirstOrDefault()
        })
        {
          dlg.ShowDialog(this);
        }
    }

    private void OnEditConfig(object sender, EventArgs e)
    {
      if (!Directory.Exists(Target))
      {
        MessageBox.Show("Please create hosting application first", "Hosting application not present.",
          MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }
      if (!Directory.Exists(Path.Combine(Target, "build")))
      {
        MessageBox.Show("Please compile application first", "Application not compiled.",
          MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }
      var runDir = Path.Combine(BuildDir, "src");
      var config = GetConfigFromDir(runDir);
      if (config != null)
        using (var dlg = new WFDuneRunner.MainForm())
        {
          dlg.OpenFile(config.FileName);
          dlg.ShowDialog(this);
        }
    }

    private void cmdFolder_Click(object sender, EventArgs e)
    {
      try
      {
        Process.Start(Path.Combine(txtTargetDir.Text, txtName.Text));
      }
      catch
      {
      }
    }
  }
}