using System.ComponentModel;

namespace LSDComponents.TypeConverters
{
	public class DrawingModeListConverter : TypeConverter
	{
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			PaletteProperties paletteProperties = context.Instance as PaletteProperties;
			return new StandardValuesCollection(paletteProperties.m_ModeList.ToArray());
		}
	}
}
