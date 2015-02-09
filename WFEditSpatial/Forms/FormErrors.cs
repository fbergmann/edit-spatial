using System;
using System.Windows.Forms;
using EditSpatial.Model;
using libsbmlcs;

namespace EditSpatial.Forms
{
  public partial class FormErrors : Form
  {
    public FormErrors()
    {
      InitializeComponent();
    }

    private SpatialModel Model { get; set; }

    public string Label
    {
      get { return label1.Text; }
      set { label1.Text = value; }
    }

    private void OnValidateClick(object sender, EventArgs e)
    {
      ReValidate();
    }

    private void ReValidate()
    {
      if (Model == null || Model.Document == null) return;
      Model.Document.clearValidators();
      Model.Document.addValidator(SpatialModel.CustomSpatialValidator);
      Model.Document.validateSBML();
      InitializeFrom(Model);
    }

    public void InitializeFrom(SpatialModel model)
    {
      Model = model;

      var haveNoDocument = model == null || model.Document == null;

      var errors = haveNoDocument
        ? "No document loaded ..."
        : model.Document.getErrorLog().toString().Replace("\n", Environment.NewLine);
      controlText1.Text = errors;

      if (haveNoDocument)
      {
        label1.Text = "";
        return;
      }

      var numWarnings = model.Document.getNumErrors(libsbml.LIBSBML_SEV_WARNING);
      var numErrors = model.Document.getNumErrors(libsbml.LIBSBML_SEV_ERROR);
      var numFatals = model.Document.getNumErrors(libsbml.LIBSBML_SEV_FATAL);

      if (numErrors + numFatals > 0)
      {
        label1.Text = "The document is invalid SBML.";
      }
      else if (numWarnings > 0)
      {
        label1.Text = "The document is valid SBML, but there were warnings.";
      }
      else
      {
        label1.Text = "The document is valid SBML.";
      }

      label1.Text += string.Format(" Warning(s): {0}, Error(s): {1}, Fatal Error(s): {2}", numWarnings, numErrors,
        numFatals);
    }

    private void OnCloseClick(object sender, EventArgs e)
    {
      Hide();
    }

    private void OnFixCommonErrors(object sender, EventArgs e)
    {
      if (Model == null) return;
      Model.FixCommonErrors();
      ReValidate();
    }

    private void OnFormClosing(object sender, FormClosingEventArgs e)
    {
      e.Cancel = true;
      Hide();
    }
  }
}