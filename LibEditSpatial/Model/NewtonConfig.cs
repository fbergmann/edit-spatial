using System.Collections.Generic;

namespace LibEditSpatial.Model
{
  public class NewtonConfig
  {
    public int LinearVerbosity { get; set; }
    public double ReassembleThreshold { get; set; }
    public int LineSearchMaxIterations { get; set; }
    public int MaxIterations { get; set; }
    public double AbsoluteLimit { get; set; }
    public double Reduction { get; set; }
    public double LinearReduction { get; set; }
    public double LineSearchDampingFactor { get; set; }
    public int Verbosity { get; set; }

    public static NewtonConfig FromDict(Dictionary<string, string> dict)
    {
      var result = new NewtonConfig
      {
        LinearVerbosity = dict.Get<int>("LinearVerbosity"),
        ReassembleThreshold = dict.Get<double>("ReassembleThreshold"),
        LineSearchMaxIterations = dict.Get<int>("LineSearchMaxIterations"),
        MaxIterations = dict.Get<int>("MaxIterations"),
        AbsoluteLimit = dict.Get<double>("AbsoluteLimit"),
        Reduction = dict.Get<double>("Reduction"),
        LinearReduction = dict.Get<double>("LinearReduction"),
        LineSearchDampingFactor = dict.Get<double>("LineSearchDampingFactor"),
        Verbosity = dict.Get<int>("Verbosity"),
      };
      return result;
    }

    public Dictionary<string, string> ToDict()
    {
      var result = new Dictionary<string, string>();
      result["LinearVerbosity"] = LinearVerbosity.ToString();
      result["ReassembleThreshold"] = ReassembleThreshold.ToString();
      result["LineSearchMaxIterations"] = LineSearchMaxIterations.ToString();
      result["MaxIterations"] = MaxIterations.ToString();
      result["AbsoluteLimit"] = AbsoluteLimit.ToString();
      result["Reduction"] = Reduction.ToString();
      result["LinearReduction"] = LinearReduction.ToString();
      result["LineSearchDampingFactor"] = LineSearchDampingFactor.ToString();
      result["Verbosity"] = Verbosity.ToString();
      return result;
    }
  }
}