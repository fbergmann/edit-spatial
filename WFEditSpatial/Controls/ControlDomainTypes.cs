﻿using System.Windows.Forms;
using libsbmlcs;

namespace EditSpatial.Controls
{
  public partial class ControlDomainTypes : BaseSpatialControl
  {
    public ControlDomainTypes()
    {
      InitializeComponent();
    }

    private Geometry Current { get; set; }

    public void InitializeFrom(Geometry geometry)
    {
      grid.Rows.Clear();
      Current = geometry;
      if (geometry == null) return;
      for (long i = 0; i < geometry.getNumDomainTypes(); ++i)
      {
        DomainType domainType = geometry.getDomainType(i);
        grid.Rows.Add(domainType.getSpatialId(), domainType.getSpatialDimensions());
      }
    }

    public override void SaveChanges()
    {
      if (Current == null) return;
      for (int i = 0; i < grid.Rows.Count && i < Current.getNumDomainTypes(); ++i)
      {
        DomainType current = Current.getDomainType(i);
        DataGridViewRow row = grid.Rows[i];
        current.setSpatialId((string) row.Cells[0].Value);
        current.setSpatialDimensions((long) row.Cells[1].Value);
      }
    }

    public override void InvalidateSelection()
    {
      Current = null;
      InitializeFrom(null);
    }
  }
}