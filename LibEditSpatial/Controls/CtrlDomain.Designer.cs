namespace WFDuneRunner.Controls
{
  partial class CtrlDomain
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
      this.grpDimensions = new System.Windows.Forms.GroupBox();
      this.txtDepth = new System.Windows.Forms.TextBox();
      this.label3 = new System.Windows.Forms.Label();
      this.txtHeight = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.txtWidth = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.txtRefinement = new System.Windows.Forms.TextBox();
      this.label7 = new System.Windows.Forms.Label();
      this.txtGridZ = new System.Windows.Forms.TextBox();
      this.label4 = new System.Windows.Forms.Label();
      this.txtGridY = new System.Windows.Forms.TextBox();
      this.label5 = new System.Windows.Forms.Label();
      this.txtGridX = new System.Windows.Forms.TextBox();
      this.label6 = new System.Windows.Forms.Label();
      this.groupBox2 = new System.Windows.Forms.GroupBox();
      this.cmdEdit = new System.Windows.Forms.Button();
      this.cmdLoad = new System.Windows.Forms.Button();
      this.txtMax = new System.Windows.Forms.TextBox();
      this.label8 = new System.Windows.Forms.Label();
      this.txtMin = new System.Windows.Forms.TextBox();
      this.label9 = new System.Windows.Forms.Label();
      this.txtFile = new System.Windows.Forms.TextBox();
      this.label10 = new System.Windows.Forms.Label();
      this.grpDimensions.SuspendLayout();
      this.groupBox1.SuspendLayout();
      this.groupBox2.SuspendLayout();
      this.SuspendLayout();
      // 
      // grpDimensions
      // 
      this.grpDimensions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.grpDimensions.Controls.Add(this.txtDepth);
      this.grpDimensions.Controls.Add(this.label3);
      this.grpDimensions.Controls.Add(this.txtHeight);
      this.grpDimensions.Controls.Add(this.label2);
      this.grpDimensions.Controls.Add(this.txtWidth);
      this.grpDimensions.Controls.Add(this.label1);
      this.grpDimensions.Location = new System.Drawing.Point(13, 12);
      this.grpDimensions.Name = "grpDimensions";
      this.grpDimensions.Size = new System.Drawing.Size(159, 106);
      this.grpDimensions.TabIndex = 0;
      this.grpDimensions.TabStop = false;
      this.grpDimensions.Text = " Dimensions: ";
      // 
      // txtDepth
      // 
      this.txtDepth.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtDepth.Location = new System.Drawing.Point(50, 71);
      this.txtDepth.Name = "txtDepth";
      this.txtDepth.Size = new System.Drawing.Size(97, 20);
      this.txtDepth.TabIndex = 7;
      this.txtDepth.Text = "1";
      this.txtDepth.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.txtDepth.TextChanged += new System.EventHandler(this.OnDepthChanged);
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(8, 74);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(39, 13);
      this.label3.TabIndex = 6;
      this.label3.Text = "Depth:";
      // 
      // txtHeight
      // 
      this.txtHeight.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtHeight.Location = new System.Drawing.Point(50, 45);
      this.txtHeight.Name = "txtHeight";
      this.txtHeight.Size = new System.Drawing.Size(97, 20);
      this.txtHeight.TabIndex = 5;
      this.txtHeight.Text = "100";
      this.txtHeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.txtHeight.TextChanged += new System.EventHandler(this.OnHeightChanged);
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(6, 48);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(41, 13);
      this.label2.TabIndex = 4;
      this.label2.Text = "Height:";
      // 
      // txtWidth
      // 
      this.txtWidth.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtWidth.Location = new System.Drawing.Point(50, 19);
      this.txtWidth.Name = "txtWidth";
      this.txtWidth.Size = new System.Drawing.Size(97, 20);
      this.txtWidth.TabIndex = 3;
      this.txtWidth.Text = "100";
      this.txtWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.txtWidth.TextChanged += new System.EventHandler(this.OnWidthChanged);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(9, 22);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(38, 13);
      this.label1.TabIndex = 2;
      this.label1.Text = "Width:";
      // 
      // groupBox1
      // 
      this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.groupBox1.Controls.Add(this.txtRefinement);
      this.groupBox1.Controls.Add(this.label7);
      this.groupBox1.Controls.Add(this.txtGridZ);
      this.groupBox1.Controls.Add(this.label4);
      this.groupBox1.Controls.Add(this.txtGridY);
      this.groupBox1.Controls.Add(this.label5);
      this.groupBox1.Controls.Add(this.txtGridX);
      this.groupBox1.Controls.Add(this.label6);
      this.groupBox1.Location = new System.Drawing.Point(13, 124);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(159, 127);
      this.groupBox1.TabIndex = 8;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = " Grid: ";
      // 
      // txtRefinement
      // 
      this.txtRefinement.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtRefinement.Location = new System.Drawing.Point(50, 97);
      this.txtRefinement.Name = "txtRefinement";
      this.txtRefinement.Size = new System.Drawing.Size(97, 20);
      this.txtRefinement.TabIndex = 9;
      this.txtRefinement.Text = "0";
      this.txtRefinement.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.txtRefinement.TextChanged += new System.EventHandler(this.OnRefinementChanged);
      // 
      // label7
      // 
      this.label7.AutoSize = true;
      this.label7.Location = new System.Drawing.Point(3, 100);
      this.label7.Name = "label7";
      this.label7.Size = new System.Drawing.Size(41, 13);
      this.label7.TabIndex = 8;
      this.label7.Text = "Refine:";
      // 
      // txtGridZ
      // 
      this.txtGridZ.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtGridZ.Location = new System.Drawing.Point(50, 71);
      this.txtGridZ.Name = "txtGridZ";
      this.txtGridZ.Size = new System.Drawing.Size(97, 20);
      this.txtGridZ.TabIndex = 7;
      this.txtGridZ.Text = "1";
      this.txtGridZ.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.txtGridZ.TextChanged += new System.EventHandler(this.OnGridZChanged);
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(21, 74);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(23, 13);
      this.label4.TabIndex = 6;
      this.label4.Text = "nZ:";
      // 
      // txtGridY
      // 
      this.txtGridY.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtGridY.Location = new System.Drawing.Point(50, 45);
      this.txtGridY.Name = "txtGridY";
      this.txtGridY.Size = new System.Drawing.Size(97, 20);
      this.txtGridY.TabIndex = 5;
      this.txtGridY.Text = "64";
      this.txtGridY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.txtGridY.TextChanged += new System.EventHandler(this.OnGridYChanged);
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(21, 48);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(23, 13);
      this.label5.TabIndex = 4;
      this.label5.Text = "nY:";
      // 
      // txtGridX
      // 
      this.txtGridX.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtGridX.Location = new System.Drawing.Point(50, 19);
      this.txtGridX.Name = "txtGridX";
      this.txtGridX.Size = new System.Drawing.Size(97, 20);
      this.txtGridX.TabIndex = 3;
      this.txtGridX.Text = "64";
      this.txtGridX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.txtGridX.TextChanged += new System.EventHandler(this.OnGridXChanged);
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Location = new System.Drawing.Point(21, 22);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(23, 13);
      this.label6.TabIndex = 2;
      this.label6.Text = "nX:";
      // 
      // groupBox2
      // 
      this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.groupBox2.Controls.Add(this.cmdEdit);
      this.groupBox2.Controls.Add(this.cmdLoad);
      this.groupBox2.Controls.Add(this.txtMax);
      this.groupBox2.Controls.Add(this.label8);
      this.groupBox2.Controls.Add(this.txtMin);
      this.groupBox2.Controls.Add(this.label9);
      this.groupBox2.Controls.Add(this.txtFile);
      this.groupBox2.Controls.Add(this.label10);
      this.groupBox2.Location = new System.Drawing.Point(13, 257);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Size = new System.Drawing.Size(159, 157);
      this.groupBox2.TabIndex = 8;
      this.groupBox2.TabStop = false;
      this.groupBox2.Text = " Overwrite Geometry:  ";
      // 
      // cmdEdit
      // 
      this.cmdEdit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.cmdEdit.Location = new System.Drawing.Point(50, 126);
      this.cmdEdit.Name = "cmdEdit";
      this.cmdEdit.Size = new System.Drawing.Size(97, 23);
      this.cmdEdit.TabIndex = 9;
      this.cmdEdit.Text = "&Edit";
      this.cmdEdit.UseVisualStyleBackColor = true;
      this.cmdEdit.Click += new System.EventHandler(this.OnEditClicked);
      // 
      // cmdLoad
      // 
      this.cmdLoad.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.cmdLoad.Location = new System.Drawing.Point(50, 97);
      this.cmdLoad.Name = "cmdLoad";
      this.cmdLoad.Size = new System.Drawing.Size(97, 23);
      this.cmdLoad.TabIndex = 8;
      this.cmdLoad.Text = "&Load";
      this.cmdLoad.UseVisualStyleBackColor = true;
      this.cmdLoad.Click += new System.EventHandler(this.OnLoadClicked);
      // 
      // txtMax
      // 
      this.txtMax.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtMax.Location = new System.Drawing.Point(50, 71);
      this.txtMax.Name = "txtMax";
      this.txtMax.Size = new System.Drawing.Size(97, 20);
      this.txtMax.TabIndex = 7;
      this.txtMax.Text = "10";
      this.txtMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.txtMax.TextChanged += new System.EventHandler(this.OnMaxChanged);
      // 
      // label8
      // 
      this.label8.AutoSize = true;
      this.label8.Location = new System.Drawing.Point(14, 74);
      this.label8.Name = "label8";
      this.label8.Size = new System.Drawing.Size(30, 13);
      this.label8.TabIndex = 6;
      this.label8.Text = "Max:";
      // 
      // txtMin
      // 
      this.txtMin.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtMin.Location = new System.Drawing.Point(50, 45);
      this.txtMin.Name = "txtMin";
      this.txtMin.Size = new System.Drawing.Size(97, 20);
      this.txtMin.TabIndex = 5;
      this.txtMin.Text = "10";
      this.txtMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.txtMin.TextChanged += new System.EventHandler(this.OnMinChanged);
      // 
      // label9
      // 
      this.label9.AutoSize = true;
      this.label9.Location = new System.Drawing.Point(17, 48);
      this.label9.Name = "label9";
      this.label9.Size = new System.Drawing.Size(27, 13);
      this.label9.TabIndex = 4;
      this.label9.Text = "Min:";
      // 
      // txtFile
      // 
      this.txtFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtFile.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
      this.txtFile.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystem;
      this.txtFile.Location = new System.Drawing.Point(50, 19);
      this.txtFile.Name = "txtFile";
      this.txtFile.Size = new System.Drawing.Size(97, 20);
      this.txtFile.TabIndex = 3;
      this.txtFile.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.txtFile.TextChanged += new System.EventHandler(this.OnFileChanged);
      // 
      // label10
      // 
      this.label10.AutoSize = true;
      this.label10.Location = new System.Drawing.Point(18, 22);
      this.label10.Name = "label10";
      this.label10.Size = new System.Drawing.Size(26, 13);
      this.label10.TabIndex = 2;
      this.label10.Text = "File:";
      // 
      // CtrlDomain
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.groupBox2);
      this.Controls.Add(this.groupBox1);
      this.Controls.Add(this.grpDimensions);
      this.Name = "CtrlDomain";
      this.Size = new System.Drawing.Size(182, 429);
      this.grpDimensions.ResumeLayout(false);
      this.grpDimensions.PerformLayout();
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.groupBox2.ResumeLayout(false);
      this.groupBox2.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.GroupBox grpDimensions;
    private System.Windows.Forms.TextBox txtDepth;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.TextBox txtHeight;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TextBox txtWidth;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.TextBox txtRefinement;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.TextBox txtGridZ;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.TextBox txtGridY;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.TextBox txtGridX;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.GroupBox groupBox2;
    private System.Windows.Forms.Button cmdEdit;
    private System.Windows.Forms.Button cmdLoad;
    private System.Windows.Forms.TextBox txtMax;
    private System.Windows.Forms.Label label8;
    private System.Windows.Forms.TextBox txtMin;
    private System.Windows.Forms.Label label9;
    private System.Windows.Forms.TextBox txtFile;
    private System.Windows.Forms.Label label10;
  }
}
