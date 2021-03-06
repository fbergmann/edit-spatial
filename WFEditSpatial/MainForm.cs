﻿using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using EditSpatial.Controls;
using EditSpatial.Forms;
using EditSpatial.Model;
using libsbmlcs;
using LibEditSpatial.Dialogs;
using LibEditSpatial.Model;
using SBW;
using SBW.Utils;

namespace EditSpatial
{
  public partial class MainForm : Form, ISBWAnalyzer
  {
    private const string NODE_COORDINATES = "nodeCoordinateComponents";
    private const string NODE_DOMAINTYPES = "nodeOfDomainTypes";
    private const string NODE_DOMAINS = "nodeOfDomains";
    private const string NODE_ADJACENTDOMAINS = "nodeOfAdjacentDomains";
    private const string NODE_GEOMS = "nodeOfGeometryDefinitions";
    private const string NODE_COMPARTMENTS = "nodeCompartments";
    private const string NODE_SPECIES = "nodeSpecies";
    private const string NODE_PARAMETERS = "nodeParameters";
    private const string NODE_REACTIONS = "nodeReactions";
    private const string NODE_RULES = "nodeRules";
    private const string NODE_INITIAL_ASSIGNMENTS = "nodeInitialAssignments";
    private const string NODE_COMPARTMENT_MAPPINGS = "nodeCompartmentMappings";
    private readonly SBWFavorites favs;
    private readonly SBWMenu menu;
    private bool haveAkira;

    public MainForm()
    {
      InitializeComponent();
      ErrorForm = new FormErrors();
      Annotation = new FormSpatialAnnotation();
      menu = new SBWMenu(mnuSBW, "Edit Spatial", () =>
      {
        if (Model == null) return "";
        if (Model.Document == null) return "";
        return libsbml.writeSBMLToString(Model.Document);
      });
      favs = new SBWFavorites(() =>
      {
        if (Model == null) return "";
        if (Model.Document == null) return "";
        return libsbml.writeSBMLToString(Model.Document);
      }, toolStrip1);

      controlInitialAssignments1.UpdateAction = () =>
      {
        if (Model.Document != null && Model.Document.getModel() != null)
          UpdateTreeWithInitialAssignments(Model.Document.getModel());
      };

      controlAdjacentDomains1.ModelChanged += (e, o) =>
      { if (Model != null) Model.Dirty = true; };
      controlAnalyticGeometry1.ModelChanged += (e, o) =>
      { if (Model != null) Model.Dirty = true; };
      controlCompartment1.ModelChanged += (e, o) =>
      { if (Model != null) Model.Dirty = true; };
      controlCoordinateComponents1.ModelChanged += (e, o) =>
      { if (Model != null) Model.Dirty = true; };
      controlDomains1.ModelChanged += (e, o) =>
      { if (Model != null) Model.Dirty = true; };
      controlDomainTypes1.ModelChanged += (e, o) =>
      { if (Model != null) Model.Dirty = true; };
      controlInitialAssignments1.ModelChanged += (e, o) =>
      { if (Model != null) Model.Dirty = true; };
      controlMapCompartments1.ModelChanged += (e, o) =>
      { if (Model != null) Model.Dirty = true; };
      controlParameters1.ModelChanged += (e, o) =>
      { if (Model != null) Model.Dirty = true; };
      controlRules1.ModelChanged += (e, o) =>
      { if (Model != null) Model.Dirty = true; };
      controlSampleFieldGeometry1.ModelChanged += (e, o) =>
      { if (Model != null) Model.Dirty = true; };
      controlSpecies1.ModelChanged += (e, o) =>
      { if (Model != null) Model.Dirty = true; };

      NewModel();
    }

    public static EditSpatialSettings Settings { get; set; }
    public SpatialModel Model { get; set; }
    public FormErrors ErrorForm { get; set; }
    public FormSpatialAnnotation Annotation { get; set; }

    public void doAnalysis(string model)
    {
      if (InvokeRequired)
      {
        Invoke(new doAnalysisDelegate(doAnalysis), model);
      }
      else
      {
        LoadFromString(model);
        Model.Dirty = true;
      }
    }

    public void InvalidateCoreAll()
    {
      foreach (var control in splitInitial.Panel2.Controls)
      {
        var current = control as BaseSpatialControl;
        if (current == null) continue;
        current.InvalidateSelection();
      }
    }

    public void SaveCoreAll()
    {
      foreach (var control in splitInitial.Panel2.Controls)
      {
        var current = control as BaseSpatialControl;
        if (current == null) continue;
        current.SaveChanges();
        if (current.UpdateAction != null)
          current.UpdateAction();
      }
    }

