using System;
using System.Collections.Generic;
using System.Windows.Forms;
using LibEditSpatial.Model;

namespace WFEditDMP.Forms
{
  public partial class FormAdjustDmp : Form
  {
    public FormAdjustDmp()
    {
      InitializeComponent();
    }

    public DmpModel Model { get; set; }
    public List<double> Numbers { get; set; }

    private void UpdateUI()
    {
      listBox1.Items.Clear();
      if (Numbers != null)
        foreach (var item in Numbers)
        {
          listBox1.Items.Add(item);
        }

      if (Model != null)
        pictureBox1.Image = Model.ToImage();
    }

    public void InitializeFrom(List<double> selection, DmpModel model)
    {
      Model = model;
      Numbers = selection;
      UpdateUI();
    }

    private void cmdMerge_Click(object sender, EventArgs e)
    {
      if (listBox1.SelectedItems.Count < 2) return;
      var target = (double) listBox1.SelectedItems[0];
      for (var i = listBox1.SelectedItems.Count - 1; i >= 1; i--)
      {
        var current = (double) listBox1.SelectedItems[i];
        Model.Transform(x => x == current ? target : x);
        listBox1.Items.Remove(current);
      }

      Numbers = Model.Range;
      UpdateUI();
    }

    private void cmdReplace_Click(object sender, EventArgs e)
    {
      if (listBox1.SelectedItems.Count < 1) return;

      double target;
      if (!double.TryParse(txtReplacement.Text, out target))
        return;

      for (var i = listBox1.SelectedItems.Count - 1; i >= 0; i--)
      {
        var current = (double) listBox1.SelectedItems[i];
        Model.Transform(x => x == current ? target : x);
        listBox1.Items.Remove(current);
        Numbers.Remove(current);
      }


      Numbers = Model.Range;

      UpdateUI();
    }

    private void cmdInvert_Click(object sender, EventArgs e)
    {
      Model.Invert();
      UpdateUI();
    }
  }
}