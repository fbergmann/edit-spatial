using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using EditSpatial.Model;
using EditSpatial.Properties;
using libsbmlcs;

namespace EditSpatial.Converter
{
  public class DuneConverter : IDisposable
  {
    private readonly SBMLDocument _Document;
    private readonly StringBuilder _errorBuilder;
    private readonly Geometry _Geometry;
    private readonly libsbmlcs.Model _Model;
    private readonly SBMLDocument _Original;
    private readonly SpatialModelPlugin SpatialModelPlugin;

    private List<string> AdditionalVars;

    /// <summary>
    /// This function tags all reactions in the document with a factor that indicates whether 
    /// it is spanning multiple compartments.
    /// </summary>
    /// <param name="document"></param>
    private void TagReactions(SBMLDocument document)
    {
      AdditionalVars = new List<string>();

      var model = document.getModel();
      if (model == null) return;
      if (model.getNumCompartments() < 2) return;

      var numReactions = model.getNumReactions();
      for (int i = 0; i < numReactions; ++i)
      {
        var compartments = new List<string>();
        var reaction = model.getReaction(i);

        if (!reaction.isSetKineticLaw()) continue;

        var kinetics = reaction.getKineticLaw();
        if (kinetics == null || !kinetics.isSetMath()) continue;

        var numSubstrates = reaction.getNumReactants();
        var numProducts = reaction.getNumProducts();
        var numModifiers = reaction.getNumModifiers();
        if (numSubstrates + numProducts < 2) continue;

        for (int j = 0; j < numSubstrates; ++j)
        {
          var current = reaction.getReactant(j);
          if (!current.isSetSpecies()) continue;
          var species = model.getSpecies(current.getSpecies());
          if (species == null || !species.isSetCompartment()) continue;
          string compartment = species.getCompartment();
          if (!compartments.Contains(compartment))
          {
            compartments.Add(compartment);
          }
        }

        for (int j = 0; j < numProducts; ++j)
        {
          var current = reaction.getProduct(j);
          if (!current.isSetSpecies()) continue;
          var species = model.getSpecies(current.getSpecies());
          if (species == null || !species.isSetCompartment()) continue;
          string compartment = species.getCompartment();
          if (!compartments.Contains(compartment))
          {
            compartments.Add(compartment);
          }
        }

        for (int j = 0; j < numModifiers; ++j)
        {
          var current = reaction.getModifier(j);
          if (!current.isSetSpecies()) continue;
          var species = model.getSpecies(current.getSpecies());
          if (species == null || !species.isSetCompartment()) continue;
          string compartment = species.getCompartment();
          if (!compartments.Contains(compartment))
          {
            compartments.Add(compartment);
          }
        }

        if (compartments.Count < 2) continue;

        compartments.Sort();

        var formula = new StringBuilder();
        foreach (var id in compartments)
        {
          formula.AppendFormat("isIn_{0} * ", id);
          if (!AdditionalVars.Contains("isIn_" + id))
          {
            // create parameter temporarily to get passt validation later on
            var param = model.createParameter();
            param.setId("isIn_" + id);            
            AdditionalVars.Add("isIn_" + id);
          }
        }

        formula.AppendFormat("({0})", libsbml.formulaToString(kinetics.getMath()));
        kinetics.setMath(libsbml.parseFormula(formula.ToString()));

      }

    }

