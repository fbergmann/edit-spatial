using System.Collections.Generic;

namespace WFDuneRunner.Model
{
  public class DomainConfig
  {
    public string Geometry { get; set; }
    public double GeometryMin { get; set; }
    public double GeometryMax { get; set; }
    public int Dimensions { get; set; }
    public double Width { get; set; }
    public double Height { get; set; }
    public double Depth { get; set; }
    public int GridX { get; set; }
    public int GridY { get; set; }
    public int GridZ { get; set; }
    public int Refinement { get; set; }


    public static DomainConfig FromDict(Dictionary<string, string> dict)
    {
      var result = new DomainConfig
      {
        Geometry = dict.Get<string>("geometry"),
        GeometryMin = dict.Get<double>("geometry_min"),
        GeometryMax = dict.Get<double>("geometry_max"),
        Dimensions = dict.Get<int>("dim"),
        Width = dict.Get<double>("width"),
        Height = dict.Get<double>("height"),
        Depth = dict.Get<double>("depth"),
        GridX = dict.Get<int>("nx"),
        GridY = dict.Get<int>("ny"),
        GridZ = dict.Get<int>("nz"),
        Refinement = dict.Get<int>("refine"),
      };
      return result;
    }

    public Dictionary<string, string> ToDict()
    {
      var result = new Dictionary<string, string>();
      result["geometry"] = Geometry;
      result["geometry_min"] = GeometryMin.ToString();
      result["geometry_max"] = GeometryMax.ToString();
      result["dim"] = Dimensions.ToString();
      result["width"] = Width.ToString();
      result["height"] = Height.ToString();
      result["depth"] = Depth.ToString();
      result["nx"] = GridX.ToString();
      result["ny"] = GridY.ToString();
      result["nz"] = GridZ.ToString();
      result["refine"] = Refinement.ToString();
      return result;
    }
  }
}