    public void InvalidateGeometryAll()
    {
      foreach (var control in splitGeometry.Panel2.Controls)
      {
        var current = control as BaseSpatialControl;
        if (current == null) continue;
        current.InvalidateSelection();
      }
    }

    public void SaveGeormetryAll()
    {
      foreach (var control in splitGeometry.Panel2.Controls)
      {
        var current = control as BaseSpatialControl;
        if (current == null) continue;
        current.SaveChanges();
        if (current.UpdateAction != null)
          current.UpdateAction();
      }
    }

    private void OnGeometrySelect(object sender, TreeViewEventArgs e)
    {
      var node = e.Node;
      SaveGeormetryAll();
      InvalidateGeometryAll();
      SelectGeometryNode(node);
    }

    private void SelectGeometryNode(TreeNode node)
    {
      if (node.Name == NODE_COORDINATES)
      {
        controlAnalyticGeometry1.Visible = false;
        controlAdjacentDomains1.Visible = false;
        controlDomainTypes1.Visible = false;
        controlDomains1.Visible = false;
        controlDisplayNode1.Visible = false;
        controlSampleFieldGeometry1.Visible = false;
        controlCoordinateComponents1.Visible = true;
        controlMapCompartments1.Visible = false;
        controlCoordinateComponents1.InitializeFrom(Model.Geometry);
      }
      else if (node.Name == NODE_DOMAINTYPES)
      {
        controlAnalyticGeometry1.Visible = false;
        controlAdjacentDomains1.Visible = false;
        controlDomainTypes1.Visible = true;
        controlDomains1.Visible = false;
        controlDisplayNode1.Visible = false;
        controlSampleFieldGeometry1.Visible = false;
        controlCoordinateComponents1.Visible = false;
        controlMapCompartments1.Visible = false;
        controlDomainTypes1.InitializeFrom(Model.Geometry);
      }
      else if (node.Name == NODE_DOMAINS)
      {
        controlAnalyticGeometry1.Visible = false;
        controlAdjacentDomains1.Visible = false;
        controlDomainTypes1.Visible = false;
        controlDomains1.Visible = true;
        controlDisplayNode1.Visible = false;
        controlSampleFieldGeometry1.Visible = false;
        controlCoordinateComponents1.Visible = false;
        controlMapCompartments1.Visible = false;
        controlDomains1.InitializeFrom(Model.Geometry);
      }
      else if (node.Name == NODE_ADJACENTDOMAINS)
      {
        controlAnalyticGeometry1.Visible = false;
        controlAdjacentDomains1.Visible = true;
        controlDomainTypes1.Visible = false;
        controlDomains1.Visible = false;
        controlDisplayNode1.Visible = false;
        controlSampleFieldGeometry1.Visible = false;
        controlCoordinateComponents1.Visible = false;
        controlMapCompartments1.Visible = false;
        controlAdjacentDomains1.InitializeFrom(Model.Geometry);
      }
      else if (node.Name == NODE_COMPARTMENT_MAPPINGS)
      {
        controlAnalyticGeometry1.Visible = false;
        controlAdjacentDomains1.Visible = false;
        controlDomainTypes1.Visible = false;
        controlDomains1.Visible = false;
        controlDisplayNode1.Visible = false;
        controlSampleFieldGeometry1.Visible = false;
        controlCoordinateComponents1.Visible = false;
        controlMapCompartments1.Visible = true;
        controlMapCompartments1.InitializeFrom(Model.Document.getModel());
      }
      else if (node.Parent != null && node.Parent.Name == NODE_GEOMS &&
               Model.Geometry != null &&
               Model.Geometry.getGeometryDefinition(node.Text) is AnalyticGeometry)
      {
        controlAnalyticGeometry1.Visible = true;
        controlAdjacentDomains1.Visible = false;
        controlDomainTypes1.Visible = false;
        controlDomains1.Visible = false;
        controlDisplayNode1.Visible = false;
        controlSampleFieldGeometry1.Visible = false;
        controlCoordinateComponents1.Visible = false;
        controlMapCompartments1.Visible = false;
        controlAnalyticGeometry1.InitializeFrom(Model.Geometry, node.Text);
      }
      else if (node.Parent != null && node.Parent.Name == NODE_GEOMS &&
               Model.Geometry != null &&
               Model.Geometry.getGeometryDefinition(node.Text) is SampledFieldGeometry)
      {
        controlAnalyticGeometry1.Visible = false;
        controlAdjacentDomains1.Visible = false;
        controlDomainTypes1.Visible = false;
        controlDomains1.Visible = false;
        controlDisplayNode1.Visible = false;
        controlSampleFieldGeometry1.Visible = true;
        controlCoordinateComponents1.Visible = false;
        controlMapCompartments1.Visible = false;
        controlSampleFieldGeometry1.InitializeFrom(Model.Geometry, node.Text);
      }
      else
      {
        controlAnalyticGeometry1.Visible = false;
        controlAdjacentDomains1.Visible = false;
        controlDomainTypes1.Visible = false;
        controlDomains1.Visible = false;
        controlDisplayNode1.Visible = true;
        controlSampleFieldGeometry1.Visible = false;
        controlCoordinateComponents1.Visible = false;
        controlMapCompartments1.Visible = false;
        controlDisplayNode1.DisplayNode(node);
      }
    }

