using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using TNT.LSD.Inventory;

namespace TNT.LSD.Settings
{
	public delegate void OnDrawLayersDelegate(int layer);
	public delegate void OnSetLegendVisibilityDelegate(bool showLegend);
	public delegate void OnSetHeightInFeetDelegate(int heightInFeet);
	public delegate void OnSetWidthInFeetDelegate(int heightInFeet);

	public class LSDSettings
	{
		protected const int MAX_WIDTH_HEIGHT = 800;

		protected string m_Version;
		protected string m_TelephoneNumber;
		protected int m_HeightInFeet;
		protected int m_WidthInFeet;
		protected bool m_DrawUnits;
		protected bool m_DrawGrid;
		protected Color m_GridColor;
		protected int m_GridColorAlphaValue;

		[XmlIgnore()]
		virtual public OnDrawLayersDelegate OnDrawLayers { private get; set; }
		[XmlIgnore()]
		virtual public OnSetHeightInFeetDelegate OnSetHeightInFeet { private get; set; }
		[XmlIgnore()]
		virtual public OnSetWidthInFeetDelegate OnSetWidthInFeet { private get; set; }

		#region Layout

		[Category("Layout")]
		[DisplayName("Draw Units")]
		[Description("Indicates whether the grid units are drawn.")]
		virtual public bool DrawUnits
		{
			get { return m_DrawUnits; }
			set
			{
				m_DrawUnits = value;
				DrawLayers(0);
			}
		}

		[Category("Layout")]
		[DisplayName("Draw Grid")]
		[Description("Indicates whether the grid lines should be drawn.")]
		[ReadOnly(true)]
		virtual public bool DrawGrid
		{
			get { return m_DrawGrid; }
			set
			{
				m_DrawGrid = value;
				DrawLayers(0);
			}
		}

		[Category("Layout")]
		[DisplayName("Grid Line Color")]
		[Description("Indicates the color to use for the grid lines.")]
		[XmlIgnore()]
		virtual public Color GridColor
		{
			get { return m_GridColor; }
			set
			{
				m_GridColor = value;
				DrawLayers(0);
			}
		}

		[Browsable(false)]
		public int _GridColor { get { return GridColor.ToArgb(); } set { GridColor = Color.FromArgb(value); } }

		[Category("Layout")]
		[DisplayName("Grid Line Opacity")]
		[Description("Indicates the opacity of the grid lines. Valid values are 0 - 255. The higher the number the more opace the color.")]
		virtual public int GridColorAlphaValue
		{
			get { return m_GridColorAlphaValue; }
			set
			{
				m_GridColorAlphaValue = value;
				DrawLayers(0);
			}
		}

		[Category("Layout")]
		[DisplayName("Height")]
		[Description("Indicates the number of feet to represent in the height of the layout.")]
		virtual public int HeightInFeet
		{
			get { return m_HeightInFeet; }
			set
			{
				m_HeightInFeet = value > MAX_WIDTH_HEIGHT ? MAX_WIDTH_HEIGHT : value;
				OnSetHeightInFeet?.Invoke(m_HeightInFeet);
			}
		}

		[Category("Layout")]
		[DisplayName("Width")]
		[Description("Indicates the number of feet to represent in the width of the layout.")]
		virtual public int WidthInFeet
		{
			get { return m_WidthInFeet; }
			set
			{
				m_WidthInFeet = value > MAX_WIDTH_HEIGHT ? MAX_WIDTH_HEIGHT : value;
				OnSetWidthInFeet?.Invoke(m_WidthInFeet);
			}
		}

		#endregion

		#region Part Options

		[Category("Part Options")]
		[DisplayName("Lateral Drains")]
		[Description("Indicates the number of automatic lateral line drains that should be figured per valve.")]
		virtual public int LateralDrains { get; set; }

		[Category("Part Options")]
		[DisplayName("Mainline Drains")]
		[Description("Indicates the number of automatic mainline line drains that should be figured.")]
		virtual public int MainlineDrains { get; set; }

		[Category("Part Options")]
		[DisplayName("Round Pipe Length")]
		[Description("If true, the calculated length of pipe will be round up to the nearest 20'.")]
		virtual public bool RoundPipe { get; set; }

		#endregion

		[Browsable(false)]
		virtual public List<Part> StaticParts { get; set; }

		virtual protected void DrawLayers(int startLayer)
		{
			OnDrawLayers?.Invoke(startLayer);
		}
	}
}
