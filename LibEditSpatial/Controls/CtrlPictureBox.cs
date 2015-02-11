using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LibEditSpatial.Controls
{
  public partial class CtrlPictureBox : PictureBox
  {
    public CtrlPictureBox()
    {
      InitializeComponent();
    }

    public InterpolationMode InterpolationMode { get; set; }

    protected override void OnPaint(PaintEventArgs paintEventArgs)
    {
      paintEventArgs.Graphics.InterpolationMode = InterpolationMode;
      base.OnPaint(paintEventArgs);
    }

  }
}
