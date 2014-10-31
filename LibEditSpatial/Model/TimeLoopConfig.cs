using System.Collections.Generic;

namespace LibEditSpatial.Model
{
  public class TimeLoopConfig
  {
    public double Time { get; set; }
    public double InitialStep { get; set; }
    public double MinStep { get; set; }
    public double MaxStep { get; set; }
    public double PlotStep { get; set; }
    public int IncreaseRate { get; set; }

    public static TimeLoopConfig FromDict(Dictionary<string, string> dict)
    {
      var result = new TimeLoopConfig
      {
        Time = dict.Get<double>("time"),
        InitialStep = dict.Get<double>("dt"),
        MinStep = dict.Get<double>("dt_min"),
        MaxStep = dict.Get<double>("dt_max"),
        PlotStep = dict.Get<double>("dt_plot"),
        IncreaseRate = dict.Get<int>("increase_rate"),
      };
      return result;
    }

    public Dictionary<string, string> ToDict()
    {
      var result = new Dictionary<string, string>();
      result["time"] = Time.ToString();
      result["dt"] = InitialStep.ToString();
      result["dt_min"] = MinStep.ToString();
      result["dt_max"] = MaxStep.ToString();
      result["dt_plot"] = PlotStep.ToString();
      result["increase_rate"] = IncreaseRate.ToString();
      return result;
    }
  }
}