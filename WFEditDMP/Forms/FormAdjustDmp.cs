using System;
using System.Collections.Generic;
using System.Linq;
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
      {
        dmpRenderControl1.LoadModel( Model);
        dmpRenderControl1.DisableEditing = true;
      }
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
      Model.Min = Numbers.FirstOrDefault();
      Model.Max = Numbers.LastOrDefault();
      UpdateUI();
    }

    private void cmdReplace_Click(object sender, EventArgs e)
    {
      if (listBox1.SelectedItems.Count < 1) return;

      double target;
      if (!double.TryParse(txtReplacement.Text, out target))
        return;

      var list = listBox1.SelectedItems.Cast<double>().ToList();      

      for (var i = list.Count - 1; i >= 0; i--)
      {
        var current = (double)list[i];
        Model.Transform(x => x == current ? target : x);
        listBox1.Items.Remove(current);
        Numbers.Remove(current);
      }


      Numbers = Model.Range;
      Model.Min = Numbers.FirstOrDefault();
      Model.Max = Numbers.LastOrDefault();

      UpdateUI();
    }

    private void OnInvertClick(object sender, EventArgs e)
    {
      Model.Invert();
      UpdateUI();
    }

    private void OnMultiplyClick(object sender, EventArgs e)
    {
      double val;
      if (!double.TryParse(txtScalar.Text, out val)) return;
      
      var list = listBox1.SelectedItems.Cast<double>().ToList();
      if (!list.Any())
        list.AddRange(Numbers);

      Model.Transform(x => list.Contains(x) ?  x * val : x);
      
      Numbers = Model.Range;
      Model.Min = Numbers.FirstOrDefault();
      Model.Max = Numbers.LastOrDefault();
      UpdateUI();
    }

    private void OnMaskClick(object sender, EventArgs e)
    {
      using (var dlg = new OpenFileDialog
      {
        Title = "Choose DMP file to mask with. ",
        Filter = "DMP files|*.dmp|Image files|*.tif;*.tiff;*.png;*.jpg;*.jpeg;*.bmp|All files|*.*",
        AutoUpgradeEnabled = true,
      })
      {
        if (dlg.ShowDialog() == DialogResult.OK)
        {
          try
          {
            var file = DmpModel.FromFile(dlg.FileName);
            Model.MaskWith(file);
            UpdateUI();
          }
          catch
          {
            MessageBox.Show("Masking failed, ensure that the files have precisely the same dimensions", "Masking failed",
              MessageBoxButtons.OK, MessageBoxIcon.Error);
          }
        }
      }
    }
  }
}