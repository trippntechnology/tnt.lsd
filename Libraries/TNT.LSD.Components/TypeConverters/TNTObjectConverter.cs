using System.ComponentModel;
using TNT.LSD.Objects;

namespace TNT.LSD.Components.TypeConverters;

public class TNTObjectConverter : ExpandableObjectConverter
{
  public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
  {
    if (destinationType == typeof(TNTObject))
    {
      return true;
    }

    return base.CanConvertTo(context, destinationType);
  }
}
