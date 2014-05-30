using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;
using libsbmlcs;
using SysBio.MathKGI;

namespace EditSpatial
{
  static class Util
  {

    public static AnalyticGeometry GetFirstAnalyticGeometry(this libsbmlcs.Geometry geometry)
    {
      if (geometry == null) return null;
      for (int i = 0; i < geometry.getNumGeometryDefinitions(); ++i)
      {
        var analytic = geometry.getGeometryDefinition(i) as AnalyticGeometry;
        if (analytic != null) return analytic;
      }
      return null;
    }

    public static void setInitialExpession(this libsbmlcs.Species species, string expression)
    {
      if (species == null || species.getSBMLDocument() == null || species.getSBMLDocument().getModel() == null)
        return;

      var model = species.getSBMLDocument().getModel();

      var node = libsbml.parseFormula(expression);

      if (node == null) return;

      if (node.isNumber())
      {
        if (species.isSetInitialAmount())
          species.setInitialAmount(GetRealValue(node));
        else
          species.setInitialConcentration(GetRealValue(node));
        return;
      }

      var initial = model.getInitialAssignment(species.getId());
      if (initial == null)
      {
        initial = model.createInitialAssignment();
        initial.setSymbol(species.getId());
      }

      initial.setMath(node);

    }

    private static double GetRealValue(ASTNode node)
    {
      switch(node.getType())
      {
        default:
        case libsbml.AST_REAL:
          return node.getReal();
        case libsbml.AST_REAL_E:
          return Math.Pow(node.getMantissa(), node.getExponent());
        case libsbml.AST_INTEGER:
          return node.getInteger();
        case libsbml.AST_RATIONAL:
          return (double)node.getNumerator() / (double)node.getDenominator();
      }
    }

    public static string getInitialExpession(this libsbmlcs.Species species)
    {
      string result = "";
      if (species == null || species.getSBMLDocument() == null || species.getSBMLDocument().getModel() == null)
        return result;
      
      var model = species.getSBMLDocument().getModel();

      var initial = model.getInitialAssignment(species.getId());
      if (initial != null && initial.isSetMath()) return libsbml.formulaToString(initial.getMath());

      if (species.isSetInitialAmount())
        return species.getInitialAmount().ToString();

      if (species.isSetInitialConcentration())
        return species.getInitialConcentration().ToString();

      return result;
    }

    public static double? getDiffusionY(this libsbmlcs.Species species)
    {
      var param = species.getParameterDiffusionY();
      if (param == null) return null;
      return param.getValue();
    }

    public static bool IsSpatial(this libsbmlcs.Parameter parameter)
    {
      var plug = (SpatialParameterPlugin)parameter.getPlugin("spatial");
      if (plug == null) return false;
      return plug.isSpatialParameter();
    }

    public static Parameter getSpatialParameter(this libsbmlcs.Species species, int typeCode, object box = null)
    {
      if (species == null || species.getSBMLDocument() == null || species.getSBMLDocument().getModel() == null)
        return null;

      var model = species.getSBMLDocument().getModel();
      for (int i = 0; i < model.getNumParameters(); ++i)
      {
        var parameter = model.getParameter(i);
        if (parameter == null) continue;
        var plugin = parameter.getPlugin("spatial") as SpatialParameterPlugin;
        if (plugin == null) continue;

        if (plugin.getType() != typeCode) continue;

        if (typeCode == libsbml.SBML_SPATIAL_DIFFUSIONCOEFFICIENT)
        {
          var diff = plugin.getDiffusionCoefficient();
          int index = 0;
          if (box != null && box is int)
            index = (int)box;
          if (diff == null || diff.getCoordinateIndex() != index || diff.getVariable() != species.getId()) continue;
        }
        else if (typeCode == libsbml.SBML_SPATIAL_BOUNDARYCONDITION)
        {
          var bc = plugin.getBoundaryCondition();
          if (bc == null || bc.getVariable() != species.getId()) continue;
          if (box != null && box is string)
          {
            var bound = box as string;
            if (bc.getCoordinateBoundary() != bound) continue;
          }
        }

        return parameter;
      }

      return null;
    }


