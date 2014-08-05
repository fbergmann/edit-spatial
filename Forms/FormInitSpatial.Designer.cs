namespace EditSpatial.Forms
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
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormInitSpatial));
      this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
      this.tabControl1 = new System.Windows.Forms.TabControl();
      this.tabSpecies = new System.Windows.Forms.TabPage();
      this.panel2 = new System.Windows.Forms.Panel();
      this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
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
      this.colType = new System.Windows.Forms.DataGridViewComboBoxColumn();
      this.panel3 = new System.Windows.Forms.Panel();
      this.cmdApplyDiff = new System.Windows.Forms.Button();
      this.txtDiffDefault = new System.Windows.Forms.TextBox();
      this.label7 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.txtDimY = new System.Windows.Forms.TextBox();
      this.txtDimX = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.lstAllSpecies = new System.Windows.Forms.ListBox();
      this.groupBox2 = new System.Windows.Forms.GroupBox();
      this.lstSpatialSpecies = new System.Windows.Forms.ListBox();
      this.tabGeometry = new System.Windows.Forms.TabPage();
      this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
      this.label3 = new System.Windows.Forms.Label();
      this.panel4 = new System.Windows.Forms.Panel();
      this.groupBox3 = new System.Windows.Forms.GroupBox();
      this.label6 = new System.Windows.Forms.Label();
      this.txtDepth = new System.Windows.Forms.TextBox();
      this.txtWidth = new System.Windows.Forms.TextBox();
      this.label4 = new System.Windows.Forms.Label();
      this.label5 = new System.Windows.Forms.Label();
      this.txtHeight = new System.Windows.Forms.TextBox();
      this.panel5 = new System.Windows.Forms.Panel();
      this.groupBox4 = new System.Windows.Forms.GroupBox();
      this.radSample = new System.Windows.Forms.RadioButton();
      this.radAnalytic = new System.Windows.Forms.RadioButton();
      this.radDefault = new System.Windows.Forms.RadioButton();
      this.panel6 = new System.Windows.Forms.Panel();
      this.controlSampleFieldGeometry1 = new EditSpatial.Controls.ControlSampleFieldGeometry();
      this.controlAnalyticGeometry1 = new EditSpatial.Controls.ControlAnalyticGeometry();
      this.panel1 = new System.Windows.Forms.Panel();
      this.cmdPrev = new System.Windows.Forms.Button();
      this.cmdFinish = new System.Windows.Forms.Button();
      this.cmdCancel = new System.Windows.Forms.Button();
      this.cmdNext = new System.Windows.Forms.Button();
      this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
      this.tableLayoutPanel1.SuspendLayout();
      this.tabControl1.SuspendLayout();
      this.tabSpecies.SuspendLayout();
      this.panel2.SuspendLayout();
      this.tableLayoutPanel2.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
      this.panel3.SuspendLayout();
      this.groupBox1.SuspendLayout();
      this.groupBox2.SuspendLayout();
      this.tabGeometry.SuspendLayout();
      this.tableLayoutPanel3.SuspendLayout();
      this.panel4.SuspendLayout();
      this.groupBox3.SuspendLayout();
      this.panel5.SuspendLayout();
      this.groupBox4.SuspendLayout();
      this.panel6.SuspendLayout();
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
      this.tableLayoutPanel1.Size = new System.Drawing.Size(905, 562);
      this.tableLayoutPanel1.TabIndex = 0;
      // 
      // tabControl1
      // 
      this.tabControl1.Controls.Add(this.tabSpecies);
      this.tabControl1.Controls.Add(this.tabGeometry);
      this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tabControl1.Location = new System.Drawing.Point(5, 5);
      this.tabControl1.Name = "tabControl1";
      this.tabControl1.SelectedIndex = 0;
      this.tabControl1.Size = new System.Drawing.Size(895, 515);
      this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.FillToRight;
      this.tabControl1.TabIndex = 2;
      this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.OnSelectedTabChanged);
      // 
      // tabSpecies
      // 
      this.tabSpecies.Controls.Add(this.panel2);
      this.tabSpecies.Location = new System.Drawing.Point(4, 22);
      this.tabSpecies.Name = "tabSpecies";
      this.tabSpecies.Padding = new System.Windows.Forms.Padding(3);
      this.tabSpecies.Size = new System.Drawing.Size(887, 489);
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
      this.panel2.Size = new System.Drawing.Size(881, 483);
      this.panel2.TabIndex = 1;
      // 
      // tableLayoutPanel2
      // 
      this.tableLayoutPanel2.ColumnCount = 2;
      this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
      this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel2.Controls.Add(this.lblIntro, 0, 0);
      this.tableLayoutPanel2.Controls.Add(this.grid, 1, 1);
      this.tableLayoutPanel2.Controls.Add(this.panel3, 1, 3);
      this.tableLayoutPanel2.Controls.Add(this.groupBox1, 0, 1);
      this.tableLayoutPanel2.Controls.Add(this.groupBox2, 0, 2);
      this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
      this.tableLayoutPanel2.Name = "tableLayoutPanel2";
      this.tableLayoutPanel2.RowCount = 4;
      this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
      this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
      this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
      this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
      this.tableLayoutPanel2.Size = new System.Drawing.Size(881, 483);
      this.tableLayoutPanel2.TabIndex = 4;
      // 
      // lblIntro
      // 
      this.lblIntro.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.tableLayoutPanel2.SetColumnSpan(this.lblIntro, 2);
      this.lblIntro.Location = new System.Drawing.Point(3, 0);
      this.lblIntro.Name = "lblIntro";
      this.lblIntro.Size = new System.Drawing.Size(875, 34);
      this.lblIntro.TabIndex = 1;
      this.lblIntro.Text = "The model you loaded is not a spatial model. This wizard aims to guide you throug" +
    "h the process of creating a simple spatial model.  Double click on a spatial spe" +
    "cies, to remove it from the list. ";
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
            this.colBoundaryYm,
            this.colType});
      this.grid.Dock = System.Windows.Forms.DockStyle.Fill;
      this.grid.GridColor = System.Drawing.SystemColors.ControlLight;
      this.grid.Location = new System.Drawing.Point(133, 37);
      this.grid.Name = "grid";
      this.tableLayoutPanel2.SetRowSpan(this.grid, 2);
      this.grid.Size = new System.Drawing.Size(745, 408);
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
      // colType
      // 
      this.colType.HeaderText = "BC Type";
      this.colType.Items.AddRange(new object[] {
            "Neumann",
            "Dirichlet"});
      this.colType.Name = "colType";
      // 
      // panel3
      // 
      this.panel3.Controls.Add(this.cmdApplyDiff);
      this.panel3.Controls.Add(this.txtDiffDefault);
      this.panel3.Controls.Add(this.label7);
      this.panel3.Controls.Add(this.label2);
      this.panel3.Controls.Add(this.txtDimY);
      this.panel3.Controls.Add(this.txtDimX);
      this.panel3.Controls.Add(this.label1);
      this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel3.Location = new System.Drawing.Point(133, 451);
      this.panel3.Name = "panel3";
      this.panel3.Size = new System.Drawing.Size(745, 29);
      this.panel3.TabIndex = 6;
      // 
      // cmdApplyDiff
      // 
      this.cmdApplyDiff.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.cmdApplyDiff.Location = new System.Drawing.Point(667, 4);
      this.cmdApplyDiff.Name = "cmdApplyDiff";
      this.cmdApplyDiff.Size = new System.Drawing.Size(75, 23);
      this.cmdApplyDiff.TabIndex = 6;
      this.cmdApplyDiff.Text = "Apply Diff";
      this.toolTip1.SetToolTip(this.cmdApplyDiff, "Sets the given default diffusion coefficient to all species. ");
      this.cmdApplyDiff.UseVisualStyleBackColor = true;
      this.cmdApplyDiff.Click += new System.EventHandler(this.OnApplyDiffClick);
      // 
      // txtDiffDefault
      // 
      this.txtDiffDefault.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.txtDiffDefault.Location = new System.Drawing.Point(561, 6);
      this.txtDiffDefault.Name = "txtDiffDefault";
      this.txtDiffDefault.Size = new System.Drawing.Size(100, 20);
      this.txtDiffDefault.TabIndex = 5;
      this.txtDiffDefault.Text = "0.001";
      this.txtDiffDefault.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.toolTip1.SetToolTip(this.txtDiffDefault, "Default Diffusion Coefficient, hit apply to set for all selected species.");
      // 
      // label7
      // 
      this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.label7.AutoSize = true;
      this.label7.Location = new System.Drawing.Point(467, 9);
      this.label7.Name = "label7";
      this.label7.Size = new System.Drawing.Size(88, 13);
      this.label7.TabIndex = 4;
      this.label7.Text = "Default Diffusion:";
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
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.lstAllSpecies);
      this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.groupBox1.Location = new System.Drawing.Point(3, 37);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(124, 201);
      this.groupBox1.TabIndex = 7;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = " All Species: ";
      // 
      // lstAllSpecies
      // 
      this.lstAllSpecies.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.lstAllSpecies.Dock = System.Windows.Forms.DockStyle.Fill;
      this.lstAllSpecies.FormattingEnabled = true;
      this.lstAllSpecies.Location = new System.Drawing.Point(3, 16);
      this.lstAllSpecies.Name = "lstAllSpecies";
      this.lstAllSpecies.Size = new System.Drawing.Size(118, 182);
      this.lstAllSpecies.TabIndex = 2;
      this.toolTip1.SetToolTip(this.lstAllSpecies, "Double click species, to add it to the list of spatial species. ");
      this.lstAllSpecies.SelectedIndexChanged += new System.EventHandler(this.OnAllSpeciesDoubleClick);
      // 
      // groupBox2
      // 
      this.groupBox2.Controls.Add(this.lstSpatialSpecies);
      this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.groupBox2.Location = new System.Drawing.Point(3, 244);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Size = new System.Drawing.Size(124, 201);
      this.groupBox2.TabIndex = 8;
      this.groupBox2.TabStop = false;
      this.groupBox2.Text = " Spatial Species: ";
      // 
      // lstSpatialSpecies
      // 
      this.lstSpatialSpecies.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.lstSpatialSpecies.Dock = System.Windows.Forms.DockStyle.Fill;
      this.lstSpatialSpecies.FormattingEnabled = true;
      this.lstSpatialSpecies.Location = new System.Drawing.Point(3, 16);
      this.lstSpatialSpecies.Name = "lstSpatialSpecies";
      this.lstSpatialSpecies.Size = new System.Drawing.Size(118, 182);
      this.lstSpatialSpecies.TabIndex = 5;
      this.toolTip1.SetToolTip(this.lstSpatialSpecies, "Double click to remove species from the list of spatial species. ");
      this.lstSpatialSpecies.SelectedIndexChanged += new System.EventHandler(this.OnSpatialSpeciesDoubleClick);
      // 
      // tabGeometry
      // 
      this.tabGeometry.Controls.Add(this.tableLayoutPanel3);
      this.tabGeometry.Location = new System.Drawing.Point(4, 22);
      this.tabGeometry.Name = "tabGeometry";
      this.tabGeometry.Padding = new System.Windows.Forms.Padding(3);
      this.tabGeometry.Size = new System.Drawing.Size(887, 489);
      this.tabGeometry.TabIndex = 1;
      this.tabGeometry.Text = "Step 2 - Geometry";
      this.tabGeometry.UseVisualStyleBackColor = true;
      // 
      // tableLayoutPanel3
      // 
      this.tableLayoutPanel3.ColumnCount = 2;
      this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
      this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
      this.tableLayoutPanel3.Controls.Add(this.label3, 0, 0);
      this.tableLayoutPanel3.Controls.Add(this.panel4, 0, 1);
      this.tableLayoutPanel3.Controls.Add(this.panel5, 1, 0);
      this.tableLayoutPanel3.Controls.Add(this.panel6, 0, 2);
      this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 3);
      this.tableLayoutPanel3.Name = "tableLayoutPanel3";
      this.tableLayoutPanel3.RowCount = 3;
      this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 43F));
      this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 55F));
      this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
      this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
      this.tableLayoutPanel3.Size = new System.Drawing.Size(881, 483);
      this.tableLayoutPanel3.TabIndex = 0;
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
      this.label3.Location = new System.Drawing.Point(3, 0);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(434, 43);
      this.label3.TabIndex = 0;
      this.label3.Text = "Please define the Geometry here,";
      this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // panel4
      // 
      this.tableLayoutPanel3.SetColumnSpan(this.panel4, 2);
      this.panel4.Controls.Add(this.groupBox3);
      this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel4.Location = new System.Drawing.Point(3, 46);
      this.panel4.Name = "panel4";
      this.panel4.Size = new System.Drawing.Size(875, 49);
      this.panel4.TabIndex = 1;
      // 
      // groupBox3
      // 
      this.groupBox3.Controls.Add(this.label6);
      this.groupBox3.Controls.Add(this.txtDepth);
      this.groupBox3.Controls.Add(this.txtWidth);
      this.groupBox3.Controls.Add(this.label4);
      this.groupBox3.Controls.Add(this.label5);
      this.groupBox3.Controls.Add(this.txtHeight);
      this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
      this.groupBox3.Location = new System.Drawing.Point(0, 0);
      this.groupBox3.Name = "groupBox3";
      this.groupBox3.Size = new System.Drawing.Size(875, 49);
      this.groupBox3.TabIndex = 8;
      this.groupBox3.TabStop = false;
      this.groupBox3.Text = " Dimensions: ";
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Location = new System.Drawing.Point(316, 22);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(42, 13);
      this.label6.TabIndex = 9;
      this.label6.Text = "Depth: ";
      // 
      // txtDepth
      // 
      this.txtDepth.Location = new System.Drawing.Point(364, 19);
      this.txtDepth.Name = "txtDepth";
      this.txtDepth.Size = new System.Drawing.Size(100, 20);
      this.txtDepth.TabIndex = 8;
      this.txtDepth.Text = "0";
      this.txtDepth.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      // 
      // txtWidth
      // 
      this.txtWidth.Location = new System.Drawing.Point(54, 19);
      this.txtWidth.Name = "txtWidth";
      this.txtWidth.Size = new System.Drawing.Size(100, 20);
      this.txtWidth.TabIndex = 5;
      this.txtWidth.Text = "100";
      this.txtWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(160, 22);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(44, 13);
      this.label4.TabIndex = 7;
      this.label4.Text = "Height: ";
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(7, 22);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(41, 13);
      this.label5.TabIndex = 4;
      this.label5.Text = "Width: ";
      // 
      // txtHeight
      // 
      this.txtHeight.Location = new System.Drawing.Point(210, 19);
      this.txtHeight.Name = "txtHeight";
      this.txtHeight.Size = new System.Drawing.Size(100, 20);
      this.txtHeight.TabIndex = 6;
      this.txtHeight.Text = "100";
      this.txtHeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      // 
      // panel5
      // 
      this.panel5.Controls.Add(this.groupBox4);
      this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel5.Location = new System.Drawing.Point(443, 3);
      this.panel5.Name = "panel5";
      this.panel5.Size = new System.Drawing.Size(435, 37);
      this.panel5.TabIndex = 2;
      // 
      // groupBox4
      // 
      this.groupBox4.Controls.Add(this.radSample);
      this.groupBox4.Controls.Add(this.radAnalytic);
      this.groupBox4.Controls.Add(this.radDefault);
      this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
      this.groupBox4.Location = new System.Drawing.Point(0, 0);
      this.groupBox4.Name = "groupBox4";
      this.groupBox4.Size = new System.Drawing.Size(435, 37);
      this.groupBox4.TabIndex = 0;
      this.groupBox4.TabStop = false;
      this.groupBox4.Text = " Mode: ";
      // 
      // radSample
      // 
      this.radSample.AutoSize = true;
      this.radSample.Location = new System.Drawing.Point(143, 14);
      this.radSample.Name = "radSample";
      this.radSample.Size = new System.Drawing.Size(60, 17);
      this.radSample.TabIndex = 2;
      this.radSample.TabStop = true;
      this.radSample.Text = "Sample";
      this.radSample.UseVisualStyleBackColor = true;
      this.radSample.CheckedChanged += new System.EventHandler(this.radSample_CheckedChanged);
      // 
      // radAnalytic
      // 
      this.radAnalytic.AutoSize = true;
      this.radAnalytic.Location = new System.Drawing.Point(71, 14);
      this.radAnalytic.Name = "radAnalytic";
      this.radAnalytic.Size = new System.Drawing.Size(62, 17);
      this.radAnalytic.TabIndex = 1;
      this.radAnalytic.TabStop = true;
      this.radAnalytic.Text = "Analytic";
      this.radAnalytic.UseVisualStyleBackColor = true;
      this.radAnalytic.CheckedChanged += new System.EventHandler(this.radAnalytic_CheckedChanged);
      // 
      // radDefault
      // 
      this.radDefault.AutoSize = true;
      this.radDefault.Checked = true;
      this.radDefault.Location = new System.Drawing.Point(6, 14);
      this.radDefault.Name = "radDefault";
      this.radDefault.Size = new System.Drawing.Size(59, 17);
      this.radDefault.TabIndex = 0;
      this.radDefault.TabStop = true;
      this.radDefault.Text = "Default";
      this.toolTip1.SetToolTip(this.radDefault, "When checked, a one compartment model, will take the whole space. ");
      this.radDefault.UseVisualStyleBackColor = true;
      this.radDefault.CheckedChanged += new System.EventHandler(this.radDefault_CheckedChanged);
      // 
      // panel6
      // 
      this.tableLayoutPanel3.SetColumnSpan(this.panel6, 2);
      this.panel6.Controls.Add(this.controlSampleFieldGeometry1);
      this.panel6.Controls.Add(this.controlAnalyticGeometry1);
      this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel6.Location = new System.Drawing.Point(3, 101);
      this.panel6.Name = "panel6";
      this.panel6.Size = new System.Drawing.Size(875, 379);
      this.panel6.TabIndex = 3;
      // 
      // controlSampleFieldGeometry1
      // 
      this.controlSampleFieldGeometry1.Current = null;
      this.controlSampleFieldGeometry1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.controlSampleFieldGeometry1.IsInitializing = false;
      this.controlSampleFieldGeometry1.Location = new System.Drawing.Point(0, 0);
      this.controlSampleFieldGeometry1.Name = "controlSampleFieldGeometry1";
      this.controlSampleFieldGeometry1.Size = new System.Drawing.Size(875, 379);
      this.controlSampleFieldGeometry1.SpatialGeometry = null;
      this.controlSampleFieldGeometry1.TabIndex = 1;
      this.controlSampleFieldGeometry1.UpdateAction = null;
      this.controlSampleFieldGeometry1.Visible = false;
      // 
      // controlAnalyticGeometry1
      // 
      this.controlAnalyticGeometry1.Current = null;
      this.controlAnalyticGeometry1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.controlAnalyticGeometry1.IsInitializing = false;
      this.controlAnalyticGeometry1.Location = new System.Drawing.Point(0, 0);
      this.controlAnalyticGeometry1.Name = "controlAnalyticGeometry1";
      this.controlAnalyticGeometry1.RowsAdded = ((System.Collections.Generic.List<int>)(resources.GetObject("controlAnalyticGeometry1.RowsAdded")));
      this.controlAnalyticGeometry1.Size = new System.Drawing.Size(875, 379);
      this.controlAnalyticGeometry1.SpatialGeometry = null;
      this.controlAnalyticGeometry1.TabIndex = 0;
      this.controlAnalyticGeometry1.UpdateAction = null;
      this.controlAnalyticGeometry1.Visible = false;
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.cmdPrev);
      this.panel1.Controls.Add(this.cmdFinish);
      this.panel1.Controls.Add(this.cmdCancel);
      this.panel1.Controls.Add(this.cmdNext);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel1.Location = new System.Drawing.Point(5, 526);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(895, 31);
      this.panel1.TabIndex = 0;
      // 
      // cmdPrev
      // 
      this.cmdPrev.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.cmdPrev.Enabled = false;
      this.cmdPrev.Location = new System.Drawing.Point(545, 5);
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
      this.cmdFinish.Location = new System.Drawing.Point(734, 5);
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
      this.cmdCancel.Location = new System.Drawing.Point(815, 5);
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
      this.cmdNext.Location = new System.Drawing.Point(626, 5);
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
      this.ClientSize = new System.Drawing.Size(905, 562);
      this.Controls.Add(this.tableLayoutPanel1);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MinimumSize = new System.Drawing.Size(921, 601);
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
      this.groupBox1.ResumeLayout(false);
      this.groupBox2.ResumeLayout(false);
      this.tabGeometry.ResumeLayout(false);
      this.tableLayoutPanel3.ResumeLayout(false);
      this.tableLayoutPanel3.PerformLayout();
      this.panel4.ResumeLayout(false);
      this.groupBox3.ResumeLayout(false);
      this.groupBox3.PerformLayout();
      this.panel5.ResumeLayout(false);
      this.groupBox4.ResumeLayout(false);
      this.groupBox4.PerformLayout();
      this.panel6.ResumeLayout(false);
      this.panel1.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    private System.Windows.Forms.TabControl tabControl1;
    private System.Windows.Forms.TabPage tabSpecies;
    private System.Windows.Forms.Panel panel2;
    private System.Windows.Forms.TabPage tabGeometry;
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
    private System.Windows.Forms.Panel panel3;
    private System.Windows.Forms.TextBox txtDimY;
    private System.Windows.Forms.TextBox txtDimX;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.GroupBox groupBox2;
    private System.Windows.Forms.ToolTip toolTip1;
    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Panel panel4;
    private System.Windows.Forms.GroupBox groupBox3;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.TextBox txtDepth;
    private System.Windows.Forms.TextBox txtWidth;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.TextBox txtHeight;
    private System.Windows.Forms.Panel panel5;
    private System.Windows.Forms.GroupBox groupBox4;
    private System.Windows.Forms.RadioButton radSample;
    private System.Windows.Forms.RadioButton radAnalytic;
    private System.Windows.Forms.RadioButton radDefault;
    private System.Windows.Forms.Panel panel6;
    private Controls.ControlSampleFieldGeometry controlSampleFieldGeometry1;
    private Controls.ControlAnalyticGeometry controlAnalyticGeometry1;
    private System.Windows.Forms.Button cmdApplyDiff;
    private System.Windows.Forms.TextBox txtDiffDefault;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.DataGridViewTextBoxColumn colSpeciesId;
    private System.Windows.Forms.DataGridViewTextBoxColumn colDiffusionX;
    private System.Windows.Forms.DataGridViewTextBoxColumn colDiffusionY;
    private System.Windows.Forms.DataGridViewTextBoxColumn colInitialExpression;
    private System.Windows.Forms.DataGridViewTextBoxColumn colBoundaryX;
    private System.Windows.Forms.DataGridViewTextBoxColumn colBoundaryY;
    private System.Windows.Forms.DataGridViewTextBoxColumn colBoundaryXm;
    private System.Windows.Forms.DataGridViewTextBoxColumn colBoundaryYm;
    private System.Windows.Forms.DataGridViewComboBoxColumn colType;
  }
}