    public DuneConverter(SBMLDocument original)
    {
      OdeVariables = new List<string>();
      _errorBuilder = new StringBuilder();
      _Document = original.clone();
      _Original = original;

      TagReactions(_Document);

      _Document.getErrorLog().setSeverityOverride(libsbml.LIBSBML_OVERRIDE_DONT_LOG);
      var prop = new ConversionProperties();
      prop.addOption("promoteLocalParameters", true,
        "Promotes all Local Parameters to Global ones");
      var status = _Document.convert(prop);
      if (status != libsbml.LIBSBML_OPERATION_SUCCESS)
      {
        _errorBuilder.AppendFormat("promoting local to global parameters failed: {0}{1}", status, Environment.NewLine);
      }

      prop = new ConversionProperties();
      prop.addOption("expandFunctionDefinitions", true);
      status = _Document.convert(prop);
      if (status != libsbml.LIBSBML_OPERATION_SUCCESS)
      {
        _errorBuilder.AppendFormat("expanding function definitions failed: {0}{1}", status, Environment.NewLine);
      }

      prop = new ConversionProperties();
      prop.addOption("replaceReactions", true,
        "Replace reactions with rateRules");
      status = _Document.convert(prop);
      if (status != libsbml.LIBSBML_OPERATION_SUCCESS)
      {
        _errorBuilder.AppendFormat("conversion of rates failed: {0}{1}", status, Environment.NewLine);
      }

      prop = new ConversionProperties();
      prop.addOption("renameSIds", true);
      prop.addOption("currentIds", "default");
      prop.addOption("newIds", "__default");
      status = _Document.convert(prop);
      if (status != libsbml.LIBSBML_OPERATION_SUCCESS)
      {
        _errorBuilder.AppendFormat("rename of ids failed: {0}{1}", status, Environment.NewLine);
      }

      _Model = _Document.getModel();

      // remove parameters added only to pass validation
      foreach (var id in AdditionalVars)
      {
        _Model.removeParameter(id);
      }

      for (var i = 0; i < _Model.getNumRules(); ++i)
      {
        var current = _Model.getRule(i);
        if (current == null || current.getTypeCode() != libsbml.SBML_RATE_RULE) continue;
        OdeVariables.Add(current.getVariable());
      }

      SpatialModelPlugin = (SpatialModelPlugin) _Model.getPlugin("spatial");

      if (SpatialModelPlugin == null) return;

      _Geometry = SpatialModelPlugin.getGeometry();
    }

    public string Errors
    {
      get
      {
        if (_errorBuilder == null) return "";
        return _errorBuilder.ToString();
      }
    }

    public double GeometryMin { get; set; }
    public double GeometryMax { get; set; }
    private List<string> OdeVariables { get; set; }
    private List<string> ParameterIds { get; set; }
    public string GeometryFile { get; set; }

    public void Dispose()
    {
      if (_Document != null)
        _Document.Dispose();
      if (_Model != null)
        _Model.Dispose();
      if (SpatialModelPlugin != null)
        SpatialModelPlugin.Dispose();
      if (_Geometry != null)
        _Geometry.Dispose();
    }

    public static string TranslateExpression(string expression, Dictionary<string, string> map = null,
      libsbmlcs.Model model = null)
    {
      return TranslateExpression(libsbml.parseFormula(expression), map, model);
    }

