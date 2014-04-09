using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SBW;

namespace EditSpatial
{
  delegate void doAnalysisDelegate(string model);
  interface ISBWAnalyzer
  {
    [Help("Loads a SBML string into EditSpatial")]
    void doAnalysis(string model);
  }
}
