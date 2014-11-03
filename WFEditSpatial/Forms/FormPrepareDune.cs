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

namespace EditSpatial.Forms
{
  public partial class FormPrepareDune : Form
  {
    public FormPrepareDune()
    {
      InitializeComponent();
    }

    public string TargetDir {
      get { return txtTargetDir.Text; }
      set { txtTargetDir.Text = value; }
    }

    public string ModuleName
    {
      get { return txtName.Text; }
      set { txtName.Text = value; }
    }

    public EditSpatialSettings Settings { get; set; }

    public SpatialModel Model { get; set; }

    private void OnBrowseClicke(object sender, EventArgs e)
    {
      txtTargetDir.Text = Util.GetDir(txtTargetDir.Text);
    }

    private void OnCreateHostClick(object sender, EventArgs e)
    {
      var target = Path.Combine(TargetDir, ModuleName);
      if (Directory.Exists(target))
      {
        var result = MessageBox.Show(
          string.Format(
            "The target directory '{0}' already consists if you continue, do you want to delete the directory before continuing?",
            target),
          "Target dir exists", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
        if (result == System.Windows.Forms.DialogResult.Yes)
          try
          {
            Directory.Delete(target, true);
          }
          catch 
          {
          }
        //if (result == System.Windows.Forms.DialogResult.Cancel)
        else
          return;
      }

      Util.UnzipArchive(
        Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "host.zip"), target);
      

    }

    private void OnCompileModel(object sender, EventArgs e)
    {
      // create build dir
      // run cmake
      // compile

    }

    private void OnExportModel(object sender, EventArgs e)
    {
      // export model into host/src
      // adapt config files
    }
  }
}
