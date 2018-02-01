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
      txtId.Text = comp.getId();
      txtType.Text = libsbml.CoordinateKind_toString(comp.getType());
      txtIndex.Text = comp.getType().ToString();
      txtUnit.Text = comp.getUnit();

      var min = comp.getBoundaryMin();
      txtMinId.Text = min.getId();
      txtMinValue.Text = min.getValue().ToString();

      var max = comp.getBoundaryMax();
      txtMaxId.Text = max.getId();
      txtMaxValue.Text = max.getValue().ToString();
    }

    public override void SaveChanges()
    {
      if (Current == null) return;

      Current.setId(txtId.Text);
      //Current.setComponentType(txtType.Text);
      Current.setType(Util.SaveInt(txtIndex.Text, Current.getType()));
      Current.setUnit(txtUnit.Text);

      var min = Current.getBoundaryMin();
      min.setId(txtMinId.Text);
      min.setValue(Util.SaveDouble(txtMinValue.Text, min.getValue()));

      var max = Current.getBoundaryMax();
      max.setId(txtMaxId.Text);
      max.setValue(Util.SaveDouble(txtMaxValue.Text, max.getValue()));
    }

    public override void InvalidateSelection()
    {
      Current = null;
      InitializeFrom(Current);
    }
  }
}