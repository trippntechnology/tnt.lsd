using System.ComponentModel;
using TNT.LSD.Components.DrawingModes;

namespace TNT.LSD.Components.TypeConverters;

public class ObjectTypeListConverter : TypeConverter
{
  public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
  {
    return true;
  }

  public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
  {
    DrawingMode drawingMode = context.Instance as DrawingMode;

    if (drawingMode != null)
    {
      return new StandardValuesCollection(drawingMode.DefaultObjects());
    }
    else
    {
      return new StandardValuesCollection(null);
    }
  }

}
