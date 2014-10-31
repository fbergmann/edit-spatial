using System.Windows.Forms;
using libsbmlcs;

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
        AdjacentDomains domain = geometry.getAdjacentDomains(i);
        string spatialId = domain.getSpatialId();
        grid.Rows.Add(spatialId, domain.getDomain1(), domain.getDomain2());
      }
    }

    public override void SaveChanges()
    {
      if (Current == null) return;

      for (int i = 0; i < grid.Rows.Count && i < Current.getNumAdjacentDomains(); ++i)
      {
        AdjacentDomains domain = Current.getAdjacentDomains(i);
        DataGridViewRow row = grid.Rows[i];
        if (domain == null) continue;

        domain.setSpatialId((string) row.Cells[0].Value);
        domain.setDomain1((string) row.Cells[1].Value);
        domain.setDomain2((string) row.Cells[2].Value);
      }
    }

    public override void InvalidateSelection()
    {
      Current = null;
      InitializeFrom(null);
    }
  }
}