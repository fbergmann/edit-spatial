namespace WFDuneRunner.Controls
{
  partial class CtrlGlobal
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
      this.txtSubsampling = new System.Windows.Forms.TextBox();
      this.label5 = new System.Windows.Forms.Label();
      this.txtExplicitsolver = new System.Windows.Forms.TextBox();
      this.label4 = new System.Windows.Forms.Label();
      this.txtIntegrationorder = new System.Windows.Forms.TextBox();
      this.label3 = new System.Windows.Forms.Label();
      this.txtOverlap = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.txtVTKname = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.chkWriteVTK = new System.Windows.Forms.CheckBox();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.radImplicit = new System.Windows.Forms.RadioButton();
      this.radExplicit = new System.Windows.Forms.RadioButton();
      this.txtImplicitSovler = new System.Windows.Forms.TextBox();
      this.label6 = new System.Windows.Forms.Label();
      this.groupBox1.SuspendLayout();
      this.SuspendLayout();
      // 
      // txtSubsampling
      // 
      this.txtSubsampling.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtSubsampling.Location = new System.Drawing.Point(92, 104);
      this.txtSubsampling.Name = "txtSubsampling";
      this.txtSubsampling.Size = new System.Drawing.Size(100, 20);
      this.txtSubsampling.TabIndex = 19;
      this.txtSubsampling.Text = "2";
      this.txtSubsampling.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.txtSubsampling.TextChanged += new System.EventHandler(this.txtSubsampling_TextChanged);
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(18, 107);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(68, 13);
      this.label5.TabIndex = 18;
      this.label5.Text = "subsampling:";
      // 
      // txtExplicitsolver
      // 
      this.txtExplicitsolver.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtExplicitsolver.Location = new System.Drawing.Point(92, 184);
      this.txtExplicitsolver.Name = "txtExplicitsolver";
      this.txtExplicitsolver.Size = new System.Drawing.Size(100, 20);
      this.txtExplicitsolver.TabIndex = 17;
      this.txtExplicitsolver.Text = "RK4";
      this.txtExplicitsolver.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.txtExplicitsolver.TextChanged += new System.EventHandler(this.txtExplicitsolver_TextChanged);
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(18, 187);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(70, 13);
      this.label4.TabIndex = 16;
      this.label4.Text = "explicitsolver:";
      // 
      // txtIntegrationorder
      // 
      this.txtIntegrationorder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtIntegrationorder.Location = new System.Drawing.Point(92, 78);
      this.txtIntegrationorder.Name = "txtIntegrationorder";
      this.txtIntegrationorder.Size = new System.Drawing.Size(100, 20);
      this.txtIntegrationorder.TabIndex = 15;
      this.txtIntegrationorder.Text = "2";
      this.txtIntegrationorder.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.txtIntegrationorder.TextChanged += new System.EventHandler(this.txtIntegrationorder_TextChanged);
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(3, 81);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(83, 13);
      this.label3.TabIndex = 14;
      this.label3.Text = "integrationorder:";
      // 
      // txtOverlap
      // 
      this.txtOverlap.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtOverlap.Location = new System.Drawing.Point(92, 52);
      this.txtOverlap.Name = "txtOverlap";
      this.txtOverlap.Size = new System.Drawing.Size(100, 20);
      this.txtOverlap.TabIndex = 13;
      this.txtOverlap.Text = "1";
      this.txtOverlap.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.txtOverlap.TextChanged += new System.EventHandler(this.txtOverlap_TextChanged);
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(41, 55);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(45, 13);
      this.label2.TabIndex = 12;
      this.label2.Text = "overlap:";
      // 
      // txtVTKname
      // 
      this.txtVTKname.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtVTKname.Location = new System.Drawing.Point(92, 26);
      this.txtVTKname.Name = "txtVTKname";
      this.txtVTKname.Size = new System.Drawing.Size(100, 20);
      this.txtVTKname.TabIndex = 11;
      this.txtVTKname.Text = "name";
      this.txtVTKname.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.txtVTKname.TextChanged += new System.EventHandler(this.txtVTKname_TextChanged);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(31, 29);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(57, 13);
      this.label1.TabIndex = 10;
      this.label1.Text = "VTKname:";
      // 
      // chkWriteVTK
      // 
      this.chkWriteVTK.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.chkWriteVTK.AutoSize = true;
      this.chkWriteVTK.Checked = true;
      this.chkWriteVTK.CheckState = System.Windows.Forms.CheckState.Checked;
      this.chkWriteVTK.Location = new System.Drawing.Point(92, 3);
      this.chkWriteVTK.Name = "chkWriteVTK";
      this.chkWriteVTK.Size = new System.Drawing.Size(69, 17);
      this.chkWriteVTK.TabIndex = 20;
      this.chkWriteVTK.Text = "writeVTK";
      this.chkWriteVTK.UseVisualStyleBackColor = true;
      this.chkWriteVTK.CheckedChanged += new System.EventHandler(this.chkWriteVTK_CheckedChanged);
      // 
      // groupBox1
      // 
      this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.groupBox1.Controls.Add(this.radExplicit);
      this.groupBox1.Controls.Add(this.radImplicit);
      this.groupBox1.Location = new System.Drawing.Point(6, 130);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(186, 48);
      this.groupBox1.TabIndex = 21;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = " timestepping: ";
      // 
      // radImplicit
      // 
      this.radImplicit.AutoSize = true;
      this.radImplicit.Checked = true;
      this.radImplicit.Location = new System.Drawing.Point(6, 20);
      this.radImplicit.Name = "radImplicit";
      this.radImplicit.Size = new System.Drawing.Size(56, 17);
      this.radImplicit.TabIndex = 0;
      this.radImplicit.TabStop = true;
      this.radImplicit.Text = "implicit";
      this.radImplicit.UseVisualStyleBackColor = true;
      this.radImplicit.CheckedChanged += new System.EventHandler(this.radImplicit_CheckedChanged);
      // 
      // radExplicit
      // 
      this.radExplicit.AutoSize = true;
      this.radExplicit.Location = new System.Drawing.Point(68, 20);
      this.radExplicit.Name = "radExplicit";
      this.radExplicit.Size = new System.Drawing.Size(57, 17);
      this.radExplicit.TabIndex = 1;
      this.radExplicit.Text = "explicit";
      this.radExplicit.UseVisualStyleBackColor = true;
      this.radExplicit.CheckedChanged += new System.EventHandler(this.radExplicit_CheckedChanged);
      // 
      // txtImplicitSovler
      // 
      this.txtImplicitSovler.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtImplicitSovler.Location = new System.Drawing.Point(92, 210);
      this.txtImplicitSovler.Name = "txtImplicitSovler";
      this.txtImplicitSovler.Size = new System.Drawing.Size(100, 20);
      this.txtImplicitSovler.TabIndex = 23;
      this.txtImplicitSovler.Text = "Alexander2";
      this.txtImplicitSovler.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.txtImplicitSovler.TextChanged += new System.EventHandler(this.txtImplicitSovler_TextChanged);
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Location = new System.Drawing.Point(19, 213);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(69, 13);
      this.label6.TabIndex = 22;
      this.label6.Text = "implicitsolver:";
      // 
      // CtrlGlobal
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.txtImplicitSovler);
      this.Controls.Add(this.label6);
      this.Controls.Add(this.groupBox1);
      this.Controls.Add(this.chkWriteVTK);
      this.Controls.Add(this.txtSubsampling);
      this.Controls.Add(this.label5);
      this.Controls.Add(this.txtExplicitsolver);
      this.Controls.Add(this.label4);
      this.Controls.Add(this.txtIntegrationorder);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.txtOverlap);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.txtVTKname);
      this.Controls.Add(this.label1);
      this.Name = "CtrlGlobal";
      this.Size = new System.Drawing.Size(203, 242);
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox txtSubsampling;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.TextBox txtExplicitsolver;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.TextBox txtIntegrationorder;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.TextBox txtOverlap;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TextBox txtVTKname;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.CheckBox chkWriteVTK;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.RadioButton radExplicit;
    private System.Windows.Forms.RadioButton radImplicit;
    private System.Windows.Forms.TextBox txtImplicitSovler;
    private System.Windows.Forms.Label label6;
  }
}
