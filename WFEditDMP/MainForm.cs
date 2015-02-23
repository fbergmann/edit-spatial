using System;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using LibEditSpatial.Model;
using WFEditDMP.Forms;

namespace WFEditDMP
{
  public partial class MainForm : Form
  {
    public MainForm()
    {
      InitializeComponent();
      NewModel();

      ctrlPalette1.PalleteValueChanged += (o, args) => { dmpRenderControl1.CurrentValue = args.Value; };
      ctrlPalette1.PalleteChanged += (o, args) =>
      {
        if (Model != null)
        {
          Model.Palette = args;
          UpdateUI();
        }
        DmpPalette.Default = args;
      };

      dmpRenderControl1.IndexLocationChanged +=
        (o, args) => { lblPosition.Text = string.Format("nx={0}, ny={1}", args.X, args.Y); };
      dmpRenderControl1.DataLocationChanged +=
        (o, args) => { lblData.Text = string.Format("x={0}, y={1}", args.X, args.Y); };
    }

    public DmpModel Model { get; set; }
    public string LastOpenDir { get; set; }
    public String[] PaletteFiles { get; set; }
    public string CurrentPalette { get; set; }

    private void SetTitle(string fileName)
    {
      if (!string.IsNullOrEmpty(fileName))
        Text = String.Format("Edit DMP - [{0}]", Path.GetFileName(fileName));
      else
        Text = "Edit DMP";
    }

    private void UpdateUI()
    {
      if (Model == null)
      {
        SetTitle(null);
        lblMessage.Text = "no model";
        return;
      }

      lblSize.Text = string.Format("{0} x {1}", Model.Columns, Model.Rows);
      txtCols.Text = Model.Columns.ToString();
      txtRows.Text = Model.Rows.ToString();
      txtMinX.Text = Model.MinX.ToString(CultureInfo.InvariantCulture);
      txtMaxX.Text = Model.MaxX.ToString(CultureInfo.InvariantCulture);
      txtMinY.Text = Model.MinY.ToString(CultureInfo.InvariantCulture);
      txtMaxY.Text = Model.MaxY.ToString(CultureInfo.InvariantCulture);
      dmpRenderControl1.LoadModel(Model);
      SetTitle(Model.FileName);
    }

    public void OpenFile(string filename)
    {
      LastOpenDir = Path.GetDirectoryName(filename);
      Model = DmpModel.FromFile(filename);
      ctrlPalette1.UpdateValues(Model.Min, ctrlPalette1.Current.Value, Model.Max);
      dmpRenderControl1.CurrentValue = Model.Max;
      Model.Palette = ctrlPalette1.Palette;
      UpdateUI();
    }

    private void NewModel()
    {
      Model = new DmpModel(50, 50);
      ctrlPalette1.UpdateValues(Model.Min, ctrlPalette1.Current.Value, Model.Max);
      dmpRenderControl1.CurrentValue = Model.Max;
      Model.Palette = ctrlPalette1.Palette;
      UpdateUI();
    }

    private void OnNewClick(object sender, EventArgs e)
    {
      NewModel();
    }

    private void OnOpenClick(object sender, EventArgs e)
    {
      using (var dialog = new OpenFileDialog
      {
        Title = "Open file",
        Filter = "DMP files|*.dmp|TIFF files|*.tif;*.tiff|All files|*.*",
        AutoUpgradeEnabled = true,
        InitialDirectory = LastOpenDir
      })
      {
        if (dialog.ShowDialog() == DialogResult.OK)
          OpenFile(dialog.FileName);
      }
    }

    private void OnSaveClick(object sender, EventArgs e)
    {
      if (string.IsNullOrWhiteSpace(Model.FileName))
      {
        OnSaveAsClick(sender, e);
        return;
      }
      SaveAs(Model.FileName);
    }

    private void OnExitClick(object sender, EventArgs e)
    {
      Close();
    }

    private void OnAboutClick(object sender, EventArgs e)
    {
      using (var dlg = new AboutBox())
      {
        dlg.ShowDialog();
      }
    }

    private void SaveAs(string fileName)
    {
      if (Model == null) return;

      string lower = fileName.ToLowerInvariant();
      if (lower.EndsWith(".tif") || lower.EndsWith(".tiff"))
        Model.ToImage().Save(fileName);
      else 
        Model.SaveAs(fileName);
      UpdateUI();
    }

