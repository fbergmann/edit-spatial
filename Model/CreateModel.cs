using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EditSpatial.Model
{
  public class SpatialSpecies
  {
    public string Id { get; set; }
    
    public string MinBoundaryX { get; set; }
    public string MinBoundaryY { get; set; }
    
    public string MaxBoundaryX { get; set; }
    public string MaxBoundaryY { get; set; }

    public string DiffusionX { get; set; }
    public string DiffusionY { get; set; }

    public string InitialCondition { get; set; }
  }

  public class AnalyticSettings
  {
    public string Type { get; set;  }
    public string Math { get; set; }
  }

  public class GeometrySettings
  {
    public double Xmin { get; set; }
    public double Xmax { get; set; }
    public double Ymin { get; set; }
    public double Ymax { get; set; }

    public List<AnalyticSettings> AnalyticDomains { get; set; }

    public GeometrySettings()
    {
      AnalyticDomains = new List<AnalyticSettings>();
    }

  }

  public class CreateModel
  {
    public List<SpatialSpecies> Species { get; set; }
    public GeometrySettings Geometry { get; set;  }

    public CreateModel()
    {
      Species = new List<SpatialSpecies>();
      Geometry = new GeometrySettings();
    }

  }
}
