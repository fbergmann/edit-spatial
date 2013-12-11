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

    public bool ConvertToSpatial(List<string> spatialElements, List<Tuple<string, string>> initialConditions, object geometryDescription)
    {
      if (Document == null) return true;

      if (!Document.setLevelAndVersion(3, 1))
        return false;

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
      max.setValue(100);

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
      max.setValue(100);

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
        point.setCoord1(50);
        point.setCoord2(50);
        point.setCoord3(0);

        var def = geometry.createAnalyticGeometry();
        def.setSpatialId("geometry");
        var vol = def.createAnalyticVolume();
        vol.setSpatialId("vol0");
        vol.setDomainType("domainType0");
        vol.setFunctionType("layered");
        vol.setOrdinal(0);
        vol.setMath(libsbml.parseFormula("1"));

        var comp = model.getCompartment(0);
        var cplug = (SpatialCompartmentPlugin)comp.getPlugin("spatial");
        if (cplug == null)
          return false;

        var map = cplug.getCompartmentMapping();
        map.setSpatialId("mapping0");
        map.setCompartment(comp.getId());
        map.setDomainType(domainType.getSpatialId());
        map.setUnitSize(1);

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
