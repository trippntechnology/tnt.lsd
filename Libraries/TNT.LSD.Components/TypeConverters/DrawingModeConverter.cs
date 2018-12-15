using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace LSDComponents.TypeConverters
{
	public class DrawingModeConverter : ExpandableObjectConverter	
	{
		public override bool CanConvertTo(ITypeDescriptorContext context, System.Type destinationType)
		{
			if (destinationType == typeof(DrawingModes.DrawingMode))
				return true;

			return base.CanConvertTo(context, destinationType);
		}
	}
}
