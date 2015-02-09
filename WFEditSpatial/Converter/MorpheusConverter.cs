using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using EditSpatial.Model;
using libsbmlcs;

namespace EditSpatial.Converter
{
  public class MorpheusConverter : IDisposable
  {
    private readonly Dictionary<string, string> boundaryConditions;
    private readonly Dictionary<string, string> boundaryLables;
    private readonly Dictionary<string, Dictionary<string, string>> boundaryValue;
    private readonly Dictionary<string, string> coordinates;
    private readonly Dictionary<string, string> diffusion;
    private readonly Dimensions dims;
    private readonly SBMLDocument document;
    private readonly StringBuilder errorBuilder;
    private readonly Dictionary<string, string> initial;
    private readonly libsbmlcs.Model Model;
    private readonly int numVariables;

    public MorpheusConverter(SBMLDocument original)
    {
      errorBuilder = new StringBuilder();
      document = original.clone();
      numVariables = 0;

      document.getErrorLog().setSeverityOverride(libsbml.LIBSBML_OVERRIDE_DONT_LOG);
      var prop = new ConversionProperties();
      prop.addOption("replaceReactions", true,
        "Replace reactions with rateRules");
      var status = document.convert(prop);
      if (status != libsbml.LIBSBML_OPERATION_SUCCESS)
      {
        errorBuilder.AppendFormat("conversion of rates failed: {0}{1}", status, Environment.NewLine);
      }

      prop = new ConversionProperties();
      prop.addOption("expandFunctionDefinitions", true);
      status = document.convert(prop);
      if (status != libsbml.LIBSBML_OPERATION_SUCCESS)
      {
        errorBuilder.AppendFormat("expanding function definitions failed: {0}{1}", status, Environment.NewLine);
      }

      Model = document.getModel();


      boundaryLables = new Dictionary<string, string>();
      coordinates = new Dictionary<string, string>();
      for (var i = 0; i < Geometry.getNumCoordinateComponents(); ++i)
      {
        var current = Geometry.getCoordinateComponent(i);
        var prefix = "x";
        if (current.getComponentType() == "cartesianX")
          prefix = "x";
        else if (current.getComponentType() == "cartesianY")
          prefix = "y";
        else if (current.getComponentType() == "cartesianZ")
          prefix = "z";

        coordinates[current.getSpatialId()] = prefix;

        boundaryLables[current.getBoundaryMin().getSpatialId()] = "-" + prefix;
        boundaryLables[current.getBoundaryMax().getSpatialId()] = prefix;
      }

      dims = new Dimensions();
      dims.setWidth(GetBoundaryMaxValue(0));
      dims.setHeight(GetBoundaryMaxValue(1));
      dims.setDepth(GetBoundaryMaxValue(2));

      boundaryConditions = new Dictionary<string, string>();
      diffusion = new Dictionary<string, string>();
      for (var i = 0; i < Model.getNumParameters(); ++i)
      {
        var current = Model.getParameter(i);
        var plugin = (SpatialParameterPlugin) current.getPlugin("spatial");
        if (plugin == null) continue;
        if (plugin.getType() == libsbml.SBML_SPATIAL_BOUNDARYCONDITION)
        {
          var condition = plugin.getBoundaryCondition();
          boundaryConditions[condition.getCoordinateBoundary()] = TranslateCondition(condition.getType());
        }
        if (plugin.getType() == libsbml.SBML_SPATIAL_DIFFUSIONCOEFFICIENT)
        {
          var diff = plugin.getDiffusionCoefficient();
          diffusion[diff.getVariable()] = current.getValue().ToString(CultureInfo.InvariantCulture);
        }
      }


      boundaryValue = new Dictionary<string, Dictionary<string, string>>();
      initial = new Dictionary<string, string>();
      for (var i = 0; i < Model.getNumSpecies(); ++i)
      {
        var current = Model.getSpecies(i);
        var plugin = (SpatialSpeciesRxnPlugin) current.getPlugin("spatial");
        if (plugin == null || !plugin.getIsSpatial()) continue;

        ++numVariables;

        boundaryValue[current.getId()] = new Dictionary<string, string>();
        var bcValue = current.getXMinBC();
        if (bcValue.HasValue && bcValue.Value != 0)
          boundaryValue[current.getId()]["-x"] = bcValue.Value.ToString();
        bcValue = current.getXMaxBC();
        if (bcValue.HasValue && bcValue.Value != 0)
          boundaryValue[current.getId()]["x"] = bcValue.Value.ToString();
        bcValue = current.getYMinBC();
        if (bcValue.HasValue && bcValue.Value != 0)
          boundaryValue[current.getId()]["-y"] = bcValue.Value.ToString();
        bcValue = current.getYMaxBC();
        if (bcValue.HasValue && bcValue.Value != 0)
          boundaryValue[current.getId()]["y"] = bcValue.Value.ToString();

        if (current.isSetInitialConcentration())
          initial[current.getId()] = current.getInitialConcentration().ToString();
        else if (current.isSetInitialAmount())
          initial[current.getId()] = current.getInitialAmount().ToString();

        var assignment = Model.getInitialAssignment(current.getId());
        if (assignment == null) continue;
        initial[current.getId()] = TranslateExpression(assignment.getMath(), coordinates);
      }
    }