    private void SetFilename(string filename)
    {
      Text = string.Format("Edit Spatial: [ {0} ]", Path.GetFileName(filename));
    }

    public void UpdateUI()
    {
      if (Model == null) return;

      InvalidateGeometryAll();
      splitGeometry.Enabled = Model.IsSpatial;
      cmdAkira.Enabled = Model.IsSpatial;
      cmdPrepareDune.Enabled = Model.IsSpatial;

      FillGeometryFromModel(Model);
      FillCoreTreeFromModel(Model);

      controlDisplayNode1.ResetText();
      controlDisplayNode2.ResetText();
      txtSBML.Text = Model.ToSBML();
      txtJarnac.Text = Model.HaveModel ? Model.ToJarnac() : "";

      SetFilename(Model.FileName);

      treeCore.SelectedNode =
        treeCore.Nodes[NODE_INITIAL_ASSIGNMENTS];

      treeSpatial.SelectedNode =
        treeSpatial.Nodes[NODE_GEOMS].Nodes.Count > 0
          ? treeSpatial.Nodes[NODE_GEOMS].FirstNode
          : treeSpatial.Nodes[NODE_GEOMS];
    }

    private void ClearCoreTree()
    {
      treeCore.Nodes[NODE_COMPARTMENTS].Nodes.Clear();
      treeCore.Nodes[NODE_SPECIES].Nodes.Clear();
      treeCore.Nodes[NODE_PARAMETERS].Nodes.Clear();
      treeCore.Nodes[NODE_REACTIONS].Nodes.Clear();
      treeCore.Nodes[NODE_RULES].Nodes.Clear();
      treeCore.Nodes[NODE_INITIAL_ASSIGNMENTS].Nodes.Clear();
    }

    private void FillCoreTreeFromModel(SpatialModel spatialModel)
    {
      if (spatialModel.Document == null)
      {
        ClearCoreTree();
        return;
      }
      var model = spatialModel.Document.getModel();
      if (model == null)
      {
        ClearCoreTree();
        return;
      }


      FillTreeWithCompartments(model);
      FillTreeWithSpecies(model);
      FillTreeWithParameters(model);
      FillTreeWithReactions(model);
      FillTreeWithRules(model);
      FillTreeWithInitialAssignments(model);
    }

    private void UpdateTreeWithInitialAssignments(libsbmlcs.Model model)
    {
      var root = treeCore.Nodes[NODE_INITIAL_ASSIGNMENTS];
      foreach (TreeNode item in root.Nodes)
      {
        var current = model.getInitialAssignment(item.Text);
        if (current == null) continue;
        item.Tag = current.toSBML();
      }
    }

    private void FillTreeWithInitialAssignments(libsbmlcs.Model model)
    {
      var root = treeCore.Nodes[NODE_INITIAL_ASSIGNMENTS];
      root.Nodes.Clear();
      for (long i = 0; i < model.getNumInitialAssignments(); ++i)
      {
        var current = model.getInitialAssignment(i);
        var node = new TreeNode(current.getSymbol()) {Tag = current.toSBML()};
        root.Nodes.Add(node);
      }
    }

    private void FillTreeWithRules(libsbmlcs.Model model)
    {
      var root = treeCore.Nodes[NODE_RULES];
      root.Nodes.Clear();
      for (long i = 0; i < model.getNumRules(); ++i)
      {
        var current = model.getRule(i);
        var node = new TreeNode(current.getId()) {Tag = current.toSBML()};
        root.Nodes.Add(node);
      }
    }

    private void FillTreeWithReactions(libsbmlcs.Model model)
    {
      var root = treeCore.Nodes[NODE_REACTIONS];
      root.Nodes.Clear();
      for (long i = 0; i < model.getNumReactions(); ++i)
      {
        var current = model.getReaction(i);
        var node = new TreeNode(current.getId()) {Tag = current.toSBML()};
        root.Nodes.Add(node);
      }
    }

