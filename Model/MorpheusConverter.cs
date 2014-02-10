using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using libsbmlcs;

namespace EditSpatial.Model
{
  public class MorpheusConverter
  {
    private readonly SBMLDocument document;
    private readonly StringBuilder errorBuilder;
    private readonly libsbmlcs.Model Model;

    private readonly Dictionary<string, string> boundaryConditions;
    private readonly Dictionary<string, string> diffusion;
    private readonly Dictionary<string, string> initial;
    private readonly Dictionary<string, string> coordinates;
    private readonly Dictionary<string, string> boundaryLables;

    private readonly Dimensions dims;

    public Geometry Geometry
    {
      get
      {
        var plugin = (SpatialModelPlugin)Model.getPlugin("spatial");
        if (plugin == null) return null;
        return plugin.getGeometry();
      }
    }
    
    
    public MorpheusConverter(SBMLDocument original)
    {
      errorBuilder = new StringBuilder();
      this.document = original.clone();
      var prop = new ConversionProperties();
      prop.addOption("replaceReactions", true,
        "Replace reactions with rateRules");
      if (document.convert(prop) != libsbml.LIBSBML_OPERATION_SUCCESS)
      {
        errorBuilder.AppendFormat("conversion of rates failed: {0}{1}", 1, 1);
      }

      Model = document.getModel();

      dims = new Dimensions();
      dims.setWidth(GetBoundaryMaxValue(0));
      dims.setHeight(GetBoundaryMaxValue(1));
      dims.setDepth(GetBoundaryMaxValue(2));

      boundaryConditions = new Dictionary<string, string>();
      diffusion = new Dictionary<string, string>();
      for (int i = 0; i < Model.getNumParameters(); ++i)
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
          diffusion[diff.getVariable()] = current.getValue().ToString();
        }
      }

      boundaryLables = new Dictionary<string, string>();
      coordinates = new Dictionary<string, string>();
      for (int i = 0; i < Geometry.getNumCoordinateComponents(); ++i)
      {
        var current = Geometry.getCoordinateComponent(i);        
        string prefix = "x";
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

      initial = new Dictionary<string, string>();
      for (int i = 0; i < Model.getNumSpecies(); ++i)
      {
        var current = Model.getSpecies(i);
        var plugin = (SpatialSpeciesRxnPlugin)current.getPlugin("spatial");
        if (plugin == null || !plugin.getIsSpatial()) continue;

        if (current.isSetInitialConcentration())
          initial[current.getId()] = current.getInitialConcentration().ToString();
        else if (current.isSetInitialAmount())
          initial[current.getId()] = current.getInitialAmount().ToString();

        var assignment = Model.getInitialAssignment(current.getId());
        if (assignment == null) continue;
        initial[current.getId()] = TranslateExpression(assignment.getMath(), coordinates);
      }

    }

    public static string TranslateExpression(string expression)
    {
      return TranslateExpression(libsbml.parseFormula(expression), null);
    }

