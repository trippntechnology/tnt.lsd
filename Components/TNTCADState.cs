using LSDComponents.Settings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using System.Xml.Serialization;
using TNT.Configuration;
using TNT.LSD.Inventory;
using TNT.LSD.Inventory.DAL;
using TNT.LSD.Objects;


namespace LSDComponents
{
	using ObjectList = List<TNTObject>;
	using ObjectListList = List<List<TNTObject>>;

	public class TNTCADState
	{
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

		public string _BackgroundImage
		{
			get
			{
				using (MemoryStream ms = new MemoryStream())
				{
					if (this.BackgroundImage != null)
					{
						this.BackgroundImage.Save(ms, ImageFormat.Png);
					}

					return Convert.ToBase64String(ms.ToArray());
				}
			}

			set
			{
				if (value.Length > 0)
				{
					byte[] array = Convert.FromBase64String(value);

					using (MemoryStream ms = new MemoryStream(array))
					{
						this.BackgroundImage = Image.FromStream(ms);
					}
				}
			}
		}

		[TypeConverter(typeof(ExpandableObjectConverter))]
		virtual public CADSettings Settings { get; set; }

		[Browsable(false)]
		virtual public ObjectListList ObjectLayers { get; set; }

		[XmlIgnore()]
		public TNTCAD Parent { protected get { return Settings != null ? Settings.CAD : null; } set { if (Settings != null) { Settings.CAD = value; } } }

		#region Layout

		public bool DrawGrid { get { return Settings.DrawGrid; } }

		[XmlIgnore()]
		public int GridColorAlphaValue { get { return Settings.GridColorAlphaValue; } set { Settings.GridColorAlphaValue = value; } }

		[XmlIgnore()]
		public Color GridColor { get { return Settings.GridColor; } set { Settings.GridColor = value; } }

		public bool DrawUnits { get { return Settings.DrawUnits; } }

		[XmlIgnore()]
		public int HeightInFeet { get { return Settings.HeightInFeet; } set { Settings.HeightInFeet = value; } }

		[XmlIgnore()]
		public int WidthInFeet { get { return Settings.WidthInFeet; } set { Settings.WidthInFeet = value; } }

		public bool ShowLegend { get { return Settings.ShowLegend; } }

		public int MainlineDrains { get { return Settings.MainlineDrains; } }

		public int LateralDrains { get { return Settings.LateralDrains; } }

		public bool RoundPipe { get { return Settings.RoundPipe; } }

		public bool ShowExternalCodes { get { return Settings is SSCSettings ? (Settings as SSCSettings).ShowExternalCodes : false; } }

		[XmlIgnore()]
		public List<Part> StaticParts { get { return Settings.StaticParts; } }

		#endregion

		#endregion

		#region Constructors

		public TNTCADState()
			: this(null)
		{
		}

		public TNTCADState(TNTCAD parent)
			: base()
		{
			Parent = parent;
			ObjectLayers = new ObjectListList();

			Settings = XmlSection<CADSettings>.Deserialize("CAD");

			if (Settings != null)
			{
				Settings.CAD = parent;

				if (Settings.StaticParts != null)
				{
					Settings.StaticParts.ForEach(p =>
						{
							p.Description = DALPart.GetDescription(p.Code);
						});
				}
			}
		}

		#endregion

		virtual public void ResolveReferences()
		{
			foreach (ObjectList ol in ObjectLayers)
			{
				foreach (TNTObject o in ol)
				{
					o.ResolveReferences(ol);
				}
			}
		}

		virtual public void SelectAll()
		{
			foreach (ObjectList ol in ObjectLayers)
			{
				ol.ForEach(o => o.Selected = true);
			}
		}
	}
}
