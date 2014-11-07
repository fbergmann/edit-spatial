using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using EditSpatial.Converter;
using libSbl2SBML;
using libsbmlcs;
using SBMLSupport;

namespace EditSpatial.Model
{
  public class SpatialModel
  {
    internal static CustomSpatialValidator CustomSpatialValidator = new CustomSpatialValidator();

    public void ExportDuneSBML(string filename)
    {
      try
      {
        var converter = new DuneConverter(Document);
        File.WriteAllText(filename, converter.ToSBML());
      }
      catch 
      {
        
      }
    }

    public SpatialModel()
    {
      DefaultHeight = DefaultWidth = 50;
      FileName = "untitled.xml";
    }

    public double DefaultWidth { get; set; }
    public double DefaultHeight { get; set; }

    public double Width
    {
      get
      {
        if (!IsSpatial || Geometry == null) return DefaultWidth;

        return GetBoundaryMaxValue(0, DefaultWidth);
      }
    }

    public double Height
    {
      get
      {
        if (!IsSpatial || Geometry == null) return DefaultHeight;

        return GetBoundaryMaxValue(1, DefaultHeight);
      }
    }

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

    public bool HaveModel
    {
      get { return Document != null && Document.getModel() != null; }
    }

    private double GetBoundaryMaxValue(int val, double defaultValue = 0)
    {
      if (val >= Geometry.getNumCoordinateComponents()) return defaultValue;
      try
      {
        return Geometry.getCoordinateComponent(val).getBoundaryMax().getValue();
      }
      catch
      {
        return defaultValue;
      }
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
      model.Document.addValidator(CustomSpatialValidator);
      model.Document.validateSBML();
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
      model.Document.addValidator(CustomSpatialValidator);
      model.Document.validateSBML();
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
      try
      {
        return convertSBML.ToSBL(ToSBML());
      }
      catch
      {
        return "";
      }
    }


    public void MoveAllRulesToAssignments()
    {
      if (Document == null || Document.getModel() == null)
        return;

      libsbmlcs.Model model = Document.getModel();
      for (int i = (int) model.getNumRules() - 1; i >= 0; i--)
      {
        var current = model.getRule(i) as AssignmentRule;
        if (current == null) continue;
        model.MoveRuleToAssignment(current.getVariable());
      }

      OnModelChanged();
    }


