using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace LibEditSpatial.Dialogs
{
  public partial class FormResize : Form
  {

    public RectangleF CanvasBounds
    {
      get { 
        float minX; float.TryParse(txtMinX.Text, out minX);
        float maxX; float.TryParse(txtMaxX.Text, out maxX);
        float minY; float.TryParse(txtMinY.Text, out minY);
        float maxY; float.TryParse(txtMaxY.Text, out maxY);
        return new RectangleF(minX, minY, maxX, maxY);
      }
      set
      {
        txtMinX.Text = value.X.ToString(CultureInfo.InvariantCulture);
        txtMinY.Text = value.Y.ToString(CultureInfo.InvariantCulture);
        txtMaxX.Text = value.Width.ToString(CultureInfo.InvariantCulture);
        txtMaxY.Text = value.Height.ToString(CultureInfo.InvariantCulture);
      }
    }

    public Size Dimensions
    {
      get { 
        int width; int.TryParse(txtColumns.Text, out width);
        int height; int.TryParse(txtRows.Text, out height);
        return new Size(width, height);
      }
      set { 
        txtColumns.Text = value.Width.ToString(); 
        txtRows.Text = value.Height.ToString(); 
      }
    }

    public bool ScaleContents
    {
      get { return chkScaleContents.Checked; }
      set { chkScaleContents.Checked = value; }
    }


    public FormResize()
    {
      InitializeComponent();
    }
  }
}
