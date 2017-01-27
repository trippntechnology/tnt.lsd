using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Xml.Serialization;
using TNT.LSD.Inventory;
using TNT.LSD.Objects;

namespace LSDComponents.Settings
{
	public class CADSettings
	{
		protected const int MAX_WIDTH_HEIGHT = 800;

		protected Legend m_HiddenLegend = null;

		protected string m_Version;
		protected string m_TelephoneNumber;
		protected int m_HeightInFeet;
		protected int m_WidthInFeet;
		protected bool m_DrawUnits;
		protected bool m_DrawGrid;
		protected Color m_GridColor;
		protected int m_GridColorAlphaValue;
		protected bool m_ShowLegend;
		protected bool m_ShowBackgroundImage;

		[Browsable(false)]
		[XmlIgnore()]
		virtual public TNTCAD CAD { get; set; }

		virtual protected List<List<TNTObject>> Layers { get { return CAD.State.ObjectLayers; } }

		#region Layout

		[Category("Layout")]
		[DisplayName("Show Background Image")]
		[Description("Show/Hide the background image")]
		virtual public bool ShowBackgroundImage
		{

			get
			{
				return m_ShowBackgroundImage;
			}
			set
			{
				m_ShowBackgroundImage = value;
				DrawLayers(0);
			}
		}

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
		[DisplayName("Show Legend")]
		[Description("Indicates whether the ledend is visible.")]
		virtual public bool ShowLegend
		{
			get { return m_ShowLegend; }
			set
			{
				m_ShowLegend = value;

				if (CAD != null && Layers.Count > 1)
				{
					// Get the legend object
					Legend legend = Layers[1].Find(o => o is Legend) as Legend;

					if (legend == null)
					{
						if (m_HiddenLegend != null)
						{
							legend = m_HiddenLegend;
						}
						else
						{
							// Doesn't exist yet so create it
							legend = new Legend(new Point(TNTConstants.PIXELS_PER_FOOT, TNTConstants.PIXELS_PER_FOOT));
						}
					}

					legend.Visible = m_ShowLegend;
					legend.Selected = false;
					Layers[1].Remove(legend);

					if (m_ShowLegend)
					{
						// Add it to layer
						Layers[1].Add(legend);
					}
					else
					{
						m_HiddenLegend = legend;
					}

					DrawLayers(1);
				}
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

				if (CAD != null)
				{
					CAD.Height = (int)(m_HeightInFeet * TNTConstants.PIXELS_PER_FOOT * CAD.DisplayScale / 100.0);
				}
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

				if (CAD != null)
				{
					CAD.Width = (int)(m_WidthInFeet * TNTConstants.PIXELS_PER_FOOT * CAD.DisplayScale / 100.0);
				}
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
			if (CAD != null)
			{
				CAD.DrawLayers(startLayer);
			}
		}

		public override string ToString()
		{
			return string.Empty;
		}
	}
}
