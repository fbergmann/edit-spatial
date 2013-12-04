using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using libsbmlcs;

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
            else
            {
              Debug.WriteLine(message);
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
  }
}
