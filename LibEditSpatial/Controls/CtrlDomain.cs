using System;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using LibEditSpatial.Model;

namespace LibEditSpatial.Controls
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

      txtWidth.Text = Model.Width.ToString(CultureInfo.InvariantCulture);
      txtHeight.Text = Model.Height.ToString(CultureInfo.InvariantCulture);
      txtDepth.Text = Model.Depth.ToString(CultureInfo.InvariantCulture);

      txtGridX.Text = Model.GridX.ToString();
      txtGridY.Text = Model.GridY.ToString();
      txtGridZ.Text = Model.GridZ.ToString();
      txtRefinement.Text = Model.Refinement.ToString();

      txtFile.Text = Model.Geometry;
      txtMin.Text = Model.GeometryMin.ToString(CultureInfo.InvariantCulture);
      txtMax.Text = Model.GeometryMax.ToString(CultureInfo.InvariantCulture);
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
      OnBrowseDmpFile();
    }

    public event EventHandler<string> OpenDmpFile;
    public event EventHandler BrowseDmpFile;

    protected virtual void OnBrowseDmpFile()
    {
      var handler = BrowseDmpFile;
      if (handler != null) handler(this, EventArgs.Empty);
    }

    protected virtual void OnOpenDmpFile(string filename)
    {
      var handler = OpenDmpFile;
      if (handler != null) handler(this, filename);
    }

    private void OnEditClicked(object sender, EventArgs e)
    {
      if (Model == null) return;
      var file = Model.Geometry;
      if (!File.Exists(file)) return;
      try
      {
        OnOpenDmpFile(file);
      }
      catch
      {
      }
    }
  }
}