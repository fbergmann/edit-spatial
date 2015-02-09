using System.ComponentModel;
using System.Windows.Forms;

namespace WFEditDMP.Forms
{
  partial class FormAdjustDmp
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

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
      this.splitContainer1 = new System.Windows.Forms.SplitContainer();
      this.panel1 = new System.Windows.Forms.Panel();
      this.listBox1 = new System.Windows.Forms.ListBox();
      this.splitContainer2 = new System.Windows.Forms.SplitContainer();
      this.pictureBox1 = new System.Windows.Forms.PictureBox();
      this.panel2 = new System.Windows.Forms.Panel();
      this.cmdCancel = new System.Windows.Forms.Button();
      this.cmdOK = new System.Windows.Forms.Button();
      this.cmdMerge = new System.Windows.Forms.Button();
      this.cmdReplace = new System.Windows.Forms.Button();
      this.txtReplacement = new System.Windows.Forms.TextBox();
      this.cmdInvert = new System.Windows.Forms.Button();
      this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
      this.tableLayoutPanel1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      this.panel1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
      this.splitContainer2.Panel1.SuspendLayout();
      this.splitContainer2.Panel2.SuspendLayout();
      this.splitContainer2.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
      this.panel2.SuspendLayout();
      this.SuspendLayout();
      // 
      // tableLayoutPanel1
      // 
      this.tableLayoutPanel1.ColumnCount = 1;
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel1.Controls.Add(this.splitContainer1, 0, 0);
      this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
      this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
      this.tableLayoutPanel1.Name = "tableLayoutPanel1";
      this.tableLayoutPanel1.RowCount = 2;
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
      this.tableLayoutPanel1.Size = new System.Drawing.Size(814, 522);
      this.tableLayoutPanel1.TabIndex = 0;
      // 
      // splitContainer1
      // 
      this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer1.Location = new System.Drawing.Point(3, 3);
      this.splitContainer1.Name = "splitContainer1";
      // 
      // splitContainer1.Panel1
      // 
      this.splitContainer1.Panel1.Controls.Add(this.listBox1);
      // 
      // splitContainer1.Panel2
      // 
      this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
      this.splitContainer1.Size = new System.Drawing.Size(808, 480);
      this.splitContainer1.SplitterDistance = 140;
      this.splitContainer1.TabIndex = 0;
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.cmdOK);
      this.panel1.Controls.Add(this.cmdCancel);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel1.Location = new System.Drawing.Point(3, 489);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(808, 30);
      this.panel1.TabIndex = 1;
      // 
      // listBox1
      // 
      this.listBox1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.listBox1.FormattingEnabled = true;
      this.listBox1.Location = new System.Drawing.Point(0, 0);
      this.listBox1.Name = "listBox1";
      this.listBox1.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
      this.listBox1.Size = new System.Drawing.Size(140, 480);
      this.listBox1.TabIndex = 0;
      // 
      // splitContainer2
      // 
      this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer2.Location = new System.Drawing.Point(0, 0);
      this.splitContainer2.Name = "splitContainer2";
      this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // splitContainer2.Panel1
      // 
      this.splitContainer2.Panel1.Controls.Add(this.pictureBox1);
      // 
      // splitContainer2.Panel2
      // 
      this.splitContainer2.Panel2.Controls.Add(this.panel2);
      this.splitContainer2.Size = new System.Drawing.Size(664, 480);
      this.splitContainer2.SplitterDistance = 310;
      this.splitContainer2.TabIndex = 0;
      // 
      // pictureBox1
      // 
      this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pictureBox1.Location = new System.Drawing.Point(0, 0);
      this.pictureBox1.Name = "pictureBox1";
      this.pictureBox1.Size = new System.Drawing.Size(664, 310);
      this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
      this.pictureBox1.TabIndex = 0;
      this.pictureBox1.TabStop = false;
      // 
      // panel2
      // 
      this.panel2.Controls.Add(this.cmdInvert);
      this.panel2.Controls.Add(this.txtReplacement);
      this.panel2.Controls.Add(this.cmdReplace);
      this.panel2.Controls.Add(this.cmdMerge);
      this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel2.Location = new System.Drawing.Point(0, 0);
      this.panel2.Name = "panel2";
      this.panel2.Size = new System.Drawing.Size(664, 166);
      this.panel2.TabIndex = 0;
      // 
      // cmdCancel
      // 
      this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.cmdCancel.Location = new System.Drawing.Point(730, 4);
      this.cmdCancel.Name = "cmdCancel";
      this.cmdCancel.Size = new System.Drawing.Size(75, 23);
      this.cmdCancel.TabIndex = 0;
      this.cmdCancel.Text = "&Cancel";
      this.cmdCancel.UseVisualStyleBackColor = true;
      // 
      // cmdOK
      // 
      this.cmdOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.cmdOK.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.cmdOK.Location = new System.Drawing.Point(649, 4);
      this.cmdOK.Name = "cmdOK";
      this.cmdOK.Size = new System.Drawing.Size(75, 23);
      this.cmdOK.TabIndex = 1;
      this.cmdOK.Text = "&Ok";
      this.cmdOK.UseVisualStyleBackColor = true;
      // 
      // cmdMerge
      // 
      this.cmdMerge.Location = new System.Drawing.Point(3, 3);
      this.cmdMerge.Name = "cmdMerge";
      this.cmdMerge.Size = new System.Drawing.Size(177, 23);
      this.cmdMerge.TabIndex = 0;
      this.cmdMerge.Text = "Merge Selected";
      this.toolTip1.SetToolTip(this.cmdMerge, "Merge selected values (all get the first value)");
      this.cmdMerge.UseVisualStyleBackColor = true;
      this.cmdMerge.Click += new System.EventHandler(this.cmdMerge_Click);
      // 
      // cmdReplace
      // 
      this.cmdReplace.Location = new System.Drawing.Point(109, 30);
      this.cmdReplace.Name = "cmdReplace";
      this.cmdReplace.Size = new System.Drawing.Size(71, 23);
      this.cmdReplace.TabIndex = 1;
      this.cmdReplace.Text = "Replace";
      this.toolTip1.SetToolTip(this.cmdReplace, "replace selected with the given value");
      this.cmdReplace.UseVisualStyleBackColor = true;
      this.cmdReplace.Click += new System.EventHandler(this.cmdReplace_Click);
      // 
      // txtReplacement
      // 
      this.txtReplacement.Location = new System.Drawing.Point(3, 32);
      this.txtReplacement.Name = "txtReplacement";
      this.txtReplacement.Size = new System.Drawing.Size(100, 20);
      this.txtReplacement.TabIndex = 2;
      this.txtReplacement.Text = "0";
      this.txtReplacement.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      // 
      // cmdInvert
      // 
      this.cmdInvert.Location = new System.Drawing.Point(3, 59);
      this.cmdInvert.Name = "cmdInvert";
      this.cmdInvert.Size = new System.Drawing.Size(177, 23);
      this.cmdInvert.TabIndex = 3;
      this.cmdInvert.Text = "Invert";
      this.toolTip1.SetToolTip(this.cmdInvert, "flip order");
      this.cmdInvert.UseVisualStyleBackColor = true;
      this.cmdInvert.Click += new System.EventHandler(this.cmdInvert_Click);
      // 
      // FormAdjustDmp
      // 
      this.AcceptButton = this.cmdOK;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.cmdCancel;
      this.ClientSize = new System.Drawing.Size(814, 522);
      this.Controls.Add(this.tableLayoutPanel1);
      this.Name = "FormAdjustDmp";
      this.Text = "Adjust ";
      this.tableLayoutPanel1.ResumeLayout(false);
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
      this.splitContainer1.ResumeLayout(false);
      this.panel1.ResumeLayout(false);
      this.splitContainer2.Panel1.ResumeLayout(false);
      this.splitContainer2.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
      this.splitContainer2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
      this.panel2.ResumeLayout(false);
      this.panel2.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private TableLayoutPanel tableLayoutPanel1;
    private SplitContainer splitContainer1;
    private ListBox listBox1;
    private SplitContainer splitContainer2;
    private PictureBox pictureBox1;
    private Panel panel2;
    private Button cmdInvert;
    private ToolTip toolTip1;
    private TextBox txtReplacement;
    private Button cmdReplace;
    private Button cmdMerge;
    private Panel panel1;
    private Button cmdOK;
    private Button cmdCancel;
  }
}