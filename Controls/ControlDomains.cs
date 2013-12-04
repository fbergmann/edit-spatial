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

namespace EditSpatial.Controls
{
  public partial class ControlDomains : BaseSpatialControl
  {

    Geometry Current { get; set;  }

    public void InitializeFrom(Geometry geometry)
    {
      grid.Rows.Clear();
      Current = geometry;
      if (geometry == null) return;

      for (long i = 0; i < geometry.getNumDomains(); ++i)
      {
        var domain = geometry.getDomain(i);
        var domainType = domain.getDomainType();
        string spatialId = domain.getSpatialId();
        var builder = new StringBuilder();
        for (long j = 0; j < domain.getNumInteriorPoints(); ++j)
        {
          var current = domain.getInteriorPoint(j);
          builder.Append("(");
          builder.Append(current.getCoord1());
          builder.Append(";");
          builder.Append(current.getCoord2());
          builder.Append(";");
          builder.Append(current.getCoord2());
          builder.Append(")");
          if (j+1 < domain.getNumInteriorPoints())
            builder.Append(", ");
        }
        grid.Rows.Add(spatialId, domainType, builder.ToString());
      }
    }

    public ControlDomains()
    {
      InitializeComponent();
    }

    public override void SaveChanges()
    {
      if (Current == null) return;

      for (int i = 0; i < grid.Rows.Count && i < Current.getNumDomains(); ++i)
      {
        var current = Current.getDomain(i);
        var row = grid.Rows[i];
        current.setSpatialId((string) row.Cells[0].Value);
        current.setDomainType((string) row.Cells[1].Value);

        var points = GetPoints((string) row.Cells[2].Value);
        for (int j = 0; j < points.Count && j < current.getNumInteriorPoints(); ++j)
        {
          var interior = current.getInteriorPoint(j);
          var point = points[j];
          interior.setCoord1(point.Item1);
          interior.setCoord2(point.Item2);
          interior.setCoord3(point.Item3);
        }
      }

    }

    private List<Tuple<double,double,double>> GetPoints(string value)
    {
      var result = new List<Tuple<double, double, double>>();
      var points = value.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries);
      foreach (var point in points)
      {
        var coords = point.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
        if (coords.Length != 3) continue;
        result.Add(new Tuple<double, double, double>(
          Util.SaveDouble(coords[0]), Util.SaveDouble(coords[1]), Util.SaveDouble(coords[2])));
      }
      return result;
    }

    public override void InvalidateSelection()
    {
      Current = null;
      InitializeFrom(null);
    }
  }
}
