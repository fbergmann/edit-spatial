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

using Model = libsbmlcs.Model;

namespace EditSpatial.Controls
{
  public partial class ControlRules : BaseSpatialControl
  {

    libsbmlcs.SBMLDocument Current { get; set; }

    private static string  RuleName(int typeCode)
    {
      if (typeCode == libsbml.SBML_ALGEBRAIC_RULE)
        return "AlgebraicRule";
      if (typeCode == libsbml.SBML_RATE_RULE)
        return "RateRule";
      return "AssignmentRule";
    }


    public void InitializeFrom(libsbmlcs.SBMLDocument document)
    {
      grid.Rows.Clear();
      Current = document;
      if (document == null || document.getModel() == null) return;

      var model = document.getModel();


      for (long i = 0; i < model.getNumRules(); ++i)
      {
        var current = model.getRule(i);
        grid.Rows.Add(current.getVariable(), 
          current.isSetMath() ? libsbml.formulaToL3String(current.getMath()): "", 
          RuleName(current.getTypeCode()));
      }
    }

    public ControlRules()
    {
      InitializeComponent();
    }

    private libsbmlcs.Rule CreateRule(libsbmlcs.Model model, string type)
    {
      if (type == "AlgebraicRule")
        return model.createAlgebraicRule();
      if (type == "RateRule")
        return model.createRateRule();
      return model.createAssignmentRule();
    }

    public override void SaveChanges()
    {
      if (Current == null) return;
      for (int i = 0; i < grid.Rows.Count; ++i)
      {
        var row = grid.Rows[i];
        string value = (string)row.Cells[1].Value;
        if (string.IsNullOrEmpty(value)) continue;
        var node = libsbml.parseL3Formula(value);
        if (node == null) continue;

        var current = Current.getModel().getRuleByVariable((string)row.Cells[0].Value);
        if (current == null)
        { 
          current = CreateRule(Current.getModel(), (string)row.Cells[2].Value);
        }

        current.setVariable((string)row.Cells[0].Value);
        current.setMath(node);

      }
    }

    public override void InvalidateSelection()
    {
      Current = null;
      InitializeFrom(null);
    }

    
  }
}
