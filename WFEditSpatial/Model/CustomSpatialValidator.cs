using libsbmlcs;

namespace EditSpatial.Model
{
  public class CustomSpatialValidator : SBMLValidator
  {
    public CustomSpatialValidator()
    {
    }


    public CustomSpatialValidator(CustomSpatialValidator orig)
      : base(orig)
    {
    }

    public override SBMLValidator clone()
    {
      return new CustomSpatialValidator(this);
    }


    private bool containsSpatialMath(ASTNode node, libsbmlcs.Model model)
    {
      for (int i = 0; i < node.getNumChildren(); ++i)
      {
        if (containsSpatialMath(node.getChild(i), model))
          return true;
      }

      if (!node.isName()) return false;

      SBase element = model.getElementBySId(node.getName());

      if (element == null) return false;

      if (element.getTypeCode() == libsbml.SBML_SPECIES)
      {
        var plug = (SpatialSpeciesRxnPlugin) (element.getPlugin("spatial"));
        if (plug == null) return false;
        return plug.getIsSpatial();
      }

      return false;
    }

    private bool dependencesAreSpatial(SBase s, libsbmlcs.Model model)
    {
      if (model == null || s == null || !s.isSetId()) return false;

      Rule rule = model.getRule(s.getId());
      if (rule == null || rule.getTypeCode() != libsbml.SBML_ASSIGNMENT_RULE) return false;

      var ar = (AssignmentRule) (rule);
      if (!ar.isSetMath()) return false;

      return containsSpatialMath(ar.getMath(), model);
    }

    public override long validate()
    {
      // if we don't have a model we don't apply this validator.
      if (getDocument() == null || getModel() == null)
        return 0;

      int numErrors = 0;

      libsbmlcs.Model model = getModel();
      for (int i = 0; i < model.getNumSpecies(); ++i)
      {
        Species species = model.getSpecies(i);
        var plug = (SpatialSpeciesRxnPlugin) species.getPlugin("spatial");
        if (plug == null) continue;

        if (plug.isSetIsSpatial() && plug.getIsSpatial()) continue;

        if (dependencesAreSpatial(species, model))
        {
          numErrors++;
          getErrorLog().add(new SBMLError(99999, 3, 1,
            string.Format(
              "This model has a non-spatial species, with rules involving spatial species. Please change the flag of '{0}'",
              species.getId()),
            0, 0,
            libsbml.LIBSBML_SEV_ERROR, // or LIBSBML_SEV_ERROR if you want to stop
            libsbml.LIBSBML_CAT_SBML // or whatever category you prefer
            ));
        }
      }
      return numErrors;
    }
  }
}