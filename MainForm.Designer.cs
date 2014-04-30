﻿namespace EditSpatial
{
  partial class MainForm
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
      System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("List of Coordinate Components");
      System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("List of Domain Types");
      System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("List of Domains");
      System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("List Of Adjacent Domains");
      System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("List of Geometry Definitions");
      System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Compartments");
      System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("Species");
      System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("Parameters");
      System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("Reactions");
      System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("Rules");
      System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("Initial Assignments");
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
      this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
      this.statusStrip1 = new System.Windows.Forms.StatusStrip();
      this.tabControl1 = new System.Windows.Forms.TabControl();
      this.tabPage1 = new System.Windows.Forms.TabPage();
      this.splitGeometry = new System.Windows.Forms.SplitContainer();
      this.treeView1 = new System.Windows.Forms.TreeView();
      this.controlSampleFieldGeometry1 = new EditSpatial.Controls.ControlSampleFieldGeometry();
      this.controlAnalyticGeometry1 = new EditSpatial.Controls.ControlAnalyticGeometry();
      this.controlAdjacentDomains1 = new EditSpatial.Controls.ControlAdjacentDomains();
      this.controlDomains1 = new EditSpatial.Controls.ControlDomains();
      this.controlDomainTypes1 = new EditSpatial.Controls.ControlDomainTypes();
      this.controlCoordinateComponents1 = new EditSpatial.Controls.ControlCoordinateComponents();
      this.controlDisplayNode1 = new EditSpatial.Controls.ControlDisplayNode();
      this.tabPage2 = new System.Windows.Forms.TabPage();
      this.splitInitial = new System.Windows.Forms.SplitContainer();
      this.treeView2 = new System.Windows.Forms.TreeView();
      this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.controlInitialAssignments1 = new EditSpatial.Controls.ControlInitialAssignments();
      this.controlDisplayNode2 = new EditSpatial.Controls.ControlDisplayNode();
      this.tabPage3 = new System.Windows.Forms.TabPage();
      this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
      this.txtJarnac = new EditSpatial.Controls.ControlText();
      this.panel1 = new System.Windows.Forms.Panel();
      this.cmdApplyJarnac = new System.Windows.Forms.Button();
      this.tabPage4 = new System.Windows.Forms.TabPage();
      this.txtSBML = new EditSpatial.Controls.ControlText();
      this.menuStrip1 = new System.Windows.Forms.MenuStrip();
      this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
      this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolExportDune = new System.Windows.Forms.ToolStripMenuItem();
      this.toolExportMorpheus = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.printToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
      this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.redoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
      this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
      this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuSBW = new System.Windows.Forms.ToolStripMenuItem();
      this.modelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.showWarningsErrorsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.spatialWizardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStrip1 = new System.Windows.Forms.ToolStrip();
      this.newToolStripButton = new System.Windows.Forms.ToolStripButton();
      this.openToolStripButton = new System.Windows.Forms.ToolStripButton();
      this.saveToolStripButton = new System.Windows.Forms.ToolStripButton();
      this.printToolStripButton = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
      this.cutToolStripButton = new System.Windows.Forms.ToolStripButton();
      this.copyToolStripButton = new System.Windows.Forms.ToolStripButton();
      this.pasteToolStripButton = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
      this.helpToolStripButton = new System.Windows.Forms.ToolStripButton();
      this.toolStripContainer1.BottomToolStripPanel.SuspendLayout();
      this.toolStripContainer1.ContentPanel.SuspendLayout();
      this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
      this.toolStripContainer1.SuspendLayout();
      this.tabControl1.SuspendLayout();
      this.tabPage1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitGeometry)).BeginInit();
      this.splitGeometry.Panel1.SuspendLayout();
      this.splitGeometry.Panel2.SuspendLayout();
      this.splitGeometry.SuspendLayout();
      this.tabPage2.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitInitial)).BeginInit();
      this.splitInitial.Panel1.SuspendLayout();
      this.splitInitial.Panel2.SuspendLayout();
      this.splitInitial.SuspendLayout();
      this.contextMenuStrip1.SuspendLayout();
      this.tabPage3.SuspendLayout();
      this.tableLayoutPanel1.SuspendLayout();
      this.panel1.SuspendLayout();
      this.tabPage4.SuspendLayout();
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
      this.toolStripContainer1.ContentPanel.Controls.Add(this.tabControl1);
      this.toolStripContainer1.ContentPanel.Padding = new System.Windows.Forms.Padding(4);
      this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(624, 370);
      this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
      this.toolStripContainer1.Name = "toolStripContainer1";
      this.toolStripContainer1.Size = new System.Drawing.Size(624, 441);
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
      this.statusStrip1.Size = new System.Drawing.Size(624, 22);
      this.statusStrip1.TabIndex = 0;
      // 
      // tabControl1
      // 
      this.tabControl1.Controls.Add(this.tabPage1);
      this.tabControl1.Controls.Add(this.tabPage2);
      this.tabControl1.Controls.Add(this.tabPage3);
      this.tabControl1.Controls.Add(this.tabPage4);
      this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tabControl1.Location = new System.Drawing.Point(4, 4);
      this.tabControl1.Name = "tabControl1";
      this.tabControl1.SelectedIndex = 0;
      this.tabControl1.Size = new System.Drawing.Size(616, 362);
      this.tabControl1.TabIndex = 0;
      // 
      // tabPage1
      // 
      this.tabPage1.Controls.Add(this.splitGeometry);
      this.tabPage1.Location = new System.Drawing.Point(4, 22);
      this.tabPage1.Name = "tabPage1";
      this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage1.Size = new System.Drawing.Size(608, 336);
      this.tabPage1.TabIndex = 0;
      this.tabPage1.Text = "Geometry";
      this.tabPage1.UseVisualStyleBackColor = true;
      // 
      // splitGeometry
      // 
      this.splitGeometry.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitGeometry.Location = new System.Drawing.Point(3, 3);
      this.splitGeometry.Name = "splitGeometry";
      // 
      // splitGeometry.Panel1
      // 
      this.splitGeometry.Panel1.Controls.Add(this.treeView1);
      // 
      // splitGeometry.Panel2
      // 
      this.splitGeometry.Panel2.AutoScroll = true;
      this.splitGeometry.Panel2.Controls.Add(this.controlSampleFieldGeometry1);
      this.splitGeometry.Panel2.Controls.Add(this.controlAnalyticGeometry1);
      this.splitGeometry.Panel2.Controls.Add(this.controlAdjacentDomains1);
      this.splitGeometry.Panel2.Controls.Add(this.controlDomains1);
      this.splitGeometry.Panel2.Controls.Add(this.controlDomainTypes1);
      this.splitGeometry.Panel2.Controls.Add(this.controlCoordinateComponents1);
      this.splitGeometry.Panel2.Controls.Add(this.controlDisplayNode1);
      this.splitGeometry.Size = new System.Drawing.Size(602, 330);
      this.splitGeometry.SplitterDistance = 200;
      this.splitGeometry.TabIndex = 0;
      // 
      // treeView1
      // 
      this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.treeView1.Location = new System.Drawing.Point(0, 0);
      this.treeView1.Name = "treeView1";
      treeNode1.Name = "nodeCoordinateComponents";
      treeNode1.Text = "List of Coordinate Components";
      treeNode2.Name = "nodeOfDomainTypes";
      treeNode2.Text = "List of Domain Types";
      treeNode3.Name = "nodeOfDomains";
      treeNode3.Text = "List of Domains";
      treeNode4.Name = "nodeOfAdjacentDomains";
      treeNode4.Text = "List Of Adjacent Domains";
      treeNode5.Name = "nodeOfGeometryDefinitions";
      treeNode5.Text = "List of Geometry Definitions";
      this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3,
            treeNode4,
            treeNode5});
      this.treeView1.Size = new System.Drawing.Size(200, 330);
      this.treeView1.TabIndex = 0;
      this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.OnGeometrySelect);
      // 
      // controlSampleFieldGeometry1
      // 
      this.controlSampleFieldGeometry1.Current = null;
      this.controlSampleFieldGeometry1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.controlSampleFieldGeometry1.Location = new System.Drawing.Point(0, 0);
      this.controlSampleFieldGeometry1.Name = "controlSampleFieldGeometry1";
      this.controlSampleFieldGeometry1.Size = new System.Drawing.Size(398, 330);
      this.controlSampleFieldGeometry1.SpatialGeometry = null;
      this.controlSampleFieldGeometry1.TabIndex = 6;
      this.controlSampleFieldGeometry1.UpdateAction = null;
      this.controlSampleFieldGeometry1.Visible = false;
      // 
      // controlAnalyticGeometry1
      // 
      this.controlAnalyticGeometry1.Current = null;
      this.controlAnalyticGeometry1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.controlAnalyticGeometry1.Location = new System.Drawing.Point(0, 0);
      this.controlAnalyticGeometry1.Name = "controlAnalyticGeometry1";
      this.controlAnalyticGeometry1.Size = new System.Drawing.Size(398, 330);
      this.controlAnalyticGeometry1.SpatialGeometry = null;
      this.controlAnalyticGeometry1.TabIndex = 5;
      this.controlAnalyticGeometry1.UpdateAction = null;
      this.controlAnalyticGeometry1.Visible = false;
      // 
      // controlAdjacentDomains1
      // 
      this.controlAdjacentDomains1.Current = null;
      this.controlAdjacentDomains1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.controlAdjacentDomains1.Location = new System.Drawing.Point(0, 0);
      this.controlAdjacentDomains1.Name = "controlAdjacentDomains1";
      this.controlAdjacentDomains1.Size = new System.Drawing.Size(398, 330);
      this.controlAdjacentDomains1.TabIndex = 4;
      this.controlAdjacentDomains1.UpdateAction = null;
      this.controlAdjacentDomains1.Visible = false;
      // 
      // controlDomains1
      // 
      this.controlDomains1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.controlDomains1.Location = new System.Drawing.Point(0, 0);
      this.controlDomains1.Name = "controlDomains1";
      this.controlDomains1.Size = new System.Drawing.Size(398, 330);
      this.controlDomains1.TabIndex = 3;
      this.controlDomains1.UpdateAction = null;
      this.controlDomains1.Visible = false;
      // 
      // controlDomainTypes1
      // 
      this.controlDomainTypes1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.controlDomainTypes1.Location = new System.Drawing.Point(0, 0);
      this.controlDomainTypes1.Name = "controlDomainTypes1";
      this.controlDomainTypes1.Size = new System.Drawing.Size(398, 330);
      this.controlDomainTypes1.TabIndex = 2;
      this.controlDomainTypes1.UpdateAction = null;
      this.controlDomainTypes1.Visible = false;
      // 
      // controlCoordinateComponents1
      // 
      this.controlCoordinateComponents1.AutoScroll = true;
      this.controlCoordinateComponents1.Current = null;
      this.controlCoordinateComponents1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.controlCoordinateComponents1.Location = new System.Drawing.Point(0, 0);
      this.controlCoordinateComponents1.Name = "controlCoordinateComponents1";
      this.controlCoordinateComponents1.Size = new System.Drawing.Size(398, 330);
      this.controlCoordinateComponents1.TabIndex = 1;
      this.controlCoordinateComponents1.UpdateAction = null;
      this.controlCoordinateComponents1.Visible = false;
      // 
      // controlDisplayNode1
      // 
      this.controlDisplayNode1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.controlDisplayNode1.Location = new System.Drawing.Point(0, 0);
      this.controlDisplayNode1.Name = "controlDisplayNode1";
      this.controlDisplayNode1.Padding = new System.Windows.Forms.Padding(4);
      this.controlDisplayNode1.Size = new System.Drawing.Size(398, 330);
      this.controlDisplayNode1.TabIndex = 0;
      this.controlDisplayNode1.UpdateAction = null;
      // 
      // tabPage2
      // 
      this.tabPage2.Controls.Add(this.splitInitial);
      this.tabPage2.Location = new System.Drawing.Point(4, 22);
      this.tabPage2.Name = "tabPage2";
      this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage2.Size = new System.Drawing.Size(608, 336);
      this.tabPage2.TabIndex = 1;
      this.tabPage2.Text = "Core Elements";
      this.tabPage2.UseVisualStyleBackColor = true;
      // 
      // splitInitial
      // 
      this.splitInitial.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitInitial.Location = new System.Drawing.Point(3, 3);
      this.splitInitial.Name = "splitInitial";
      // 
      // splitInitial.Panel1
      // 
      this.splitInitial.Panel1.Controls.Add(this.treeView2);
      // 
      // splitInitial.Panel2
      // 
      this.splitInitial.Panel2.Controls.Add(this.controlInitialAssignments1);
      this.splitInitial.Panel2.Controls.Add(this.controlDisplayNode2);
      this.splitInitial.Size = new System.Drawing.Size(602, 330);
      this.splitInitial.SplitterDistance = 200;
      this.splitInitial.TabIndex = 1;
      // 
      // treeView2
      // 
      this.treeView2.ContextMenuStrip = this.contextMenuStrip1;
      this.treeView2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.treeView2.Location = new System.Drawing.Point(0, 0);
      this.treeView2.Name = "treeView2";
      treeNode6.Name = "nodeCompartments";
      treeNode6.Text = "Compartments";
      treeNode7.Name = "nodeSpecies";
      treeNode7.Text = "Species";
      treeNode8.Name = "nodeParameters";
      treeNode8.Text = "Parameters";
      treeNode9.Name = "nodeReactions";
      treeNode9.Text = "Reactions";
      treeNode10.Name = "nodeRules";
      treeNode10.Text = "Rules";
      treeNode11.Name = "nodeInitialAssignments";
      treeNode11.Text = "Initial Assignments";
      this.treeView2.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode6,
            treeNode7,
            treeNode8,
            treeNode9,
            treeNode10,
            treeNode11});
      this.treeView2.Size = new System.Drawing.Size(200, 330);
      this.treeView2.TabIndex = 0;
      this.treeView2.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.OnCoreSelect);
      // 
      // contextMenuStrip1
      // 
      this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteToolStripMenuItem});
      this.contextMenuStrip1.Name = "contextMenuStrip1";
      this.contextMenuStrip1.Size = new System.Drawing.Size(108, 26);
      // 
      // deleteToolStripMenuItem
      // 
      this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
      this.deleteToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
      this.deleteToolStripMenuItem.Text = "&Delete";
      this.deleteToolStripMenuItem.Click += new System.EventHandler(this.OnItemDeleteClick);
      // 
      // controlInitialAssignments1
      // 
      this.controlInitialAssignments1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.controlInitialAssignments1.Location = new System.Drawing.Point(0, 0);
      this.controlInitialAssignments1.Name = "controlInitialAssignments1";
      this.controlInitialAssignments1.Size = new System.Drawing.Size(398, 330);
      this.controlInitialAssignments1.TabIndex = 1;
      this.controlInitialAssignments1.UpdateAction = null;
      this.controlInitialAssignments1.Visible = false;
      // 
      // controlDisplayNode2
      // 
      this.controlDisplayNode2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.controlDisplayNode2.Location = new System.Drawing.Point(0, 0);
      this.controlDisplayNode2.Name = "controlDisplayNode2";
      this.controlDisplayNode2.Padding = new System.Windows.Forms.Padding(4);
      this.controlDisplayNode2.Size = new System.Drawing.Size(398, 330);
      this.controlDisplayNode2.TabIndex = 0;
      this.controlDisplayNode2.UpdateAction = null;
      // 
      // tabPage3
      // 
      this.tabPage3.Controls.Add(this.tableLayoutPanel1);
      this.tabPage3.Location = new System.Drawing.Point(4, 22);
      this.tabPage3.Name = "tabPage3";
      this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage3.Size = new System.Drawing.Size(608, 336);
      this.tabPage3.TabIndex = 2;
      this.tabPage3.Text = "Model";
      this.tabPage3.UseVisualStyleBackColor = true;
      // 
      // tableLayoutPanel1
      // 
      this.tableLayoutPanel1.ColumnCount = 1;
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel1.Controls.Add(this.txtJarnac, 0, 0);
      this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
      this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
      this.tableLayoutPanel1.Name = "tableLayoutPanel1";
      this.tableLayoutPanel1.RowCount = 2;
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
      this.tableLayoutPanel1.Size = new System.Drawing.Size(602, 330);
      this.tableLayoutPanel1.TabIndex = 1;
      // 
      // txtJarnac
      // 
      this.txtJarnac.Dock = System.Windows.Forms.DockStyle.Fill;
      this.txtJarnac.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txtJarnac.Location = new System.Drawing.Point(3, 3);
      this.txtJarnac.MaxLength = 0;
      this.txtJarnac.Multiline = true;
      this.txtJarnac.Name = "txtJarnac";
      this.txtJarnac.ScrollBars = System.Windows.Forms.ScrollBars.Both;
      this.txtJarnac.Size = new System.Drawing.Size(596, 295);
      this.txtJarnac.TabIndex = 0;
      this.txtJarnac.WordWrap = false;
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.cmdApplyJarnac);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel1.Location = new System.Drawing.Point(3, 304);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(596, 23);
      this.panel1.TabIndex = 1;
      // 
      // cmdApplyJarnac
      // 
      this.cmdApplyJarnac.Location = new System.Drawing.Point(3, 0);
      this.cmdApplyJarnac.Name = "cmdApplyJarnac";
      this.cmdApplyJarnac.Size = new System.Drawing.Size(75, 23);
      this.cmdApplyJarnac.TabIndex = 0;
      this.cmdApplyJarnac.Text = "&Apply";
      this.cmdApplyJarnac.UseVisualStyleBackColor = true;
      this.cmdApplyJarnac.Click += new System.EventHandler(this.OnApplyJarnacClick);
      // 
      // tabPage4
      // 
      this.tabPage4.Controls.Add(this.txtSBML);
      this.tabPage4.Location = new System.Drawing.Point(4, 22);
      this.tabPage4.Name = "tabPage4";
      this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage4.Size = new System.Drawing.Size(608, 336);
      this.tabPage4.TabIndex = 3;
      this.tabPage4.Text = "SBML";
      this.tabPage4.UseVisualStyleBackColor = true;
      // 
      // txtSBML
      // 
      this.txtSBML.Dock = System.Windows.Forms.DockStyle.Fill;
      this.txtSBML.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txtSBML.Location = new System.Drawing.Point(3, 3);
      this.txtSBML.MaxLength = 0;
      this.txtSBML.Multiline = true;
      this.txtSBML.Name = "txtSBML";
      this.txtSBML.ScrollBars = System.Windows.Forms.ScrollBars.Both;
      this.txtSBML.Size = new System.Drawing.Size(602, 330);
      this.txtSBML.TabIndex = 0;
      this.txtSBML.WordWrap = false;
      // 
      // menuStrip1
      // 
      this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
      this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.mnuSBW,
            this.modelToolStripMenuItem,
            this.helpToolStripMenuItem});
      this.menuStrip1.Location = new System.Drawing.Point(0, 0);
      this.menuStrip1.Name = "menuStrip1";
      this.menuStrip1.Size = new System.Drawing.Size(624, 24);
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
            this.toolExportDune,
            this.toolExportMorpheus,
            this.toolStripSeparator1,
            this.printToolStripMenuItem,
            this.toolStripSeparator2,
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
      this.newToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
      this.newToolStripMenuItem.Text = "&New";
      this.newToolStripMenuItem.Click += new System.EventHandler(this.OnNewModel);
      // 
      // openToolStripMenuItem
      // 
      this.openToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("openToolStripMenuItem.Image")));
      this.openToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.openToolStripMenuItem.Name = "openToolStripMenuItem";
      this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
      this.openToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
      this.openToolStripMenuItem.Text = "&Open";
      this.openToolStripMenuItem.Click += new System.EventHandler(this.OnOpenFile);
      // 
      // toolStripSeparator
      // 
      this.toolStripSeparator.Name = "toolStripSeparator";
      this.toolStripSeparator.Size = new System.Drawing.Size(201, 6);
      // 
      // saveToolStripMenuItem
      // 
      this.saveToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("saveToolStripMenuItem.Image")));
      this.saveToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
      this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
      this.saveToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
      this.saveToolStripMenuItem.Text = "&Save";
      this.saveToolStripMenuItem.Click += new System.EventHandler(this.OnSaveFile);
      // 
      // toolExportDune
      // 
      this.toolExportDune.Name = "toolExportDune";
      this.toolExportDune.Size = new System.Drawing.Size(204, 22);
      this.toolExportDune.Text = "&Export Dune";
      this.toolExportDune.Click += new System.EventHandler(this.OnExportDuneClick);
      // 
      // toolExportMorpheus
      // 
      this.toolExportMorpheus.Name = "toolExportMorpheus";
      this.toolExportMorpheus.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
      this.toolExportMorpheus.Size = new System.Drawing.Size(204, 22);
      this.toolExportMorpheus.Text = "Export &Morpheus";
      this.toolExportMorpheus.ToolTipText = "Export model to Morpheus";
      this.toolExportMorpheus.Click += new System.EventHandler(this.OnExportMorpheusClick);
      // 
      // toolStripSeparator1
      // 
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new System.Drawing.Size(201, 6);
      // 
      // printToolStripMenuItem
      // 
      this.printToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("printToolStripMenuItem.Image")));
      this.printToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.printToolStripMenuItem.Name = "printToolStripMenuItem";
      this.printToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
      this.printToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
      this.printToolStripMenuItem.Text = "&Print";
      this.printToolStripMenuItem.Click += new System.EventHandler(this.OnPrint);
      // 
      // toolStripSeparator2
      // 
      this.toolStripSeparator2.Name = "toolStripSeparator2";
      this.toolStripSeparator2.Size = new System.Drawing.Size(201, 6);
      // 
      // exitToolStripMenuItem
      // 
      this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
      this.exitToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
      this.exitToolStripMenuItem.Text = "E&xit";
      this.exitToolStripMenuItem.Click += new System.EventHandler(this.OnExit);
      // 
      // editToolStripMenuItem
      // 
      this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.undoToolStripMenuItem,
            this.redoToolStripMenuItem,
            this.toolStripSeparator3,
            this.cutToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem,
            this.toolStripSeparator4,
            this.selectAllToolStripMenuItem});
      this.editToolStripMenuItem.Name = "editToolStripMenuItem";
      this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
      this.editToolStripMenuItem.Text = "&Edit";
      // 
      // undoToolStripMenuItem
      // 
      this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
      this.undoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
      this.undoToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
      this.undoToolStripMenuItem.Text = "&Undo";
      // 
      // redoToolStripMenuItem
      // 
      this.redoToolStripMenuItem.Name = "redoToolStripMenuItem";
      this.redoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
      this.redoToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
      this.redoToolStripMenuItem.Text = "&Redo";
      // 
      // toolStripSeparator3
      // 
      this.toolStripSeparator3.Name = "toolStripSeparator3";
      this.toolStripSeparator3.Size = new System.Drawing.Size(141, 6);
      // 
      // cutToolStripMenuItem
      // 
      this.cutToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("cutToolStripMenuItem.Image")));
      this.cutToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
      this.cutToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
      this.cutToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
      this.cutToolStripMenuItem.Text = "Cu&t";
      // 
      // copyToolStripMenuItem
      // 
      this.copyToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("copyToolStripMenuItem.Image")));
      this.copyToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
      this.copyToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
      this.copyToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
      this.copyToolStripMenuItem.Text = "&Copy";
      // 
      // pasteToolStripMenuItem
      // 
      this.pasteToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("pasteToolStripMenuItem.Image")));
      this.pasteToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
      this.pasteToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
      this.pasteToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
      this.pasteToolStripMenuItem.Text = "&Paste";
      // 
      // toolStripSeparator4
      // 
      this.toolStripSeparator4.Name = "toolStripSeparator4";
      this.toolStripSeparator4.Size = new System.Drawing.Size(141, 6);
      // 
      // selectAllToolStripMenuItem
      // 
      this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
      this.selectAllToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
      this.selectAllToolStripMenuItem.Text = "Select &All";
      // 
      // mnuSBW
      // 
      this.mnuSBW.Name = "mnuSBW";
      this.mnuSBW.Size = new System.Drawing.Size(43, 20);
      this.mnuSBW.Text = "S&BW";
      // 
      // modelToolStripMenuItem
      // 
      this.modelToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showWarningsErrorsToolStripMenuItem,
            this.spatialWizardToolStripMenuItem});
      this.modelToolStripMenuItem.Name = "modelToolStripMenuItem";
      this.modelToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
      this.modelToolStripMenuItem.Text = "&Model";
      // 
      // showWarningsErrorsToolStripMenuItem
      // 
      this.showWarningsErrorsToolStripMenuItem.Name = "showWarningsErrorsToolStripMenuItem";
      this.showWarningsErrorsToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
      this.showWarningsErrorsToolStripMenuItem.Text = "Show Warnings / Errors";
      this.showWarningsErrorsToolStripMenuItem.Click += new System.EventHandler(this.OnShowWarnings);
      // 
      // spatialWizardToolStripMenuItem
      // 
      this.spatialWizardToolStripMenuItem.Name = "spatialWizardToolStripMenuItem";
      this.spatialWizardToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
      this.spatialWizardToolStripMenuItem.Text = "Spatial Wizard";
      this.spatialWizardToolStripMenuItem.Click += new System.EventHandler(this.OnShowSpatialWizard);
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
      this.aboutToolStripMenuItem.Click += new System.EventHandler(this.OnAbout);
      // 
      // toolStrip1
      // 
      this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
      this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripButton,
            this.openToolStripButton,
            this.saveToolStripButton,
            this.printToolStripButton,
            this.toolStripSeparator6,
            this.cutToolStripButton,
            this.copyToolStripButton,
            this.pasteToolStripButton,
            this.toolStripSeparator7,
            this.helpToolStripButton});
      this.toolStrip1.Location = new System.Drawing.Point(3, 24);
      this.toolStrip1.Name = "toolStrip1";
      this.toolStrip1.Size = new System.Drawing.Size(208, 25);
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
      this.newToolStripButton.Click += new System.EventHandler(this.OnNewModel);
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
      // printToolStripButton
      // 
      this.printToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.printToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("printToolStripButton.Image")));
      this.printToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.printToolStripButton.Name = "printToolStripButton";
      this.printToolStripButton.Size = new System.Drawing.Size(23, 22);
      this.printToolStripButton.Text = "&Print";
      this.printToolStripButton.Click += new System.EventHandler(this.OnPrint);
      // 
      // toolStripSeparator6
      // 
      this.toolStripSeparator6.Name = "toolStripSeparator6";
      this.toolStripSeparator6.Size = new System.Drawing.Size(6, 25);
      // 
      // cutToolStripButton
      // 
      this.cutToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.cutToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("cutToolStripButton.Image")));
      this.cutToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.cutToolStripButton.Name = "cutToolStripButton";
      this.cutToolStripButton.Size = new System.Drawing.Size(23, 22);
      this.cutToolStripButton.Text = "C&ut";
      this.cutToolStripButton.Click += new System.EventHandler(this.OnCut);
      // 
      // copyToolStripButton
      // 
      this.copyToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.copyToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("copyToolStripButton.Image")));
      this.copyToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.copyToolStripButton.Name = "copyToolStripButton";
      this.copyToolStripButton.Size = new System.Drawing.Size(23, 22);
      this.copyToolStripButton.Text = "&Copy";
      this.copyToolStripButton.Click += new System.EventHandler(this.OnCopy);
      // 
      // pasteToolStripButton
      // 
      this.pasteToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.pasteToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("pasteToolStripButton.Image")));
      this.pasteToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.pasteToolStripButton.Name = "pasteToolStripButton";
      this.pasteToolStripButton.Size = new System.Drawing.Size(23, 22);
      this.pasteToolStripButton.Text = "&Paste";
      this.pasteToolStripButton.Click += new System.EventHandler(this.OnPaste);
      // 
      // toolStripSeparator7
      // 
      this.toolStripSeparator7.Name = "toolStripSeparator7";
      this.toolStripSeparator7.Size = new System.Drawing.Size(6, 25);
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
      // MainForm
      // 
      this.AllowDrop = true;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(624, 441);
      this.Controls.Add(this.toolStripContainer1);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MainMenuStrip = this.menuStrip1;
      this.MinimumSize = new System.Drawing.Size(640, 480);
      this.Name = "MainForm";
      this.Text = "Edit Spatial";
      this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.OnFormClosed);
      this.Load += new System.EventHandler(this.OnLoad);
      this.DragDrop += new System.Windows.Forms.DragEventHandler(this.MainForm_DragDrop);
      this.DragEnter += new System.Windows.Forms.DragEventHandler(this.MainForm_DragEnter);
      this.toolStripContainer1.BottomToolStripPanel.ResumeLayout(false);
      this.toolStripContainer1.BottomToolStripPanel.PerformLayout();
      this.toolStripContainer1.ContentPanel.ResumeLayout(false);
      this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
      this.toolStripContainer1.TopToolStripPanel.PerformLayout();
      this.toolStripContainer1.ResumeLayout(false);
      this.toolStripContainer1.PerformLayout();
      this.tabControl1.ResumeLayout(false);
      this.tabPage1.ResumeLayout(false);
      this.splitGeometry.Panel1.ResumeLayout(false);
      this.splitGeometry.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitGeometry)).EndInit();
      this.splitGeometry.ResumeLayout(false);
      this.tabPage2.ResumeLayout(false);
      this.splitInitial.Panel1.ResumeLayout(false);
      this.splitInitial.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitInitial)).EndInit();
      this.splitInitial.ResumeLayout(false);
      this.contextMenuStrip1.ResumeLayout(false);
      this.tabPage3.ResumeLayout(false);
      this.tableLayoutPanel1.ResumeLayout(false);
      this.tableLayoutPanel1.PerformLayout();
      this.panel1.ResumeLayout(false);
      this.tabPage4.ResumeLayout(false);
      this.tabPage4.PerformLayout();
      this.menuStrip1.ResumeLayout(false);
      this.menuStrip1.PerformLayout();
      this.toolStrip1.ResumeLayout(false);
      this.toolStrip1.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.ToolStripContainer toolStripContainer1;
    private System.Windows.Forms.StatusStrip statusStrip1;
    private System.Windows.Forms.TabControl tabControl1;
    private System.Windows.Forms.TabPage tabPage1;
    private System.Windows.Forms.TabPage tabPage2;
    private System.Windows.Forms.TabPage tabPage3;
    private System.Windows.Forms.TabPage tabPage4;
    private System.Windows.Forms.MenuStrip menuStrip1;
    private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
    private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    private System.Windows.Forms.ToolStripMenuItem printToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem redoToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
    private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
    private System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
    private System.Windows.Forms.ToolStrip toolStrip1;
    private System.Windows.Forms.ToolStripButton newToolStripButton;
    private System.Windows.Forms.ToolStripButton openToolStripButton;
    private System.Windows.Forms.ToolStripButton saveToolStripButton;
    private System.Windows.Forms.ToolStripButton printToolStripButton;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
    private System.Windows.Forms.ToolStripButton cutToolStripButton;
    private System.Windows.Forms.ToolStripButton copyToolStripButton;
    private System.Windows.Forms.ToolStripButton pasteToolStripButton;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
    private System.Windows.Forms.ToolStripButton helpToolStripButton;
    private System.Windows.Forms.SplitContainer splitGeometry;
    private System.Windows.Forms.TreeView treeView1;
    private Controls.ControlDisplayNode controlDisplayNode1;
    private Controls.ControlText txtSBML;
    private System.Windows.Forms.SplitContainer splitInitial;
    private System.Windows.Forms.TreeView treeView2;
    private Controls.ControlDisplayNode controlDisplayNode2;
    private Controls.ControlText txtJarnac;
    private Controls.ControlCoordinateComponents controlCoordinateComponents1;
    private Controls.ControlDomainTypes controlDomainTypes1;
    private Controls.ControlDomains controlDomains1;
    private Controls.ControlAdjacentDomains controlAdjacentDomains1;
    private Controls.ControlAnalyticGeometry controlAnalyticGeometry1;
    private Controls.ControlSampleFieldGeometry controlSampleFieldGeometry1;
    private System.Windows.Forms.ToolStripMenuItem modelToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem showWarningsErrorsToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem mnuSBW;
    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.Button cmdApplyJarnac;
    private System.Windows.Forms.ToolStripMenuItem toolExportMorpheus;
    private Controls.ControlInitialAssignments controlInitialAssignments1;
    private System.Windows.Forms.ToolStripMenuItem spatialWizardToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem toolExportDune;
    private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
    private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
  }
}

