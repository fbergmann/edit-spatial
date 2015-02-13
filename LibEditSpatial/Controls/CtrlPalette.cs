using System;
using System.Windows.Forms;
using LibEditSpatial.Model;

namespace LibEditSpatial.Controls
{
  public partial class CtrlPalette : UserControl
  {
    private DmpPalette _Palette;

    public CtrlPalette()
    {
      InitializeComponent();
      Current = new PalleteArgs();
      IssuingEvents = true;
      IsPainting = false;
    }

    public PalleteArgs Current { get; set; }
    public bool IssuingEvents { get; set; }
    public bool IsPainting { get; set; }

    public DmpPalette Palette
    {
      get
      {
        if (_Palette == null)
          _Palette = DmpPalette.Default;
        return _Palette;
      }

      set
      {
        _Palette = value;
        pictureBox1.Image = _Palette.ToImage(pictureBox1.Size);
      }
    }

    public event EventHandler<PalleteArgs> PalleteValueChanged;
    public event EventHandler<DmpPalette> PalleteChanged;

    public void UpdateValues(double min, double current, double max)
    {
      if (current > max) current = max;
      if (current < min) current = min;

      IssuingEvents = false;
      txtMin.Text = min.ToString();
      txtCurrent.Text = current.ToString();
      txtMax.Text = max.ToString();
      trackBar1.Minimum = (int) (min*100f);
      trackBar1.Maximum = (int) (max*100f);
      trackBar1.Value = (int) (current*100f);
      Current.Min = min;
      Current.Value = current;
      Current.Max = max;
      IssuingEvents = true;
    }

    private void OnPaletteChanged()
    {
      if (PalleteChanged != null && IssuingEvents)
        PalleteChanged(this, Palette);
    }

    private void OnPaletteValueChanged()
    {
      if (PalleteValueChanged != null && IssuingEvents)
        PalleteValueChanged(this, Current);
    }

    private void OnMinChanged(object sender, EventArgs e)
    {
      double val;
      if (double.TryParse(txtMin.Text, out val))
      {
        Current.Min = val;
        trackBar1.Minimum = (int) (val*100);
        OnPaletteValueChanged();
      }
    }

    private void SetCurrent(double val)
    {
      Current.Value = val;
      trackBar1.Value = (int) Math.Min((Math.Max(val, trackBar1.Minimum)*100), trackBar1.Maximum);
      OnPaletteValueChanged();
    }

    private void OnCurrentChanged(object sender, EventArgs e)
    {
      double val;
      if (double.TryParse(txtCurrent.Text, out val))
      {
        SetCurrent(val);
      }
    }

    private void OnScrollChanged(object sender, EventArgs e)
    {
      txtCurrent.Text = ((double) (trackBar1.Value)/100f).ToString();
    }

    private void OnMaxChanged(object sender, EventArgs e)
    {
      double val;
      if (double.TryParse(txtMax.Text, out val))
      {
        Current.Max = val;
        trackBar1.Maximum = (int) (val*100);
        OnPaletteValueChanged();
      }
    }

    private void OnMouseUp(object sender, MouseEventArgs e)
    {
      IsPainting = false;
    }

    private void OnMouseDown(object sender, MouseEventArgs e)
    {
      IsPainting = true;
    }

    private void OnMouseMove(object sender, MouseEventArgs e)
    {
      if (Palette == null || !IsPainting) return;

      var length = pictureBox1.Width/(float) Palette.Colors.Count;
      float test = (int) (e.X/length);
      if (test >= Palette.Colors.Count)
        test = Palette.Colors.Count - 1;

      var stretchX = Math.Round(Current.MapFromUnit(test / Palette.Colors.Count), 2);
      SetCurrent(stretchX);
      txtCurrent.Text = stretchX.ToString();
      //
    }

    private void OnMouseLeave(object sender, EventArgs e)
    {
      IsPainting = false;
    }

    private void OnMouseClick(object sender, MouseEventArgs e)
    {
      if (Palette == null) return;

      var length = pictureBox1.Width/(float) Palette.Colors.Count;
      float test = (int) (e.X/length);
      if (test >= Palette.Colors.Count)
        test = Palette.Colors.Count - 1;

      var stretchX = Math.Round(Current.MapFromUnit(test / Palette.Colors.Count), 2);
      SetCurrent(stretchX);
      txtCurrent.Text = stretchX.ToString();
    }

    public void ChangePalette(DmpPalette palette)
    {
      Palette = palette;
      OnPaletteChanged();
    }

    public void ChangePalette(string filename)
    {
      var palette = DmpPalette.FromFile(filename);
      ChangePalette(palette);
    }

    private void OnLoadPalette(object sender, EventArgs e)
    {
      using (var dialog = new OpenFileDialog
      {
        Title = "Open Palette",
        Filter = "Palette files|*.txt|All files|*.*",
        AutoUpgradeEnabled = true
      })
      {
        if (dialog.ShowDialog() == DialogResult.OK)
        {
          ChangePalette(dialog.FileName);
        }
      }
    }
  }
}