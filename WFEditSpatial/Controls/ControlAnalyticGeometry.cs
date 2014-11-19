﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Windows.Forms;
using EditSpatial.Model;
using LibEditSpatial.Model;
using libsbmlcs;
using Image = System.Drawing.Image;

namespace EditSpatial.Controls
{
  public partial class ControlAnalyticGeometry : BaseSpatialControl
  {
    public ControlAnalyticGeometry()
    {
      InitializeComponent();
      IsInitializing = false;
      RowsAdded = new List<int>();
    }

    public List<int> RowsAdded { get; set; }

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


    private void CommitAddedRows(AnalyticGeometry analytic)
    {
      if (RowsAdded.Count > 0 && analytic != null)
      {
        for (int i = RowsAdded.Count - 1; i >= 0; i--)
        {
          DataGridViewRow row = grid.Rows[RowsAdded[i]];
          AnalyticVolume vol = analytic.createAnalyticVolume();
          vol.setSpatialId(row.Cells[0].Value as string);
          vol.setFunctionType(row.Cells[1].Value as string);
          long ordinal = 0;
          if (long.TryParse(row.Cells[2].Value as string, out ordinal))
            vol.setOrdinal(ordinal);
          vol.setDomainType(row.Cells[3].Value as string);
          vol.setMath(libsbml.parseL3Formula(row.Cells[4].Value as string));
          RowsAdded.RemoveAt(i);
        }
      }
    }

