namespace EditSpatial.Forms
{
  partial class FormPrepareDune
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

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
      this.panel1 = new System.Windows.Forms.Panel();
      this.cmdClose = new System.Windows.Forms.Button();
      this.panel2 = new System.Windows.Forms.Panel();
      this.splitContainer1 = new System.Windows.Forms.SplitContainer();
      this.panel3 = new System.Windows.Forms.Panel();
      this.cmdEditConfig = new System.Windows.Forms.Button();
      this.cmdRun = new System.Windows.Forms.Button();
      this.cmdCompile = new System.Windows.Forms.Button();
      this.cmdExportDune = new System.Windows.Forms.Button();
      this.txtName = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.cmdBrowseTarget = new System.Windows.Forms.Button();
      this.txtTargetDir = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.cmdCreateHost = new System.Windows.Forms.Button();
      this.textBox1 = new System.Windows.Forms.TextBox();
      this.tableLayoutPanel1.SuspendLayout();
      this.panel1.SuspendLayout();
      this.panel2.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      this.panel3.SuspendLayout();
      this.SuspendLayout();
      // 
      // tableLayoutPanel1
      // 
      this.tableLayoutPanel1.ColumnCount = 1;
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
      this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 0);
      this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
      this.tableLayoutPanel1.Name = "tableLayoutPanel1";
      this.tableLayoutPanel1.RowCount = 2;
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
      this.tableLayoutPanel1.Size = new System.Drawing.Size(624, 441);
      this.tableLayoutPanel1.TabIndex = 0;
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.cmdClose);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel1.Location = new System.Drawing.Point(3, 408);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(618, 30);
      this.panel1.TabIndex = 0;
      // 
      // cmdClose
      // 
      this.cmdClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.cmdClose.Location = new System.Drawing.Point(540, 4);
      this.cmdClose.Name = "cmdClose";
      this.cmdClose.Size = new System.Drawing.Size(75, 23);
      this.cmdClose.TabIndex = 0;
      this.cmdClose.Text = "&Close";
      this.cmdClose.UseVisualStyleBackColor = true;
      // 
      // panel2
      // 
      this.panel2.Controls.Add(this.splitContainer1);
      this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel2.Location = new System.Drawing.Point(3, 3);
      this.panel2.Name = "panel2";
      this.panel2.Size = new System.Drawing.Size(618, 399);
      this.panel2.TabIndex = 1;
      // 
      // splitContainer1
      // 
      this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
      this.splitContainer1.Location = new System.Drawing.Point(0, 0);
      this.splitContainer1.Name = "splitContainer1";
      this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // splitContainer1.Panel1
      // 
      this.splitContainer1.Panel1.Controls.Add(this.panel3);
      // 
      // splitContainer1.Panel2
      // 
      this.splitContainer1.Panel2.Controls.Add(this.textBox1);
      this.splitContainer1.Size = new System.Drawing.Size(618, 399);
      this.splitContainer1.SplitterDistance = 107;
      this.splitContainer1.TabIndex = 0;
      // 
      // panel3
      // 
      this.panel3.Controls.Add(this.cmdEditConfig);
      this.panel3.Controls.Add(this.cmdRun);
      this.panel3.Controls.Add(this.cmdCompile);
      this.panel3.Controls.Add(this.cmdExportDune);
      this.panel3.Controls.Add(this.txtName);
      this.panel3.Controls.Add(this.label2);
      this.panel3.Controls.Add(this.cmdBrowseTarget);
      this.panel3.Controls.Add(this.txtTargetDir);
      this.panel3.Controls.Add(this.label1);
      this.panel3.Controls.Add(this.cmdCreateHost);
      this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel3.Location = new System.Drawing.Point(0, 0);
      this.panel3.Name = "panel3";
      this.panel3.Size = new System.Drawing.Size(618, 107);
      this.panel3.TabIndex = 0;
      // 
      // cmdEditConfig
      // 
      this.cmdEditConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.cmdEditConfig.Location = new System.Drawing.Point(459, 75);
      this.cmdEditConfig.Name = "cmdEditConfig";
      this.cmdEditConfig.Size = new System.Drawing.Size(75, 23);
      this.cmdEditConfig.TabIndex = 9;
      this.cmdEditConfig.Text = "&Edit";
      this.cmdEditConfig.UseVisualStyleBackColor = true;
      this.cmdEditConfig.Click += new System.EventHandler(this.OnEditConfig);
      // 
      // cmdRun
      // 
      this.cmdRun.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.cmdRun.Location = new System.Drawing.Point(540, 75);
      this.cmdRun.Name = "cmdRun";
      this.cmdRun.Size = new System.Drawing.Size(75, 23);
      this.cmdRun.TabIndex = 8;
      this.cmdRun.Text = "&Run";
      this.cmdRun.UseVisualStyleBackColor = true;
      this.cmdRun.Click += new System.EventHandler(this.OnRunClick);
      // 
      // cmdCompile
      // 
      this.cmdCompile.Location = new System.Drawing.Point(283, 75);
      this.cmdCompile.Name = "cmdCompile";
      this.cmdCompile.Size = new System.Drawing.Size(98, 23);
      this.cmdCompile.TabIndex = 7;
      this.cmdCompile.Text = "Compile Module";
      this.cmdCompile.UseVisualStyleBackColor = true;
      this.cmdCompile.Click += new System.EventHandler(this.OnCompileModel);
      // 
      // cmdExportDune
      // 
      this.cmdExportDune.Location = new System.Drawing.Point(182, 75);
      this.cmdExportDune.Name = "cmdExportDune";
      this.cmdExportDune.Size = new System.Drawing.Size(95, 23);
      this.cmdExportDune.TabIndex = 6;
      this.cmdExportDune.Text = "Export to Dune";
      this.cmdExportDune.UseVisualStyleBackColor = true;
      this.cmdExportDune.Click += new System.EventHandler(this.OnExportModel);
      // 
      // txtName
      // 
      this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtName.Location = new System.Drawing.Point(102, 40);
      this.txtName.Name = "txtName";
      this.txtName.Size = new System.Drawing.Size(432, 20);
      this.txtName.TabIndex = 5;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(55, 43);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(41, 13);
      this.label2.TabIndex = 4;
      this.label2.Text = "Name: ";
      // 
      // cmdBrowseTarget
      // 
      this.cmdBrowseTarget.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.cmdBrowseTarget.Location = new System.Drawing.Point(540, 9);
      this.cmdBrowseTarget.Name = "cmdBrowseTarget";
      this.cmdBrowseTarget.Size = new System.Drawing.Size(75, 23);
      this.cmdBrowseTarget.TabIndex = 3;
      this.cmdBrowseTarget.Text = "...";
      this.cmdBrowseTarget.UseVisualStyleBackColor = true;
      this.cmdBrowseTarget.Click += new System.EventHandler(this.OnBrowseClicke);
      // 
      // txtTargetDir
      // 
      this.txtTargetDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtTargetDir.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
      this.txtTargetDir.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystemDirectories;
      this.txtTargetDir.Location = new System.Drawing.Point(102, 11);
      this.txtTargetDir.Name = "txtTargetDir";
      this.txtTargetDir.Size = new System.Drawing.Size(432, 20);
      this.txtTargetDir.TabIndex = 2;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(3, 14);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(93, 13);
      this.label1.TabIndex = 1;
      this.label1.Text = "Create Module in: ";
      // 
      // cmdCreateHost
      // 
      this.cmdCreateHost.Location = new System.Drawing.Point(102, 75);
      this.cmdCreateHost.Name = "cmdCreateHost";
      this.cmdCreateHost.Size = new System.Drawing.Size(74, 23);
      this.cmdCreateHost.TabIndex = 0;
      this.cmdCreateHost.Text = "Create Host";
      this.cmdCreateHost.UseVisualStyleBackColor = true;
      this.cmdCreateHost.Click += new System.EventHandler(this.OnCreateHostClick);
      // 
      // textBox1
      // 
      this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.textBox1.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.textBox1.Location = new System.Drawing.Point(0, 0);
      this.textBox1.MaxLength = 0;
      this.textBox1.Multiline = true;
      this.textBox1.Name = "textBox1";
      this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
      this.textBox1.Size = new System.Drawing.Size(618, 288);
      this.textBox1.TabIndex = 0;
      // 
      // FormPrepareDune
      // 
      this.AcceptButton = this.cmdClose;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.cmdClose;
      this.ClientSize = new System.Drawing.Size(624, 441);
      this.Controls.Add(this.tableLayoutPanel1);
      this.MinimumSize = new System.Drawing.Size(640, 480);
      this.Name = "FormPrepareDune";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Prepare Model for DUNE";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnFormClosing);
      this.tableLayoutPanel1.ResumeLayout(false);
      this.panel1.ResumeLayout(false);
      this.panel2.ResumeLayout(false);
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel2.ResumeLayout(false);
      this.splitContainer1.Panel2.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
      this.splitContainer1.ResumeLayout(false);
      this.panel3.ResumeLayout(false);
      this.panel3.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.Button cmdClose;
    private System.Windows.Forms.Panel panel2;
    private System.Windows.Forms.SplitContainer splitContainer1;
    private System.Windows.Forms.Panel panel3;
    private System.Windows.Forms.TextBox textBox1;
    private System.Windows.Forms.TextBox txtName;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Button cmdBrowseTarget;
    private System.Windows.Forms.TextBox txtTargetDir;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Button cmdCreateHost;
    private System.Windows.Forms.Button cmdCompile;
    private System.Windows.Forms.Button cmdExportDune;
    private System.Windows.Forms.Button cmdRun;
    private System.Windows.Forms.Button cmdEditConfig;
  }
}