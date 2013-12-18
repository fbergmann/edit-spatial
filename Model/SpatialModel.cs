using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using libSbl2SBML;
using libsbmlcs;
using SBMLSupport;

namespace EditSpatial.Model
{
  public class SpatialModel
  {
    public SBMLDocument Document { get; set; }

    public Geometry Geometry
    {
      get
      {
        if (!IsSpatial) return null;

        var plugin = (SpatialModelPlugin) Document.getModel().getPlugin("spatial");
        if (plugin == null) return null;
        return plugin.getGeometry();

      }
    }
    
    public string FileName { get; set; }

    public bool IsSpatial
    {
      get
      {
        if (Document == null) return false;
        if (Document.getPlugin("spatial") == null) return false;
        if (Document.getModel() == null) return false;
        if (Document.getModel().getPlugin("spatial") == null) return false;
        return true;
      }
    }

    public SpatialModel()
    {
      FileName = "untitled.xml";
    }

    public static SpatialModel FromFile(string fileName)
    {
      var model = new SpatialModel
      {
        Document = libsbml.readSBMLFromFile(fileName), 
        FileName = fileName

      };
      model.Document.setConsistencyChecks(libsbml.LIBSBML_CAT_UNITS_CONSISTENCY, false);
      model.Document.setConsistencyChecks(libsbml.LIBSBML_CAT_MODELING_PRACTICE, false);
      return model;
    }

    public static SpatialModel FromJarnac(string content)
    {
      NOM.Namespaces = new List<string>();      
      TModel model = TModel.ReadModel(content);
      model.RemoveAnnotation();
      model.RemoveNotes();
      return FromString(model.toSBML(false));
    }

    public static SpatialModel FromString(string content)
    {
      var model = new SpatialModel
      {
        Document = libsbml.readSBMLFromString(content), 
        FileName = "fromstring.xml"
      };
      model.Document.setConsistencyChecks(libsbml.LIBSBML_CAT_UNITS_CONSISTENCY, false);
      model.Document.setConsistencyChecks(libsbml.LIBSBML_CAT_MODELING_PRACTICE, false);
      return model;
    }

    public void SaveTo(string filename)
    {
      if (Document == null) return;
      FileName = filename;
      libsbml.writeSBMLToFile(Document, filename);
    }

    public string ToSBML()
    {
      if (Document == null) return "<none>";
      return libsbml.writeSBMLToString(Document).Replace("\n", Environment.NewLine);
    }

    public string ToJarnac()
    {
      if (Document == null) return "";
      return libSbl2SBML.convertSBML.ToSBL(ToSBML());
    }

