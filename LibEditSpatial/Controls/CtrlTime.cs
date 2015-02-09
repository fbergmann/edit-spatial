using System;
using System.Windows.Forms;
using LibEditSpatial.Model;

namespace LibEditSpatial.Controls
{
  public partial class CtrlTime : UserControl
  {
    public CtrlTime()
    {
      InitializeComponent();
    }

    public TimeLoopConfig Model { get; set; }

    public void LoadModel(TimeLoopConfig model)
    {
      Model = model;
      txtTime.Text = Model.Time.ToString();
      txtInitialStep.Text = Model.InitialStep.ToString();
      txtMinStep.Text = Model.MinStep.ToString();
      txtMaxStep.Text = Model.MaxStep.ToString();
      txtPlotStep.Text = Model.PlotStep.ToString();
    }

    private void OnEndTimeChanged(object sender, EventArgs e)
    {
      if (Model == null) return;
      double temp;
      if (double.TryParse(txtTime.Text, out temp))
        Model.Time = temp;
    }

    private void OnInitialStepChanged(object sender, EventArgs e)
    {
      if (Model == null) return;
      double temp;
      if (double.TryParse(txtInitialStep.Text, out temp))
        Model.InitialStep = temp;
    }

    private void OnMinStepChanged(object sender, EventArgs e)
    {
      if (Model == null) return;
      double temp;
      if (double.TryParse(txtMinStep.Text, out temp))
        Model.MinStep = temp;
    }

    private void OnMaxStepChanged(object sender, EventArgs e)
    {
      if (Model == null) return;
      double temp;
      if (double.TryParse(txtMaxStep.Text, out temp))
        Model.MaxStep = temp;
    }

    private void OnPlotStepChanged(object sender, EventArgs e)
    {
      if (Model == null) return;
      double temp;
      if (double.TryParse(txtPlotStep.Text, out temp))
        Model.PlotStep = temp;
    }
  }
}