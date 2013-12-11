using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EditSpatial.Model;

namespace EditSpatial
{
  public partial class FormInitSpatial : Form
  {
    public FormInitSpatial()
    {
      InitializeComponent();      
    }

    public CreateModel CreateModel
    {
      get
      {
        var result = new CreateModel();
        foreach (var item in lstSpatialSpecies.Items)
          result.Species.Add(new SpatialSpecies
          {
            Id = (string) item,
            InitialCondition = "1"
          });
        return result;
      }
    }
    public SpatialModel SpatialModel { get; set; }

    private void cmdCancel_Click(object sender, EventArgs e)
    {
      Close();
    }

    private void cmdFinish_Click(object sender, EventArgs e)
    {
      Close();
    }

    private void OnAllSpeciesDoubleClick(object sender, EventArgs e)
    {
      AddSelected();

    }
    private void OnSpatialSpeciesDoubleClick(object sender, EventArgs e)
    {
      RemoveSelected();
    }

    private void AddSelected()
    {
      var selected = lstAllSpecies.SelectedItem;
      if (selected == null) return;
      AddSelected(selected);
    }

    private void AddSelected(object o)
    {
      if (o == null) return;
      if (lstSpatialSpecies.Items.Contains(o)) return;
      lstSpatialSpecies.Items.Add(o);
    }

    private void RemoveSelected()
    {
      var selected = lstSpatialSpecies.SelectedItem;
      if (selected == null) return;
      RemoveSelected(selected);
    }

    private void RemoveSelected(object o)
    {
      if (o == null) return;
      lstSpatialSpecies.Items.Remove(o);
    }

    private void OnFirstShown(object sender, EventArgs e)
    {
      UpdateUI();
    }

    private void UpdateUI()
    {
      if (SpatialModel == null || 
        SpatialModel.Document == null || 
        SpatialModel.Document.getModel() == null) return;
      var model = SpatialModel.Document.getModel();
      for (int i = 0; i < model.getNumSpecies();++i)
      {
        var current = model.getSpecies(i);
        lstAllSpecies.Items.Add(current.getId());
        if (current == null || current.getConstant() || current.getBoundaryCondition())
          continue;        
        lstSpatialSpecies.Items.Add(current.getId());        
      }

    }

  }
}
