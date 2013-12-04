using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EditSpatial.Model;
using libsbmlcs;
using SBW.Utils;

namespace EditSpatial
{
  public partial class MainForm : Form
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
    
    public SpatialModel Model { get; set; }
    public FormErrors ErrorForm { get; set; }

    private SBWMenu menu;
    private SBWFavorites favs;

    public MainForm()
    {
      InitializeComponent();
      ErrorForm = new FormErrors();
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
      NewModel();
    }

    public void InvalidateGeometryAll()
    {
      foreach (var control in splitGeometry.Panel2.Controls)
      {
        var current = control as Controls.BaseSpatialControl;
        if (current == null) continue;
        current.InvalidateSelection();
      }
    }

    public void SaveGeormetryAll()
    {
      foreach (var control in splitGeometry.Panel2.Controls)
      {
        var current = control as Controls.BaseSpatialControl;
        if (current == null) continue;
        current.SaveChanges();
      }
    }

    private void OnGeometrySelect(object sender, TreeViewEventArgs e)
    {
      TreeNode node = e.Node;
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
        controlAdjacentDomains1.InitializeFrom(Model.Geometry);
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
        controlDisplayNode1.DisplayNode(node);
      }
    }

    public void UpdateUI()
    {
      if (Model == null) return;

      InvalidateGeometryAll();
      splitGeometry.Enabled = Model.IsSpatial;
      
      FillGeometryFromModel(Model);
      FillCoreTreeFromModel(Model);

      controlDisplayNode1.ResetText();
      controlDisplayNode2.ResetText();
      txtSBML.Text = Model.ToSBML();
      txtJarnac.Text = Model.ToJarnac();

      Text = "Edit Spatial: [ " + Path.GetFileName(Model.FileName) + " ]";

      treeView1.SelectedNode = 
        treeView1.Nodes[NODE_GEOMS].Nodes.Count > 0 ? 
        treeView1.Nodes[NODE_GEOMS].FirstNode : 
        treeView1.Nodes[NODE_GEOMS];

    }

    private void FillCoreTreeFromModel(SpatialModel spatialModel)
    {
      if (spatialModel.Document == null) return;
      var model = spatialModel.Document.getModel();
      if (model == null) return;

      var root = treeView2.Nodes[NODE_COMPARTMENTS];
      root.Nodes.Clear();
      for (long i = 0; i < model.getNumCompartments(); ++i)
      {
        var current = model.getCompartment(i);
        var node = new TreeNode(current.getId()) {Tag = current.toSBML()};
        root.Nodes.Add(node);
      }

      root = treeView2.Nodes[NODE_SPECIES];
      root.Nodes.Clear();
      for (long i = 0; i < model.getNumSpecies(); ++i)
      {
        var current = model.getSpecies(i);
        var node = new TreeNode(current.getId()) {Tag = current.toSBML()};
        root.Nodes.Add(node);
      }
      root = treeView2.Nodes[NODE_PARAMETERS];
      root.Nodes.Clear();
      for (long i = 0; i < model.getNumParameters(); ++i)
      {
        var current = model.getParameter(i);
        var node = new TreeNode(current.getId()) {Tag = current.toSBML()};
        root.Nodes.Add(node);
      }
      root = treeView2.Nodes[NODE_REACTIONS];
      root.Nodes.Clear();
      for (long i = 0; i < model.getNumReactions(); ++i)
      {
        var current = model.getReaction(i);
        var node = new TreeNode(current.getId()) {Tag = current.toSBML()};
        root.Nodes.Add(node);
      }
    }

    private void FillGeometryFromModel(SpatialModel model)
    {
      var geom = model.Geometry;
      if (geom == null) return;

      var root = treeView1.Nodes[NODE_COORDINATES];
      root.Nodes.Clear();
      for (long i = 0; i < geom.getNumCoordinateComponents(); ++i)
      {
        var current = geom.getCoordinateComponent(i);
        var node = new TreeNode(current.getSpatialId()) {Tag = current.toSBML()};
        root.Nodes.Add( node );
      }

      root = treeView1.Nodes[NODE_DOMAINTYPES];
      root.Nodes.Clear();
      for (long i = 0; i < geom.getNumDomainTypes(); ++i)
      {
        var current = geom.getDomainType(i);
        var node = new TreeNode(current.getSpatialId()) {Tag = current.toSBML()};
        root.Nodes.Add(node);
      }


      root = treeView1.Nodes[NODE_DOMAINS];
      root.Nodes.Clear();
      for (long i = 0; i < geom.getNumDomains(); ++i)
      {
        var current = geom.getDomain(i);
        var node = new TreeNode(current.getSpatialId()) {Tag = current.toSBML()};
        root.Nodes.Add(node);
      }

      root = treeView1.Nodes[NODE_ADJACENTDOMAINS];
      root.Nodes.Clear();
      for (long i = 0; i < geom.getNumAdjacentDomains(); ++i)
      {
        var current = geom.getAdjacentDomains(i);
        var node = new TreeNode(current.getSpatialId()) {Tag = current.toSBML()};
        root.Nodes.Add(node);
      }

      root = treeView1.Nodes[NODE_GEOMS];
      root.Nodes.Clear();
      for (long i = 0; i < geom.getNumGeometryDefinitions(); ++i)
      {
        var current = geom.getGeometryDefinition(i);
        var node = new TreeNode(current.getSpatialId()) {Tag = current.toSBML()};

        var analytic = current as AnalyticGeometry;
        if (analytic != null)
        {
          for (long j = 0; j < analytic.getNumAnalyticVolumes(); ++j)
          {
            var vol = analytic.getAnalyticVolume(j);
            var volNode = new TreeNode(vol.getSpatialId()) {Tag = vol.toSBML()};
            node.Nodes.Add(volNode);

          }
          
        }

        var sample = current as SampledFieldGeometry;
        if (sample != null)
        {
          var field = sample.getSampledField();
          var fieldNode = new TreeNode(field.getSpatialId()) {Tag = field.toSBML()};
          node.Nodes.Add(fieldNode);

          for (long j = 0; j < sample.getNumSampledVolumes(); ++j)
          {
            var vol = sample.getSampledVolume(j);
            var volNode = new TreeNode(vol.getSpatialId()) {Tag = vol.toSBML()};
            node.Nodes.Add(volNode);
          }
        }


        root.Nodes.Add(node);
      }
    }

    private void OnNewModel(object sender, EventArgs e)
    {
      NewModel();
    }

    private void NewModel()
    {
      Model = new SpatialModel();
      UpdateUI();
    }

    private void OnOpenFile(object sender, EventArgs e)
    {
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

    public void OpenFile(string fileName)
    {
      Model = SpatialModel.FromFile(fileName);      
      UpdateUI();

      if (Model != null && Model.Document.getNumErrors(libsbml.LIBSBML_SEV_ERROR) > 0)
      {
        ShowErrors();
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
      new FormAbout().ShowDialog();
    }

    private void OnExit(object sender, EventArgs e)
    {
      Application.Exit();
    }

    private void OnCoreSelect(object sender, TreeViewEventArgs e)
    {
      controlDisplayNode2.DisplayNode(e.Node);
    }

    private void OnLoad(object sender, EventArgs e)
    {
      ReadSettings();

      bool remove = false;
      try
      {
        favs.Update();
        menu.UpdateSBWMenu();
      }
      catch
      {
        remove = true;
      }

      if (remove || mnuSBW.DropDownItems.Count == 0)
      {
        favs.RemoveFromToolStrip();
        mnuSBW.Visible = false;
      }

    }

    public void ReadSettings()
    {
      var reg = Application.UserAppDataRegistry;
      if (reg == null) return;
      
      var width = (int)reg.GetValue("width", Size.Width);
      var height = (int)reg.GetValue("height", Size.Height);

      if (width < Screen.FromControl(this).WorkingArea.Width && height < Screen.FromControl(this).WorkingArea.Height)
        Size = new Size(width, height);


    }

    private void OnFormClosed(object sender, FormClosedEventArgs e)
    {
      WriteSettings();
    }

    private void WriteSettings()
    {
      var reg = Application.UserAppDataRegistry;
      if (reg == null) return;
      reg.SetValue("width", Size.Width);
      reg.SetValue("height", Size.Height);
    }

    private void OnShowWarnings(object sender, EventArgs e)
    {
      ShowErrors();
    }
  }
}
