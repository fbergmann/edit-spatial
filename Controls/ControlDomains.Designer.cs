namespace EditSpatial.Controls
{
  partial class ControlDomains
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
      this.colTypes = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colInteriorPoints = new System.Windows.Forms.DataGridViewTextBoxColumn();
      ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
      this.SuspendLayout();
      // 
      // grid
      // 
      this.grid.AllowUserToAddRows = false;
      this.grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colSpatialId,
            this.colTypes,
            this.colInteriorPoints});
      this.grid.Dock = System.Windows.Forms.DockStyle.Fill;
      this.grid.Location = new System.Drawing.Point(0, 0);
      this.grid.Name = "grid";
      this.grid.Size = new System.Drawing.Size(484, 370);
      this.grid.TabIndex = 0;
      // 
      // colSpatialId
      // 
      this.colSpatialId.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
      this.colSpatialId.HeaderText = "Spatial Id";
      this.colSpatialId.Name = "colSpatialId";
      this.colSpatialId.Width = 74;
      // 
      // colTypes
      // 
      this.colTypes.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
      this.colTypes.HeaderText = "Domain Type";
      this.colTypes.Name = "colTypes";
      this.colTypes.Width = 93;
      // 
      // colInteriorPoints
      // 
      this.colInteriorPoints.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
      this.colInteriorPoints.HeaderText = "Interior Points";
      this.colInteriorPoints.Name = "colInteriorPoints";
      // 
      // ControlDomains
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.grid);
      this.Name = "ControlDomains";
      this.Size = new System.Drawing.Size(484, 370);
      ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.DataGridView grid;
    private System.Windows.Forms.DataGridViewTextBoxColumn colSpatialId;
    private System.Windows.Forms.DataGridViewTextBoxColumn colTypes;
    private System.Windows.Forms.DataGridViewTextBoxColumn colInteriorPoints;
  }
}
