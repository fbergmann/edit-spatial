using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EditSpatial.Model
{
  public class GeometrySettings
  {
    public double Xmin { get; set; }
    public double Xmax { get; set; }
    public double Ymin { get; set; }
    public double Ymax { get; set; }

    public List<AnalyticSettings> AnalyticDomains { get; set; }

    public bool WrapOutside { get; set; }

    public GeometrySettings()
    {
      WrapOutside = false;
      AnalyticDomains = new List<AnalyticSettings>();
    }

  }
}
