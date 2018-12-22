using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Reflection;
using TNT.Utilities;
using LSDDrawingModes = LSDComponents.DrawingModes;

namespace LSDComponents
{
	public class PaletteProperties
	{
		/// <summary>
		/// This is used by DrawingModeListConverter
		/// </summary>
		internal List<string> m_ModeList = null;
		private string m_DrawingModeType = string.Empty;
		private string m_ImageObjectType = string.Empty;

		public PaletteProperties()
		{
			m_ModeList = Utilities.GetNameSpaceClasses("LSDComponents.DrawingModes", "TNT.LSD.Components.dll");
			DrawingMode = new DrawingModes.NullMode();
		}

		public PaletteProperties(PaletteProperties obj)
		{
			m_ModeList = obj.m_ModeList;
			m_DrawingModeType = obj.m_DrawingModeType;
			m_ImageObjectType = obj.m_ImageObjectType;
			DrawingMode = obj.DrawingMode.Clone();
			Description = obj.Description;
		}

		[DisplayName("Drawing Mode Type")]
		[Description("Type of drawing mode to use.")]
		[TypeConverter(typeof(TypeConverters.DrawingModeListConverter))]
		public string DrawingModeType
		{
			get { return m_DrawingModeType; }
			set
			{
				Assembly asm = Assembly.LoadFrom("TNT.LSD.Components.dll");
				m_DrawingModeType = value;

				if (!string.IsNullOrEmpty(m_DrawingModeType))
				{
					DrawingMode = (LSDDrawingModes.DrawingMode)asm.CreateInstance(m_DrawingModeType);
				}
			}
		}

		[DisplayName("Drawing Mode")]
		[Description("Drawing Mode used when node is selected")]
		[TypeConverterAttribute(typeof(TypeConverters.DrawingModeConverter))]
		public LSDDrawingModes.DrawingMode DrawingMode { get; set; }

		[Description("Description of the node.")]
		[Editor(typeof(System.ComponentModel.Design.MultilineStringEditor), typeof(UITypeEditor))]
		public string Description { get; set; }
	}
}
