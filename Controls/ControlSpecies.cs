using System;
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
  public partial class ControlSpecies : BaseSpatialControl
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
      for (long i = 0; i < model.getNumSpecies(); ++i)
      {
        var current = model.getSpecies(i);
          grid.Rows.Add(current.getId(), 
          current.getName(), 
          current.isSetInitialConcentration() ?  current.getInitialConcentration().ToString() :
           current.getInitialAmount().ToString(), 
           current.getCompartment(), 
           current.getBoundaryCondition().ToString()
          );
      }
      }
      finally
      {
        IsInitializing = false;       
      }
    }

    public ControlSpecies()
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
            var row = grid.Rows[RowsAdded[i]];
            var species = current.getModel().createSpecies();
            species.initDefaults();
            species.setId(row.Cells[0].Value as string);
            species.setName(row.Cells[1].Value as string);
            double value; if (double.TryParse((string)row.Cells[2].Value, out value))
              species.setInitialConcentration(value);
            species.setCompartment(row.Cells[3].Value as string);
            bool bvalue; if (bool.TryParse((string)row.Cells[2].Value, out bvalue))
              species.setBoundaryCondition(bvalue);
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
        var current = Current.getModel().getSpecies((string)row.Cells[0].Value);
        if (current == null) continue;

        current.setName(row.Cells[1].Value as string);
        double value; if (double.TryParse((string)row.Cells[2].Value, out value))
          current.setInitialConcentration(value);
        current.setCompartment(row.Cells[3].Value as string);
        bool bvalue; if (bool.TryParse((string)row.Cells[2].Value, out bvalue))
          current.setBoundaryCondition(bvalue);
      
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
      if (Current == null || IsInitializing) return;

      RowsAdded.Add(e.RowIndex - 1);
    }

    public List<int> RowsAdded { get; set; }

  }
}
