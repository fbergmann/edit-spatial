using System;
using System.Collections.Generic;
using System.Linq;
using libsbmlcs;

namespace EditSpatial.Model
{
  public class ReactionInfo : IDisposable
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
        var list = Reactants.Select(sri => sri.Compartment).ToList();
        list.AddRange(Products.Select(sri => sri.Compartment).ToList());
        return list.Distinct().ToList();
      }
    }

    private void AnalyzeReaction()
    {
      for (var i = 0; i < Reaction.getNumReactants(); ++i)
      {
        var reference = Reaction.getReactant(i);
        if (reference != null && reference.isSetSpecies())
        {
          var species = Model.getSpecies(reference.getSpecies());
          if (species == null || !species.isSetCompartment()) continue;
          Reactants.Add(new SpeciesReferenceInfo
          {
            Id = species.getId(),
            Compartment = species.getCompartment(),
            Stoichiometry = reference.getStoichiometry()
          });
        }
      }

      for (var i = 0; i < Reaction.getNumProducts(); ++i)
      {
        var reference = Reaction.getProduct(i);
        if (reference != null && reference.isSetSpecies())
        {
          var species = Model.getSpecies(reference.getSpecies());
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

    public void Dispose()
    {
      if (Model != null)
      Model.Dispose();
    }
  }
}
