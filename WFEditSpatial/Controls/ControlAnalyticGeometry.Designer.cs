﻿using System.ComponentModel;
using System.Windows.Forms;

namespace EditSpatial.Controls
{
  partial class ControlAnalyticGeometry
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

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      this.grid = new System.Windows.Forms.DataGridView();
      this.colSpatialId = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colType = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colOrdinal = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colDomainType = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colMath = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.label1 = new System.Windows.Forms.Label();
      this.txtId = new System.Windows.Forms.TextBox();
      this.thumbGeometry = new System.Windows.Forms.PictureBox();
      this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
      this.panel1 = new System.Windows.Forms.Panel();
      this.cmdExport = new System.Windows.Forms.Button();
      this.cmdMakeFirst = new System.Windows.Forms.Button();
      this.cmdSort = new System.Windows.Forms.Button();
      this.cmdCreateTiff = new System.Windows.Forms.Button();
      this.trackBar1 = new System.Windows.Forms.TrackBar();
      this.cmdUpdateImage = new System.Windows.Forms.Button();
      this.txtZ = new System.Windows.Forms.TextBox();
      this.lblZ = new System.Windows.Forms.Label();
      this.txtSize = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.cmdReorder = new System.Windows.Forms.Button();
      this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
      this.chkIsActive = new System.Windows.Forms.CheckBox();
      ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
      this.groupBox1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.thumbGeometry)).BeginInit();
      this.tableLayoutPanel1.SuspendLayout();
      this.panel1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
      this.SuspendLayout();
      // 
      // grid
      // 
      this.grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colSpatialId,
            this.colType,
            this.colOrdinal,
            this.colDomainType,
            this.colMath});
      this.grid.Dock = System.Windows.Forms.DockStyle.Fill;
      this.grid.Location = new System.Drawing.Point(3, 16);
      this.grid.Name = "grid";
      this.grid.Size = new System.Drawing.Size(542, 159);
      this.grid.TabIndex = 0;
      this.grid.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.OnRowsAdded);
      // 
      // colSpatialId
      // 
      this.colSpatialId.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
      this.colSpatialId.HeaderText = "Spatial Id";
      this.colSpatialId.Name = "colSpatialId";
      this.colSpatialId.Width = 76;
      // 
      // colType
      // 
      this.colType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader;
      this.colType.HeaderText = "Function Type";
      this.colType.Name = "colType";
      this.colType.Width = 21;
      // 
      // colOrdinal
      // 
      this.colOrdinal.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
      this.colOrdinal.HeaderText = "Ordinal";
      this.colOrdinal.Name = "colOrdinal";
      this.colOrdinal.Width = 65;
      // 
      // colDomainType
      // 
      this.colDomainType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader;
      this.colDomainType.HeaderText = "Domain Type";
      this.colDomainType.Name = "colDomainType";
      this.colDomainType.Width = 21;
      // 
      // colMath
      // 
      this.colMath.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
      this.colMath.HeaderText = "Math";
      this.colMath.Name = "colMath";
      // 
      // groupBox1
      // 
      this.tableLayoutPanel1.SetColumnSpan(this.groupBox1, 2);
      this.groupBox1.Controls.Add(this.grid);
      this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.groupBox1.Location = new System.Drawing.Point(3, 187);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(548, 178);
      this.groupBox1.TabIndex = 1;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Analytic Volumes: ";
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(18, 10);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(22, 13);
      this.label1.TabIndex = 2;
      this.label1.Text = "Id: ";
      // 
      // txtId
      // 
      this.txtId.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtId.Location = new System.Drawing.Point(46, 7);
      this.txtId.Name = "txtId";
      this.txtId.Size = new System.Drawing.Size(211, 20);
      this.txtId.TabIndex = 3;
      // 
      // thumbGeometry
      // 
      this.thumbGeometry.BackColor = System.Drawing.Color.White;
      this.thumbGeometry.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.thumbGeometry.Dock = System.Windows.Forms.DockStyle.Fill;
      this.thumbGeometry.InitialImage = global::EditSpatial.Properties.Resources.IMAGE_NoGeomery;
      this.thumbGeometry.Location = new System.Drawing.Point(280, 3);
      this.thumbGeometry.Name = "thumbGeometry";
      this.thumbGeometry.Size = new System.Drawing.Size(271, 178);
      this.thumbGeometry.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.thumbGeometry.TabIndex = 4;
      this.thumbGeometry.TabStop = false;
      // 
      // tableLayoutPanel1
      // 
      this.tableLayoutPanel1.ColumnCount = 2;
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
      this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 1);
      this.tableLayoutPanel1.Controls.Add(this.thumbGeometry, 1, 0);
      this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
      this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
      this.tableLayoutPanel1.MinimumSize = new System.Drawing.Size(547, 326);
      this.tableLayoutPanel1.Name = "tableLayoutPanel1";
      this.tableLayoutPanel1.RowCount = 2;
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
      this.tableLayoutPanel1.Size = new System.Drawing.Size(554, 368);
      this.tableLayoutPanel1.TabIndex = 5;
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.chkIsActive);
      this.panel1.Controls.Add(this.cmdExport);
      this.panel1.Controls.Add(this.cmdMakeFirst);
      this.panel1.Controls.Add(this.cmdSort);
      this.panel1.Controls.Add(this.cmdCreateTiff);
      this.panel1.Controls.Add(this.trackBar1);
      this.panel1.Controls.Add(this.cmdUpdateImage);
      this.panel1.Controls.Add(this.txtZ);
      this.panel1.Controls.Add(this.lblZ);
      this.panel1.Controls.Add(this.txtSize);
      this.panel1.Controls.Add(this.label2);
      this.panel1.Controls.Add(this.cmdReorder);
      this.panel1.Controls.Add(this.label1);
      this.panel1.Controls.Add(this.txtId);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel1.Location = new System.Drawing.Point(3, 3);
      this.panel1.MinimumSize = new System.Drawing.Size(267, 142);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(271, 178);
      this.panel1.TabIndex = 5;
      // 
      // cmdExport
      // 
      this.cmdExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.cmdExport.Location = new System.Drawing.Point(182, 31);
      this.cmdExport.Name = "cmdExport";
      this.cmdExport.Size = new System.Drawing.Size(75, 23);
      this.cmdExport.TabIndex = 14;
      this.cmdExport.Text = "Export DMP";
      this.cmdExport.UseVisualStyleBackColor = true;
      this.cmdExport.Click += new System.EventHandler(this.cmdExport_Click);
      // 
      // cmdMakeFirst
      // 
      this.cmdMakeFirst.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.cmdMakeFirst.Location = new System.Drawing.Point(182, 54);
      this.cmdMakeFirst.Name = "cmdMakeFirst";
      this.cmdMakeFirst.Size = new System.Drawing.Size(75, 23);
      this.cmdMakeFirst.TabIndex = 13;
      this.cmdMakeFirst.Text = "First";
      this.toolTip1.SetToolTip(this.cmdMakeFirst, "Moves the selected entry to the first position");
      this.cmdMakeFirst.UseVisualStyleBackColor = true;
      this.cmdMakeFirst.Click += new System.EventHandler(this.OnMakeFirstClick);
      // 
      // cmdSort
      // 
      this.cmdSort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.cmdSort.Location = new System.Drawing.Point(182, 151);
      this.cmdSort.Name = "cmdSort";
      this.cmdSort.Size = new System.Drawing.Size(75, 23);
      this.cmdSort.TabIndex = 12;
      this.cmdSort.Text = "Sor&t";
      this.cmdSort.UseVisualStyleBackColor = true;
      this.cmdSort.Click += new System.EventHandler(this.OnSortClick);
      // 
      // cmdCreateTiff
      // 
      this.cmdCreateTiff.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.cmdCreateTiff.Location = new System.Drawing.Point(182, 77);
      this.cmdCreateTiff.Name = "cmdCreateTiff";
      this.cmdCreateTiff.Size = new System.Drawing.Size(75, 23);
      this.cmdCreateTiff.TabIndex = 11;
      this.cmdCreateTiff.Text = "Tiff";
      this.cmdCreateTiff.UseVisualStyleBackColor = true;
      this.cmdCreateTiff.Click += new System.EventHandler(this.OnExportTiffClick);
      // 
      // trackBar1
      // 
      this.trackBar1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.trackBar1.BackColor = System.Drawing.Color.White;
      this.trackBar1.Location = new System.Drawing.Point(6, 77);
      this.trackBar1.Maximum = 100;
      this.trackBar1.Name = "trackBar1";
      this.trackBar1.Size = new System.Drawing.Size(166, 45);
      this.trackBar1.TabIndex = 10;
      this.trackBar1.TickFrequency = 5;
      this.trackBar1.Scroll += new System.EventHandler(this.OnTrackChanged);
      // 
      // cmdUpdateImage
      // 
      this.cmdUpdateImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.cmdUpdateImage.Location = new System.Drawing.Point(182, 101);
      this.cmdUpdateImage.Name = "cmdUpdateImage";
      this.cmdUpdateImage.Size = new System.Drawing.Size(75, 23);
      this.cmdUpdateImage.TabIndex = 9;
      this.cmdUpdateImage.Text = "&Update";
      this.cmdUpdateImage.UseVisualStyleBackColor = true;
      this.cmdUpdateImage.Click += new System.EventHandler(this.OnUpdateImage);
      // 
      // txtZ
      // 
      this.txtZ.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtZ.Location = new System.Drawing.Point(72, 128);
      this.txtZ.Name = "txtZ";
      this.txtZ.Size = new System.Drawing.Size(104, 20);
      this.txtZ.TabIndex = 8;
      this.txtZ.Text = "0";
      this.txtZ.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      // 
      // lblZ
      // 
      this.lblZ.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.lblZ.AutoSize = true;
      this.lblZ.Location = new System.Drawing.Point(3, 131);
      this.lblZ.Name = "lblZ";
      this.lblZ.Size = new System.Drawing.Size(46, 13);
      this.lblZ.TabIndex = 7;
      this.lblZ.Text = "Z value:";
      // 
      // txtSize
      // 
      this.txtSize.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtSize.Location = new System.Drawing.Point(72, 154);
      this.txtSize.Name = "txtSize";
      this.txtSize.Size = new System.Drawing.Size(104, 20);
      this.txtSize.TabIndex = 6;
      this.txtSize.Text = "128";
      this.txtSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      // 
      // label2
      // 
      this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(3, 157);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(63, 13);
      this.label2.TabIndex = 5;
      this.label2.Text = "ThumbSize:";
      // 
      // cmdReorder
      // 
      this.cmdReorder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.cmdReorder.Location = new System.Drawing.Point(182, 126);
      this.cmdReorder.Name = "cmdReorder";
      this.cmdReorder.Size = new System.Drawing.Size(75, 23);
      this.cmdReorder.TabIndex = 4;
      this.cmdReorder.Text = "&Flip Order";
      this.cmdReorder.UseVisualStyleBackColor = true;
      this.cmdReorder.Click += new System.EventHandler(this.OnReorderClick);
      // 
      // chkIsActive
      // 
      this.chkIsActive.AutoSize = true;
      this.chkIsActive.Location = new System.Drawing.Point(21, 36);
      this.chkIsActive.Name = "chkIsActive";
      this.chkIsActive.Size = new System.Drawing.Size(64, 17);
      this.chkIsActive.TabIndex = 15;
      this.chkIsActive.Text = "IsActive";
      this.chkIsActive.UseVisualStyleBackColor = true;
      // 
      // ControlAnalyticGeometry
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.tableLayoutPanel1);
      this.Name = "ControlAnalyticGeometry";
      this.Size = new System.Drawing.Size(554, 368);
      ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
      this.groupBox1.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.thumbGeometry)).EndInit();
      this.tableLayoutPanel1.ResumeLayout(false);
      this.panel1.ResumeLayout(false);
      this.panel1.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private DataGridView grid;
    private GroupBox groupBox1;
    private Label label1;
    private TextBox txtId;
    private DataGridViewTextBoxColumn colSpatialId;
    private DataGridViewTextBoxColumn colType;
    private DataGridViewTextBoxColumn colOrdinal;
    private DataGridViewTextBoxColumn colDomainType;
    private DataGridViewTextBoxColumn colMath;
    private PictureBox thumbGeometry;
    private TableLayoutPanel tableLayoutPanel1;
    private Panel panel1;
    private TextBox txtSize;
    private Label label2;
    private Button cmdReorder;
    private Button cmdUpdateImage;
    private TextBox txtZ;
    private Label lblZ;
    private TrackBar trackBar1;
    private Button cmdCreateTiff;
    private ToolTip toolTip1;
    private Button cmdSort;
    private Button cmdMakeFirst;
    private Button cmdExport;
    private CheckBox chkIsActive;
  }
}
