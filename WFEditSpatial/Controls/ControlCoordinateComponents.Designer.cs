namespace EditSpatial.Controls
{
  partial class ControlCoordinateComponents
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
      this.label1 = new System.Windows.Forms.Label();
      this.txtCoordSystem = new System.Windows.Forms.TextBox();
      this.tblLayout = new System.Windows.Forms.TableLayoutPanel();
      this.panel1 = new System.Windows.Forms.Panel();
      this.tblLayout.SuspendLayout();
      this.panel1.SuspendLayout();
      this.SuspendLayout();
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(4, 9);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(98, 13);
      this.label1.TabIndex = 0;
      this.label1.Text = "Coordinate System:";
      // 
      // txtCoordSystem
      // 
      this.txtCoordSystem.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtCoordSystem.Location = new System.Drawing.Point(108, 6);
      this.txtCoordSystem.Name = "txtCoordSystem";
      this.txtCoordSystem.Size = new System.Drawing.Size(370, 20);
      this.txtCoordSystem.TabIndex = 1;
      // 
      // tblLayout
      // 
      this.tblLayout.ColumnCount = 1;
      this.tblLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
      this.tblLayout.Controls.Add(this.panel1, 0, 0);
      this.tblLayout.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tblLayout.Location = new System.Drawing.Point(0, 0);
      this.tblLayout.Name = "tblLayout";
      this.tblLayout.RowCount = 1;
      this.tblLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
      this.tblLayout.Size = new System.Drawing.Size(487, 362);
      this.tblLayout.TabIndex = 2;
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.txtCoordSystem);
      this.panel1.Controls.Add(this.label1);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel1.Location = new System.Drawing.Point(3, 3);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(481, 356);
      this.panel1.TabIndex = 0;
      // 
      // ControlCoordinateComponents
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.AutoScroll = true;
      this.Controls.Add(this.tblLayout);
      this.Name = "ControlCoordinateComponents";
      this.Size = new System.Drawing.Size(487, 362);
      this.tblLayout.ResumeLayout(false);
      this.panel1.ResumeLayout(false);
      this.panel1.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox txtCoordSystem;
    private System.Windows.Forms.TableLayoutPanel tblLayout;
    private System.Windows.Forms.Panel panel1;
  }
}
