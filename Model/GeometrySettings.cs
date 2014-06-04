using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EditSpatial.Model
{

  public enum GeometryType
  {
    Default, 
    Analytic, 
    Sample
  }

  public class GeometrySettings
  {
    public double Xmin { get; set; }
    public double Xmax { get; set; }
    public double Ymin { get; set; }
    public double Ymax { get; set; }
    public double Zmin { get; set; }
    public double Zmax { get; set; }

    public List<string> UsedSymbols { get; set;  }

    public List<AnalyticSettings> AnalyticDomains { get; set; }

    public GeometryType Type { get; set; } 

    public bool WrapOutside { get; set; }

    public GeometrySettings()
    {
      WrapOutside = false;
      AnalyticDomains = new List<AnalyticSettings>();
      UsedSymbols = new List<string>();
    }

  }
}
