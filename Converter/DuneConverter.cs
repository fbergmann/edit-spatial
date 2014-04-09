using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using libsbmlcs;

namespace EditSpatial.Converter
{
  public class DuneConverter : IDisposable
  {
    private readonly SBMLDocument document;
    private readonly StringBuilder errorBuilder;
    private readonly libsbmlcs.Model Model;
    private List<string> OdeVariables { get; set; }
    public void Dispose()
    {
      if (document != null)
        document.Dispose();
      if (Model != null)
        Model.Dispose();
    }

    public DuneConverter(SBMLDocument original)
    {
      OdeVariables = new List<string>();
      errorBuilder = new StringBuilder();
      document = original.clone();

      document.getErrorLog().setSeverityOverride(libsbml.LIBSBML_OVERRIDE_DONT_LOG);
      var prop = new ConversionProperties();
      prop.addOption("promoteLocalParameters", true,
        "Promotes all Local Parameters to Global ones");
      var status = document.convert(prop);
      if (status != libsbml.LIBSBML_OPERATION_SUCCESS)
      {
        errorBuilder.AppendFormat("promoting local to global parameters failed: {0}{1}", status, Environment.NewLine);
      } 
      
      prop = new ConversionProperties();
      prop.addOption("expandFunctionDefinitions", true);
      status = document.convert(prop);
      if (status != libsbml.LIBSBML_OPERATION_SUCCESS)
      {
        errorBuilder.AppendFormat("expanding function definitions failed: {0}{1}", status, Environment.NewLine);
      }

      prop = new ConversionProperties();
      prop.addOption("replaceReactions", true,
        "Replace reactions with rateRules");
      status = document.convert(prop);
      if (status != libsbml.LIBSBML_OPERATION_SUCCESS)
      {
        errorBuilder.AppendFormat("conversion of rates failed: {0}{1}", status, Environment.NewLine);
      }

      Model = document.getModel();

      for (int i = 0; i < Model.getNumRules(); ++i)
      {
        var current = Model.getRule(i);
        if (current == null || current.getTypeCode() != libsbml.SBML_RATE_RULE) continue;
        OdeVariables.Add(current.getVariable());
      }

    }


    public static string TranslateExpression(string expression)
    {
      return TranslateExpression(libsbml.parseFormula(expression), null);
    }

    public static string TranslateExpression(ASTNode math, Dictionary<string, string> map = null)
    {
      if (math == null) return "";
      switch (math.getType())
      {
        case libsbml.AST_INTEGER:
          return math.getInteger().ToString("E17");
        case libsbml.AST_REAL:
          return math.getReal().ToString("E17");
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
            builder.AppendFormat("({0}", TranslateExpression(math.getChild(0), map));
            for (int i = 1; i < math.getNumChildren(); ++i)
            {
              builder.AppendFormat(" && {0}", TranslateExpression(math.getChild(i), map));
            }
            builder.AppendFormat(")");
            return builder.ToString();
          }
        case libsbml.AST_LOGICAL_OR:
          {
            var builder = new StringBuilder();
            builder.AppendFormat("({0}", TranslateExpression(math.getChild(0), map));
            for (int i = 1; i < math.getNumChildren(); ++i)
            {
              builder.AppendFormat(" || {0}", TranslateExpression(math.getChild(i), map));
            }
            builder.AppendFormat(")");
            return builder.ToString();
          }
        case libsbml.AST_FUNCTION_PIECEWISE:
          {
            var builder = new StringBuilder();
            builder.AppendFormat("(");
            for (int i = 0; i < math.getNumChildren() - 1; i += 2)
            {
              builder.AppendFormat("({0}) ", TranslateExpression(math.getChild(i + 1), map));
              builder.AppendFormat("? {0} :", TranslateExpression(math.getChild(i), map));
            }
            builder.AppendFormat(" {0}", TranslateExpression(math.getChild(math.getNumChildren() - 1), map));
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
            return map[math.getName()];
          }
          return math.getName();


      }
    }

    public string ToSBML()
    {
      return libsbml.writeSBMLToString(document);
    }

    public void ExportTo(string path)
    {
      WriteCMakeLists(path);
      WriteConfigFile(path);
      WriteComponentParameters(path);
      WriteInitialConditions(path);
      WriteLocalOperator(path);
      WriteMainFile(path);
      WriteReactionAdapter(path);
    }

