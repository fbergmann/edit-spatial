namespace EditSpatial
{
  partial class FormInitSpatial
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

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormInitSpatial));
      this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
      this.tabControl1 = new System.Windows.Forms.TabControl();
      this.tabSpecies = new System.Windows.Forms.TabPage();
      this.panel2 = new System.Windows.Forms.Panel();
      this.lstSpatialSpecies = new System.Windows.Forms.ListBox();
      this.lstAllSpecies = new System.Windows.Forms.ListBox();
      this.lblIntro = new System.Windows.Forms.Label();
      this.tabGeometry = new System.Windows.Forms.TabPage();
      this.tabInitial = new System.Windows.Forms.TabPage();
      this.panel1 = new System.Windows.Forms.Panel();
      this.cmdFinish = new System.Windows.Forms.Button();
      this.cmdCancel = new System.Windows.Forms.Button();
      this.cmdNext = new System.Windows.Forms.Button();
      this.cmdPrev = new System.Windows.Forms.Button();
      this.tableLayoutPanel1.SuspendLayout();
      this.tabControl1.SuspendLayout();
      this.tabSpecies.SuspendLayout();
      this.panel2.SuspendLayout();
      this.panel1.SuspendLayout();
      this.SuspendLayout();
      // 
      // tableLayoutPanel1
      // 
      this.tableLayoutPanel1.ColumnCount = 1;
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel1.Controls.Add(this.tabControl1, 0, 0);
      this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
      this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
      this.tableLayoutPanel1.Name = "tableLayoutPanel1";
      this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(2);
      this.tableLayoutPanel1.RowCount = 2;
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 37F));
      this.tableLayoutPanel1.Size = new System.Drawing.Size(624, 441);
      this.tableLayoutPanel1.TabIndex = 0;
      // 
      // tabControl1
      // 
      this.tabControl1.Controls.Add(this.tabSpecies);
      this.tabControl1.Controls.Add(this.tabGeometry);
      this.tabControl1.Controls.Add(this.tabInitial);
      this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tabControl1.Location = new System.Drawing.Point(5, 5);
      this.tabControl1.Name = "tabControl1";
      this.tabControl1.SelectedIndex = 0;
      this.tabControl1.Size = new System.Drawing.Size(614, 394);
      this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.FillToRight;
      this.tabControl1.TabIndex = 2;
      // 
      // tabSpecies
      // 
      this.tabSpecies.Controls.Add(this.panel2);
      this.tabSpecies.Location = new System.Drawing.Point(4, 22);
      this.tabSpecies.Name = "tabSpecies";
      this.tabSpecies.Padding = new System.Windows.Forms.Padding(3);
      this.tabSpecies.Size = new System.Drawing.Size(606, 368);
      this.tabSpecies.TabIndex = 0;
      this.tabSpecies.Text = "Step 1 - Species";
      this.tabSpecies.UseVisualStyleBackColor = true;
      // 
      // panel2
      // 
      this.panel2.Controls.Add(this.lstSpatialSpecies);
      this.panel2.Controls.Add(this.lstAllSpecies);
      this.panel2.Controls.Add(this.lblIntro);
      this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel2.Location = new System.Drawing.Point(3, 3);
      this.panel2.Name = "panel2";
      this.panel2.Size = new System.Drawing.Size(600, 362);
      this.panel2.TabIndex = 1;
      // 
      // lstSpatialSpecies
      // 
      this.lstSpatialSpecies.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.lstSpatialSpecies.FormattingEnabled = true;
      this.lstSpatialSpecies.Location = new System.Drawing.Point(397, 52);
      this.lstSpatialSpecies.Name = "lstSpatialSpecies";
      this.lstSpatialSpecies.Size = new System.Drawing.Size(200, 251);
      this.lstSpatialSpecies.TabIndex = 2;
      this.lstSpatialSpecies.DoubleClick += new System.EventHandler(this.OnSpatialSpeciesDoubleClick);
      // 
      // lstAllSpecies
      // 
      this.lstAllSpecies.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
      this.lstAllSpecies.FormattingEnabled = true;
      this.lstAllSpecies.Location = new System.Drawing.Point(6, 52);
      this.lstAllSpecies.Name = "lstAllSpecies";
      this.lstAllSpecies.Size = new System.Drawing.Size(200, 251);
      this.lstAllSpecies.TabIndex = 1;
      this.lstAllSpecies.DoubleClick += new System.EventHandler(this.OnAllSpeciesDoubleClick);
      // 
      // lblIntro
      // 
      this.lblIntro.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.lblIntro.Location = new System.Drawing.Point(3, 0);
      this.lblIntro.Name = "lblIntro";
      this.lblIntro.Size = new System.Drawing.Size(594, 49);
      this.lblIntro.TabIndex = 0;
      this.lblIntro.Text = "The model you loaded is not a spatial model. This wizard aims to guide you throug" +
    "h the process of creating a simple spatial model. ";
      // 
      // tabGeometry
      // 
      this.tabGeometry.Location = new System.Drawing.Point(4, 22);
      this.tabGeometry.Name = "tabGeometry";
      this.tabGeometry.Padding = new System.Windows.Forms.Padding(3);
      this.tabGeometry.Size = new System.Drawing.Size(606, 368);
      this.tabGeometry.TabIndex = 1;
      this.tabGeometry.Text = "Step 2 - Geometry";
      this.tabGeometry.UseVisualStyleBackColor = true;
      // 
      // tabInitial
      // 
      this.tabInitial.Location = new System.Drawing.Point(4, 22);
      this.tabInitial.Name = "tabInitial";
      this.tabInitial.Size = new System.Drawing.Size(606, 368);
      this.tabInitial.TabIndex = 2;
      this.tabInitial.Text = "Step 3 - Initial Conditions";
      this.tabInitial.UseVisualStyleBackColor = true;
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.cmdPrev);
      this.panel1.Controls.Add(this.cmdFinish);
      this.panel1.Controls.Add(this.cmdCancel);
      this.panel1.Controls.Add(this.cmdNext);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel1.Location = new System.Drawing.Point(5, 405);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(614, 31);
      this.panel1.TabIndex = 0;
      // 
      // cmdFinish
      // 
      this.cmdFinish.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.cmdFinish.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.cmdFinish.Location = new System.Drawing.Point(453, 5);
      this.cmdFinish.Name = "cmdFinish";
      this.cmdFinish.Size = new System.Drawing.Size(75, 23);
      this.cmdFinish.TabIndex = 2;
      this.cmdFinish.Text = "&Finish";
      this.cmdFinish.UseVisualStyleBackColor = true;
      this.cmdFinish.Click += new System.EventHandler(this.OnFinishClick);
      // 
      // cmdCancel
      // 
      this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.cmdCancel.Location = new System.Drawing.Point(534, 5);
      this.cmdCancel.Name = "cmdCancel";
      this.cmdCancel.Size = new System.Drawing.Size(75, 23);
      this.cmdCancel.TabIndex = 3;
      this.cmdCancel.Text = "&Cancel";
      this.cmdCancel.UseVisualStyleBackColor = true;
      this.cmdCancel.Click += new System.EventHandler(this.OnCancelClick);
      // 
      // cmdNext
      // 
      this.cmdNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.cmdNext.Enabled = false;
      this.cmdNext.Location = new System.Drawing.Point(345, 5);
      this.cmdNext.Name = "cmdNext";
      this.cmdNext.Size = new System.Drawing.Size(75, 23);
      this.cmdNext.TabIndex = 1;
      this.cmdNext.Text = "&Next";
      this.cmdNext.UseVisualStyleBackColor = true;
      this.cmdNext.Click += new System.EventHandler(this.OnNextClick);
      // 
      // cmdPrev
      // 
      this.cmdPrev.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.cmdPrev.Enabled = false;
      this.cmdPrev.Location = new System.Drawing.Point(264, 5);
      this.cmdPrev.Name = "cmdPrev";
      this.cmdPrev.Size = new System.Drawing.Size(75, 23);
      this.cmdPrev.TabIndex = 0;
      this.cmdPrev.Text = "&Prev";
      this.cmdPrev.UseVisualStyleBackColor = true;
      this.cmdPrev.Click += new System.EventHandler(this.OnPrevClick);
      // 
      // FormInitSpatial
      // 
      this.AcceptButton = this.cmdFinish;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.cmdCancel;
      this.ClientSize = new System.Drawing.Size(624, 441);
      this.Controls.Add(this.tableLayoutPanel1);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "FormInitSpatial";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Spatial Wizard";
      this.Shown += new System.EventHandler(this.OnFirstShown);
      this.tableLayoutPanel1.ResumeLayout(false);
      this.tabControl1.ResumeLayout(false);
      this.tabSpecies.ResumeLayout(false);
      this.panel2.ResumeLayout(false);
      this.panel1.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    private System.Windows.Forms.TabControl tabControl1;
    private System.Windows.Forms.TabPage tabSpecies;
    private System.Windows.Forms.Panel panel2;
    private System.Windows.Forms.Label lblIntro;
    private System.Windows.Forms.TabPage tabGeometry;
    private System.Windows.Forms.TabPage tabInitial;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.Button cmdCancel;
    private System.Windows.Forms.Button cmdNext;
    private System.Windows.Forms.Button cmdFinish;
    private System.Windows.Forms.ListBox lstSpatialSpecies;
    private System.Windows.Forms.ListBox lstAllSpecies;
    private System.Windows.Forms.Button cmdPrev;
  }
}