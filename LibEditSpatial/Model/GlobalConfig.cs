using System.Collections.Generic;

namespace LibEditSpatial.Model
{
  public class GlobalConfig
  {
    public bool WriteVTK { get; set; }
    public string VTKname { get; set; }
    public int Overlap { get; set; }
    public int IntegrationOrder { get; set; }
    public int SubSampling { get; set; }
    public string TimeStepping { get; set; }
    public string ExplicitSolver { get; set; }
    public string ImplicitSolver { get; set; }
    public string SBMLFile { get; set; }

    public static GlobalConfig Default
    {
      get
      {
        return new GlobalConfig
        {
          WriteVTK = true,
          ExplicitSolver = "RK4",
          ImplicitSolver = "Alexander2",
          IntegrationOrder = 2,
          SubSampling = 2,
          Overlap = 1,
          TimeStepping = "implicit"
        };
      }
    }

    public static GlobalConfig FromDict(Dictionary<string, string> dict)
    {
      var result = new GlobalConfig
      {
        WriteVTK = dict.GetBool("writeVTK"),
        VTKname = dict.GetString("VTKname"),
        Overlap = dict.Get<int>("overlap"),
        IntegrationOrder = dict.Get<int>("integrationorder"),
        SubSampling = dict.Get<int>("subsampling"),
        TimeStepping = dict.GetString("timestepping"),
        ExplicitSolver = dict.GetString("explicitsolver"),
        ImplicitSolver = dict.GetString("implicitsolver"),
        SBMLFile = dict.GetString("sbmlfile")
      };

      return result;
    }

    public Dictionary<string, string> ToDict()
    {
      var result = new Dictionary<string, string>();
      result["writeVTK"] = WriteVTK ? "yes" : "no";
      result["VTKname"] = VTKname;
      result["overlap"] = Overlap.ToString();
      result["integrationorder"] = IntegrationOrder.ToString();
      result["subsampling"] = SubSampling.ToString();
      result["timestepping"] = TimeStepping;
      result["explicitsolver"] = ExplicitSolver;
      result["implicitsolver"] = ImplicitSolver;
      result["sbmlfile"] = SBMLFile;
      return result;
    }
  }
}