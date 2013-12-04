﻿namespace EditSpatial.Controls
{
  partial class ControlSampleFieldGeometry
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

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.grid = new System.Windows.Forms.DataGridView();
      this.colSpatialId = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colDomainType = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colSampledValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colMinValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colMaxValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.label1 = new System.Windows.Forms.Label();
      this.txtId = new System.Windows.Forms.TextBox();
      this.thumbGeometry = new System.Windows.Forms.PictureBox();
      this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
      this.panel1 = new System.Windows.Forms.Panel();
      this.txtSize = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
      this.groupBox1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.thumbGeometry)).BeginInit();
      this.tableLayoutPanel1.SuspendLayout();
      this.panel1.SuspendLayout();
      this.SuspendLayout();
      // 
      // grid
      // 
      this.grid.AllowUserToAddRows = false;
      this.grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colSpatialId,
            this.colDomainType,
            this.colSampledValue,
            this.colMinValue,
            this.colMaxValue});
      this.grid.Dock = System.Windows.Forms.DockStyle.Fill;
      this.grid.Location = new System.Drawing.Point(3, 16);
      this.grid.Name = "grid";
      this.grid.Size = new System.Drawing.Size(535, 123);
      this.grid.TabIndex = 0;
      // 
      // colSpatialId
      // 
      this.colSpatialId.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
      this.colSpatialId.HeaderText = "Spatial Id";
      this.colSpatialId.Name = "colSpatialId";
      this.colSpatialId.Width = 68;
      // 
      // colDomainType
      // 
      this.colDomainType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader;
      this.colDomainType.HeaderText = "Domain Type";
      this.colDomainType.Name = "colDomainType";
      this.colDomainType.Width = 5;
      // 
      // colSampledValue
      // 
      this.colSampledValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader;
      this.colSampledValue.HeaderText = "Sampled Value";
      this.colSampledValue.Name = "colSampledValue";
      this.colSampledValue.Width = 5;
      // 
      // colMinValue
      // 
      this.colMinValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader;
      this.colMinValue.HeaderText = "Min Value";
      this.colMinValue.Name = "colMinValue";
      this.colMinValue.Width = 5;
      // 
      // colMaxValue
      // 
      this.colMaxValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
      this.colMaxValue.HeaderText = "Max Value";
      this.colMaxValue.Name = "colMaxValue";
      // 
      // groupBox1
      // 
      this.tableLayoutPanel1.SetColumnSpan(this.groupBox1, 2);
      this.groupBox1.Controls.Add(this.grid);
      this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.groupBox1.Location = new System.Drawing.Point(3, 151);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(541, 142);
      this.groupBox1.TabIndex = 1;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Analytic Volumes: ";
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(3, 10);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(22, 13);
      this.label1.TabIndex = 2;
      this.label1.Text = "Id: ";
      // 
      // txtId
      // 
      this.txtId.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtId.Location = new System.Drawing.Point(31, 7);
      this.txtId.Name = "txtId";
      this.txtId.Size = new System.Drawing.Size(222, 20);
      this.txtId.TabIndex = 3;
      // 
      // thumbGeometry
      // 
      this.thumbGeometry.BackColor = System.Drawing.Color.White;
      this.thumbGeometry.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.thumbGeometry.Dock = System.Windows.Forms.DockStyle.Fill;
      this.thumbGeometry.InitialImage = global::EditSpatial.Properties.Resources.IMAGE_NoGeomery;
      this.thumbGeometry.Location = new System.Drawing.Point(276, 3);
      this.thumbGeometry.Name = "thumbGeometry";
      this.thumbGeometry.Size = new System.Drawing.Size(268, 142);
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
      this.tableLayoutPanel1.Name = "tableLayoutPanel1";
      this.tableLayoutPanel1.RowCount = 2;
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
      this.tableLayoutPanel1.Size = new System.Drawing.Size(547, 296);
      this.tableLayoutPanel1.TabIndex = 5;
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.txtSize);
      this.panel1.Controls.Add(this.label2);
      this.panel1.Controls.Add(this.label1);
      this.panel1.Controls.Add(this.txtId);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel1.Location = new System.Drawing.Point(3, 3);
      this.panel1.MinimumSize = new System.Drawing.Size(267, 142);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(267, 142);
      this.panel1.TabIndex = 5;
      // 
      // txtSize
      // 
      this.txtSize.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtSize.Location = new System.Drawing.Point(72, 118);
      this.txtSize.Name = "txtSize";
      this.txtSize.Size = new System.Drawing.Size(100, 20);
      this.txtSize.TabIndex = 6;
      this.txtSize.Text = "128";
      this.txtSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      // 
      // label2
      // 
      this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(3, 121);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(63, 13);
      this.label2.TabIndex = 5;
      this.label2.Text = "ThumbSize:";
      // 
      // ControlSampleFieldGeometry
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.tableLayoutPanel1);
      this.Name = "ControlSampleFieldGeometry";
      this.Size = new System.Drawing.Size(547, 296);
      ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
      this.groupBox1.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.thumbGeometry)).EndInit();
      this.tableLayoutPanel1.ResumeLayout(false);
      this.panel1.ResumeLayout(false);
      this.panel1.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.DataGridView grid;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox txtId;
    private System.Windows.Forms.PictureBox thumbGeometry;
    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.TextBox txtSize;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.DataGridViewTextBoxColumn colSpatialId;
    private System.Windows.Forms.DataGridViewTextBoxColumn colDomainType;
    private System.Windows.Forms.DataGridViewTextBoxColumn colSampledValue;
    private System.Windows.Forms.DataGridViewTextBoxColumn colMinValue;
    private System.Windows.Forms.DataGridViewTextBoxColumn colMaxValue;
  }
}
