using System.ComponentModel;
using System.Windows.Forms;

namespace LibEditSpatial.Controls
{
  partial class DmpRenderControl
  {
    /// <summary> 
    /// Required designer variable.
    /// </summary>
    private IContainer components = null;

    /// <summary> 
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      this.pictureBox1 = new LibEditSpatial.Controls.CtrlPictureBox();
      this.timer1 = new System.Windows.Forms.Timer(this.components);
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
      this.SuspendLayout();
      // 
      // pictureBox1
      // 
      this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pictureBox1.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
      this.pictureBox1.Location = new System.Drawing.Point(0, 0);
      this.pictureBox1.Name = "pictureBox1";
      this.pictureBox1.Size = new System.Drawing.Size(150, 150);
      this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pictureBox1.TabIndex = 0;
      this.pictureBox1.TabStop = false;
      this.pictureBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.OnMouseClick);
      this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnMouseDown);
      this.pictureBox1.MouseLeave += new System.EventHandler(this.OnMouseLeave);
      this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.OnMouseMove);
      this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OnMouseUp);
      // 
      // timer1
      // 
      this.timer1.Tick += new System.EventHandler(this.OnTimerTick);
      // 
      // DmpRenderControl
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
      this.Controls.Add(this.pictureBox1);
      this.DoubleBuffered = true;
      this.Name = "DmpRenderControl";
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private CtrlPictureBox pictureBox1;
    private Timer timer1;
  }
}
