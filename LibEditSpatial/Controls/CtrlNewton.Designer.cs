namespace WFDuneRunner.Controls
{
  partial class CtrlNewton
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
      this.txtMaxIterations = new System.Windows.Forms.TextBox();
      this.label5 = new System.Windows.Forms.Label();
      this.txtAbsoluteLimit = new System.Windows.Forms.TextBox();
      this.label4 = new System.Windows.Forms.Label();
      this.txtLineSearchMaxIterations = new System.Windows.Forms.TextBox();
      this.label3 = new System.Windows.Forms.Label();
      this.txtReassembleThreshold = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.txtLinearVerbosity = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.txtReduction = new System.Windows.Forms.TextBox();
      this.label6 = new System.Windows.Forms.Label();
      this.txtLinearReduction = new System.Windows.Forms.TextBox();
      this.label7 = new System.Windows.Forms.Label();
      this.txtLineSearchDampingFactor = new System.Windows.Forms.TextBox();
      this.label8 = new System.Windows.Forms.Label();
      this.txtVerbosity = new System.Windows.Forms.TextBox();
      this.label9 = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // txtMaxIterations
      // 
      this.txtMaxIterations.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtMaxIterations.Location = new System.Drawing.Point(145, 90);
      this.txtMaxIterations.Name = "txtMaxIterations";
      this.txtMaxIterations.Size = new System.Drawing.Size(162, 20);
      this.txtMaxIterations.TabIndex = 19;
      this.txtMaxIterations.Text = "30";
      this.txtMaxIterations.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.txtMaxIterations.TextChanged += new System.EventHandler(this.txtMaxIterations_TextChanged);
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(66, 93);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(73, 13);
      this.label5.TabIndex = 18;
      this.label5.Text = "MaxIterations:";
      // 
      // txtAbsoluteLimit
      // 
      this.txtAbsoluteLimit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtAbsoluteLimit.Location = new System.Drawing.Point(145, 116);
      this.txtAbsoluteLimit.Name = "txtAbsoluteLimit";
      this.txtAbsoluteLimit.Size = new System.Drawing.Size(162, 20);
      this.txtAbsoluteLimit.TabIndex = 17;
      this.txtAbsoluteLimit.Text = "1e-8";
      this.txtAbsoluteLimit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.txtAbsoluteLimit.TextChanged += new System.EventHandler(this.txtAbsoluteLimit_TextChanged);
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(67, 119);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(72, 13);
      this.label4.TabIndex = 16;
      this.label4.Text = "AbsoluteLimit:";
      // 
      // txtLineSearchMaxIterations
      // 
      this.txtLineSearchMaxIterations.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtLineSearchMaxIterations.Location = new System.Drawing.Point(145, 64);
      this.txtLineSearchMaxIterations.Name = "txtLineSearchMaxIterations";
      this.txtLineSearchMaxIterations.Size = new System.Drawing.Size(162, 20);
      this.txtLineSearchMaxIterations.TabIndex = 15;
      this.txtLineSearchMaxIterations.Text = "5";
      this.txtLineSearchMaxIterations.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.txtLineSearchMaxIterations.TextChanged += new System.EventHandler(this.txtLineSearchMaxIterations_TextChanged);
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(12, 67);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(127, 13);
      this.label3.TabIndex = 14;
      this.label3.Text = "LineSearchMaxIterations:";
      // 
      // txtReassembleThreshold
      // 
      this.txtReassembleThreshold.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtReassembleThreshold.Location = new System.Drawing.Point(145, 38);
      this.txtReassembleThreshold.Name = "txtReassembleThreshold";
      this.txtReassembleThreshold.Size = new System.Drawing.Size(162, 20);
      this.txtReassembleThreshold.TabIndex = 13;
      this.txtReassembleThreshold.Text = "0";
      this.txtReassembleThreshold.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.txtReassembleThreshold.TextChanged += new System.EventHandler(this.txtReassembleThreshold_TextChanged);
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(24, 41);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(115, 13);
      this.label2.TabIndex = 12;
      this.label2.Text = "ReassembleThreshold:";
      // 
      // txtLinearVerbosity
      // 
      this.txtLinearVerbosity.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtLinearVerbosity.Location = new System.Drawing.Point(145, 12);
      this.txtLinearVerbosity.Name = "txtLinearVerbosity";
      this.txtLinearVerbosity.Size = new System.Drawing.Size(162, 20);
      this.txtLinearVerbosity.TabIndex = 11;
      this.txtLinearVerbosity.Text = "0";
      this.txtLinearVerbosity.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.txtLinearVerbosity.TextChanged += new System.EventHandler(this.txtLinearVerbosity_TextChanged);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(57, 15);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(82, 13);
      this.label1.TabIndex = 10;
      this.label1.Text = "LinearVerbosity:";
      // 
      // txtReduction
      // 
      this.txtReduction.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtReduction.Location = new System.Drawing.Point(145, 145);
      this.txtReduction.Name = "txtReduction";
      this.txtReduction.Size = new System.Drawing.Size(162, 20);
      this.txtReduction.TabIndex = 21;
      this.txtReduction.Text = "1e-8";
      this.txtReduction.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.txtReduction.TextChanged += new System.EventHandler(this.txtReduction_TextChanged);
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Location = new System.Drawing.Point(80, 148);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(59, 13);
      this.label6.TabIndex = 20;
      this.label6.Text = "Reduction:";
      // 
      // txtLinearReduction
      // 
      this.txtLinearReduction.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtLinearReduction.Location = new System.Drawing.Point(145, 171);
      this.txtLinearReduction.Name = "txtLinearReduction";
      this.txtLinearReduction.Size = new System.Drawing.Size(162, 20);
      this.txtLinearReduction.TabIndex = 23;
      this.txtLinearReduction.Text = "1e-4";
      this.txtLinearReduction.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.txtLinearReduction.TextChanged += new System.EventHandler(this.txtLinearReduction_TextChanged);
      // 
      // label7
      // 
      this.label7.AutoSize = true;
      this.label7.Location = new System.Drawing.Point(51, 171);
      this.label7.Name = "label7";
      this.label7.Size = new System.Drawing.Size(88, 13);
      this.label7.TabIndex = 22;
      this.label7.Text = "LinearReduction:";
      // 
      // txtLineSearchDampingFactor
      // 
      this.txtLineSearchDampingFactor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtLineSearchDampingFactor.Location = new System.Drawing.Point(145, 197);
      this.txtLineSearchDampingFactor.Name = "txtLineSearchDampingFactor";
      this.txtLineSearchDampingFactor.Size = new System.Drawing.Size(162, 20);
      this.txtLineSearchDampingFactor.TabIndex = 25;
      this.txtLineSearchDampingFactor.Text = "0.5";
      this.txtLineSearchDampingFactor.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.txtLineSearchDampingFactor.TextChanged += new System.EventHandler(this.txtLineSearchDampingFactor_TextChanged);
      // 
      // label8
      // 
      this.label8.AutoSize = true;
      this.label8.Location = new System.Drawing.Point(3, 197);
      this.label8.Name = "label8";
      this.label8.Size = new System.Drawing.Size(136, 13);
      this.label8.TabIndex = 24;
      this.label8.Text = "LineSearchDampingFactor:";
      // 
      // txtVerbosity
      // 
      this.txtVerbosity.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtVerbosity.Location = new System.Drawing.Point(145, 223);
      this.txtVerbosity.Name = "txtVerbosity";
      this.txtVerbosity.Size = new System.Drawing.Size(162, 20);
      this.txtVerbosity.TabIndex = 27;
      this.txtVerbosity.Text = "0";
      this.txtVerbosity.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.txtVerbosity.TextChanged += new System.EventHandler(this.txtVerbosity_TextChanged);
      // 
      // label9
      // 
      this.label9.AutoSize = true;
      this.label9.Location = new System.Drawing.Point(86, 226);
      this.label9.Name = "label9";
      this.label9.Size = new System.Drawing.Size(53, 13);
      this.label9.TabIndex = 26;
      this.label9.Text = "Verbosity:";
      // 
      // CtrlNewton
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.txtVerbosity);
      this.Controls.Add(this.label9);
      this.Controls.Add(this.txtLineSearchDampingFactor);
      this.Controls.Add(this.label8);
      this.Controls.Add(this.txtLinearReduction);
      this.Controls.Add(this.label7);
      this.Controls.Add(this.txtReduction);
      this.Controls.Add(this.label6);
      this.Controls.Add(this.txtMaxIterations);
      this.Controls.Add(this.label5);
      this.Controls.Add(this.txtAbsoluteLimit);
      this.Controls.Add(this.label4);
      this.Controls.Add(this.txtLineSearchMaxIterations);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.txtReassembleThreshold);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.txtLinearVerbosity);
      this.Controls.Add(this.label1);
      this.Name = "CtrlNewton";
      this.Size = new System.Drawing.Size(323, 259);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox txtMaxIterations;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.TextBox txtAbsoluteLimit;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.TextBox txtLineSearchMaxIterations;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.TextBox txtReassembleThreshold;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TextBox txtLinearVerbosity;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox txtReduction;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.TextBox txtLinearReduction;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.TextBox txtLineSearchDampingFactor;
    private System.Windows.Forms.Label label8;
    private System.Windows.Forms.TextBox txtVerbosity;
    private System.Windows.Forms.Label label9;
  }
}
