﻿using libsbmlcs;

namespace EditSpatial.Controls
{
  public partial class ControlAdjacentDomains : BaseSpatialControl
  {
    public ControlAdjacentDomains()
    {
      InitializeComponent();
    }

    public Geometry Current { get; set; }

    public void InitializeFrom(Geometry geometry)
    {
      grid.Rows.Clear();
      Current = geometry;
      if (geometry == null) return;

      for (long i = 0; i < geometry.getNumAdjacentDomains(); ++i)
      {
        var domain = geometry.getAdjacentDomains(i);
        var spatialId = domain.getId();
        grid.Rows.Add(spatialId, domain.getDomain1(), domain.getDomain2());
      }
    }

    public override void SaveChanges()
    {
      if (Current == null) return;

      for (var i = 0; i < grid.Rows.Count && i < Current.getNumAdjacentDomains(); ++i)
      {
        var domain = Current.getAdjacentDomains(i);
        var row = grid.Rows[i];
        if (domain == null) continue;

        domain.setId((string) row.Cells[0].Value);
        domain.setDomain1((string) row.Cells[1].Value);
        domain.setDomain2((string) row.Cells[2].Value);
      }

      OnModelChanged();
    }

    public override void InvalidateSelection()
    {
      Current = null;
      InitializeFrom(null);
    }
  }
}