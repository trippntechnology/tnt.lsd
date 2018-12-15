using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace LSDComponents.TypeConverters
{
	public class ObjectTypeListConverter: TypeConverter
	{
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			DrawingModes.DrawingMode drawingMode = context.Instance as DrawingModes.DrawingMode;

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
}
