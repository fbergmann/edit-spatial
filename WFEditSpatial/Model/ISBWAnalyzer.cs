using SBW;

namespace EditSpatial.Model
{
  internal delegate void doAnalysisDelegate(string model);

  internal interface ISBWAnalyzer
  {
    [Help("Loads a SBML string into EditSpatial")]
    void doAnalysis(string model);
  }
}