    public bool FixCommonErrors()
    {
      bool result = false;

      if (Document == null) return result;


      FixDiffusionCoefficients();

      if (Document.getNumErrors(libsbml.LIBSBML_SEV_ERROR) == 0) return result;

      for (int i = 0; i < Document.getNumErrors(); ++i)
      {
        SBMLError current = Document.getError(i);
        long id = current.getErrorId();
        string message = current.getMessage();

        switch (current.getErrorId())
        {
          case 99109:
          {
            SBasePlugin plugin = Document.getPlugin("req");
            if (plugin == null) break;
            Document.setPackageRequired("req", false);
            result = true;
            break;
          }
          case 20517:
          {
            for (int j = 0; j < Document.getModel().getNumCompartments(); ++j)
            {
              Compartment comp = Document.getModel().getCompartment(j);
              if (!comp.isSetConstant())
                comp.setConstant(true);
            }
            result = true;
            break;
          }
          case 20623:
          {
            for (int j = 0; j < Document.getModel().getNumSpecies(); ++j)
            {
              Species species = Document.getModel().getSpecies(j);
              if (!species.isSetConstant())
                species.setConstant(false);
            }
            result = true;
            break;
          }

          case 21116:
          {
            for (int j = 0; j < Document.getModel().getNumReactions(); ++j)
            {
              Reaction reaction = Document.getModel().getReaction(j);
              for (int k = 0; k < reaction.getNumProducts(); ++k)
              {
                SpeciesReference reference = reaction.getProduct(k);
                if (!reference.isSetConstant())
                  reference.setConstant(true);
              }
              for (int k = 0; k < reaction.getNumReactants(); ++k)
              {
                SpeciesReference reference = reaction.getReactant(k);
                if (!reference.isSetConstant())
                  reference.setConstant(true);
              }
            }
            result = true;
            break;
          }

          case 21110:
          {
            for (int j = 0; j < Document.getModel().getNumReactions(); ++j)
            {
              Reaction reaction = Document.getModel().getReaction(j);
              if (!reaction.isSetFast())
                reaction.setFast(false);
              if (!reaction.isSetReversible())
                reaction.setReversible(false);
            }
            result = true;
            break;
          }

          case 20706:
          {
            for (int j = 0; j < Document.getModel().getNumParameters(); ++j)
            {
              Parameter parameter = Document.getModel().getParameter(j);
              if (!parameter.isSetConstant())
                parameter.setConstant(false);
            }
            result = true;
            break;
          }

          case 20610:
          {
            // have both rule and reaction on species this error is there in old versions of 
            // vcell code, that would export initial assignments as assignment rules
            Match match = Regex.Match(message.Split(new[] {'\n'})[2],
              ".*'(?<speciesId>\\w+?)'.*'(?<reactionId>\\w+?)'.*",
              RegexOptions.ExplicitCapture);
            if (match.Success)
            {
              string speciesId = match.Groups["speciesId"].Value;

              Document.getModel().MoveRuleToAssignment(speciesId);
              result = true;
            }
            break;
          }
          case 21121:
          {
            Match match = Regex.Match(message, ".*'(?<speciesId>\\w+?)'.*'(?<reactionId>\\w+?)'.*",
              RegexOptions.ExplicitCapture);
            if (match.Success)
            {
              string speciesId = match.Groups["speciesId"].Value;
              string reactionId = match.Groups["reactionId"].Value;
              Reaction reaction = Document.getModel().getReaction(reactionId);
              if (reaction != null)
              {
                if (reaction.getModifier(speciesId) == null)
                {
                  ModifierSpeciesReference modifier = reaction.createModifier();
                  modifier.setSpecies(speciesId);
                  result = true;
                }
              }
            }

            break;
          }


          case 99303:
          {
            // remove the unitdefinition referenced by an element
            Match match = Regex.Match(message, ".*<(?<type>\\w+?)>.*'(?<id>\\w+?)'.*",
              RegexOptions.ExplicitCapture);
            if (match.Success)
            {
              string type = match.Groups["type"].Value;
              string elementId = match.Groups["id"].Value;

              switch (type)
              {
                case "parameter":
                {
                  Parameter param = Document.getModel().getParameter(elementId);
                  if (param != null)
                  {
                    param.unsetUnits();
                    result = true;
                  }
                  break;
                }
                default:
                {
                  Debug.WriteLine("Need to fix type '{0}' und id '{1}'", type, elementId);
                  break;
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

      if (result)
        OnModelChanged();

      return result;
    }

    public void FixDiffusionCoefficients()
    {
      if (Document == null || !IsSpatial || Geometry == null) return;

      long numCooords = Geometry.getNumCoordinateComponents();

      var dict = new Dictionary<string, List<Tuple<int, Parameter>>>();

      for (int i = 0; i < Document.getModel().getNumParameters(); ++i)
      {
        Parameter current = Document.getModel().getParameter(i);
        if (current == null) continue;
        var plug = current.getPlugin("spatial") as SpatialParameterPlugin;
        if (plug == null) continue;
        if (plug.getType() != libsbml.SBML_SPATIAL_DIFFUSIONCOEFFICIENT) continue;
        DiffusionCoefficient diff = plug.getDiffusionCoefficient();
        if (diff == null || !diff.isSetCoordinateIndex()) continue;
        if (!dict.ContainsKey(diff.getVariable()))
          dict[diff.getVariable()] = new List<Tuple<int, Parameter>>();
        dict[diff.getVariable()].Add(new Tuple<int, Parameter>((int) diff.getCoordinateIndex(), current));
      }

      foreach (string key in dict.Keys)
      {
        List<Tuple<int, Parameter>> current = dict[key];
        if (current == null || current.Count == 0 || current.Count == numCooords) continue;

        foreach (var entry in current)
        {
          Parameter param = entry.Item2;
          if (param == null) continue;
          var plug = param.getPlugin("spatial") as SpatialParameterPlugin;
          if (plug == null) continue;
          if (plug.getType() != libsbml.SBML_SPATIAL_DIFFUSIONCOEFFICIENT) continue;
          DiffusionCoefficient diff = plug.getDiffusionCoefficient();
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

    public bool ConvertToL3()
    {
      if (Document.getLevel() < 3)
      {
        var prop = new ConversionProperties(new SBMLNamespaces(3, 1));
        prop.addOption("strict", false);
        prop.addOption("setLevelAndVersion", true);
        prop.addOption("ignorePackages", true);

        if (Document.convert(prop) != libsbml.LIBSBML_OPERATION_SUCCESS)
        {
          return false;
        }
      }

      Document.enablePackage(SpatialExtension.getXmlnsL3V1V1(), "spatial", true);
      Document.setPackageRequired("spatial", true);

      Document.enablePackage(RequiredElementsExtension.getXmlnsL3V1V1(), "req", true);
      Document.setPackageRequired("req", false);

      return true;
    }

    public bool ConvertToSpatial(CreateModel createModel)
    {
      if (Document == null) return true;

      if (!ConvertToL3()) return false;

      libsbmlcs.Model model = Document.getModel();
      var plugin = (SpatialModelPlugin) model.getPlugin("spatial");
      if (plugin == null)
        return false;
      Geometry geometry = plugin.getGeometry();
      if (geometry == null)
        return false;

      // create coordinate components    
      CreateCoordinateSystem(geometry, model, createModel.Geometry);

      //if (geometry.getNumGeometryDefinitions()==0)
      if (!SetupGeometry(model, geometry, createModel)) return false;

      SetupSpecies(createModel, model);

      SetIsLocalOnReactions(model);

      return true;
    }

    private void SplitReaction(libsbmlcs.Model model, Reaction reaction)
    {
      var info = new ReactionInfo(reaction);
      if (info.CompartmentIds.Count < 2) return;

      if (info.CompartmentIds.Count == 2)
      {
        // straight transport
        string comp1Id = info.CompartmentIds[0];
        Compartment comp1 = model.getCompartment(comp1Id);
        if (comp1 == null || comp1.getSpatialDimensions() == 2) return;
        string comp2Id = info.CompartmentIds[1];
        Compartment comp2 = model.getCompartment(comp2Id);
        if (comp2 == null || comp2.getSpatialDimensions() == 2) return;

        string memId = String.Format("mem_{0}_{1}", comp2Id, comp1Id);
        Compartment memComp = model.getCompartment(memId);
        if (memComp == null)
        {
          memId = String.Format("mem_{0}_{1}", comp1Id, comp2Id);
          memComp = model.getCompartment(memId);
        }
        if (memComp == null)
        {
          memComp = model.createCompartment();
          memComp.initDefaults();
          memComp.setId(memId);
          memComp.setSize(1);
          memComp.setUnits("area");
          memComp.setSpatialDimensions(2);
        }

        var math = new ASTNode(reaction.getKineticLaw().getMath());
        Reaction react1 = model.createReaction();
        react1.initDefaults();
        react1.setReversible(reaction.getReversible());
        react1.setId(String.Format("mem_{0}_{1}", comp1Id, reaction.getId()));

        var react2builder = new StringBuilder("1");
        Reaction react2 = model.createReaction();
        react2.initDefaults();
        react2.setReversible(reaction.getReversible());
        react2.setId(String.Format("mem_{0}_{1}", comp2Id, reaction.getId()));


        for (int i = 0; i < reaction.getNumReactants(); ++i)
        {
          SpeciesReference reference = reaction.getReactant(i);
          Species species = model.getSpecies(reference.getSpecies());
          if (species.getCompartment() == comp1Id)
          {
            SpeciesReference newRef = react1.createReactant();
            newRef.setSpecies(reference.getSpecies());
            newRef.setStoichiometry(reference.getStoichiometry());
          }
          else
          {
            string newId = "mem_" + species.getId();
            Species newSpecies = model.getSpecies(newId);
            if (newSpecies == null)
            {
              newSpecies = model.createSpecies();
              newSpecies.initDefaults();
              newSpecies.setId(newId);
              newSpecies.setName(species.getId());
              newSpecies.setCompartment(memId);
              newSpecies.setInitialConcentration(0);
            }

            SpeciesReference newRef = react2.createProduct();
            newRef.setSpecies(newSpecies.getId());
            newRef.setStoichiometry(reference.getStoichiometry());

            newRef = react1.createReactant();
            newRef.setSpecies(newSpecies.getId());
            newRef.setStoichiometry(reference.getStoichiometry());

            react2builder.Append(" * " + newSpecies.getId());
            math.renameSIdRefs(species.getId(), newSpecies.getId());
          }
        }


        for (int i = 0; i < reaction.getNumProducts(); ++i)
        {
          SpeciesReference reference = reaction.getProduct(i);
          Species species = model.getSpecies(reference.getSpecies());
          if (species.getCompartment() == comp1Id)
          {
            SpeciesReference newRef = react1.createProduct();
            newRef.setSpecies(species.getId());
            newRef.setStoichiometry(reference.getStoichiometry());
          }
          else
          {
            string newId = "mem_" + species.getId();
            Species newSpecies = model.getSpecies(newId);
            if (newSpecies == null)
            {
              newSpecies = model.createSpecies();
              newSpecies.initDefaults();
              newSpecies.setId(newId);
              newSpecies.setName(species.getId());
              newSpecies.setCompartment(memId);
              newSpecies.setInitialConcentration(0);
            }

            SpeciesReference newRef = react1.createProduct();
            newRef.setSpecies(newSpecies.getId());
            newRef.setStoichiometry(reference.getStoichiometry());

            newRef = react2.createReactant();
            newRef.setSpecies(newSpecies.getId());
            newRef.setStoichiometry(reference.getStoichiometry());
            react2builder.Append(" * " + newSpecies.getId());

            newRef = react2.createProduct();
            newRef.setSpecies(species.getId());
            newRef.setStoichiometry(reference.getStoichiometry());
            math.renameSIdRefs(species.getId(), newSpecies.getId());
          }
        }

        KineticLaw law = react2.createKineticLaw();
        law.setFormula(react2builder.ToString());

        law = react1.createKineticLaw();
        for (int l = 0; l < reaction.getKineticLaw().getNumLocalParameters(); ++l)
        {
          Parameter param = reaction.getKineticLaw().getParameter(l);
          Parameter newParam = law.createParameter();
          newParam.initDefaults();
          newParam.setId(param.getId());
          newParam.setValue(param.getValue());
        }
        law.setMath(math);

        model.removeReaction(reaction.getId());
        reaction.Dispose();
      }
    }

    /// <summary>
    ///   This method first goes ahead, and looks through reactions,
    ///   whether a transport reaction can be found. If so, it tests
    ///   whether the compartments differ in spatialDimensions (i.e
    ///   checks that the transport goes via a membrane), if it does
    ///   not, it will create a membrane compartment, and split the
    ///   reaction such that it will go from the compartment -&gt; to
    ///   the membrane -&gt; to the other compartment.
    /// </summary>
    /// <param name="model">the model to check</param>
    internal void CorrectCompartmentsAndTransport(libsbmlcs.Model model)
    {
      for (int i = (int) model.getNumReactions() - 1; i >= 0; --i)
      {
        Reaction reaction = model.getReaction(i);
        List<string> comps = Util.GetCompartmentsFromReaction(reaction);
        if (comps.Count < 2)
          continue;

        SplitReaction(model, reaction);
      }
    }

    private bool SetupGeometry(libsbmlcs.Model model, Geometry geometry,
      CreateModel createModel)
    {
      long numCompartments = model.getNumCompartments();
      if (numCompartments == 1 || MainForm.Settings.IgnoreMultiCompartments)
        return SetupUnicompartmentalGeometry(model, geometry, createModel);

      int num3DComps = 0;
      for (int index = 0; index < numCompartments; ++index)
      {
        Compartment current = model.getCompartment(index);
        if (current == null || !current.isSetSpatialDimensions()) continue;
        if (current.getSpatialDimensions() == 3) num3DComps ++;
      }

      double length = createModel.Geometry.Xmax/num3DComps;
      AnalyticGeometry def = geometry.createAnalyticGeometry();
      def.setSpatialId("geometry");

      List<string> order = OrderCompartments(model);
      int i = 0;
      AdjacentDomains lastAdjacent = null;
      Domain memDomain = null;
      for (int j = 0; j < order.Count; j++)
      {
        Compartment comp = model.getCompartment(order[j]);
        var cplug = (SpatialCompartmentPlugin) comp.getPlugin("spatial");
        if (cplug == null)
          return false;
        DomainType domainType = geometry.createDomainType();
        domainType.setSpatialId("domainType_" + order[j]);
        domainType.setSpatialDimensions((int) comp.getSpatialDimensionsAsDouble());
        Domain domain = geometry.createDomain();
        domain.setSpatialId("domain_" + order[j]);
        domain.setDomainType("domainType_" + order[j]);
        InteriorPoint point = domain.createInteriorPoint();
        point.setCoord1(i*length + length/2.0);
        point.setCoord2(createModel.Geometry.Ymax/2.0);
        point.setCoord3(0);
        if (lastAdjacent != null)
        {
          AdjacentDomains adj = geometry.createAdjacentDomains();
          adj.setSpatialId(String.Format("adj_{0}_{1}", memDomain.getSpatialId(), domain.getSpatialId()));
          adj.setDomain1(memDomain.getSpatialId());
          adj.setDomain2(domain.getSpatialId());
        }
        AnalyticVolume vol = def.createAnalyticVolume();
        vol.setSpatialId("vol_" + order[j]);
        vol.setDomainType("domainType_" + order[j]);
        vol.setFunctionType("layered");
        vol.setOrdinal(i);
        vol.setMath(libsbml.parseFormula(i == 0
          ? "1" // first compartment gets all
          : j == order.Count - 1
            ? string.Format("geq(x, {0})", (i*length)) // last compartment gets rest
            : string.Format("and(geq(x, {0}), lt(x, {1}))", (i*length), ((i + 1)*length))));
        CompartmentMapping map = cplug.getCompartmentMapping();
        map.setSpatialId("mapping_" + order[j]);
        map.setCompartment(comp.getId());
        map.setDomainType(domainType.getSpatialId());
        map.setUnitSize(1);

        if (i + 1 < numCompartments)
        {
          if (j + 1 < order.Count && model.getCompartment(order[j + 1]).getSpatialDimensions() == 2)
          {
            // next compartment is a 2D compartment!
            domainType = geometry.createDomainType();
            domainType.setSpatialId("domainType_" + order[j + 1]);
            domainType.setSpatialDimensions(2);

            comp = model.getCompartment(order[j + 1]);
            cplug = (SpatialCompartmentPlugin) comp.getPlugin("spatial");
            if (cplug == null)
              return false;
            map = cplug.getCompartmentMapping();
            map.setSpatialId("mapping_" + comp.getId());
            map.setCompartment(comp.getId());
            map.setDomainType(domainType.getSpatialId());
            map.setUnitSize(1);

            memDomain = geometry.createDomain();
            memDomain.setSpatialId("domain_mem_" + order[j]);
            memDomain.setDomainType(domainType.getSpatialId());
            lastAdjacent = geometry.createAdjacentDomains();
            lastAdjacent.setSpatialId(String.Format("adj_{0}_{1}", memDomain.getSpatialId(), domain.getSpatialId()));
            lastAdjacent.setDomain1(memDomain.getSpatialId());
            lastAdjacent.setDomain2(domain.getSpatialId());

            ++j; //++i;

            //vol = def.createAnalyticVolume();
            //vol.setSpatialId("vol_" + order[j]);
            //vol.setDomainType("domainType_" + order[j]);
            //vol.setFunctionType("layered");
            //vol.setOrdinal(i);
            //vol.setMath(libsbml.parseFormula(i == 0 ? "1" : //            string.Format(
            //  //"and(geq(x, {0}), lt(x, {1}))", (i * length), ((i + 1) * length))
            //string.Format(//"and(and(geq(x, {0}), lt(x, {1})), and(geq(y, {0}), lt(y, {1})))", (i * length), ((i + 1) * length))
            //"and(geq(x, {0}), lt(x, {1}))", (i * length), ((i + 1) * length))));
          }
          else
          {
            domainType = geometry.createDomainType();
            domainType.setSpatialId("mem_" + order[j]);
            domainType.setSpatialDimensions(2);
            comp = model.createCompartment();
            comp.initDefaults();
            comp.setId("c_mem_" + order[j]);
            comp.setSize(1);
            cplug = (SpatialCompartmentPlugin) comp.getPlugin("spatial");
            if (cplug == null)
              return false;
            map = cplug.getCompartmentMapping();
            map.setSpatialId("mapping_" + comp.getId());
            map.setCompartment(comp.getId());
            map.setDomainType(domainType.getSpatialId());
            map.setUnitSize(1);
            memDomain = geometry.createDomain();
            memDomain.setSpatialId("domain_mem_" + order[j]);
            memDomain.setDomainType("mem_" + order[j]);
            lastAdjacent = geometry.createAdjacentDomains();
            lastAdjacent.setSpatialId(String.Format("adj_{0}_{1}", memDomain.getSpatialId(), domain.getSpatialId()));
            lastAdjacent.setDomain1(memDomain.getSpatialId());
            lastAdjacent.setDomain2(domain.getSpatialId());
          }
        }
        ++i;
      }
      return true;
    }

    private static bool SetupUnicompartmentalGeometry(libsbmlcs.Model model, Geometry geometry, CreateModel createModel)
    {
      DomainType domainType = geometry.createDomainType();
      domainType.setSpatialId("domainType0");
      domainType.setSpatialDimensions(3);

      Domain domain = geometry.createDomain();
      domain.setSpatialId("domain0");
      domain.setDomainType("domainType0");
      InteriorPoint point = domain.createInteriorPoint();
      point.setCoord1(createModel.Geometry.Xmax/2.0);
      point.setCoord2(createModel.Geometry.Ymax/2.0);
      point.setCoord3(0);


      AnalyticVolume vol = null;
      AnalyticGeometry def = null;

      def = geometry.GetFirstAnalyticGeometry();
      if (def == null)
      {
        def = geometry.createAnalyticGeometry();
        def.setSpatialId("geometry0");
      }

      vol = def.getAnalyticVolume(0);
      if (vol == null)
      {
        vol = def.createAnalyticVolume();
        vol.setSpatialId("vol0");
        vol.setOrdinal(0);
      }

      if (!vol.isSetMath() || string.IsNullOrWhiteSpace(libsbml.formulaToString(vol.getMath())))
        vol.setMath(libsbml.parseFormula("1"));

      vol.setDomainType("domainType0");
      vol.setFunctionType("layered");


      Compartment comp = model.getCompartment(0);
      var cplug = (SpatialCompartmentPlugin) comp.getPlugin("spatial");
      if (cplug == null)
        return false;

      CompartmentMapping map = cplug.getCompartmentMapping();
      map.setSpatialId("mapping0");
      map.setCompartment(comp.getId());
      map.setDomainType(domainType.getSpatialId());
      map.setUnitSize(1);

      if (createModel.Geometry.WrapOutside)
      {
        comp = model.createCompartment();
        comp.initDefaults();
        comp.setId("__outside");
        cplug = (SpatialCompartmentPlugin) comp.getPlugin("spatial");

        domainType = geometry.createDomainType();
        domainType.setSpatialId("domainType1");
        domainType.setSpatialDimensions(3);

        domain = geometry.createDomain();
        domain.setSpatialId("domain1");
        domain.setDomainType("domainType1");
        point = domain.createInteriorPoint();
        point.setCoord1(0);
        point.setCoord2(0);
        point.setCoord3(0);

        map = cplug.getCompartmentMapping();
        map.setSpatialId("mapping1");
        map.setCompartment(comp.getId());
        map.setDomainType(domainType.getSpatialId());
        map.setUnitSize(1);

        vol.setOrdinal(1);
        vol.setMath(
          libsbml.parseFormula(
            string.Format("piecewise(1, and(geq(x, {0}), leq(x, {1}), geq(y, {2}), leq(y, {3})), 0)",
              createModel.Geometry.Xmin + 1, createModel.Geometry.Xmax - 1,
              createModel.Geometry.Ymin + 1, createModel.Geometry.Ymax - 1
              )
            ));

        vol = def.createAnalyticVolume();
        vol.setSpatialId("vol1");
        vol.setDomainType("domainType1");
        vol.setFunctionType("layered");
        vol.setOrdinal(0);
        vol.setMath(libsbml.parseFormula("1"));

        // membrane
        comp = model.createCompartment();
        comp.initDefaults();
        comp.setId("__membrane");
        cplug = (SpatialCompartmentPlugin) comp.getPlugin("spatial");

        domainType = geometry.createDomainType();
        domainType.setSpatialId("domainType2");
        domainType.setSpatialDimensions(2);

        domain = geometry.createDomain();
        domain.setSpatialId("domain2");
        domain.setDomainType("domainType2");

        map = cplug.getCompartmentMapping();
        map.setSpatialId("mapping2");
        map.setCompartment(comp.getId());
        map.setDomainType(domainType.getSpatialId());
        map.setUnitSize(1);

        AdjacentDomains adjacent = geometry.createAdjacentDomains();
        adjacent.setSpatialId("adj_1");
        adjacent.setDomain1("domain2");
        adjacent.setDomain2("domain1");

        adjacent = geometry.createAdjacentDomains();
        adjacent.setSpatialId("adj_2");
        adjacent.setDomain1("domain2");
        adjacent.setDomain2("domain0");
      }

      return true;
    }

    private static void SetupSpecies(CreateModel createModel, libsbmlcs.Model model)
    {
      List<string> spatialElements = (from s in createModel.Species select s.Id).ToList();

      for (int i = 0; i < model.getNumSpecies(); i++)
      {
        Species species = model.getSpecies(i);
        var splug = (SpatialSpeciesRxnPlugin) species.getPlugin("spatial");
        if (splug == null) continue;
        splug.setIsSpatial(false);
        string id = species.getId();
        if (!spatialElements.Contains(id)) continue;

        SpatialSpecies currentSpecies = createModel[id];
        species.setInitialExpession(currentSpecies.InitialCondition);
        splug.setIsSpatial(true);
        SetRequiredElements(species);

        Parameter temp = species.getParameterDiffusionX();
        if (temp != null && temp.isSetId())
          model.removeParameter(temp.getId());
        Parameter param = model.createParameter();
        param.initDefaults();
        param.setId(id + "_diff_X");
        param.setValue(currentSpecies.DiffusionX);
        var pplug = (SpatialParameterPlugin) param.getPlugin("spatial");
        DiffusionCoefficient diff = pplug.getDiffusionCoefficient();
        diff.setVariable(id);
        diff.setCoordinateIndex(0);
        SetRequiredElements(param);

        temp = species.getParameterDiffusionY();
        if (temp != null && temp.isSetId())
          model.removeParameter(temp.getId());
        model.removeParameter(id + "_diff_Y");
        param = model.createParameter();
        param.initDefaults();
        param.setId(id + "_diff_Y");
        param.setValue(currentSpecies.DiffusionY);
        pplug = (SpatialParameterPlugin) param.getPlugin("spatial");
        diff = pplug.getDiffusionCoefficient();
        diff.setVariable(id);
        diff.setCoordinateIndex(1);
        SetRequiredElements(param);

        temp = species.getSpatialParameter(libsbml.SBML_SPATIAL_BOUNDARYCONDITION, "Xmin");
        if (temp != null && temp.isSetId())
          model.removeParameter(temp.getId());
        param = model.createParameter();
        param.initDefaults();
        param.setId(id + "_BC_Xmin");
        param.setValue(currentSpecies.MinBoundaryX);
        pplug = (SpatialParameterPlugin) param.getPlugin("spatial");
        BoundaryCondition bc = pplug.getBoundaryCondition();
        bc.setVariable(id);
        bc.setCoordinateBoundary("Xmin");
        bc.setType(currentSpecies.BCType == "Dirichlet" ? "Value" : "Flux");
        SetRequiredElements(param);

        temp = species.getSpatialParameter(libsbml.SBML_SPATIAL_BOUNDARYCONDITION, "Xmax");
        if (temp != null && temp.isSetId())
          model.removeParameter(temp.getId());
        param = model.createParameter();
        param.initDefaults();
        param.setId(id + "_BC_Xmax");
        param.setValue(currentSpecies.MaxBoundaryX);
        pplug = (SpatialParameterPlugin) param.getPlugin("spatial");
        bc = pplug.getBoundaryCondition();
        bc.setVariable(id);
        bc.setCoordinateBoundary("Xmax");
        bc.setType(currentSpecies.BCType == "Dirichlet" ? "Value" : "Flux");
        SetRequiredElements(param);

        temp = species.getSpatialParameter(libsbml.SBML_SPATIAL_BOUNDARYCONDITION, "Ymin");
        if (temp != null && temp.isSetId())
          model.removeParameter(temp.getId());

        param = model.createParameter();
        param.initDefaults();
        param.setId(id + "_BC_Ymin");
        param.setValue(currentSpecies.MinBoundaryY);
        pplug = (SpatialParameterPlugin) param.getPlugin("spatial");
        bc = pplug.getBoundaryCondition();
        bc.setVariable(id);
        bc.setCoordinateBoundary("Ymin");
        bc.setType(currentSpecies.BCType == "Dirichlet" ? "Value" : "Flux");
        SetRequiredElements(param);

        temp = species.getSpatialParameter(libsbml.SBML_SPATIAL_BOUNDARYCONDITION, "Ymax");
        if (temp != null && temp.isSetId())
          model.removeParameter(temp.getId());

        param = model.createParameter();
        param.initDefaults();
        param.setId(id + "_BC_Ymax");
        param.setValue(currentSpecies.MaxBoundaryY);
        pplug = (SpatialParameterPlugin) param.getPlugin("spatial");
        bc = pplug.getBoundaryCondition();
        bc.setVariable(id);
        bc.setCoordinateBoundary("Ymax");
        bc.setType(currentSpecies.BCType == "Dirichlet" ? "Value" : "Flux");
        SetRequiredElements(param);
      }
    }

    private void SetIsLocalOnReactions(libsbmlcs.Model model)
    {
      for (int i = 0; i < model.getNumReactions(); i++)
      {
        Reaction reaction = model.getReaction(i);
        var rplug = (SpatialSpeciesRxnPlugin) reaction.getPlugin("spatial");
        if (rplug == null) continue;
        SetRequiredElements(reaction);
        List<string> idsContainedIn = GetSpeciesReferenceIdsContainedIn(reaction);
        bool isLocal = idsContainedIn.Count > 0;
        rplug.setIsLocal(isLocal);
      }
    }

    public void CreateCoordinateSystem(Geometry geometry, libsbmlcs.Model model,
      GeometrySettings settings)
    {
      geometry.setCoordinateSystem("Cartesian");

      CoordinateComponent coord = geometry.getCoordinateComponent("x");
      if (coord == null)
      {
        coord = geometry.createCoordinateComponent();
      }

      coord.setSpatialId("x");
      coord.setSbmlUnit("um");
      coord.setComponentType("cartesianX");
      coord.setIndex(0);

      BoundaryMin min = coord.getBoundaryMin();
      if (min == null)
        min = coord.createBoundaryMin();
      min.setSpatialId("Xmin");
      min.setValue(settings.Xmin);
      BoundaryMax max = coord.getBoundaryMax();
      if (max == null) max = coord.createBoundaryMax();
      max.setSpatialId("Xmax");
      max.setValue(settings.Xmax);

      coord = geometry.getCoordinateComponent("y");
      if (coord == null)
        coord = geometry.createCoordinateComponent();
      coord.setSpatialId("y");
      coord.setSbmlUnit("um");
      coord.setComponentType("cartesianY");
      coord.setIndex(1);
      min = coord.getBoundaryMin();
      if (min == null)
        min = coord.createBoundaryMin();
      min.setSpatialId("Ymin");
      min.setValue(settings.Ymin);
      max = coord.getBoundaryMax();
      if (max == null)
        max = coord.createBoundaryMax();
      max.setSpatialId("Ymax");
      max.setValue(settings.Ymax);

      model.removeParameter("x");
      Parameter param = model.createParameter();
      param.initDefaults();
      param.setId("x");
      param.setValue(0);
      var pplug = (SpatialParameterPlugin) param.getPlugin("spatial");
      SpatialSymbolReference symbol = pplug.getSpatialSymbolReference();
      symbol.setSpatialId("x");
      symbol.setType("coordinateComponent");
      SetRequiredElements(param, false);

      model.removeParameter("y");
      param = model.createParameter();
      param.initDefaults();
      param.setId("y");
      param.setValue(0);
      pplug = (SpatialParameterPlugin) param.getPlugin("spatial");
      symbol = pplug.getSpatialSymbolReference();
      symbol.setSpatialId("y");
      symbol.setType("coordinateComponent");
      SetRequiredElements(param, false);

      if (settings.UsedSymbols.Contains("width"))
      {
        model.removeParameter("width");
        param = model.createParameter();
        param.initDefaults();
        param.setId("width");
        param.setValue(settings.Xmax);
        pplug = (SpatialParameterPlugin) param.getPlugin("spatial");
        symbol = pplug.getSpatialSymbolReference();
        symbol.setSpatialId("Xmax");
        symbol.setType("");
        SetRequiredElements(param, false);
      }

      if (settings.UsedSymbols.Contains("height"))
      {
        model.removeParameter("height");
        param = model.createParameter();
        param.initDefaults();
        param.setId("height");
        param.setValue(settings.Ymax);
        pplug = (SpatialParameterPlugin) param.getPlugin("spatial");
        symbol = pplug.getSpatialSymbolReference();
        symbol.setSpatialId("Ymax");
        symbol.setType("");
        SetRequiredElements(param, false);
      }

      if (settings.UsedSymbols.Contains("Xmax"))
      {
        model.removeParameter("Xmax");
        param = model.createParameter();
        param.initDefaults();
        param.setId("Xmax");
        param.setValue(settings.Xmax);
        pplug = (SpatialParameterPlugin) param.getPlugin("spatial");
        symbol = pplug.getSpatialSymbolReference();
        symbol.setSpatialId("Xmax");
        symbol.setType("");
        SetRequiredElements(param, false);
      }

      if (settings.UsedSymbols.Contains("Ymax"))
      {
        model.removeParameter("Ymax");
        param = model.createParameter();
        param.initDefaults();
        param.setId("Ymax");
        param.setValue(settings.Ymax);
        pplug = (SpatialParameterPlugin) param.getPlugin("spatial");
        symbol = pplug.getSpatialSymbolReference();
        symbol.setSpatialId("Ymax");
        symbol.setType("");
        SetRequiredElements(param, false);
      }


      if (settings.UsedSymbols.Contains("Xmin"))
      {
        model.removeParameter("Xmin");
        param = model.createParameter();
        param.initDefaults();
        param.setId("Xmin");
        param.setValue(settings.Xmin);
        pplug = (SpatialParameterPlugin) param.getPlugin("spatial");
        symbol = pplug.getSpatialSymbolReference();
        symbol.setSpatialId("Xmin");
        symbol.setType("");
        SetRequiredElements(param, false);
      }

      if (settings.UsedSymbols.Contains("Ymin"))
      {
        model.removeParameter("Ymin");
        param = model.createParameter();
        param.initDefaults();
        param.setId("Ymin");
        param.setValue(settings.Ymin);
        pplug = (SpatialParameterPlugin) param.getPlugin("spatial");
        symbol = pplug.getSpatialSymbolReference();
        symbol.setSpatialId("Ymin");
        symbol.setType("");
        SetRequiredElements(param, false);
      }
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
        Reaction reaction = model.getReaction(i);
        string id = reaction.getId();
        for (int j = 0; j < reaction.getNumReactants(); j++)
        {
          SpeciesReference reference = reaction.getReactant(j);
          if (!reference.isSetSpecies()) continue;
          Species species = model.getSpecies(reference.getSpecies());
          if (species == null) continue;

          if (!orders.ContainsKey(id))
            orders[id] = new List<string>();

          if (!orders[id].Contains(species.getCompartment()))
            orders[id].Add(species.getCompartment());

          matrix[reactions.IndexOf(id), compartments.IndexOf(species.getCompartment())] = 1;
        }

        for (int j = 0; j < reaction.getNumProducts(); j++)
        {
          SpeciesReference reference = reaction.getProduct(j);
          if (!reference.isSetSpecies()) continue;
          Species species = model.getSpecies(reference.getSpecies());
          if (species == null) continue;

          if (!orders.ContainsKey(id))
            orders[id] = new List<string>();

          if (!orders[id].Contains(species.getCompartment()))
            orders[id].Add(species.getCompartment());

          matrix[reactions.IndexOf(id), compartments.IndexOf(species.getCompartment())] = 1;
        }

        for (int j = 0; j < reaction.getNumModifiers(); j++)
        {
          ModifierSpeciesReference reference = reaction.getModifier(j);
          if (!reference.isSetSpecies()) continue;
          Species species = model.getSpecies(reference.getSpecies());
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

      Dictionary<string, int> counts = CountOccurances(uniqueOrders);

      int max = counts.Any() ? counts.Values.Max() : 0;

      while (max > 1)
      {
        // get first one that occurs only once
        KeyValuePair<string, int> current = counts.FirstOrDefault(e => e.Value == 1);
        string id = current.Key;
        if (string.IsNullOrEmpty(id)) break;

        List<string> order = RemoveIdFrom(uniqueOrders, id);
        if (list.Contains(id))
        {
          if (counts.ContainsKey(id))
            counts.Remove(id);
          continue;
        }
        list.Add(id);

        list.AddRange(order.Where(item => !item.Equals(id)));

        counts = CountOccurances(uniqueOrders);
        if (counts.ContainsKey(id))
          counts.Remove(id);
        max = counts.Any() ? counts.Values.Max() : 0;
      }

      // add remaining
      for (int i = 0; i < model.getNumCompartments(); ++i)
      {
        Compartment current = model.getCompartment(i);
        if (!list.Contains(current.getId())) list.Add(current.getId());
      }

      return list;
    }

    private List<string> RemoveIdFrom(List<List<string>> uniqueOrders, string id)
    {
      List<string> item = uniqueOrders.FirstOrDefault(e => e.Contains(id));
      if (item == null) return null;
      uniqueOrders.Remove(item);
      return item;
    }

    private static Dictionary<string, int> CountOccurances(List<List<string>> orders)
    {
      var counts = new Dictionary<string, int>();
      foreach (var entries in orders)
      {
        foreach (string key in entries)
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

    /// <summary>
    ///   Verifies whether the reaction uses any spatial elements
    /// </summary>
    /// <param name="reaction"></param>
    /// <returns></returns>
    private List<string> GetSpeciesReferenceIdsContainedIn(Reaction reaction)
    {
      var result = new List<string>();
      if (reaction == null || reaction.getSBMLDocument() == null) return result;
      libsbmlcs.Model model = reaction.getSBMLDocument().getModel();
      if (model == null) return result;
      for (int i = 0; i < reaction.getNumReactants(); ++i)
      {
        SpeciesReference currentRef = reaction.getReactant(i);
        string id = currentRef.getSpecies();
        Species current = model.getSpecies(id);
        if (current == null) continue;
        var plug = (SpatialSpeciesRxnPlugin) current.getPlugin("spatial");
        bool isSpatial = plug != null && plug.getIsSpatial();
        if (isSpatial && !result.Contains(id))
          result.Add(id);
      }
      for (int i = 0; i < reaction.getNumProducts(); ++i)
      {
        SpeciesReference currentRef = reaction.getProduct(i);
        string id = currentRef.getSpecies();
        Species current = model.getSpecies(id);
        if (current == null) continue;
        var plug = (SpatialSpeciesRxnPlugin) current.getPlugin("spatial");
        bool isSpatial = plug != null && plug.getIsSpatial();
        if (isSpatial && !result.Contains(id))
          result.Add(id);
      }
      for (int i = 0; i < reaction.getNumModifiers(); ++i)
      {
        ModifierSpeciesReference currentRef = reaction.getModifier(i);
        string id = currentRef.getSpecies();
        Species current = model.getSpecies(id);
        if (current == null) continue;
        var plug = (SpatialSpeciesRxnPlugin) model.getSpecies(id).getPlugin("spatial");
        bool isSpatial = plug != null && plug.getIsSpatial();
        if (isSpatial && !result.Contains(id))
          result.Add(id);
      }
      return result;
    }

    public event EventHandler ModelChanged;

    protected virtual void OnModelChanged()
    {
      EventHandler handler = ModelChanged;
      if (handler != null) handler(this, EventArgs.Empty);
    }

    private static void SetRequiredElements(SBase sbase, bool hasAlternativeMath = true)
    {
      var req = (RequiredElementsSBasePlugin) sbase.getPlugin("req");
      if (req == null) return;
      req.setCoreHasAlternateMath(hasAlternativeMath);
      req.setMathOverridden("spatial");
    }

    public string ToMorpheus(string filename = null)
    {
      var converter = new MorpheusConverter(Document);
      return converter.ToMorpheus(filename);
    }

    public void ExportToDune(string filename)
    {
      var converter = new DuneConverter(Document);
      converter.ExportTo(filename);
    }
  }
}