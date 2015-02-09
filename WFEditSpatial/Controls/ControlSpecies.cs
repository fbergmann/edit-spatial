using System.Collections.Generic;
using System.Windows.Forms;
using libsbmlcs;

namespace EditSpatial.Controls
{
  public partial class ControlSpecies : BaseSpatialControl
  {
    public ControlSpecies()
    {
      InitializeComponent();
      RowsAdded = new List<int>();
    }

    private SBMLDocument Current { get; set; }
    public List<int> RowsAdded { get; set; }

    public void InitializeFrom(SBMLDocument document)
    {
      grid.Rows.Clear();
      Current = document;
      if (document == null || document.getModel() == null) return;
      try
      {
        IsInitializing = true;
        var model = document.getModel();

        CommitAddedRows(Current);
        for (long i = 0; i < model.getNumSpecies(); ++i)
        {
          var current = model.getSpecies(i);

          var plug = (SpatialSpeciesRxnPlugin) current.getPlugin("spatial");
          var isSpatial = "NA";
          if (plug != null && plug.isSetIsSpatial())
          {
            isSpatial = plug.getIsSpatial().ToString();
          }

          grid.Rows.Add(current.getId(),
            current.getName(),
            current.isSetInitialConcentration()
              ? current.getInitialConcentration().ToString()
              : current.getInitialAmount().ToString(),
            current.getCompartment(),
            current.getBoundaryCondition().ToString(),
            isSpatial
            );
        }
      }
      finally
      {
        IsInitializing = false;
      }
    }

    private void CommitAddedRows(SBMLDocument current)
    {
      if (RowsAdded.Count > 0 && current != null)
      {
        for (var i = RowsAdded.Count - 1; i >= 0; i--)
        {
          try
          {
            var row = grid.Rows[RowsAdded[i]];
            var species = current.getModel().createSpecies();
            species.initDefaults();
            species.setId(row.Cells[0].Value as string);
            species.setName(row.Cells[1].Value as string);
            double value;
            if (double.TryParse((string) row.Cells[2].Value, out value))
              species.setInitialConcentration(value);
            species.setCompartment(row.Cells[3].Value as string);
            bool bvalue;
            if (bool.TryParse((string) row.Cells[4].Value, out bvalue))
              species.setBoundaryCondition(bvalue);

            if (bool.TryParse((string) row.Cells[5].Value, out bvalue))
            {
              var plug = (SpatialSpeciesRxnPlugin) current.getPlugin("spatial");
              if (plug != null)
              {
                plug.setIsSpatial(bvalue);
              }
            }


            RowsAdded.RemoveAt(i);
          }
          catch
          {
          }
        }
      }
    }

    public override void SaveChanges()
    {
      if (Current == null) return;

      CommitAddedRows(Current);

      for (var i = 0; i < grid.Rows.Count; ++i)
      {
        var row = grid.Rows[i];
        var current = Current.getModel().getSpecies((string) row.Cells[0].Value);
        if (current == null) continue;

        current.setName(row.Cells[1].Value as string);
        double value;
        if (double.TryParse((string) row.Cells[2].Value, out value))
          current.setInitialConcentration(value);
        current.setCompartment(row.Cells[3].Value as string);
        bool bvalue;
        if (bool.TryParse((string) row.Cells[4].Value, out bvalue))
          current.setBoundaryCondition(bvalue);

        if (bool.TryParse((string) row.Cells[5].Value, out bvalue))
        {
          var plug = (SpatialSpeciesRxnPlugin) current.getPlugin("spatial");
          if (plug != null)
          {
            plug.setIsSpatial(bvalue);
          }
        }
      }
    }

    public override void InvalidateSelection()
    {
      Current = null;
      InitializeFrom(null);
    }

    private void OnUserDeletedRow(object sender, DataGridViewRowEventArgs e)
    {
      if (Current == null) return;
      var id = e.Row.Cells[0].Value as string;
      Current.getModel().removeParameter(id);
      InitializeFrom(Current);
    }

    private void OnRowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
    {
      if (Current == null || IsInitializing || e.RowIndex == 0) return;

      RowsAdded.Add(e.RowIndex - 1);
    }
  }
}