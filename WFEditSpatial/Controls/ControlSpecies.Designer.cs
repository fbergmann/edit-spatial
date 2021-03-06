﻿using System.ComponentModel;
using System.Windows.Forms;

namespace EditSpatial.Controls
{
  partial class ControlSpecies
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
      this.grid = new System.Windows.Forms.DataGridView();
      this.colId = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colComp = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colBoundary = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colIsSpatial = new System.Windows.Forms.DataGridViewTextBoxColumn();
      ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
      this.SuspendLayout();
      // 
      // grid
      // 
      this.grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colId,
            this.colName,
            this.colValue,
            this.colComp,
            this.colBoundary,
            this.colIsSpatial});
      this.grid.Dock = System.Windows.Forms.DockStyle.Fill;
      this.grid.Location = new System.Drawing.Point(0, 0);
      this.grid.Name = "grid";
      this.grid.Size = new System.Drawing.Size(675, 371);
      this.grid.TabIndex = 0;
      this.grid.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.OnRowsAdded);
      this.grid.UserDeletedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.OnUserDeletedRow);
      // 
      // colId
      // 
      this.colId.HeaderText = "Id";
      this.colId.Name = "colId";
      // 
      // colName
      // 
      this.colName.HeaderText = "Name";
      this.colName.Name = "colName";
      // 
      // colValue
      // 
      this.colValue.HeaderText = "Conc";
      this.colValue.Name = "colValue";
      // 
      // colComp
      // 
      this.colComp.HeaderText = "Compartment";
      this.colComp.Name = "colComp";
      // 
      // colBoundary
      // 
      this.colBoundary.HeaderText = "Boundary";
      this.colBoundary.Name = "colBoundary";
      // 
      // colIsSpatial
      // 
      this.colIsSpatial.HeaderText = "IsSpatial";
      this.colIsSpatial.Name = "colIsSpatial";
      // 
      // ControlSpecies
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.grid);
      this.Name = "ControlSpecies";
      this.Size = new System.Drawing.Size(675, 371);
      ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private DataGridView grid;
    private DataGridViewTextBoxColumn colId;
    private DataGridViewTextBoxColumn colName;
    private DataGridViewTextBoxColumn colValue;
    private DataGridViewTextBoxColumn colComp;
    private DataGridViewTextBoxColumn colBoundary;
    private DataGridViewTextBoxColumn colIsSpatial;
  }
}
