using System.ComponentModel;
using System.Windows.Forms;

namespace LibEditSpatial.Dialogs
{
  partial class FormSettings
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
      this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
      this.panel1 = new System.Windows.Forms.Panel();
      this.cmdOK = new System.Windows.Forms.Button();
      this.cmdCancel = new System.Windows.Forms.Button();
      this.panel2 = new System.Windows.Forms.Panel();
      this.cmdBrowseDefaultDir = new System.Windows.Forms.Button();
      this.txtDefaultDir = new System.Windows.Forms.TextBox();
      this.label4 = new System.Windows.Forms.Label();
      this.chkIgnoreCompartments = new System.Windows.Forms.CheckBox();
      this.cmdBrowseDune = new System.Windows.Forms.Button();
      this.txtDuneDir = new System.Windows.Forms.TextBox();
      this.label3 = new System.Windows.Forms.Label();
      this.cmdBrowsePV = new System.Windows.Forms.Button();
      this.txtParaviewDir = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.cmdBrowseCygwin = new System.Windows.Forms.Button();
      this.txtCygwin = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.tableLayoutPanel1.SuspendLayout();
      this.panel1.SuspendLayout();
      this.panel2.SuspendLayout();
      this.SuspendLayout();
      // 
      // tableLayoutPanel1
      // 
      this.tableLayoutPanel1.ColumnCount = 1;
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
      this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 0);
      this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
      this.tableLayoutPanel1.Name = "tableLayoutPanel1";
      this.tableLayoutPanel1.RowCount = 2;
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38F));
      this.tableLayoutPanel1.Size = new System.Drawing.Size(624, 441);
      this.tableLayoutPanel1.TabIndex = 0;
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.cmdOK);
      this.panel1.Controls.Add(this.cmdCancel);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel1.Location = new System.Drawing.Point(3, 406);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(618, 32);
      this.panel1.TabIndex = 0;
      // 
      // cmdOK
      // 
      this.cmdOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.cmdOK.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.cmdOK.Location = new System.Drawing.Point(453, 5);
      this.cmdOK.Name = "cmdOK";
      this.cmdOK.Size = new System.Drawing.Size(75, 23);
      this.cmdOK.TabIndex = 1;
      this.cmdOK.Text = "&OK";
      this.cmdOK.UseVisualStyleBackColor = true;
      // 
      // cmdCancel
      // 
      this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.cmdCancel.Location = new System.Drawing.Point(534, 5);
      this.cmdCancel.Name = "cmdCancel";
      this.cmdCancel.Size = new System.Drawing.Size(75, 23);
      this.cmdCancel.TabIndex = 0;
      this.cmdCancel.Text = "&Cancel";
      this.cmdCancel.UseVisualStyleBackColor = true;
      // 
      // panel2
      // 
      this.panel2.Controls.Add(this.cmdBrowseDefaultDir);
      this.panel2.Controls.Add(this.txtDefaultDir);
      this.panel2.Controls.Add(this.label4);
      this.panel2.Controls.Add(this.chkIgnoreCompartments);
      this.panel2.Controls.Add(this.cmdBrowseDune);
      this.panel2.Controls.Add(this.txtDuneDir);
      this.panel2.Controls.Add(this.label3);
      this.panel2.Controls.Add(this.cmdBrowsePV);
      this.panel2.Controls.Add(this.txtParaviewDir);
      this.panel2.Controls.Add(this.label2);
      this.panel2.Controls.Add(this.cmdBrowseCygwin);
      this.panel2.Controls.Add(this.txtCygwin);
      this.panel2.Controls.Add(this.label1);
      this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel2.Location = new System.Drawing.Point(3, 3);
      this.panel2.Name = "panel2";
      this.panel2.Size = new System.Drawing.Size(618, 397);
      this.panel2.TabIndex = 1;
      // 
      // cmdBrowseDefaultDir
      // 
      this.cmdBrowseDefaultDir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.cmdBrowseDefaultDir.Location = new System.Drawing.Point(534, 94);
      this.cmdBrowseDefaultDir.Name = "cmdBrowseDefaultDir";
      this.cmdBrowseDefaultDir.Size = new System.Drawing.Size(75, 23);
      this.cmdBrowseDefaultDir.TabIndex = 12;
      this.cmdBrowseDefaultDir.Text = "...";
      this.cmdBrowseDefaultDir.UseVisualStyleBackColor = true;
      this.cmdBrowseDefaultDir.Click += new System.EventHandler(this.cmdBrowseDefaultDir_Click);
      // 
      // txtDefaultDir
      // 
      this.txtDefaultDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtDefaultDir.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
      this.txtDefaultDir.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystemDirectories;
      this.txtDefaultDir.Location = new System.Drawing.Point(87, 96);
      this.txtDefaultDir.Name = "txtDefaultDir";
      this.txtDefaultDir.Size = new System.Drawing.Size(441, 20);
      this.txtDefaultDir.TabIndex = 11;
      this.txtDefaultDir.TextChanged += new System.EventHandler(this.txtDefaultDir_TextChanged);
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(19, 99);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(63, 13);
      this.label4.TabIndex = 10;
      this.label4.Text = "Default Dir: ";
      // 
      // chkIgnoreCompartments
      // 
      this.chkIgnoreCompartments.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.chkIgnoreCompartments.AutoSize = true;
      this.chkIgnoreCompartments.Location = new System.Drawing.Point(87, 132);
      this.chkIgnoreCompartments.Name = "chkIgnoreCompartments";
      this.chkIgnoreCompartments.Size = new System.Drawing.Size(340, 17);
      this.chkIgnoreCompartments.TabIndex = 9;
      this.chkIgnoreCompartments.Text = "Don\'t adjust Geometry / Ratelaws for Compartments automatically. ";
      this.chkIgnoreCompartments.UseVisualStyleBackColor = true;
      this.chkIgnoreCompartments.CheckedChanged += new System.EventHandler(this.chkIgnoreCompartments_CheckedChanged);
      // 
      // cmdBrowseDune
      // 
      this.cmdBrowseDune.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.cmdBrowseDune.Location = new System.Drawing.Point(534, 68);
      this.cmdBrowseDune.Name = "cmdBrowseDune";
      this.cmdBrowseDune.Size = new System.Drawing.Size(75, 23);
      this.cmdBrowseDune.TabIndex = 8;
      this.cmdBrowseDune.Text = "...";
      this.cmdBrowseDune.UseVisualStyleBackColor = true;
      this.cmdBrowseDune.Click += new System.EventHandler(this.OnBrowseDune);
      // 
      // txtDuneDir
      // 
      this.txtDuneDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtDuneDir.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
      this.txtDuneDir.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystemDirectories;
      this.txtDuneDir.Location = new System.Drawing.Point(87, 70);
      this.txtDuneDir.Name = "txtDuneDir";
      this.txtDuneDir.Size = new System.Drawing.Size(441, 20);
      this.txtDuneDir.TabIndex = 7;
      this.txtDuneDir.TextChanged += new System.EventHandler(this.txtDuneDir_TextChanged);
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(26, 73);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(55, 13);
      this.label3.TabIndex = 6;
      this.label3.Text = "Dune Dir: ";
      // 
      // cmdBrowsePV
      // 
      this.cmdBrowsePV.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.cmdBrowsePV.Location = new System.Drawing.Point(534, 42);
      this.cmdBrowsePV.Name = "cmdBrowsePV";
      this.cmdBrowsePV.Size = new System.Drawing.Size(75, 23);
      this.cmdBrowsePV.TabIndex = 5;
      this.cmdBrowsePV.Text = "...";
      this.cmdBrowsePV.UseVisualStyleBackColor = true;
      this.cmdBrowsePV.Click += new System.EventHandler(this.OnBrowsePV);
      // 
      // txtParaviewDir
      // 
      this.txtParaviewDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtParaviewDir.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
      this.txtParaviewDir.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystemDirectories;
      this.txtParaviewDir.Location = new System.Drawing.Point(87, 44);
      this.txtParaviewDir.Name = "txtParaviewDir";
      this.txtParaviewDir.Size = new System.Drawing.Size(441, 20);
      this.txtParaviewDir.TabIndex = 4;
      this.txtParaviewDir.TextChanged += new System.EventHandler(this.txtParaviewDir_TextChanged);
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(9, 47);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(73, 13);
      this.label2.TabIndex = 3;
      this.label2.Text = "Paraview Dir: ";
      // 
      // cmdBrowseCygwin
      // 
      this.cmdBrowseCygwin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.cmdBrowseCygwin.Location = new System.Drawing.Point(534, 16);
      this.cmdBrowseCygwin.Name = "cmdBrowseCygwin";
      this.cmdBrowseCygwin.Size = new System.Drawing.Size(75, 23);
      this.cmdBrowseCygwin.TabIndex = 2;
      this.cmdBrowseCygwin.Text = "...";
      this.cmdBrowseCygwin.UseVisualStyleBackColor = true;
      this.cmdBrowseCygwin.Click += new System.EventHandler(this.OnBrowseCygwin);
      // 
      // txtCygwin
      // 
      this.txtCygwin.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtCygwin.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
      this.txtCygwin.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystemDirectories;
      this.txtCygwin.Location = new System.Drawing.Point(87, 18);
      this.txtCygwin.Name = "txtCygwin";
      this.txtCygwin.Size = new System.Drawing.Size(441, 20);
      this.txtCygwin.TabIndex = 1;
      this.txtCygwin.TextChanged += new System.EventHandler(this.txtCygwin_TextChanged);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(18, 21);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(63, 13);
      this.label1.TabIndex = 0;
      this.label1.Text = "Cygwin Dir: ";
      // 
      // FormSettings
      // 
      this.AcceptButton = this.cmdOK;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.cmdCancel;
      this.ClientSize = new System.Drawing.Size(624, 441);
      this.Controls.Add(this.tableLayoutPanel1);
      this.MinimumSize = new System.Drawing.Size(640, 480);
      this.Name = "FormSettings";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Preferences";
      this.tableLayoutPanel1.ResumeLayout(false);
      this.panel1.ResumeLayout(false);
      this.panel2.ResumeLayout(false);
      this.panel2.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private TableLayoutPanel tableLayoutPanel1;
    private Panel panel1;
    private Button cmdOK;
    private Button cmdCancel;
    private Panel panel2;
    private Button cmdBrowsePV;
    private TextBox txtParaviewDir;
    private Label label2;
    private Button cmdBrowseCygwin;
    private TextBox txtCygwin;
    private Label label1;
    private CheckBox chkIgnoreCompartments;
    private Button cmdBrowseDune;
    private TextBox txtDuneDir;
    private Label label3;
    private Button cmdBrowseDefaultDir;
    private TextBox txtDefaultDir;
    private Label label4;
  }
}