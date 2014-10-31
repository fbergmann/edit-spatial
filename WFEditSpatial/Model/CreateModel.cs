using System.Collections.Generic;

namespace EditSpatial.Model
{
  public class CreateModel
  {
    public CreateModel()
    {
      Species = new List<SpatialSpecies>();
      Geometry = new GeometrySettings();
    }

    public List<SpatialSpecies> Species { get; set; }
    public GeometrySettings Geometry { get; set; }

    public SpatialSpecies this[string id]
    {
      get { return Species.Find(species => species.Id == id); }
    }
  }
}