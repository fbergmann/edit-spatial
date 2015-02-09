using System.ComponentModel;
using System.Windows.Forms;

namespace LibEditSpatial.Controls
{
  partial class CtrlTime
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
      this.label1 = new System.Windows.Forms.Label();
      this.txtTime = new System.Windows.Forms.TextBox();
      this.txtInitialStep = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.txtMinStep = new System.Windows.Forms.TextBox();
      this.label3 = new System.Windows.Forms.Label();
      this.txtPlotStep = new System.Windows.Forms.TextBox();
      this.label4 = new System.Windows.Forms.Label();
      this.txtMaxStep = new System.Windows.Forms.TextBox();
      this.label5 = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(40, 17);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(33, 13);
      this.label1.TabIndex = 0;
      this.label1.Text = "Time:";
      // 
      // txtTime
      // 
      this.txtTime.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtTime.Location = new System.Drawing.Point(79, 14);
      this.txtTime.Name = "txtTime";
      this.txtTime.Size = new System.Drawing.Size(100, 20);
      this.txtTime.TabIndex = 1;
      this.txtTime.Text = "10";
      this.txtTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.txtTime.TextChanged += new System.EventHandler(this.OnEndTimeChanged);
      // 
      // txtInitialStep
      // 
      this.txtInitialStep.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtInitialStep.Location = new System.Drawing.Point(79, 40);
      this.txtInitialStep.Name = "txtInitialStep";
      this.txtInitialStep.Size = new System.Drawing.Size(100, 20);
      this.txtInitialStep.TabIndex = 3;
      this.txtInitialStep.Text = "0.01";
      this.txtInitialStep.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.txtInitialStep.TextChanged += new System.EventHandler(this.OnInitialStepChanged);
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(14, 43);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(59, 13);
      this.label2.TabIndex = 2;
      this.label2.Text = "InitialStep: ";
      // 
      // txtMinStep
      // 
      this.txtMinStep.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtMinStep.Location = new System.Drawing.Point(79, 66);
      this.txtMinStep.Name = "txtMinStep";
      this.txtMinStep.Size = new System.Drawing.Size(100, 20);
      this.txtMinStep.TabIndex = 5;
      this.txtMinStep.Text = "1e-6";
      this.txtMinStep.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.txtMinStep.TextChanged += new System.EventHandler(this.OnMinStepChanged);
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(21, 69);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(52, 13);
      this.label3.TabIndex = 4;
      this.label3.Text = "Min Step:";
      // 
      // txtPlotStep
      // 
      this.txtPlotStep.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtPlotStep.Location = new System.Drawing.Point(79, 118);
      this.txtPlotStep.Name = "txtPlotStep";
      this.txtPlotStep.Size = new System.Drawing.Size(100, 20);
      this.txtPlotStep.TabIndex = 7;
      this.txtPlotStep.Text = "0.01";
      this.txtPlotStep.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.txtPlotStep.TextChanged += new System.EventHandler(this.OnPlotStepChanged);
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(17, 121);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(56, 13);
      this.label4.TabIndex = 6;
      this.label4.Text = "Plot Step: ";
      // 
      // txtMaxStep
      // 
      this.txtMaxStep.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtMaxStep.Location = new System.Drawing.Point(79, 92);
      this.txtMaxStep.Name = "txtMaxStep";
      this.txtMaxStep.Size = new System.Drawing.Size(100, 20);
      this.txtMaxStep.TabIndex = 9;
      this.txtMaxStep.Text = "0.01";
      this.txtMaxStep.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.txtMaxStep.TextChanged += new System.EventHandler(this.OnMaxStepChanged);
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(18, 95);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(55, 13);
      this.label5.TabIndex = 8;
      this.label5.Text = "Max Step:";
      // 
      // CtrlTime
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.txtMaxStep);
      this.Controls.Add(this.label5);
      this.Controls.Add(this.txtPlotStep);
      this.Controls.Add(this.label4);
      this.Controls.Add(this.txtMinStep);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.txtInitialStep);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.txtTime);
      this.Controls.Add(this.label1);
      this.Name = "CtrlTime";
      this.Size = new System.Drawing.Size(201, 156);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private Label label1;
    private TextBox txtTime;
    private TextBox txtInitialStep;
    private Label label2;
    private TextBox txtMinStep;
    private Label label3;
    private TextBox txtPlotStep;
    private Label label4;
    private TextBox txtMaxStep;
    private Label label5;
  }
}
