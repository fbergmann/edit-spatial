using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using LibEditSpatial.Model;

namespace LibEditSpatial.Dialogs
{
  public partial class DlgRun : Form
  {
    private bool closing;
    private Process process;
    private FileSystemWatcher watcher;

    public DlgRun()
    {
      InitializeComponent();
      closing = false;
      CygwinDir = @"C:\cygwin\bin";
      ParaViewDir = @"E:\Program Files (x86)\ParaView 4.2.0\bin\";
    }

    public string CygwinDir { get; set; }
    public string ParaViewDir { get; set; }

    public DuneConfig Config { get; set; }

    public string FileName
    {
      get { return txtFileName.Text; }
      set { txtFileName.Text = value; }
    }

    private void OnKillClick(object sender, EventArgs e)
    {
      if (process == null) return;
      try
      {
        process.Kill();
      }
      catch
      {
      }
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
      txtResult.Text += data.Replace("\n", Environment.NewLine);
    }

    private void ClearWatcher()
    {
      if (watcher != null)
      {
        watcher.EnableRaisingEvents = false;
        watcher.Dispose();
        watcher = null;
      }
    }

    private void ReEnableUI()
    {
      if (closing) return;
      if (InvokeRequired)
      {
        Invoke(new VoidDelegate(ReEnableUI));
        return;
      }
      progressBar1.Style = ProgressBarStyle.Continuous;
      cmdRun.Enabled = true;
      cmdCompile.Enabled = true;
      cmdKill.Enabled = false;

      ClearWatcher();

      process = null;
    }

    private bool CheckIfEmpty(string dir)
    {
      string outdir = Path.Combine(dir, "vtk");
      if (!Directory.Exists(outdir)) return false;
      string[] files = Directory.GetFiles(outdir, "*.vt?", SearchOption.TopDirectoryOnly);
      if (files.Length == 0) return false;

      DialogResult result =
        MessageBox.Show("There are already results present from a previous simulation, shall I move them away?",
          "Move Existing results?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
      if (result == DialogResult.Yes)
      {
        DateTime now = DateTime.Now;
        string newDir = Path.Combine(dir, now.ToString("yyyy-MM-dd_-_HH-mm-ss") + "_old_results");
        Directory.CreateDirectory(newDir);
        Directory.Move(outdir, Path.Combine(newDir, "vtk"));
        files = Directory.GetFiles(dir, "*.pvd");
        foreach (string item in files)
        {
          string name = Path.GetFileName(item);
          if (name == null) continue;
          File.Move(item, Path.Combine(newDir, name));
        }
      }

      if (result == DialogResult.Cancel)
        return true;

      return false;
    }

    private void OnRunClick(object sender, EventArgs e)
    {
      if (process != null) return;

      string dir = Path.GetDirectoryName(FileName);
      if (dir == null) return;

      string outdir = Path.Combine(dir, "vtk");

      if (CheckIfEmpty(dir)) return;


      if (!Directory.Exists(outdir))
        Directory.CreateDirectory(outdir);
      string config = Path.Combine(dir, "temp.conf");
      Config.SaveAs(config);

      var info = new ProcessStartInfo
      {
        FileName = FileName,
        Arguments = Util.ToCygwin(config),
        WorkingDirectory = dir,
        RedirectStandardError = true,
        RedirectStandardOutput = true,
        UseShellExecute = false,
        CreateNoWindow = true
      };

#pragma warning disable 4014
      StartProcess(info);
#pragma warning restore 4014
    }

    private void OnNewResult(object sender, FileSystemEventArgs e)
    {
      if (closing) return;
      if (InvokeRequired)
      {
        Invoke(new FileSystemEventHandler(OnNewResult), new[] {sender, e});
        return;
      }

      if (!chkPreview.Checked || cmbVariable.Items.Count == 0) return;
#pragma warning disable 4014
      GeneratePreview(e.FullPath, cmbVariable.SelectedItem as string);
#pragma warning restore 4014
    }

    private void UpdatePreview(byte[] bytes, string species)
    {
      if (InvokeRequired)
      {
        Invoke(new VoidByteStringDelegate(UpdatePreview), bytes, species);
        return;
      }
      if (!cmbVariable.SelectedItem.Equals(species))
        return;

      Image image;
      using (var stream = new MemoryStream(bytes))
      {
        image = Image.FromStream(stream);
      }
      pictureBox1.Image = image;
    }

    public async Task GeneratePreview(string filename, string species)
    {
      if (string.IsNullOrWhiteSpace(species)) return;
      if (!File.Exists(filename)) return;

      Debug.WriteLine("Generating Preview for: {0} from {1}", species, Path.GetFileNameWithoutExtension(filename));

      string tempFile = Path.Combine(Path.GetTempPath(),
        Path.GetRandomFileName() + ".png");

      await Task.Run(() => Thread.Sleep(1000));

      var info = new ProcessStartInfo
      {
        FileName = Path.Combine(ParaViewDir, "pvpython"),
        Arguments = string.Format("\"{0}\" \"{1}\" {2} \"{3}\"",
          Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "pvrender.py"),
          filename,
          species,
          tempFile
          ),
        UseShellExecute = false,
        CreateNoWindow = true,
        RedirectStandardError = true,
        RedirectStandardOutput = true,
      };


      await Task.Run(() =>
      {
        var previewProcess = new Process {StartInfo = info};
        previewProcess.OutputDataReceived += (o, e2) => OnAddString(e2.Data);
        previewProcess.ErrorDataReceived += (o, e3) => OnAddString(e3.Data);
        previewProcess.Start();
        previewProcess.EnableRaisingEvents = true;
        previewProcess.BeginOutputReadLine();
        previewProcess.BeginErrorReadLine();
        previewProcess.WaitForExit();
        previewProcess.EnableRaisingEvents = false;
      });

      if (File.Exists(tempFile))
      {
        byte[] bytes = await Task.Run(() => File.ReadAllBytes(tempFile));

        File.Delete(tempFile);

        UpdatePreview(bytes, species);
      }
    }


