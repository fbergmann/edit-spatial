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


    private void OnTimer(object sender, EventArgs e)
    {
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
      //for (int i = 0; i < PencilSize; i++)
      Parallel.For(0, PencilSize, i =>
      {
        for (int j = 0; j < PencilSize; j++)
        {
          double offSetX = Math.Floor(PencilSize/2f) - i;
          double offSetY = Math.Floor(PencilSize/2f) - j;


          var posX = (int) Math.Round(point.X - offSetX);
          var posY = (int) Math.Round(point.Y - offSetY);

          if (posX < 0) posX = 0;
          if (posY < 0) posY = 0;

          if (posX >= Model.Columns) posX = Model.Columns - 1;
          if (posY >= Model.Rows) posY = Model.Rows - 1;

          double distance = Math.Sqrt(Math.Pow(point.X - posX, 2) + Math.Pow(point.Y - posY, 2));

          if (distance > PencilSize/2f) continue;

          Model[posX, posY] = CurrentValue;
        }
      });

      Model[point.X, point.Y] = CurrentValue;
      UpdateUI();
    }

    private void OnMouseClick(object sender, MouseEventArgs e)
    {
      if (Model == null) return;

      Point point = GetPointForMouse(e.X, e.Y);
      SetValueAround(point);
    }

    private Point GetPointForMouse(int x, int y)
    {
      double stretchX = (Model.Columns - 1)/(double) pictureBox1.Width;
      double stretchY = (Model.Rows - 1)/(double) pictureBox1.Height;

      return new Point(
        Math.Max(Math.Min((int) Math.Round(stretchX*x + stretchX/2f), Model.Rows - 1), 0),
        Math.Max(Math.Min((int) Math.Round(stretchY*y + stretchX/2f), Model.Columns - 1), 0));
    }

    private void OnMouseMove(object sender, MouseEventArgs e)
    {
      if (Model == null || !Painting) return;

      Point point = GetPointForMouse(e.X, e.Y);
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