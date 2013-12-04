using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using libsbmlcs;
using Image = System.Drawing.Image;

namespace EditSpatial.Controls
{
  public partial class ControlSampleFieldGeometry : BaseSpatialControl
  {
    public Geometry SpatialGeometry { get; set; }
    public SampledFieldGeometry Current { get; set; }

    public int ThumbSize
    {
      get
      {
        int thumbSize;
        if (int.TryParse(txtSize.Text, out thumbSize))
          return thumbSize;
        return 128;
      }
    }

    public void InitializeFrom(Geometry geometry, string id)
    {
      txtId.Text = id;
      grid.Rows.Clear();
      thumbGeometry.Image = thumbGeometry.InitialImage;
      SpatialGeometry = geometry;
      if (geometry == null) return;

      var sampledFieldGeometry = geometry.getGeometryDefinition(id) as SampledFieldGeometry;
      Current = sampledFieldGeometry;
      if (sampledFieldGeometry == null) return;

      for (long i = 0; i < sampledFieldGeometry.getNumSampledVolumes(); ++i)
      {
        var vol = sampledFieldGeometry.getSampledVolume(i);
        string spatialId = vol.getSpatialId();
        grid.Rows.Add(spatialId, vol.getDomainType(), vol.getSampledValue(), vol.getMinValue(), vol.getMaxValue());
      }

      thumbGeometry.Image = GenerateImage(sampledFieldGeometry, geometry, ThumbSize, ThumbSize);

    }

    public override void SaveChanges()
    {
      if (Current == null || SpatialGeometry == null) return;

      Current.setSpatialId(txtId.Text);
      for (int i = 0; i < grid.Rows.Count && i < Current.getNumSampledVolumes(); ++i)
      {
        var row = grid.Rows[i];
        var current = Current.getSampledVolume(i);
        current.setSpatialId((string) row.Cells[0].Value);
        current.setSampledValue((double) row.Cells[1].Value);
        current.setMinValue((double) row.Cells[2].Value);
        current.setMaxValue((double) row.Cells[3].Value);
      }

    }


    private Image GenerateImage(SampledFieldGeometry sampledFieldGeometry, Geometry geometry, int resX = 128, int resY = 128)
    {
      if (geometry == null || sampledFieldGeometry == null || geometry.getNumCoordinateComponents() < 2)
        return new Bitmap(1, 1);

      try
      {
        var field = sampledFieldGeometry.getSampledField();
        if (field == null)
          return new Bitmap(1, 1);

        var data = field.getImageData();
        if (data == null)
          return new Bitmap(1, 1);

        long uncompressedLength = data.getUncompressedLength();
        var array = new int[uncompressedLength];
        data.getUncompressed(array);      

        var result = new Bitmap(field.getNumSamples1(), field.getNumSamples2());
        for (long i = 0; i < field.getNumSamples1(); ++i)
        {
          for (long j = 0; j < field.getNumSamples2(); ++j)
          {
            int index = GetIndexFor(array[i +field.getNumSamples1()*j]);

            result.SetPixel( (int)i,(int)j, GetColorForIndex(index));
          }
        }


        return result;        
      }
      catch
      {
        
      }
      return new Bitmap(1, 1);
    }

    private int GetIndexFor(int value)
    {
      if (Current == null) return 0;

      for (long i = 0; i < Current.getNumSampledVolumes(); ++i)
      {
        var current = Current.getSampledVolume(i);
        if (Math.Abs(current.getSampledValue() - value) < 1e-10)
          return (int)i;
        if (current.isSetMinValue() && current.isSetMaxValue() &&
          (value >= current.getMinValue() && value <= current.getMaxValue()))
          return (int)i;
      }

      return 0;
    }

    private Color GetColorForIndex(int index)
    {
      switch (index)
      {
        default:
          case 0:
          return Color.Black;
        case 1:
          return Color.Red;
        case 2:
          return Color.Blue;
        case 3:
          return Color.Green;
      }
    }

    public ControlSampleFieldGeometry()
    {
      InitializeComponent();
    }



    private void OnReorderClick(object sender, EventArgs e)
    {
      if (Current == null)
        return;
    }

    public override void InvalidateSelection()
    {
      SpatialGeometry = null;
      Current = null;
      InitializeFrom(null, null);
    }
  }
}
