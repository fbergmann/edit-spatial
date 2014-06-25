using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using libsbmlcs;

namespace EditSpatial.Forms
{
  public partial class FormSpatialAnnotation : Form
  {

    public const string SPATIAL_ANNOTATION_URL = "http://fbergmann.github.io/spatial-sbml/settings";

    public FormSpatialAnnotation()
    {
      InitializeComponent();
    }

    public libsbmlcs.Model Model { get; set; }
    public DialogResult Status { get; set; }

    private void InitializeIdCombo(libsbmlcs.Model model)
    {
      colId.Items.Clear();
      for (int i = 0; i < model.getNumSpecies(); i++)
      {
        colId.Items.Add(model.getSpecies(i).getId());
      }
    }

    
    XMLNode getAnnotationNode(libsbmlcs.Model model, string ns)
    {
      if (model == null || !model.isSetAnnotation()) return null;

      XMLNode parent = model.getAnnotation();
      bool again = true;
      while(again)
      {
        again = false;
        for (int i = 0;i < parent.getNumChildren(); ++i)
        {
          XMLNode current = parent.getChild(i);
          if (current.getName() == "annotation")
          {
            again = true;
            parent = current;
            break;
          }

          if (current.hasNamespaceURI(ns))
            return current;
        }
      }

      return null;
    }

    private string Clean(string original)
    {
      var result = original.Replace("<notes>", "");
      result = result.Replace("</notes>", "");
      result = result.Replace("</body>", "");
      result = result.Replace("<pre>", "");
      result = result.Replace("</pre>", "");
      result = result.Replace("<body xmlns=\"http://www.w3.org/1999/xhtml\">", "");
      result = result.Replace("\n", "");
      result = result.Trim();
      return result;
    }

    private void AddRowForNote(string text, Species species)
    {
      text = Clean(text);

      if (string.IsNullOrWhiteSpace(text)) return;
      if (!text.Contains("spatial")) return;


      var palette = "black-blue";
      if (text.Contains("GFP"))
        palette = "black-green";
      if (text.Contains("RFP"))
        palette = "black-red";

      double max = 6;
      double scale = 10;
      if (species.isSetInitialConcentration() && species.getInitialConcentration() > 0)
        max = scale * species.getInitialConcentration();
      else if (species.isSetInitialAmount() && species.getInitialAmount() > 0)
        max = scale * species.getInitialAmount();


      grid.Rows.Add(species.getId(), palette, max.ToString(CultureInfo.InvariantCulture));

    }

    private void InitializeFromNotes(libsbmlcs.Model model)
    {
      grid.Rows.Clear();

      if (model == null) return;

      for (int i =0 ; i< model.getNumSpecies(); ++i)
      {
        var current = model.getSpecies(i);
        if (current == null || !current.isSetNotes()) continue;


        AddRowForNote(current.getNotesString(), current);
        
      }

    }

    private void InitializeFromAnnotation(libsbmlcs.Model model)
    {
      var node = getAnnotationNode(model, SPATIAL_ANNOTATION_URL);
      if (node == null) return;

      XMLNode update = node.getChild("update");
      if (update.getName() == "update")
      {
        txtStep.Text = update.getAttrValue("step");
        txtUpdate.Text = update.getAttrValue("freq");
      }

      grid.Rows.Clear();

      XMLNode items = node.getChild("items");
      if (items.getName() == "items")
      {
        for (int i = 0; i < items.getNumChildren(); ++i)
        {
          XMLNode item = items.getChild(i);

          string id = item.getAttrValue("sbmlId");
          string palette = item.getAttrValue("palette");
          string max = item.getAttrValue("max");

          grid.Rows.Add(id, palette, max);

        }
      }

    }


    public void SaveToModel(libsbmlcs.Model model)
    {
      if (model == null) return;

      var node = new XMLNode(new XMLTriple("spatialInfo", SPATIAL_ANNOTATION_URL, ""), new XMLAttributes());
      node.addAttr("xmlns", SPATIAL_ANNOTATION_URL);

      // save stepsize
      XMLNode update = new XMLNode(new XMLTriple("update", SPATIAL_ANNOTATION_URL, ""), new XMLAttributes());
      update.addAttr("step", txtStep.Text);
      update.addAttr("freq", txtUpdate.Text);

      node.addChild(update);

      // save assignments
      if (grid.Rows.Count > 0)
      {
        XMLNode items = new XMLNode(new XMLTriple("items", SPATIAL_ANNOTATION_URL, ""), new XMLAttributes());

        for (int i = 0; i < grid.Rows.Count; ++i)
        {
          var current = grid.Rows[i];
          XMLNode item = new XMLNode(new XMLTriple("item", SPATIAL_ANNOTATION_URL, ""), new XMLAttributes());
          string id = current.Cells[0].Value as string;
          if (string.IsNullOrWhiteSpace(id)) continue;
          item.addAttr("sbmlId", id);
          item.addAttr("palette", current.Cells[1].Value as string);
          item.addAttr("max", current.Cells[2].Value as string);
          items.addChild(item);
        }
        node.addChild(items);
      }

      model.removeTopLevelAnnotationElement("spatialInfo", SPATIAL_ANNOTATION_URL, false);
      model.appendAnnotation(node);
    }

    public void InitFrom(EditSpatial.Model.SpatialModel spatialModel)
    {
      Status = System.Windows.Forms.DialogResult.Cancel;
      colId.Items.Clear();

      if (spatialModel == null || spatialModel.Document == null || spatialModel.Document.getModel() == null)
        return;

      Model = spatialModel.Document.getModel();
      if (Model == null)
        return;

      InitializeIdCombo(Model);

      InitializeFromAnnotation(Model);

      if (grid.Rows.Count < 2)
        InitializeFromNotes(Model);

    }


    private void FormSpatialAnnotation_FormClosing(object sender, FormClosingEventArgs e)
    {
      e.Cancel = true;
      Hide();
    }

    private void OnInitFromNotesClick(object sender, EventArgs e)
    {
      InitializeFromNotes(Model);
    }

    private void OnClearClick(object sender, EventArgs e)
    {
      grid.Rows.Clear();
    }

    private void cmdOK_Click(object sender, EventArgs e)
    {
      Status = DialogResult.OK;
    }


  
  }
}