    public static string TranslateExpression(ASTNode math, Dictionary<string, string> map = null,
      libsbmlcs.Model model = null)
    {
      if (math == null) return "";
      switch (math.getType())
      {
        case libsbml.AST_INTEGER:
          return math.getInteger().ToString("E17");
        case libsbml.AST_REAL:
        case libsbml.AST_REAL_E:
          return math.getReal().ToString("E17");
        case libsbml.AST_FUNCTION_POWER:
        {
          {
            var builder = new StringBuilder();
            builder.AppendDuneNode("pow({0}", math.getChild(0), map, model);
            builder.AppendDuneNode(", {0})", math.getChild(1), map, model);
            return builder.ToString();
          }
        }
        case libsbml.AST_DIVIDE:
        {
          var builder = new StringBuilder();
          builder.AppendDuneNode("{0}", math.getChild(0), map, model);
          builder.AppendDuneNode(" / {0}", math.getChild(1), map, model);
          return builder.ToString();
        }
        case libsbml.AST_MINUS:
        {
          var builder = new StringBuilder();
          if (math.getNumChildren() == 1)
          {
            builder.AppendDuneNode(" - {0}", math.getChild(0), map, model);
          }
          else
          {
            builder.AppendDuneNode("{0}", math.getChild(0), map, model);
            builder.AppendDuneNode(" - {0}", math.getChild(1), map, model);
          }
          return builder.ToString();
        }
        case libsbml.AST_PLUS:
        {
          var builder = new StringBuilder();
          builder.AppendDuneNode("{0}", math.getChild(0), map, model);
          for (var i = 1; i < math.getNumChildren(); ++i)
          {
            builder.AppendDuneNode(" + {0}", math.getChild(i), map, model);
          }
          return builder.ToString();
        }
        case libsbml.AST_TIMES:
        {
          var builder = new StringBuilder();
          builder.AppendDuneNode("{0}", math.getChild(0), map, model);
          for (var i = 1; i < math.getNumChildren(); ++i)
          {
            builder.AppendDuneNode(" * {0}", math.getChild(i), map, model);
          }
          return builder.ToString();
        }
        case libsbml.AST_RELATIONAL_LEQ:
        {
          var builder = new StringBuilder();
          builder.AppendDuneNode("{0}", math.getChild(0), map, model);
          builder.AppendDuneNode(" <= {0}", math.getChild(1), map, model);
          return builder.ToString();
        }
        case libsbml.AST_RELATIONAL_LT:
        {
          var builder = new StringBuilder();
          builder.AppendDuneNode("{0}", math.getChild(0), map, model);
          builder.AppendDuneNode(" < {0}", math.getChild(1), map, model);
          return builder.ToString();
        }
        case libsbml.AST_RELATIONAL_GT:
        {
          var builder = new StringBuilder();
          builder.AppendDuneNode("{0}", math.getChild(0), map, model);
          builder.AppendDuneNode(" > {0}", math.getChild(1), map, model);
          return builder.ToString();
        }
        case libsbml.AST_RELATIONAL_GEQ:
        {
          var builder = new StringBuilder();
          builder.AppendDuneNode("{0}", math.getChild(0), map, model);
          builder.AppendDuneNode(" >= {0}", math.getChild(1), map, model);
          return builder.ToString();
        }
        case libsbml.AST_POWER:
        {
          var builder = new StringBuilder();
          builder.AppendFormat("pow({0}", TranslateExpression(math.getChild(0), map, model));
          builder.AppendFormat(", {0})", TranslateExpression(math.getChild(1), map, model));
          return builder.ToString();
        }
        case libsbml.AST_LOGICAL_AND:
        {
          var builder = new StringBuilder();
          builder.AppendFormat("({0}", TranslateExpression(math.getChild(0), map, model));
          for (var i = 1; i < math.getNumChildren(); ++i)
          {
            builder.AppendFormat(" && {0}", TranslateExpression(math.getChild(i), map, model));
          }
          builder.AppendFormat(")");
          return builder.ToString();
        }
        case libsbml.AST_LOGICAL_NOT:
        {
          var builder = new StringBuilder();
          builder.AppendFormat("!({0})", TranslateExpression(math.getChild(0), map, model));
          return builder.ToString();
        }
        case libsbml.AST_LOGICAL_OR:
        {
          var builder = new StringBuilder();
          builder.AppendFormat("({0}", TranslateExpression(math.getChild(0), map, model));
          for (var i = 1; i < math.getNumChildren(); ++i)
          {
            builder.AppendFormat(" || {0}", TranslateExpression(math.getChild(i), map, model));
          }
          builder.AppendFormat(")");
          return builder.ToString();
        }
        case libsbml.AST_FUNCTION_PIECEWISE:
        {
          var builder = new StringBuilder();
          builder.AppendFormat("(");
          for (var i = 0; i < math.getNumChildren() - 1; i += 2)
          {
            builder.AppendFormat("({0}) ", TranslateExpression(math.getChild(i + 1), map, model));
            builder.AppendFormat("? {0} :", TranslateExpression(math.getChild(i), map, model));
          }
          builder.AppendFormat(" {0}", TranslateExpression(math.getChild(math.getNumChildren() - 1), map, model));
          builder.AppendFormat(")");
          return builder.ToString();
        }
        case libsbml.AST_FUNCTION:
        {
          var name = math.getName();
          var builder = new StringBuilder();
          builder.AppendFormat("{0}(", name);
          builder.AppendFormat("{0}", TranslateExpression(math.getChild(0), map, model));
          for (var i = 1; i < math.getNumChildren(); ++i)
          {
            builder.AppendFormat(", {0}", TranslateExpression(math.getChild(i), map, model));
          }
          builder.AppendFormat(")");
          return builder.ToString();
        }
        //case libsbml.AST_NAME:
        default:
        {
          var name = math.getName();
          if (map != null && map.ContainsKey(name))
          {
            if (map.ContainsKey("use_compartments") && model != null)
            {
              var species = model.getSpecies(name);
              if (species == null || !species.isSetCompartment()) return map[name];
              return String.Format("isIn_{0} * {1}", species.getCompartment(), map[name]);
            }
            return map[name];
          }
          return name;
        }
      }
    }