    public static Parameter getParameterDiffusionX(this libsbmlcs.Species species)
    {      
      return species.getSpatialParameter(libsbml.SBML_SPATIAL_DIFFUSIONCOEFFICIENT, 0);
    }

    public static Parameter getParameterDiffusionY(this libsbmlcs.Species species)
    {
      return species.getSpatialParameter(libsbml.SBML_SPATIAL_DIFFUSIONCOEFFICIENT, 1);
    }


    public static double? getDiffusionX(this libsbmlcs.Species species)
    {
      var param = species.getParameterDiffusionX();
      if (param == null) return null;
      return param.getValue();
    }

    internal static byte[] ToBytes(int[] array)
    {
      var result = new byte[array.Length];
      for (int i = 0; i < array.Length; i++)
      {
        result[i] = (byte)(array[i]);
      }
      return result;
    }
    private static int[] ToInt(byte[] array)
    {
      var result = new int[array.Length];
      for (int i = 0; i < array.Length; i++)
      {
        result[i] = (int)array[i];
      }
      return result;
    }

    internal static int[] GetArray(ImageData data)
    {
      switch (data.getDataType())
      {
        case "":
        case "compressed":
          {
            var intValue = new int[data.getSamplesLength()];
            data.getSamples(intValue);
            var bytes = ToBytes(intValue);
            var stream = new MemoryStream(bytes);
            var result = new MemoryStream();
            var zipInputStream = new InflaterInputStream(stream);

            var buffer = new byte[4096];		// 4K is optimum

            StreamUtils.Copy(zipInputStream, result, buffer);
            return ToInt(result.ToArray());
          }
        default:
          {
            var intValue = new int[data.getSamplesLength()];
            data.getSamples(intValue);
            return intValue;
          }

      }
    }


    internal static double ComputeValueForFormula(string formula, List<string> variableIds, List<double> variableData)
    {
      if (formula == null && variableIds.Count > 0) formula = variableIds[0];

      // if we are lucky just return a given variable column
      if (variableIds.Contains(formula))
      {
        var index = variableIds.IndexOf(formula);
        return variableData[index];
      }
      // at this point ... we should try and get a libSBML AST Tree and then evaluate it ...

      var tree = libsbml.parseFormula(formula);

      if (tree == null)
        throw new Exception("Invalid MathML in ComputeChange::ComputeValueForFormula");

      return Evaluate(tree, variableIds, variableData, new List<Tuple<string, double>>());
    }
    
    internal static double Evaluate(ASTNode tree, List<string> sVariableIds, List<double> oVariableData,
                                    List<Tuple<string,double>> listOfParameters)
    {
      // no data ...
      if (oVariableData == null || oVariableData.Count < 1)
      {
        if (tree.isReal()) return tree.getReal();
        if (tree.isInteger()) return tree.getInteger();
        return 0;
      }

      return EvaluateSingle(tree, sVariableIds, oVariableData, listOfParameters);
    }

