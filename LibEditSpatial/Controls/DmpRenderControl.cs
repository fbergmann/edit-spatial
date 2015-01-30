using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using LibEditSpatial.Model;

namespace LibEditSpatial.Controls
{
  public partial class DmpRenderControl : UserControl
  {
    public DmpRenderControl()
    {
      InitializeComponent();
      Painting = false;
      CurrentValue = 10;
      PencilSize = 1;
    }

    public DmpModel Model { get; set; }

    public double CurrentValue { get; set; }
    private bool Painting { get; set; }
    public int PencilSize { get; set; }

    public bool DisableEditing { get; set;  }
    public bool DisableNotification { get; set;  }

    public event EventHandler<Point> IndexLocationChanged;
    protected virtual void OnLocationChanged(Point e)
    {
      if (DisableNotification) return;
      var handler = IndexLocationChanged;
      if (handler != null) handler(this, e);
    }

    public event EventHandler<PointF> DataLocationChanged;
    protected virtual void OnDataLocationChanged(PointF e)
    {
      if (DisableNotification) return;
      var handler = DataLocationChanged;
      if (handler != null) handler(this, e);
    }

    private void OnTimerTick(object sender, EventArgs e)
    {
      if (Model == null) return;
      Bitmap bmp = Model.ToImage();
      pictureBox1.Image = bmp;
      timer1.Enabled = false;
    }

    private void UpdateUI()
    {
      timer1.Enabled = true;
    }

    public void LoadModel(DmpModel model)
    {
      Painting = false;
      Model = model;
      UpdateUI();
    }

    private void SetValueAround(Point point)
    {
      if (DisableEditing) return; 

      Parallel.For(0, PencilSize, i =>
      {
        for (int j = 0; j < PencilSize; j++)
        {
          double offSetX = Math.Floor(PencilSize / 2f) - i;
          double offSetY = Math.Floor(PencilSize / 2f) - j;


          var posX = (int)Math.Round(point.X - offSetX);
          var posY = (int)Math.Round(point.Y - offSetY);

          if (posX < 0) posX = 0;
          if (posY < 0) posY = 0;

          if (posX >= Model.Columns) posX = Model.Columns - 1;
          if (posY >= Model.Rows) posY = Model.Rows - 1;

          double distance = Math.Sqrt(Math.Pow(point.X - posX, 2) + Math.Pow(point.Y - posY, 2));

          if (distance > PencilSize / 2f) continue;

          Model[posX, posY] = CurrentValue;
        }
        Application.DoEvents();
      });

      Model[point.X, point.Y] = CurrentValue;
      UpdateUI();
    }

    private void OnMouseClick(object sender, MouseEventArgs e)
    {
      if (Model == null) return;

      Point point = GetPointForMouse(e.X, e.Y);

      if (DisableEditing) return;
      SetValueAround(point);
    }

    private Point GetPointForMouse(int x, int y)
    {
      if (Model == null) return Point.Empty;

      double stretchX = (Model.Columns - 1) / (double)pictureBox1.Width;
      double stretchY = (Model.Rows - 1) / (double)pictureBox1.Height;

      var point = new Point(
              Math.Max(Math.Min((int)Math.Round(stretchX * x + stretchX / 2f), Model.Columns - 1), 0),
              Math.Max(Math.Min((int)Math.Round(stretchY * y + stretchY / 2f), Model.Rows - 1), 0));

      OnDataLocationChanged(new PointF(
        (float)(((float)point.X / (float)Model.Columns) * (Model.MaxX - Model.MinX) + Model.MinX),
        (float)(((float)point.Y / (float)Model.Rows) * (Model.MaxY - Model.MinY) + Model.MinY)));


      OnLocationChanged(point);
      return point;
    }

    private void OnMouseMove(object sender, MouseEventArgs e)
    {
      Point point = GetPointForMouse(e.X, e.Y);

      if (Model == null || !Painting) return;


      SetValueAround(point);
    }

    private void OnMouseDown(object sender, MouseEventArgs e)
    {
      Painting = e.Button == MouseButtons.Left;
    }

    private void OnMouseUp(object sender, MouseEventArgs e)
    {
      Painting = false;
    }

    private void OnMouseLeave(object sender, EventArgs e)
    {
      Painting = false;
    }
  }
}