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
      this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
      this.lstSpatialSpecies = new System.Windows.Forms.ListBox();
      this.lstAllSpecies = new System.Windows.Forms.ListBox();
      this.lblIntro = new System.Windows.Forms.Label();
      this.grid = new System.Windows.Forms.DataGridView();
      this.colSpeciesId = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colDiffusionX = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colDiffusionY = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colInitialExpression = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colBoundaryX = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colBoundaryY = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colBoundaryXm = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colBoundaryYm = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.panel3 = new System.Windows.Forms.Panel();
      this.label2 = new System.Windows.Forms.Label();
      this.txtDimY = new System.Windows.Forms.TextBox();
      this.txtDimX = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.tabGeometry = new System.Windows.Forms.TabPage();
      this.tabInitial = new System.Windows.Forms.TabPage();
      this.panel1 = new System.Windows.Forms.Panel();
      this.cmdPrev = new System.Windows.Forms.Button();
      this.cmdFinish = new System.Windows.Forms.Button();
      this.cmdCancel = new System.Windows.Forms.Button();
      this.cmdNext = new System.Windows.Forms.Button();
      this.tableLayoutPanel1.SuspendLayout();
      this.tabControl1.SuspendLayout();
      this.tabSpecies.SuspendLayout();
      this.panel2.SuspendLayout();
      this.tableLayoutPanel2.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
      this.panel3.SuspendLayout();
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
      this.panel2.Controls.Add(this.tableLayoutPanel2);
      this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel2.Location = new System.Drawing.Point(3, 3);
      this.panel2.Name = "panel2";
      this.panel2.Size = new System.Drawing.Size(600, 362);
      this.panel2.TabIndex = 1;
      // 
      // tableLayoutPanel2
      // 
      this.tableLayoutPanel2.ColumnCount = 2;
      this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 87F));
      this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel2.Controls.Add(this.lstSpatialSpecies, 0, 2);
      this.tableLayoutPanel2.Controls.Add(this.lstAllSpecies, 0, 1);
      this.tableLayoutPanel2.Controls.Add(this.lblIntro, 0, 0);
      this.tableLayoutPanel2.Controls.Add(this.grid, 1, 1);
      this.tableLayoutPanel2.Controls.Add(this.panel3, 1, 3);
      this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
      this.tableLayoutPanel2.Name = "tableLayoutPanel2";
      this.tableLayoutPanel2.RowCount = 4;
      this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
      this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
      this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
      this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
      this.tableLayoutPanel2.Size = new System.Drawing.Size(600, 362);
      this.tableLayoutPanel2.TabIndex = 4;
      // 
      // lstSpatialSpecies
      // 
      this.lstSpatialSpecies.Dock = System.Windows.Forms.DockStyle.Fill;
      this.lstSpatialSpecies.FormattingEnabled = true;
      this.lstSpatialSpecies.Location = new System.Drawing.Point(3, 183);
      this.lstSpatialSpecies.Name = "lstSpatialSpecies";
      this.lstSpatialSpecies.Size = new System.Drawing.Size(81, 140);
      this.lstSpatialSpecies.TabIndex = 5;
      this.lstSpatialSpecies.SelectedIndexChanged += new System.EventHandler(this.OnSpatialSpeciesDoubleClick);
      // 
      // lstAllSpecies
      // 
      this.lstAllSpecies.Dock = System.Windows.Forms.DockStyle.Fill;
      this.lstAllSpecies.FormattingEnabled = true;
      this.lstAllSpecies.Location = new System.Drawing.Point(3, 37);
      this.lstAllSpecies.Name = "lstAllSpecies";
      this.lstAllSpecies.Size = new System.Drawing.Size(81, 140);
      this.lstAllSpecies.TabIndex = 2;
      this.lstAllSpecies.SelectedIndexChanged += new System.EventHandler(this.OnAllSpeciesDoubleClick);
      // 
      // lblIntro
      // 
      this.lblIntro.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.tableLayoutPanel2.SetColumnSpan(this.lblIntro, 2);
      this.lblIntro.Location = new System.Drawing.Point(3, 0);
      this.lblIntro.Name = "lblIntro";
      this.lblIntro.Size = new System.Drawing.Size(594, 34);
      this.lblIntro.TabIndex = 1;
      this.lblIntro.Text = "The model you loaded is not a spatial model. This wizard aims to guide you throug" +
    "h the process of creating a simple spatial model. ";
      // 
      // grid
      // 
      this.grid.AllowUserToAddRows = false;
      this.grid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
      this.grid.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
      this.grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colSpeciesId,
            this.colDiffusionX,
            this.colDiffusionY,
            this.colInitialExpression,
            this.colBoundaryX,
            this.colBoundaryY,
            this.colBoundaryXm,
            this.colBoundaryYm});
      this.grid.Dock = System.Windows.Forms.DockStyle.Fill;
      this.grid.GridColor = System.Drawing.SystemColors.ControlLight;
      this.grid.Location = new System.Drawing.Point(90, 37);
      this.grid.Name = "grid";
      this.tableLayoutPanel2.SetRowSpan(this.grid, 2);
      this.grid.Size = new System.Drawing.Size(507, 286);
      this.grid.TabIndex = 4;
      this.grid.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.OnCellEndEdit);
      this.grid.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.OnUserDeletingRow);
      // 
      // colSpeciesId
      // 
      this.colSpeciesId.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
      this.colSpeciesId.HeaderText = "Id";
      this.colSpeciesId.Name = "colSpeciesId";
      this.colSpeciesId.ReadOnly = true;
      this.colSpeciesId.Width = 39;
      // 
      // colDiffusionX
      // 
      this.colDiffusionX.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
      this.colDiffusionX.HeaderText = "Diffusion X";
      this.colDiffusionX.Name = "colDiffusionX";
      this.colDiffusionX.Width = 75;
      // 
      // colDiffusionY
      // 
      this.colDiffusionY.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
      this.colDiffusionY.HeaderText = "Diffusion Y";
      this.colDiffusionY.Name = "colDiffusionY";
      this.colDiffusionY.Width = 75;
      // 
      // colInitialExpression
      // 
      this.colInitialExpression.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
      this.colInitialExpression.HeaderText = "Initial Expression";
      this.colInitialExpression.Name = "colInitialExpression";
      this.colInitialExpression.Width = 99;
      // 
      // colBoundaryX
      // 
      this.colBoundaryX.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
      this.colBoundaryX.HeaderText = "Boundary X";
      this.colBoundaryX.Name = "colBoundaryX";
      this.colBoundaryX.Width = 78;
      // 
      // colBoundaryY
      // 
      this.colBoundaryY.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
      this.colBoundaryY.HeaderText = "Boundary Y";
      this.colBoundaryY.Name = "colBoundaryY";
      this.colBoundaryY.Width = 78;
      // 
      // colBoundaryXm
      // 
      this.colBoundaryXm.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
      this.colBoundaryXm.HeaderText = "Boundary X-";
      this.colBoundaryXm.Name = "colBoundaryXm";
      this.colBoundaryXm.Width = 81;
      // 
      // colBoundaryYm
      // 
      this.colBoundaryYm.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
      this.colBoundaryYm.HeaderText = "Boundary Y-";
      this.colBoundaryYm.Name = "colBoundaryYm";
      this.colBoundaryYm.Width = 81;
      // 
      // panel3
      // 
      this.panel3.Controls.Add(this.label2);
      this.panel3.Controls.Add(this.txtDimY);
      this.panel3.Controls.Add(this.txtDimX);
      this.panel3.Controls.Add(this.label1);
      this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel3.Location = new System.Drawing.Point(90, 329);
      this.panel3.Name = "panel3";
      this.panel3.Size = new System.Drawing.Size(507, 30);
      this.panel3.TabIndex = 6;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(164, 9);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(44, 13);
      this.label2.TabIndex = 3;
      this.label2.Text = "Height: ";
      // 
      // txtDimY
      // 
      this.txtDimY.Location = new System.Drawing.Point(214, 6);
      this.txtDimY.Name = "txtDimY";
      this.txtDimY.Size = new System.Drawing.Size(100, 20);
      this.txtDimY.TabIndex = 2;
      this.txtDimY.Text = "100";
      this.txtDimY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      // 
      // txtDimX
      // 
      this.txtDimX.Location = new System.Drawing.Point(50, 6);
      this.txtDimX.Name = "txtDimX";
      this.txtDimX.Size = new System.Drawing.Size(100, 20);
      this.txtDimX.TabIndex = 1;
      this.txtDimX.Text = "100";
      this.txtDimX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(3, 9);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(41, 13);
      this.label1.TabIndex = 0;
      this.label1.Text = "Width: ";
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
      // FormInitSpatial
      // 
      this.AcceptButton = this.cmdFinish;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.cmdCancel;
      this.ClientSize = new System.Drawing.Size(624, 441);
      this.Controls.Add(this.tableLayoutPanel1);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MinimumSize = new System.Drawing.Size(640, 480);
      this.Name = "FormInitSpatial";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Spatial Wizard";
      this.Shown += new System.EventHandler(this.OnFirstShown);
      this.tableLayoutPanel1.ResumeLayout(false);
      this.tabControl1.ResumeLayout(false);
      this.tabSpecies.ResumeLayout(false);
      this.panel2.ResumeLayout(false);
      this.tableLayoutPanel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
      this.panel3.ResumeLayout(false);
      this.panel3.PerformLayout();
      this.panel1.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    private System.Windows.Forms.TabControl tabControl1;
    private System.Windows.Forms.TabPage tabSpecies;
    private System.Windows.Forms.Panel panel2;
    private System.Windows.Forms.TabPage tabGeometry;
    private System.Windows.Forms.TabPage tabInitial;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.Button cmdCancel;
    private System.Windows.Forms.Button cmdNext;
    private System.Windows.Forms.Button cmdFinish;
    private System.Windows.Forms.Button cmdPrev;
    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
    private System.Windows.Forms.ListBox lstSpatialSpecies;
    private System.Windows.Forms.ListBox lstAllSpecies;
    private System.Windows.Forms.Label lblIntro;
    private System.Windows.Forms.DataGridView grid;
    private System.Windows.Forms.DataGridViewTextBoxColumn colSpeciesId;
    private System.Windows.Forms.DataGridViewTextBoxColumn colDiffusionX;
    private System.Windows.Forms.DataGridViewTextBoxColumn colDiffusionY;
    private System.Windows.Forms.DataGridViewTextBoxColumn colInitialExpression;
    private System.Windows.Forms.DataGridViewTextBoxColumn colBoundaryX;
    private System.Windows.Forms.DataGridViewTextBoxColumn colBoundaryY;
    private System.Windows.Forms.DataGridViewTextBoxColumn colBoundaryXm;
    private System.Windows.Forms.DataGridViewTextBoxColumn colBoundaryYm;
    private System.Windows.Forms.Panel panel3;
    private System.Windows.Forms.TextBox txtDimY;
    private System.Windows.Forms.TextBox txtDimX;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
  }
}