    public string ToSBML()
    {
      return libsbml.writeSBMLToString(_Document);
    }

    public void ExportTo(string filename)
    {
      var path = Path.GetDirectoryName(filename);
      var name = Path.GetFileNameWithoutExtension(filename);
      WriteCMakeLists(path, name);
      WriteConfigFile(path, name);
      WriteComponentParameters(path);
      WriteInitialConditions(path);
      WriteLocalOperator(path);
      WriteMainFile(path, name);
      WriteReactionAdapter(path);
      if (_Original != null && path != null)
        libsbml.writeSBMLToFile(_Original, Path.Combine(path, "sbml.xml"));
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
          "    RF {0};{1}"
          , item
          , Environment.NewLine
          );
      }

      var count = 0;
      odes.AppendLine("        //// variable map: variable -> x(lsfu,...)");
      var varMap = new Dictionary<string, string>();
      foreach (var item in OdeVariables)
      {
        varMap[item] = string.Format("x(lfsu,{0})", count);
        odes.AppendFormat("        // {0} -> {1}{2}", item, varMap[item], Environment.NewLine);
        ++count;
      }
      odes.AppendLine();
      odes.AppendLine();


      var numCompartments = _Model.getNumCompartments();
      if (numCompartments > 1)
      {
        varMap["use_compartments"] = "1";
        odes.AppendLine("        //// determine whether current location is in a compartment");
        for (var i = 0; i < numCompartments; ++i)
        {
          var comp = _Model.getCompartment(i);
          var compId = comp.getId();
          initializer.AppendFormat(
            "	   , dh_in{0}(DataHelper::forFile(param.sub(\"Reaction\").template get<std::string>(\"in_{0}\", \"\"), NearestNeighbor)){1}"
            , compId
            , Environment.NewLine
            );
          declarations.AppendFormat(
            "    const DataHelper* dh_in{0};{1}"
            , compId
            , Environment.NewLine
            );
          odes.AppendFormat(
            "        RF isIn_{0} = dh_in{0} == NULL ? 1 :(RF)dh_in{0}->get((double)pos[0], (double)pos[1]);{1}", compId,
            Environment.NewLine);
        }
      }


      odes.AppendLine();
      odes.AppendLine();
      odes.AppendLine("        //// calculate assignment rules");
      for (var i = 0; i < _Model.getNumRules(); ++i)
      {
        var rule = _Model.getRule(i) as AssignmentRule;
        if (rule == null || rule.getTypeCode() != libsbml.SBML_ASSIGNMENT_RULE) continue;
        odes.AppendFormat("        {0} = {1};{2}",
          TranslateExpression(rule.getVariable(), varMap),
          TranslateExpression(rule.getMath(), varMap, _Model),
          Environment.NewLine
          );
      }
      odes.AppendLine();
      odes.AppendLine();

