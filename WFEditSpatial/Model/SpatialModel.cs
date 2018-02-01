using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using EditSpatial.Converter;
using libsbmlcs;
using libSbl2SBML;
using SBMLSupport;

namespace EditSpatial.Model
{
  public class SpatialModel
  {
    public bool Dirty { get; set; }


    internal static CustomSpatialValidator CustomSpatialValidator = new CustomSpatialValidator();

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

        var plugin = (SpatialModelPlugin)Document.getModel().getPlugin("spatial");
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

      if (model.Document.getNumErrors(libsbml.LIBSBML_SEV_ERROR) > 0)
      {
        var docString = File.ReadAllText(fileName);
        if (docString.Contains("spatial:spatialId"))
        {
          var assembly = System.Reflection.Assembly.GetAssembly(typeof(SBMLDocument));
          var converter = from type in assembly.GetTypes() where type.Name == "OldSpatialToSpatialConverter" select type;
          if (converter != null && converter.Any())
            model.Document = converter.First().GetMethod("convertOldSpatialSBMLFromString").Invoke(null, new object[] { docString }) as SBMLDocument;
          //libsbml.Oldspat
        }
      }

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
      var model = TModel.ReadModel(content);
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
      Dirty = false;
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

      var model = Document.getModel();
      for (var i = (int)model.getNumRules() - 1; i >= 0; i--)
      {
        var current = model.getRule(i) as AssignmentRule;
        if (current == null) continue;
        model.MoveRuleToAssignment(current.getVariable());
      }

      OnModelChanged();
    }