    public static string TranslateExpression(ASTNode math, Dictionary<string,string> map)
    {
      if (math == null) return "";
      switch (math.getType())
      {
        case libsbml.AST_INTEGER:
          return math.getInteger().ToString();
        case libsbml.AST_REAL:
          return math.getReal().ToString();
        case libsbml.AST_DIVIDE:
          {
            var builder = new StringBuilder();
            builder.AppendFormat("{0}", TranslateExpression(math.getChild(0), map));
            builder.AppendFormat(" / {0}", TranslateExpression(math.getChild(1), map));
            return builder.ToString();
          }
        case libsbml.AST_MINUS:
          {
            var builder = new StringBuilder();
            if (math.getNumChildren() == 1)
            {
              builder.AppendFormat(" - {0}", TranslateExpression(math.getChild(0), map));
            }
            else
            {
              builder.AppendFormat("{0}", TranslateExpression(math.getChild(0), map));
              builder.AppendFormat(" - {0}", TranslateExpression(math.getChild(1), map));
            }
            return builder.ToString();
          }
        case libsbml.AST_PLUS:
          {
          var builder = new StringBuilder();
          builder.AppendFormat("{0}", TranslateExpression(math.getChild(0), map));
          for (int i = 1; i < math.getNumChildren(); ++i)
          {
            builder.AppendFormat(" + {0}", TranslateExpression(math.getChild(i), map));
          }
          return builder.ToString();
          }
        case libsbml.AST_TIMES:
          {
            var builder = new StringBuilder();
            builder.AppendFormat("{0}", TranslateExpression(math.getChild(0), map));
            for (int i = 1; i < math.getNumChildren(); ++i)
            {
              builder.AppendFormat(" * {0}", TranslateExpression(math.getChild(i), map));
            }
            return builder.ToString();
          }

        case libsbml.AST_RELATIONAL_LEQ:
          {
            var builder = new StringBuilder();
            builder.AppendFormat("{0}", TranslateExpression(math.getChild(0), map));
            builder.AppendFormat(" <= {0}", TranslateExpression(math.getChild(1), map));
            return builder.ToString();
          }
        case libsbml.AST_RELATIONAL_LT:
        {
          var builder = new StringBuilder();
          builder.AppendFormat("{0}", TranslateExpression(math.getChild(0), map));
          builder.AppendFormat(" < {0}", TranslateExpression(math.getChild(1), map));
          return builder.ToString();
        }
        case libsbml.AST_RELATIONAL_GT:
        {
          var builder = new StringBuilder();
          builder.AppendFormat("{0}", TranslateExpression(math.getChild(0), map));
          builder.AppendFormat(" > {0}", TranslateExpression(math.getChild(1), map));
          return builder.ToString();
        }
        case libsbml.AST_RELATIONAL_GEQ:
        {
          var builder = new StringBuilder();
          builder.AppendFormat("{0}", TranslateExpression(math.getChild(0), map));
          builder.AppendFormat(" >= {0}", TranslateExpression(math.getChild(1), map));
          return builder.ToString();
        }
        case libsbml.AST_LOGICAL_AND:
        {
          var builder = new StringBuilder();
          builder.AppendFormat("{0}", TranslateExpression(math.getChild(0), map));
          for (int i = 1; i < math.getNumChildren(); ++i)
          {
            builder.AppendFormat(" and {0}", TranslateExpression(math.getChild(i), map));
          }
          return builder.ToString();
        }
        case libsbml.AST_LOGICAL_OR:
        {
          var builder = new StringBuilder();
          builder.AppendFormat("{0}", TranslateExpression(math.getChild(0), map));
          for (int i = 1; i < math.getNumChildren(); ++i)
          {
            builder.AppendFormat(" or {0}", TranslateExpression(math.getChild(i), map));
          }
          return builder.ToString();
        }
        case libsbml.AST_FUNCTION_PIECEWISE:
        {
          var builder = new StringBuilder();
          builder.AppendFormat("if(");
          for (int i = 0; i < math.getNumChildren()-1; i+=2)
          {
            builder.AppendFormat("{0}", TranslateExpression(math.getChild(i + 1), map));
            builder.AppendFormat(", {0}", TranslateExpression(math.getChild(i), map));
          }
          builder.AppendFormat(", {0}", TranslateExpression(math.getChild(math.getNumChildren() - 1), map));
          builder.AppendFormat(")");
          return builder.ToString();
        }
        case libsbml.AST_FUNCTION:
        {
          var builder = new StringBuilder();
          builder.AppendFormat("{0}(", math.getName());
          builder.AppendFormat("{0}", TranslateExpression(math.getChild(0), map));
          for (int i = 1; i < math.getNumChildren(); ++i)
          {
            builder.AppendFormat(", {0}", TranslateExpression(math.getChild(i), map));
          }
          builder.AppendFormat(")");
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
          return "noflux";
        case "value":
          return "constant";          
      }
    }


    public string ToMorpheus()
    {
      
      var settings = new XmlWriterSettings {Indent = true, Encoding = Encoding.UTF8, OmitXmlDeclaration = true};
      var buffer = new StringBuilder();
      var writer = XmlWriter.Create(buffer, settings);
      
      writer.WriteStartDocument();

      writer.WriteStartElement("MorpheusModel");
      writer.WriteAttributeString("version", "1");

      WriteDescription(writer);
      WriteSpace(writer);
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
      const double scale = 4;

      writer.WriteStartElement("Analysis");
      writer.WriteStartElement("Gnuplotter");
      writer.WriteAttributeString("interval", "1");
      writer.WriteAttributeString("timename", "false");
      writer.WriteStartElement("Terminal");
      writer.WriteAttributeString("size", 
        string.Format("{0} {1} {2}",
          scale*dims.getWidth(),
          scale*dims.getHeight(),
          scale*dims.getDepth())
        );
      writer.WriteAttributeString("name","png");
      writer.WriteEndElement(); // Terminal


      for (int i = 0; i < Model.getNumSpecies(); i++)
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

    private double GetBoundaryMaxValue(int val)
    {
      try
      {
        return Geometry.getCoordinateComponent(val).getBoundaryMax().getValue();
      }
      catch
      {
        return 0;
      }
    }

    private void WritePDE(XmlWriter writer)
    {
      writer.WriteStartElement("PDE");

      for (int i = 0; i < Model.getNumSpecies(); i++)
      {
        var current = Model.getSpecies(i);
        var plugin = (SpatialSpeciesRxnPlugin)current.getPlugin("spatial");
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

        //TODO: BOundary value


        writer.WriteEndElement(); // Layer
      }


      WriteSystem(writer);
        

      writer.WriteEndElement(); // PDE
    }

    private void WriteSystem(XmlWriter writer)
    {
      writer.WriteStartElement("System");
      writer.WriteAttributeString("solver", "runge-kutta");
      writer.WriteAttributeString("time-step", "0.01");
      writer.WriteAttributeString("name", Model.isSetName() ? Model.getName() : Model.getId());

      for (int i = 0; i < Model.getNumCompartments(); ++i)
      {
        var current = Model.getCompartment(i);

        writer.WriteStartElement("Constant");
        writer.WriteAttributeString("symbol", current.getId());
        writer.WriteAttributeString("value", current.getSize().ToString());
        writer.WriteEndElement(); // Constant
      }
      for (int i = 0; i < Model.getNumParameters(); ++i)
      {
        var current = Model.getParameter(i);
        var plugin = (SpatialParameterPlugin) current.getPlugin("spatial");
        if (plugin != null && plugin.getType() != -1) continue;

        writer.WriteStartElement("Constant");
        writer.WriteAttributeString("symbol", current.getId());
        writer.WriteAttributeString("value", current.getValue().ToString());
        writer.WriteEndElement(); // Constant
      }

      for (int i = 0; i < Model.getNumRules(); ++i)
      {
        var current = Model.getRule(i);
        if (current.isRate())
        {
          writer.WriteStartElement("DiffEqn");
          writer.WriteAttributeString("symbol-ref", current.getVariable());
          writer.WriteStartElement("Expression");
          writer.WriteString(TranslateExpression(current.getMath(), coordinates));
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

    private void WriteSpace(XmlWriter writer)
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
      writer.WriteString(Model.getName());
      writer.WriteEndElement(); // Title
      writer.WriteStartElement("Details");
      writer.WriteString("Conversion messages: " + errorBuilder.ToString());
      writer.WriteEndElement(); // Details
      writer.WriteEndElement(); // Description
     
    }

    public string ToSBML()
    {
      return libsbml.writeSBMLToString(document);
    }
  }
}