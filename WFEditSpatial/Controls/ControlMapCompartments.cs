using System.Windows.Forms;
using EditSpatial.Model;
using libsbmlcs;

namespace EditSpatial.Controls
{
  public partial class ControlMapCompartments : BaseSpatialControl
  {
    public ControlMapCompartments()
    {
      InitializeComponent();
    }

    private libsbmlcs.Model Current { get; set; }

    public void InitializeFrom(libsbmlcs.Model model)
    {
      grid.Rows.Clear();
      Current = model;

      colDomainType.Items.Clear();

      if (model == null) return;

      var mplug = (SpatialModelPlugin)model.getPlugin("spatial");
      if (mplug != null)
      {
        var dtypes = mplug.getGeometry().getListOfDomainTypes();
        for (int i = 0; i < dtypes.size(); ++i)
        {
          var dtype = dtypes.get(i);
          if (dtype == null) continue;
          colDomainType.Items.Add(dtype.getSpatialId());
        }
      }


      for (long i = 0; i < model.getNumCompartments(); ++i)
      {
        var  comp = model.getCompartment(i);
        var map = comp.getCompartmentMapping();
        grid.Rows.Add(comp.getId(), (map == null) ? "" : map.getDomainType() );
      }
    }

    public override void SaveChanges()
    {
      if (Current == null) return;
      for (int i = 0; i < grid.Rows.Count && i < Current.getNumCompartments(); ++i)
      {
        var row = grid.Rows[i];
        var current = Current.getCompartment((string)row.Cells[0].Value);
        var map = current.getCompartmentMapping();
        if (map == null) continue;
        map.setDomainType((string) row.Cells[1].Value);
      }
    }

    public override void InvalidateSelection()
    {
      Current = null;
      InitializeFrom(null);
    }
  }
}