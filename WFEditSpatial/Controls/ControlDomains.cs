using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EditSpatial.Model;
using libsbmlcs;

namespace EditSpatial.Controls
{
  public partial class ControlDomains : BaseSpatialControl
  {
    public ControlDomains()
    {
      InitializeComponent();
    }

    private Geometry Current { get; set; }

    public void InitializeFrom(Geometry geometry)
    {
      grid.Rows.Clear();
      Current = geometry;
      if (geometry == null) return;

      for (long i = 0; i < geometry.getNumDomains(); ++i)
      {
        var domain = geometry.getDomain(i);
        var domainType = domain.getDomainType();
        var spatialId = domain.getSpatialId();
        var builder = new StringBuilder();
        for (long j = 0; j < domain.getNumInteriorPoints(); ++j)
        {
          var current = domain.getInteriorPoint(j);
          builder.Append("(");
          builder.Append(current.getCoord1());
          builder.Append(";");
          builder.Append(current.getCoord2());
          builder.Append(";");
          builder.Append(current.getCoord3());
          builder.Append(")");
          if (j + 1 < domain.getNumInteriorPoints())
            builder.Append(", ");
        }
        grid.Rows.Add(spatialId, domainType, builder.ToString());
      }
    }

    public override void SaveChanges()
    {
      if (Current == null) return;

      for (var i = 0; i < grid.Rows.Count && i < Current.getNumDomains(); ++i)
      {
        var current = Current.getDomain(i);
        var row = grid.Rows[i];
        current.setSpatialId((string) row.Cells[0].Value);
        current.setDomainType((string) row.Cells[1].Value);

        var points = GetPoints((string) row.Cells[2].Value);
        for (var j = 0; j < points.Count && j < current.getNumInteriorPoints(); ++j)
        {
          var interior = current.getInteriorPoint(j);
          var point = points[j];
          interior.setCoord1(point.Item1);
          interior.setCoord2(point.Item2);
          interior.setCoord3(point.Item3);
        }
      }
      OnModelChanged();
    }

    private List<Tuple<double, double, double>> GetPoints(string value)
    {
      value = value.Replace("(", "");
      value = value.Replace(")", "");
      var points = value.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries);
      return (from point in points
        select point.Split(new[] {';'}, StringSplitOptions.RemoveEmptyEntries)
        into coords
        where coords.Length == 3
        select
          new Tuple<double, double, double>
            (Util.SaveDouble(coords[0]),
              Util.SaveDouble(coords[1]),
              Util.SaveDouble(coords[2])))
        .ToList();
    }

    public override void InvalidateSelection()
    {
      Current = null;
      InitializeFrom(null);
    }
  }
}