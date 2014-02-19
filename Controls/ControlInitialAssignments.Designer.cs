namespace EditSpatial.Controls
{
  partial class ControlInitialAssignments
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
      this.colSymbol = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colMath = new System.Windows.Forms.DataGridViewTextBoxColumn();
      ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
      this.SuspendLayout();
      // 
      // grid
      // 
      this.grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colSymbol,
            this.colMath});
      this.grid.Dock = System.Windows.Forms.DockStyle.Fill;
      this.grid.Location = new System.Drawing.Point(0, 0);
      this.grid.Name = "grid";
      this.grid.Size = new System.Drawing.Size(484, 370);
      this.grid.TabIndex = 0;
      // 
      // colSymbol
      // 
      this.colSymbol.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
      this.colSymbol.HeaderText = "Symbol";
      this.colSymbol.Name = "colSymbol";
      this.colSymbol.Width = 64;
      // 
      // colMath
      // 
      this.colMath.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
      this.colMath.HeaderText = "Math";
      this.colMath.Name = "colMath";
      // 
      // ControlInitialAssignments
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.grid);
      this.Name = "ControlInitialAssignments";
      this.Size = new System.Drawing.Size(484, 370);
      ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.DataGridView grid;
    private System.Windows.Forms.DataGridViewTextBoxColumn colSymbol;
    private System.Windows.Forms.DataGridViewTextBoxColumn colMath;
  }
}
