using System.ComponentModel;
using System.Windows.Forms;
using LibEditSpatial.Controls;

namespace WFDuneRunner
{
  partial class MainForm
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
      this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
      this.statusStrip1 = new System.Windows.Forms.StatusStrip();
      this.splitContainer1 = new System.Windows.Forms.SplitContainer();
      this.tabControl1 = new System.Windows.Forms.TabControl();
      this.tabTime = new System.Windows.Forms.TabPage();
      this.ctrlTime1 = new LibEditSpatial.Controls.CtrlTime();
      this.tabDomain = new System.Windows.Forms.TabPage();
      this.ctrlDomain1 = new LibEditSpatial.Controls.CtrlDomain();
      this.tabNewton = new System.Windows.Forms.TabPage();
      this.ctrlNewton1 = new LibEditSpatial.Controls.CtrlNewton();
      this.tabGlobal = new System.Windows.Forms.TabPage();
      this.ctrlGlobal1 = new LibEditSpatial.Controls.CtrlGlobal();
      this.tabControl2 = new System.Windows.Forms.TabControl();
      this.tabVariables = new System.Windows.Forms.TabPage();
      this.gridVariables = new System.Windows.Forms.DataGridView();
      this.colVarname = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colDiffusion = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colBcType = new System.Windows.Forms.DataGridViewComboBoxColumn();
      this.colBCMinX = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colBCmaxX = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colBCminY = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colBCmaxY = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colData = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colEdit = new System.Windows.Forms.DataGridViewButtonColumn();
      this.colBrowse = new System.Windows.Forms.DataGridViewButtonColumn();
      this.tabParameters = new System.Windows.Forms.TabPage();
      this.gridParameters = new System.Windows.Forms.DataGridView();
      this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.tabCompartments = new System.Windows.Forms.TabPage();
      this.cmdInitFromSBML = new System.Windows.Forms.Button();
      this.cmdAssignCompartments = new System.Windows.Forms.Button();
      this.gridCompartments = new System.Windows.Forms.DataGridView();
      this.colCompartment = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Filename = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colBrowseFile = new System.Windows.Forms.DataGridViewButtonColumn();
      this.colViewDmp = new System.Windows.Forms.DataGridViewButtonColumn();
      this.cmdClearAssignments = new System.Windows.Forms.Button();
      this.menuStrip1 = new System.Windows.Forms.MenuStrip();
      this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
      this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStrip1 = new System.Windows.Forms.ToolStrip();
      this.newToolStripButton = new System.Windows.Forms.ToolStripButton();
      this.openToolStripButton = new System.Windows.Forms.ToolStripButton();
      this.saveToolStripButton = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
      this.cmdRun = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
      this.helpToolStripButton = new System.Windows.Forms.ToolStripButton();
      this.colCompartmentMask = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.toolStripContainer1.BottomToolStripPanel.SuspendLayout();
      this.toolStripContainer1.ContentPanel.SuspendLayout();
      this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
      this.toolStripContainer1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      this.tabControl1.SuspendLayout();
      this.tabTime.SuspendLayout();
      this.tabDomain.SuspendLayout();
      this.tabNewton.SuspendLayout();
      this.tabGlobal.SuspendLayout();
      this.tabControl2.SuspendLayout();
      this.tabVariables.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.gridVariables)).BeginInit();
      this.tabParameters.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.gridParameters)).BeginInit();
      this.tabCompartments.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.gridCompartments)).BeginInit();
      this.menuStrip1.SuspendLayout();
      this.toolStrip1.SuspendLayout();
      this.SuspendLayout();
      // 
      // toolStripContainer1
      // 
      // 
      // toolStripContainer1.BottomToolStripPanel
      // 
      this.toolStripContainer1.BottomToolStripPanel.Controls.Add(this.statusStrip1);
      // 
      // toolStripContainer1.ContentPanel
      // 
      this.toolStripContainer1.ContentPanel.Controls.Add(this.splitContainer1);
      this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(984, 490);
      this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
      this.toolStripContainer1.Name = "toolStripContainer1";
      this.toolStripContainer1.Size = new System.Drawing.Size(984, 561);
      this.toolStripContainer1.TabIndex = 0;
      this.toolStripContainer1.Text = "toolStripContainer1";
      // 
      // toolStripContainer1.TopToolStripPanel
      // 
      this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.menuStrip1);
      this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip1);
      // 
      // statusStrip1
      // 
      this.statusStrip1.Dock = System.Windows.Forms.DockStyle.None;
      this.statusStrip1.Location = new System.Drawing.Point(0, 0);
      this.statusStrip1.Name = "statusStrip1";
      this.statusStrip1.Size = new System.Drawing.Size(984, 22);
      this.statusStrip1.TabIndex = 0;
      // 
      // splitContainer1
      // 
      this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer1.Location = new System.Drawing.Point(0, 0);
      this.splitContainer1.Name = "splitContainer1";
      // 
      // splitContainer1.Panel1
      // 
      this.splitContainer1.Panel1.Controls.Add(this.tabControl1);
      // 
      // splitContainer1.Panel2
      // 
      this.splitContainer1.Panel2.Controls.Add(this.tabControl2);
      this.splitContainer1.Size = new System.Drawing.Size(984, 490);
      this.splitContainer1.SplitterDistance = 227;
      this.splitContainer1.TabIndex = 0;
      // 
      // tabControl1
      // 
      this.tabControl1.Controls.Add(this.tabTime);
      this.tabControl1.Controls.Add(this.tabDomain);
      this.tabControl1.Controls.Add(this.tabNewton);
      this.tabControl1.Controls.Add(this.tabGlobal);
      this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tabControl1.Location = new System.Drawing.Point(0, 0);
      this.tabControl1.Name = "tabControl1";
      this.tabControl1.SelectedIndex = 0;
      this.tabControl1.Size = new System.Drawing.Size(227, 490);
      this.tabControl1.TabIndex = 0;
      // 
      // tabTime
      // 
      this.tabTime.Controls.Add(this.ctrlTime1);
      this.tabTime.Location = new System.Drawing.Point(4, 22);
      this.tabTime.Name = "tabTime";
      this.tabTime.Padding = new System.Windows.Forms.Padding(3);
      this.tabTime.Size = new System.Drawing.Size(219, 464);
      this.tabTime.TabIndex = 0;
      this.tabTime.Text = "Time";
      this.tabTime.UseVisualStyleBackColor = true;
      // 
      // ctrlTime1
      // 
      this.ctrlTime1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.ctrlTime1.Location = new System.Drawing.Point(3, 3);
      this.ctrlTime1.Model = null;
      this.ctrlTime1.Name = "ctrlTime1";
      this.ctrlTime1.Size = new System.Drawing.Size(213, 458);
      this.ctrlTime1.TabIndex = 0;
      // 
      // tabDomain
      // 
      this.tabDomain.Controls.Add(this.ctrlDomain1);
      this.tabDomain.Location = new System.Drawing.Point(4, 22);
      this.tabDomain.Name = "tabDomain";
      this.tabDomain.Padding = new System.Windows.Forms.Padding(3);
      this.tabDomain.Size = new System.Drawing.Size(219, 464);
      this.tabDomain.TabIndex = 1;
      this.tabDomain.Text = "Domain";
      this.tabDomain.UseVisualStyleBackColor = true;
      // 
      // ctrlDomain1
      // 
      this.ctrlDomain1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.ctrlDomain1.Location = new System.Drawing.Point(3, 3);
      this.ctrlDomain1.Model = null;
      this.ctrlDomain1.Name = "ctrlDomain1";
      this.ctrlDomain1.Size = new System.Drawing.Size(213, 458);
      this.ctrlDomain1.TabIndex = 0;
      // 
      // tabNewton
      // 
      this.tabNewton.Controls.Add(this.ctrlNewton1);
      this.tabNewton.Location = new System.Drawing.Point(4, 22);
      this.tabNewton.Name = "tabNewton";
      this.tabNewton.Size = new System.Drawing.Size(219, 464);
      this.tabNewton.TabIndex = 2;
      this.tabNewton.Text = "Newton";
      this.tabNewton.UseVisualStyleBackColor = true;
      // 
      // ctrlNewton1
      // 
      this.ctrlNewton1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.ctrlNewton1.Location = new System.Drawing.Point(0, 0);
      this.ctrlNewton1.Model = null;
      this.ctrlNewton1.Name = "ctrlNewton1";
      this.ctrlNewton1.Size = new System.Drawing.Size(219, 464);
      this.ctrlNewton1.TabIndex = 0;
      // 
      // tabGlobal
      // 
      this.tabGlobal.Controls.Add(this.ctrlGlobal1);
      this.tabGlobal.Location = new System.Drawing.Point(4, 22);
      this.tabGlobal.Name = "tabGlobal";
      this.tabGlobal.Padding = new System.Windows.Forms.Padding(3);
      this.tabGlobal.Size = new System.Drawing.Size(219, 464);
      this.tabGlobal.TabIndex = 3;
      this.tabGlobal.Text = "Global";
      this.tabGlobal.UseVisualStyleBackColor = true;
      // 
      // ctrlGlobal1
      // 
      this.ctrlGlobal1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.ctrlGlobal1.Location = new System.Drawing.Point(3, 3);
      this.ctrlGlobal1.Model = null;
      this.ctrlGlobal1.Name = "ctrlGlobal1";
      this.ctrlGlobal1.Size = new System.Drawing.Size(213, 458);
      this.ctrlGlobal1.TabIndex = 0;
      // 
      // tabControl2
      // 
      this.tabControl2.Controls.Add(this.tabVariables);
      this.tabControl2.Controls.Add(this.tabParameters);
      this.tabControl2.Controls.Add(this.tabCompartments);
      this.tabControl2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tabControl2.Location = new System.Drawing.Point(0, 0);
      this.tabControl2.Name = "tabControl2";
      this.tabControl2.SelectedIndex = 0;
      this.tabControl2.Size = new System.Drawing.Size(753, 490);
      this.tabControl2.TabIndex = 0;
      // 
      // tabVariables
      // 
      this.tabVariables.Controls.Add(this.gridVariables);
      this.tabVariables.Location = new System.Drawing.Point(4, 22);
      this.tabVariables.Name = "tabVariables";
      this.tabVariables.Padding = new System.Windows.Forms.Padding(3);
      this.tabVariables.Size = new System.Drawing.Size(745, 464);
      this.tabVariables.TabIndex = 0;
      this.tabVariables.Text = "Variables";
      this.tabVariables.UseVisualStyleBackColor = true;
      // 
      // gridVariables
      // 
      this.gridVariables.AllowUserToAddRows = false;
      this.gridVariables.AllowUserToDeleteRows = false;
      this.gridVariables.AllowUserToOrderColumns = true;
      this.gridVariables.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
      this.gridVariables.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.gridVariables.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colVarname,
            this.colDiffusion,
            this.colBcType,
            this.colBCMinX,
            this.colBCmaxX,
            this.colBCminY,
            this.colBCmaxY,
            this.colData,
            this.colEdit,
            this.colBrowse,
            this.colCompartmentMask});
      this.gridVariables.Dock = System.Windows.Forms.DockStyle.Fill;
      this.gridVariables.Location = new System.Drawing.Point(3, 3);
      this.gridVariables.Name = "gridVariables";
      this.gridVariables.Size = new System.Drawing.Size(739, 458);
      this.gridVariables.TabIndex = 0;
      this.gridVariables.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.OnVariableCellClick);
      this.gridVariables.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.OnVariableChanged);
      // 
      // colVarname
      // 
      this.colVarname.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
      this.colVarname.HeaderText = "Name";
      this.colVarname.Name = "colVarname";
      this.colVarname.ReadOnly = true;
      // 
      // colDiffusion
      // 
      this.colDiffusion.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
      this.colDiffusion.HeaderText = "Diffusion";
      this.colDiffusion.Name = "colDiffusion";
      this.colDiffusion.Width = 71;
      // 
      // colBcType
      // 
      this.colBcType.HeaderText = "BC Type";
      this.colBcType.Items.AddRange(new object[] {
            "Dirichlet",
            "Neumann",
            "Outflow",
            "None"});
      this.colBcType.Name = "colBcType";
      // 
      // colBCMinX
      // 
      this.colBCMinX.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
      this.colBCMinX.HeaderText = "BC minX";
      this.colBCMinX.Name = "colBCMinX";
      this.colBCMinX.Width = 70;
      // 
      // colBCmaxX
      // 
      this.colBCmaxX.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
      this.colBCmaxX.HeaderText = "BC maxX";
      this.colBCmaxX.Name = "colBCmaxX";
      this.colBCmaxX.Width = 73;
      // 
      // colBCminY
      // 
      this.colBCminY.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
      this.colBCminY.HeaderText = "BC minY";
      this.colBCminY.Name = "colBCminY";
      this.colBCminY.Width = 70;
      // 
      // colBCmaxY
      // 
      this.colBCmaxY.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
      this.colBCmaxY.HeaderText = "BC maxY";
      this.colBCmaxY.Name = "colBCmaxY";
      this.colBCmaxY.Width = 73;
      // 
      // colData
      // 
      this.colData.HeaderText = "File";
      this.colData.Name = "colData";
      // 
      // colEdit
      // 
      this.colEdit.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
      this.colEdit.HeaderText = "Edit";
      this.colEdit.Name = "colEdit";
      this.colEdit.Width = 29;
      // 
      // colBrowse
      // 
      this.colBrowse.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
      this.colBrowse.HeaderText = "...";
      this.colBrowse.Name = "colBrowse";
      this.colBrowse.Width = 20;
      // 
      // tabParameters
      // 
      this.tabParameters.Controls.Add(this.gridParameters);
      this.tabParameters.Location = new System.Drawing.Point(4, 22);
      this.tabParameters.Name = "tabParameters";
      this.tabParameters.Padding = new System.Windows.Forms.Padding(3);
      this.tabParameters.Size = new System.Drawing.Size(745, 464);
      this.tabParameters.TabIndex = 1;
      this.tabParameters.Text = "Parameters";
      this.tabParameters.UseVisualStyleBackColor = true;
      // 
      // gridParameters
      // 
      this.gridParameters.AllowUserToAddRows = false;
      this.gridParameters.AllowUserToDeleteRows = false;
      this.gridParameters.AllowUserToOrderColumns = true;
      this.gridParameters.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
      this.gridParameters.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.gridParameters.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colName,
            this.colValue});
      this.gridParameters.Dock = System.Windows.Forms.DockStyle.Fill;
      this.gridParameters.GridColor = System.Drawing.SystemColors.ControlLight;
      this.gridParameters.Location = new System.Drawing.Point(3, 3);
      this.gridParameters.Name = "gridParameters";
      this.gridParameters.Size = new System.Drawing.Size(739, 458);
      this.gridParameters.TabIndex = 0;
      // 
      // colName
      // 
      this.colName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
      this.colName.HeaderText = "Name";
      this.colName.Name = "colName";
      this.colName.ReadOnly = true;
      this.colName.Width = 58;
      // 
      // colValue
      // 
      this.colValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
      this.colValue.HeaderText = "Value";
      this.colValue.Name = "colValue";
      // 
      // tabCompartments
      // 
      this.tabCompartments.Controls.Add(this.cmdInitFromSBML);
      this.tabCompartments.Controls.Add(this.cmdAssignCompartments);
      this.tabCompartments.Controls.Add(this.gridCompartments);
      this.tabCompartments.Controls.Add(this.cmdClearAssignments);
      this.tabCompartments.Location = new System.Drawing.Point(4, 22);
      this.tabCompartments.Name = "tabCompartments";
      this.tabCompartments.Padding = new System.Windows.Forms.Padding(3);
      this.tabCompartments.Size = new System.Drawing.Size(745, 464);
      this.tabCompartments.TabIndex = 2;
      this.tabCompartments.Text = "Compartments";
      this.tabCompartments.UseVisualStyleBackColor = true;
      // 
      // cmdInitFromSBML
      // 
      this.cmdInitFromSBML.Location = new System.Drawing.Point(216, 6);
      this.cmdInitFromSBML.Name = "cmdInitFromSBML";
      this.cmdInitFromSBML.Size = new System.Drawing.Size(104, 23);
      this.cmdInitFromSBML.TabIndex = 4;
      this.cmdInitFromSBML.Text = "Init from SBML";
      this.cmdInitFromSBML.UseVisualStyleBackColor = true;
      this.cmdInitFromSBML.Click += new System.EventHandler(this.OnInitFromSBMLClick);
      // 
      // cmdAssignCompartments
      // 
      this.cmdAssignCompartments.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.cmdAssignCompartments.Location = new System.Drawing.Point(551, 6);
      this.cmdAssignCompartments.Name = "cmdAssignCompartments";
      this.cmdAssignCompartments.Size = new System.Drawing.Size(191, 23);
      this.cmdAssignCompartments.TabIndex = 3;
      this.cmdAssignCompartments.Text = "Apply Compartment Assignments";
      this.cmdAssignCompartments.UseVisualStyleBackColor = true;
      this.cmdAssignCompartments.Click += new System.EventHandler(this.OnApplyCompartmentAssignments);
      // 
      // gridCompartments
      // 
      this.gridCompartments.AllowUserToAddRows = false;
      this.gridCompartments.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
      this.gridCompartments.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
      this.gridCompartments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.gridCompartments.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colCompartment,
            this.Filename,
            this.colBrowseFile,
            this.colViewDmp});
      this.gridCompartments.Location = new System.Drawing.Point(6, 35);
      this.gridCompartments.Name = "gridCompartments";
      this.gridCompartments.Size = new System.Drawing.Size(736, 423);
      this.gridCompartments.TabIndex = 2;
      this.gridCompartments.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.OnCompartmentCellClick);
      this.gridCompartments.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.OnCompartmentAssignmentChanged);
      // 
      // colCompartment
      // 
      this.colCompartment.HeaderText = "Compartment Id";
      this.colCompartment.Name = "colCompartment";
      // 
      // Filename
      // 
      this.Filename.HeaderText = "Filename";
      this.Filename.Name = "Filename";
      // 
      // colBrowseFile
      // 
      this.colBrowseFile.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
      this.colBrowseFile.HeaderText = "...";
      this.colBrowseFile.Name = "colBrowseFile";
      this.colBrowseFile.Width = 20;
      // 
      // colViewDmp
      // 
      this.colViewDmp.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
      this.colViewDmp.HeaderText = "Edit";
      this.colViewDmp.Name = "colViewDmp";
      this.colViewDmp.Resizable = System.Windows.Forms.DataGridViewTriState.True;
      this.colViewDmp.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
      this.colViewDmp.Width = 48;
      // 
      // cmdClearAssignments
      // 
      this.cmdClearAssignments.Location = new System.Drawing.Point(6, 6);
      this.cmdClearAssignments.Name = "cmdClearAssignments";
      this.cmdClearAssignments.Size = new System.Drawing.Size(204, 23);
      this.cmdClearAssignments.TabIndex = 1;
      this.cmdClearAssignments.Text = "Remove Compartment Assignments";
      this.cmdClearAssignments.UseVisualStyleBackColor = true;
      this.cmdClearAssignments.Click += new System.EventHandler(this.OnClearAllCompartmentAssignments);
      // 
      // menuStrip1
      // 
      this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
      this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
      this.menuStrip1.Location = new System.Drawing.Point(0, 0);
      this.menuStrip1.Name = "menuStrip1";
      this.menuStrip1.Size = new System.Drawing.Size(984, 24);
      this.menuStrip1.TabIndex = 0;
      this.menuStrip1.Text = "menuStrip1";
      // 
      // fileToolStripMenuItem
      // 
      this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.toolStripSeparator,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
      this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
      this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
      this.fileToolStripMenuItem.Text = "&File";
      // 
      // newToolStripMenuItem
      // 
      this.newToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("newToolStripMenuItem.Image")));
      this.newToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.newToolStripMenuItem.Name = "newToolStripMenuItem";
      this.newToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
      this.newToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
      this.newToolStripMenuItem.Text = "&New";
      this.newToolStripMenuItem.Click += new System.EventHandler(this.OnNewFile);
      // 
      // openToolStripMenuItem
      // 
      this.openToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("openToolStripMenuItem.Image")));
      this.openToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.openToolStripMenuItem.Name = "openToolStripMenuItem";
      this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
      this.openToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
      this.openToolStripMenuItem.Text = "&Open";
      this.openToolStripMenuItem.Click += new System.EventHandler(this.OnOpenFile);
      // 
      // toolStripSeparator
      // 
      this.toolStripSeparator.Name = "toolStripSeparator";
      this.toolStripSeparator.Size = new System.Drawing.Size(143, 6);
      // 
      // saveToolStripMenuItem
      // 
      this.saveToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("saveToolStripMenuItem.Image")));
      this.saveToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
      this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
      this.saveToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
      this.saveToolStripMenuItem.Text = "&Save";
      this.saveToolStripMenuItem.Click += new System.EventHandler(this.OnSaveFile);
      // 
      // saveAsToolStripMenuItem
      // 
      this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
      this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
      this.saveAsToolStripMenuItem.Text = "Save &As";
      this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.OnSaveAs);
      // 
      // toolStripSeparator1
      // 
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new System.Drawing.Size(143, 6);
      // 
      // exitToolStripMenuItem
      // 
      this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
      this.exitToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
      this.exitToolStripMenuItem.Text = "E&xit";
      this.exitToolStripMenuItem.Click += new System.EventHandler(this.OnExit);
      // 
      // helpToolStripMenuItem
      // 
      this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
      this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
      this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
      this.helpToolStripMenuItem.Text = "&Help";
      // 
      // aboutToolStripMenuItem
      // 
      this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
      this.aboutToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
      this.aboutToolStripMenuItem.Text = "&About...";
      // 
      // toolStrip1
      // 
      this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
      this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripButton,
            this.openToolStripButton,
            this.saveToolStripButton,
            this.toolStripSeparator2,
            this.cmdRun,
            this.toolStripSeparator6,
            this.helpToolStripButton});
      this.toolStrip1.Location = new System.Drawing.Point(3, 24);
      this.toolStrip1.Name = "toolStrip1";
      this.toolStrip1.Size = new System.Drawing.Size(148, 25);
      this.toolStrip1.TabIndex = 1;
      // 
      // newToolStripButton
      // 
      this.newToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.newToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("newToolStripButton.Image")));
      this.newToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.newToolStripButton.Name = "newToolStripButton";
      this.newToolStripButton.Size = new System.Drawing.Size(23, 22);
      this.newToolStripButton.Text = "&New";
      this.newToolStripButton.Click += new System.EventHandler(this.OnNewFile);
      // 
      // openToolStripButton
      // 
      this.openToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.openToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("openToolStripButton.Image")));
      this.openToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.openToolStripButton.Name = "openToolStripButton";
      this.openToolStripButton.Size = new System.Drawing.Size(23, 22);
      this.openToolStripButton.Text = "&Open";
      this.openToolStripButton.Click += new System.EventHandler(this.OnOpenFile);
      // 
      // saveToolStripButton
      // 
      this.saveToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.saveToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("saveToolStripButton.Image")));
      this.saveToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.saveToolStripButton.Name = "saveToolStripButton";
      this.saveToolStripButton.Size = new System.Drawing.Size(23, 22);
      this.saveToolStripButton.Text = "&Save";
      this.saveToolStripButton.Click += new System.EventHandler(this.OnSaveFile);
      // 
      // toolStripSeparator2
      // 
      this.toolStripSeparator2.Name = "toolStripSeparator2";
      this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
      // 
      // cmdRun
      // 
      this.cmdRun.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
      this.cmdRun.Image = ((System.Drawing.Image)(resources.GetObject("cmdRun.Image")));
      this.cmdRun.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.cmdRun.Name = "cmdRun";
      this.cmdRun.Size = new System.Drawing.Size(32, 22);
      this.cmdRun.Text = "&Run";
      this.cmdRun.Click += new System.EventHandler(this.OnRunClick);
      // 
      // toolStripSeparator6
      // 
      this.toolStripSeparator6.Name = "toolStripSeparator6";
      this.toolStripSeparator6.Size = new System.Drawing.Size(6, 25);
      // 
      // helpToolStripButton
      // 
      this.helpToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.helpToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("helpToolStripButton.Image")));
      this.helpToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.helpToolStripButton.Name = "helpToolStripButton";
      this.helpToolStripButton.Size = new System.Drawing.Size(23, 22);
      this.helpToolStripButton.Text = "He&lp";
      this.helpToolStripButton.Click += new System.EventHandler(this.OnAbout);
      // 
      // colCompartmentMask
      // 
      this.colCompartmentMask.HeaderText = "Compartment";
      this.colCompartmentMask.Name = "colCompartmentMask";
      this.colCompartmentMask.Visible = false;
      // 
      // MainForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(984, 561);
      this.Controls.Add(this.toolStripContainer1);
      this.MainMenuStrip = this.menuStrip1;
      this.Name = "MainForm";
      this.Text = "Dune Runner";
      this.toolStripContainer1.BottomToolStripPanel.ResumeLayout(false);
      this.toolStripContainer1.BottomToolStripPanel.PerformLayout();
      this.toolStripContainer1.ContentPanel.ResumeLayout(false);
      this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
      this.toolStripContainer1.TopToolStripPanel.PerformLayout();
      this.toolStripContainer1.ResumeLayout(false);
      this.toolStripContainer1.PerformLayout();
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
      this.splitContainer1.ResumeLayout(false);
      this.tabControl1.ResumeLayout(false);
      this.tabTime.ResumeLayout(false);
      this.tabDomain.ResumeLayout(false);
      this.tabNewton.ResumeLayout(false);
      this.tabGlobal.ResumeLayout(false);
      this.tabControl2.ResumeLayout(false);
      this.tabVariables.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.gridVariables)).EndInit();
      this.tabParameters.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.gridParameters)).EndInit();
      this.tabCompartments.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.gridCompartments)).EndInit();
      this.menuStrip1.ResumeLayout(false);
      this.menuStrip1.PerformLayout();
      this.toolStrip1.ResumeLayout(false);
      this.toolStrip1.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private ToolStripContainer toolStripContainer1;
    private StatusStrip statusStrip1;
    private MenuStrip menuStrip1;
    private ToolStripMenuItem fileToolStripMenuItem;
    private ToolStripMenuItem newToolStripMenuItem;
    private ToolStripMenuItem openToolStripMenuItem;
    private ToolStripSeparator toolStripSeparator;
    private ToolStripMenuItem saveToolStripMenuItem;
    private ToolStripMenuItem saveAsToolStripMenuItem;
    private ToolStripSeparator toolStripSeparator1;
    private ToolStripMenuItem exitToolStripMenuItem;
    private ToolStripMenuItem helpToolStripMenuItem;
    private ToolStripMenuItem aboutToolStripMenuItem;
    private ToolStrip toolStrip1;
    private ToolStripButton newToolStripButton;
    private ToolStripButton openToolStripButton;
    private ToolStripButton saveToolStripButton;
    private ToolStripSeparator toolStripSeparator6;
    private ToolStripButton helpToolStripButton;
    private SplitContainer splitContainer1;
    private TabControl tabControl1;
    private TabPage tabTime;
    private TabPage tabDomain;
    private TabPage tabNewton;
    private TabControl tabControl2;
    private TabPage tabVariables;
    private TabPage tabParameters;
    private DataGridView gridParameters;
    private DataGridViewTextBoxColumn colName;
    private DataGridViewTextBoxColumn colValue;
    private CtrlTime ctrlTime1;
    private CtrlDomain ctrlDomain1;
    private DataGridView gridVariables;
    private CtrlNewton ctrlNewton1;
    private ToolStripSeparator toolStripSeparator2;
    private ToolStripButton cmdRun;
    private TabPage tabGlobal;
    private CtrlGlobal ctrlGlobal1;
    private DataGridViewTextBoxColumn colVarname;
    private DataGridViewTextBoxColumn colDiffusion;
    private DataGridViewComboBoxColumn colBcType;
    private DataGridViewTextBoxColumn colBCMinX;
    private DataGridViewTextBoxColumn colBCmaxX;
    private DataGridViewTextBoxColumn colBCminY;
    private DataGridViewTextBoxColumn colBCmaxY;
    private DataGridViewTextBoxColumn colData;
    private DataGridViewButtonColumn colEdit;
    private DataGridViewButtonColumn colBrowse;
    private TabPage tabCompartments;
    private DataGridView gridCompartments;
    private Button cmdClearAssignments;
    private Button cmdAssignCompartments;
    private Button cmdInitFromSBML;
    private DataGridViewTextBoxColumn colCompartment;
    private DataGridViewTextBoxColumn Filename;
    private DataGridViewButtonColumn colBrowseFile;
    private DataGridViewButtonColumn colViewDmp;
    private DataGridViewTextBoxColumn colCompartmentMask;
  }
}

