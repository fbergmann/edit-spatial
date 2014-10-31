using System.Collections.Generic;
using System.Windows.Forms;
using libsbmlcs;

namespace EditSpatial.Controls
{
  public partial class ControlParameters : BaseSpatialControl
  {
    public ControlParameters()
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
        for (long i = 0; i < model.getNumParameters(); ++i)
        {
          Parameter current = model.getParameter(i);
          grid.Rows.Add(current.getId(),
            current.getName(),
            current.getValue().ToString());
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
            int index = RowsAdded[i];
            if (index < 0) continue;
            DataGridViewRow row = grid.Rows[index];
            Parameter param = current.getModel().createParameter();
            param.initDefaults();
            param.setId(row.Cells[0].Value as string);
            param.setName(row.Cells[1].Value as string);
            double value;
            if (double.TryParse((string) row.Cells[2].Value, out value))
              param.setValue(value);
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
        Parameter current = Current.getModel().getParameter((string) row.Cells[0].Value);
        if (current == null) continue;

        current.setName((string) row.Cells[1].Value);
        double value;
        if (double.TryParse((string) row.Cells[2].Value, out value))
          current.setValue(value);
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

      var ids = new List<string>();
      if (grid.SelectedRows.Count > 1)
      {
        foreach (DataGridViewRow row in grid.SelectedRows)
        {
          ids.Add(row.Cells[0].Value as string);
        }
      }
      else
      {
        ids.Add(e.Row.Cells[0].Value as string);
      }

      foreach (string id in ids)
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