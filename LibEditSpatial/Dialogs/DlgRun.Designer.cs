using System.ComponentModel;
using System.Windows.Forms;

namespace LibEditSpatial.Dialogs
{
  partial class DlgRun
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

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      this.label1 = new System.Windows.Forms.Label();
      this.txtFileName = new System.Windows.Forms.TextBox();
      this.cmdBrowse = new System.Windows.Forms.Button();
      this.progressBar1 = new System.Windows.Forms.ProgressBar();
      this.cmdKill = new System.Windows.Forms.Button();
      this.txtResult = new System.Windows.Forms.TextBox();
      this.cmdRun = new System.Windows.Forms.Button();
      this.cmdCompile = new System.Windows.Forms.Button();
      this.tabControl1 = new System.Windows.Forms.TabControl();
      this.tabPreview = new System.Windows.Forms.TabPage();
      this.pictureBox1 = new System.Windows.Forms.PictureBox();
      this.cmbVariable = new System.Windows.Forms.ComboBox();
      this.label2 = new System.Windows.Forms.Label();
      this.chkPreview = new System.Windows.Forms.CheckBox();
      this.tabMessages = new System.Windows.Forms.TabPage();
      this.cmdParaview = new System.Windows.Forms.Button();
      this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
      this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
      this.cmdFolder = new System.Windows.Forms.Button();
      this.tabControl1.SuspendLayout();
      this.tabPreview.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
      this.tabMessages.SuspendLayout();
      this.SuspendLayout();
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(16, 9);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(29, 13);
      this.label1.TabIndex = 0;
      this.label1.Text = "File: ";
      // 
      // txtFileName
      // 
      this.txtFileName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtFileName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
      this.txtFileName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystem;
      this.txtFileName.Location = new System.Drawing.Point(47, 6);
      this.txtFileName.Name = "txtFileName";
      this.txtFileName.Size = new System.Drawing.Size(379, 20);
      this.txtFileName.TabIndex = 1;
      // 
      // cmdBrowse
      // 
      this.cmdBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.cmdBrowse.Location = new System.Drawing.Point(432, 4);
      this.cmdBrowse.Name = "cmdBrowse";
      this.cmdBrowse.Size = new System.Drawing.Size(75, 23);
      this.cmdBrowse.TabIndex = 2;
      this.cmdBrowse.Text = "..";
      this.cmdBrowse.UseVisualStyleBackColor = true;
      this.cmdBrowse.Click += new System.EventHandler(this.OnBrowseClick);
      // 
      // progressBar1
      // 
      this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.progressBar1.Location = new System.Drawing.Point(15, 58);
      this.progressBar1.Name = "progressBar1";
      this.progressBar1.Size = new System.Drawing.Size(492, 23);
      this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
      this.progressBar1.TabIndex = 3;
      // 
      // cmdKill
      // 
      this.cmdKill.Location = new System.Drawing.Point(15, 87);
      this.cmdKill.Name = "cmdKill";
      this.cmdKill.Size = new System.Drawing.Size(75, 23);
      this.cmdKill.TabIndex = 4;
      this.cmdKill.Text = "&Kill";
      this.cmdKill.UseVisualStyleBackColor = true;
      this.cmdKill.Click += new System.EventHandler(this.OnKillClick);
      // 
      // txtResult
      // 
      this.txtResult.Dock = System.Windows.Forms.DockStyle.Fill;
      this.txtResult.Location = new System.Drawing.Point(3, 3);
      this.txtResult.MaxLength = 0;
      this.txtResult.Multiline = true;
      this.txtResult.Name = "txtResult";
      this.txtResult.ReadOnly = true;
      this.txtResult.ScrollBars = System.Windows.Forms.ScrollBars.Both;
      this.txtResult.Size = new System.Drawing.Size(478, 181);
      this.txtResult.TabIndex = 5;
      // 
      // cmdRun
      // 
      this.cmdRun.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.cmdRun.Location = new System.Drawing.Point(432, 87);
      this.cmdRun.Name = "cmdRun";
      this.cmdRun.Size = new System.Drawing.Size(75, 23);
      this.cmdRun.TabIndex = 6;
      this.cmdRun.Text = "&Run";
      this.cmdRun.UseVisualStyleBackColor = true;
      this.cmdRun.Click += new System.EventHandler(this.OnRunClick);
      // 
      // cmdCompile
      // 
      this.cmdCompile.Location = new System.Drawing.Point(96, 87);
      this.cmdCompile.Name = "cmdCompile";
      this.cmdCompile.Size = new System.Drawing.Size(75, 23);
      this.cmdCompile.TabIndex = 7;
      this.cmdCompile.Text = "Compile";
      this.cmdCompile.UseVisualStyleBackColor = true;
      this.cmdCompile.Click += new System.EventHandler(this.OnCompileClick);
      // 
      // tabControl1
      // 
      this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.tabControl1.Controls.Add(this.tabPreview);
      this.tabControl1.Controls.Add(this.tabMessages);
      this.tabControl1.Location = new System.Drawing.Point(15, 126);
      this.tabControl1.Name = "tabControl1";
      this.tabControl1.SelectedIndex = 0;
      this.tabControl1.Size = new System.Drawing.Size(492, 213);
      this.tabControl1.TabIndex = 8;
      // 
      // tabPreview
      // 
      this.tabPreview.Controls.Add(this.pictureBox1);
      this.tabPreview.Controls.Add(this.cmbVariable);
      this.tabPreview.Controls.Add(this.label2);
      this.tabPreview.Controls.Add(this.chkPreview);
      this.tabPreview.Location = new System.Drawing.Point(4, 22);
      this.tabPreview.Name = "tabPreview";
      this.tabPreview.Padding = new System.Windows.Forms.Padding(3);
      this.tabPreview.Size = new System.Drawing.Size(484, 187);
      this.tabPreview.TabIndex = 0;
      this.tabPreview.Text = "Preview";
      this.tabPreview.UseVisualStyleBackColor = true;
      // 
      // pictureBox1
      // 
      this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.pictureBox1.Location = new System.Drawing.Point(6, 29);
      this.pictureBox1.Name = "pictureBox1";
      this.pictureBox1.Size = new System.Drawing.Size(472, 152);
      this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pictureBox1.TabIndex = 3;
      this.pictureBox1.TabStop = false;
      // 
      // cmbVariable
      // 
      this.cmbVariable.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.cmbVariable.FormattingEnabled = true;
      this.cmbVariable.Location = new System.Drawing.Point(202, 4);
      this.cmbVariable.Name = "cmbVariable";
      this.cmbVariable.Size = new System.Drawing.Size(276, 21);
      this.cmbVariable.TabIndex = 2;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(148, 7);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(48, 13);
      this.label2.TabIndex = 1;
      this.label2.Text = "Variable:";
      // 
      // chkPreview
      // 
      this.chkPreview.AutoSize = true;
      this.chkPreview.Location = new System.Drawing.Point(6, 6);
      this.chkPreview.Name = "chkPreview";
      this.chkPreview.Size = new System.Drawing.Size(116, 17);
      this.chkPreview.TabIndex = 0;
      this.chkPreview.Text = "Generate Previews";
      this.chkPreview.UseVisualStyleBackColor = true;
      // 
      // tabMessages
      // 
      this.tabMessages.Controls.Add(this.txtResult);
      this.tabMessages.Location = new System.Drawing.Point(4, 22);
      this.tabMessages.Name = "tabMessages";
      this.tabMessages.Padding = new System.Windows.Forms.Padding(3);
      this.tabMessages.Size = new System.Drawing.Size(484, 187);
      this.tabMessages.TabIndex = 1;
      this.tabMessages.Text = "Messages";
      this.tabMessages.UseVisualStyleBackColor = true;
      // 
      // cmdParaview
      // 
      this.cmdParaview.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.cmdParaview.Location = new System.Drawing.Point(351, 87);
      this.cmdParaview.Name = "cmdParaview";
      this.cmdParaview.Size = new System.Drawing.Size(75, 23);
      this.cmdParaview.TabIndex = 9;
      this.cmdParaview.Text = "PV";
      this.toolTip1.SetToolTip(this.cmdParaview, "Opens the current result in paraview");
      this.cmdParaview.UseVisualStyleBackColor = true;
      this.cmdParaview.Click += new System.EventHandler(this.OnOpenPV);
      // 
      // backgroundWorker1
      // 
      this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.UpdateWorkerDoWork);
      this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.UpdateWorkerCompleted);
      // 
      // cmdFolder
      // 
      this.cmdFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.cmdFolder.Location = new System.Drawing.Point(270, 87);
      this.cmdFolder.Name = "cmdFolder";
      this.cmdFolder.Size = new System.Drawing.Size(75, 23);
      this.cmdFolder.TabIndex = 10;
      this.cmdFolder.Text = "&Folder";
      this.cmdFolder.UseVisualStyleBackColor = true;
      this.cmdFolder.Click += new System.EventHandler(this.OnFolderClick);
      // 
      // DlgRun
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(519, 351);
      this.Controls.Add(this.cmdFolder);
      this.Controls.Add(this.cmdParaview);
      this.Controls.Add(this.tabControl1);
      this.Controls.Add(this.cmdCompile);
      this.Controls.Add(this.cmdRun);
      this.Controls.Add(this.cmdKill);
      this.Controls.Add(this.progressBar1);
      this.Controls.Add(this.cmdBrowse);
      this.Controls.Add(this.txtFileName);
      this.Controls.Add(this.label1);
      this.Name = "DlgRun";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Run";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DlgRun_FormClosing);
      this.Load += new System.EventHandler(this.DlgRun_Load);
      this.tabControl1.ResumeLayout(false);
      this.tabPreview.ResumeLayout(false);
      this.tabPreview.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
      this.tabMessages.ResumeLayout(false);
      this.tabMessages.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private Label label1;
    private TextBox txtFileName;
    private Button cmdBrowse;
    private ProgressBar progressBar1;
    private Button cmdKill;
    private TextBox txtResult;
    private Button cmdRun;
    private Button cmdCompile;
    private TabControl tabControl1;
    private TabPage tabPreview;
    private PictureBox pictureBox1;
    private ComboBox cmbVariable;
    private Label label2;
    private CheckBox chkPreview;
    private TabPage tabMessages;
    private Button cmdParaview;
    private ToolTip toolTip1;
    private BackgroundWorker backgroundWorker1;
    private Button cmdFolder;
  }
}