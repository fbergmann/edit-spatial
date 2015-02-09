using System.ComponentModel;
using System.Windows.Forms;
using EditSpatial.Controls;

namespace EditSpatial.Forms
{
  partial class FormErrors
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormErrors));
      this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
      this.panel1 = new System.Windows.Forms.Panel();
      this.cmdFixCommon = new System.Windows.Forms.Button();
      this.cmdClose = new System.Windows.Forms.Button();
      this.cmdValidate = new System.Windows.Forms.Button();
      this.panel2 = new System.Windows.Forms.Panel();
      this.label1 = new System.Windows.Forms.Label();
      this.controlText1 = new EditSpatial.Controls.ControlText();
      this.tableLayoutPanel1.SuspendLayout();
      this.panel1.SuspendLayout();
      this.panel2.SuspendLayout();
      this.SuspendLayout();
      // 
      // tableLayoutPanel1
      // 
      this.tableLayoutPanel1.ColumnCount = 1;
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
      this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 2);
      this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 0);
      this.tableLayoutPanel1.Controls.Add(this.controlText1, 0, 1);
      this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
      this.tableLayoutPanel1.Name = "tableLayoutPanel1";
      this.tableLayoutPanel1.RowCount = 3;
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
      this.tableLayoutPanel1.Size = new System.Drawing.Size(624, 441);
      this.tableLayoutPanel1.TabIndex = 0;
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.cmdFixCommon);
      this.panel1.Controls.Add(this.cmdClose);
      this.panel1.Controls.Add(this.cmdValidate);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel1.Location = new System.Drawing.Point(3, 410);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(618, 28);
      this.panel1.TabIndex = 0;
      // 
      // cmdFixCommon
      // 
      this.cmdFixCommon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.cmdFixCommon.Location = new System.Drawing.Point(84, 3);
      this.cmdFixCommon.Name = "cmdFixCommon";
      this.cmdFixCommon.Size = new System.Drawing.Size(107, 23);
      this.cmdFixCommon.TabIndex = 2;
      this.cmdFixCommon.Text = "&Fix Common Errors";
      this.cmdFixCommon.UseVisualStyleBackColor = true;
      this.cmdFixCommon.Click += new System.EventHandler(this.OnFixCommonErrors);
      // 
      // cmdClose
      // 
      this.cmdClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.cmdClose.Location = new System.Drawing.Point(540, 3);
      this.cmdClose.Name = "cmdClose";
      this.cmdClose.Size = new System.Drawing.Size(75, 23);
      this.cmdClose.TabIndex = 1;
      this.cmdClose.Text = "&Close";
      this.cmdClose.UseVisualStyleBackColor = true;
      this.cmdClose.Click += new System.EventHandler(this.OnCloseClick);
      // 
      // cmdValidate
      // 
      this.cmdValidate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.cmdValidate.Location = new System.Drawing.Point(3, 3);
      this.cmdValidate.Name = "cmdValidate";
      this.cmdValidate.Size = new System.Drawing.Size(75, 23);
      this.cmdValidate.TabIndex = 0;
      this.cmdValidate.Text = "&Validate";
      this.cmdValidate.UseVisualStyleBackColor = true;
      this.cmdValidate.Click += new System.EventHandler(this.OnValidateClick);
      // 
      // panel2
      // 
      this.panel2.Controls.Add(this.label1);
      this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel2.Location = new System.Drawing.Point(3, 3);
      this.panel2.Name = "panel2";
      this.panel2.Size = new System.Drawing.Size(618, 14);
      this.panel2.TabIndex = 1;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.label1.Location = new System.Drawing.Point(0, 0);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(0, 13);
      this.label1.TabIndex = 0;
      this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // controlText1
      // 
      this.controlText1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.controlText1.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.controlText1.Location = new System.Drawing.Point(3, 23);
      this.controlText1.MaxLength = 0;
      this.controlText1.Multiline = true;
      this.controlText1.Name = "controlText1";
      this.controlText1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
      this.controlText1.Size = new System.Drawing.Size(618, 381);
      this.controlText1.TabIndex = 2;
      this.controlText1.WordWrap = false;
      // 
      // FormErrors
      // 
      this.AcceptButton = this.cmdClose;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.cmdClose;
      this.ClientSize = new System.Drawing.Size(624, 441);
      this.Controls.Add(this.tableLayoutPanel1);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MinimumSize = new System.Drawing.Size(400, 400);
      this.Name = "FormErrors";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Errors & Warnings";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnFormClosing);
      this.tableLayoutPanel1.ResumeLayout(false);
      this.tableLayoutPanel1.PerformLayout();
      this.panel1.ResumeLayout(false);
      this.panel2.ResumeLayout(false);
      this.panel2.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private TableLayoutPanel tableLayoutPanel1;
    private Panel panel1;
    private Panel panel2;
    private ControlText controlText1;
    private Button cmdClose;
    private Button cmdValidate;
    private Label label1;
    private Button cmdFixCommon;
  }
}