using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using EditSpatial.Model;
using libsbmlcs;
using Image = System.Drawing.Image;

namespace EditSpatial.Controls
{
  public partial class ControlSampleFieldGeometry : BaseSpatialControl
  {
    private ImageData _Data;
    private SampledField _Field;

    public ControlSampleFieldGeometry()
    {
      InitializeComponent();
    }

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
        var spatialId = vol.getSpatialId();

        grid.Rows.Add(
          spatialId,
          vol.getDomainType(),
          vol.getSampledValue().ToString(),
          vol.getMinValue().ToString(),
          vol.getMaxValue().ToString()
          );
      }

      _Field = sampledFieldGeometry.getSampledField();
      _Data = _Field == null ? null : _Field.getImageData();
      var numSamples = _Field == null ? 0 : _Field.getNumSamples3() - 1;


      if (SpatialGeometry.getNumCoordinateComponents() < 3 || numSamples == 0)
      {
        txtZ.Visible = false;
        trackBar1.Visible = false;
        lblZ.Visible = false;
      }
      else
      {
        txtZ.Visible = true;
        trackBar1.Visible = true;
        lblZ.Visible = true;
        trackBar1.Minimum = 0;
        trackBar1.Maximum = numSamples;
      }


      thumbGeometry.Image = GenerateImage(sampledFieldGeometry, geometry, ThumbSize, ThumbSize);
    }

    public override void SaveChanges()
    {
      if (Current == null || SpatialGeometry == null) return;

      Current.setSpatialId(txtId.Text);
      for (var i = 0; i < grid.Rows.Count && i < Current.getNumSampledVolumes(); ++i)
      {
        var row = grid.Rows[i];
        var current = Current.getSampledVolume(i);
        current.setSpatialId((string) row.Cells[0].Value);
        current.setDomainType((string) row.Cells[1].Value);
        double value;
        if (double.TryParse((string) row.Cells[2].Value, out value))
          current.setSampledValue(value);
        if (double.TryParse((string) row.Cells[3].Value, out value))
          current.setMinValue(value);
        if (double.TryParse((string) row.Cells[4].Value, out value))
          current.setMaxValue(value);
      }
    }

    private List<int> GetUniqueValues(int[] data)
    {
      var result = new List<int>();
      foreach (var t in data.Where(t => !result.Contains(t)))
      {
        result.Add(t);
      }
      return result;
    }

    private Image GenerateImage(SampledFieldGeometry sampledFieldGeometry, Geometry geometry, int resX = 128,
      int resY = 128)
    {
      if (geometry == null || sampledFieldGeometry == null || geometry.getNumCoordinateComponents() < 2)
        return new Bitmap(1, 1);

      try
      {
        if (_Field == null || _Data == null)
          return new Bitmap(1, 1);


        var uncompressedLength = _Data.getUncompressedLength();
        if (uncompressedLength == 0)
          _Data.uncompress();
        uncompressedLength = _Data.getUncompressedLength();
        var array = new int[uncompressedLength];
        _Data.getUncompressed(array);

        var values = GetUniqueValues(array);

        var z = Util.SaveInt(txtZ.Text, 0);
        if (z >= _Field.getNumSamples3())
          z = _Field.getNumSamples3() - 1;
        if (z < 0) z = 0;

        var result = new Bitmap(_Field.getNumSamples1(), _Field.getNumSamples2());
        for (long i = 0; i < _Field.getNumSamples1(); ++i)
        {
          for (long j = 0; j < _Field.getNumSamples2(); ++j)
          {
            var index =
              GetIndexFor(array[i + _Field.getNumSamples1()*j + _Field.getNumSamples1()*_Field.getNumSamples2()*z]);

            result.SetPixel((int) i, (int) j, GetColorForIndex(index));
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
      if (Current == null) return -1;

      for (long i = 0; i < Current.getNumSampledVolumes(); ++i)
      {
        var current = Current.getSampledVolume(i);
        var currentValue = current.getSampledValue();
        var currentIntValue = (uint) currentValue;
        if (Math.Abs(currentValue - value) < 1e-10)
          return (int) i;
        if (currentIntValue == (byte) value)
          return (int) i;
        if (current.isSetMinValue() && current.isSetMaxValue() &&
            (value >= current.getMinValue() && value <= current.getMaxValue()))
          return (int) i;
      }

      return -1;
    }

    private Color GetColorForIndex(int index)
    {
      switch (index)
      {
        case -1:
          return Color.Transparent;
        default:
        case 0:
          return Color.Black;
        case 1:
          return Color.Red;
        case 2:
          return Color.Blue;
        case 3:
          return Color.Green;
        case 4:
          return Color.Yellow;
        case 5:
          return Color.Goldenrod;
        case 6:
          return Color.Lime;
        case 7:
          return Color.LightBlue;
        case 8:
          return Color.LightSalmon;
        case 9:
          return Color.MediumAquamarine;
      }
    }

    private void OnReorderClick(object sender, EventArgs e)
    {
    }

    public override void InvalidateSelection()
    {
      SpatialGeometry = null;
      Current = null;
      InitializeFrom(null, null);
    }

    private void OnTrackChanged(object sender, EventArgs e)
    {
      if (SpatialGeometry == null)
        return;
      if (SpatialGeometry.getNumCoordinateComponents() < 3)
        return;

      txtZ.Text = trackBar1.Value.ToString();
      if (Current == null)
        return;
      InitializeFrom(SpatialGeometry, Current.getSpatialId());
    }

    private void OnUpdateImage(object sender, EventArgs e)
    {
      if (Current == null)
        return;
      InitializeFrom(SpatialGeometry, Current.getSpatialId());
    }

    private void OnReorder(object sender, EventArgs e)
    {
      if (Current == null)
        return;

      var vols = new List<SampledVolume>();
      for (var i = (int) Current.getNumSampledVolumes() - 1; i >= 0; --i)
        vols.Add(Current.removeSampledVolume(i));
      foreach (var vol in vols)
      {
        Current.addSampledVolume(vol);
      }


      InitializeFrom(SpatialGeometry, Current.getSpatialId());
    }

    private void OnImageLoad(object sender, EventArgs e)
    {
      using (var dialog = new OpenFileDialog {Filter = "TIFF files|*.tif|All files|*.*"})
      {
        if (dialog.ShowDialog() == DialogResult.OK)
        {
          LoadImage(dialog.FileName);
        }
      }
    }

    private void LoadImage(string fileName)
    {
      var image = Image.FromFile(fileName) as Bitmap;
      if (Current == null)
      {
        Current = SpatialGeometry.createSampledFieldGeometry();
      }
      Current.setSpatialId(Path.GetFileNameWithoutExtension(fileName));
      _Field = Current.createSampledField();
      _Field.setNumSamples1(image.Width);
      _Field.setNumSamples2(image.Height);
      _Field.setNumSamples3(1);
      var data = new int[image.Width*image.Height];
      var count = 0;
      for (var i = 0; i < image.Width; ++i)
        for (var j = 0; j < image.Height; ++j)
        {
          var currentColor = image.GetPixel(i, j);

          var value = (int) (currentColor.GetBrightness()*10f);
          //if (currentColor.B > 10 && currentColor.G > 10 && currentColor.R > 10)
          //{ 
          //  value = 1;
          //}
          data[count++] = value;
        }

      var values = GetUniqueValues(data);
      var col = 0;
      foreach (var val in values)
      {
        var vol = Current.createSampledVolume();
        vol.setSpatialId(string.Format("vol_{0}", col++));
        vol.setSampledValue(val);
        vol.setMinValue(val);
        vol.setMaxValue(val);
      }

      _Data = _Field.createImageData();
      _Data.setDataType("uncompressed");
      _Data.setSamples(data, data.Length);
      _Data.uncompress();


      InitializeFrom(SpatialGeometry, Current.getSpatialId());
    }

    private void OnImageSave(object sender, EventArgs e)
    {
      using (var dialog = new SaveFileDialog {Filter = "TIFF files|*.tif|All files|*.*"})
      {
        if (dialog.ShowDialog() == DialogResult.OK)
        {
          thumbGeometry.Image.Save(dialog.FileName, ImageFormat.Tiff);
        }
      }
    }

    private void OnUserDeletedRow(object sender, DataGridViewRowEventArgs e)
    {
      if (Current == null) return;
      var id = e.Row.Cells[0].Value as string;
      Current.removeSampledVolume(id);
      InitializeFrom(SpatialGeometry, Current.getSpatialId());
    }
  }
}