    public void FixCommonErrors()
    {
      if (Document == null) return;

      FixDiffusionCoefficients();

      if (Document.getNumErrors(libsbml.LIBSBML_SEV_ERROR) == 0) return;

      for (int i = 0; i < Document.getNumErrors(); ++i)
      {
        var current = Document.getError(i);
        long id = current.getErrorId();
        string message = current.getMessage();

        switch (current.getErrorId())
        {
          case  99109:
          {
            var plugin = Document.getPlugin("req");
            if (plugin == null) break;
            Document.setPackageRequired("req", false);
            break;
          }
          case 20517:
          {
            for (int j = 0; j < Document.getModel().getNumCompartments(); ++j)
            {
              var comp = Document.getModel().getCompartment(j);
              if (!comp.isSetConstant())
                comp.setConstant(true);
            }
            break;
          }
          case 20623:
          {
            for (int j = 0; j < Document.getModel().getNumSpecies(); ++j)
            {
              var species = Document.getModel().getSpecies(j);
              if (!species.isSetConstant())
                species.setConstant(false);
            }
            break;
          }

          case 21116:
          {
            for (int j = 0; j < Document.getModel().getNumReactions(); ++j)
            {
              var reaction = Document.getModel().getReaction(j);
              for (int k = 0; k < reaction.getNumProducts(); ++k)
              {
                var reference = reaction.getProduct(k);
                if (!reference.isSetConstant())
                  reference.setConstant(true);
              }
              for (int k = 0; k < reaction.getNumReactants(); ++k)
              {
                var reference = reaction.getReactant(k);
                if (!reference.isSetConstant())
                  reference.setConstant(true);
              }
            }
            break;
            
          }

          case 21110:
          {
            for (int j = 0; j < Document.getModel().getNumReactions(); ++j)
            {
              var reaction = Document.getModel().getReaction(j);
              if (!reaction.isSetFast())
                reaction.setFast(false);
              if (!reaction.isSetReversible())
                reaction.setReversible(false);
            }
            break;
            
          }

          case 20706 :
          {
            for (int j = 0; j < Document.getModel().getNumParameters(); ++j)
            {
              var parameter = Document.getModel().getParameter(j);
              if (!parameter.isSetConstant())
                parameter.setConstant(false);
            }
            break;
            
          }

          case 21121:
          {
            var match = System.Text.RegularExpressions.Regex.Match(message, ".*'(?<speciesId>\\w+?)'.*'(?<reactionId>\\w+?)'.*", System.Text.RegularExpressions.RegexOptions.ExplicitCapture);
            if (match.Success)
            {
              var speciesId = match.Groups["speciesId"].Value;
              var reactionId = match.Groups["reactionId"].Value;
              var reaction = Document.getModel().getReaction(reactionId);
              if (reaction != null)
              {
                if (reaction.getModifier(speciesId) == null)
                {
                  var modifier = reaction.createModifier();
                  modifier.setSpecies(speciesId);
                }
              }
            }

            break;
          }

          default:
            Debug.WriteLine("Don't know what to do with:  {0}: {1}", id, message);
            break;
        }

      }
      Document.getErrorLog().clearLog();
    }

    public void FixDiffusionCoefficients()
    {
      if (Document == null || !IsSpatial || Geometry == null) return;

      var numCooords = Geometry.getNumCoordinateComponents();

      var dict = new Dictionary<string, List<Tuple<int, Parameter>>>();

      for (int i = 0; i < Document.getModel().getNumParameters(); ++i)
      {
        var current = Document.getModel().getParameter(i);
        if (current == null) continue;
        var plug = current.getPlugin("spatial") as SpatialParameterPlugin;
        if (plug == null) continue;
        if (plug.getType() != libsbml.SBML_SPATIAL_DIFFUSIONCOEFFICIENT) continue;
        var diff = plug.getDiffusionCoefficient();
        if (diff == null || !diff.isSetCoordinateIndex()) continue;
        if (!dict.ContainsKey(diff.getVariable()))
          dict[diff.getVariable()] = new List<Tuple<int, Parameter>>();
        dict[diff.getVariable()].Add(new Tuple<int, Parameter>((int)diff.getCoordinateIndex(), current));
      }

      foreach (var key in dict.Keys)
      {
        var current = dict[key];
        if (current == null || current.Count == 0 || current.Count == numCooords) continue;

        foreach (var entry in current)
        {
          var param = entry.Item2;
          if (param == null) continue;
          var plug = param.getPlugin("spatial") as SpatialParameterPlugin;
          if (plug == null) continue;
          if (plug.getType() != libsbml.SBML_SPATIAL_DIFFUSIONCOEFFICIENT) continue;
          var diff = plug.getDiffusionCoefficient();
          if (diff == null || !diff.isSetCoordinateIndex()) continue;
          diff.unsetCoordinateIndex();
        }

        //for (int i = 0; i < 3; ++i)
        //{
        //  var param = current.FirstOrDefault(e => e.Item1 == i);
        //  if (param != null) continue;
        //}
      }

    }

