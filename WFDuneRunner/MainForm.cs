using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using libsbmlcs;
using LibEditSpatial.Dialogs;
using LibEditSpatial.Model;

namespace WFDuneRunner
{
  public partial class MainForm : Form
  {
    private bool _isLoading;

    public MainForm()
    {
      InitializeComponent();

      ctrlDomain1.OpenDmpFile += (o, e) => OpenDmpFile(e);
      ctrlDomain1.BrowseDmpFile += (o, e) =>
      {
        string result;
        if (BrowseDmpFile(out result))
        {
          ctrlDomain1.Model.Geometry = result;
          ctrlDomain1.LoadModel(ctrlDomain1.Model);
        }
      };

      NewModel();
    }

    public DuneConfig Config { get; set; }
    public string FileName { get; set; }
    public string CurrentDir { get; set; }
    public Dictionary<string, string> VariableCompartmentMap { get; set; }
    public List<string> CompartmentIds { get; set; }

    public string ExecutableFileName
    {
      get
      {
        var path = Path.GetDirectoryName(FileName);
        if (path == null) return null;
        var files = Directory.GetFiles(path, "*.exe");
        if (files.Length == 0) return null;
        return files[0];
      }
    }

    public void LoadFile(string filename)
    {
      FileName = filename;
      CurrentDir = Path.GetDirectoryName(filename);

      Config = DuneConfig.FromFile(filename);
      VariableCompartmentMap = new Dictionary<string, string>();
      CompartmentIds = new List<string>();
      UpdateUI();
    }

    private string ConvertBCType(string bcString)
    {
      switch (bcString)
      {
        case "Dirichlet":
          return "1";
        default:
          //case "Neumann":
          return "-1";
        case "Outflow":
          return "-2";
        case "None":
          return "-3";
      }
    }

    private string GetBcType(string bcCode)
    {
      switch (bcCode)
      {
        case "1":
          return "Dirichlet";
        default:
          //case "-1":
          return "Neumann";
        case "-2":
          return "Outflow";
        case "-3":
          return "None";
      }
    }

    private void SetTitle()
    {
      if (string.IsNullOrWhiteSpace(FileName))
        Text = "Dune Runner";
      else
        Text = string.Format("Dune Runner [{0}]", Path.GetFileName(FileName));
    }

    internal void UpdateUI()
    {
      SetTitle();

      ctrlTime1.LoadModel(Config.TimeConfig);
      ctrlDomain1.LoadModel(Config.DomainConfig);
      ctrlNewton1.LoadModel(Config.NewtonConfig);
      ctrlGlobal1.LoadModel(Config.GlobalConfig);

      _isLoading = true;
      gridParameters.Rows.Clear();
      var parameters = Config["Reaction"];
      foreach (var item in parameters)
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
          entries.ContainsKey("BCType") ? GetBcType(entries["BCType"]) : "Neumann",
          entries.ContainsKey("Xmin") ? entries["Xmin"] : "0",
          entries.ContainsKey("Xmax") ? entries["Xmax"] : "0",
          entries.ContainsKey("Ymin") ? entries["Ymin"] : "0",
          entries.ContainsKey("Xmax") ? entries["Xmax"] : "0",
          (data != null && data.ContainsKey(item)) ? data[item] : ""
          );
      }

      cmdRun.Enabled = (!string.IsNullOrWhiteSpace(ExecutableFileName));

      var sbmlFile = Config.GlobalConfig.SBMLFile;
      if (!string.IsNullOrWhiteSpace(sbmlFile))
      {
        if (File.Exists(sbmlFile))
        {
          InitFromSBML(sbmlFile);
        }

        else if (File.Exists(Path.Combine(CurrentDir, sbmlFile)))
        {
          InitFromSBML(Path.Combine(CurrentDir, sbmlFile));
        }
      }

