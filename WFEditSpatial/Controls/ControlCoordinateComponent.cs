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
  public partial class ControlCoordinateComponent : BaseSpatialControl
  {
    CoordinateComponent Current { get; set;  }


    public void InitializeFrom(CoordinateComponent comp)
    {
      Current = comp;
      if (Current == null)
        return;
      txtId.Text = comp.getSpatialId();
      txtType.Text = comp.getComponentType();
      txtIndex.Text = comp.getIndex().ToString();
      txtUnit.Text = comp.getSbmlUnit();

      var min = comp.getBoundaryMin();
      txtMinId.Text = min.getSpatialId();
      txtMinValue.Text = min.getValue().ToString();

      var max = comp.getBoundaryMax();
      txtMaxId.Text = max.getSpatialId();
      txtMaxValue.Text = max.getValue().ToString();

    }

    public ControlCoordinateComponent()
    {
      InitializeComponent();
    }

    public override void SaveChanges()
    {
      if (Current == null) return;

      Current.setSpatialId(txtId.Text);
      Current.setComponentType(txtType.Text);
      Current.setIndex(Util.SaveInt(txtIndex.Text, Current.getIndex()));
      Current.setSbmlUnit(txtUnit.Text);

      var min = Current.getBoundaryMin();
      min.setSpatialId(txtMinId.Text);
      min.setValue(Util.SaveDouble(txtMinValue.Text, min.getValue()));

      var max = Current.getBoundaryMax();
      max.setSpatialId(txtMaxId.Text);
      max.setValue(Util.SaveDouble(txtMaxValue.Text, max.getValue()));

    }

    public override void InvalidateSelection()
    {
      Current = null;
      InitializeFrom(Current);
    }
  }
}
