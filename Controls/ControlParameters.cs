﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using libsbmlcs;

using Model = libsbmlcs.Model;

namespace EditSpatial.Controls
{
  public partial class ControlParameters : BaseSpatialControl
  {

    libsbmlcs.SBMLDocument Current { get; set; }

    public void InitializeFrom(libsbmlcs.SBMLDocument document)
    {
      grid.Rows.Clear();
      Current = document;
      if (document == null || document.getModel() == null) return;
      try
      {
        IsInitializing = true;
      var model = document.getModel();

      CommitAddedRows(Current);
      for (long i = 0; i < model.getNumParameters(); ++i)
      {
        var current = model.getParameter(i);
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

    public ControlParameters()
    {
      InitializeComponent();
      RowsAdded = new List<int>();
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
            var row = grid.Rows[index];
            var param = current.getModel().createParameter();
            param.initDefaults();
            param.setId(row.Cells[0].Value as string);
            param.setName(row.Cells[1].Value as string);
            double value; if (double.TryParse((string)row.Cells[2].Value, out value))
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
        var row = grid.Rows[i];
        var current = Current.getModel().getParameter((string)row.Cells[0].Value);
        if (current == null) continue;

        current.setName((string) row.Cells[1].Value);
        double value; if (double.TryParse((string)row.Cells[2].Value, out value))
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

      foreach (var id in ids)
      Current.getModel().removeParameter(id);

      InitializeFrom(Current);
    }

    private void OnRowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
    {
      if (Current == null || IsInitializing || e.RowIndex == 0) return;

      RowsAdded.Add(e.RowIndex - 1);
    }

    public List<int> RowsAdded { get; set; }

  }
}
