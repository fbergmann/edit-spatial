namespace EditSpatial.Forms
{
  partial class FormSpatialAnnotation
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSpatialAnnotation));
      this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
      this.splitContainer1 = new System.Windows.Forms.SplitContainer();
      this.toolStrip1 = new System.Windows.Forms.ToolStrip();
      this.cmdFromNotes = new System.Windows.Forms.ToolStripButton();
      this.cmdClear = new System.Windows.Forms.ToolStripButton();
      this.grid = new System.Windows.Forms.DataGridView();
      this.colId = new System.Windows.Forms.DataGridViewComboBoxColumn();
      this.colPalette = new System.Windows.Forms.DataGridViewComboBoxColumn();
      this.colMax = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.panel1 = new System.Windows.Forms.Panel();
      this.txtUpdate = new System.Windows.Forms.TextBox();
      this.label3 = new System.Windows.Forms.Label();
      this.txtStep = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.cmdOK = new System.Windows.Forms.Button();
      this.cmdCancel = new System.Windows.Forms.Button();
      this.label1 = new System.Windows.Forms.Label();
      this.tableLayoutPanel1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      this.toolStrip1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
      this.panel1.SuspendLayout();
      this.SuspendLayout();
      // 
      // tableLayoutPanel1
      // 
      this.tableLayoutPanel1.ColumnCount = 1;
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel1.Controls.Add(this.splitContainer1, 0, 1);
      this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 2);
      this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
      this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
      this.tableLayoutPanel1.Name = "tableLayoutPanel1";
      this.tableLayoutPanel1.RowCount = 3;
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
      this.tableLayoutPanel1.Size = new System.Drawing.Size(616, 476);
      this.tableLayoutPanel1.TabIndex = 0;
      // 
      // splitContainer1
      // 
      this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer1.Location = new System.Drawing.Point(3, 37);
      this.splitContainer1.Name = "splitContainer1";
      // 
      // splitContainer1.Panel1
      // 
      this.splitContainer1.Panel1.Controls.Add(this.toolStrip1);
      // 
      // splitContainer1.Panel2
      // 
      this.splitContainer1.Panel2.Controls.Add(this.grid);
      this.splitContainer1.Size = new System.Drawing.Size(610, 402);
      this.splitContainer1.SplitterDistance = 42;
      this.splitContainer1.TabIndex = 0;
      // 
      // toolStrip1
      // 
      this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
      this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmdFromNotes,
            this.cmdClear});
      this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
      this.toolStrip1.Location = new System.Drawing.Point(0, 0);
      this.toolStrip1.Name = "toolStrip1";
      this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
      this.toolStrip1.Size = new System.Drawing.Size(42, 402);
      this.toolStrip1.TabIndex = 0;
      this.toolStrip1.Text = "toolStrip1";
      this.toolStrip1.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
      // 
      // cmdFromNotes
      // 
      this.cmdFromNotes.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
      this.cmdFromNotes.Image = ((System.Drawing.Image)(resources.GetObject("cmdFromNotes.Image")));
      this.cmdFromNotes.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.cmdFromNotes.Name = "cmdFromNotes";
      this.cmdFromNotes.Size = new System.Drawing.Size(40, 73);
      this.cmdFromNotes.Text = "From Notes";
      this.cmdFromNotes.Click += new System.EventHandler(this.OnInitFromNotesClick);
      // 
      // cmdClear
      // 
      this.cmdClear.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
      this.cmdClear.Image = ((System.Drawing.Image)(resources.GetObject("cmdClear.Image")));
      this.cmdClear.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.cmdClear.Name = "cmdClear";
      this.cmdClear.Size = new System.Drawing.Size(40, 38);
      this.cmdClear.Text = "Clear";
      this.cmdClear.Click += new System.EventHandler(this.OnClearClick);
      // 
      // grid
      // 
      this.grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colId,
            this.colPalette,
            this.colMax});
      this.grid.Dock = System.Windows.Forms.DockStyle.Fill;
      this.grid.Location = new System.Drawing.Point(0, 0);
      this.grid.Name = "grid";
      this.grid.Size = new System.Drawing.Size(564, 402);
      this.grid.TabIndex = 0;
      // 
      // colId
      // 
      this.colId.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
      this.colId.HeaderText = "SBML ID";
      this.colId.Items.AddRange(new object[] {
            "A"});
      this.colId.Name = "colId";
      this.colId.Resizable = System.Windows.Forms.DataGridViewTriState.True;
      this.colId.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
      // 
      // colPalette
      // 
      this.colPalette.HeaderText = "Palette";
      this.colPalette.Items.AddRange(new object[] {
            "black-blue.txt",
            "black-green.txt",
            "black-red.txt"});
      this.colPalette.Name = "colPalette";
      this.colPalette.Resizable = System.Windows.Forms.DataGridViewTriState.True;
      this.colPalette.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
      // 
      // colMax
      // 
      this.colMax.HeaderText = "Max";
      this.colMax.Name = "colMax";
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.txtUpdate);
      this.panel1.Controls.Add(this.label3);
      this.panel1.Controls.Add(this.txtStep);
      this.panel1.Controls.Add(this.label2);
      this.panel1.Controls.Add(this.cmdOK);
      this.panel1.Controls.Add(this.cmdCancel);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel1.Location = new System.Drawing.Point(3, 445);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(610, 28);
      this.panel1.TabIndex = 1;
      // 
      // txtUpdate
      // 
      this.txtUpdate.Location = new System.Drawing.Point(168, 4);
      this.txtUpdate.Name = "txtUpdate";
      this.txtUpdate.Size = new System.Drawing.Size(64, 20);
      this.txtUpdate.TabIndex = 5;
      this.txtUpdate.Text = "100";
      this.txtUpdate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(117, 7);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(45, 13);
      this.label3.TabIndex = 4;
      this.label3.Text = "Update:";
      // 
      // txtStep
      // 
      this.txtStep.Location = new System.Drawing.Point(47, 4);
      this.txtStep.Name = "txtStep";
      this.txtStep.Size = new System.Drawing.Size(64, 20);
      this.txtStep.TabIndex = 3;
      this.txtStep.Text = "0.1";
      this.txtStep.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(9, 7);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(32, 13);
      this.label2.TabIndex = 2;
      this.label2.Text = "Step:";
      // 
      // cmdOK
      // 
      this.cmdOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.cmdOK.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.cmdOK.Location = new System.Drawing.Point(451, 2);
      this.cmdOK.Name = "cmdOK";
      this.cmdOK.Size = new System.Drawing.Size(75, 23);
      this.cmdOK.TabIndex = 1;
      this.cmdOK.Text = "&OK";
      this.cmdOK.UseVisualStyleBackColor = true;
      this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
      // 
      // cmdCancel
      // 
      this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.cmdCancel.Location = new System.Drawing.Point(532, 2);
      this.cmdCancel.Name = "cmdCancel";
      this.cmdCancel.Size = new System.Drawing.Size(75, 23);
      this.cmdCancel.TabIndex = 0;
      this.cmdCancel.Text = "&Cancel";
      this.cmdCancel.UseVisualStyleBackColor = true;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.label1.Location = new System.Drawing.Point(3, 0);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(610, 34);
      this.label1.TabIndex = 2;
      this.label1.Text = "Here you can specify the initial display for  Akira\'s simulator";
      this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // FormSpatialAnnotation
      // 
      this.AcceptButton = this.cmdOK;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.cmdCancel;
      this.ClientSize = new System.Drawing.Size(616, 476);
      this.Controls.Add(this.tableLayoutPanel1);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "FormSpatialAnnotation";
      this.Text = "Annotation";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormSpatialAnnotation_FormClosing);
      this.tableLayoutPanel1.ResumeLayout(false);
      this.tableLayoutPanel1.PerformLayout();
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel1.PerformLayout();
      this.splitContainer1.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
      this.splitContainer1.ResumeLayout(false);
      this.toolStrip1.ResumeLayout(false);
      this.toolStrip1.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
      this.panel1.ResumeLayout(false);
      this.panel1.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    private System.Windows.Forms.SplitContainer splitContainer1;
    private System.Windows.Forms.DataGridView grid;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.TextBox txtUpdate;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.TextBox txtStep;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Button cmdOK;
    private System.Windows.Forms.Button cmdCancel;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.DataGridViewComboBoxColumn colId;
    private System.Windows.Forms.DataGridViewComboBoxColumn colPalette;
    private System.Windows.Forms.DataGridViewTextBoxColumn colMax;
    private System.Windows.Forms.ToolStrip toolStrip1;
    private System.Windows.Forms.ToolStripButton cmdFromNotes;
    private System.Windows.Forms.ToolStripButton cmdClear;
  }
}