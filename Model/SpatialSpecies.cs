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

    public string BCType { get; set; }

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
      return Equals((SpatialSpecies)obj);
    }


  }
}
