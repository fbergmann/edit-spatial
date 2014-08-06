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
    private readonly SpatialModelPlugin SpatialModelPlugin;
    private readonly Geometry Geometry;

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

      prop = new ConversionProperties();
      prop.addOption("renameSIds", true);
      prop.addOption("currentIds", "default");
      prop.addOption("newIds", "__default");
      status = document.convert(prop);
      if (status != libsbml.LIBSBML_OPERATION_SUCCESS)
      {
        errorBuilder.AppendFormat("rename of ids failed: {0}{1}", status, Environment.NewLine);
      }

      Model = document.getModel();

      for (int i = 0; i < Model.getNumRules(); ++i)
      {
        var current = Model.getRule(i);
        if (current == null || current.getTypeCode() != libsbml.SBML_RATE_RULE) continue;
        OdeVariables.Add(current.getVariable());
      }

      SpatialModelPlugin = (SpatialModelPlugin)Model.getPlugin("spatial");

      if (SpatialModelPlugin == null) return;

      Geometry = SpatialModelPlugin.getGeometry();

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
        case libsbml.AST_FUNCTION_POWER:
          {
            {
              var builder = new StringBuilder();
              builder.AppendDuneNode("pow({0}", math.getChild(0), map);
              builder.AppendDuneNode(", {0})", math.getChild(1), map);
              return builder.ToString();
            }
          }
        case libsbml.AST_DIVIDE:
          {
            var builder = new StringBuilder();
            builder.AppendDuneNode("{0}", math.getChild(0), map);
            builder.AppendDuneNode(" / {0}", math.getChild(1), map);
            return builder.ToString();
          }
        case libsbml.AST_MINUS:
          {
            var builder = new StringBuilder();
            if (math.getNumChildren() == 1)
            {
              builder.AppendDuneNode(" - {0}", math.getChild(0), map);
            }
            else
            {
              builder.AppendDuneNode("{0}", math.getChild(0), map);
              builder.AppendDuneNode(" - {0}", math.getChild(1), map);
            }
            return builder.ToString();
          }
        case libsbml.AST_PLUS:
          {
            var builder = new StringBuilder();
            builder.AppendDuneNode("{0}", math.getChild(0), map);
            for (int i = 1; i < math.getNumChildren(); ++i)
            {
              builder.AppendDuneNode(" + {0}", math.getChild(i), map);
            }
            return builder.ToString();
          }
        case libsbml.AST_TIMES:
          {
            var builder = new StringBuilder();
            builder.AppendDuneNode("{0}", math.getChild(0), map);
            for (int i = 1; i < math.getNumChildren(); ++i)
            {
              builder.AppendDuneNode(" * {0}", math.getChild(i), map);
            }
            return builder.ToString();
          }

        case libsbml.AST_RELATIONAL_LEQ:
          {
            var builder = new StringBuilder();
            builder.AppendDuneNode("{0}", math.getChild(0), map);
            builder.AppendDuneNode(" <= {0}", math.getChild(1), map);
            return builder.ToString();
          }
        case libsbml.AST_RELATIONAL_LT:
          {
            var builder = new StringBuilder();
            builder.AppendDuneNode("{0}", math.getChild(0), map);
            builder.AppendDuneNode(" < {0}", math.getChild(1), map);
            return builder.ToString();
          }
        case libsbml.AST_RELATIONAL_GT:
          {
            var builder = new StringBuilder();
            builder.AppendDuneNode("{0}", math.getChild(0), map);
            builder.AppendDuneNode(" > {0}", math.getChild(1), map);
            return builder.ToString();
          }
        case libsbml.AST_RELATIONAL_GEQ:
          {
            var builder = new StringBuilder();
            builder.AppendDuneNode("{0}", math.getChild(0), map);
            builder.AppendDuneNode(" >= {0}", math.getChild(1), map);
            return builder.ToString();
          }
        case libsbml.AST_POWER:
          {
            var builder = new StringBuilder();
            builder.AppendFormat("pow({0}", TranslateExpression(math.getChild(0), map));
            builder.AppendFormat(", {0})", TranslateExpression(math.getChild(1), map));
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
        case libsbml.AST_LOGICAL_NOT:
          {
            var builder = new StringBuilder();
            builder.AppendFormat("!({0})", TranslateExpression(math.getChild(0), map));
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
            string name = math.getName();
            var builder = new StringBuilder();
            builder.AppendFormat("{0}(", name);
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
          {
            string name = math.getName();
            if (map != null && map.ContainsKey(name))
            {
              return map[name];
            }
            return name;
          }


      }
    }

    public string ToSBML()
    {
      return libsbml.writeSBMLToString(document);
    }

    public void ExportTo(string filename)
    {
      var path = Path.GetDirectoryName(filename);
      string name = Path.GetFileNameWithoutExtension(filename);
      WriteCMakeLists(path, name);
      WriteConfigFile(path, name);
      WriteComponentParameters(path);
      WriteInitialConditions(path);
      WriteLocalOperator(path);
      WriteMainFile(path, name);
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

    private int GetBoundaryType(Parameter param)
    {
      var plug = (SpatialParameterPlugin)param.getPlugin("spatial");
      if (plug == null) return 0;
      var bc = plug.getBoundaryCondition();
      if (bc == null) return 0;
      if (bc.getType() == "Flux") return 1; // Dirichlet
      return 0; // Neumann
    }

    private int GetBoundaryType(Parameter xmin, Parameter xmax, Parameter ymin, Parameter ymax)
    {
      if (xmin != null) return GetBoundaryType(xmin);
      if (xmax != null) return GetBoundaryType(xmax);
      if (ymin != null) return GetBoundaryType(ymin);
      if (ymax != null) return GetBoundaryType(ymax);
      return 0;
    }

    private void WriteConfigFile(string path, string name)
    {
      var builder = new StringBuilder();
      var map = new Dictionary<string, string>();

      map["%NAME%"] = name;

      map["%TIME_TEND%"] = "500";
      map["%TIME_DTMAX%"] = "1";
      map["%TIME_DTPLOT%"] = "1";

      map["%WORLD_WIDTH%"] = Geometry.getCoordinateComponent(0).getBoundaryMax().getValue().ToString();
      map["%WORLD_HEIGHT%"] = Geometry.getCoordinateComponent(1).getBoundaryMax().getValue().ToString();

      map["%GRID_X%"] = "64";
      map["%GRID_Y%"] = "64";

      builder.AppendLine(ExpandTemplate(EditSpatial.Properties.Resources.config, map));

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

      for (int i = 0; i < Model.getNumSpecies(); ++i)
      {
        var current = Model.getSpecies(i);
        if (current == null || !current.getBoundaryCondition()) continue;
        ParameterIds.Add(current.getId());
        builder.AppendFormat("{0} = {1}{2}", current.getId(), current.getInitialConcentration(), Environment.NewLine);

      }


      builder.AppendLine();

      int count = 1;
      // add component sections
      for (int i = 0; i < OdeVariables.Count; ++i)
      {
        var species = Model.getSpecies(OdeVariables[i]);
        if (species == null) continue;
        var plug = (SpatialSpeciesRxnPlugin)species.getPlugin("spatial");
        if (plug == null || plug.getIsSpatial() == false) continue;

        var diff = species.getDiffusionX();
        if (!diff.HasValue)
          diff = species.getDiffusionY();

        if (!diff.HasValue) diff = 0;


        var Xmin = species.getSpatialParameter(libsbml.SBML_SPATIAL_BOUNDARYCONDITION, "Xmin");
        var Xmax = species.getSpatialParameter(libsbml.SBML_SPATIAL_BOUNDARYCONDITION, "Xmax");
        if (Xmin == null) Xmin = Xmax;
        if (Xmax == null) Xmax = Xmin;
        var Ymin = species.getSpatialParameter(libsbml.SBML_SPATIAL_BOUNDARYCONDITION, "Ymin");
        var Ymax = species.getSpatialParameter(libsbml.SBML_SPATIAL_BOUNDARYCONDITION, "Ymax");
        if (Ymin == null) Ymin = Ymax;
        if (Ymax == null) Ymax = Ymin;

        var type = GetBoundaryType(Xmin, Xmax, Ymin, Ymax);

        builder.AppendFormat("[Component{0}]{1}", count, Environment.NewLine);
        builder.AppendFormat("# {0}{1}", species.getId(), Environment.NewLine);
        builder.AppendFormat("D = {0}{1}", diff.Value, Environment.NewLine);
        builder.AppendFormat("Xmin = {0}{1}", Xmin == null ? 0 : Xmin.getValue(), Environment.NewLine);
        builder.AppendFormat("Xmax = {0}{1}", Xmax == null ? 0 : Xmax.getValue(), Environment.NewLine);
        builder.AppendFormat("Ymin = {0}{1}", Ymin == null ? 0 : Ymin.getValue(), Environment.NewLine);
        builder.AppendFormat("Ymax = {0}{1}", Ymax == null ? 0 : Ymax.getValue(), Environment.NewLine);
        builder.AppendFormat("BCType = {0}{1}", type, Environment.NewLine);

        builder.AppendLine();
        ++count;

      }

      File.WriteAllText(Path.Combine(path, name + ".conf"),
        builder.ToString());
    }

    private void WriteMainFile(string path, string name)
    {
      var builder = new StringBuilder();
      var map = new Dictionary<string, string>();

      var dpCreation = new StringBuilder();
      var dpInitialization = new StringBuilder();
      var subCreation = new StringBuilder();
      var initialTypeCreation = new StringBuilder();
      var initialTypes = new StringBuilder();
      var initialArgs = new StringBuilder();
      var gridFunctions = new StringBuilder();

      map["%NUMCOMPONENTS%"] = OdeVariables.Count.ToString();
      map["%GEOMETRY%"] = GenerateGeometryExpression();

      for (int index = 0; index < OdeVariables.Count; index++)
      {
        dpCreation.AppendFormat("    DP dp{0}(param, \"Component{0}\");{1}"
          , index + 1
          , Environment.NewLine);

        dpInitialization.AppendFormat("dp{0}", index + 1);
        initialTypes.AppendFormat("U{0}InitialType", index);
        initialArgs.AppendFormat("u{0}initial", index);
        if (index + 1 < OdeVariables.Count)
        {
          dpInitialization.Append(",");
          initialTypes.Append(",");
          initialArgs.Append(",");
        }

        subCreation.AppendFormat("    typedef Dune::PDELab::GridFunctionSubSpace<TPGFS,Dune::TypeTree::TreePath<{0}> > U{0}SUB;{1}"
          , index, Environment.NewLine);
        subCreation.AppendFormat("    U{0}SUB u{0}sub(tpgfs);{1}"
          , index, Environment.NewLine);

        initialTypeCreation.AppendFormat("    typedef U{0}Initial<GV,RF> U{0}InitialType;{1}"
          , index, Environment.NewLine);
        initialTypeCreation.AppendFormat("    U{0}InitialType u{0}initial(gv,param);{1}"
          , index, Environment.NewLine);

        gridFunctions.AppendFormat("    typedef Dune::PDELab::DiscreteGridFunction<U{0}SUB,V> U{0}DGF;{1}"
          , index, Environment.NewLine);
        gridFunctions.AppendFormat("    U{0}DGF u{0}dgf(u{0}sub,uold);{1}"
          , index, Environment.NewLine);
        gridFunctions.AppendFormat("    pvdwriter.addVertexData(new Dune::PDELab::VTKGridFunctionAdapter<U{0}DGF>(u{0}dgf,\"{1}\"));{2}"
          , index
          , OdeVariables[index]
          , Environment.NewLine);

      }

      map["%DPCREATION%"] = dpCreation.ToString();
      map["%SUBCREATION%"] = subCreation.ToString();
      map["%DPINITIALIZATION%"] = dpInitialization.ToString();
      map["%INITIALTYPECREATION%"] = initialTypeCreation.ToString();
      map["%INITIALTYPES%"] = initialTypes.ToString();
      map["%INITIALARGS%"] = initialArgs.ToString();
      map["%CREATEGRIDFUNCTIONS%"] = gridFunctions.ToString();
      map["%NAME%"] = name;

      builder.AppendLine(ExpandTemplate(EditSpatial.Properties.Resources.main, map));

      File.WriteAllText(Path.Combine(path, name + ".cc"),
        builder.ToString());
    }

    private string GenerateGeometryExpression()
    {
      // by default return the full area
      string result = "return true;";
      // string result = GenerateFish();   // or the fish

      if (SpatialModelPlugin == null) return result;

      if (Geometry == null || Geometry.getNumGeometryDefinitions() == 0) return result;

      var vol = Geometry.GetFirstAnalyticGeometry();
      if (vol == null || vol.getNumAnalyticVolumes() == 0) return result;

      var volume = vol.getAnalyticVolume(0);
      var builder = new StringBuilder();
      builder.Append("    const auto& x = point[0];\n");
      builder.Append("    const auto& y = point[1];\n");
      builder.Append("    const auto& width = dimension[0];\n");
      builder.Append("    const auto& height = dimension[1];\n");
      builder.Append("\n");
      builder.AppendFormat("    bool inside={0};\n",
        TranslateExpression(volume.getMath()));
      builder.Append("    return inside;\n");
      return builder.ToString();

    }

    private static string GenerateFish()
    {
      return
        "    const auto& x = point[0];\n" +
        "    const auto& y = point[1];\n" +
        "\n" +
        "    bool inside= (pow(0.27 * (-42. + x), 2.) + pow(0.4 * (-50. + y), 2.) < 100. ||\n" +
        "            (y < (-10.+ x) &&  y > (110. + -x) && x < 90.));\n" +
        "\n" +
        "    return inside;\n";
    }

    private void WriteLocalOperator(string path)
    {
      File.WriteAllText(Path.Combine(path, "local_operator.hh"),
        ExpandTemplate(EditSpatial.Properties.Resources.local_operator));
    }

    private void WriteInitialConditions(string path)
    {
      var builder = new StringBuilder();
      builder.AppendLine("#ifndef INITIAL_CONDITIONS_H");
      builder.AppendLine("#define INITIAL_CONDITIONS_H");

      int count = 0;
      foreach (var item in OdeVariables)
      {
        var map = new Dictionary<string, string>();
        map["%COUNT%"] = count.ToString();

        var initializer = new StringBuilder();
        var declarations = new StringBuilder();
        var odes = new StringBuilder();
        var odes2 = new StringBuilder();

        foreach (var pItem in ParameterIds)
        {
          initializer.AppendFormat(
            "	   , {0}(param.sub(\"Reaction\").template get<RF>(\"{0}\")){1}"
            , pItem
            , Environment.NewLine
            );
          declarations.AppendFormat(
            "    const RF {0};{1}"
            , pItem
            , Environment.NewLine
            );
        }


        map["%INITIALIZER%"] = initializer.ToString();
        map["%DECLARATION%"] = declarations.ToString();

        var initial = Model.getInitialAssignment(item);
        if (initial != null && initial.isSetMath())
        {
          var tempBuilder = new StringBuilder();
          tempBuilder.AppendLine("        const int dim = Traits::GridViewType::Grid::dimension;");
          tempBuilder.AppendLine("        typedef typename Traits::GridViewType::Grid::ctype ctype;");
          tempBuilder.AppendLine("        Dune::FieldVector<ctype,dim> x = e.geometry().global(xlocal);");
          tempBuilder.AppendLine();
          tempBuilder.AppendFormat(
            "        __initial = {0};{1}",
            TranslateExpression(initial.getMath(),
              new Dictionary<string, string>
              {
                {"x", "x[0]"},
                {"y", "x[1]"},
                {"z", "x[2]"}
              }
              ),
            Environment.NewLine
            );
          map["%INITIALCONDITION%"] = tempBuilder.ToString();
        }
        else
        {
          var species = Model.getSpecies(item);
          if (species != null)
          {
            if (species.isSetInitialConcentration())
              map["%INITIALCONDITION%"] = string.Format(
                "        __initial = {0};{1}"
                , species.getInitialConcentration().ToString("E17")
                , Environment.NewLine);
            else
              map["%INITIALCONDITION%"] = string.Format(
                "        __initial = {0};{1}"
                , species.getInitialAmount().ToString("E17")
                , Environment.NewLine);
          }
        }

        //map["%INITIALCONDITION%"] =TranslateExpression(rule.getMath(), varMap),;
        builder.AppendLine(ExpandTemplate(EditSpatial.Properties.Resources.initial_conditions, map));
        builder.AppendLine();
        ++count;
      }

      builder.AppendLine("#endif // INITIAL_CONDITIONS_H");

      File.WriteAllText(Path.Combine(path, "initial_conditions.hh"),
        builder.ToString());
    }

    private void WriteComponentParameters(string path)
    {
      File.WriteAllText(Path.Combine(path, "componentparameters.hh"),
        ExpandTemplate(EditSpatial.Properties.Resources.componentparameters));
    }

    private void WriteCMakeLists(string path, string name)
    {
      File.WriteAllText(Path.Combine(path, "CMakeLists.txt"),
        ExpandTemplate(EditSpatial.Properties.Resources.CMakeLists,
        new Dictionary<string, string>{
          {"%NAME%", name}
        }
        ));
    }

    private string ExpandTemplate(string content,
      Dictionary<string, string> map = null)
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