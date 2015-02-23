using libsbmlcs;

namespace EditSpatial.Controls
{
  public partial class ControlInitialAssignments : BaseSpatialControl
  {
    public ControlInitialAssignments()
    {
      InitializeComponent();
    }

    private SBMLDocument Current { get; set; }

    public void InitializeFrom(SBMLDocument document)
    {
      grid.Rows.Clear();
      Current = document;
      if (document == null || document.getModel() == null) return;

      var model = document.getModel();


      for (long i = 0; i < model.getNumInitialAssignments(); ++i)
      {
        var current = model.getInitialAssignment(i);
        grid.Rows.Add(current.getSymbol(),
          current.isSetMath()
            ? libsbml.formulaToL3String(current.getMath())
            : "");
      }
    }

    public override void SaveChanges()
    {
      if (Current == null) return;
      for (var i = 0; i < grid.Rows.Count; ++i)
      {
        var row = grid.Rows[i];
        var value = (string) row.Cells[1].Value;
        if (string.IsNullOrEmpty(value)) continue;
        var node = libsbml.parseL3Formula(value);
        if (node == null) continue;

        var current = Current.getModel().getInitialAssignment((string) row.Cells[0].Value);
        if (current == null)
          current = Current.getModel().createInitialAssignment();

        current.setSymbol((string) row.Cells[0].Value);
        current.setMath(node);
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