    public bool ConvertToSpatial(List<string> spatialElements, List<Tuple<string, string>> initialConditions, object geometryDescription)
    {
      if (Document == null) return true;

      if (Document.getLevel() < 3)
      {
        //if (!Document.setLevelAndVersion(3, 1, false))
        //  return false;

        var prop = new ConversionProperties(new SBMLNamespaces(3, 1));
        prop.addOption("strict", false);
        prop.addOption("setLevelAndVersion", true);
        prop.addOption("ignorePackages", true);

        if (Document.convert(prop) != libsbml.LIBSBML_OPERATION_SUCCESS)
        {
          return false;
        }
      }
      var result = Document.enablePackage(SpatialExtension.getXmlnsL3V1V1(), "spatial", true);
      Document.setPackageRequired("spatial", true);
      result = Document.enablePackage(RequiredElementsExtension.getXmlnsL3V1V1(), "req", true);
      Document.setPackageRequired("req", false);

      var model = Document.getModel();
      var plugin = (SpatialModelPlugin) model.getPlugin("spatial");
      if (plugin == null)
        return false;
      var geometry = plugin.getGeometry();
      if (geometry == null)
        return false;

      double xmax=100;
      double ymax=100;
      // create coordinate components
      geometry.setCoordinateSystem("Cartesian");
      var coord = geometry.createCoordinateComponent();
      coord.setSpatialId("x");
      coord.setSbmlUnit("um");
      coord.setComponentType("cartesianX");
      coord.setIndex(0);
      var min = coord.createBoundaryMin();
      min.setSpatialId("Xmin");
      min.setValue(0);
      var max = coord.createBoundaryMax();
      max.setSpatialId("Xmax");
      max.setValue(xmax);

      coord = geometry.createCoordinateComponent();
      coord.setSpatialId("y");
      coord.setSbmlUnit("um");
      coord.setComponentType("cartesianY");
      coord.setIndex(1);
      min = coord.createBoundaryMin();
      min.setSpatialId("Ymin");
      min.setValue(0);
      max = coord.createBoundaryMax();
      max.setSpatialId("Ymax");
      max.setValue(ymax);

      var param = model.createParameter();
      param.initDefaults();
      param.setId("x");
      param.setValue(0);
      var pplug = (SpatialParameterPlugin)param.getPlugin("spatial");
      var symbol = pplug.getSpatialSymbolReference();
      symbol.setSpatialId("x");
      symbol.setType("coordinateComponent");      
      SetRequiredElements(param);

      param = model.createParameter();
      param.initDefaults();
      param.setId("y");
      param.setValue(0);
      pplug = (SpatialParameterPlugin)param.getPlugin("spatial");
      symbol = pplug.getSpatialSymbolReference();
      symbol.setSpatialId("y");
      symbol.setType("coordinateComponent");
      SetRequiredElements(param);



      if (model.getNumCompartments() == 1)
      {

        var domainType = geometry.createDomainType();
        domainType.setSpatialId("domainType0");
        domainType.setSpatialDimensions(3);

        var domain = geometry.createDomain();
        domain.setSpatialId("domain0");
        domain.setDomainType("domainType0");
        var point = domain.createInteriorPoint();
        point.setCoord1(xmax/2.0);
        point.setCoord2(ymax/2.0);
        point.setCoord3(0);

        var def = geometry.createAnalyticGeometry();
        def.setSpatialId("geometry0");
        var vol = def.createAnalyticVolume();
        vol.setSpatialId("vol0");
        vol.setDomainType("domainType0");
        vol.setFunctionType("layered");
        vol.setOrdinal(0);
        vol.setMath(libsbml.parseFormula("1"));

        var comp = model.getCompartment(0);
        var cplug = (SpatialCompartmentPlugin) comp.getPlugin("spatial");
        if (cplug == null)
          return false;

        var map = cplug.getCompartmentMapping();
        map.setSpatialId("mapping0");
        map.setCompartment(comp.getId());
        map.setDomainType(domainType.getSpatialId());
        map.setUnitSize(1);

      }

      else
      {
        var numCompartments = model.getNumCompartments();
        double length = xmax/numCompartments;
        var def = geometry.createAnalyticGeometry();
        def.setSpatialId("geometry");

        var order = OrderCompartments(model);
        int i = 0;
        AdjacentDomains lastAdjacent = null;
        Domain domain = null;
        Domain memDomain = null;
        foreach (var compid in order)
        {

          var domainType = geometry.createDomainType();
          domainType.setSpatialId("domainType_" + compid);
          domainType.setSpatialDimensions(3);

          domain = geometry.createDomain();
          domain.setSpatialId("domain_" + compid);
          domain.setDomainType("domainType_" + compid);
          var point = domain.createInteriorPoint();
          point.setCoord1(i*length + length/2.0);
          point.setCoord2(ymax/2.0);
          point.setCoord3(0);

          if (lastAdjacent != null)
          {
            var adj = geometry.createAdjacentDomains();
            adj.setSpatialId("adj_" + memDomain.getSpatialId() + "_" + domain.getSpatialId());
            adj.setDomain1(memDomain.getSpatialId());
            adj.setDomain2(domain.getSpatialId());
          }

          var vol = def.createAnalyticVolume();
          vol.setSpatialId("vol_" + compid);
          vol.setDomainType("domainType_" + compid);
          vol.setFunctionType("layered");
          vol.setOrdinal(i);
          vol.setMath(libsbml.parseFormula(
            i == 0 ? 
            "1"
            //            string.Format(
            //"and(geq(x, {0}), lt(x, {1}))", (i * length), ((i + 1) * length))
            
            :
            string.Format(
            //"and(and(geq(x, {0}), lt(x, {1})), and(geq(y, {0}), lt(y, {1})))", (i * length), ((i + 1) * length))
            "and(geq(x, {0}), lt(x, {1}))", (i * length), ((i + 1) * length))
            ));

          var comp = model.getCompartment(compid);
          var cplug = (SpatialCompartmentPlugin)comp.getPlugin("spatial");
          if (cplug == null)
            return false;

          var map = cplug.getCompartmentMapping();
          map.setSpatialId("mapping_" + compid);
          map.setCompartment(comp.getId());
          map.setDomainType(domainType.getSpatialId());
          map.setUnitSize(1);

          if (i + 1 < numCompartments)
          {
            domainType = geometry.createDomainType();
            domainType.setSpatialId("mem_" + compid);
            domainType.setSpatialDimensions(2);

            comp = model.createCompartment();
            comp.initDefaults();
            comp.setId("c_mem_" + compid);
            comp.setSize(1);

            cplug = (SpatialCompartmentPlugin)comp.getPlugin("spatial");
            if (cplug == null)
              return false;

            map = cplug.getCompartmentMapping();
            map.setSpatialId("mapping_" + comp.getId());
            map.setCompartment(comp.getId());
            map.setDomainType(domainType.getSpatialId());
            map.setUnitSize(1);

            memDomain = geometry.createDomain();
            memDomain.setSpatialId("domain_mem_" + compid);
            memDomain.setDomainType("mem_" + compid);

            lastAdjacent = geometry.createAdjacentDomains();
            lastAdjacent.setSpatialId("adj_" + memDomain.getSpatialId() + "_" + domain.getSpatialId());
            lastAdjacent.setDomain1(memDomain.getSpatialId());
            lastAdjacent.setDomain2(domain.getSpatialId());

          }

          ++i;
        }
      }


      for (int i = 0; i < model.getNumSpecies(); i++)
      {
        var species = model.getSpecies(i);
        var splug = (SpatialSpeciesRxnPlugin)species.getPlugin("spatial");
        if (splug == null) continue;
        splug.setIsSpatial(false);
        var id = species.getId();
        if (spatialElements.Contains(id))
        {
          splug.setIsSpatial(true);
          SetRequiredElements(species);

          param = model.createParameter();
          param.initDefaults();
          param.setId(id + "_diff_X");
          param.setValue(0.01);
          pplug = (SpatialParameterPlugin)param.getPlugin("spatial");
          var diff = pplug.getDiffusionCoefficient();
          diff.setVariable(id);
          diff.setCoordinateIndex(0);
          SetRequiredElements(param);

          param = model.createParameter();
          param.initDefaults();
          param.setId(id + "_diff_Y");
          param.setValue(0.01);
          pplug = (SpatialParameterPlugin)param.getPlugin("spatial");
          diff = pplug.getDiffusionCoefficient();
          diff.setVariable(id);
          diff.setCoordinateIndex(1);
          SetRequiredElements(param);

          param = model.createParameter();
          param.initDefaults();
          param.setId(id + "_BC_Xmin");
          param.setValue(0);
          pplug = (SpatialParameterPlugin)param.getPlugin("spatial");
          var bc = pplug.getBoundaryCondition();
          bc.setVariable(id);
          bc.setCoordinateBoundary("Xmin");
          bc.setType("Flux");
          SetRequiredElements(param);

          param = model.createParameter();
          param.initDefaults();
          param.setId(id + "_BC_Xmax");
          param.setValue(0);
          pplug = (SpatialParameterPlugin)param.getPlugin("spatial");
          bc = pplug.getBoundaryCondition();
          bc.setVariable(id);
          bc.setCoordinateBoundary("Xmax");
          bc.setType("Flux");
          SetRequiredElements(param);

          param = model.createParameter();
          param.initDefaults();
          param.setId(id + "_BC_Ymin");
          param.setValue(0);
          pplug = (SpatialParameterPlugin)param.getPlugin("spatial");
          bc = pplug.getBoundaryCondition();
          bc.setVariable(id);
          bc.setCoordinateBoundary("Ymin");
          bc.setType("Flux");
          SetRequiredElements(param);

          param = model.createParameter();
          param.initDefaults();
          param.setId(id + "_BC_Ymax");
          param.setValue(0);
          pplug = (SpatialParameterPlugin)param.getPlugin("spatial");
          bc = pplug.getBoundaryCondition();
          bc.setVariable(id);
          bc.setCoordinateBoundary("Ymax");
          bc.setType("Flux");
          SetRequiredElements(param);

        }
      }

      for (int i = 0; i < model.getNumReactions(); i++)
      {
        var reaction = model.getReaction(i);
        var rplug = (SpatialSpeciesRxnPlugin) reaction.getPlugin("spatial");
        if (rplug == null) continue;
        SetRequiredElements(reaction);
        rplug.setIsLocal( GetSpeciesReferenceIdsContainedIn(reaction, spatialElements).Count > 0);
      }

      return true;

    }

