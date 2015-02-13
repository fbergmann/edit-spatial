using System.ComponentModel;
using System.Windows.Forms;
using LibEditSpatial.Controls;

namespace WFEditDMP
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
      LibEditSpatial.Controls.PalleteArgs palleteArgs1 = new LibEditSpatial.Controls.PalleteArgs();
      LibEditSpatial.Model.DmpPalette dmpPalette1 = new LibEditSpatial.Model.DmpPalette();
      this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
      this.sizeToolStrip = new System.Windows.Forms.ToolStrip();
      this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
      this.txtCols = new System.Windows.Forms.ToolStripTextBox();
      this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
      this.txtRows = new System.Windows.Forms.ToolStripTextBox();
      this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
      this.txtMinX = new System.Windows.Forms.ToolStripTextBox();
      this.txtMaxX = new System.Windows.Forms.ToolStripTextBox();
      this.txtMinY = new System.Windows.Forms.ToolStripTextBox();
      this.txtMaxY = new System.Windows.Forms.ToolStripTextBox();
      this.statusStrip1 = new System.Windows.Forms.StatusStrip();
      this.lblMessage = new System.Windows.Forms.ToolStripStatusLabel();
      this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
      this.lblSize = new System.Windows.Forms.ToolStripStatusLabel();
      this.lblPosition = new System.Windows.Forms.ToolStripStatusLabel();
      this.lblData = new System.Windows.Forms.ToolStripStatusLabel();
      this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
      this.ctrlPalette1 = new LibEditSpatial.Controls.CtrlPalette();
      this.dmpRenderControl1 = new LibEditSpatial.Controls.DmpRenderControl();
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
      this.mainToolStrip = new System.Windows.Forms.ToolStrip();
      this.newToolStripButton = new System.Windows.Forms.ToolStripButton();
      this.openToolStripButton = new System.Windows.Forms.ToolStripButton();
      this.saveToolStripButton = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
      this.cmbPalettes = new System.Windows.Forms.ToolStripComboBox();
      this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
      this.txtSize = new System.Windows.Forms.ToolStripTextBox();
      this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
      this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
      this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
      this.cmdCenter = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
      this.toolAdjust = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
      this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
      this.helpToolStripButton = new System.Windows.Forms.ToolStripButton();
      this.toolStripContainer1.BottomToolStripPanel.SuspendLayout();
      this.toolStripContainer1.ContentPanel.SuspendLayout();
      this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
      this.toolStripContainer1.SuspendLayout();
      this.sizeToolStrip.SuspendLayout();
      this.statusStrip1.SuspendLayout();
      this.tableLayoutPanel1.SuspendLayout();
      this.menuStrip1.SuspendLayout();
      this.mainToolStrip.SuspendLayout();
      this.SuspendLayout();
      // 
      // toolStripContainer1
      // 
      // 
      // toolStripContainer1.BottomToolStripPanel
      // 
      this.toolStripContainer1.BottomToolStripPanel.Controls.Add(this.sizeToolStrip);
      this.toolStripContainer1.BottomToolStripPanel.Controls.Add(this.statusStrip1);
      // 
      // toolStripContainer1.ContentPanel
      // 
      this.toolStripContainer1.ContentPanel.Controls.Add(this.tableLayoutPanel1);
      this.toolStripContainer1.ContentPanel.Padding = new System.Windows.Forms.Padding(5);
      this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(984, 465);
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
      this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.mainToolStrip);
      // 
      // sizeToolStrip
      // 
      this.sizeToolStrip.Dock = System.Windows.Forms.DockStyle.None;
      this.sizeToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
      this.sizeToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.txtCols,
            this.toolStripLabel2,
            this.txtRows,
            this.toolStripButton1,
            this.toolStripSeparator4,
            this.txtMinX,
            this.txtMaxX,
            this.txtMinY,
            this.txtMaxY});
      this.sizeToolStrip.Location = new System.Drawing.Point(0, 0);
      this.sizeToolStrip.Name = "sizeToolStrip";
      this.sizeToolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
      this.sizeToolStrip.Size = new System.Drawing.Size(984, 25);
      this.sizeToolStrip.Stretch = true;
      this.sizeToolStrip.TabIndex = 2;
      // 
      // toolStripLabel1
      // 
      this.toolStripLabel1.Name = "toolStripLabel1";
      this.toolStripLabel1.Size = new System.Drawing.Size(37, 22);
      this.toolStripLabel1.Text = "Dims:";
      // 
      // txtCols
      // 
      this.txtCols.Name = "txtCols";
      this.txtCols.Size = new System.Drawing.Size(50, 25);
      this.txtCols.ToolTipText = "Columns";
      // 
      // toolStripLabel2
      // 
      this.toolStripLabel2.Name = "toolStripLabel2";
      this.toolStripLabel2.Size = new System.Drawing.Size(12, 22);
      this.toolStripLabel2.Text = "x";
      // 
      // txtRows
      // 
      this.txtRows.Name = "txtRows";
      this.txtRows.Size = new System.Drawing.Size(50, 25);
      this.txtRows.ToolTipText = "Rows";
      // 
      // toolStripButton1
      // 
      this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
      this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
      this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripButton1.Name = "toolStripButton1";
      this.toolStripButton1.Size = new System.Drawing.Size(43, 22);
      this.toolStripButton1.Text = "&Resize";
      this.toolStripButton1.Click += new System.EventHandler(this.OnResizeClicked);
      // 
      // toolStripSeparator4
      // 
      this.toolStripSeparator4.Name = "toolStripSeparator4";
      this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
      // 
      // txtMinX
      // 
      this.txtMinX.Name = "txtMinX";
      this.txtMinX.Size = new System.Drawing.Size(50, 25);
      this.txtMinX.ToolTipText = "Min X";
      this.txtMinX.TextChanged += new System.EventHandler(this.OnMinXChanged);
      // 
      // txtMaxX
      // 
      this.txtMaxX.Name = "txtMaxX";
      this.txtMaxX.Size = new System.Drawing.Size(50, 25);
      this.txtMaxX.ToolTipText = "Max X";
      this.txtMaxX.TextChanged += new System.EventHandler(this.OnMaxXChanged);
      // 
      // txtMinY
      // 
      this.txtMinY.Name = "txtMinY";
      this.txtMinY.Size = new System.Drawing.Size(50, 25);
      this.txtMinY.ToolTipText = "Min Y";
      this.txtMinY.TextChanged += new System.EventHandler(this.OnMinYChanged);
      // 
      // txtMaxY
      // 
      this.txtMaxY.Name = "txtMaxY";
      this.txtMaxY.Size = new System.Drawing.Size(50, 25);
      this.txtMaxY.ToolTipText = "Max Y";
      this.txtMaxY.TextChanged += new System.EventHandler(this.OnMaxYChanged);
      // 
      // statusStrip1
      // 
      this.statusStrip1.Dock = System.Windows.Forms.DockStyle.None;
      this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblMessage,
            this.lblStatus,
            this.lblSize,
            this.lblPosition,
            this.lblData});
      this.statusStrip1.Location = new System.Drawing.Point(0, 25);
      this.statusStrip1.Name = "statusStrip1";
      this.statusStrip1.Size = new System.Drawing.Size(984, 22);
      this.statusStrip1.TabIndex = 0;
      // 
      // lblMessage
      // 
      this.lblMessage.Name = "lblMessage";
      this.lblMessage.Size = new System.Drawing.Size(969, 17);
      this.lblMessage.Spring = true;
      // 
      // lblStatus
      // 
      this.lblStatus.Name = "lblStatus";
      this.lblStatus.Size = new System.Drawing.Size(0, 17);
      this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lblSize
      // 
      this.lblSize.Name = "lblSize";
      this.lblSize.Size = new System.Drawing.Size(0, 17);
      // 
      // lblPosition
      // 
      this.lblPosition.Name = "lblPosition";
      this.lblPosition.Size = new System.Drawing.Size(0, 17);
      // 
      // lblData
      // 
      this.lblData.Name = "lblData";
      this.lblData.Size = new System.Drawing.Size(0, 17);
      // 
      // tableLayoutPanel1
      // 
      this.tableLayoutPanel1.ColumnCount = 1;
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel1.Controls.Add(this.ctrlPalette1, 0, 1);
      this.tableLayoutPanel1.Controls.Add(this.dmpRenderControl1, 0, 0);
      this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tableLayoutPanel1.Location = new System.Drawing.Point(5, 5);
      this.tableLayoutPanel1.Name = "tableLayoutPanel1";
      this.tableLayoutPanel1.RowCount = 2;
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 71F));
      this.tableLayoutPanel1.Size = new System.Drawing.Size(974, 455);
      this.tableLayoutPanel1.TabIndex = 2;
      // 
      // ctrlPalette1
      // 
      palleteArgs1.Max = 0D;
      palleteArgs1.Min = 0D;
      palleteArgs1.Value = 0D;
      this.ctrlPalette1.Current = palleteArgs1;
      this.ctrlPalette1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.ctrlPalette1.IsPainting = false;
      this.ctrlPalette1.IssuingEvents = true;
      this.ctrlPalette1.Location = new System.Drawing.Point(3, 387);
      this.ctrlPalette1.Name = "ctrlPalette1";
      dmpPalette1.Colors = ((System.Collections.Generic.List<System.Drawing.Color>)(resources.GetObject("dmpPalette1.Colors")));
      dmpPalette1.IsFirstTransparent = true;
      this.ctrlPalette1.Palette = dmpPalette1;
      this.ctrlPalette1.Size = new System.Drawing.Size(968, 65);
      this.ctrlPalette1.TabIndex = 1;
      // 
      // dmpRenderControl1
      // 
      this.dmpRenderControl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
      this.dmpRenderControl1.CurrentValue = 10D;
      this.dmpRenderControl1.DisableEditing = false;
      this.dmpRenderControl1.DisableNotification = false;
      this.dmpRenderControl1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.dmpRenderControl1.Location = new System.Drawing.Point(3, 3);
      this.dmpRenderControl1.Model = null;
      this.dmpRenderControl1.Name = "dmpRenderControl1";
      this.dmpRenderControl1.PencilSize = 1;
      this.dmpRenderControl1.Size = new System.Drawing.Size(968, 378);
      this.dmpRenderControl1.TabIndex = 0;
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
      this.newToolStripMenuItem.Click += new System.EventHandler(this.OnNewClick);
      // 
      // openToolStripMenuItem
      // 
      this.openToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("openToolStripMenuItem.Image")));
      this.openToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.openToolStripMenuItem.Name = "openToolStripMenuItem";
      this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
      this.openToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
      this.openToolStripMenuItem.Text = "&Open";
      this.openToolStripMenuItem.Click += new System.EventHandler(this.OnOpenClick);
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
      this.saveToolStripMenuItem.Click += new System.EventHandler(this.OnSaveClick);
      // 
      // saveAsToolStripMenuItem
      // 
      this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
      this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
      this.saveAsToolStripMenuItem.Text = "Save &As";
      this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.OnSaveAsClick);
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
      this.exitToolStripMenuItem.Click += new System.EventHandler(this.OnExitClick);
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
      this.aboutToolStripMenuItem.Click += new System.EventHandler(this.OnAboutClick);
      // 
      // mainToolStrip
      // 
      this.mainToolStrip.Dock = System.Windows.Forms.DockStyle.None;
      this.mainToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
      this.mainToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripButton,
            this.openToolStripButton,
            this.saveToolStripButton,
            this.toolStripSeparator6,
            this.cmbPalettes,
            this.toolStripSeparator2,
            this.txtSize,
            this.toolStripSeparator3,
            this.toolStripButton2,
            this.toolStripButton3,
            this.cmdCenter,
            this.toolStripSeparator5,
            this.toolAdjust,
            this.toolStripSeparator7,
            this.toolStripButton4,
            this.toolStripSeparator8,
            this.helpToolStripButton});
      this.mainToolStrip.Location = new System.Drawing.Point(0, 24);
      this.mainToolStrip.Name = "mainToolStrip";
      this.mainToolStrip.Padding = new System.Windows.Forms.Padding(10, 0, 1, 0);
      this.mainToolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
      this.mainToolStrip.Size = new System.Drawing.Size(984, 25);
      this.mainToolStrip.Stretch = true;
      this.mainToolStrip.TabIndex = 1;
      // 
      // newToolStripButton
      // 
      this.newToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.newToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("newToolStripButton.Image")));
      this.newToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.newToolStripButton.Name = "newToolStripButton";
      this.newToolStripButton.Size = new System.Drawing.Size(23, 22);
      this.newToolStripButton.Text = "&New";
      this.newToolStripButton.Click += new System.EventHandler(this.OnNewClick);
      // 
      // openToolStripButton
      // 
      this.openToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.openToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("openToolStripButton.Image")));
      this.openToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.openToolStripButton.Name = "openToolStripButton";
      this.openToolStripButton.Size = new System.Drawing.Size(23, 22);
      this.openToolStripButton.Text = "&Open";
      this.openToolStripButton.Click += new System.EventHandler(this.OnOpenClick);
      // 
      // saveToolStripButton
      // 
      this.saveToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.saveToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("saveToolStripButton.Image")));
      this.saveToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.saveToolStripButton.Name = "saveToolStripButton";
      this.saveToolStripButton.Size = new System.Drawing.Size(23, 22);
      this.saveToolStripButton.Text = "&Save";
      this.saveToolStripButton.Click += new System.EventHandler(this.OnSaveClick);
      // 
      // toolStripSeparator6
      // 
      this.toolStripSeparator6.Name = "toolStripSeparator6";
      this.toolStripSeparator6.Size = new System.Drawing.Size(6, 25);
      // 
      // cmbPalettes
      // 
      this.cmbPalettes.Items.AddRange(new object[] {
            "Default"});
      this.cmbPalettes.Name = "cmbPalettes";
      this.cmbPalettes.Size = new System.Drawing.Size(121, 25);
      this.cmbPalettes.Text = "Default";
      this.cmbPalettes.ToolTipText = "Choose Palette";
      this.cmbPalettes.DropDownClosed += new System.EventHandler(this.OnPaletteChanged);
      this.cmbPalettes.SelectedIndexChanged += new System.EventHandler(this.OnPaletteChanged);
      this.cmbPalettes.TextUpdate += new System.EventHandler(this.OnPaletteChanged);
      // 
      // toolStripSeparator2
      // 
      this.toolStripSeparator2.Name = "toolStripSeparator2";
      this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
      // 
      // txtSize
      // 
      this.txtSize.Name = "txtSize";
      this.txtSize.Size = new System.Drawing.Size(100, 25);
      this.txtSize.Text = "3";
      this.txtSize.ToolTipText = "Width of Pen";
      this.txtSize.TextChanged += new System.EventHandler(this.OnSizeChanged);
      // 
      // toolStripSeparator3
      // 
      this.toolStripSeparator3.Name = "toolStripSeparator3";
      this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
      // 
      // toolStripButton2
      // 
      this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
      this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
      this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripButton2.Name = "toolStripButton2";
      this.toolStripButton2.Size = new System.Drawing.Size(31, 22);
      this.toolStripButton2.Text = "Left";
      this.toolStripButton2.Click += new System.EventHandler(this.OnRotateLeft);
      // 
      // toolStripButton3
      // 
      this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
      this.toolStripButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton3.Image")));
      this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripButton3.Name = "toolStripButton3";
      this.toolStripButton3.Size = new System.Drawing.Size(39, 22);
      this.toolStripButton3.Text = "Right";
      this.toolStripButton3.Click += new System.EventHandler(this.OnRotateRight);
      // 
      // cmdCenter
      // 
      this.cmdCenter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
      this.cmdCenter.Image = ((System.Drawing.Image)(resources.GetObject("cmdCenter.Image")));
      this.cmdCenter.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.cmdCenter.Name = "cmdCenter";
      this.cmdCenter.Size = new System.Drawing.Size(46, 22);
      this.cmdCenter.Text = "Center";
      this.cmdCenter.ToolTipText = "Center scene in the middle of the grid";
      this.cmdCenter.Click += new System.EventHandler(this.OnCenterClick);
      // 
      // toolStripSeparator5
      // 
      this.toolStripSeparator5.Name = "toolStripSeparator5";
      this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
      // 
      // toolAdjust
      // 
      this.toolAdjust.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
      this.toolAdjust.Image = ((System.Drawing.Image)(resources.GetObject("toolAdjust.Image")));
      this.toolAdjust.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolAdjust.Name = "toolAdjust";
      this.toolAdjust.Size = new System.Drawing.Size(45, 22);
      this.toolAdjust.Text = "Adjust";
      this.toolAdjust.Click += new System.EventHandler(this.OnSplitIntoIndividuals);
      // 
      // toolStripSeparator7
      // 
      this.toolStripSeparator7.Name = "toolStripSeparator7";
      this.toolStripSeparator7.Size = new System.Drawing.Size(6, 25);
      // 
      // toolStripButton4
      // 
      this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
      this.toolStripButton4.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton4.Image")));
      this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripButton4.Name = "toolStripButton4";
      this.toolStripButton4.Size = new System.Drawing.Size(47, 22);
      this.toolStripButton4.Text = "Aspect";
      this.toolStripButton4.Click += new System.EventHandler(this.OnAspectClick);
      // 
      // toolStripSeparator8
      // 
      this.toolStripSeparator8.Name = "toolStripSeparator8";
      this.toolStripSeparator8.Size = new System.Drawing.Size(6, 25);
      // 
      // helpToolStripButton
      // 
      this.helpToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.helpToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("helpToolStripButton.Image")));
      this.helpToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.helpToolStripButton.Name = "helpToolStripButton";
      this.helpToolStripButton.Size = new System.Drawing.Size(23, 22);
      this.helpToolStripButton.Text = "He&lp";
      this.helpToolStripButton.Click += new System.EventHandler(this.OnAboutClick);
      // 
      // MainForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(984, 561);
      this.Controls.Add(this.toolStripContainer1);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MainMenuStrip = this.menuStrip1;
      this.Name = "MainForm";
      this.Text = "Edit DMP";
      this.Load += new System.EventHandler(this.OnLoad);
      this.toolStripContainer1.BottomToolStripPanel.ResumeLayout(false);
      this.toolStripContainer1.BottomToolStripPanel.PerformLayout();
      this.toolStripContainer1.ContentPanel.ResumeLayout(false);
      this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
      this.toolStripContainer1.TopToolStripPanel.PerformLayout();
      this.toolStripContainer1.ResumeLayout(false);
      this.toolStripContainer1.PerformLayout();
      this.sizeToolStrip.ResumeLayout(false);
      this.sizeToolStrip.PerformLayout();
      this.statusStrip1.ResumeLayout(false);
      this.statusStrip1.PerformLayout();
      this.tableLayoutPanel1.ResumeLayout(false);
      this.menuStrip1.ResumeLayout(false);
      this.menuStrip1.PerformLayout();
      this.mainToolStrip.ResumeLayout(false);
      this.mainToolStrip.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private ToolStripContainer toolStripContainer1;
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
    private ToolStrip mainToolStrip;
    private ToolStripButton newToolStripButton;
    private ToolStripButton openToolStripButton;
    private ToolStripButton saveToolStripButton;
    private ToolStripSeparator toolStripSeparator6;
    private ToolStripSeparator toolStripSeparator7;
    private ToolStripButton helpToolStripButton;
    private StatusStrip statusStrip1;
    private ToolStripStatusLabel lblStatus;
    private ToolStripStatusLabel lblSize;
    private DmpRenderControl dmpRenderControl1;
    private TableLayoutPanel tableLayoutPanel1;
    private CtrlPalette ctrlPalette1;
    private ToolStripComboBox cmbPalettes;
    private ToolStripSeparator toolStripSeparator2;
    private ToolStripTextBox txtSize;
    private ToolStripSeparator toolStripSeparator3;
    private ToolStripButton toolStripButton2;
    private ToolStripButton toolStripButton3;
    private ToolStripButton cmdCenter;
    private ToolStrip sizeToolStrip;
    private ToolStripLabel toolStripLabel1;
    private ToolStripTextBox txtCols;
    private ToolStripLabel toolStripLabel2;
    private ToolStripTextBox txtRows;
    private ToolStripButton toolStripButton1;
    private ToolStripSeparator toolStripSeparator4;
    private ToolStripTextBox txtMinX;
    private ToolStripTextBox txtMaxX;
    private ToolStripTextBox txtMinY;
    private ToolStripTextBox txtMaxY;
    private ToolStripStatusLabel lblPosition;
    private ToolStripStatusLabel lblMessage;
    private ToolStripStatusLabel lblData;
    private ToolStripSeparator toolStripSeparator5;
    private ToolStripButton toolAdjust;
    private ToolStripButton toolStripButton4;
    private ToolStripSeparator toolStripSeparator8;
  }
}

