using System.ComponentModel;
using System.Windows.Forms;

namespace EditSpatial.Controls
{
  partial class ControlSampleFieldGeometry
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
      this.components = new System.ComponentModel.Container();
      this.grid = new System.Windows.Forms.DataGridView();
      this.colSpatialId = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colDomainType = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colSampledValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colMinValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colMaxValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.label1 = new System.Windows.Forms.Label();
      this.txtId = new System.Windows.Forms.TextBox();
      this.thumbGeometry = new System.Windows.Forms.PictureBox();
      this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
      this.panel1 = new System.Windows.Forms.Panel();
      this.cmdSave = new System.Windows.Forms.Button();
      this.cmdLoad = new System.Windows.Forms.Button();
      this.cmdReorder = new System.Windows.Forms.Button();
      this.cmdUpdateImage = new System.Windows.Forms.Button();
      this.txtZ = new System.Windows.Forms.TextBox();
      this.lblZ = new System.Windows.Forms.Label();
      this.trackBar1 = new System.Windows.Forms.TrackBar();
      this.txtSize = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
      ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
      this.groupBox1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.thumbGeometry)).BeginInit();
      this.tableLayoutPanel1.SuspendLayout();
      this.panel1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
      this.SuspendLayout();
      // 
      // grid
      // 
      this.grid.AllowUserToAddRows = false;
      this.grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colSpatialId,
            this.colDomainType,
            this.colSampledValue,
            this.colMinValue,
            this.colMaxValue});
      this.grid.Dock = System.Windows.Forms.DockStyle.Fill;
      this.grid.Location = new System.Drawing.Point(3, 16);
      this.grid.Name = "grid";
      this.grid.Size = new System.Drawing.Size(542, 131);
      this.grid.TabIndex = 0;
      this.grid.UserDeletedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.OnUserDeletedRow);
      // 
      // colSpatialId
      // 
      this.colSpatialId.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
      this.colSpatialId.HeaderText = "Spatial Id";
      this.colSpatialId.Name = "colSpatialId";
      this.colSpatialId.Width = 68;
      // 
      // colDomainType
      // 
      this.colDomainType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader;
      this.colDomainType.HeaderText = "Domain Type";
      this.colDomainType.Name = "colDomainType";
      this.colDomainType.Width = 5;
      // 
      // colSampledValue
      // 
      this.colSampledValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader;
      this.colSampledValue.HeaderText = "Sampled Value";
      this.colSampledValue.Name = "colSampledValue";
      this.colSampledValue.Width = 5;
      // 
      // colMinValue
      // 
      this.colMinValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader;
      this.colMinValue.HeaderText = "Min Value";
      this.colMinValue.Name = "colMinValue";
      this.colMinValue.Width = 5;
      // 
      // colMaxValue
      // 
      this.colMaxValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
      this.colMaxValue.HeaderText = "Max Value";
      this.colMaxValue.Name = "colMaxValue";
      // 
      // groupBox1
      // 
      this.tableLayoutPanel1.SetColumnSpan(this.groupBox1, 2);
      this.groupBox1.Controls.Add(this.grid);
      this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.groupBox1.Location = new System.Drawing.Point(3, 158);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(548, 150);
      this.groupBox1.TabIndex = 1;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Analytic Volumes: ";
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(3, 10);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(22, 13);
      this.label1.TabIndex = 2;
      this.label1.Text = "Id: ";
      // 
      // txtId
      // 
      this.txtId.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtId.Location = new System.Drawing.Point(31, 7);
      this.txtId.Name = "txtId";
      this.txtId.Size = new System.Drawing.Size(226, 20);
      this.txtId.TabIndex = 3;
      // 
      // thumbGeometry
      // 
      this.thumbGeometry.BackColor = System.Drawing.Color.White;
      this.thumbGeometry.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.thumbGeometry.Dock = System.Windows.Forms.DockStyle.Fill;
      this.thumbGeometry.InitialImage = global::EditSpatial.Properties.Resources.IMAGE_NoGeomery;
      this.thumbGeometry.Location = new System.Drawing.Point(280, 3);
      this.thumbGeometry.Name = "thumbGeometry";
      this.thumbGeometry.Size = new System.Drawing.Size(271, 149);
      this.thumbGeometry.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.thumbGeometry.TabIndex = 4;
      this.thumbGeometry.TabStop = false;
      // 
      // tableLayoutPanel1
      // 
      this.tableLayoutPanel1.ColumnCount = 2;
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
      this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 1);
      this.tableLayoutPanel1.Controls.Add(this.thumbGeometry, 1, 0);
      this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
      this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
      this.tableLayoutPanel1.Name = "tableLayoutPanel1";
      this.tableLayoutPanel1.RowCount = 2;
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
      this.tableLayoutPanel1.Size = new System.Drawing.Size(554, 311);
      this.tableLayoutPanel1.TabIndex = 5;
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.cmdSave);
      this.panel1.Controls.Add(this.cmdLoad);
      this.panel1.Controls.Add(this.cmdReorder);
      this.panel1.Controls.Add(this.cmdUpdateImage);
      this.panel1.Controls.Add(this.txtZ);
      this.panel1.Controls.Add(this.lblZ);
      this.panel1.Controls.Add(this.trackBar1);
      this.panel1.Controls.Add(this.txtSize);
      this.panel1.Controls.Add(this.label2);
      this.panel1.Controls.Add(this.label1);
      this.panel1.Controls.Add(this.txtId);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel1.Location = new System.Drawing.Point(3, 3);
      this.panel1.MinimumSize = new System.Drawing.Size(267, 142);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(271, 149);
      this.panel1.TabIndex = 5;
      // 
      // cmdSave
      // 
      this.cmdSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.cmdSave.Location = new System.Drawing.Point(182, 33);
      this.cmdSave.Name = "cmdSave";
      this.cmdSave.Size = new System.Drawing.Size(75, 23);
      this.cmdSave.TabIndex = 17;
      this.cmdSave.Text = "Save";
      this.toolTip1.SetToolTip(this.cmdSave, "Save Image To file");
      this.cmdSave.UseVisualStyleBackColor = true;
      this.cmdSave.Click += new System.EventHandler(this.OnImageSave);
      // 
      // cmdLoad
      // 
      this.cmdLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.cmdLoad.Location = new System.Drawing.Point(101, 33);
      this.cmdLoad.Name = "cmdLoad";
      this.cmdLoad.Size = new System.Drawing.Size(75, 23);
      this.cmdLoad.TabIndex = 16;
      this.cmdLoad.Text = "Load";
      this.toolTip1.SetToolTip(this.cmdLoad, "Load a new Image");
      this.cmdLoad.UseVisualStyleBackColor = true;
      this.cmdLoad.Click += new System.EventHandler(this.OnImageLoad);
      // 
      // cmdReorder
      // 
      this.cmdReorder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.cmdReorder.Location = new System.Drawing.Point(182, 123);
      this.cmdReorder.Name = "cmdReorder";
      this.cmdReorder.Size = new System.Drawing.Size(75, 23);
      this.cmdReorder.TabIndex = 15;
      this.cmdReorder.Text = "&Flip Order";
      this.cmdReorder.UseVisualStyleBackColor = true;
      this.cmdReorder.Click += new System.EventHandler(this.OnReorder);
      // 
      // cmdUpdateImage
      // 
      this.cmdUpdateImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.cmdUpdateImage.Location = new System.Drawing.Point(182, 97);
      this.cmdUpdateImage.Name = "cmdUpdateImage";
      this.cmdUpdateImage.Size = new System.Drawing.Size(75, 23);
      this.cmdUpdateImage.TabIndex = 14;
      this.cmdUpdateImage.Text = "&Update";
      this.cmdUpdateImage.UseVisualStyleBackColor = true;
      this.cmdUpdateImage.Click += new System.EventHandler(this.OnUpdateImage);
      // 
      // txtZ
      // 
      this.txtZ.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtZ.Location = new System.Drawing.Point(72, 99);
      this.txtZ.Name = "txtZ";
      this.txtZ.Size = new System.Drawing.Size(104, 20);
      this.txtZ.TabIndex = 13;
      this.txtZ.Text = "0";
      this.txtZ.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      // 
      // lblZ
      // 
      this.lblZ.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.lblZ.AutoSize = true;
      this.lblZ.Location = new System.Drawing.Point(3, 102);
      this.lblZ.Name = "lblZ";
      this.lblZ.Size = new System.Drawing.Size(46, 13);
      this.lblZ.TabIndex = 12;
      this.lblZ.Text = "Z value:";
      // 
      // trackBar1
      // 
      this.trackBar1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.trackBar1.BackColor = System.Drawing.Color.White;
      this.trackBar1.Location = new System.Drawing.Point(10, 56);
      this.trackBar1.Maximum = 100;
      this.trackBar1.Name = "trackBar1";
      this.trackBar1.Size = new System.Drawing.Size(247, 45);
      this.trackBar1.TabIndex = 11;
      this.trackBar1.TickFrequency = 5;
      this.trackBar1.Scroll += new System.EventHandler(this.OnTrackChanged);
      // 
      // txtSize
      // 
      this.txtSize.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtSize.Location = new System.Drawing.Point(72, 125);
      this.txtSize.Name = "txtSize";
      this.txtSize.Size = new System.Drawing.Size(104, 20);
      this.txtSize.TabIndex = 6;
      this.txtSize.Text = "128";
      this.txtSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      // 
      // label2
      // 
      this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(3, 128);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(63, 13);
      this.label2.TabIndex = 5;
      this.label2.Text = "ThumbSize:";
      // 
      // ControlSampleFieldGeometry
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.tableLayoutPanel1);
      this.Name = "ControlSampleFieldGeometry";
      this.Size = new System.Drawing.Size(554, 311);
      ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
      this.groupBox1.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.thumbGeometry)).EndInit();
      this.tableLayoutPanel1.ResumeLayout(false);
      this.panel1.ResumeLayout(false);
      this.panel1.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private DataGridView grid;
    private GroupBox groupBox1;
    private Label label1;
    private TextBox txtId;
    private PictureBox thumbGeometry;
    private TableLayoutPanel tableLayoutPanel1;
    private Panel panel1;
    private TextBox txtSize;
    private Label label2;
    private DataGridViewTextBoxColumn colSpatialId;
    private DataGridViewTextBoxColumn colDomainType;
    private DataGridViewTextBoxColumn colSampledValue;
    private DataGridViewTextBoxColumn colMinValue;
    private DataGridViewTextBoxColumn colMaxValue;
    private TrackBar trackBar1;
    private TextBox txtZ;
    private Label lblZ;
    private Button cmdUpdateImage;
    private Button cmdReorder;
    private Button cmdSave;
    private ToolTip toolTip1;
    private Button cmdLoad;
  }
}
