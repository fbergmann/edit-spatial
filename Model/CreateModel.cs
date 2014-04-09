using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EditSpatial.Model
{
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