      count = 1;
      odes.AppendLine("        //// calculate rate rules");
      foreach (var item in OdeVariables)
      {
        var rule = _Model.getRateRule(item);
        if (rule == null || !rule.isSetMath()) continue;

        odes.AppendFormat("        // {0} {1}", item, Environment.NewLine);
        var species = _Model.getSpecies(rule.getVariable());
        if (species != null && numCompartments > 1 && species.isSetCompartment())
          odes.AppendFormat("        RF r{0} = isIn_{1}*({2});{3}",
            count,
            species.getCompartment(),
            TranslateExpression(rule.getMath(), varMap, _Model),
            Environment.NewLine
            );
        else
          odes.AppendFormat("        RF r{0} = {1};{2}",
            count,
            TranslateExpression(rule.getMath(), varMap, _Model),
            Environment.NewLine
            );

        odes2.AppendFormat("        r.accumulate(lfsv,{0},-r{1}*eg.geometry().volume());{2}",
          count - 1,
          count,
          Environment.NewLine
          );
        odes.AppendLine();
        ++count;
      }

      odes.AppendLine();
      odes.AppendLine(odes2.ToString());


      map["%INITIALIZER%"] = initializer.ToString();
      map["%DECLARATION%"] = declarations.ToString();
      map["%ODES%"] = odes.ToString();