    private void OnSaveAsClick(object sender, EventArgs e)
    {
      if (Model == null) return;
      using (var dialog = new SaveFileDialog
      {
        Title = "Save file",
        Filter = "DMP files|*.dmp|TIFF files|*.tif;*.tiff|All files|*.*",
        AutoUpgradeEnabled = true,
        InitialDirectory = LastOpenDir
      })
      {
        if (dialog.ShowDialog() == DialogResult.OK)
          SaveAs(dialog.FileName);
      }
    }

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

      LoadPalettes(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Palettes"));
    }

    private void OnPaletteChanged(object sender, EventArgs e)
    {
      var index = cmbPalettes.SelectedIndex;

      if (index < 0) return;

      if ((string) cmbPalettes.Items[index] == CurrentPalette)
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
      CurrentPalette = (string) cmbPalettes.Items[index];
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
      if (int.TryParse(txtCols.Text, out cols) && int.TryParse(txtRows.Text, out rows))
      {
        Model.Resize(cols, rows);
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

    private void OnCenterClick(object sender, EventArgs e)
    {
      Model.Center();
      UpdateUI();
    }

    private void OnMinXChanged(object sender, EventArgs e)
    {
      double temp;
      if (Model != null && double.TryParse(txtMinX.Text, out temp))
        Model.MinX = temp;
    }

    private void OnMaxXChanged(object sender, EventArgs e)
    {
      double temp;
      if (Model != null && double.TryParse(txtMaxX.Text, out temp))
        Model.MaxX = temp;
    }

    private void OnMinYChanged(object sender, EventArgs e)
    {
      double temp;
      if (Model != null && double.TryParse(txtMinY.Text, out temp))
        Model.MinY = temp;
    }

    private void OnMaxYChanged(object sender, EventArgs e)
    {
      double temp;
      if (Model != null && double.TryParse(txtMaxY.Text, out temp))
        Model.MaxY = temp;
    }

    private void OnSplitIntoIndividuals(object sender, EventArgs e)
    {
      if (Model == null) return;
      var range = Model.Range;

      using (var dlg = new FormAdjustDmp())
      {
        dlg.InitializeFrom(range, (DmpModel) Model.Clone());
        if (dlg.ShowDialog(this) == DialogResult.OK)
        {
          Model = dlg.Model;
          UpdateUI();
        }
      }
    }

    private int FindHCF(int m, int n)
    {
      int temp, reminder;
      if (m < n)
      {
        temp = m;
        m = n;
        n = temp;
      }
      while (true)
      {
        reminder = m % n;
        if (reminder == 0)
          return n;
        else
          m = n;
        n = reminder;
      }
    }

    private void OnAspectClick(object sender, EventArgs e)
    {
      // resizes the window to match the aspect ratio
      var screen = Screen.FromControl(this);
      StartPosition = FormStartPosition.Manual;
      var bounds = screen.Bounds;
      Location = bounds.Location;

      int hcf = FindHCF((int)Math.Round(Model.MaxX), (int)Math.Round(Model.MaxY));

      double width = Math.Round(Model.MaxX);
      double height = Math.Round(Model.MaxY);

      double factorX = width / (double)hcf;
      double factorY = height / (double)hcf;

      double maxWidth = bounds.Width - 50;
      double maxHeight = bounds.Height - 50;
      double min = Math.Min(maxWidth, maxHeight);

      if (factorX > factorY)
      {
        double newHeight = height / width * min;
        Size = new System.Drawing.Size((int)min, (int)newHeight);
      }
      else
      {
        double newWidth = width / height * min;
        Size = new System.Drawing.Size((int)newWidth, (int)min);
      }
    }

    private void OnFormClosing(object sender, FormClosingEventArgs e)
    {
      if (Model == null || !Model.Dirty) return;

      DialogResult result =
        MessageBox.Show(this,
          "There are unsaved changes in the model. Would you like to save them?",
          "Save changes?",
          MessageBoxButtons.YesNoCancel,
          MessageBoxIcon.Question,
          MessageBoxDefaultButton.Button3);

      if (result == DialogResult.Cancel)
      {
        e.Cancel = true;
      }

      if (result == DialogResult.Yes)
      {
        OnSaveClick(sender, e);
      }
    }
  }
}