      _isLoading = false;
    }

    private void NewModel()
    {
      FileName = null;
      Config = new DuneConfig();
      UpdateUI();
    }

    private void OnNewFile(object sender, EventArgs e)
    {
      NewModel();
    }

    private void OnOpenFile(object sender, EventArgs e)
    {
      using (var dialog = new OpenFileDialog
      {
        Title = "Open Config",
        Filter = "Config files|*.conf|All files|*.*",
        InitialDirectory = CurrentDir
      })
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
      CurrentDir = Path.GetDirectoryName(fileName);
      UpdateUI();
    }

    private void OnSaveAs(object sender, EventArgs e)
    {
      using (var dialog = new SaveFileDialog
      {
        Title = "Save Config",
        Filter = "Config files|*.conf|All files|*.*",
        InitialDirectory = CurrentDir
      })
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
      var fileName = ExecutableFileName;
      if (fileName == null) return;

      using (var dialog = new DlgRun
      {
        Config = Config,
        FileName = fileName
      })
      {
        dialog.ShowDialog(this);
      }
    }

    private void OnVariableChanged(object sender, DataGridViewCellEventArgs e)
    {
      if (_isLoading) return;

      if (e.RowIndex < 0) return;

      var row = gridVariables.Rows[e.RowIndex];
      if (row.IsNewRow) return;

      var id = row.Cells[0].Value as string;
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
        using (var dlg = new WFEditDMP.MainForm())
        {
          dlg.OpenFile(file);
          dlg.StartPosition = FormStartPosition.CenterParent;
          dlg.ShowDialog(this);
        }
      }
      catch
      {
      }
    }

    private bool BrowseDmpFile(out string result)
    {
      result = String.Empty;
      var assign = false;
      using (var dlg = new OpenFileDialog
      {
        Title = "Open file",
        Filter = "DMP files|*.dmp|All files|*.*",
        InitialDirectory = CurrentDir
      })
      {
        if (dlg.ShowDialog() == DialogResult.OK)
        {
          var path = Path.GetDirectoryName(FileName);
          var filename = dlg.FileName;
          if (filename.StartsWith(path))
          {
            filename = filename.Replace(path, "");
            while (filename.StartsWith("\\"))
              filename = filename.Substring(1);
          }
          result = filename;
          assign = true;
        }
      }
      return assign;
    }

    private void OnVariableCellClick(object sender, DataGridViewCellEventArgs e)
    {
      if (_isLoading) return;

      if (e.RowIndex < 0) return;

      var row = gridVariables.Rows[e.RowIndex];
      if (row.IsNewRow) return;

      switch (e.ColumnIndex)
      {
        case 8: // open file
        {
          var file = row.Cells[7].Value as string;
          var id = row.Cells[0].Value as string;
          if (string.IsNullOrWhiteSpace(file))
          {
            var count = 1;
            var baseName = "ic_" + id;
            var name = baseName + ".dmp";

            var dir = Path.GetDirectoryName(FileName);
            while (File.Exists(Path.Combine(dir, name)))
            {
              name = string.Format("{0}_{1}.dmp", baseName, count++);
            }

            row.Cells[7].Value = name;
            var dmpFile = new DmpModel(Config.DomainConfig.GridX, Config.DomainConfig.GridY);
            file = Path.Combine(dir, name);
            dmpFile.SaveAs(file);
          }

          OpenDmpFile(file);
          break;
        }
        case 9: // browse file
        {
          string result;
          if (BrowseDmpFile(out result))
            row.Cells[7].Value = result;
          break;
        }
        default:
          break;
      }
    }

    private void OnCompartmentAssignmentChanged(object sender, DataGridViewCellEventArgs e)
    {
      if (_isLoading) return;

      if (e.RowIndex < 0) return;
    }

    private void OnCompartmentCellClick(object sender, DataGridViewCellEventArgs e)
    {
      if (_isLoading) return;

      if (e.RowIndex < 0) return;

      var row = gridCompartments.Rows[e.RowIndex];
      if (row.IsNewRow) return;

      switch (e.ColumnIndex)
      {
        case 2: // browse file
        {
          string result;
          if (BrowseDmpFile(out result))
            row.Cells[1].Value = result;
          break;
        }
        case 3: // view compartment
        {
          var file = row.Cells[1].Value as string;
          var id = row.Cells[0].Value as string;
          if (string.IsNullOrWhiteSpace(file))
          {
            var count = 1;
            var baseName = "inside_" + id;
            var name = baseName + ".dmp";

            var dir = Path.GetDirectoryName(FileName);
            while (File.Exists(Path.Combine(dir, name)))
            {
              name = string.Format("{0}_{1}.dmp", baseName, count++);
            }

            row.Cells[1].Value = name;
            var dmpFile = new DmpModel(Config.DomainConfig.GridX, Config.DomainConfig.GridY);
            file = Path.Combine(dir, name);
            dmpFile.SaveAs(file);
          }

          OpenDmpFile(file);
          break;
        }
        default:
          break;
      }
    }

    private void OnClearAllCompartmentAssignments(object sender, EventArgs e)
    {
      if (_isLoading) return;

      var variables = Config.GetVariableKeys();
      foreach (var id in variables)
      {
        var current = Config[id];
        if (current.ContainsKey("file_compartment"))
          current.Remove("file_compartment");
      }

      var main = Config["Reaction"];
      var keys = main.Keys;
      foreach (var key in keys)
      {
        if (key.StartsWith("in_"))
          main.Remove(key);
      }
    }

    private void OnApplyCompartmentAssignments(object sender, EventArgs e)
    {
      if (_isLoading) return;

      OnClearAllCompartmentAssignments(sender, e);

      var variables = Config.GetVariableKeys();
      var main = Config["Reaction"];

      foreach (DataGridViewRow row in gridCompartments.Rows)
      {
        var compId = row.Cells[0].Value as string;
        var dmpFile = row.Cells[1].Value as string;

        var ids = VariableCompartmentMap.Where(item => item.Value == compId).Select(item2 => item2.Key).ToList();

        foreach (var id in ids)
        {
          if (!variables.Contains(id)) continue;
          var current = Config[id];
          if (current == null) continue;
          current["file_compartment"] = dmpFile;
        }

        main["in_" + compId] = dmpFile;
      }
    }

    private void InitFromSBML(string fileName)
    {
      CompartmentIds = new List<string>();
      VariableCompartmentMap = new Dictionary<string, string>();

      var doc = libsbml.readSBMLFromFile(fileName);
      var model = doc.getModel();
      if (model == null) return;

      CompartmentIds = new List<string>();
      VariableCompartmentMap = new Dictionary<string, string>();

      for (var i = 0; i < model.getNumCompartments(); ++i)
        CompartmentIds.Add(model.getCompartment(i).getId());

      for (var i = 0; i < model.getNumSpecies(); ++i)
      {
        var species = model.getSpecies(i);
        VariableCompartmentMap[species.getId()] = species.getCompartment();
      }

      gridCompartments.Rows.Clear();
      foreach (var id in CompartmentIds)
      {
        gridCompartments.Rows.Add(id, "");
      }
    }

    private void OnInitFromSBMLClick(object sender, EventArgs e)
    {
      using (var dlg = new OpenFileDialog
      {
        Title = "Open SBML file",
        Filter = "SBML files|*.xml;*.sbml|All files|*.*",
        InitialDirectory = CurrentDir
      })
      {
        if (dlg.ShowDialog() == DialogResult.OK)
        {
          InitFromSBML(dlg.FileName);
        }
      }
    }
  }
}