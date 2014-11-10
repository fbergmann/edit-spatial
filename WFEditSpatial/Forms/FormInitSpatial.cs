using System;
using System.Windows.Forms;
using EditSpatial.Model;
using libsbmlcs;

namespace EditSpatial.Forms
{
  public partial class FormInitSpatial : Form
  {
    private AnalyticGeometry _analyticGeometry;
    private Geometry _geometry;
    private libsbmlcs.Model _model;
    private SampledFieldGeometry _sampleGeometry;

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
          Geometry = GeometryModel
        };
        foreach (object item in lstSpatialSpecies.Items)
          result.Species.Add(item as SpatialSpecies);
        return result;
      }
    }


    public GeometrySettings GeometryModel
    {
      get
      {
        var geom = new GeometrySettings
        {
          Xmax = Util.SaveDouble(txtDimX.Text, 50),
          Ymax = Util.SaveDouble(txtDimY.Text, 50),
          Type = GeometryType.Default
        };

        if (radAnalytic.Checked)
        {
          geom.Type = GeometryType.Analytic;

          if (_analyticGeometry != null && _analyticGeometry.getNumAnalyticVolumes() > 0)
          {
            _analyticGeometry.ExpandMath();

            for (int i = 0; i < _analyticGeometry.getNumAnalyticVolumes(); ++i)
            {
              AnalyticVolume current = _analyticGeometry.getAnalyticVolume(i);
              string math = libsbml.formulaToString(current.getMath());
              if (math.Contains(" width "))
                geom.UsedSymbols.Add("width");
              if (math.Contains(" height "))
                geom.UsedSymbols.Add("height");
              if (math.Contains(" Xmax "))
                geom.UsedSymbols.Add("Xmax");
              if (math.Contains(" Ymax "))
                geom.UsedSymbols.Add("Ymax");
              if (math.Contains(" Xmin "))
                geom.UsedSymbols.Add("Xmin");
              if (math.Contains(" Ymin "))
                geom.UsedSymbols.Add("Ymin");
            }
          }
        }

        if (radSample.Checked)
        {
          geom.Type = GeometryType.Sample;
        }


        return
          geom;
      }
    }

    public SpatialModel SpatialModel { get; set; }

    private void OnCancelClick(object sender, EventArgs e)
    {
      Close();
    }

    private void OnFinishClick(object sender, EventArgs e)
    {
      if (tabControl1.SelectedIndex == 1)
      {
        txtDimX.Text = txtWidth.Text;
        txtDimY.Text = txtHeight.Text;
      }
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
      object selected = lstAllSpecies.SelectedItem;
      if (selected == null) return;
      AddSelected(selected as string);
    }

    private void AddSelected(Species species)
    {
      if (species == null || _model == null) return;


      double? diffusionX = species.getDiffusionX();
      double? diffusionY = species.getDiffusionY();
      double? Xmax = species.getXMaxBC();
      double? Xmin = species.getXMinBC();
      double? Ymax = species.getYMaxBC();
      double? Ymin = species.getYMinBC();
      string bcType = species.getBcType();


      // assignments do not need to diffuse by themselves automatically
      double defaultDiff =
        species.getModel().
          getAssignmentRuleByVariable(species.getId()) == null
          ? 0.001
          : 0.0;

      var spatialSpecies = new SpatialSpecies
      {
        Id = species.getId(),
        DiffusionX = diffusionX.HasValue
          ? diffusionX.Value
          : diffusionY.HasValue ? diffusionY.Value : defaultDiff,
        DiffusionY = diffusionY.HasValue
          ? diffusionY.Value
          : diffusionX.HasValue ? diffusionX.Value : defaultDiff,
        MinBoundaryX = Xmin.HasValue ? Xmin.Value : 0,
        MaxBoundaryX = Xmax.HasValue ? Xmax.Value : 0,
        MinBoundaryY = Ymin.HasValue ? Ymin.Value : 0,
        MaxBoundaryY = Ymax.HasValue ? Ymax.Value : 0,
        BCType = bcType,
        InitialCondition = species.getInitialExpession()
      };

      lstSpatialSpecies.Items.Add(spatialSpecies);

      grid.Rows.Add(spatialSpecies.Id,
        spatialSpecies.DiffusionX,
        spatialSpecies.DiffusionY,
        spatialSpecies.InitialCondition,
        spatialSpecies.MaxBoundaryX, spatialSpecies.MaxBoundaryY,
        spatialSpecies.MinBoundaryX, spatialSpecies.MinBoundaryY,
        spatialSpecies.BCType);
    }

    private void AddSelected(string id)
    {
      if (id == null || _model == null) return;
      if (lstSpatialSpecies.Items.Contains(id)) return;

      AddSelected(_model.getSpecies(id));
    }

    private void RemoveSelected()
    {
      object selected = lstSpatialSpecies.SelectedItem;
      if (selected == null) return;
      RemoveSelected(selected as SpatialSpecies);
    }

    private void RemoveSelected(string id)
    {
      SpatialSpecies species = GetSpecies(id);

      RemoveSelected(species);
    }

    private SpatialSpecies GetSpecies(string id)
    {
      SpatialSpecies species = null;
      for (int i = lstSpatialSpecies.Items.Count - 1; i >= 0; i--)
      {
        var current = lstSpatialSpecies.Items[i] as SpatialSpecies;
        if (current != null && current.Id == id)
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
      SpatialSpecies current = o;
      lstSpatialSpecies.Items.Remove(o);

      for (int i = grid.Rows.Count - 1; i >= 0; i--)
      {
        if (current.Id == (string) grid.Rows[i].Cells[0].Value)
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

      // Correct compartments & Transport      
      if (!MainForm.Settings.IgnoreMultiCompartments)
      SpatialModel.CorrectCompartmentsAndTransport(_model);

      txtDimX.Text = SpatialModel.Width.ToString();
      txtDimY.Text = SpatialModel.Height.ToString();

      lstSpatialSpecies.Items.Clear();
      grid.Rows.Clear();

      for (int i = 0; i < _model.getNumSpecies(); ++i)
      {
        Species current = _model.getSpecies(i);
        if (current == null) continue;
        lstAllSpecies.Items.Add(current.getId());
        if (current.getConstant() || current.getBoundaryCondition())
          continue;

        AddSelected(current);
      }

      Geometry geom = SpatialModel.Geometry;
      if (geom == null) return;

      _analyticGeometry = geom.GetFirstAnalyticGeometry();
      if (_analyticGeometry != null)
      {
        if (_analyticGeometry.getNumAnalyticVolumes() == 1 && _analyticGeometry.getAnalyticVolume(0).isSetMath()
            && (libsbml.formulaToString(_analyticGeometry.getAnalyticVolume(0).getMath()) == "1"))
          radDefault.Checked = true;
        else
          radAnalytic.Checked = true;
      }

      _sampleGeometry = geom.GetFirstSampledFieldGeometry();
      if (_sampleGeometry != null)
      {
        radSample.Checked = true;
      }
    }

    private void ShowPage(int index)
    {
      if (index < 0) index = 0;
      if (index >= tabControl1.TabCount) index = tabControl1.TabCount - 1;

      int oldIndex = tabControl1.SelectedIndex;
      if (oldIndex == index) return;

      tabControl1.SelectedIndex = index;
    }

    private void OnPrevClick(object sender, EventArgs e)
    {
      ShowPage(tabControl1.SelectedIndex - 1);
    }

    private void OnNextClick(object sender, EventArgs e)
    {
      ShowPage(tabControl1.SelectedIndex + 1);
    }

    private void OnCellEndEdit(object sender, DataGridViewCellEventArgs e)
    {
      DataGridViewRow row = grid.Rows[e.RowIndex];
      var current = (string)row.Cells[0].Value;
      SpatialSpecies species = GetSpecies(current);

      switch (e.ColumnIndex)
      {
        case 1:
          {
            species.DiffusionX = row.Cells[1].GetDouble();
            break;
          }
        case 2:
          {
            species.DiffusionY = row.Cells[2].GetDouble();
            break;
          }
        case 3:
          {
            ASTNode node = libsbml.parseFormula((string)row.Cells[3].Value);
            if (node != null)
            {
              string formula = libsbml.formulaToString(node);
              species.InitialCondition = formula;
              row.Cells[3].Value = formula;
            }
            break;
          }
        case 4:
          {
            species.MaxBoundaryX = row.Cells[4].GetDouble();
            break;
          }
        case 5:
          {
            species.MaxBoundaryY = row.Cells[5].GetDouble();
            break;
          }
        case 6:
          {
            species.MinBoundaryX = row.Cells[6].GetDouble();
            break;
          }
        case 7:
          {
            species.MinBoundaryY = row.Cells[7].GetDouble();
            break;
          }
        case 8:
          {
            species.BCType = (string)row.Cells[8].Value;
            break;
          }
        default:
          break;
      }
    }


    private void OnUserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
    {
      e.Cancel = true;
      var id = (string) e.Row.Cells[0].Value;
      RemoveSelected(id);
    }

    private void OnSelectedTabChanged(object sender, EventArgs e)
    {
      if (tabControl1.SelectedIndex == 0)
      {
        txtDimX.Text = txtWidth.Text;
        txtDimY.Text = txtHeight.Text;
        cmdNext.Enabled = true;
        cmdPrev.Enabled = false;
      }
      else if (tabControl1.SelectedIndex == 1)
      {
        txtWidth.Text = txtDimX.Text;
        txtHeight.Text = txtDimY.Text;
        cmdNext.Enabled = false;
        cmdPrev.Enabled = true;
      }
    }

    private void radAnalytic_CheckedChanged(object sender, EventArgs e)
    {
      controlAnalyticGeometry1.Visible = true;
      controlSampleFieldGeometry1.Visible = false;

      if (!radAnalytic.Checked) return;

      _geometry = SpatialModel.Geometry;

      if (SpatialModel.Document == null)
        return;

      if (_geometry == null)
      {
        _geometry = new Geometry(3, 1);
      }

      SpatialModel.CreateCoordinateSystem(_geometry,
        SpatialModel.Document.getModel(),
        new GeometrySettings
        {
          Xmax = Util.SaveDouble(txtDimX.Text, 50),
          Ymax = Util.SaveDouble(txtDimY.Text, 50),
        }
        );

      _analyticGeometry = _geometry.GetFirstAnalyticGeometry();

      if (_analyticGeometry == null)
      {
        _analyticGeometry = _geometry.createAnalyticGeometry();
        AnalyticVolume vol = _analyticGeometry.createAnalyticVolume();
        vol.setSpatialId("vol0");
        vol.setDomainType("");
        vol.setFunctionType("layered");
        vol.setMath(libsbml.parseFormula("1"));
      }

      if (!_analyticGeometry.isSetSpatialId())
        _analyticGeometry.setSpatialId("ana1");

      controlAnalyticGeometry1.InitializeFrom(_geometry, _analyticGeometry);
    }

    private void radSample_CheckedChanged(object sender, EventArgs e)
    {
      controlAnalyticGeometry1.Visible = false;
      controlSampleFieldGeometry1.Visible = true;

      if (!radSample.Checked) return;

      _geometry = SpatialModel.Geometry;

      if (SpatialModel.Document == null) return;

      if (_geometry == null)
      {
        _geometry = new Geometry(3, 1);
      }

      SpatialModel.CreateCoordinateSystem(_geometry,
        SpatialModel.Document.getModel(),
        new GeometrySettings
        {
          Xmax = Util.SaveDouble(txtDimX.Text, 50),
          Ymax = Util.SaveDouble(txtDimY.Text, 50),
        }
        );

      _sampleGeometry = _geometry.GetFirstSampledFieldGeometry();

      if (_sampleGeometry == null)
      {
        _sampleGeometry = _geometry.createSampledFieldGeometry();
      }

      if (!_sampleGeometry.isSetSpatialId())
        _sampleGeometry.setSpatialId("sample1");

      controlSampleFieldGeometry1.InitializeFrom(_geometry, _sampleGeometry.getSpatialId());
    }

    private void radDefault_CheckedChanged(object sender, EventArgs e)
    {
      controlAnalyticGeometry1.Visible = false;
      controlSampleFieldGeometry1.Visible = false;
    }

    private void OnApplyDiffClick(object sender, EventArgs e)
    {
      double defaultDiff;
      if (!double.TryParse(txtDiffDefault.Text, out defaultDiff))
        return;

      for (int i = 0; i < grid.Rows.Count; ++i)
      {
        DataGridViewRow current = grid.Rows[i];
        if (current.IsNewRow) break;
        current.Cells[1].Value = defaultDiff;
        current.Cells[2].Value = defaultDiff;

        var id = current.Cells[0].Value as string;
        SpatialSpecies species = GetSpecies(id);
        if (species == null)
          continue;

        species.DiffusionX = defaultDiff;
        species.DiffusionY = defaultDiff;
      }
    }

    private void OnMakeIsotropic(object sender, EventArgs e)
    {
      for (int i = 0; i < grid.Rows.Count; ++i)
      {
        DataGridViewRow current = grid.Rows[i];
        if (current.IsNewRow) break;
        current.Cells[2].Value = current.Cells[1].Value;

        var id = current.Cells[0].Value as string;
        SpatialSpecies species = GetSpecies(id);
        if (species == null)
          continue;

        species.DiffusionY = species.DiffusionX;
      }
    }
  }
}