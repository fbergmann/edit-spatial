using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using WFDuneRunner.Model;

namespace WFDuneRunner.Controls
{
  public partial class CtrlDomain : UserControl
  {
    public CtrlDomain()
    {
      InitializeComponent();
    }

    public DomainConfig Model { get; set; }

    public void LoadModel(DomainConfig model)
    {
      Model = model;

      txtWidth.Text = Model.Width.ToString();
      txtHeight.Text = Model.Height.ToString();
      txtDepth.Text = Model.Depth.ToString();

      txtGridX.Text = Model.GridX.ToString();
      txtGridY.Text = Model.GridY.ToString();
      txtGridZ.Text = Model.GridZ.ToString();
      txtRefinement.Text = Model.Refinement.ToString();

      txtFile.Text = Model.Geometry;
      txtMin.Text = Model.GeometryMin.ToString();
      txtMax.Text = Model.GeometryMax.ToString();
    }

    private void OnWidthChanged(object sender, EventArgs e)
    {
      if (Model == null) return;
      double temp;
      if (double.TryParse(txtWidth.Text, out temp))
        Model.Width = temp;
    }

    private void OnHeightChanged(object sender, EventArgs e)
    {
      if (Model == null) return;
      double temp;
      if (double.TryParse(txtHeight.Text, out temp))
        Model.Height = temp;
    }

    private void OnDepthChanged(object sender, EventArgs e)
    {
      if (Model == null) return;
      double temp;
      if (double.TryParse(txtDepth.Text, out temp))
        Model.Depth = temp;
    }

    private void OnGridXChanged(object sender, EventArgs e)
    {
      if (Model == null) return;
      int temp;
      if (int.TryParse(txtGridX.Text, out temp))
        Model.GridX = temp;
    }

    private void OnGridYChanged(object sender, EventArgs e)
    {
      if (Model == null) return;
      int temp;
      if (int.TryParse(txtGridY.Text, out temp))
        Model.GridY = temp;
    }

    private void OnGridZChanged(object sender, EventArgs e)
    {
      if (Model == null) return;
      int temp;
      if (int.TryParse(txtGridZ.Text, out temp))
        Model.GridZ = temp;
    }

    private void OnRefinementChanged(object sender, EventArgs e)
    {
      if (Model == null) return;
      int temp;
      if (int.TryParse(txtRefinement.Text, out temp))
        Model.Refinement = temp;
    }

    private void OnFileChanged(object sender, EventArgs e)
    {
      if (Model == null) return;
      Model.Geometry = txtFile.Text;
    }

    private void OnMinChanged(object sender, EventArgs e)
    {
      if (Model == null) return;
      double temp;
      if (double.TryParse(txtMin.Text, out temp))
        Model.GeometryMin = temp;
    }

    private void OnMaxChanged(object sender, EventArgs e)
    {
      if (Model == null) return;
      double temp;
      if (double.TryParse(txtMax.Text, out temp))
        Model.GeometryMax = temp;
    }

    private void OnLoadClicked(object sender, EventArgs e)
    {
      if (Model == null) return;
      using (var dlg = new OpenFileDialog {Title = "Open Geometry file", Filter = "DMP files|*.dmp|All files|*.*"})
      {
        if (dlg.ShowDialog() == DialogResult.OK)
        {
          txtFile.Text = dlg.FileName;
          Model.Geometry = txtFile.Text;
        }
      }
    }

    private void OnEditClicked(object sender, EventArgs e)
    {
      if (Model == null) return;
      string file = Model.Geometry;
      if (!File.Exists(file)) return;
      try
      {
        Process.Start(file);
      }
      catch
      {
      }
    }
  }
}