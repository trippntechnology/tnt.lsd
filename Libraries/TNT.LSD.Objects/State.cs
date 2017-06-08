using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using TNT.LSD.Inventory;

namespace TNT.LSD.Objects
{
	using ObjectListList = List<List<TNTObject>>;

	public class State
	{
		protected const int MAX_WIDTH_HEIGHT = 500;

		protected string m_Version;
		protected string m_TelephoneNumber;
		protected int m_HeightInFeet;
		protected int m_WidthInFeet;

		#region Properties

		[ReadOnly(true)]
		virtual public string Version
		{
			get
			{
				Assembly asm = Assembly.GetEntryAssembly();
				return asm.GetName().Version.ToString();
			}
			set { }
		}


		[Browsable(false)]
		[XmlIgnore()]
		virtual public Image BackgroundImage { get; set; }

		#region Owner

		[Category("Owner")]
		[DisplayName("Design Number")]
		[Description("Indicates the design number.")]
		virtual public string Number { get; set; }

		[Category("Owner")]
		[Description("Name of the property owner.")]
		virtual public string Name { get; set; }

		[Category("Owner")]
		[DisplayName("Telephone Number")]
		[Description("Telephone number where owner can be reached.")]
		virtual public string TelephoneNumber
		{
			get { return m_TelephoneNumber; }
			set
			{
				string tmp = Regex.Replace(value, "[^0-9]*", "");

				if (tmp.Length == 7)
				{
					m_TelephoneNumber = string.Format("{0}-{1}", tmp.Substring(0, 3), tmp.Substring(3, 4));
				}
				else if (tmp.Length == 10)
				{
					m_TelephoneNumber = string.Format("({0}) {1}-{2}", tmp.Substring(0, 3), tmp.Substring(3, 3), tmp.Substring(6, 4));
				}
				else
				{
					m_TelephoneNumber = value;
				}
			}
		}

		[Category("Owner")]
		[DisplayName("eMail Address")]
		[Description("Owner's email address.")]
		virtual public string EmailAddress { get; set; }

		#endregion

		#region Culinary properties

		[TypeConverter(typeof(TypeConverters.YesNoNAList))]
		[Category("Culinary Source")]
		[DisplayName("Include Culinary Stop and Waste")]
		[Description("Stop and Waste values provide a way to turn the water on and off to the sprinkler system.")]
		virtual public string IncludeCulinarySW { get; set; }

		[TypeConverter(typeof(TypeConverters.PSIList))]
		[Category("Culinary Source")]
		[DisplayName("Working Culinary PSI")]
		[Description("Working (dynamic) water pressure.")]
		virtual public string CulinaryPSI { get; set; }

		[TypeConverter(typeof(TypeConverters.PipeSizeList))]
		[Category("Culinary Source")]
		[DisplayName("Culinary Supply Size")]
		[Description("Size of pipe that provides the water supply.")]
		virtual public string CulinarySize { get; set; }

		[TypeConverter(typeof(TypeConverters.PipeTypeList))]
		[Category("Culinary Source")]
		[DisplayName("Culinary Supply Type")]
		[Description("Type of pipe that provides the water supply.")]
		virtual public string CulinaryType { get; set; }

		#endregion

		#region Secondary properties

		[TypeConverter(typeof(TypeConverters.YesNoNAList))]
		[Category("Secondary Source")]
		[DisplayName("Include Secondary Stop and Waste")]
		[Description("Stop and Waste values provide a way to turn the water on and off to the sprinkler system.")]
		virtual public string IncludeSecondarySW { get; set; }

		[TypeConverter(typeof(TypeConverters.PSIList))]
		[Category("Secondary Source")]
		[DisplayName("Working Secondary PSI")]
		[Description("Working (dynamic) water pressure.")]
		virtual public string SecondaryPSI { get; set; }

		[TypeConverter(typeof(TypeConverters.PipeSizeList))]
		[Category("Secondary Source")]
		[DisplayName("Secondary Supply Size")]
		[Description("Size of pipe that provides the water supply.")]
		virtual public string SecondarySize { get; set; }

		[TypeConverter(typeof(TypeConverters.PipeTypeList))]
		[Category("Secondary Source")]
		[DisplayName("Secondary Supply Type")]
		[Description("Type of pipe that provides the water supply.")]
		virtual public string SecondaryType { get; set; }

		#endregion

		#region Layout

		[Category("Layout")]
		[DisplayName("Draw Units")]
		[Description("Indicates whether the grid units are drawn.")]
		virtual public bool DrawUnits { get; set; }

		[Category("Layout")]
		[DisplayName("Draw Grid")]
		[Description("Indicates whether the grid lines should be drawn.")]
		[ReadOnly(true)]
		virtual public bool DrawGrid { get; set; }

		[Category("Layout")]
		[DisplayName("Grid Line Color")]
		[Description("Indicates the color to use for the grid lines.")]
		[XmlIgnore()]
		virtual public Color GridColor { get; set; }

		[Browsable(false)]
		public int _GridColor { get { return GridColor.ToArgb(); } set { GridColor = Color.FromArgb(value); } }

		[Category("Layout")]
		[DisplayName("Grid Line Opacity")]
		[Description("Indicates the opacity of the grid lines. Valid values are 0 - 255. The higher the number the more opace the color.")]
		virtual public int GridColorAlphaValue { get; set; }

		[Category("Layout")]
		[DisplayName("Show Legend")]
		[Description("Indicates whether the ledend is visible.")]
		virtual public bool ShowLegend { get; set; }

		[Category("Layout")]
		[DisplayName("Height")]
		[Description("Indicates the number of feet to represent in the height of the layout.")]
		virtual public int HeightInFeet 
		{
			get { return m_HeightInFeet; }
			set
			{
				m_HeightInFeet = value > MAX_WIDTH_HEIGHT ? MAX_WIDTH_HEIGHT : value;
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
			}
		}

		[Category("Layout")]
		[Description("Additional comments")]
		[Editor(typeof(System.ComponentModel.Design.MultilineStringEditor), typeof(UITypeEditor))]
		virtual public string Comment { get; set; }

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

		[Category("Part Options")]
		[DisplayName("Show External Codes")]
		[Description("If true, codes displayed in the parts listing will be those mapped to internal codes.")]
		virtual public bool ShowExternalCodes { get; set; }

		#endregion

		[Browsable(false)]
		virtual public ObjectListList ObjectLayers { get; set; }

		[Browsable(false)]
		virtual public List<Part> StaticParts { get; set; }

		#endregion

		#region Constructors

		public State()
		{
		}

		#endregion
	}
}