      File.WriteAllText(Path.Combine(path, "reactionadapter.hh"),
        ExpandTemplate(Resources.reactionadapter, map));
    }

    private int GetBoundaryType(Parameter param)
    {
      var plug = (SpatialParameterPlugin) param.getPlugin("spatial");
      if (plug == null) return -1;
      var bc = plug.getBoundaryCondition();
      if (bc == null) return -1;
      if (bc.getType() == "Value") return 1; // Dirichlet
      return -1; // Neumann
    }

    private int GetBoundaryType(Parameter xmin, Parameter xmax, Parameter ymin, Parameter ymax)
    {
      if (xmin != null) return GetBoundaryType(xmin);
      if (xmax != null) return GetBoundaryType(xmax);
      if (ymin != null) return GetBoundaryType(ymin);
      if (ymax != null) return GetBoundaryType(ymax);
      return -1;
    }

    private void WriteConfigFile(string path, string name)
    {
      var builder = new StringBuilder();
      var map = new Dictionary<string, string>();

      map["%GEOMETRY_FILE%"] = GeometryFile;
      map["%GEOMETRY_MIN%"] = GeometryMin.ToString(CultureInfo.InvariantCulture);
      map["%GEOMETRY_MAX%"] = GeometryMax.ToString(CultureInfo.InvariantCulture);

      map["%NAME%"] = name;
      map["%TIME_TEND%"] = "500";
      map["%TIME_DTMAX%"] = "1";
      map["%TIME_DTPLOT%"] = "1";

      map["%WORLD_WIDTH%"] = _Geometry.getCoordinateComponent(0).getBoundaryMax().getValue().ToString(CultureInfo.InvariantCulture);
      map["%WORLD_HEIGHT%"] = _Geometry.getCoordinateComponent(1).getBoundaryMax().getValue().ToString(CultureInfo.InvariantCulture);

      map["%GRID_X%"] = "64";
      map["%GRID_Y%"] = "64";

      builder.AppendLine(ExpandTemplate(Resources.config, map));

      // add reaction section
      builder.AppendLine("[Reaction]");
      ParameterIds = new List<string>();
      for (var i = 0; i < _Model.getNumParameters(); ++i)
      {
        var current = _Model.getParameter(i);
        if (current.IsSpatial()) continue;

        ParameterIds.Add(current.getId());
        double currentValue = current.getValue();
        if (double.IsNaN(currentValue)) currentValue = 0;
        builder.AppendFormat("{0} = {1}{2}", current.getId(), currentValue, Environment.NewLine);
      }

      for (var i = 0; i < _Model.getNumCompartments(); ++i)
      {
        var current = _Model.getCompartment(i);

        ParameterIds.Add(current.getId());
        builder.AppendFormat("{0} = {1}{2}", current.getId(), current.getSize(), Environment.NewLine);
      }

      for (var i = 0; i < _Model.getNumSpecies(); ++i)
      {
        var current = _Model.getSpecies(i);
        if (current == null || !current.getBoundaryCondition()) continue;
        ParameterIds.Add(current.getId());
        double currentValue = current.getInitialConcentration();
        if (!current.isSetInitialConcentration()) currentValue = current.getInitialAmount();
        builder.AppendFormat("{0} = {1}{2}", current.getId(), currentValue, Environment.NewLine);
      }

      builder.AppendLine();

      // add component sections
      for (var i = 0; i < OdeVariables.Count; ++i)
      {
        var species = _Model.getSpecies(OdeVariables[i]);
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

        builder.AppendFormat("[{0}]{1}", species.getId(), Environment.NewLine);
        builder.AppendFormat("# Species '{0}' in Compartment '{1}'{2}", species.getId(), species.getCompartment(),
          Environment.NewLine);
        builder.AppendFormat("D = {0}{1}", diff.Value, Environment.NewLine);
        builder.AppendFormat("Xmin = {0}{1}", Xmin == null ? 0 : Xmin.getValue(), Environment.NewLine);
        builder.AppendFormat("Xmax = {0}{1}", Xmax == null ? 0 : Xmax.getValue(), Environment.NewLine);
        builder.AppendFormat("Ymin = {0}{1}", Ymin == null ? 0 : Ymin.getValue(), Environment.NewLine);
        builder.AppendFormat("Ymax = {0}{1}", Ymax == null ? 0 : Ymax.getValue(), Environment.NewLine);
        builder.AppendFormat("# Dirichlet=1, Neumann=-1, Outflow=-2, None=-3{0}", Environment.NewLine);
        builder.AppendFormat("BCType = {0}{1}", type, Environment.NewLine);

        builder.AppendLine();
      }

      builder.AppendLine();
      builder.AppendLine("[Data]");
      builder.AppendLine("# use this section to override initial conditions of any variable");
      builder.AppendLine("# by simply specifying the variable followed by a filename");
      builder.AppendLine("# of the dmp file containing the initial values");
      foreach (var item in OdeVariables)
        builder.AppendFormat("{0} = {1}", item, Environment.NewLine);
      builder.AppendLine();

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
      map["%GEOMETRY%"] = GenerateGeometryExpression(path);

      for (var index = 0; index < OdeVariables.Count; index++)
      {
        dpCreation.AppendFormat("    DP dp{0}(param, \"{1}\");{2}"
          , index + 1
          , OdeVariables[index]
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

        subCreation.AppendFormat(
          "    typedef Dune::PDELab::GridFunctionSubSpace<TPGFS,Dune::TypeTree::TreePath<{0}> > U{0}SUB;{1}"
          , index, Environment.NewLine);
        subCreation.AppendFormat("    U{0}SUB u{0}sub(tpgfs);{1}"
          , index, Environment.NewLine);


        initialTypeCreation.AppendFormat("    UGeneralInitialType u{0}initial(gv,param,{0});{1}"
          , index, Environment.NewLine);

        gridFunctions.AppendFormat("    typedef Dune::PDELab::DiscreteGridFunction<U{0}SUB,V> U{0}DGF;{1}"
          , index, Environment.NewLine);
        gridFunctions.AppendFormat("    U{0}DGF u{0}dgf(u{0}sub,uold);{1}"
          , index, Environment.NewLine);
        gridFunctions.AppendFormat(
          "    pvdwriter.addVertexData(new Dune::PDELab::VTKGridFunctionAdapter<U{0}DGF>(u{0}dgf,\"{1}\"));{2}"
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

      builder.AppendLine(ExpandTemplate(Resources.main, map));

      File.WriteAllText(Path.Combine(path, name + ".cc"),
        builder.ToString());
    }

    private static string GenerateGeometryExpressionForAnalyticVolume(AnalyticGeometry vol)
    {
      var volume = vol.getAnalyticVolume(0);
      var builder = new StringBuilder();
      builder.Append("\n");
      builder.AppendFormat("    bool inside={0};\n",
        TranslateExpression(volume.getMath()));
      builder.Append("    return inside;\n");
      return builder.ToString();
    }

    private string WriteFieldToFile(string path, ImageData data, int numSamples1, int numSamples2, int z = 0,
      string basename = "geometry1.dmp")
    {
      var name = Path.Combine(path, basename);
      var length = data.getUncompressedLength();
      var array = new int[length];
      data.getUncompressed(array);

      using (var writer = new StreamWriter(name))
      {
        writer.WriteLine("{0} {1}", numSamples1, numSamples2);
        for (var i = 0; i < numSamples1; ++i)
        {
          for (var j = 0; j < numSamples2; ++j)
          {
            writer.Write(array[i + numSamples1*j + numSamples1*numSamples2*z]);
            writer.Write(" ");
          }
          writer.WriteLine();
        }
      }

      return name;
    }

    private string GenerateExpressionForSampleFieldGeometry(SampledFieldGeometry sample, string path)
    {
      var field = sample.getSampledField();
      var data = field.getImageData();

      GeometryFile = WriteFieldToFile(path,
        data,
        field.getNumSamples1(),
        field.getNumSamples2());

      var min = sample.getSampledVolume(0).getMinValue();
      var max = sample.getSampledVolume(0).getMaxValue();

      GeometryMin = min;
      GeometryMax = max;

      return "";
    }

    private string GenerateGeometryExpression(string path)
    {
      // by default return the full area
      var result = "return true;";
      // string result = GenerateFish();   // or the fish

      if (SpatialModelPlugin == null) return result;

      if (_Geometry == null || _Geometry.getNumGeometryDefinitions() == 0) return result;

      var vol = _Geometry.GetFirstAnalyticGeometry();
      if (vol != null && vol.getNumAnalyticVolumes() > 0)
        return GenerateGeometryExpressionForAnalyticVolume(vol);

      var sample = _Geometry.GetFirstSampledFieldGeometry();
      if (sample != null && sample.getNumSampledVolumes() > 0)
        return GenerateExpressionForSampleFieldGeometry(sample, path);

      return result;
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
        ExpandTemplate(Resources.local_operator));
    }

    private void WriteInitialConditions(string path)
    {
      var builder = new StringBuilder();
      builder.AppendLine("#ifndef INITIAL_CONDITIONS_H");
      builder.AppendLine("#define INITIAL_CONDITIONS_H");
      builder.AppendLine();
      builder.AppendLine("#include <dune/copasi/utilities/datahelper.hh>");
      builder.AppendLine();
      var map = new Dictionary<string, string>();

      var initializer = new StringBuilder();
      var declarations = new StringBuilder();

      foreach (var pItem in ParameterIds)
      {
        initializer.AppendFormat(
          "	   , {0}(param.sub(\"Reaction\").template get<RF>(\"{0}\")){1}"
          , pItem
          , Environment.NewLine
          );
        declarations.AppendFormat(
          "    RF {0};{1}"
          , pItem
          , Environment.NewLine
          );
      }

      var varMap
        = new Dictionary<string, string>
        {
          {"x", "x[0]"},
          {"y", "x[1]"},
          {"z", "x[2]"}
        };


      var additionalInitializations = new StringBuilder();

      var tempBuilder = new StringBuilder();

      var numCompartments = _Model.getNumCompartments();
      if (numCompartments > 1)
      {
        additionalInitializations.AppendLine("      //// determine whether current location is in a compartment");
        for (var i = 0; i < numCompartments; ++i)
        {
          var comp = _Model.getCompartment(i);
          var compId = comp.getId();
          initializer.AppendFormat(
            "	   , dh_in{0}(DataHelper::forFile(param.sub(\"Reaction\").template get<std::string>(\"in_{0}\", \"\"), NearestNeighbor)){1}"
            , compId
            , Environment.NewLine
            );
          declarations.AppendFormat(
            "    const DataHelper* dh_in{0};{1}"
            , compId
            , Environment.NewLine
            );
          additionalInitializations.AppendFormat(
            "      RF isIn_{0} = dh_in{0} == NULL ? 1 :(RF)dh_in{0}->get((double)x[0], (double)x[1]);{1}", compId,
            Environment.NewLine);
        }
      }


      var count = 0;
      foreach (var item in OdeVariables)
      {
        initializer.AppendFormat(
          "	   , dh_{0}(DataHelper::forFile(param.sub(\"Data\").template get<std::string>(\"{0}\"))){1}"
          , item
          , Environment.NewLine
          );

        declarations.AppendFormat(
          "    const DataHelper* dh_{0};{1}"
          , item
          , Environment.NewLine
          );

        tempBuilder.AppendLine();
        tempBuilder.AppendFormat("        case {0}:{1}", count, Environment.NewLine);
        tempBuilder.AppendLine("        {");
        tempBuilder.AppendFormat("          // initial conditions for {0}{1}", item, Environment.NewLine);

        tempBuilder.AppendFormat("          if(dh_{0} != NULL){1}", item, Environment.NewLine);
        tempBuilder.AppendFormat(
          "            __initial  = (typename Traits::RangeType)(*dh_{0}).get((double)(x[0]),(double)x[1] );{1}", item,
          Environment.NewLine);
        tempBuilder.AppendLine("          else");


        tempBuilder.Append("            __initial = ");

        var species = _Model.getSpecies(item);
        if (numCompartments > 1 && species != null && species.isSetCompartment())
        {
          tempBuilder.AppendFormat("isIn_{0} * ", species.getCompartment());
        }

        var initial = _Model.getInitialAssignment(item);
        if (initial != null && initial.isSetMath())
        {
          tempBuilder.AppendFormat(
            "{0};{1}",
            TranslateExpression(initial.getMath(),
              varMap, _Model
              ),
            Environment.NewLine
            );
          tempBuilder.AppendLine();
        }
        else
        {
          if (species != null)
          {
            if (species.isSetInitialConcentration())
              tempBuilder.AppendFormat(
                "{0};{1}"
                , species.getInitialConcentration().ToString("E17")
                , Environment.NewLine);
            else
              tempBuilder.AppendFormat(
                "{0};{1}"
                , species.getInitialAmount().ToString("E17")
                , Environment.NewLine);
          }
        }
        tempBuilder.AppendLine("          break;");
        tempBuilder.AppendLine("        }");

        ++count;
      }


      map["%INITIALIZER%"] = initializer.ToString();
      map["%DECLARATION%"] = declarations.ToString();
      map["%INITIALCONDITION%"] = tempBuilder.ToString();
      map["%ADDITIONAL_INITIAlIZATION%"] = additionalInitializations.ToString();

      builder.AppendLine(ExpandTemplate(Resources.initial_conditions, map));
      builder.AppendLine();

      builder.AppendLine("#endif // INITIAL_CONDITIONS_H");

      File.WriteAllText(Path.Combine(path, "initial_conditions.hh"),
        builder.ToString());
    }

    private void WriteComponentParameters(string path)
    {
      File.WriteAllText(Path.Combine(path, "componentparameters.hh"),
        ExpandTemplate(Resources.componentparameters));
    }

    private void WriteCMakeLists(string path, string name)
    {
      File.WriteAllText(Path.Combine(path, "CMakeLists.txt"),
        ExpandTemplate(Resources.CMakeLists,
          new Dictionary<string, string>
          {
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