    private void FillTreeWithParameters(libsbmlcs.Model model)
    {
      var root = treeCore.Nodes[NODE_PARAMETERS];
      root.Nodes.Clear();
      for (long i = 0; i < model.getNumParameters(); ++i)
      {
        var current = model.getParameter(i);
        var node = new TreeNode(current.getId()) {Tag = current.toSBML()};
        root.Nodes.Add(node);
      }
    }

    private void FillTreeWithSpecies(libsbmlcs.Model model)
    {
      var root = treeCore.Nodes[NODE_SPECIES];
      root.Nodes.Clear();
      for (long i = 0; i < model.getNumSpecies(); ++i)
      {
        var current = model.getSpecies(i);
        var node = new TreeNode(current.getId()) {Tag = current.toSBML()};
        root.Nodes.Add(node);
      }
    }

    private void FillTreeWithCompartments(libsbmlcs.Model model)
    {
      var root = treeCore.Nodes[NODE_COMPARTMENTS];
      root.Nodes.Clear();
      for (long i = 0; i < model.getNumCompartments(); ++i)
      {
        var current = model.getCompartment(i);
        var node = new TreeNode(current.getId()) {Tag = current.toSBML()};
        root.Nodes.Add(node);
      }
    }

    private void FillGeometryFromModel(SpatialModel model)
    {
      var geom = model.Geometry;
      if (geom == null)
      {
        treeSpatial.Nodes[NODE_COORDINATES].Nodes.Clear();
        treeSpatial.Nodes[NODE_DOMAINTYPES].Nodes.Clear();
        treeSpatial.Nodes[NODE_DOMAINS].Nodes.Clear();
        treeSpatial.Nodes[NODE_ADJACENTDOMAINS].Nodes.Clear();
        treeSpatial.Nodes[NODE_GEOMS].Nodes.Clear();
        return;
      }

      var root = treeSpatial.Nodes[NODE_COORDINATES];
      root.Nodes.Clear();
      for (long i = 0; i < geom.getNumCoordinateComponents(); ++i)
      {
        var current = geom.getCoordinateComponent(i);
        var node = new TreeNode(current.getId()) {Tag = current.toSBML()};
        root.Nodes.Add(node);
      }

      root = treeSpatial.Nodes[NODE_DOMAINTYPES];
      root.Nodes.Clear();
      for (long i = 0; i < geom.getNumDomainTypes(); ++i)
      {
        var current = geom.getDomainType(i);
        var node = new TreeNode(current.getId()) {Tag = current.toSBML()};
        root.Nodes.Add(node);
      }


      root = treeSpatial.Nodes[NODE_DOMAINS];
      root.Nodes.Clear();
      for (long i = 0; i < geom.getNumDomains(); ++i)
      {
        var current = geom.getDomain(i);
        var node = new TreeNode(current.getId()) {Tag = current.toSBML()};
        root.Nodes.Add(node);
      }

      root = treeSpatial.Nodes[NODE_ADJACENTDOMAINS];
      root.Nodes.Clear();
      for (long i = 0; i < geom.getNumAdjacentDomains(); ++i)
      {
        var current = geom.getAdjacentDomains(i);
        var node = new TreeNode(current.getId()) {Tag = current.toSBML()};
        root.Nodes.Add(node);
      }

      root = treeSpatial.Nodes[NODE_GEOMS];
      root.Nodes.Clear();
      for (long i = 0; i < geom.getNumGeometryDefinitions(); ++i)
      {
        var current = geom.getGeometryDefinition(i);
        var node = new TreeNode(current.getId()) {Tag = current.toSBML()};

        var analytic = current as AnalyticGeometry;
        if (analytic != null)
        {
          for (long j = 0; j < analytic.getNumAnalyticVolumes(); ++j)
          {
            var vol = analytic.getAnalyticVolume(j);
            var volNode = new TreeNode(vol.getId()) {Tag = vol.toSBML()};
            node.Nodes.Add(volNode);
          }
        }

        var sample = current as SampledFieldGeometry;
        if (sample != null)
        {
          var field = geom.getSampledField(sample.getSampledField());

          if (field != null)
          {
            var fieldNode = new TreeNode(field.getId()) {Tag = field.toSBML()};
            node.Nodes.Add(fieldNode);

            for (long j = 0; j < sample.getNumSampledVolumes(); ++j)
            {
              var vol = sample.getSampledVolume(j);
              var volNode = new TreeNode(vol.getId()) {Tag = vol.toSBML()};
              node.Nodes.Add(volNode);
            }
          }
        }


        root.Nodes.Add(node);
      }
    }