    private void WriteReactionAdapter(string path)
    {

      var map = new Dictionary<string, string>();

      var initializer = new StringBuilder();
      var declarations = new StringBuilder();
      var odes = new StringBuilder();
      var odes2 = new StringBuilder();

      foreach (var item in ParameterIds)
      {
        initializer.AppendFormat(
          "	   , {0}(param.sub(\"Reaction\").template get<RF>(\"{0}\")){1}"
          , item
          , Environment.NewLine
          );
        declarations.AppendFormat(
          "    const RF {0};{1}"
          , item
          , Environment.NewLine
          );
      }
      int count = 0;
      var varMap = new Dictionary<string, string>();
      foreach (var item in OdeVariables) 
      {
        varMap[item] = string.Format("x(lfsu,{0})", count);
        ++count;
      }

    count = 1;
      foreach (var item in OdeVariables)
      {
        var rule = Model.getRateRule(item);
        if (rule == null || !rule.isSetMath()) continue;

        odes.AppendFormat("        RF r{0} = {1};{2}",
          count,
          TranslateExpression(rule.getMath(), varMap),
          Environment.NewLine
          );

        odes2.AppendFormat("        r.accumulate(lfsv,{0},-r{1}*eg.geometry().volume());{2}",
          count - 1,
          count,
          Environment.NewLine
          );
        ++count;
      }

      odes.AppendLine();
      odes.AppendLine(odes2.ToString());


      map["%INITIALIZER%"] = initializer.ToString();
      map["%DECLARATION%"] = declarations.ToString();
      map["%ODES%"] = odes.ToString();

      File.WriteAllText(Path.Combine(path, "reactionadapter.hh"),
        ExpandTemplate(EditSpatial.Properties.Resources.reactionadapter, map));
    }

    List<string> ParameterIds { get; set; }

    private void WriteConfigFile(string path)
    {
      var builder = new StringBuilder();
      builder.AppendLine(ExpandTemplate(EditSpatial.Properties.Resources.config));

      // add reaction section
      builder.AppendLine("[Reaction]");
      ParameterIds = new List<string>();
      for (int i = 0; i < Model.getNumParameters(); ++i)
      {
        var current = Model.getParameter(i);
        if (current.IsSpatial()) continue;

        ParameterIds.Add(current.getId());
        builder.AppendFormat("{0} = {1}{2}", current.getId(), current.getValue(), Environment.NewLine);
      }

      for (int i = 0; i < Model.getNumCompartments(); ++i)
      {
        var current = Model.getCompartment(i);

        ParameterIds.Add(current.getId());
        builder.AppendFormat("{0} = {1}{2}", current.getId(), current.getSize(), Environment.NewLine);
        
      }


      builder.AppendLine();

      int count = 1;
      // add component sections
      for (int i = 0; i < Model.getNumSpecies(); ++i)
      {
        var species = Model.getSpecies(i);
        if (species.getBoundaryCondition()) continue;
        var plug = (SpatialSpeciesRxnPlugin)species.getPlugin("spatial");
        if (plug == null || plug.getIsSpatial() == false) continue;

        var diff = species.getDiffusionX();
        if (!diff.HasValue)
          diff = species.getDiffusionY();

        if (!diff.HasValue) continue;

        builder.AppendFormat("[Component{0}]{1}", count, Environment.NewLine);
        builder.AppendFormat("D = {0}{1}", diff.Value, Environment.NewLine);
        builder.AppendLine();
        ++count;

      }



      File.WriteAllText(Path.Combine(path, "main.conf"),
        builder.ToString());
    }

    private void WriteMainFile(string path)
    {
      File.WriteAllText(Path.Combine(path, "main.cc"),
        ExpandTemplate(EditSpatial.Properties.Resources.main));
    }

    private void WriteLocalOperator(string path)
    {
      File.WriteAllText(Path.Combine(path, "local_operator.hh"),
        ExpandTemplate(EditSpatial.Properties.Resources.local_operator));
    }

    private void WriteInitialConditions(string path)
    {
      File.WriteAllText(Path.Combine(path, "initial_conditions.hh"),
        ExpandTemplate(EditSpatial.Properties.Resources.initial_conditions));
    }

    private void WriteComponentParameters(string path)
    {
      File.WriteAllText(Path.Combine(path, "componentparameters.hh"),
        ExpandTemplate(EditSpatial.Properties.Resources.componentparameters));
    }

    private void WriteCMakeLists(string path)
    {
      File.WriteAllText(Path.Combine(path, "CMakeLists.txt"), 
        ExpandTemplate(EditSpatial.Properties.Resources.CMakeLists));
    }

    private string ExpandTemplate(string content, 
      Dictionary<string,string> map = null)
    {
      var final = content;

      if (map != null)
      foreach (var pair in map)
      {
        final = final.Replace(pair.Key, pair.Value);
      }

      return final;
    }
  }
}