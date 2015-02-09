using System.ComponentModel;
using System.Windows.Forms;

namespace EditSpatial.Controls
{
  partial class ControlMapCompartments
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
      this.colCompartmentId = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colDomainType = new System.Windows.Forms.DataGridViewComboBoxColumn();
      ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
      this.SuspendLayout();
      // 
      // grid
      // 
      this.grid.AllowUserToAddRows = false;
      this.grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colCompartmentId,
            this.colDomainType});
      this.grid.Dock = System.Windows.Forms.DockStyle.Fill;
      this.grid.Location = new System.Drawing.Point(0, 0);
      this.grid.Name = "grid";
      this.grid.Size = new System.Drawing.Size(484, 370);
      this.grid.TabIndex = 0;
      // 
      // colCompartmentId
      // 
      this.colCompartmentId.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
      this.colCompartmentId.HeaderText = "Compartment Id";
      this.colCompartmentId.Name = "colCompartmentId";
      this.colCompartmentId.ReadOnly = true;
      this.colCompartmentId.Width = 95;
      // 
      // colDomainType
      // 
      this.colDomainType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
      this.colDomainType.HeaderText = "Domain Type";
      this.colDomainType.Name = "colDomainType";
      this.colDomainType.Resizable = System.Windows.Forms.DataGridViewTriState.True;
      this.colDomainType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
      // 
      // ControlMapCompartments
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.grid);
      this.Name = "ControlMapCompartments";
      this.Size = new System.Drawing.Size(484, 370);
      ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private DataGridView grid;
    private DataGridViewTextBoxColumn colCompartmentId;
    private DataGridViewComboBoxColumn colDomainType;
  }
}
