using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EditSpatial.Model
{
  public class SpatialSpecies
  {
    protected bool Equals(SpatialSpecies other)
    {
      return string.Equals(Id, other.Id);
    }

    public override int GetHashCode()
    {
      return (Id != null ? Id.GetHashCode() : 0);
    }

    public string Id { get; set; }

    public double MinBoundaryX { get; set; }
    public double MinBoundaryY { get; set; }

    public double MaxBoundaryX { get; set; }
    public double MaxBoundaryY { get; set; }

    public double DiffusionX { get; set; }
    public double DiffusionY { get; set; }

    public string InitialCondition { get; set; }

    public override string ToString()
    {
      return Id;
    }


    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj is string) return Id == (string)obj;
      if (obj.GetType() != this.GetType()) return false;
      return Equals((SpatialSpecies) obj);
    }
    

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

    public SpatialSpecies this[string id]
    {
      get
      {
        return Species.Find(species => species.Id == id);
      }
    }

    public CreateModel()
    {
      Species = new List<SpatialSpecies>();
      Geometry = new GeometrySettings();
    }

  }
}
