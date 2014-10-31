using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EditSpatial.Controls
{
  public partial class ControlDisplayNode : BaseSpatialControl
  {
    public ControlDisplayNode()
    {
      InitializeComponent();
      controlText1.WordWrap = true;
      controlText1.ReadOnly = true;
    }

    public void DisplayNode(TreeNode node)
    {      
      var sbml = node == null ? null :  (string)node.Tag;
      if (string.IsNullOrWhiteSpace(sbml))
        sbml = "<none>";
      sbml = sbml.Replace("\n", Environment.NewLine);
      controlText1.Text = sbml;
    }

    public override void SaveChanges()
    {
      // not supported ... 
    }

    public override void InvalidateSelection()
    {
      DisplayNode(null);
    }
  }
}
