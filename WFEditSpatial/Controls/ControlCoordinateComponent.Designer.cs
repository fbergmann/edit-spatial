using System.ComponentModel;
using System.Windows.Forms;

namespace EditSpatial.Controls
{
  partial class ControlCoordinateComponent
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
      this.txtId = new System.Windows.Forms.TextBox();
      this.txtType = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.txtIndex = new System.Windows.Forms.TextBox();
      this.label3 = new System.Windows.Forms.Label();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.txtMinValue = new System.Windows.Forms.TextBox();
      this.label5 = new System.Windows.Forms.Label();
      this.txtMinId = new System.Windows.Forms.TextBox();
      this.label6 = new System.Windows.Forms.Label();
      this.groupBox2 = new System.Windows.Forms.GroupBox();
      this.txtMaxValue = new System.Windows.Forms.TextBox();
      this.label4 = new System.Windows.Forms.Label();
      this.txtMaxId = new System.Windows.Forms.TextBox();
      this.label7 = new System.Windows.Forms.Label();
      this.txtUnit = new System.Windows.Forms.TextBox();
      this.label8 = new System.Windows.Forms.Label();
      this.groupBox1.SuspendLayout();
      this.groupBox2.SuspendLayout();
      this.SuspendLayout();
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(14, 13);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(22, 13);
      this.label1.TabIndex = 0;
      this.label1.Text = "Id: ";
      // 
      // txtId
      // 
      this.txtId.Location = new System.Drawing.Point(42, 10);
      this.txtId.Name = "txtId";
      this.txtId.Size = new System.Drawing.Size(123, 20);
      this.txtId.TabIndex = 1;
      // 
      // txtType
      // 
      this.txtType.Location = new System.Drawing.Point(220, 10);
      this.txtType.Name = "txtType";
      this.txtType.Size = new System.Drawing.Size(177, 20);
      this.txtType.TabIndex = 3;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(177, 13);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(37, 13);
      this.label2.TabIndex = 2;
      this.label2.Text = "Type: ";
      // 
      // txtIndex
      // 
      this.txtIndex.Location = new System.Drawing.Point(220, 36);
      this.txtIndex.Name = "txtIndex";
      this.txtIndex.Size = new System.Drawing.Size(177, 20);
      this.txtIndex.TabIndex = 5;
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(175, 39);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(39, 13);
      this.label3.TabIndex = 4;
      this.label3.Text = "Index: ";
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.txtMinValue);
      this.groupBox1.Controls.Add(this.label5);
      this.groupBox1.Controls.Add(this.txtMinId);
      this.groupBox1.Controls.Add(this.label6);
      this.groupBox1.Location = new System.Drawing.Point(3, 62);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(487, 54);
      this.groupBox1.TabIndex = 6;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = " Min: ";
      // 
      // txtMinValue
      // 
      this.txtMinValue.Location = new System.Drawing.Point(217, 19);
      this.txtMinValue.Name = "txtMinValue";
      this.txtMinValue.Size = new System.Drawing.Size(177, 20);
      this.txtMinValue.TabIndex = 9;
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(168, 22);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(43, 13);
      this.label5.TabIndex = 8;
      this.label5.Text = " Value: ";
      // 
      // txtMinId
      // 
      this.txtMinId.Location = new System.Drawing.Point(39, 19);
      this.txtMinId.Name = "txtMinId";
      this.txtMinId.Size = new System.Drawing.Size(123, 20);
      this.txtMinId.TabIndex = 7;
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Location = new System.Drawing.Point(11, 22);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(22, 13);
      this.label6.TabIndex = 6;
      this.label6.Text = "Id: ";
      // 
      // groupBox2
      // 
      this.groupBox2.Controls.Add(this.txtMaxValue);
      this.groupBox2.Controls.Add(this.label4);
      this.groupBox2.Controls.Add(this.txtMaxId);
      this.groupBox2.Controls.Add(this.label7);
      this.groupBox2.Location = new System.Drawing.Point(3, 122);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Size = new System.Drawing.Size(487, 54);
      this.groupBox2.TabIndex = 10;
      this.groupBox2.TabStop = false;
      this.groupBox2.Text = " Max: ";
      // 
      // txtMaxValue
      // 
      this.txtMaxValue.Location = new System.Drawing.Point(217, 19);
      this.txtMaxValue.Name = "txtMaxValue";
      this.txtMaxValue.Size = new System.Drawing.Size(177, 20);
      this.txtMaxValue.TabIndex = 9;
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(168, 22);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(43, 13);
      this.label4.TabIndex = 8;
      this.label4.Text = " Value: ";
      // 
      // txtMaxId
      // 
      this.txtMaxId.Location = new System.Drawing.Point(39, 19);
      this.txtMaxId.Name = "txtMaxId";
      this.txtMaxId.Size = new System.Drawing.Size(123, 20);
      this.txtMaxId.TabIndex = 7;
      // 
      // label7
      // 
      this.label7.AutoSize = true;
      this.label7.Location = new System.Drawing.Point(11, 22);
      this.label7.Name = "label7";
      this.label7.Size = new System.Drawing.Size(22, 13);
      this.label7.TabIndex = 6;
      this.label7.Text = "Id: ";
      // 
      // txtUnit
      // 
      this.txtUnit.Location = new System.Drawing.Point(42, 36);
      this.txtUnit.Name = "txtUnit";
      this.txtUnit.Size = new System.Drawing.Size(123, 20);
      this.txtUnit.TabIndex = 12;
      // 
      // label8
      // 
      this.label8.AutoSize = true;
      this.label8.Location = new System.Drawing.Point(4, 39);
      this.label8.Name = "label8";
      this.label8.Size = new System.Drawing.Size(32, 13);
      this.label8.TabIndex = 11;
      this.label8.Text = "Unit: ";
      // 
      // ControlCoordinateComponent
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.Controls.Add(this.txtUnit);
      this.Controls.Add(this.label8);
      this.Controls.Add(this.groupBox2);
      this.Controls.Add(this.groupBox1);
      this.Controls.Add(this.txtIndex);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.txtType);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.txtId);
      this.Controls.Add(this.label1);
      this.MinimumSize = new System.Drawing.Size(496, 183);
      this.Name = "ControlCoordinateComponent";
      this.Size = new System.Drawing.Size(494, 181);
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.groupBox2.ResumeLayout(false);
      this.groupBox2.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private Label label1;
    private TextBox txtId;
    private TextBox txtType;
    private Label label2;
    private TextBox txtIndex;
    private Label label3;
    private GroupBox groupBox1;
    private TextBox txtMinValue;
    private Label label5;
    private TextBox txtMinId;
    private Label label6;
    private GroupBox groupBox2;
    private TextBox txtMaxValue;
    private Label label4;
    private TextBox txtMaxId;
    private Label label7;
    private TextBox txtUnit;
    private Label label8;
  }
}
