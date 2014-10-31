using System.Collections.Generic;
using System.Linq;
using libsbmlcs;

namespace EditSpatial.Model
{
  public class ReactionInfo
  {
    public ReactionInfo(Reaction reaction)
    {
      Reactants = new List<SpeciesReferenceInfo>();
      Products = new List<SpeciesReferenceInfo>();

      Reaction = reaction;
      Model = reaction.getModel();

      AnalyzeReaction();
    }

    public Reaction Reaction { get; set; }
    public libsbmlcs.Model Model { get; set; }

    public List<SpeciesReferenceInfo> Reactants { get; set; }
    public List<SpeciesReferenceInfo> Products { get; set; }

    public List<string> CompartmentIds
    {
      get
      {
        List<string> list = Reactants.Select(sri => sri.Compartment).ToList();
        list.AddRange(Products.Select(sri => sri.Compartment).ToList());
        return list.Distinct().ToList();
      }
    }

    private void AnalyzeReaction()
    {
      for (int i = 0; i < Reaction.getNumReactants(); ++i)
      {
        SpeciesReference reference = Reaction.getReactant(i);
        if (reference != null && reference.isSetSpecies())
        {
          Species species = Model.getSpecies(reference.getSpecies());
          if (species == null || !species.isSetCompartment()) continue;
          Reactants.Add(new SpeciesReferenceInfo
          {
            Id = species.getId(),
            Compartment = species.getCompartment(),
            Stoichiometry = reference.getStoichiometry()
          });
        }
      }

      for (int i = 0; i < Reaction.getNumProducts(); ++i)
      {
        SpeciesReference reference = Reaction.getProduct(i);
        if (reference != null && reference.isSetSpecies())
        {
          Species species = Model.getSpecies(reference.getSpecies());
          if (species == null || !species.isSetCompartment()) continue;
          Products.Add(new SpeciesReferenceInfo
          {
            Id = species.getId(),
            Compartment = species.getCompartment(),
            Stoichiometry = reference.getStoichiometry()
          });
        }
      }
    }
  }
}