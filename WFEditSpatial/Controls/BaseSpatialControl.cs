using System;
using System.Windows.Forms;
using EditSpatial.Model;

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
    
    public event EventHandler ModelChanged;

    public virtual void SaveChanges()
    {
    }

    public virtual void InvalidateSelection()
    {
    }


    protected virtual void OnModelChanged()
    {
      var handler = ModelChanged;
      if (handler != null) handler(this, EventArgs.Empty);
    }
  }

  
}