    public Geometry Geometry
    {
      get
      {
        var plugin = (SpatialModelPlugin) Model.getPlugin("spatial");
        if (plugin == null) return null;
        return plugin.getGeometry();
      }
    }

    public void Dispose()
    {
      if (document != null)
        document.Dispose();
      if (Model != null)
        Model.Dispose();
      if (dims != null)
        dims.Dispose();
    }

    public static string TranslateExpression(string expression)
    {
      return TranslateExpression(libsbml.parseFormula(expression), null);
    }

    public static string TranslateExpression(ASTNode math, Dictionary<string, string> map)
    {
      if (math == null) return "";
      switch (math.getType())
      {
        case libsbml.AST_INTEGER:
          return math.getInteger().ToString();
        case libsbml.AST_REAL:
        case libsbml.AST_REAL_E:
          return math.getReal().ToString();
        case libsbml.AST_DIVIDE:
        {
          var builder = new StringBuilder();
          builder.AppendMorpheusNode("{0}", math.getChild(0), map);
          builder.AppendMorpheusNode(" / {0}", math.getChild(1), map);
          return builder.ToString();
        }
        case libsbml.AST_MINUS:
        {
          var builder = new StringBuilder();
          if (math.getNumChildren() == 1)
          {
            builder.AppendMorpheusNode(" - {0}", math.getChild(0), map);
          }
          else
          {
            builder.AppendMorpheusNode("{0}", math.getChild(0), map);
            builder.AppendMorpheusNode(" - {0}", math.getChild(1), map);
          }
          return builder.ToString();
        }
        case libsbml.AST_PLUS:
        {
          var builder = new StringBuilder();
          builder.AppendMorpheusNode("{0}", math.getChild(0), map);
          for (var i = 1; i < math.getNumChildren(); ++i)
          {
            builder.AppendMorpheusNode(" + {0}", math.getChild(i), map);
          }
          return builder.ToString();
        }
        case libsbml.AST_TIMES:
        {
          var builder = new StringBuilder();
          builder.AppendMorpheusNode("{0}", math.getChild(0), map);
          for (var i = 1; i < math.getNumChildren(); ++i)
          {
            builder.AppendMorpheusNode(" * {0}", math.getChild(i), map);
          }
          return builder.ToString();
        }

        case libsbml.AST_RELATIONAL_LEQ:
        {
          var builder = new StringBuilder();
          builder.AppendMorpheusNode("{0}", math.getChild(0), map);
          builder.AppendMorpheusNode(" <= {0}", math.getChild(1), map);
          return builder.ToString();
        }
        case libsbml.AST_RELATIONAL_LT:
        {
          var builder = new StringBuilder();
          builder.AppendMorpheusNode("{0}", math.getChild(0), map);
          builder.AppendMorpheusNode(" < {0}", math.getChild(1), map);
          return builder.ToString();
        }
        case libsbml.AST_RELATIONAL_GT:
        {
          var builder = new StringBuilder();
          builder.AppendMorpheusNode("{0}", math.getChild(0), map);
          builder.AppendMorpheusNode(" > {0}", math.getChild(1), map);
          return builder.ToString();
        }
        case libsbml.AST_RELATIONAL_GEQ:
        {
          var builder = new StringBuilder();
          builder.AppendMorpheusNode("{0}", math.getChild(0), map);
          builder.AppendMorpheusNode(" >= {0}", math.getChild(1), map);
          return builder.ToString();
        }
        case libsbml.AST_LOGICAL_AND:
        {
          var builder = new StringBuilder();
          builder.AppendMorpheusNode("{0}", math.getChild(0), map);
          for (var i = 1; i < math.getNumChildren(); ++i)
          {
            builder.AppendMorpheusNode(" and {0}", math.getChild(i), map);
          }
          return builder.ToString();
        }
        case libsbml.AST_LOGICAL_OR:
        {
          var builder = new StringBuilder();
          builder.AppendMorpheusNode("{0}", math.getChild(0), map);
          for (var i = 1; i < math.getNumChildren(); ++i)
          {
            builder.AppendMorpheusNode(" or {0}", math.getChild(i), map);
          }
          return builder.ToString();
        }
        case libsbml.AST_FUNCTION_PIECEWISE:
        {
          var builder = new StringBuilder();
          builder.AppendFormat("if(");
          for (var i = 0; i < math.getNumChildren() - 1; i += 2)
          {
            builder.AppendMorpheusNode("{0}", math.getChild(i + 1), map);
            builder.AppendMorpheusNode(", {0}", math.getChild(i), map);
          }
          builder.AppendMorpheusNode(", {0}", math.getChild(math.getNumChildren() - 1), map);
          builder.AppendFormat(")");
          return builder.ToString();
        }
        case libsbml.AST_FUNCTION:
        {
          var builder = new StringBuilder();
          builder.AppendFormat("{0}(", math.getName());
          builder.AppendFormat("{0}", TranslateExpression(math.getChild(0), map));
          for (var i = 1; i < math.getNumChildren(); ++i)
          {
            builder.AppendFormat(", {0}", TranslateExpression(math.getChild(i), map));
          }
          builder.AppendFormat(")");
          return builder.ToString();
        }
        case libsbml.AST_FUNCTION_POWER:
        case libsbml.AST_POWER:
        {
          var builder = new StringBuilder();
          builder.AppendFormat("pow({0}", TranslateExpression(math.getChild(0), map));
          builder.AppendFormat(", {0})", TranslateExpression(math.getChild(1), map));
          return builder.ToString();
        }
        case libsbml.AST_NAME:
        default:
          if (map != null && map.ContainsKey(math.getName()))
          {
            return "l." + map[math.getName()];
          }
          return math.getName();
      }
    }

