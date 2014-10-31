using System;
using System.Windows.Forms;
using WFDuneRunner.Model;

namespace WFDuneRunner.Controls
{
  public partial class CtrlGlobal : UserControl
  {
    public CtrlGlobal()
    {
      InitializeComponent();
    }

    public GlobalConfig Model { get; set; }

    public void LoadModel(GlobalConfig model)
    {
      Model = model;

      chkWriteVTK.Checked = Model.WriteVTK;
      txtVTKname.Text = Model.VTKname;
      txtOverlap.Text = Model.Overlap.ToString();
      txtIntegrationorder.Text = Model.IntegrationOrder.ToString();
      txtSubsampling.Text = Model.SubSampling.ToString();
      if (Model.TimeStepping == "explicit")
        radExplicit.Checked = true;
      else
        radImplicit.Checked = true;
      txtExplicitsolver.Text = Model.ExplicitSolver;
      txtImplicitSovler.Text = Model.ImplicitSolver;
    }

    private void chkWriteVTK_CheckedChanged(object sender, EventArgs e)
    {
      if (Model == null) return;
      Model.WriteVTK = chkWriteVTK.Checked;
    }

    private void txtVTKname_TextChanged(object sender, EventArgs e)
    {
      if (Model == null) return;
      Model.VTKname = txtVTKname.Text;
    }

    private void txtOverlap_TextChanged(object sender, EventArgs e)
    {
      if (Model == null) return;
      int temp;
      if (int.TryParse(txtOverlap.Text, out temp))
        Model.Overlap = temp;
    }

    private void txtIntegrationorder_TextChanged(object sender, EventArgs e)
    {
      if (Model == null) return;
      int temp;
      if (int.TryParse(txtIntegrationorder.Text, out temp))
        Model.IntegrationOrder = temp;
    }

    private void txtSubsampling_TextChanged(object sender, EventArgs e)
    {
      if (Model == null) return;
      int temp;
      if (int.TryParse(txtSubsampling.Text, out temp))
        Model.SubSampling = temp;
    }

    private void radImplicit_CheckedChanged(object sender, EventArgs e)
    {
      if (Model == null) return;
      if (radImplicit.Checked)
        Model.TimeStepping = "implicit";
      else
        Model.TimeStepping = "explicit";
    }

    private void radExplicit_CheckedChanged(object sender, EventArgs e)
    {
      if (Model == null) return;
      if (radExplicit.Checked)
        Model.TimeStepping = "explicit";
      else
        Model.TimeStepping = "implicit";
    }

    private void txtExplicitsolver_TextChanged(object sender, EventArgs e)
    {
      if (Model == null) return;
      Model.ExplicitSolver = txtExplicitsolver.Text;
    }

    private void txtImplicitSovler_TextChanged(object sender, EventArgs e)
    {
      if (Model == null) return;
      Model.ImplicitSolver = txtImplicitSovler.Text;
    }
  }
}