    private void OnNewModel(object sender, EventArgs e)
    {
      if (SaveModelIfDirtyOrCancel()) return;
      NewModel();
    }

    private void NewModel()
    {
      Model = new SpatialModel();
      Model.ModelChanged += (o, args) => UpdateUI();
      UpdateUI();
    }

    private void OnOpenFile(object sender, EventArgs e)
    {
      if (SaveModelIfDirtyOrCancel()) return;

      var dialog = new OpenFileDialog
      {
        Title = "Open Spatial SBML",
        Filter = "SBML files|*.xml;*.sbml|All files|*.*",
        AutoUpgradeEnabled = true
      };

      if (dialog.ShowDialog() != DialogResult.OK)
        return;

      OpenFile(dialog.FileName);
    }

    public void LoadFromString(string content)
    {
      OpenModel(SpatialModel.FromString(content));
    }

    public void LoadFromJarnac(string content)
    {
      OpenModel(SpatialModel.FromJarnac(content));
    }

    public void OpenFile(string fileName)
    {
      OpenModel(SpatialModel.FromFile(fileName));
    }

    private void OpenModel(SpatialModel model)
    {
      try
      {
        Model = model;
        Model.ModelChanged += (o, args) => UpdateUI();

        UpdateUI();

        if (Model != null && Model.Document.getNumErrors(libsbml.LIBSBML_SEV_ERROR) > 0)
        {
          ShowErrors();
        }
        else
        {
          ErrorForm.Hide();
        }

        if (Model != null && !Model.IsSpatial)
        {
          Model.ConvertToL3();
          ConvertToSpatial();
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(
          string.Format("Could not load the given model.{0}{1}", Environment.NewLine, ex.Message),
          "Model could not be loaded",
          MessageBoxButtons.OK,
          MessageBoxIcon.Error
          );
      }
    }

    private void ShowErrors()
    {
      ErrorForm.InitializeFrom(Model);
      ErrorForm.Show();
    }

    private void OnSaveFile(object sender, EventArgs e)
    {
      var dialog = new SaveFileDialog
      {
        Title = "Save Spatial SBML",
        Filter = "SBML files|*.xml;*.sbml|All files|*.*",
        AutoUpgradeEnabled = true
      };

      if (dialog.ShowDialog() != DialogResult.OK)
        return;

      SaveGeormetryAll();
      Model.SaveTo(dialog.FileName);
      UpdateUI();
    }

    private void OnPrint(object sender, EventArgs e)
    {
    }

    private void OnCut(object sender, EventArgs e)
    {
    }

    private void OnCopy(object sender, EventArgs e)
    {
    }

    private void OnPaste(object sender, EventArgs e)
    {
    }

    private void OnAbout(object sender, EventArgs e)
    {
      new FormAbout().ShowDialog(this);
    }

    private void OnExit(object sender, EventArgs e)
    {
      ErrorForm.Dispose();
      ErrorForm = null;
      Application.Exit();
    }

    private void OnCoreSelect(object sender, TreeViewEventArgs e)
    {
      var node = e.Node;
      SaveCoreAll();
      InvalidateCoreAll();
      SelectCoreNode(node);
      controlDisplayNode2.DisplayNode(e.Node);
    }

    private void SelectCoreNode(TreeNode node)
    {
      if (node.Name == NODE_INITIAL_ASSIGNMENTS)
      {
        controlDisplayNode2.Visible = false;
        controlInitialAssignments1.Visible = true;
        controlInitialAssignments1.InitializeFrom(Model.Document);
        controlParameters1.Visible = false;
        controlSpecies1.Visible = false;
        controlCompartment1.Visible = false;
        controlRules1.Visible = false;
      }
      else if (node.Name == NODE_PARAMETERS)
      {
        controlDisplayNode2.Visible = false;
        controlInitialAssignments1.Visible = false;
        controlParameters1.Visible = true;
        controlSpecies1.Visible = false;
        controlCompartment1.Visible = false;
        controlRules1.Visible = false;
        controlParameters1.InitializeFrom(Model.Document);
      }
      else if (node.Name == NODE_SPECIES)
      {
        controlDisplayNode2.Visible = false;
        controlInitialAssignments1.Visible = false;
        controlParameters1.Visible = false;
        controlSpecies1.Visible = true;
        controlCompartment1.Visible = false;
        controlSpecies1.InitializeFrom(Model.Document);
        controlRules1.Visible = false;
      }
      else if (node.Name == NODE_COMPARTMENTS)
      {
        controlDisplayNode2.Visible = false;
        controlInitialAssignments1.Visible = false;
        controlParameters1.Visible = false;
        controlSpecies1.Visible = false;
        controlCompartment1.Visible = true;
        controlCompartment1.InitializeFrom(Model.Document);
        controlRules1.Visible = false;
      }
      else if (node.Name == NODE_RULES)
      {
        controlDisplayNode2.Visible = false;
        controlInitialAssignments1.Visible = false;
        controlParameters1.Visible = false;
        controlSpecies1.Visible = false;
        controlCompartment1.Visible = false;
        controlRules1.Visible = true;
        controlRules1.InitializeFrom(Model.Document);
      }
      else
      {
        controlDisplayNode2.Visible = true;
        controlInitialAssignments1.Visible = false;
        controlParameters1.Visible = false;
        controlSpecies1.Visible = false;
        controlCompartment1.Visible = false;
        controlRules1.Visible = false;
        controlDisplayNode2.DisplayNode(node);
      }
    }

    private void OnLoad(object sender, EventArgs e)
    {
      ReadSettings();

      var remove = false;
      haveAkira = false;
      try
      {
        favs.Update();
        menu.UpdateSBWMenu();

        SBWExporter.SetupImport(
          mnuImport, doAnalysis, s => SetFilename(s));

        foreach (ToolStripItem item in mnuSBW.DropDownItems)
        {
          haveAkira |= haveAkira || item.Text == "Spatial SBML";
        }
      }
      catch
      {
        remove = true;
      }

      if (remove || mnuSBW.DropDownItems.Count == 0)
      {
        favs.RemoveFromToolStrip();
        mnuSBW.Visible = false;
        mnuExportDune.Visible = false;
      }

      cmdAkira.Visible = haveAkira;
    }

    public void ReadSettings()
    {
      Settings = EditSpatialSettings.GetDefault(Size);

      if (Settings.Width < Screen.FromControl(this).WorkingArea.Width &&
          Settings.Height < Screen.FromControl(this).WorkingArea.Height)
        Size = new Size(Settings.Width, Settings.Height);

      chkMultipleCompartments.Checked = Settings.IgnoreMultiCompartments;
    }

    private void OnFormClosed(object sender, FormClosedEventArgs e)
    {
      WriteSettings();
    }

    private void WriteSettings()
    {
      Settings.Save();
    }

    private void OnShowWarnings(object sender, EventArgs e)
    {
      ShowErrors();
    }

    private void OnApplyJarnacClick(object sender, EventArgs e)
    {
      LoadFromJarnac(txtJarnac.Text);
    }

    private void OnExportMorpheusClick(object sender, EventArgs e)
    {
      if (Model == null || Model.Document == null)
        return;
      Model.Document.clearValidators();
      Model.Document.addValidator(SpatialModel.CustomSpatialValidator);
      Model.Document.checkConsistency();
      if (Model.Document.getNumErrors(libsbml.LIBSBML_SEV_ERROR) > 0)
      {
        MessageBox.Show(
          "Unfortunately, the SBML model contains a number of errors. These need to be corrected, before the model can be exported to Morpheus.",
          "Invalid Model", MessageBoxButtons.OK, MessageBoxIcon.Error);
        ShowErrors();
        return;
      }

      var dialog = new SaveFileDialog
      {
        Title = "Save Morpheus Configuration File",
        Filter = "Configuration file|*.xml|All files|*.*",
        AutoUpgradeEnabled = true
      };

      if (dialog.ShowDialog() != DialogResult.OK)
        return;

      ExportMorpheusFile(dialog.FileName);
    }

    public void ExportMorpheusFile(string fileName)
    {
      File.WriteAllText(fileName, Model.ToMorpheus(fileName));
    }

    private void ConvertToSpatial()
    {
      var dialog = new FormInitSpatial { SpatialModel = Model };
      if (dialog.ShowDialog(this) == DialogResult.OK)
      {
        var selection = dialog.CreateModel;

        if (!Model.ConvertToSpatial(selection))
        {
          ShowErrors();
        }
        else
        {
          UpdateUI();
        }
      }
    }

    private void OnShowSpatialWizard(object sender, EventArgs e)
    {
      ConvertToSpatial();
    }

    private void OnExportDuneSBMLClick(object sender, EventArgs e)
    {
      if (Model == null || Model.Document == null)
        return;
      Model.Document.clearValidators();
      Model.Document.addValidator(SpatialModel.CustomSpatialValidator);
      Model.Document.checkConsistency();
      if (Model.Document.getNumErrors(libsbml.LIBSBML_SEV_ERROR) > 0)
      {
        MessageBox.Show(
          "Unfortunately, the SBML model contains a number of errors. These need to be corrected, before the model can be exported to Dune.",
          "Invalid Model", MessageBoxButtons.OK, MessageBoxIcon.Error);
        ShowErrors();
        return;
      }

      var dialog = new SaveFileDialog
      {
        Title = "Export DUNE SBML Model",
        Filter = "SBML files|*.xml;*.sbml|All files|*.*",
        AutoUpgradeEnabled = true
      };

      if (dialog.ShowDialog() != DialogResult.OK)
        return;

      Model.ExportDuneSBML(dialog.FileName);
    }

    private void OnExportDuneClick(object sender, EventArgs e)
    {
      if (Model == null || Model.Document == null)
        return;
      Model.Document.clearValidators();
      Model.Document.addValidator(SpatialModel.CustomSpatialValidator);
      Model.Document.checkConsistency();
      if (Model.Document.getNumErrors(libsbml.LIBSBML_SEV_ERROR) > 0)
      {
        MessageBox.Show(
          "Unfortunately, the SBML model contains a number of errors. These need to be corrected, before the model can be exported to Dune.",
          "Invalid Model", MessageBoxButtons.OK, MessageBoxIcon.Error);
        ShowErrors();
        return;
      }

      var dialog = new SaveFileDialog
      {
        Title = "Export Model to DUNE",
        Filter = "Main Implementation file|*.cc|All files|*.*",
        AutoUpgradeEnabled = true
      };

      if (dialog.ShowDialog() != DialogResult.OK)
        return;

      Model.ExportToDune(dialog.FileName);
    }

    private void OnAddSampledFieldGeometry(object sender, EventArgs e)
    {
      if (Model.Geometry == null) return;

      var geom = Model.Geometry.createSampledFieldGeometry();
      geom.setId(String.Format("sampledFieldGeometry{0}", Model.Geometry.GetNumSampledFieldGeometries()));

      Model.Dirty = true;
      UpdateUI();
    }

    private void OnAddAnalyticGeometry(object sender, EventArgs e)
    {
      if (Model.Geometry == null) return;

      var geom = Model.Geometry.createAnalyticGeometry();
      geom.setId(String.Format("analyticGeometry{0}", Model.Geometry.GetNumAnalyticGeometries()));
      
      Model.Dirty = true;
      UpdateUI();
    }

    private void OnSpatialItemDeleteClick(object sender, EventArgs e)
    {
      var selected = treeSpatial.SelectedNode;
      if (selected == null || selected.Level == 0) return;

      var selectedId = selected.Text;
      var parent = selected.Parent;
      if (Model.Geometry == null) return;

      Model.Dirty = true;

      if (selected.Level == 1)
        switch (parent.Name)
        {
          case NODE_DOMAINS:
          {
            Model.Geometry.removeDomain(selectedId);
            UpdateUI();
            break;
          }
          case NODE_DOMAINTYPES:
          {
            Model.Geometry.removeDomainType(selectedId);
            UpdateUI();
            break;
          }
          case NODE_GEOMS:
          {
            Model.Geometry.removeGeometryDefinition(selectedId);
            UpdateUI();
            break;
          }
          case NODE_ADJACENTDOMAINS:
          {
            Model.Geometry.removeAdjacentDomains(selectedId);
            UpdateUI();
            break;
          }
          case NODE_COORDINATES:
          {
            Model.Geometry.removeCoordinateComponent(selectedId);
            UpdateUI();
            break;
          }
        }

      if (selected.Level == 2)
      {
        switch (parent.Parent.Name)
        {
          case NODE_GEOMS:
          {
            var geom = Model.Geometry.getGeometryDefinition(parent.Text);
            if (geom is AnalyticGeometry)
            {
              var ageom = geom as AnalyticGeometry;
              ageom.removeAnalyticVolume(selectedId);
              UpdateUI();
            }
            else if (geom is SampledFieldGeometry)
            {
              var sgeom = geom as SampledFieldGeometry;
              sgeom.removeSampledVolume(selectedId);
              UpdateUI();
            }
            break;
          }
        }
      }

      treeSpatial.SelectedNode = parent;
    }

    private void OnCoreItemDeleteClick(object sender, EventArgs e)
    {
      var selected = treeCore.SelectedNode;
      if (selected == null || selected.Level == 0) return;

      var selectedId = selected.Text;
      var parent = selected.Parent;
      switch (parent.Name)
      {
        case NODE_PARAMETERS:
        {
          Model.Document.getModel().removeParameter(selectedId);
          UpdateUI();
          break;
        }
        case NODE_COMPARTMENTS:
        {
          Model.Document.getModel().removeCompartment(selectedId);
          UpdateUI();
          break;
        }
        case NODE_SPECIES:
        {
          Model.Document.getModel().removeSpecies(selectedId);
          UpdateUI();
          break;
        }
        case NODE_REACTIONS:
        {
          Model.Document.getModel().removeSpecies(selectedId);
          UpdateUI();
          break;
        }
        case NODE_RULES:
        {
          Model.Document.getModel().removeRule(selectedId);
          UpdateUI();
          break;
        }
        case NODE_INITIAL_ASSIGNMENTS:
        {
          Model.Document.getModel().removeInitialAssignment(selectedId);
          UpdateUI();
          break;
        }
      }
      treeCore.SelectedNode = parent;
    }

    private void OnMoveARtoIAClick(object sender, EventArgs e)
    {
      if (Model == null) return;
      Model.MoveAllRulesToAssignments();

      Model.Dirty = true;
      UpdateUI();
    }

    private void OnEditSpatialAnnotationClick(object sender, EventArgs e)
    {
      if (Model == null || Model.Document == null) return;

      Annotation.InitFrom(Model);

      Annotation.ShowDialog(this);
      if (Annotation.Status == DialogResult.OK)
      {
        Annotation.SaveToModel(Model.Document.getModel());
      }
    }

    private void OnSimulateWithAkiraClick(object sender, EventArgs e)
    {
      if (!haveAkira) return;
      if (Model == null || Model.Document == null) return;
      if (!Model.IsSpatial) return;
      try
      {
        HighLevel.Send("Spatial SBML", "Spatial SBML", "void doAnalysis(string)", Model.ToSBML());
      }
      catch
      {
      }
    }

    private void OnCheckMultipleCompartmentsClick(object sender, EventArgs e)
    {
      Settings.IgnoreMultiCompartments = chkMultipleCompartments.Checked;
      Settings.Save();
    }

    private void OnEditPreferencesClick(object sender, EventArgs e)
    {
      using (var dlg = new FormSettings {Settings = Settings})
      {
        if (dlg.ShowDialog(this) == DialogResult.OK)
        {
          dlg.Settings.Save();
          ReadSettings();
        }
      }
    }

    private void OnPrepareDuneClick(object sender, EventArgs e)
    {
      if (Model == null || Model.Document == null) return;
      if (!Model.IsSpatial) return;

      using (var dlg = new FormPrepareDune
      {
        Settings = Settings,
        Model = Model,
        TargetDir = Settings.DefaultDir,
        ModuleName = Path.GetFileNameWithoutExtension(Model.FileName)
      })
      {
        dlg.ShowDialog(this);
      }
    }

    #region Drag / Drop

    private void MainForm_DragDrop(object sender, DragEventArgs e)
    {
      try
      {
        var sFilenames = (string[]) e.Data.GetData(DataFormats.FileDrop);
        var oInfo = new FileInfo(sFilenames[0]);
        if (oInfo.Extension.ToLower() == ".xml" || oInfo.Extension.ToLower() == ".sbml")
        {
          OpenFile(sFilenames[0]);
        }
      }
      catch (Exception)
      {
      }
    }

    private void MainForm_DragEnter(object sender, DragEventArgs e)
    {
      if (e.Data.GetDataPresent(DataFormats.FileDrop))
      {
        var sFilenames = (string[]) e.Data.GetData(DataFormats.FileDrop);
        var oInfo = new FileInfo(sFilenames[0]);
        if (oInfo.Extension.ToLower() == ".xml" || oInfo.Extension.ToLower() == ".sbml")
        {
          e.Effect = DragDropEffects.Copy;
          return;
        }
      }
      e.Effect = DragDropEffects.None;
    }

    #endregion


    /// <summary>
    /// This function asks a user whether the model should be saved
    /// </summary>
    /// <returns>true, if model is dirty and the user pressed cancel, false otherwise</returns>
    private bool SaveModelIfDirtyOrCancel()
    {
      if (Model == null || !Model.Dirty) return false;

      DialogResult result =
        MessageBox.Show(this,
          "There are unsaved changes in the model. Would you like to save them?",
          "Save changes?",
          MessageBoxButtons.YesNoCancel,
          MessageBoxIcon.Question,
          MessageBoxDefaultButton.Button3);

      if (result == DialogResult.Cancel)
      {
        return true;
      }

      if (result == DialogResult.Yes)
      {
        OnSaveFile(this, EventArgs.Empty);
      }

      return false;
    }

    private void OnFormClosing(object sender, FormClosingEventArgs e)
    {
      e.Cancel = SaveModelIfDirtyOrCancel();
    }
  }
}
