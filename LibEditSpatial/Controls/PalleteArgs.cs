namespace LibEditSpatial.Controls
{
  public class PalleteArgs
  {
    public double Min { get; set; }

    public double Max { get; set; }
    
    public double Value { get; set; }

    public double Range
    {
      get { return Max - Min; }
    }

    public double MapToUnit(double value)
    {
      if (value < Min) return 0;
      if (value > Max) return 1;

      if (Range == 0) return 0;

      return (value - Min) / Range;
    }

    public double MapFromUnit(double value)
    {
      return value * Range + Min;
    }

  }
}
