using System;
using System.ComponentModel;
using TNT.LSD.Objects;

namespace LSDComponents.TypeConverters
{
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
}
