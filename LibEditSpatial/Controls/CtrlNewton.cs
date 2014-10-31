using System;
using System.Windows.Forms;
using LibEditSpatial.Model;

namespace LibEditSpatial.Controls
{
  public partial class CtrlNewton : UserControl
  {
    public CtrlNewton()
    {
      InitializeComponent();
    }

    public NewtonConfig Model { get; set; }

    public void LoadModel(NewtonConfig model)
    {
      Model = model;

      txtLinearVerbosity.Text = model.LinearVerbosity.ToString();
      txtReassembleThreshold.Text = model.ReassembleThreshold.ToString();
      txtLineSearchMaxIterations.Text = model.LineSearchMaxIterations.ToString();
      txtMaxIterations.Text = model.MaxIterations.ToString();
      txtAbsoluteLimit.Text = model.AbsoluteLimit.ToString();
      txtReduction.Text = model.Reduction.ToString();
      txtLinearReduction.Text = model.LinearReduction.ToString();
      txtLineSearchDampingFactor.Text = model.LineSearchDampingFactor.ToString();
      txtVerbosity.Text = model.Verbosity.ToString();
    }

    private void txtVerbosity_TextChanged(object sender, EventArgs e)
    {
      if (Model == null) return;
      int temp;
      if (int.TryParse(txtVerbosity.Text, out temp))
        Model.Verbosity = temp;
    }

    private void txtLineSearchDampingFactor_TextChanged(object sender, EventArgs e)
    {
      if (Model == null) return;
      double temp;
      if (double.TryParse(txtLineSearchDampingFactor.Text, out temp))
        Model.LineSearchDampingFactor = temp;
    }

    private void txtLinearReduction_TextChanged(object sender, EventArgs e)
    {
      if (Model == null) return;
      double temp;
      if (double.TryParse(txtLinearReduction.Text, out temp))
        Model.LinearReduction = temp;
    }

    private void txtReduction_TextChanged(object sender, EventArgs e)
    {
      if (Model == null) return;
      double temp;
      if (double.TryParse(txtReduction.Text, out temp))
        Model.Reduction = temp;
    }

    private void txtAbsoluteLimit_TextChanged(object sender, EventArgs e)
    {
      if (Model == null) return;
      double temp;
      if (double.TryParse(txtAbsoluteLimit.Text, out temp))
        Model.AbsoluteLimit = temp;
    }

    private void txtMaxIterations_TextChanged(object sender, EventArgs e)
    {
      if (Model == null) return;
      int temp;
      if (int.TryParse(txtMaxIterations.Text, out temp))
        Model.MaxIterations = temp;
    }

    private void txtLineSearchMaxIterations_TextChanged(object sender, EventArgs e)
    {
      if (Model == null) return;
      int temp;
      if (int.TryParse(txtLineSearchMaxIterations.Text, out temp))
        Model.LineSearchMaxIterations = temp;
    }

    private void txtReassembleThreshold_TextChanged(object sender, EventArgs e)
    {
      if (Model == null) return;
      double temp;
      if (double.TryParse(txtReassembleThreshold.Text, out temp))
        Model.ReassembleThreshold = temp;
    }

    private void txtLinearVerbosity_TextChanged(object sender, EventArgs e)
    {
      if (Model == null) return;
      int temp;
      if (int.TryParse(txtLinearVerbosity.Text, out temp))
        Model.LinearVerbosity = temp;
    }
  }
}