    public async Task StartProcess(ProcessStartInfo info)
    {
      if (!string.IsNullOrEmpty(CygwinDir))
        info.EnvironmentVariables["PATH"] = CygwinDir + ";" + info.EnvironmentVariables["PATH"];

      process = new Process {StartInfo = info, EnableRaisingEvents = true};
      process.Exited += (o, e1) => ReEnableUI();
      process.OutputDataReceived += (o, e2) => OnAddString(e2.Data);
      process.ErrorDataReceived += (o, e3) => OnAddString(e3.Data);
      process.Start();
      Thread.Sleep(100);
      process.BeginOutputReadLine();
      process.BeginErrorReadLine();
      cmdRun.Enabled = false;
      cmdCompile.Enabled = false;
      cmdKill.Enabled = true;
      progressBar1.Style = ProgressBarStyle.Marquee;

      ClearWatcher();

      watcher = new FileSystemWatcher
      {
        Path = Path.Combine(info.WorkingDirectory, "vtk"),
        IncludeSubdirectories = false,
        Filter = "*.vt?",
        NotifyFilter = NotifyFilters.FileName | NotifyFilters.LastWrite
      };

      watcher.Created += OnNewResult;
      watcher.Changed += OnNewResult;
      watcher.EnableRaisingEvents = true;

      await Task.Run(() => process.WaitForExit());
    }

    private void OnCompileClick(object sender, EventArgs e)
    {
      if (process != null) return;

      string dir = Path.GetDirectoryName(FileName);

      if (dir == null) return;

      var info = new ProcessStartInfo
      {
        FileName = Path.Combine(CygwinDir, "bash"),
        Arguments = "-c make",
        WorkingDirectory = dir,
        RedirectStandardError = true,
        RedirectStandardOutput = true,
        UseShellExecute = false,
        CreateNoWindow = true
      };

#pragma warning disable 4014
      StartProcess(info);
#pragma warning restore 4014
    }


    private void OnBrowseClick(object sender, EventArgs e)
    {
      using (var dialog = new OpenFileDialog
      {
        FileName = FileName,
        Title = "Select executable",
        Filter = "All files|*.*"
      })
      {
        if (dialog.ShowDialog() == DialogResult.OK)
          FileName = dialog.FileName;
      }
    }

    private void DlgRun_Load(object sender, EventArgs e)
    {
      cmbVariable.Items.Clear();
      if (Config != null)
      {
        foreach (string v in Config.GetVariableKeys())
          cmbVariable.Items.Add(v);
        if (cmbVariable.Items.Count > 0)
        {
          cmbVariable.SelectedIndex = 0;
          chkPreview.Checked = true;
        }
        else
          chkPreview.Checked = false;
      }

      if (File.Exists(FileName))
        OnRunClick(sender, e);
    }

    private void DlgRun_FormClosing(object sender, FormClosingEventArgs e)
    {
      if (process != null && !process.HasExited)
      {
        if (
          MessageBox.Show("The simulation is still running, do you want to kill it?", "Kill Simulation?",
            MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
        {
          if (process != null)
          {
            process.Kill();
          }

          ClearWatcher();

          closing = true;
        }
        else e.Cancel = true;
      }
    }

    private void OnOpenPV(object sender, EventArgs e)
    {
      try
      {
        string dir = Path.GetDirectoryName(FileName);
        if (dir == null) return;
        string[] pvd = Directory.GetFiles(dir, "*.pvd", SearchOption.TopDirectoryOnly);
        if (pvd.Length > 0)
          Process.Start(Path.Combine(ParaViewDir, "paraview"), pvd[0]);
      }
      catch
      {
      }
    }

  }
}