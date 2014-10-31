﻿using System;
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
  public partial class ControlCoordinateComponents : BaseSpatialControl
  {

    public Geometry Current { get; set; }

    public void InitializeFrom(Geometry geometry)
    {
      while (tblLayout.Controls.Count > 1)
        tblLayout.Controls.RemoveAt(1);

      // remove old ones
      while (tblLayout.RowStyles.Count > 1)
        tblLayout.RowStyles.RemoveAt(1);

      Current = geometry;
      if (geometry == null) return;
      txtCoordSystem.Text = geometry.getCoordinateSystem();

      for (long i = 0; i < geometry.getNumCoordinateComponents(); ++i)
      {
        tblLayout.RowStyles.Add(new RowStyle
        {
          SizeType = SizeType.AutoSize
        });
        
        var current = geometry.getCoordinateComponent(i);
        var control = new ControlCoordinateComponent {Dock = DockStyle.Fill};
        control.InitializeFrom(current);
        this.tblLayout.Controls.Add(control, 0, ((int)i) + 1);

      }

    }

    public override void SaveChanges()
    {
      if (Current == null) return;
      Current.setCoordinateSystem(txtCoordSystem.Text);

      foreach (var control in tblLayout.Controls)
      {
        var current = control as BaseSpatialControl;
        if (current == null) continue;
        current.SaveChanges();
      }

    }

    public ControlCoordinateComponents()
    {
      InitializeComponent();
    }

    public override void InvalidateSelection()
    {
      Current = null;
      InitializeFrom(null);
    }
  }
}