    private string TranslateCondition(string type)
    {
      switch (type.ToLowerInvariant())
      {
        default:
        case "flux":
          return "constant";
        case "value":
          return "noflux";
      }
    }

    public string ToMorpheus(string filename = null)
    {
      var settings = new XmlWriterSettings {Indent = true, Encoding = Encoding.UTF8, OmitXmlDeclaration = true};
      var buffer = new StringBuilder();
      var writer = XmlWriter.Create(buffer, settings);

      writer.WriteStartDocument();

      writer.WriteStartElement("MorpheusModel");
      writer.WriteAttributeString("version", "1");

      WriteDescription(writer);
      WriteSpace(writer, filename);
      WriteTime(writer);
      WritePDE(writer);
      WriteAnalysis(writer);

      writer.WriteEndElement(); // MorpheusModel

      writer.WriteEndDocument();
      writer.Flush();
      writer.Close();
      return buffer.ToString();
    }

    private void WriteAnalysis(XmlWriter writer)
    {
      var multiplier = Math.Max(1, numVariables/2);
      const double scale = 4;

      writer.WriteStartElement("Analysis");
      writer.WriteStartElement("Gnuplotter");
      writer.WriteAttributeString("interval", "1");
      writer.WriteAttributeString("timename", "false");
      writer.WriteStartElement("Terminal");
      writer.WriteAttributeString("size",
        string.Format("{0} {1} {2}",
          Math.Max(200*multiplier, scale*dims.getWidth()*multiplier),
          Math.Max(200*multiplier, scale*dims.getHeight()*multiplier),
          scale*dims.getDepth())
        );
      writer.WriteAttributeString("name", "png");
      writer.WriteEndElement(); // Terminal


      for (var i = 0; i < Model.getNumSpecies(); i++)
      {
        var current = Model.getSpecies(i);
        var plugin = (SpatialSpeciesRxnPlugin) current.getPlugin("spatial");
        if (plugin == null) continue;
        if (plugin.getIsSpatial())
        {
          writer.WriteStartElement("PDE");
          writer.WriteAttributeString("symbol-ref", current.getId());
          writer.WriteAttributeString("min", "0");
          writer.WriteEndElement(); // PDE
        }
      }

      writer.WriteEndElement(); // Gnuplotter
      writer.WriteEndElement(); // Analysis
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

    private void WritePDE(XmlWriter writer)
    {
      writer.WriteStartElement("PDE");

      for (var i = 0; i < Model.getNumSpecies(); i++)
      {
        var current = Model.getSpecies(i);
        var plugin = (SpatialSpeciesRxnPlugin) current.getPlugin("spatial");
        if (plugin == null) continue;
        if (!plugin.getIsSpatial()) continue;

        writer.WriteStartElement("Layer");
        writer.WriteAttributeString("symbol", current.getId());
        writer.WriteAttributeString("name", current.isSetName() ? current.getName() : current.getId());

        writer.WriteStartElement("Diffusion");
        writer.WriteAttributeString("rate",
          diffusion.ContainsKey(current.getId()) ? diffusion[current.getId()] : "0");
        writer.WriteEndElement(); // Diffusion

        writer.WriteStartElement("Initial");
        writer.WriteStartElement("InitPDEExpression");
        writer.WriteStartElement("Expression");
        writer.WriteString(initial[current.getId()]);
        writer.WriteEndElement(); // Expression 
        writer.WriteEndElement(); // InitPDEExpression
        writer.WriteEndElement(); // Initial

        if (boundaryValue.ContainsKey(current.getId()))
        {
          var bounds = boundaryValue[current.getId()];
          foreach (var item in bounds)
          {
            writer.WriteStartElement("BoundaryValue");
            writer.WriteAttributeString("boundary", item.Key);
            writer.WriteAttributeString("value", item.Value);
            writer.WriteEndElement(); // BoundaryValue
          }
        }

        writer.WriteEndElement(); // Layer
      }


      WriteSystem(writer);


      writer.WriteEndElement(); // PDE
    }

    private string SimplifyExpression(string expression)
    {
      var result = expression.Replace("+ ( - 1) *", "-");
      result = result.Replace(" +  - ", " - ");
      result = result.Replace(" 1 * ", " ");
      result = result.Replace(" * 1 ", " ");
      if (result.StartsWith("1 * "))
        result = result.Substring("1 * ".Length);
      return result;
    }

    private void WriteSystem(XmlWriter writer)
    {
      writer.WriteStartElement("System");
      writer.WriteAttributeString("solver", "runge-kutta");
      writer.WriteAttributeString("time-step", "0.01");
      writer.WriteAttributeString("name", Model.isSetName() ? Model.getName() : Model.getId());

      for (var i = 0; i < Model.getNumCompartments(); ++i)
      {
        var current = Model.getCompartment(i);

        writer.WriteStartElement("Constant");
        writer.WriteAttributeString("symbol", current.getId());
        writer.WriteAttributeString("value", current.getSize().ToString());
        writer.WriteEndElement(); // Constant
      }
      for (var i = 0; i < Model.getNumParameters(); ++i)
      {
        var current = Model.getParameter(i);
        var plugin = (SpatialParameterPlugin) current.getPlugin("spatial");
        if (plugin != null && plugin.getType() != -1) continue;

        writer.WriteStartElement("Constant");
        writer.WriteAttributeString("symbol", current.getId());
        writer.WriteAttributeString("value", current.getValue().ToString());
        writer.WriteEndElement(); // Constant
      }

      for (var i = 0; i < Model.getNumRules(); ++i)
      {
        var current = Model.getRule(i);
        if (current.isRate())
        {
          writer.WriteStartElement("DiffEqn");
          writer.WriteAttributeString("symbol-ref", current.getVariable());
          writer.WriteStartElement("Expression");
          writer.WriteString(SimplifyExpression(TranslateExpression(current.getMath(), coordinates)));
          writer.WriteEndElement(); // Expression
          writer.WriteEndElement(); // DiffEqn
        }
      }

      writer.WriteEndElement(); // System
    }

    private void WriteTime(XmlWriter writer)
    {
      writer.WriteStartElement("Time");
      writer.WriteStartElement("StartTime");
      writer.WriteAttributeString("unit", "sec");
      writer.WriteAttributeString("value", "0");
      writer.WriteEndElement(); // StartTime
      writer.WriteStartElement("StopTime");
      writer.WriteAttributeString("unit", "sec");
      writer.WriteAttributeString("value", (60*60*24).ToString());
      writer.WriteEndElement(); // StopTime
      writer.WriteStartElement("SaveInterval");
      writer.WriteAttributeString("value", "0");
      writer.WriteEndElement(); // SaveInterval
      writer.WriteStartElement("RandomSeed");
      writer.WriteAttributeString("value", "2");
      writer.WriteEndElement(); // RandomSeed
      writer.WriteEndElement(); // Time
    }

    private void WriteSpace(XmlWriter writer, string filename = null)
    {
      writer.WriteStartElement("Space");
      writer.WriteStartElement("Lattice");
      writer.WriteAttributeString("class", "square");
      writer.WriteStartElement("Size");
      writer.WriteAttributeString("symbol", "size");
      writer.WriteAttributeString("value",
        string.Format("{0} {1} {2}",
          dims.getWidth(),
          dims.getHeight(),
          dims.getDepth())
        );
      writer.WriteEndElement(); // Size
      writer.WriteStartElement("NodeLength");
      writer.WriteAttributeString("value", "1");
      writer.WriteEndElement(); // NodeLength
      writer.WriteStartElement("BoundaryConditions");

      foreach (var entry in boundaryConditions)
      {
        writer.WriteStartElement("Condition");
        writer.WriteAttributeString("boundary", boundaryLables[entry.Key]);
        writer.WriteAttributeString("type", entry.Value);
        writer.WriteEndElement(); // Condition        
      }
      writer.WriteEndElement(); // BoundaryConditions

      // write domain (if analytic)
      var analyticGeometry = Geometry.GetFirstAnalyticGeometry();
      if (filename != null && analyticGeometry != null && analyticGeometry.getNumAnalyticVolumes() > 0)
      {
        var path = Path.GetDirectoryName(filename);
        var name = Path.GetFileNameWithoutExtension(filename);
        if (!string.IsNullOrWhiteSpace(path) && !string.IsNullOrWhiteSpace(name))
        {
          var vol = analyticGeometry.getAnalyticVolume(0);
          writer.WriteStartElement("Domain");
          writer.WriteAttributeString("boundary-type", boundaryConditions.Values.First());
          writer.WriteStartElement("Image");
          var image = analyticGeometry.GenerateTiffForOrdinal(Geometry, (int) vol.getOrdinal());
          image.Save(Path.Combine(path, name + ".tif"), ImageFormat.Tiff);
          writer.WriteAttributeString("path", name + ".tif");
          writer.WriteEndElement(); // Image
          writer.WriteEndElement(); // Domain
        }
      }

      writer.WriteEndElement(); // Lattice
      writer.WriteStartElement("SpaceSymbol");
      writer.WriteAttributeString("symbol", "l");
      writer.WriteAttributeString("name", "location");
      writer.WriteEndElement(); // SpaceSymbol
      writer.WriteEndElement(); // Space
    }

    private void WriteDescription(XmlWriter writer)
    {
      writer.WriteStartElement("Description");
      writer.WriteStartElement("Title");
      if (Model.isSetName())
        writer.WriteString(Model.getName());
      else
        writer.WriteString(Model.getId());
      writer.WriteEndElement(); // Title
      writer.WriteStartElement("Details");
      var errorString = errorBuilder.ToString();
      if (!string.IsNullOrWhiteSpace(errorString))
        writer.WriteString("Conversion messages: " + errorString);
      writer.WriteEndElement(); // Details
      writer.WriteEndElement(); // Description
    }

    public string ToSBML()
    {
      return libsbml.writeSBMLToString(document);
    }
  }
}