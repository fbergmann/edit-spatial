namespace WFEditDMP.Controls
{
  partial class CtrlPalette
  {
    /// <summary> 
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CtrlPalette));
      this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
      this.pictureBox1 = new System.Windows.Forms.PictureBox();
      this.txtMin = new System.Windows.Forms.TextBox();
      this.txtCurrent = new System.Windows.Forms.TextBox();
      this.trackBar1 = new System.Windows.Forms.TrackBar();
      this.txtMax = new System.Windows.Forms.TextBox();
      this.panel1 = new System.Windows.Forms.Panel();
      this.toolStrip1 = new System.Windows.Forms.ToolStrip();
      this.openToolStripButton = new System.Windows.Forms.ToolStripButton();
      this.saveToolStripButton = new System.Windows.Forms.ToolStripButton();
      this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
      this.tableLayoutPanel1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
      this.panel1.SuspendLayout();
      this.toolStrip1.SuspendLayout();
      this.SuspendLayout();
      // 
      // tableLayoutPanel1
      // 
      this.tableLayoutPanel1.ColumnCount = 5;
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
      this.tableLayoutPanel1.Controls.Add(this.pictureBox1, 0, 1);
      this.tableLayoutPanel1.Controls.Add(this.txtMin, 0, 0);
      this.tableLayoutPanel1.Controls.Add(this.txtCurrent, 1, 0);
      this.tableLayoutPanel1.Controls.Add(this.trackBar1, 2, 0);
      this.tableLayoutPanel1.Controls.Add(this.txtMax, 3, 0);
      this.tableLayoutPanel1.Controls.Add(this.panel1, 4, 0);
      this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
      this.tableLayoutPanel1.Name = "tableLayoutPanel1";
      this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(5);
      this.tableLayoutPanel1.RowCount = 2;
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
      this.tableLayoutPanel1.Size = new System.Drawing.Size(857, 99);
      this.tableLayoutPanel1.TabIndex = 0;
      // 
      // pictureBox1
      // 
      this.tableLayoutPanel1.SetColumnSpan(this.pictureBox1, 5);
      this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pictureBox1.Location = new System.Drawing.Point(8, 38);
      this.pictureBox1.Name = "pictureBox1";
      this.pictureBox1.Size = new System.Drawing.Size(841, 53);
      this.pictureBox1.TabIndex = 0;
      this.pictureBox1.TabStop = false;
      this.toolTip1.SetToolTip(this.pictureBox1, "Pick Value");
      this.pictureBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.OnMouseClick);
      this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnMouseDown);
      this.pictureBox1.MouseLeave += new System.EventHandler(this.OnMouseLeave);
      this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.OnMouseMove);
      this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OnMouseUp);
      // 
      // txtMin
      // 
      this.txtMin.Dock = System.Windows.Forms.DockStyle.Fill;
      this.txtMin.Location = new System.Drawing.Point(8, 8);
      this.txtMin.Name = "txtMin";
      this.txtMin.Size = new System.Drawing.Size(84, 20);
      this.txtMin.TabIndex = 1;
      this.txtMin.Text = "0";
      this.txtMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.toolTip1.SetToolTip(this.txtMin, "Min Value");
      this.txtMin.TextChanged += new System.EventHandler(this.OnMinChanged);
      // 
      // txtCurrent
      // 
      this.txtCurrent.Dock = System.Windows.Forms.DockStyle.Fill;
      this.txtCurrent.Location = new System.Drawing.Point(98, 8);
      this.txtCurrent.Name = "txtCurrent";
      this.txtCurrent.Size = new System.Drawing.Size(84, 20);
      this.txtCurrent.TabIndex = 2;
      this.txtCurrent.Text = "5";
      this.txtCurrent.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.toolTip1.SetToolTip(this.txtCurrent, "Current Value");
      this.txtCurrent.TextChanged += new System.EventHandler(this.OnCurrentChanged);
      // 
      // trackBar1
      // 
      this.trackBar1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.trackBar1.Location = new System.Drawing.Point(188, 8);
      this.trackBar1.Maximum = 1000;
      this.trackBar1.Name = "trackBar1";
      this.trackBar1.Size = new System.Drawing.Size(481, 24);
      this.trackBar1.TabIndex = 3;
      this.trackBar1.TickFrequency = 10;
      this.toolTip1.SetToolTip(this.trackBar1, "Track Current");
      this.trackBar1.Value = 500;
      this.trackBar1.Scroll += new System.EventHandler(this.OnScrollChanged);
      // 
      // txtMax
      // 
      this.txtMax.Dock = System.Windows.Forms.DockStyle.Fill;
      this.txtMax.Location = new System.Drawing.Point(675, 8);
      this.txtMax.Name = "txtMax";
      this.txtMax.Size = new System.Drawing.Size(84, 20);
      this.txtMax.TabIndex = 4;
      this.txtMax.Text = "10";
      this.txtMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.toolTip1.SetToolTip(this.txtMax, "Max Value");
      this.txtMax.TextChanged += new System.EventHandler(this.OnMaxChanged);
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.toolStrip1);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel1.Location = new System.Drawing.Point(765, 8);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(84, 24);
      this.panel1.TabIndex = 5;
      // 
      // toolStrip1
      // 
      this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripButton,
            this.saveToolStripButton});
      this.toolStrip1.Location = new System.Drawing.Point(0, 0);
      this.toolStrip1.Name = "toolStrip1";
      this.toolStrip1.Size = new System.Drawing.Size(84, 25);
      this.toolStrip1.TabIndex = 0;
      this.toolStrip1.Text = "toolStrip1";
      // 
      // openToolStripButton
      // 
      this.openToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.openToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("openToolStripButton.Image")));
      this.openToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.openToolStripButton.Name = "openToolStripButton";
      this.openToolStripButton.Size = new System.Drawing.Size(23, 22);
      this.openToolStripButton.Text = "&Open";
      this.openToolStripButton.Click += new System.EventHandler(this.OnLoadPalette);
      // 
      // saveToolStripButton
      // 
      this.saveToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.saveToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("saveToolStripButton.Image")));
      this.saveToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.saveToolStripButton.Name = "saveToolStripButton";
      this.saveToolStripButton.Size = new System.Drawing.Size(23, 22);
      this.saveToolStripButton.Text = "&Save";
      // 
      // CtrlPalette
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.tableLayoutPanel1);
      this.Name = "CtrlPalette";
      this.Size = new System.Drawing.Size(857, 99);
      this.tableLayoutPanel1.ResumeLayout(false);
      this.tableLayoutPanel1.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
      this.panel1.ResumeLayout(false);
      this.panel1.PerformLayout();
      this.toolStrip1.ResumeLayout(false);
      this.toolStrip1.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    private System.Windows.Forms.PictureBox pictureBox1;
    private System.Windows.Forms.TextBox txtMin;
    private System.Windows.Forms.TextBox txtCurrent;
    private System.Windows.Forms.TrackBar trackBar1;
    private System.Windows.Forms.TextBox txtMax;
    private System.Windows.Forms.ToolTip toolTip1;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.ToolStrip toolStrip1;
    private System.Windows.Forms.ToolStripButton openToolStripButton;
    private System.Windows.Forms.ToolStripButton saveToolStripButton;
  }
}
