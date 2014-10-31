using EditSpatial.Model;
using libsbmlcs;

namespace EditSpatial.Controls
{
  public partial class ControlCoordinateComponent : BaseSpatialControl
  {
    public ControlCoordinateComponent()
    {
      InitializeComponent();
    }

    private CoordinateComponent Current { get; set; }


    public void InitializeFrom(CoordinateComponent comp)
    {
      Current = comp;
      if (Current == null)
        return;
      txtId.Text = comp.getSpatialId();
      txtType.Text = comp.getComponentType();
      txtIndex.Text = comp.getIndex().ToString();
      txtUnit.Text = comp.getSbmlUnit();

      BoundaryMin min = comp.getBoundaryMin();
      txtMinId.Text = min.getSpatialId();
      txtMinValue.Text = min.getValue().ToString();

      BoundaryMax max = comp.getBoundaryMax();
      txtMaxId.Text = max.getSpatialId();
      txtMaxValue.Text = max.getValue().ToString();
    }

    public override void SaveChanges()
    {
      if (Current == null) return;

      Current.setSpatialId(txtId.Text);
      Current.setComponentType(txtType.Text);
      Current.setIndex(Util.SaveInt(txtIndex.Text, Current.getIndex()));
      Current.setSbmlUnit(txtUnit.Text);

      BoundaryMin min = Current.getBoundaryMin();
      min.setSpatialId(txtMinId.Text);
      min.setValue(Util.SaveDouble(txtMinValue.Text, min.getValue()));

      BoundaryMax max = Current.getBoundaryMax();
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