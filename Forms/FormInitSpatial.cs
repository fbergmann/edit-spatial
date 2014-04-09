using System;
using System.Windows.Forms;
using EditSpatial.Model;
using libsbmlcs;

namespace EditSpatial.Forms
{
  public partial class FormInitSpatial : Form
  {
    private libsbmlcs.Model _model;

    public FormInitSpatial()
    {
      InitializeComponent();
      _model = null;
    }

    public CreateModel CreateModel
    {
      get
      {
        var result = new CreateModel
        {
          Geometry = new GeometrySettings
          {
            Xmax = Util.SaveDouble(txtDimX.Text, 50),
            Ymax = Util.SaveDouble(txtDimX.Text, 50),
          }
        };
        foreach (var item in lstSpatialSpecies.Items)
          result.Species.Add(item as SpatialSpecies);
        return result;
      }
    }
    public SpatialModel SpatialModel { get; set; }

    private void OnCancelClick(object sender, EventArgs e)
    {
      Close();
    }

    private void OnFinishClick(object sender, EventArgs e)
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
      AddSelected(selected as string);
    }

    private void AddSelected(Species species)
    {
      if (species == null || _model == null) return;


      var diffusionX = species.getDiffusionX();
      var diffusionY = species.getDiffusionY();
      var spatialSpecies = new SpatialSpecies
      {
        Id = species.getId(),
        DiffusionX = diffusionX.HasValue ? diffusionX.Value :
          diffusionY.HasValue ? diffusionY.Value : 0.01,
        DiffusionY = diffusionY.HasValue ? diffusionY.Value :
          diffusionX.HasValue ? diffusionX.Value : 0.01,

        InitialCondition = species.getInitialExpession()
      };

      lstSpatialSpecies.Items.Add(spatialSpecies);

      grid.Rows.Add(spatialSpecies.Id,
        spatialSpecies.DiffusionX,
        spatialSpecies.DiffusionY,
        spatialSpecies.InitialCondition,
        spatialSpecies.MaxBoundaryX, spatialSpecies.MaxBoundaryY,
        spatialSpecies.MinBoundaryX, spatialSpecies.MinBoundaryY);

    }

    private void AddSelected(string id)
    {
      if (id == null || _model == null) return;
      if (lstSpatialSpecies.Items.Contains(id)) return;

      AddSelected(_model.getSpecies(id));

    }

    private void RemoveSelected()
    {
      var selected = lstSpatialSpecies.SelectedItem;
      if (selected == null) return;
      RemoveSelected(selected as SpatialSpecies);
    }

    private void RemoveSelected(string id)
    {
      var species = GetSpecies(id);

      RemoveSelected(species);
    }

    private SpatialSpecies GetSpecies(string id)
    {
      SpatialSpecies species = null;
      for (int i = lstSpatialSpecies.Items.Count - 1; i >= 0; i--)
      {
        var current = lstSpatialSpecies.Items[i] as SpatialSpecies;
        if (current != null &&  current.Id == id)
        {
          species = current;
          break;
        }
      }
      return species;
    }

    private void RemoveSelected(SpatialSpecies o)
    {
      if (o == null) return;
      var current = o;
      lstSpatialSpecies.Items.Remove(o);

      for (int i = grid.Rows.Count - 1; i >= 0; i--)
      {
        if (current.Id == (string)grid.Rows[i].Cells[0].Value)
          grid.Rows.RemoveAt(i);
      }
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
      _model = SpatialModel.Document.getModel();

      txtDimX.Text = SpatialModel.Width.ToString();
      txtDimY.Text = SpatialModel.Height.ToString();

      lstSpatialSpecies.Items.Clear();
      grid.Rows.Clear();
      for (int i = 0; i < _model.getNumSpecies(); ++i)
      {
        var current = _model.getSpecies(i);
        if (current == null) continue;
        lstAllSpecies.Items.Add(current.getId());
        if (current.getConstant() || current.getBoundaryCondition())
          continue;

        AddSelected(current);

      }

    }

    private void OnPrevClick(object sender, EventArgs e)
    {

    }

    private void OnNextClick(object sender, EventArgs e)
    {

    }

    private void OnCellEndEdit(object sender, DataGridViewCellEventArgs e)
    {
      var row = grid.Rows[e.RowIndex];
      var current = (string)row.Cells[0].Value;
      var species = GetSpecies(current);

      switch (e.ColumnIndex)
      {
        case 1:
          {
            species.DiffusionX = Util.SaveDouble((string)row.Cells[1].Value, species.DiffusionX);
            break;
          }
        case 2:
          {
            species.DiffusionY = Util.SaveDouble((string)row.Cells[2].Value, species.DiffusionY);
            break;
          }
        case 3:
          {
            var node = libsbml.parseFormula((string)row.Cells[3].Value);
            if (node != null)
            {
              var formula = libsbml.formulaToString(node);
              species.InitialCondition = formula;
              row.Cells[3].Value = formula;
            }
            break;
          }
        case 4:
          {
            species.MaxBoundaryX = Util.SaveDouble((string)row.Cells[4].Value, species.MaxBoundaryX);
            break;
          }
        case 5:
          {
            species.MaxBoundaryY = Util.SaveDouble((string)row.Cells[5].Value, species.MaxBoundaryY);
            break;
          }
        case 6:
          {
            species.MinBoundaryX = Util.SaveDouble((string)row.Cells[6].Value, species.MinBoundaryX);
            break;
          }
        case 7:
          {
            species.MinBoundaryY = Util.SaveDouble((string)row.Cells[7].Value, species.MinBoundaryY);
            break;
          }
        default:
          break;
      }

    }


    private void OnUserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
    {
      e.Cancel = true;
      var id = (string)e.Row.Cells[0].Value;
      RemoveSelected(id);
    }





  }
}
