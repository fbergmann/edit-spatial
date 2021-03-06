﻿using System;
using System.Windows.Forms;
using LibEditSpatial.Model;

namespace LibEditSpatial.Dialogs
{
  public partial class FormSettings : Form
  {
    private EditSpatialSettings _Settings;

    public FormSettings()
    {
      InitializeComponent();
    }

    public EditSpatialSettings Settings
    {
      get { return _Settings; }
      set
      {
        _Settings = value;
        UpdateUI();
      }
    }

    private void UpdateUI()
    {
      if (_Settings == null) return;

      txtCygwin.Text = _Settings.CygwinDir;
      txtParaviewDir.Text = _Settings.ParaViewDir;
      txtDuneDir.Text = _Settings.DuneDir;
      txtDefaultDir.Text = _Settings.DefaultDir;
      chkIgnoreCompartments.Checked = _Settings.IgnoreMultiCompartments;
    }

    private void OnBrowseCygwin(object sender, EventArgs e)
    {
      if (_Settings == null) return;
      _Settings.CygwinDir = Util.GetDir(_Settings.CygwinDir);
      txtCygwin.Text = _Settings.CygwinDir;
    }

    private void OnBrowsePV(object sender, EventArgs e)
    {
      if (_Settings == null) return;
      _Settings.ParaViewDir = Util.GetDir(_Settings.ParaViewDir);
      txtParaviewDir.Text = _Settings.ParaViewDir;
    }

    private void OnBrowseDune(object sender, EventArgs e)
    {
      if (_Settings == null) return;
      _Settings.DuneDir = Util.GetDir(_Settings.DuneDir);
      txtDuneDir.Text = _Settings.DuneDir;
    }

    private void txtDuneDir_TextChanged(object sender, EventArgs e)
    {
      if (_Settings == null) return;
      _Settings.DuneDir = txtDuneDir.Text;
    }

    private void txtParaviewDir_TextChanged(object sender, EventArgs e)
    {
      if (_Settings == null) return;
      _Settings.ParaViewDir = txtParaviewDir.Text;
    }

    private void txtCygwin_TextChanged(object sender, EventArgs e)
    {
      if (_Settings == null) return;
      _Settings.CygwinDir = txtCygwin.Text;
    }

    private void chkIgnoreCompartments_CheckedChanged(object sender, EventArgs e)
    {
      if (_Settings == null) return;
      _Settings.IgnoreMultiCompartments = chkIgnoreCompartments.Checked;
    }

    private void txtDefaultDir_TextChanged(object sender, EventArgs e)
    {
      if (_Settings == null) return;
      _Settings.DefaultDir = txtDefaultDir.Text;
    }

    private void cmdBrowseDefaultDir_Click(object sender, EventArgs e)
    {
      if (_Settings == null) return;
      _Settings.DefaultDir = Util.GetDir(_Settings.DefaultDir);
      txtDefaultDir.Text = _Settings.DefaultDir;
    }
  }
}