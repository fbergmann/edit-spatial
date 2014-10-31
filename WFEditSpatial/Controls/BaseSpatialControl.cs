using System;
using System.Windows.Forms;

namespace EditSpatial.Controls
{
  public partial class BaseSpatialControl : UserControl
  {
    public BaseSpatialControl()
    {
      InitializeComponent();
    }

    public bool IsInitializing { get; set; }

    public Action UpdateAction { get; set; }

    public virtual void SaveChanges()
    {
    }

    public virtual void InvalidateSelection()
    {
    }
  }
}