    private List<string> OrderCompartments(libsbmlcs.Model model)
    {
      var list = new List<string>();

      var orders = new Dictionary<string, List<string>>();
      var matrix = new int[model.getNumReactions(), model.getNumCompartments()];
      var reactions = new List<string>();
      for (int i = 0; i < model.getNumReactions(); i++)
        reactions.Add(model.getReaction(i).getId());
      var compartments = new List<string>();
      for (int i = 0; i < model.getNumCompartments(); i++)
        compartments.Add(model.getCompartment(i).getId());

      for (int i = 0; i < model.getNumReactions(); i++)
      {
        var reaction = model.getReaction(i);
        var id = reaction.getId();
        for (int j = 0; j < reaction.getNumReactants(); j++)
        {
          var reference = reaction.getReactant(j);
          if (!reference.isSetSpecies()) continue;
          var species = model.getSpecies(reference.getSpecies());
          if (species == null) continue;
          
          if (!orders.ContainsKey(id))
            orders[id] = new List<string>();

          if (!orders[id].Contains(species.getCompartment()))
            orders[id].Add(species.getCompartment());

          matrix[reactions.IndexOf(id), compartments.IndexOf(species.getCompartment())] = 1; 

        }

        for (int j = 0; j < reaction.getNumProducts(); j++)
        {
          var reference = reaction.getProduct(j);
          if (!reference.isSetSpecies()) continue;
          var species = model.getSpecies(reference.getSpecies());
          if (species == null) continue;

          if (!orders.ContainsKey(id))
            orders[id] = new List<string>();

          if (!orders[id].Contains(species.getCompartment()))
            orders[id].Add(species.getCompartment());

          matrix[reactions.IndexOf(id), compartments.IndexOf(species.getCompartment())] = 1;
        }

        for (int j = 0; j < reaction.getNumModifiers(); j++)
        {
          var reference = reaction.getModifier(j);
          if (!reference.isSetSpecies()) continue;
          var species = model.getSpecies(reference.getSpecies());
          if (species == null) continue;

          if (!orders.ContainsKey(id))
            orders[id] = new List<string>();

          if (!orders[id].Contains(species.getCompartment()))
            orders[id].Add(species.getCompartment());

          matrix[reactions.IndexOf(id), compartments.IndexOf(species.getCompartment())] = 1;
        }
      }

      foreach (var order in orders.Values)
        order.Sort();

      var uniqueOrders = new List<List<string>>();

      foreach (var order in orders.Values)
      {
        if (!Contains(uniqueOrders, order))
          uniqueOrders.Add(order);
      }

      for (int i = uniqueOrders.Count - 1; i >= 0; i--)
        if (uniqueOrders[i].Count < 2)
          uniqueOrders.RemoveAt(i);

      var counts = CountOccurances(uniqueOrders);

      var max = counts.Values.Max();

      while (max > 1)
      {
        // get first one that occurs only once
        var current = counts.FirstOrDefault(e => e.Value == 1);
        var id = current.Key;
        
        var order = RemoveIdFrom(uniqueOrders, id);
        if (list.Contains(id))
          continue;
        list.Add(id);

        foreach (var item in order)
        {
          if (item.Equals(id))
            continue;
          list.Add(item);
        }

        counts = CountOccurances(uniqueOrders);
        max = counts.Values.Max();


      }

      // add remaining
      for (int i = 0; i < model.getNumCompartments(); ++i)
      {
        var current = model.getCompartment(i);
        if (!list.Contains(current.getId())) list.Add(current.getId());
      }

      return list;
    }

