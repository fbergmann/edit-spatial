﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Forms;
using EditSpatial.Model;
using libsbmlcs;

namespace EditSpatial.Forms
{
  public partial class FormSpatialAnnotation : Form
  {
    public const string SPATIAL_ANNOTATION_URL = "http://fbergmann.github.io/spatial-sbml/settings";
    private readonly Random rand;

    public FormSpatialAnnotation()
    {
      InitializeComponent();
      rand = new Random();
    }

    public libsbmlcs.Model Model { get; set; }
    public DialogResult Status { get; set; }

    private void InitializeIdCombo(libsbmlcs.Model model)
    {
      colId.Items.Clear();
      for (var i = 0; i < model.getNumSpecies(); i++)
      {
        colId.Items.Add(model.getSpecies(i).getId());
      }
    }

    private XMLNode getAnnotationNode(libsbmlcs.Model model, string ns)
    {
      if (model == null || !model.isSetAnnotation()) return null;

      var parent = model.getAnnotation();
      var again = true;
      while (again)
      {
        again = false;
        for (var i = 0; i < parent.getNumChildren(); ++i)
        {
          var current = parent.getChild(i);
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
        max = scale*species.getInitialConcentration();
      else if (species.isSetInitialAmount() && species.getInitialAmount() > 0)
        max = scale*species.getInitialAmount();


      grid.Rows.Add(species.getId(), palette, max.ToString(CultureInfo.InvariantCulture));
    }

    private void InitializeFromNotes(libsbmlcs.Model model)
    {
      grid.Rows.Clear();

      if (model == null) return;
      Model = model;
      for (var i = 0; i < model.getNumSpecies(); ++i)
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

      var update = node.getChild("update");
      if (update.getName() == "update")
      {
        txtStep.Text = update.getAttrValue("step");
        txtUpdate.Text = update.getAttrValue("freq");
      }

      grid.Rows.Clear();

      var items = node.getChild("items");
      if (items.getName() == "items")
      {
        for (var i = 0; i < items.getNumChildren(); ++i)
        {
          var item = items.getChild(i);

          var id = item.getAttrValue("sbmlId");
          var palette = item.getAttrValue("palette");
          var max = item.getAttrValue("max");

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
      var update = new XMLNode(new XMLTriple("update", SPATIAL_ANNOTATION_URL, ""), new XMLAttributes());
      update.addAttr("step", txtStep.Text);
      update.addAttr("freq", txtUpdate.Text);

      node.addChild(update);

      // save assignments
      if (grid.Rows.Count > 0)
      {
        var items = new XMLNode(new XMLTriple("items", SPATIAL_ANNOTATION_URL, ""), new XMLAttributes());

        for (var i = 0; i < grid.Rows.Count; ++i)
        {
          var current = grid.Rows[i];
          var item = new XMLNode(new XMLTriple("item", SPATIAL_ANNOTATION_URL, ""), new XMLAttributes());
          var id = current.Cells[0].Value as string;
          if (string.IsNullOrWhiteSpace(id)) continue;
          item.addAttr("sbmlId", id);
          item.addAttr("palette", current.Cells[1].Value as string);
          item.addAttr("max", current.Cells[2].Value as string);
          items.addChild(item);
        }
        node.addChild(items);
      }

      if (model.isSetAnnotation())
      {
        var annot = model.getAnnotation();
        var num = (int) annot.getNumChildren();
        for (var i = num - 1; i >= 0; i--)
        {
          var child = annot.getChild(i);
          if (child.getName() == "spatialInfo" &&
              (child.getNamespaceURI() == SPATIAL_ANNOTATION_URL || child.getNamespaceURI() == ""))
            annot.removeChild(i);
        }
      }
      model.removeTopLevelAnnotationElement("spatialInfo", SPATIAL_ANNOTATION_URL, false);
      model.appendAnnotation(node);
    }

    public void InitFrom(SpatialModel spatialModel)
    {
      Status = DialogResult.Cancel;
      colId.Items.Clear();
      grid.Rows.Clear();

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

    private void cmdCombine_Click(object sender, EventArgs e)
    {
      var selected = grid.SelectedRows;
      if (selected == null || selected.Count < 2) return;

      // get common properties
      var palette = selected[0].Cells[1].Value as string;
      var max = selected[0].Cells[2].Value as string;

      // remove rows 
      var ids = new List<string>();
      for (var i = selected.Count - 1; i >= 0; i--)
      {
        ids.Insert(0, selected[i].Cells[0].Value as string);
        grid.Rows.Remove(selected[i]);
      }

      // add species with assignment rule 
      var species = Model.createSpecies();
      species.initDefaults();

      var name = "combined_" + ids[0];
      var formula = ids[0];
      colId.Items.Remove(ids[0]);
      for (var i = 1; i < ids.Count; i++)
      {
        name = name + "_" + ids[i];
        formula = formula + " + " + ids[i];
        colId.Items.Remove(ids[i]);
      }

      species.setId(name);
      species.setCompartment(Model.getSpecies(ids[0]).getCompartment());
      var plug = species.getPlugin("spatial") as SpatialSpeciesPlugin;
      if (plug != null)
        plug.setIsSpatial(true);

      var assignment = Model.createAssignmentRule();
      assignment.setVariable(species.getId());
      assignment.setFormula(formula);

      colId.Items.Add(species.getId());

      // add new row
      var index = grid.Rows.Add(species.getId(), palette, max);

      // select row
      grid.Rows[index].Selected = true;
    }

    private string getRandomPalette()
    {
      switch (rand.Next(3))
      {
        default:
          return "black-green";
        case 2:
          return "black-red";
        case 1:
          return "black-blue";
      }
    }

    private void OnAddAllClick(object sender, EventArgs e)
    {
      OnClearClick(sender, e);

      for (var i = 0; i < Model.getNumSpecies(); i++)
      {
        var species = Model.getSpecies(i);
        double max = 6;
        double scale = 10;
        if (species.isSetInitialConcentration() && species.getInitialConcentration() > 0)
          max = scale*species.getInitialConcentration();
        else if (species.isSetInitialAmount() && species.getInitialAmount() > 0)
          max = scale*species.getInitialAmount();

        grid.Rows.Add(species.getId(), getRandomPalette(), max);
      }
    }
  }
}