    public void InitializeFrom(Geometry geometry, AnalyticGeometry analytic)
    {
      IsInitializing = true;
      try
      {
        if (geometry == null)
        {
          txtId.Text = "";
          grid.Rows.Clear();
          SpatialGeometry = geometry;
          thumbGeometry.Image = thumbGeometry.InitialImage;
          return;
        }

        CommitAddedRows(analytic);

        grid.Rows.Clear();
        SpatialGeometry = geometry;
        thumbGeometry.Image = thumbGeometry.InitialImage;
        Current = analytic;
        if (analytic == null) return;

        txtId.Text = analytic.getSpatialId();

        for (long i = 0; i < analytic.getNumAnalyticVolumes(); ++i)
        {
          AnalyticVolume vol = analytic.getAnalyticVolume(i);
          string spatialId = vol.getSpatialId();
          grid.Rows.Add(spatialId, vol.getFunctionType(), vol.getOrdinal().ToString(), vol.getDomainType(),
            libsbml.formulaToL3String(vol.getMath()));
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
      finally
      {
        IsInitializing = false;
      }
    }

    public void InitializeFrom(Geometry geometry, string id)
    {
      if (geometry == null)
      {
        txtId.Text = id;
        grid.Rows.Clear();
        SpatialGeometry = geometry;
        thumbGeometry.Image = thumbGeometry.InitialImage;
        return;
      }

      var analytic = geometry.getGeometryDefinition(id) as AnalyticGeometry;
      InitializeFrom(geometry, analytic);
    }

    public override void SaveChanges()
    {
      if (Current == null || SpatialGeometry == null) return;

      Current.setSpatialId(txtId.Text);

      CommitAddedRows(Current);

      for (int i = 0; i < grid.Rows.Count && i < Current.getNumAnalyticVolumes(); ++i)
      {
        DataGridViewRow row = grid.Rows[i];
        if (row.IsNewRow) continue;
        AnalyticVolume current = Current.getAnalyticVolume(i);
        current.setSpatialId((string) row.Cells[0].Value);
        current.setFunctionType((string) row.Cells[1].Value);
        current.setOrdinal(Util.SaveInt((string) row.Cells[2].Value, 0L));
        current.setDomainType((string) row.Cells[3].Value);
        current.setMath(libsbml.parseL3Formula((string) row.Cells[4].Value));
      }

      Current.ExpandMath();
    }


    private DmpModel GenerateDmp(int thumbSize)
    {
      if (Current == null || SpatialGeometry == null) return null;

      try
      {
        Current.ExpandMath();

        var formulas = new List<Tuple<int, ASTNode>>();
        for (long i = 0; i < Current.getNumAnalyticVolumes(); ++i)
        {
          AnalyticVolume current = Current.getAnalyticVolume(i);
          formulas.Add(new Tuple<int, ASTNode>((int)current.getOrdinal(), current.getMath()));
        }
        formulas.Sort((a, b) => b.Item1.CompareTo(a.Item1));

        CoordinateComponent range1 = SpatialGeometry.getCoordinateComponent(0);
        double r1Min = range1.getBoundaryMin().getValue();
        double r1Max = range1.getBoundaryMax().getValue();
        CoordinateComponent range2 = SpatialGeometry.getCoordinateComponent(1);
        double r2Min = range2.getBoundaryMin().getValue();
        double r2Max = range2.getBoundaryMax().getValue();

        double r3Max = SpatialGeometry.getNumCoordinateComponents() == 3
          ? SpatialGeometry.getCoordinateComponent(2).getBoundaryMax().getValue()
          : 0;
        double r3Min = SpatialGeometry.getNumCoordinateComponents() == 3
          ? SpatialGeometry.getCoordinateComponent(2).getBoundaryMin().getValue()
          : 0;

        var resX = thumbSize;
        var resY = thumbSize;

        var result = new DmpModel(resX, resY) 
        { 
          MinX = r1Min, 
          MaxX = r1Max, 
          MinY = r2Min, 
          MaxY = r2Max
        };

        for (int nX = 0; nX < resX; ++nX)
        {
          double x = r1Min
                     +
                     (r1Max - r1Min) /
                     resX * nX;
          for (int nY = 0; nY < resY; ++nY)
          {
            double y = r2Min
                       +
                       (r2Max - r2Min) /
                       resY * nY;

            for (int index = 0; index < formulas.Count; index++)
            {
              Tuple<int, ASTNode> item = formulas[index];
              double isInside = Util.Evaluate(item.Item2,
                new List<string>
                {
                  "x",
                  "y",
                  "z",
                  "width",
                  "height",
                  "depth",
                  "Xmin",
                  "Xmax",
                  "Ymin",
                  "Ymax",
                  "Zmin",
                  "Zmax"
                },
                new List<double> { x, y, CurrentZ, r1Max, r2Max, r3Max, r1Min, r1Max, r2Min, r2Max, r3Min, r3Max },
                new List<Tuple<string, double>>()
                );
              if (Math.Abs((isInside - 1.0)) < 1E-10)
              {
                result[nX, nY] = item.Item1;
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

      return null;

    }

    private void ExportDmp(string fileName, int thumbSize)
    {
      var dmp = GenerateDmp(thumbSize);
      if (dmp != null)
        dmp.SaveAs(fileName);
    }

    private Image GenerateImage(AnalyticGeometry analytic, Geometry geometry, int resX = 128, int resY = 128)
    {
      if (geometry == null || analytic == null || geometry.getNumCoordinateComponents() < 2)
        return new Bitmap(1, 1);

      try
      {
        Current.ExpandMath();

        var formulas = new List<Tuple<int, ASTNode>>();
        for (long i = 0; i < analytic.getNumAnalyticVolumes(); ++i)
        {
          AnalyticVolume current = analytic.getAnalyticVolume(i);
          formulas.Add(new Tuple<int, ASTNode>((int) current.getOrdinal(), current.getMath()));
        }
        formulas.Sort((a, b) => b.Item1.CompareTo(a.Item1));

        CoordinateComponent range1 = geometry.getCoordinateComponent(0);
        double r1Min = range1.getBoundaryMin().getValue();
        double r1Max = range1.getBoundaryMax().getValue();
        CoordinateComponent range2 = geometry.getCoordinateComponent(1);
        double r2Min = range2.getBoundaryMin().getValue();
        double r2Max = range2.getBoundaryMax().getValue();

        double r3Max = geometry.getNumCoordinateComponents() == 3
          ? geometry.getCoordinateComponent(2).getBoundaryMax().getValue()
          : 0;
        double r3Min = geometry.getNumCoordinateComponents() == 3
          ? geometry.getCoordinateComponent(2).getBoundaryMin().getValue()
          : 0;


        var result = new Bitmap(resX, resY);
        for (int i = 0; i < resX; ++i)
        {
          double x = r1Min
                     +
                     (r1Max - r1Min)/
                     resX*i;
          for (int j = 0; j < resY; ++j)
          {
            double y = r2Min
                       +
                       (r2Max - r2Min)/
                       resY*j;

            for (int index = 0; index < formulas.Count; index++)
            {
              Tuple<int, ASTNode> item = formulas[index];
              double isInside = Util.Evaluate(item.Item2,
                new List<string>
                {
                  "x",
                  "y",
                  "z",
                  "width",
                  "height",
                  "depth",
                  "Xmin",
                  "Xmax",
                  "Ymin",
                  "Ymax",
                  "Zmin",
                  "Zmax"
                },
                new List<double> {x, y, CurrentZ, r1Max, r2Max, r3Max, r1Min, r1Max, r2Min, r2Max, r3Min, r3Max},
                new List<Tuple<string, double>>()
                );
              if (Math.Abs((isInside - 1.0)) < 1E-10)
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
        case 4:
          return Color.Chocolate;
        case 5:
          return Color.SlateGray;
      }
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
        AnalyticVolume current = geometry.getAnalyticVolume(i);
        list.Add(new Tuple<int, int>((int) i, (int) current.getOrdinal()));
      }

      IOrderedEnumerable<Tuple<int, int>> ordered = list.OrderByDescending(item => item.Item2);
      foreach (var item in ordered)
      {
        AnalyticVolume current = geometry.getAnalyticVolume(item.Item1);
        current.setOrdinal((geometry.getNumAnalyticVolumes() - 1) - item.Item2);
      }
    }

    private void SortedOrder(AnalyticGeometry geometry)
    {
      if (geometry == null)
        return;
      if (geometry.getNumAnalyticVolumes() == 0)
        return;
      var list = new List<Tuple<int, int, string>>();
      for (long i = 0; i < geometry.getNumAnalyticVolumes(); ++i)
      {
        AnalyticVolume current = geometry.getAnalyticVolume(i);
        list.Add(new Tuple<int, int, string>((int) i, (int) current.getOrdinal(), current.getSpatialId()));
      }

      IOrderedEnumerable<Tuple<int, int, string>> ordered = list.OrderByDescending(item => item.Item2);
      ListOfAnalyticVolumes volList = geometry.getListOfAnalyticVolumes();
      foreach (var item in ordered)
      {
        AnalyticVolume vol = volList.remove(item.Item3);
        volList.insert(item.Item2, vol);
      }
    }


    private void OnReorderClick(object sender, EventArgs e)
    {
      if (Current == null)
        return;

      FlipOrder(Current);
      InitializeFrom(SpatialGeometry, Current.getSpatialId());
    }

    private void OnSortClick(object sender, EventArgs e)
    {
      if (Current == null)
        return;

      SortedOrder(Current);
      InitializeFrom(SpatialGeometry, Current.getSpatialId());
    }


    private void OnUpdateImage(object sender, EventArgs e)
    {
      if (Current == null)
        return;

      IsInitializing = true;
      SaveChanges();
      IsInitializing = false;
      InitializeFrom(SpatialGeometry, Current.getSpatialId());
    }

    private void OnTrackChanged(object sender, EventArgs e)
    {
      if (SpatialGeometry == null)
        return;
      if (SpatialGeometry.getNumCoordinateComponents() < 3)
        return;

      CoordinateComponent range1 = SpatialGeometry.getCoordinateComponent(2);
      double r1Min = range1.getBoundaryMin().getValue();
      double r1Max = range1.getBoundaryMax().getValue();
      double x = r1Min
                 +
                 (r1Max - r1Min)/
                 trackBar1.Maximum*trackBar1.Value;
      txtZ.Text = x.ToString();
      if (Current == null)
        return;
      InitializeFrom(SpatialGeometry, Current.getSpatialId());
    }


    public override void InvalidateSelection()
    {
      Current = null;
      SpatialGeometry = null;
      InitializeFrom(null, string.Empty);
    }

    private void OnExportTiffClick(object sender, EventArgs e)
    {
      if (grid.SelectedRows.Count == 0) return;
      long ordinal = 0;
      long.TryParse(grid.SelectedRows[0].Cells[2].Value as string, out ordinal);

      CoordinateComponent range1 = SpatialGeometry.getCoordinateComponent(0);
      double r1Min = range1.getBoundaryMin().getValue();
      double r1Max = range1.getBoundaryMax().getValue();
      CoordinateComponent range2 = SpatialGeometry.getCoordinateComponent(1);
      double r2Min = range2.getBoundaryMin().getValue();
      double r2Max = range2.getBoundaryMax().getValue();

      Image image = Current.GenerateTiff(SpatialGeometry, (int) r1Max, (int) r2Max, (int) ordinal, CurrentZ);

      using (var dialog = new SaveFileDialog {Filter = "TIFF files|*.tif|All files|*.*"})
      {
        if (dialog.ShowDialog() == DialogResult.OK)
        {
          image.Save(dialog.FileName, ImageFormat.Tiff);
        }
      }
    }

    private void OnRowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
    {
      if (Current == null || IsInitializing || e.RowIndex - 1 < 0) return;

      RowsAdded.Add(e.RowIndex - 1);
    }

    private void OnMakeFirstClick(object sender, EventArgs e)
    {
      if (Current == null || IsInitializing || grid.SelectedRows.Count != 1) return;

      var selected = grid.SelectedRows[0].Cells[0].Value as string;
      for (int i = 0; i < Current.getNumAnalyticVolumes(); ++i)
      {
        AnalyticVolume current = Current.getAnalyticVolume(i);
        if (current == null) continue;
        if (current.getSpatialId() != selected) continue;

        Current.removeAnalyticVolume(i);
        Current.getListOfAnalyticVolumes().insertAndOwn(0, current);
        InitializeFrom(SpatialGeometry, Current.getSpatialId());
        break;
      }
    }

    private void cmdExport_Click(object sender, EventArgs e)
    {
      using (var dlg = new SaveFileDialog{ Title = "Export DMP", Filter = "DMP files|*.dmp|All files|*.*"})
      {
        if (dlg.ShowDialog() == DialogResult.OK)
          ExportDmp(dlg.FileName, ThumbSize);
      }
    }
  }
}