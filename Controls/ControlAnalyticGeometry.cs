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
  public partial class ControlAnalyticGeometry : BaseSpatialControl
  {
    public Geometry SpatialGeometry { get; set; }
    public AnalyticGeometry Current { get; set; }

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

    public double CurrentZ
    {
      get
      {
        double value;
        if (double.TryParse(txtZ.Text, out value))
          return value;
        return 0;
      }
    }


    public void InitializeFrom(Geometry geometry, string id)
    {
      txtId.Text = id;
      grid.Rows.Clear();
      SpatialGeometry = geometry;
      thumbGeometry.Image = thumbGeometry.InitialImage;
      if (geometry == null) return;

      var analytic = geometry.getGeometryDefinition(id) as AnalyticGeometry;
      Current = analytic;
      if (analytic == null) return;

      for (long i = 0; i < analytic.getNumAnalyticVolumes(); ++i)
      {
        var vol = analytic.getAnalyticVolume(i);
        string spatialId = vol.getSpatialId();
        grid.Rows.Add(spatialId, vol.getFunctionType(), vol.getOrdinal(), vol.getDomainType(),
          libsbml.formulaToString(vol.getMath()));          
      }

      if (SpatialGeometry.getNumCoordinateComponents() < 3)
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
      }


      thumbGeometry.Image = GenerateImage(analytic, geometry, ThumbSize, ThumbSize);

    }


    public override void SaveChanges()
    {
      if (Current == null || SpatialGeometry == null) return;

      Current.setSpatialId(txtId.Text);
      for (int i = 0; i < grid.Rows.Count && i < Current.getNumAnalyticVolumes(); ++i)
      {
        var row = grid.Rows[i];
        var current = Current.getAnalyticVolume(i);
        current.setSpatialId((string)row.Cells[0].Value);
        current.setFunctionType((string)row.Cells[1].Value);
        current.setOrdinal((long)row.Cells[2].Value);
        current.setDomainType((string)row.Cells[3].Value);
        current.setMath(libsbml.parseFormula((string)row.Cells[4].Value));
      }

    }



    private Image GenerateImage(AnalyticGeometry analytic, Geometry geometry, int resX = 128, int resY = 128)
    {
      if (geometry == null || analytic == null || geometry.getNumCoordinateComponents() < 2)
        return new Bitmap(1, 1);

      try
      {
        var formulas = new List<Tuple<int, ASTNode>>();
        for (long i = 0; i < analytic.getNumAnalyticVolumes(); ++i)
        {
          var current = analytic.getAnalyticVolume(i);
          formulas.Add(new Tuple<int, ASTNode>( (int)current.getOrdinal(), current.getMath()));
        }
        formulas.Sort((a, b) => b.Item1.CompareTo(a.Item1));


        var range1 = geometry.getCoordinateComponent(0);
        double r1Min = range1.getBoundaryMin().getValue();
        double r1Max = range1.getBoundaryMax().getValue();
        var range2 = geometry.getCoordinateComponent(1);
        double r2Min = range2.getBoundaryMin().getValue();
        double r2Max = range2.getBoundaryMax().getValue();

        var result = new Bitmap(resX, resY);
        for (int i = 0; i < resX; ++i)
        {
          double x = r1Min
                     +
                     (r1Max - r1Min)/
                     (double) resX*(double) i;
          for (int j = 0; j < resY; ++j)
          {
            double y = r2Min
                     +
                     (r2Max - r2Min) /
                     (double)resY * (double)j;

            for (int index = 0; index < formulas.Count; index++)
            {
              var item = formulas[index];
              var isInside = Util.Evaluate(item.Item2,
                new List<string> {"x", "y", "z"},
                new List<double> {x, y, CurrentZ},
                new List<Tuple<string, double>>()
                );
              if ( Math.Abs((isInside - 1.0)) < 1E-10)
              {
                result.SetPixel(i, j, GetColorForIndex(item.Item1));
                break;
              }
            }
          }
        }
        return result;
      }
      catch
      {
        
      }
      return new Bitmap(1, 1);
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

    public ControlAnalyticGeometry()
    {
      InitializeComponent();
    }


    private void FlipOrder(AnalyticGeometry geometry)
    {
      if (geometry == null)
        return;
      if (geometry.getNumAnalyticVolumes() == 0)
        return;
      var list = new List<Tuple<int, int>>();
      for (long i = 0; i < geometry.getNumAnalyticVolumes(); ++i)
      {
        var current = geometry.getAnalyticVolume(i);
        list.Add(new Tuple<int, int>((int)i, (int)current.getOrdinal()));
      }

      var ordered = list.OrderByDescending(item => item.Item2);
      foreach (var item in ordered)
      {
        var current = geometry.getAnalyticVolume(item.Item1);
        current.setOrdinal((geometry.getNumAnalyticVolumes()-1)- item.Item2);
      }


    }


    private void OnReorderClick(object sender, EventArgs e)
    {
      if (Current == null)
        return;

      FlipOrder(Current);
      InitializeFrom(SpatialGeometry, Current.getSpatialId());

    }

    private void OnUpdateImage(object sender, EventArgs e)
    {
      if (Current == null)
        return;
      InitializeFrom(SpatialGeometry, Current.getSpatialId());
    }

    private void OnTrackChanged(object sender, EventArgs e)
    {
      if (SpatialGeometry == null)
        return;
      if (SpatialGeometry.getNumCoordinateComponents() < 3)
        return;

      var range1 = SpatialGeometry.getCoordinateComponent(2);
      double r1Min = range1.getBoundaryMin().getValue();
      double r1Max = range1.getBoundaryMax().getValue();
      double x = r1Min
              +
              (r1Max - r1Min) /
              (double)trackBar1.Maximum * (double)trackBar1.Value;
      txtZ.Text = x.ToString();
      if (Current == null)
        return;
      InitializeFrom(SpatialGeometry, Current.getSpatialId());
    }


    public override void InvalidateSelection()
    {
      Current = null;
      SpatialGeometry = null;
      InitializeFrom(null, null);
    }

    private Image GenerateTiff(AnalyticGeometry analytic, Geometry geometry, int resX = 128, int resY = 128, int ordinal=1)
    {
      if (geometry == null || analytic == null || geometry.getNumCoordinateComponents() < 2)
        return new Bitmap(1, 1);

      try
      {
        var formulas = new List<Tuple<int, ASTNode>>();
        for (long i = 0; i < analytic.getNumAnalyticVolumes(); ++i)
        {
          var current = analytic.getAnalyticVolume(i);
          formulas.Add(new Tuple<int, ASTNode>((int)current.getOrdinal(), current.getMath()));
        }
        formulas.Sort((a, b) => b.Item1.CompareTo(a.Item1));


        var range1 = geometry.getCoordinateComponent(0);
        double r1Min = range1.getBoundaryMin().getValue();
        double r1Max = range1.getBoundaryMax().getValue();
        var range2 = geometry.getCoordinateComponent(1);
        double r2Min = range2.getBoundaryMin().getValue();
        double r2Max = range2.getBoundaryMax().getValue();

        var result = new Bitmap(resX, resY);
        for (int i = 0; i < resX; ++i)
        {
          double x = r1Min
                     +
                     (r1Max - r1Min) /
                     (double)resX * (double)i;
          for (int j = 0; j < resY; ++j)
          {
            double y = r2Min
                     +
                     (r2Max - r2Min) /
                     (double)resY * (double)j;

            for (int index = 0; index < formulas.Count; index++)
            {
              var item = formulas[index];
              var isInside = Util.Evaluate(item.Item2,
                new List<string> { "x", "y", "z" },
                new List<double> { x, y, CurrentZ },
                new List<Tuple<string, double>>()
                );
              if (Math.Abs((isInside - 1.0)) < 1E-10)
              {
                result.SetPixel(i, j,  ordinal == index ? Color.White : Color.Black);
                break;
              }
            }
          }
        }
        return result;
      }
      catch
      {

      }
      return new Bitmap(1, 1);
    }

    private void OnExportTiffClick(object sender, EventArgs e)
    {
      if (grid.SelectedRows.Count == 0) return;
      var ordinal = (long)grid.SelectedRows[0].Cells[2].Value;

      var range1 = SpatialGeometry.getCoordinateComponent(0);
      double r1Min = range1.getBoundaryMin().getValue();
      double r1Max = range1.getBoundaryMax().getValue();
      var range2 = SpatialGeometry.getCoordinateComponent(1);
      double r2Min = range2.getBoundaryMin().getValue();
      double r2Max = range2.getBoundaryMax().getValue();

      var image = GenerateTiff(Current, SpatialGeometry, (int)r1Max, (int)r2Max, (int)ordinal);

      using (var dialog = new SaveFileDialog { Filter = "TIFF files|*.tif|All files|*.*" })
      {
        if (dialog.ShowDialog() == DialogResult.OK)
        {
          image.Save(dialog.FileName, System.Drawing.Imaging.ImageFormat.Tiff);
        }
      }

    }
  }
}
