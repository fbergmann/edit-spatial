using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WFEditDMP.Model;

namespace WFEditDMP
{
  public partial class MainForm : Form
  {
    public DmpModel Model { get; set; }
    public string FileName { get; set; }
    public string LastOpenDir { get; set; }

    private void UpdateUI()
    {
      if (!string.IsNullOrEmpty(FileName))
        Text = String.Format("Edit DMP - [{0}]", Path.GetFileName(FileName));
      else
        Text = "Edit DMP";

      if (Model == null)
      {
        lblSize.Text = "no model";
        return;
      }

      lblSize.Text = string.Format("{0} x {1}", Model.Rows, Model.Columns);
      txtRows.Text = Model.Rows.ToString();
      txtCols.Text = Model.Columns.ToString();
      dmpRenderControl1.LoadModel(Model);
    }

    public void OpenFile(string filename)
    {
      FileName = filename;
      LastOpenDir = Path.GetDirectoryName(filename);
      Model = DmpModel.FromFile(filename);
      ctrlPalette1.UpdateValues(Model.Min, ctrlPalette1.Current.Value, Model.Max);
      Model.Palette = ctrlPalette1.Palette;
      UpdateUI();
    }

    public MainForm()
    {
      InitializeComponent();
      NewModel();

      ctrlPalette1.PalleteValueChanged += (o, args) =>
        {
          dmpRenderControl1.CurrentValue = args.Value;
        };

      ctrlPalette1.PalleteChanged+= (o, args) =>
      {
        if (Model != null)
        {
          Model.Palette = args;
          UpdateUI();
        }
        DmpPalette.Default = args;
      };
    }

    private void NewModel()
    {
      Model = new DmpModel(50, 50);
      ctrlPalette1.UpdateValues(Model.Min, ctrlPalette1.Current.Value, Model.Max);
      Model.Palette = ctrlPalette1.Palette;
      UpdateUI();
    }
    private void OnNewClick(object sender, EventArgs e)
    {
      NewModel();
    }

    private void OnOpenClick(object sender, EventArgs e)
    {
      using (var dialog = new OpenFileDialog { 
        Title = "Open file", 
        Filter = "DMP files|*.dmp|TIFF files|*.tif;*.tiff|All files|*.*", 
        AutoUpgradeEnabled = true, 
      })
      {
        if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
          OpenFile(dialog.FileName);
      }
    }

    private void OnSaveClick(object sender, EventArgs e)
    {
      if (string.IsNullOrWhiteSpace(FileName))
      { 
        OnSaveAsClick(sender, e);
        return;
      }
      SaveAs(FileName);
    }

    private void OnExitClick(object sender, EventArgs e)
    {
      Close();
    }

    private void OnAboutClick(object sender, EventArgs e)
    {

    }

    private void SaveAs(string fileName)
    {
      if (Model == null) return;
      Model.SaveAs(fileName);
    }

    private void OnSaveAsClick(object sender, EventArgs e)
    {
      if (Model == null) return;
      using (var dialog = new SaveFileDialog
      {
        Title = "Save file",
        Filter = "DMP files|*.dmp|TIFF files|*.tif;*.tiff|All files|*.*",
        AutoUpgradeEnabled = true,
      })
      {
        if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
          SaveAs(dialog.FileName);
      }
    }

    public String[] PaletteFiles { get; set; }

    private void LoadPalettes(string baseDirectory)
    {
      PaletteFiles = Directory.GetFiles(baseDirectory, "*.txt", SearchOption.TopDirectoryOnly);
      cmbPalettes.Items.Clear();
      cmbPalettes.Items.Add("Default");
      foreach (var file in PaletteFiles)
      {
        cmbPalettes.Items.Add(Path.GetFileNameWithoutExtension(file));
      }
    }
    private void OnLoad(object sender, EventArgs e)
    {
      ctrlPalette1.Palette = DmpPalette.Default;

      LoadPalettes(AppDomain.CurrentDomain.BaseDirectory);

    }

    public string CurrentPalette { get; set;  }

    private void OnPaletteChanged(object sender, EventArgs e)
    {
      var index = cmbPalettes.SelectedIndex;

      if (index < 0) return;

      if ((string)cmbPalettes.Items[index] == CurrentPalette)
        return;

      if (index == 0)
      {
        ctrlPalette1.ChangePalette(DmpPalette.Default);        
      }
      else
        try
        {
          ctrlPalette1.ChangePalette(PaletteFiles[index - 1]);
        }
        catch 
        {

        }
      CurrentPalette = (string)cmbPalettes.Items[index];
    }

    private void OnSizeChanged(object sender, EventArgs e)
    {
      int val; 

      if (int.TryParse(txtSize.Text, out val))
      {
        dmpRenderControl1.PencilSize = val;
      }
    }

    private void OnResizeClicked(object sender, EventArgs e)
    {
      int rows, cols;
      if (int.TryParse(txtRows.Text, out rows) && int.TryParse(txtCols.Text, out cols))
      {
        Model.Resize(rows, cols);
        UpdateUI();
      }
    }

    private void OnRotateLeft(object sender, EventArgs e)
    {
      Model.RotateRight();
      Model.RotateRight();
      Model.RotateRight();
      UpdateUI();
    }

    private void OnRotateRight(object sender, EventArgs e)
    {
      Model.RotateRight();
      UpdateUI();
    }
  }
}