    internal static double EvaluateSingle(ASTNode node, List<string> sVariableIds,
                                         List<double> oVariableData, List<Tuple<string, double>> listOfParameters)
    {
      if (node == null) 
        return 0;
      if (node.isReal()) return node.getReal();
      if (node.isInteger()) return node.getInteger();

      if (node.isOperator())
      {
        switch (node.getType())
        {
          case libsbml.AST_PLUS:
            return
                EvaluateSingle(node.getLeftChild(), sVariableIds, oVariableData, listOfParameters) +
                EvaluateSingle(node.getRightChild(), sVariableIds, oVariableData, listOfParameters);
          case libsbml.AST_MINUS:
            if (node.getNumChildren() == 1)
              return -
                EvaluateSingle(node.getLeftChild(), sVariableIds, oVariableData, listOfParameters);
            return
                EvaluateSingle(node.getLeftChild(), sVariableIds, oVariableData, listOfParameters) -
                EvaluateSingle(node.getRightChild(), sVariableIds, oVariableData, listOfParameters);
          case libsbml.AST_DIVIDE:
            return
                EvaluateSingle(node.getLeftChild(), sVariableIds, oVariableData, listOfParameters) /
                EvaluateSingle(node.getRightChild(), sVariableIds, oVariableData, listOfParameters);
          case libsbml.AST_TIMES:
            return
                EvaluateSingle(node.getLeftChild(), sVariableIds, oVariableData, listOfParameters) *
                EvaluateSingle(node.getRightChild(), sVariableIds, oVariableData, listOfParameters);
          case libsbml.AST_POWER:
            return
                Math.Pow(
                    EvaluateSingle(node.getLeftChild(), sVariableIds, oVariableData, listOfParameters),
                    EvaluateSingle(node.getRightChild(), sVariableIds, oVariableData,
                                   listOfParameters));
          default:
            break;
        }
      }
      else if (node.isName())
      {
        if (sVariableIds.Contains(node.getName()))
        {
          var variableIndex = sVariableIds.IndexOf(node.getName());
          return oVariableData[variableIndex];
        }
        foreach (var param in listOfParameters)
        {
          if (param.Item1 == node.getName())
            return param.Item2;
        }
      }

      // we are still here ... this is bad ... as last measure try a couple of inbuilts
      switch (node.getType())
      {
        case libsbml.AST_FUNCTION_ABS:
          return
              Math.Abs(EvaluateSingle(node.getLeftChild(), sVariableIds, oVariableData,
                                      listOfParameters));
        case libsbml.AST_FUNCTION_ARCCOS:
          return
              Math.Acos(EvaluateSingle(node.getLeftChild(), sVariableIds, oVariableData,
                                       listOfParameters));
        case libsbml.AST_FUNCTION_ARCCOSH:
          return
              MathKGI.Acosh(EvaluateSingle(node.getLeftChild(), sVariableIds, oVariableData,
                                           listOfParameters));
        case libsbml.AST_FUNCTION_ARCCOT:
          return
              MathKGI.Acot(EvaluateSingle(node.getLeftChild(), sVariableIds, oVariableData,
                                          listOfParameters));
        case libsbml.AST_FUNCTION_ARCCOTH:
          return
              MathKGI.Acoth(EvaluateSingle(node.getLeftChild(), sVariableIds, oVariableData,
                                           listOfParameters));
        case libsbml.AST_FUNCTION_ARCCSC:
          return
              MathKGI.Acsc(EvaluateSingle(node.getLeftChild(), sVariableIds, oVariableData,
                                          listOfParameters));
        case libsbml.AST_FUNCTION_ARCCSCH:
          return
              MathKGI.Acsch(EvaluateSingle(node.getLeftChild(), sVariableIds, oVariableData,
                                           listOfParameters));
        case libsbml.AST_FUNCTION_ARCSEC:
          return
              MathKGI.Asec(EvaluateSingle(node.getLeftChild(), sVariableIds, oVariableData,
                                          listOfParameters));
        case libsbml.AST_FUNCTION_ARCSECH:
          return
              MathKGI.Asech(EvaluateSingle(node.getLeftChild(), sVariableIds, oVariableData,
                                           listOfParameters));
        case libsbml.AST_FUNCTION_ARCSIN:
          return
              Math.Asin(EvaluateSingle(node.getLeftChild(), sVariableIds, oVariableData,
                                       listOfParameters));
        case libsbml.AST_FUNCTION_ARCSINH:
          return
              MathKGI.Asinh(EvaluateSingle(node.getLeftChild(), sVariableIds, oVariableData,
                                           listOfParameters));
        case libsbml.AST_FUNCTION_ARCTAN:
          return
              Math.Atan(EvaluateSingle(node.getLeftChild(), sVariableIds, oVariableData,
                                       listOfParameters));
        case libsbml.AST_FUNCTION_ARCTANH:
          return
              MathKGI.Atanh(EvaluateSingle(node.getLeftChild(), sVariableIds, oVariableData,
                                           listOfParameters));
        case libsbml.AST_FUNCTION_CEILING:
          return
              Math.Ceiling(EvaluateSingle(node.getLeftChild(), sVariableIds, oVariableData,
                                          listOfParameters));
        case libsbml.AST_FUNCTION_COS:
          return
              Math.Cos(EvaluateSingle(node.getLeftChild(), sVariableIds, oVariableData,
                                      listOfParameters));
        case libsbml.AST_FUNCTION_COSH:
          return
              Math.Cosh(EvaluateSingle(node.getLeftChild(), sVariableIds, oVariableData,
                                       listOfParameters));
        case libsbml.AST_FUNCTION_COT:
          return
              MathKGI.Cot(EvaluateSingle(node.getLeftChild(), sVariableIds, oVariableData,
                                         listOfParameters));
        case libsbml.AST_FUNCTION_COTH:
          return
              MathKGI.Coth(EvaluateSingle(node.getLeftChild(), sVariableIds, oVariableData,
                                          listOfParameters));
        case libsbml.AST_FUNCTION_CSC:
          return
              MathKGI.Csc(EvaluateSingle(node.getLeftChild(), sVariableIds, oVariableData,
                                         listOfParameters));
        case libsbml.AST_FUNCTION_CSCH:
          return
              MathKGI.Csch(EvaluateSingle(node.getLeftChild(), sVariableIds, oVariableData,
                                          listOfParameters));
        case libsbml.AST_FUNCTION_DELAY:
          return
              MathKGI.Delay(EvaluateSingle(node.getLeftChild(), sVariableIds, oVariableData,
                                           listOfParameters),
                            EvaluateSingle(node.getRightChild(), sVariableIds, oVariableData,
                                           listOfParameters));
        case libsbml.AST_FUNCTION_EXP:
          return
              Math.Exp(EvaluateSingle(node.getLeftChild(), sVariableIds, oVariableData,
                                      listOfParameters));
        case libsbml.AST_FUNCTION_FACTORIAL:
          return
              MathKGI.Factorial(EvaluateSingle(node.getLeftChild(), sVariableIds, oVariableData,
                                               listOfParameters));
        case libsbml.AST_FUNCTION_FLOOR:
          return
              Math.Floor(EvaluateSingle(node.getLeftChild(), sVariableIds, oVariableData,
                                        listOfParameters));
        case libsbml.AST_FUNCTION_LN:
          return
              Math.Log(
                  EvaluateSingle(node.getLeftChild(), sVariableIds, oVariableData, listOfParameters),
                  Math.E);
        case libsbml.AST_FUNCTION_LOG:
          return
              Math.Log10(EvaluateSingle(node.getLeftChild(), sVariableIds, oVariableData,
                                        listOfParameters));
        case libsbml.AST_FUNCTION_PIECEWISE:
          {
            var numChildren = (int)node.getNumChildren();
            var temps = new double[numChildren];
            for (var i = 0; i < numChildren; i++)
            {
              temps[i] = EvaluateSingle(node.getChild(i), sVariableIds, oVariableData,
                                        listOfParameters);
            }
            return Piecewise(temps);
            // MathKGI.Piecewise(temps);
          }
        case libsbml.AST_FUNCTION_POWER:
          return Math.Pow(EvaluateSingle(node.getLeftChild(), sVariableIds, oVariableData,
                                         listOfParameters),
                          EvaluateSingle(node.getRightChild(), sVariableIds, oVariableData,
                                         listOfParameters));
        case libsbml.AST_FUNCTION_ROOT:
          return MathKGI.Root(EvaluateSingle(node.getLeftChild(), sVariableIds, oVariableData,
                                             listOfParameters),
                              EvaluateSingle(node.getRightChild(), sVariableIds, oVariableData,
                                             listOfParameters));
        case libsbml.AST_FUNCTION_SEC:
          return MathKGI.Sec(EvaluateSingle(node.getLeftChild(), sVariableIds, oVariableData,
                                            listOfParameters));
        case libsbml.AST_FUNCTION_SECH:
          return MathKGI.Sech(EvaluateSingle(node.getLeftChild(), sVariableIds, oVariableData,
                                             listOfParameters));
        case libsbml.AST_FUNCTION_SIN:
          return Math.Sin(EvaluateSingle(node.getLeftChild(), sVariableIds, oVariableData,
                                         listOfParameters));
        case libsbml.AST_FUNCTION_SINH:
          return Math.Sinh(EvaluateSingle(node.getLeftChild(), sVariableIds, oVariableData,
                                          listOfParameters));
        case libsbml.AST_FUNCTION_TAN:
          return Math.Tan(EvaluateSingle(node.getLeftChild(), sVariableIds, oVariableData,
                                         listOfParameters));
        case libsbml.AST_FUNCTION_TANH:
          return Math.Tanh(EvaluateSingle(node.getLeftChild(), sVariableIds, oVariableData,
                                          listOfParameters));
        case libsbml.AST_LOGICAL_AND:
          {
            var numChildren = (int)node.getNumChildren();
            var temps = new double[numChildren];
            for (var i = 0; i < numChildren; i++)
            {
              temps[i] = EvaluateSingle(node.getChild(i), sVariableIds, oVariableData,
                                        listOfParameters);
            }
            return
                MathKGI.And(temps);
          }
        case libsbml.AST_LOGICAL_NOT:
          {
            return
                MathKGI.Not(EvaluateSingle(node.getLeftChild(), sVariableIds, oVariableData,
                                           listOfParameters));
          }
        case libsbml.AST_LOGICAL_OR:
          {
            var numChildren = (int)node.getNumChildren();
            var temps = new double[numChildren];
            for (var i = 0; i < numChildren; i++)
            {
              temps[i] = EvaluateSingle(node.getChild(i), sVariableIds, oVariableData,
                                        listOfParameters);
            }
            return
                MathKGI.Or(temps);
          }
        case libsbml.AST_LOGICAL_XOR:
          {
            var numChildren = (int)node.getNumChildren();
            var temps = new double[numChildren];
            for (var i = 0; i < numChildren; i++)
            {
              temps[i] = EvaluateSingle(node.getChild(i), sVariableIds, oVariableData,
                                        listOfParameters);
            }
            return
                MathKGI.Xor(temps);
          }
        case libsbml.AST_RELATIONAL_EQ:
          {
            return
                MathKGI.Eq(EvaluateSingle(node.getLeftChild(), sVariableIds, oVariableData,
                                          listOfParameters),
                           EvaluateSingle(node.getRightChild(), sVariableIds, oVariableData,
                                          listOfParameters));
          }
        case libsbml.AST_RELATIONAL_GEQ:
          return
              MathKGI.Geq(EvaluateSingle(node.getLeftChild(), sVariableIds, oVariableData,
                                         listOfParameters),
                          EvaluateSingle(node.getRightChild(), sVariableIds, oVariableData,
                                         listOfParameters));
        case libsbml.AST_RELATIONAL_GT:
          return
              MathKGI.Gt(EvaluateSingle(node.getLeftChild(), sVariableIds, oVariableData,
                                        listOfParameters),
                         EvaluateSingle(node.getRightChild(), sVariableIds, oVariableData,
                                        listOfParameters));
        case libsbml.AST_RELATIONAL_LEQ:
          return
              MathKGI.Leq(EvaluateSingle(node.getLeftChild(), sVariableIds, oVariableData,
                                         listOfParameters),
                          EvaluateSingle(node.getRightChild(), sVariableIds, oVariableData,
                                         listOfParameters));
        case libsbml.AST_RELATIONAL_LT:
          return
              MathKGI.Lt(EvaluateSingle(node.getLeftChild(), sVariableIds, oVariableData,
                                        listOfParameters),
                         EvaluateSingle(node.getRightChild(), sVariableIds, oVariableData,
                                        listOfParameters));
        case libsbml.AST_RELATIONAL_NEQ:
          return
              MathKGI.Neq(EvaluateSingle(node.getLeftChild(), sVariableIds, oVariableData,
                                         listOfParameters),
                          EvaluateSingle(node.getRightChild(), sVariableIds, oVariableData,
                                         listOfParameters));
        default:
          break;
      }

      return 0;
    }

    public static double Piecewise(double[] args)
    {
      double result;
      for (int i = 0; i < args.Length - 1; i += 2)
      {
        var num = args[i + 1];
        if (num == 1.0)
        {
          result = args[i];
          return result;
        }
      }
      result = args[args.Length - 1];

      return result;
    }

    public static double SaveDouble(string value, double defaultValue=0)
    {
      double val;
      if (double.TryParse(value, out val))
        return val;
      return defaultValue;
    }

    public static long SaveInt(string value, long defaultValue)
    {
      long val;
      if (long.TryParse(value, out val))
        return val;
      return defaultValue;
    }

    public static int SaveInt(string value, int defaultValue)
    {
      int val;
      if (int.TryParse(value, out val))
        return val;
      return defaultValue;
    }
  }
}