    private List<string> RemoveIdFrom(List<List<string>> uniqueOrders, string id)
    {
      var item = uniqueOrders.FirstOrDefault(e => e.Contains(id));
      if (item == null) return null;
      uniqueOrders.Remove(item);
      return item;
    }

    private static Dictionary<string, int> CountOccurances(List<List<string>> orders)
    {
      var counts = new Dictionary<string, int>();
      foreach (var entries in orders)
      {
        foreach (var key in entries)
        {
          if (!counts.ContainsKey(key))
            counts[key] = 0;
          counts[key]++;
        }
      }
      return counts;
    }


    private bool Contains(List<List<string>> uniqueOrders, List<string> order)
    {      
      string stringOrder = Combine(order);
      foreach (var item in uniqueOrders)
      {
        if (Combine(item) == stringOrder)
          return true;
      }
      return false;
    }

    private string Combine(List<string> order)
    {
      var builder = new StringBuilder();
      for (int i = 0; i < order.Count; i++)
      {
        builder.Append(order[i]);
        if (i + 1 < order.Count)
          builder.Append(", ");
      }
      return builder.ToString();
    }

    private List<string> GetSpeciesReferenceIdsContainedIn(Reaction reaction, List<string> spatialElements)
    {
      var result = new List<string>();
      if (reaction == null || reaction.getSBMLDocument() == null) return result;
      var model = reaction.getSBMLDocument().getModel();
      if (model == null) return result;
      for (int i = 0; i < reaction.getNumReactants(); ++i)
      {
        var current = reaction.getReactant(i);
        var id = current.getSpecies();
        var plug = (SpatialSpeciesRxnPlugin)current.getPlugin("spatial");
        var isSpatial = plug != null && plug.getIsSpatial();
        if (isSpatial && !result.Contains(id))
          result.Add(id);
      }
      for (int i = 0; i < reaction.getNumProducts(); ++i)
      {
        var current = reaction.getProduct(i);
        var id = current.getSpecies();
        var plug = (SpatialSpeciesRxnPlugin)current.getPlugin("spatial");
        var isSpatial = plug != null && plug.getIsSpatial();
        if (isSpatial && !result.Contains(id))
          result.Add(id);
      }
      for (int i = 0; i < reaction.getNumModifiers(); ++i)
      {
        var current = reaction.getModifier(i);
        var id = current.getSpecies();
        var plug = (SpatialSpeciesRxnPlugin)model.getSpecies(id).getPlugin("spatial");
        var isSpatial = plug != null && plug.getIsSpatial();
        if (isSpatial && !result.Contains(id))
          result.Add(id);
      }
      return result;
    }

    private static void SetRequiredElements(SBase sbase)
    {
      var req = (RequiredElementsSBasePlugin) sbase.getPlugin("req");
      if (req == null) return;
      req.setCoreHasAlternateMath(true);
      req.setMathOverridden("spatial");
    }
  }
}
