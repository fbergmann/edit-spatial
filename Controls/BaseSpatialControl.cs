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
  public  partial class BaseSpatialControl : UserControl
  {

    public virtual void SaveChanges()
    {
      
    }

    public virtual void InvalidateSelection()
    {
      
    }


    public BaseSpatialControl()
    {
      InitializeComponent();
    }
  }
}
