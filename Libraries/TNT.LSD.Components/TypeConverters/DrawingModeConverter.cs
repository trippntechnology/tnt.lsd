using System.ComponentModel;
using TNT.LSD.Components.DrawingModes;

namespace TNT.LSD.Components.TypeConverters;

public class DrawingModeConverter : ExpandableObjectConverter
{
  public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
  {
    if (destinationType == typeof(DrawingMode))
      return true;

    return base.CanConvertTo(context, destinationType);
  }
}
