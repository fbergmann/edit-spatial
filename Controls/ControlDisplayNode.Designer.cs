namespace EditSpatial.Controls
{
  partial class ControlDisplayNode
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
      this.controlText1 = new EditSpatial.Controls.ControlText();
      this.SuspendLayout();
      // 
      // controlText1
      // 
      this.controlText1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.controlText1.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.controlText1.Location = new System.Drawing.Point(4, 4);
      this.controlText1.Multiline = true;
      this.controlText1.Name = "controlText1";
      this.controlText1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
      this.controlText1.Size = new System.Drawing.Size(378, 323);
      this.controlText1.TabIndex = 0;
      this.controlText1.WordWrap = false;
      // 
      // ControlDisplayNode
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.controlText1);
      this.Name = "ControlDisplayNode";
      this.Padding = new System.Windows.Forms.Padding(4);
      this.Size = new System.Drawing.Size(386, 331);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private ControlText controlText1;

  }
}
