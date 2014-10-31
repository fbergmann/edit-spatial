using System.Collections.Generic;
using System.Windows.Forms;
using libsbmlcs;

namespace EditSpatial.Controls
{
  public partial class ControlCompartment : BaseSpatialControl
  {
    public ControlCompartment()
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
        libsbmlcs.Model model = document.getModel();

        CommitAddedRows(Current);
        for (long i = 0; i < model.getNumCompartments(); ++i)
        {
          Compartment current = model.getCompartment(i);
          grid.Rows.Add(current.getId(),
            current.getName(),
            current.getSize().ToString(),
            current.getSpatialDimensionsAsDouble().ToString()
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
        for (int i = RowsAdded.Count - 1; i >= 0; i--)
        {
          try
          {
            DataGridViewRow row = grid.Rows[RowsAdded[i]];
            Compartment comp = current.getModel().createCompartment();
            comp.initDefaults();
            comp.setId(row.Cells[0].Value as string);
            comp.setName(row.Cells[1].Value as string);
            double value;
            if (double.TryParse((string) row.Cells[2].Value, out value))
              comp.setSize(value);
            if (double.TryParse((string) row.Cells[3].Value, out value))
              comp.setSpatialDimensions(value);
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

      for (int i = 0; i < grid.Rows.Count; ++i)
      {
        DataGridViewRow row = grid.Rows[i];
        Compartment current = Current.getModel().getCompartment((string) row.Cells[0].Value);
        if (current == null) continue;

        current.setName(row.Cells[1].Value as string);
        double value;
        if (double.TryParse((string) row.Cells[2].Value, out value))
          current.setSize(value);
        if (double.TryParse((string) row.Cells[3].Value, out value))
          current.setSpatialDimensions(value);
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