    public bool FixCommonErrors()
    {
      var result = false;

      if (Document == null) return result;


      FixDiffusionCoefficients();

      if (Document.getNumErrors(libsbml.LIBSBML_SEV_ERROR) == 0) return result;

      for (var i = 0; i < Document.getNumErrors(); ++i)
      {
        var current = Document.getError(i);
        var id = current.getErrorId();
        var message = current.getMessage();

        switch (current.getErrorId())
        {
          case 99109:
            {
              var plugin = Document.getPlugin("req");
              if (plugin == null) break;
              Document.setPackageRequired("req", false);
              result = true;
              break;
            }
          case 20517:
            {
              for (var j = 0; j < Document.getModel().getNumCompartments(); ++j)
              {
                var comp = Document.getModel().getCompartment(j);
                if (!comp.isSetConstant())
                  comp.setConstant(true);
              }
              result = true;
              break;
            }
          case 20623:
            {
              for (var j = 0; j < Document.getModel().getNumSpecies(); ++j)
              {
                var species = Document.getModel().getSpecies(j);
                if (!species.isSetConstant())
                  species.setConstant(false);
              }
              result = true;
              break;
            }

          case 21116:
            {
              for (var j = 0; j < Document.getModel().getNumReactions(); ++j)
              {
                var reaction = Document.getModel().getReaction(j);
                for (var k = 0; k < reaction.getNumProducts(); ++k)
                {
                  var reference = reaction.getProduct(k);
                  if (!reference.isSetConstant())
                    reference.setConstant(true);
                }
                for (var k = 0; k < reaction.getNumReactants(); ++k)
                {
                  var reference = reaction.getReactant(k);
                  if (!reference.isSetConstant())
                    reference.setConstant(true);
                }
              }
              result = true;
              break;
            }

          case 21110:
            {
              for (var j = 0; j < Document.getModel().getNumReactions(); ++j)
              {
                var reaction = Document.getModel().getReaction(j);
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
              for (var j = 0; j < Document.getModel().getNumParameters(); ++j)
              {
                var parameter = Document.getModel().getParameter(j);
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
              var match = Regex.Match(message.Split('\n')[2],
                ".*'(?<speciesId>\\w+?)'.*'(?<reactionId>\\w+?)'.*",
                RegexOptions.ExplicitCapture);
              if (match.Success)
              {
                var speciesId = match.Groups["speciesId"].Value;

                Document.getModel().MoveRuleToAssignment(speciesId);
                result = true;
              }
              break;
            }
          case 21121:
            {
              var match = Regex.Match(message, ".*'(?<speciesId>\\w+?)'.*'(?<reactionId>\\w+?)'.*",
                RegexOptions.ExplicitCapture);
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
                    result = true;
                  }
                }
              }

              break;
            }


          case 99303:
            {
              // remove the unitdefinition referenced by an element
              var match = Regex.Match(message, ".*<(?<type>\\w+?)>.*'(?<id>\\w+?)'.*",
                RegexOptions.ExplicitCapture);
              if (match.Success)
              {
                var type = match.Groups["type"].Value;
                var elementId = match.Groups["id"].Value;

                switch (type)
                {
                  case "parameter":
                    {
                      var param = Document.getModel().getParameter(elementId);
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

      var numCooords = Geometry.getNumCoordinateComponents();

      var dict = new Dictionary<string, List<Tuple<int, Parameter>>>();

      for (var i = 0; i < Document.getModel().getNumParameters(); ++i)
      {
        var current = Document.getModel().getParameter(i);
        if (current == null) continue;
        var plug = current.getPlugin("spatial") as SpatialParameterPlugin;
        if (plug == null) continue;
        if (plug.getType() != libsbml.SBML_SPATIAL_DIFFUSIONCOEFFICIENT) continue;
        var diff = plug.getDiffusionCoefficient();
        if (diff == null || !diff.isSetCoordinateReference1()) continue;
        if (!dict.ContainsKey(diff.getVariable()))
          dict[diff.getVariable()] = new List<Tuple<int, Parameter>>();
        dict[diff.getVariable()].Add(new Tuple<int, Parameter>((int)diff.getCoordinateReference1(), current));
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
          if (diff == null || !diff.isSetCoordinateReference1()) continue;
          diff.unsetCoordinateReference1();
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

      //Document.enablePackage(RequiredElementsExtension.getXmlnsL3V1V1(), "req", true);
      //Document.setPackageRequired("req", false);

      return true;
    }

    public bool ConvertToSpatial(CreateModel createModel)
    {
      if (Document == null) return true;

      if (!ConvertToL3()) return false;

      var model = Document.getModel();
      var plugin = (SpatialModelPlugin)model.getPlugin("spatial");
      if (plugin == null)
        return false;

      var geometry = plugin.getGeometry();
      if (geometry == null)
        geometry = plugin.createGeometry();

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
        var comp1Id = info.CompartmentIds[0];
        var comp1 = model.getCompartment(comp1Id);
        if (comp1 == null || comp1.getSpatialDimensions() == 2) return;
        var comp2Id = info.CompartmentIds[1];
        var comp2 = model.getCompartment(comp2Id);
        if (comp2 == null || comp2.getSpatialDimensions() == 2) return;

        var memId = String.Format("mem_{0}_{1}", comp2Id, comp1Id);
        var memComp = model.getCompartment(memId);
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
        var react1 = model.createReaction();
        react1.initDefaults();
        react1.setReversible(reaction.getReversible());
        react1.setId(String.Format("mem_{0}_{1}", comp1Id, reaction.getId()));

        var react2builder = new StringBuilder("1");
        var react2 = model.createReaction();
        react2.initDefaults();
        react2.setReversible(reaction.getReversible());
        react2.setId(String.Format("mem_{0}_{1}", comp2Id, reaction.getId()));


        for (var i = 0; i < reaction.getNumReactants(); ++i)
        {
          var reference = reaction.getReactant(i);
          var species = model.getSpecies(reference.getSpecies());
          if (species.getCompartment() == comp1Id)
          {
            var newRef = react1.createReactant();
            newRef.setSpecies(reference.getSpecies());
            newRef.setStoichiometry(reference.getStoichiometry());
          }
          else
          {
            var newId = "mem_" + species.getId();
            var newSpecies = model.getSpecies(newId);
            if (newSpecies == null)
            {
              newSpecies = model.createSpecies();
              newSpecies.initDefaults();
              newSpecies.setId(newId);
              newSpecies.setName(species.getId());
              newSpecies.setCompartment(memId);
              newSpecies.setInitialConcentration(0);
            }

            var newRef = react2.createProduct();
            newRef.setSpecies(newSpecies.getId());
            newRef.setStoichiometry(reference.getStoichiometry());

            newRef = react1.createReactant();
            newRef.setSpecies(newSpecies.getId());
            newRef.setStoichiometry(reference.getStoichiometry());

            react2builder.Append(" * " + newSpecies.getId());
            math.renameSIdRefs(species.getId(), newSpecies.getId());
          }
        }


        for (var i = 0; i < reaction.getNumProducts(); ++i)
        {
          var reference = reaction.getProduct(i);
          var species = model.getSpecies(reference.getSpecies());
          if (species.getCompartment() == comp1Id)
          {
            var newRef = react1.createProduct();
            newRef.setSpecies(species.getId());
            newRef.setStoichiometry(reference.getStoichiometry());
          }
          else
          {
            var newId = "mem_" + species.getId();
            var newSpecies = model.getSpecies(newId);
            if (newSpecies == null)
            {
              newSpecies = model.createSpecies();
              newSpecies.initDefaults();
              newSpecies.setId(newId);
              newSpecies.setName(species.getId());
              newSpecies.setCompartment(memId);
              newSpecies.setInitialConcentration(0);
            }

            var newRef = react1.createProduct();
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

        var law = react2.createKineticLaw();
        law.setFormula(react2builder.ToString());

        law = react1.createKineticLaw();
        for (var l = 0; l < reaction.getKineticLaw().getNumLocalParameters(); ++l)
        {
          var param = reaction.getKineticLaw().getParameter(l);
          var newParam = law.createParameter();
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
      for (var i = (int)model.getNumReactions() - 1; i >= 0; --i)
      {
        var reaction = model.getReaction(i);
        var comps = Util.GetCompartmentsFromReaction(reaction);
        if (comps.Count < 2)
          continue;

        SplitReaction(model, reaction);
      }
    }

    private bool SetupGeometry(libsbmlcs.Model model, Geometry geometry,
      CreateModel createModel)
    {
      var numCompartments = model.getNumCompartments();
      if (numCompartments == 1 || MainForm.Settings.IgnoreMultiCompartments)
        return SetupUnicompartmentalGeometry(model, geometry, createModel);

      var num3DComps = 0;
      for (var index = 0; index < numCompartments; ++index)
      {
        var current = model.getCompartment(index);
        if (current == null || !current.isSetSpatialDimensions()) continue;
        if (current.getSpatialDimensions() == 3) num3DComps++;
      }

      var length = createModel.Geometry.Xmax / num3DComps;
      var def = geometry.createAnalyticGeometry();
      def.setId("geometry");

      var order = OrderCompartments(model);
      var i = 0;
      AdjacentDomains lastAdjacent = null;
      Domain memDomain = null;
      for (var j = 0; j < order.Count; j++)
      {
        var comp = model.getCompartment(order[j]);
        var cplug = (SpatialCompartmentPlugin)comp.getPlugin("spatial");
        if (cplug == null)
          return false;
        var domainType = geometry.createDomainType();
        domainType.setId("domainType_" + order[j]);
        domainType.setSpatialDimensions((int)comp.getSpatialDimensionsAsDouble());
        var domain = geometry.createDomain();
        domain.setId("domain_" + order[j]);
        domain.setDomainType("domainType_" + order[j]);
        var point = domain.createInteriorPoint();
        point.setCoord1(i * length + length / 2.0);
        point.setCoord2(createModel.Geometry.Ymax / 2.0);
        point.setCoord3(0);
        if (lastAdjacent != null)
        {
          var adj = geometry.createAdjacentDomains();
          adj.setId(String.Format("adj_{0}_{1}", memDomain.getId(), domain.getId()));
          adj.setDomain1(memDomain.getId());
          adj.setDomain2(domain.getId());
        }
        var vol = def.createAnalyticVolume();
        vol.setId("vol_" + order[j]);
        vol.setDomainType("domainType_" + order[j]);
        vol.setFunctionType(libsbml.SPATIAL_FUNCTIONKIND_LAYERED);
        vol.setOrdinal(i);
        vol.setMath(libsbml.parseFormula(i == 0
          ? "1" // first compartment gets all
          : j == order.Count - 1
            ? string.Format("geq(x, {0})", (i * length)) // last compartment gets rest
            : string.Format("and(geq(x, {0}), lt(x, {1}))", (i * length), ((i + 1) * length))));
        var map = cplug.getCompartmentMapping();
        if (map == null)
          map = cplug.createCompartmentMapping();

        map.setId("mapping_" + order[j]);
        map.setDomainType(domainType.getId());
        map.setUnitSize(1);

        if (i + 1 < numCompartments)
        {
          if (j + 1 < order.Count && model.getCompartment(order[j + 1]).getSpatialDimensions() == 2)
          {
            // next compartment is a 2D compartment!
            domainType = geometry.createDomainType();
            domainType.setId("domainType_" + order[j + 1]);
            domainType.setSpatialDimensions(2);

            comp = model.getCompartment(order[j + 1]);
            cplug = (SpatialCompartmentPlugin)comp.getPlugin("spatial");
            if (cplug == null)
              return false;
            map = cplug.getCompartmentMapping();
            if (map == null)
              map = cplug.createCompartmentMapping();

            map.setId("mapping_" + comp.getId());
            map.setDomainType(domainType.getId());
            map.setUnitSize(1);

            memDomain = geometry.createDomain();
            memDomain.setId("domain_mem_" + order[j]);
            memDomain.setDomainType(domainType.getId());
            lastAdjacent = geometry.createAdjacentDomains();
            lastAdjacent.setId(String.Format("adj_{0}_{1}", memDomain.getId(), domain.getId()));
            lastAdjacent.setDomain1(memDomain.getId());
            lastAdjacent.setDomain2(domain.getId());

            ++j; //++i;

            //vol = def.createAnalyticVolume();
            //vol.setId("vol_" + order[j]);
            //vol.setDomainType("domainType_" + order[j]);
            //vol.setFunctionType(libsbml.SPATIAL_FUNCTIONKIND_LAYERED);
            //vol.setOrdinal(i);
            //vol.setMath(libsbml.parseFormula(i == 0 ? "1" : //            string.Format(
            //  //"and(geq(x, {0}), lt(x, {1}))", (i * length), ((i + 1) * length))
            //string.Format(//"and(and(geq(x, {0}), lt(x, {1})), and(geq(y, {0}), lt(y, {1})))", (i * length), ((i + 1) * length))
            //"and(geq(x, {0}), lt(x, {1}))", (i * length), ((i + 1) * length))));
          }
          else
          {
            domainType = geometry.createDomainType();
            domainType.setId("mem_" + order[j]);
            domainType.setSpatialDimensions(2);
            comp = model.createCompartment();
            comp.initDefaults();
            comp.setId("c_mem_" + order[j]);
            comp.setSize(1);
            cplug = (SpatialCompartmentPlugin)comp.getPlugin("spatial");
            if (cplug == null)
              return false;
            map = cplug.getCompartmentMapping();
            if (map == null)
              map = cplug.createCompartmentMapping();

            map.setId("mapping_" + comp.getId());
            map.setDomainType(domainType.getId());
            map.setUnitSize(1);
            memDomain = geometry.createDomain();
            memDomain.setId("domain_mem_" + order[j]);
            memDomain.setDomainType("mem_" + order[j]);
            lastAdjacent = geometry.createAdjacentDomains();
            lastAdjacent.setId(String.Format("adj_{0}_{1}", memDomain.getId(), domain.getId()));
            lastAdjacent.setDomain1(memDomain.getId());
            lastAdjacent.setDomain2(domain.getId());
          }
        }
        ++i;
      }
      return true;
    }

    private static bool SetupUnicompartmentalGeometry(libsbmlcs.Model model, Geometry geometry, CreateModel createModel)
    {
      var domainType = geometry.createDomainType();
      domainType.setId("domainType0");
      domainType.setSpatialDimensions(3);

      var domain = geometry.createDomain();
      domain.setId("domain0");
      domain.setDomainType("domainType0");
      var point = domain.createInteriorPoint();
      point.setCoord1(createModel.Geometry.Xmax / 2.0);
      point.setCoord2(createModel.Geometry.Ymax / 2.0);
      point.setCoord3(0);


      AnalyticVolume vol = null;
      AnalyticGeometry def = null;

      def = geometry.GetFirstAnalyticGeometry();
      if (def == null)
      {
        def = geometry.createAnalyticGeometry();
        def.setId("geometry0");
      }

      vol = def.getAnalyticVolume(0);
      if (vol == null)
      {
        vol = def.createAnalyticVolume();
        vol.setId("vol0");
        vol.setOrdinal(0);
      }

      if (!vol.isSetMath() || string.IsNullOrWhiteSpace(libsbml.formulaToL3String(vol.getMath())))
        vol.setMath(libsbml.parseL3Formula("1"));

      vol.setDomainType("domainType0");
      vol.setFunctionType(libsbml.SPATIAL_FUNCTIONKIND_LAYERED);


      var comp = model.getCompartment(0);
      var cplug = (SpatialCompartmentPlugin)comp.getPlugin("spatial");
      if (cplug == null)
        return false;

      var map = cplug.getCompartmentMapping();
      if (map == null)
        map = cplug.createCompartmentMapping();

      map.setId("mapping0");
      map.setDomainType(domainType.getId());
      map.setUnitSize(1);

      if (createModel.Geometry.WrapOutside)
      {
        comp = model.createCompartment();
        comp.initDefaults();
        comp.setId("__outside");
        cplug = (SpatialCompartmentPlugin)comp.getPlugin("spatial");

        domainType = geometry.createDomainType();
        domainType.setId("domainType1");
        domainType.setSpatialDimensions(3);

        domain = geometry.createDomain();
        domain.setId("domain1");
        domain.setDomainType("domainType1");
        point = domain.createInteriorPoint();
        point.setCoord1(0);
        point.setCoord2(0);
        point.setCoord3(0);

        map = cplug.getCompartmentMapping();
        if (map == null)
          map = cplug.createCompartmentMapping();

        map.setId("mapping1");
        map.setDomainType(domainType.getId());
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
        vol.setId("vol1");
        vol.setDomainType("domainType1");
        vol.setFunctionType(libsbml.SPATIAL_FUNCTIONKIND_LAYERED);
        vol.setOrdinal(0);
        vol.setMath(libsbml.parseFormula("1"));

        // membrane
        comp = model.createCompartment();
        comp.initDefaults();
        comp.setId("__membrane");
        cplug = (SpatialCompartmentPlugin)comp.getPlugin("spatial");

        domainType = geometry.createDomainType();
        domainType.setId("domainType2");
        domainType.setSpatialDimensions(2);

        domain = geometry.createDomain();
        domain.setId("domain2");
        domain.setDomainType("domainType2");

        map = cplug.getCompartmentMapping();
        if (map == null)
          map = cplug.createCompartmentMapping();

        map.setId("mapping2");
        map.setDomainType(domainType.getId());
        map.setUnitSize(1);

        var adjacent = geometry.createAdjacentDomains();
        adjacent.setId("adj_1");
        adjacent.setDomain1("domain2");
        adjacent.setDomain2("domain1");

        adjacent = geometry.createAdjacentDomains();
        adjacent.setId("adj_2");
        adjacent.setDomain1("domain2");
        adjacent.setDomain2("domain0");
      }

      return true;
    }

    private static void SetupSpecies(CreateModel createModel, libsbmlcs.Model model)
    {
      var spatialElements = (from s in createModel.Species select s.Id).ToList();

      for (var i = 0; i < model.getNumSpecies(); i++)
      {
        var species = model.getSpecies(i);
        var splug = (SpatialSpeciesPlugin)species.getPlugin("spatial");
        if (splug == null) continue;
        splug.setIsSpatial(false);
        var id = species.getId();
        if (!spatialElements.Contains(id)) continue;

        var currentSpecies = createModel[id];
        species.setInitialExpession(currentSpecies.InitialCondition);
        splug.setIsSpatial(true);
        SetRequiredElements(species);

        var temp = species.getParameterDiffusionX();
        if (temp != null && temp.isSetId())
          model.removeParameter(temp.getId());
        var param = model.createParameter();
        param.initDefaults();
        param.setId(id + "_diff_X");
        param.setValue(currentSpecies.DiffusionX);
        var pplug = (SpatialParameterPlugin)param.getPlugin("spatial");
        var diff = pplug.getDiffusionCoefficient();
        if (diff == null)
          diff = pplug.createDiffusionCoefficient();
        diff.setVariable(id);
        diff.setType(libsbml.SPATIAL_DIFFUSIONKIND_ANISOTROPIC);
        diff.setCoordinateReference1(0);
        SetRequiredElements(param);

        temp = species.getParameterDiffusionY();
        if (temp != null && temp.isSetId())
          model.removeParameter(temp.getId());
        model.removeParameter(id + "_diff_Y");
        param = model.createParameter();
        param.initDefaults();
        param.setId(id + "_diff_Y");
        param.setValue(currentSpecies.DiffusionY);
        pplug = (SpatialParameterPlugin)param.getPlugin("spatial");
        diff = pplug.getDiffusionCoefficient();
        if (diff == null)
          diff = pplug.createDiffusionCoefficient();
        diff.setVariable(id);
        diff.setType(libsbml.SPATIAL_DIFFUSIONKIND_ANISOTROPIC);
        diff.setCoordinateReference1(1);
        SetRequiredElements(param);

        temp = species.getSpatialParameter(libsbml.SBML_SPATIAL_BOUNDARYCONDITION, "Xmin");
        if (temp != null && temp.isSetId())
          model.removeParameter(temp.getId());
        param = model.createParameter();
        param.initDefaults();
        param.setId(id + "_BC_Xmin");
        param.setValue(currentSpecies.MinBoundaryX);
        pplug = (SpatialParameterPlugin)param.getPlugin("spatial");
        var bc = pplug.getBoundaryCondition();
        if (bc == null)
          bc = pplug.createBoundaryCondition();
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
        pplug = (SpatialParameterPlugin)param.getPlugin("spatial");
        bc = pplug.getBoundaryCondition();
        if (bc == null)
          bc = pplug.createBoundaryCondition();
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
        pplug = (SpatialParameterPlugin)param.getPlugin("spatial");
        bc = pplug.getBoundaryCondition();
        if (bc == null)
          bc = pplug.createBoundaryCondition();
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
        pplug = (SpatialParameterPlugin)param.getPlugin("spatial");
        bc = pplug.getBoundaryCondition();
        if (bc == null)
          bc = pplug.createBoundaryCondition();
        bc.setVariable(id);
        bc.setCoordinateBoundary("Ymax");
        bc.setType(currentSpecies.BCType == "Dirichlet" ? "Value" : "Flux");
        SetRequiredElements(param);
      }
    }

    private void SetIsLocalOnReactions(libsbmlcs.Model model)
    {
      for (var i = 0; i < model.getNumReactions(); i++)
      {
        var reaction = model.getReaction(i);
        var rplug = (SpatialReactionPlugin)reaction.getPlugin("spatial");
        if (rplug == null) continue;
        SetRequiredElements(reaction);
        var idsContainedIn = GetSpeciesReferenceIdsContainedIn(reaction);
        var isLocal = idsContainedIn.Count > 0;
        rplug.setIsLocal(isLocal);
      }
    }

    public void CreateCoordinateSystem(Geometry geometry, libsbmlcs.Model model,
      GeometrySettings settings)
    {
      geometry.setCoordinateSystem(libsbml.SPATIAL_GEOMETRYKIND_CARTESIAN);

      var coord = geometry.getCoordinateComponent("x");
      if (coord == null)
      {
        coord = geometry.createCoordinateComponent();
      }

      coord.setId("x");
      coord.setUnit("um");
      coord.setType(libsbml.SPATIAL_COORDINATEKIND_CARTESIAN_X);

      var min = coord.getBoundaryMin();
      if (min == null)
        min = coord.createBoundaryMin();
      min.setId("Xmin");
      min.setValue(settings.Xmin);
      var max = coord.getBoundaryMax();
      if (max == null) max = coord.createBoundaryMax();
      max.setId("Xmax");
      max.setValue(settings.Xmax);

      coord = geometry.getCoordinateComponent("y");
      if (coord == null)
        coord = geometry.createCoordinateComponent();
      coord.setId("y");
      coord.setUnit("um");
      coord.setType(libsbml.SPATIAL_COORDINATEKIND_CARTESIAN_Y);
      min = coord.getBoundaryMin();
      if (min == null)
        min = coord.createBoundaryMin();
      min.setId("Ymin");
      min.setValue(settings.Ymin);
      max = coord.getBoundaryMax();
      if (max == null)
        max = coord.createBoundaryMax();
      max.setId("Ymax");
      max.setValue(settings.Ymax);

      model.removeParameter("x");
      var param = model.createParameter();
      param.initDefaults();
      param.setId("x");
      param.setValue(0);
      var pplug = (SpatialParameterPlugin)param.getPlugin("spatial");
      var symbol = pplug.createSpatialSymbolReference();
      symbol.setId("x");
      SetRequiredElements(param, false);

      model.removeParameter("y");
      param = model.createParameter();
      param.initDefaults();
      param.setId("y");
      param.setValue(0);
      pplug = (SpatialParameterPlugin)param.getPlugin("spatial");
      symbol = pplug.createSpatialSymbolReference();
      symbol.setId("y");
      SetRequiredElements(param, false);

      if (settings.UsedSymbols.Contains("width"))
      {
        model.removeParameter("width");
        param = model.createParameter();
        param.initDefaults();
        param.setId("width");
        param.setValue(settings.Xmax);
        pplug = (SpatialParameterPlugin)param.getPlugin("spatial");
        symbol = pplug.createSpatialSymbolReference();
        symbol.setId("Xmax");
        SetRequiredElements(param, false);
      }

      if (settings.UsedSymbols.Contains("height"))
      {
        model.removeParameter("height");
        param = model.createParameter();
        param.initDefaults();
        param.setId("height");
        param.setValue(settings.Ymax);
        pplug = (SpatialParameterPlugin)param.getPlugin("spatial");
        symbol = pplug.createSpatialSymbolReference();
        symbol.setId("Ymax");
        SetRequiredElements(param, false);
      }

      if (settings.UsedSymbols.Contains("Xmax"))
      {
        model.removeParameter("Xmax");
        param = model.createParameter();
        param.initDefaults();
        param.setId("Xmax");
        param.setValue(settings.Xmax);
        pplug = (SpatialParameterPlugin)param.getPlugin("spatial");
        symbol = pplug.createSpatialSymbolReference();
        symbol.setId("Xmax");
        SetRequiredElements(param, false);
      }

      if (settings.UsedSymbols.Contains("Ymax"))
      {
        model.removeParameter("Ymax");
        param = model.createParameter();
        param.initDefaults();
        param.setId("Ymax");
        param.setValue(settings.Ymax);
        pplug = (SpatialParameterPlugin)param.getPlugin("spatial");
        symbol = pplug.createSpatialSymbolReference();
        symbol.setId("Ymax");
        SetRequiredElements(param, false);
      }


      if (settings.UsedSymbols.Contains("Xmin"))
      {
        model.removeParameter("Xmin");
        param = model.createParameter();
        param.initDefaults();
        param.setId("Xmin");
        param.setValue(settings.Xmin);
        pplug = (SpatialParameterPlugin)param.getPlugin("spatial");
        symbol = pplug.createSpatialSymbolReference();
        symbol.setId("Xmin");
        SetRequiredElements(param, false);
      }

      if (settings.UsedSymbols.Contains("Ymin"))
      {
        model.removeParameter("Ymin");
        param = model.createParameter();
        param.initDefaults();
        param.setId("Ymin");
        param.setValue(settings.Ymin);
        pplug = (SpatialParameterPlugin)param.getPlugin("spatial");
        symbol = pplug.createSpatialSymbolReference();
        symbol.setId("Ymin");
        SetRequiredElements(param, false);
      }
    }

    private List<string> OrderCompartments(libsbmlcs.Model model)
    {
      var list = new List<string>();

      var orders = new Dictionary<string, List<string>>();
      var matrix = new int[model.getNumReactions(), model.getNumCompartments()];
      var reactions = new List<string>();
      for (var i = 0; i < model.getNumReactions(); i++)
        reactions.Add(model.getReaction(i).getId());
      var compartments = new List<string>();
      for (var i = 0; i < model.getNumCompartments(); i++)
        compartments.Add(model.getCompartment(i).getId());

      for (var i = 0; i < model.getNumReactions(); i++)
      {
        var reaction = model.getReaction(i);
        var id = reaction.getId();
        for (var j = 0; j < reaction.getNumReactants(); j++)
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

        for (var j = 0; j < reaction.getNumProducts(); j++)
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

        for (var j = 0; j < reaction.getNumModifiers(); j++)
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

      for (var i = uniqueOrders.Count - 1; i >= 0; i--)
        if (uniqueOrders[i].Count < 2)
          uniqueOrders.RemoveAt(i);

      var counts = CountOccurances(uniqueOrders);

      var max = counts.Any() ? counts.Values.Max() : 0;

      while (max > 1)
      {
        // get first one that occurs only once
        var current = counts.FirstOrDefault(e => e.Value == 1);
        var id = current.Key;
        if (string.IsNullOrEmpty(id)) break;

        var order = RemoveIdFrom(uniqueOrders, id);
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
      for (var i = 0; i < model.getNumCompartments(); ++i)
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
      var stringOrder = Combine(order);
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
      for (var i = 0; i < order.Count; i++)
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
      var model = reaction.getSBMLDocument().getModel();
      if (model == null) return result;
      for (var i = 0; i < reaction.getNumReactants(); ++i)
      {
        var currentRef = reaction.getReactant(i);
        var id = currentRef.getSpecies();
        var current = model.getSpecies(id);
        if (current == null) continue;
        var plug = (SpatialSpeciesPlugin)current.getPlugin("spatial");
        var isSpatial = plug != null && plug.getIsSpatial();
        if (isSpatial && !result.Contains(id))
          result.Add(id);
      }
      for (var i = 0; i < reaction.getNumProducts(); ++i)
      {
        var currentRef = reaction.getProduct(i);
        var id = currentRef.getSpecies();
        var current = model.getSpecies(id);
        if (current == null) continue;
        var plug = (SpatialSpeciesPlugin)current.getPlugin("spatial");
        var isSpatial = plug != null && plug.getIsSpatial();
        if (isSpatial && !result.Contains(id))
          result.Add(id);
      }
      for (var i = 0; i < reaction.getNumModifiers(); ++i)
      {
        var currentRef = reaction.getModifier(i);
        var id = currentRef.getSpecies();
        var current = model.getSpecies(id);
        if (current == null) continue;
        var plug = (SpatialSpeciesPlugin)current.getPlugin("spatial");
        var isSpatial = plug != null && plug.getIsSpatial();
        if (isSpatial && !result.Contains(id))
          result.Add(id);
      }
      return result;
    }

    public event EventHandler ModelChanged;

    protected virtual void OnModelChanged()
    {
      var handler = ModelChanged;
      if (handler != null) handler(this, EventArgs.Empty);
    }

    private static void SetRequiredElements(SBase sbase, bool hasAlternativeMath = true)
    {
      //var req = (RequiredElementsSBasePlugin) sbase.getPlugin("req");
      //if (req == null) return;
      //req.setCoreHasAlternateMath(hasAlternativeMath);
      //req.setMathOverridden("spatial");
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