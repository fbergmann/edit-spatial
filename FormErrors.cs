using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EditSpatial.Model;
using libsbmlcs;

namespace EditSpatial
{
  public partial class FormErrors : Form
  {
    SpatialModel Model { get; set; }

    public string Label {
      get
      {
        return label1.Text; 
      }
      set
      {
        label1.Text = value;
      }
    }

    public FormErrors()
    {
      InitializeComponent();
    }

    private void OnValidateClick(object sender, EventArgs e)
    {
      ReValidate();
    }

    private void ReValidate()
    {
      if (Model == null || Model.Document == null) return;
      Model.Document.checkInternalConsistency();
      InitializeFrom(Model);
    }

    public void InitializeFrom(SpatialModel model)
    {
      Model = model;

      var numWarnings = model.Document.getNumErrors(libsbml.LIBSBML_SEV_WARNING);
      var numErrors  = model.Document.getNumErrors(libsbml.LIBSBML_SEV_ERROR);
      var numFatals  = model.Document.getNumErrors(libsbml.LIBSBML_SEV_FATAL);

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

      label1.Text += string.Format("( Warning(s): {0}, Error(s): {1}, Fatal Error(s): {2}", numWarnings, numErrors, numFatals);

      string errors = model == null || model.Document == null
        ? "No document loaded ..."
        : model.Document.getErrorLog().toString().Replace("\n", Environment.NewLine);
      controlText1.Text = errors;
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

    private void FormErrors_FormClosing(object sender, FormClosingEventArgs e)
    {
      e.Cancel = true;
      Hide();
    }
  }
}
