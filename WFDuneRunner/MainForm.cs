using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WFDuneRunner.Dialogs;
using WFDuneRunner.Model;

namespace WFDuneRunner
{
  public partial class MainForm : Form
  {
    public MainForm()
    {
      InitializeComponent();
    }

    public DuneConfig Config {get; set;}

    public string FileName { get; set; }

    public void LoadFile(string filename)
    {
      FileName = filename;
      Config = DuneConfig.FromFile(filename);
      UpdateUI();
    }

    private string ConvertBCType(string bcString)
    {
      switch (bcString)
      {
        case "Dirichlet":
          return "1";
        default:
        case "Neumann":
          return "-1";
        case "Outflow":
          return "-2";
        case "None":
          return "-3";
      }
    }
    private string GetBcType(string bcCode)
    {
      switch(bcCode)
      {
        case "1":
          return "Dirichlet";
        default:
        case "-1":
          return "Neumann";
        case "-2":
          return "Outflow";
        case "-3":
          return "None";
      }
    }
    internal void UpdateUI()
    {
      if (string.IsNullOrWhiteSpace(FileName))
        Text = "Dune Runner";
      else
        Text = string.Format("Dune Runner [{0}]", Path.GetFileName(FileName));

      ctrlTime1.LoadModel(Config.TimeConfig);
      ctrlDomain1.LoadModel(Config.DomainConfig);
      ctrlNewton1.LoadModel(Config.NewtonConfig);
      ctrlGlobal1.LoadModel(Config.GlobalConfig);

      _isLoading = true;
      gridParameters.Rows.Clear();
      var parameters = Config["Reaction"];
      foreach(var item in parameters)
      {
        gridParameters.Rows.Add(item.Key, item.Value);
      }

      gridVariables.Rows.Clear();
      var data = Config["Data"];
      var varKeys = Config.GetVariableKeys();
      foreach (var item in varKeys)
      {
        var entries = Config[item];
        gridVariables.Rows.Add(item, 
          entries["D"],
          entries.ContainsKey("BCType") ?  GetBcType(entries["BCType"]) : "Neumann", 
          entries.ContainsKey("Xmin") ? entries["Xmin"] : "0",
          entries.ContainsKey("Xmax") ? entries["Xmax"] : "0",
          entries.ContainsKey("Ymin") ? entries["Ymin"] : "0",
          entries.ContainsKey("Xmax") ? entries["Xmax"] : "0", 
          (data != null && data.ContainsKey(item))? data[item] : ""
          );
      }

      _isLoading = false;

    }

    private void OnNewFile(object sender, EventArgs e)
    {
      FileName = null;
      Config = new DuneConfig();
      UpdateUI();
    }

    private void OnOpenFile(object sender, EventArgs e)
    {
      using (var dialog = new OpenFileDialog{ Title = "Open Config", Filter = "Config files|*.conf|All files|*.*" })
      {
        if (dialog.ShowDialog() == DialogResult.OK)
          LoadFile(dialog.FileName);
      }
    }

    private void OnSaveFile(object sender, EventArgs e)
    {
      // SaveAs for now
      // otherwise
      // SaveFile(FileName);
      OnSaveAs(sender, e);
    }

    private void OnAbout(object sender, EventArgs e)
    {

    }

    private void SaveFile(string fileName)
    {      
      Config.SaveAs(fileName);
      FileName = fileName;
      UpdateUI();
    }
    private void OnSaveAs(object sender, EventArgs e)
    {
      using (var dialog = new SaveFileDialog { Title = "Save Config", Filter = "Config files|*.conf|All files|*.*" })
      {
        if (dialog.ShowDialog() == DialogResult.OK)
          SaveFile(dialog.FileName);
      }
    }

    private void OnExit(object sender, EventArgs e)
    {
      Close();
    }

    private void OnRunClick(object sender, EventArgs e)
    {
      var path = Path.GetDirectoryName(FileName);
      if (path == null) return;
      var files = Directory.GetFiles(path, "*.exe");
      using (var dialog = new DlgRun {
        Config = Config, 
        FileName = files.Length > 0 ? files[0] : path
      })
      {
        dialog.ShowDialog();
      }
    }

    bool _isLoading;


    private void OnVariableChanged(object sender, DataGridViewCellEventArgs e)
    {
      if (_isLoading) return;

      if (e.RowIndex < 0) return;

      var row = gridVariables.Rows[e.RowIndex];
      if (row.IsNewRow) return;

      string id = row.Cells[0].Value as string;
      var entry = Config[id];
      var data = Config["Data"];

      switch (e.ColumnIndex)
      {
        case 1:
          {
            entry["D"] = row.Cells[1].Value as string;
            break;
          }
        case 2:
          {
            entry["BCType"] = ConvertBCType(row.Cells[2].Value as string);
            break;
          }
        case 3:
          {
            entry["Xmin"] = row.Cells[3].Value as string;
            break;
          }
        case 4:
          {
            entry["Xmax"] = row.Cells[4].Value as string;
            break;
          }
        case 5:
          {
            entry["Ymin"] = row.Cells[5].Value as string;
            break;
          }
        case 6:
          {
            entry["Ymax"] = row.Cells[6].Value as string;
            break;
          }
        case 7:
          {
            data[id] = row.Cells[7].Value as string;
            break;
          }
        default:
          break;
      }
    }

    private void OpenDmpFile(string file)
    {
      if (string.IsNullOrWhiteSpace(file)) return;

      if (!File.Exists(file))
      {
        file = Path.Combine(Path.GetDirectoryName(FileName), file);
        if (!File.Exists(file)) return;
      }
      try
      {
        Process.Start(file);
      }
      catch
      {

      }
    }
    private void gridVariables_CellClick(object sender, DataGridViewCellEventArgs e)
    {
      if (_isLoading) return;

      if (e.RowIndex < 0) return;

      var row = gridVariables.Rows[e.RowIndex];
      if (row.IsNewRow) return;

      switch(e.ColumnIndex)
      {
        case 8: // open file
          OpenDmpFile(row.Cells[7].Value as string);
          break;
        case 9: // browse file
          {
            using (var dlg = new OpenFileDialog{ Title= "Open file", Filter = "DMP files|*.dmp|All files|*.*"})
            {
              if (dlg.ShowDialog() == DialogResult.OK)
                row.Cells[7].Value = dlg.FileName;
            }
            break;
          }
        default:
          break;
      }

      

    }

  }
}
