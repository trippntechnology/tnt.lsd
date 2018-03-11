using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using TNT.LSD.Inventory;
using TNT.LSD.Objects.ControlPoints;

namespace TNT.LSD.Objects
{
	public class Sprinkler : LateralPart
	{
		#region Static Constants

		static Point[] HEXAGON = {new Point(-4,-8), new Point(3,-8),
															new Point(7, -4), new Point(7,3),
															new Point(3, 7), new Point(-4, 7),
															new Point(-8, 3), new Point(-8, -4)};

		#endregion

		#region Members

		protected List<string> m_BodyPartCodesList = new List<string>();
		protected List<string> m_NozzlePartCodesList = new List<string>();

		protected string m_Body;

		protected string m_Radius = string.Empty;
		protected List<string> m_RadiiList = new List<string>();

		#endregion

		#region Properties

		#region Body

		[Description("Specifies the type of body to use")]
		[TypeConverter(typeof(TypeConverters.PartDescriptionList))]
		public string Body
		{
			get { return m_Body; }
			set
			{
				m_Body = value;

				if (BodyDescriptions != null)
				{
					int descriptionIndex = BodyDescriptions.IndexOf(m_Body);

					if (descriptionIndex > -1 && m_BodyPartCodesList.Count > descriptionIndex)
						BodyCode = m_BodyPartCodesList[descriptionIndex];
				}
			}
		}

		[XmlIgnore()]
		[Browsable(false)]
		public List<string> BodyDescriptions { get; set; }

		[Description("The part code associated with the body portion of the sprinkler")]
		[DisplayName("Body Code")]
		[ReadOnly(true)]
		public string BodyCode { get; set; }

		[Editor(@"System.Windows.Forms.Design.StringCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
#if !PALETTE_PROPERTIES
		[Browsable(false)]
#endif
		public List<string> BodyCodes { get { return m_BodyPartCodesList; } set { m_BodyPartCodesList = value; } }

		#endregion

		#region Radius

		[Description("Indicates the distance the water shoot from the sprinkler")]
		[TypeConverter(typeof(TypeConverters.RadiiList))]
		public string Radius
		{
			get { return m_Radius; }
			set
			{
				m_Radius = value;

				if (Radii != null)
				{
					int index = Radii.IndexOf(m_Radius);

					if (index > -1)
					{
						if (NozzleCodes.Count > index)
						{
							NozzleCode = NozzleCodes[index];
						}

						SetGPM();
					}
				}
			}
		}

		[Editor(@"System.Windows.Forms.Design.StringCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
#if !PALETTE_PROPERTIES
		[Browsable(false)]
#endif
		public List<string> Radii { get { return m_RadiiList; } set { m_RadiiList = value; } }

		#endregion

		#region Nozzle

		[XmlIgnore()]
		[Browsable(false)]
		public List<string> NozzleDescriptions { get; set; }

		[Description("The nozzle code associated with the nozzle of the sprinkler")]
		[DisplayName("Nozzle Code")]
		[ReadOnly(true)]
		public string NozzleCode { get; set; }

		[Editor(@"System.Windows.Forms.Design.StringCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
#if !PALETTE_PROPERTIES
		[Browsable(false)]
#endif
		public List<string> NozzleCodes { get { return m_NozzlePartCodesList; } set { m_NozzlePartCodesList = value; } }

		#endregion

		#region Inlet

		[Description("Specifies the inlet size of the sprinkler")]
		[DisplayName("Inlet Size")]
		[TypeConverter(typeof(TypeConverters.SizeList))]
#if !PALETTE_PROPERTIES
		[Browsable(false)]
#endif
		public string InletSize { get; set; }

		#endregion

		#region XOffset

		[Description("Specifies the X offset")]
		[DisplayName("X Offset")]
		[DefaultValue(0.0)]
#if !PALETTE_PROPERTIES
		[Browsable(false)]
#endif
		public double XOffset { get; set; }

		#endregion

		#region YOffset

		[Description("Specifies the Y offset")]
		[DisplayName("Y Offset")]
		[DefaultValue(0.0)]
#if !PALETTE_PROPERTIES
		[Browsable(false)]
#endif
		public double YOffset { get; set; }

		#endregion

		#endregion

		#region Constructors

		public Sprinkler()
			: base()
		{
			InletSize = "1/2\"";
		}

		public Sprinkler(Sprinkler obj)
			: base(obj)
		{
			BodyDescriptions = obj.BodyDescriptions != null ? new List<string>(obj.BodyDescriptions) : null;
			BodyCode = obj.BodyCode;
			m_BodyPartCodesList = obj.m_BodyPartCodesList != null ? new List<string>(obj.m_BodyPartCodesList) : null;
			m_NozzlePartCodesList = obj.m_NozzlePartCodesList != null ? new List<string>(obj.m_NozzlePartCodesList) : null;
			NozzleDescriptions = obj.NozzleDescriptions != null ? new List<string>(obj.NozzleDescriptions) : null;
			NozzleCode = obj.NozzleCode;
			Body = obj.Body;
			InletSize = obj.InletSize;
			Radii = obj.Radii != null ? new List<string>(obj.Radii) : null;
			Radius = obj.Radius;
			XOffset = obj.XOffset;
			YOffset = obj.YOffset;
		}

		#endregion

		/// <summary>
		/// Sets the GPM property. By default it using the GPMList item with the 
		/// same index as the Radii item.
		/// </summary>
		virtual protected void SetGPM()
		{
			if (!string.IsNullOrEmpty(Radius))
			{
				int index = Radii.IndexOf(Radius);

				if (GPMList.Count > index)
				{
					GPM = GPMList[index];
				}
			}
		}

		#region Overrides

		public override TNTObject Clone()
		{
			return new Sprinkler(this);
		}

		/// <summary>
		/// Creates undo copy
		/// </summary>
		/// <returns>Undo copy</returns>
		public override TNTObject CreateUndoCopy()
		{
			Sprinkler newObj = base.CreateUndoCopy() as Sprinkler;

			newObj.ControlPoints[0].OnMoved = CenterPointMoved;
			(newObj.ControlPoints[1] as RotationControlPoint).OnMoved = RotationPointMoved;
			newObj.BodyCodes = BodyCodes;
			newObj.Body = Body;
			newObj.NozzleCodes = NozzleCodes;
			newObj.Radii = Radii;
			newObj.Radius = Radius;

			return newObj;
		}

		/// <summary>
		/// Set obj properties on this object
		/// </summary>
		/// <param name="obj">Source object</param>
		public override void Assign(TNTObject obj)
		{
			base.Assign(obj);

			Sprinkler sprinkler = obj as Sprinkler;

			if (sprinkler != null)
			{
				BodyCodes = sprinkler.BodyCodes;
				Body = sprinkler.Body;
				NozzleCodes = sprinkler.NozzleCodes;
				Radii = sprinkler.Radii;
				Radius = sprinkler.Radius;
				XOffset = sprinkler.XOffset;
				YOffset = sprinkler.YOffset;
			}
		}

		public override void Draw(System.Drawing.Graphics graphics, DrawingOptions drawingOptions)
		{
			#region Riser indicator

			if (Regex.IsMatch(BodyCode, "NI[0-9]*X[0-9]*"))
			{
				graphics.TranslateTransform(ControlPoints[0].XPos, ControlPoints[0].YPos);
				graphics.DrawPolygon(new Pen(Color.Black), HEXAGON);
				graphics.TranslateTransform(-ControlPoints[0].XPos, -ControlPoints[0].YPos);
			}

			#endregion

			base.Draw(graphics, drawingOptions);

			if (this is Sprinkler && drawingOptions.ShowCoverage)
			{
				Regex rectangular = new Regex("(?<y>[0-9]*)' *(x|X) *(?<x>[0-9]*)'");
				Match match = rectangular.Match(Radius);

				if (match.Success)
				{
					int x = Convert.ToInt32(match.Groups["x"].Value);
					int y = Convert.ToInt32(match.Groups["y"].Value);

					int rectX = (-(x * TNTConstants.PIXELS_PER_FOOT) / 2) + (int)(this.XOffset * TNTConstants.PIXELS_PER_FOOT);
					int rectY = -(y * TNTConstants.PIXELS_PER_FOOT) + (int)(this.YOffset * TNTConstants.PIXELS_PER_FOOT);
					int rectWidth = x * TNTConstants.PIXELS_PER_FOOT;
					int rectHeight = y * TNTConstants.PIXELS_PER_FOOT;

					Rectangle drawingRect = new Rectangle(rectX, rectY, rectWidth, rectHeight);

					graphics.TranslateTransform(ControlPoints[0].XPos, ControlPoints[0].YPos);
					graphics.RotateTransform(RotationAngle);
					graphics.FillRectangle(new SolidBrush(TNTConstants.COVERAGE_COLOR), drawingRect);
					graphics.RotateTransform(-RotationAngle);
					graphics.TranslateTransform(-ControlPoints[0].XPos, -ControlPoints[0].YPos);
				}
			}

			#region Label heads

			if (drawingOptions.LabelHeads)
			{
				graphics.TranslateTransform(ControlPoints[0].XPos, ControlPoints[0].YPos);

				Font myFont = new Font(drawingOptions.BaseFont.Name, drawingOptions.BaseFont.Size);
				SolidBrush sBrush = new SolidBrush(Color.Black);
				RectangleF textRect = new RectangleF(-100.0f, -25.0f, 200.0f, 50.0f);
				StringFormat format = new StringFormat();
				format.Alignment = StringAlignment.Center;
				format.LineAlignment = StringAlignment.Center;

				graphics.TranslateTransform(0, 15);
				graphics.DrawString(Radius.ToString(), myFont, sBrush, textRect, format);
				graphics.TranslateTransform(0, -15);

				graphics.TranslateTransform(-ControlPoints[0].XPos, -ControlPoints[0].YPos);
			}

			#endregion
		}

		public override void SetPartQuantity(Dictionary<string, TNT.LSD.Inventory.Part> parts)
		{
			base.SetPartQuantity(parts);

			#region Body/Nozzle

			string[] bodyCodes = BodyCode.Split(';');

			foreach (string bodyCode in bodyCodes)
			{
				if (!parts.ContainsKey(bodyCode))
				{
					parts.Add(bodyCode, new Part() { Code = bodyCode });
				}

				parts[bodyCode].Quantity += 1;
			}

			if (!string.IsNullOrEmpty(NozzleCode))
			{
				if (!parts.ContainsKey(NozzleCode))
				{
					parts.Add(NozzleCode, new Part() { Code = NozzleCode });
				}

				parts[NozzleCode].Quantity += 1;
			}

			#endregion

			if (m_Pipes == null || m_Pipes.Count < 1)
			{
				return;
			}

			bool isRiser = Regex.IsMatch(BodyCode, "NI[0-9]{3}X[0-9]*");
			int pipeSizeIndex = GetMaxPipeSizeIndex();
			int inletSizeIndex = (new TypeConverters.SizeList()).IndexOf(InletSize);
			int outletSizeIndex = isRiser ? inletSizeIndex : 0;

			string pipeCode = SIZE_CODE[pipeSizeIndex];
			string inletCode = SIZE_CODE[inletSizeIndex];
			string outletCode = SIZE_CODE[outletSizeIndex];

			#region Fitting

			string fittingCode = string.Empty;

			if (m_Pipes.Count == 1)
			{
				if (pipeCode == outletCode)
				{
					fittingCode = string.Format("FI{0}ST90", pipeCode);
				}
				else
				{
					fittingCode = string.Format("FI{0}X{1}ST90", pipeCode, outletCode);
				}

				if (!parts.ContainsKey(fittingCode))
				{
					parts.Add(fittingCode, new Part() { Code = fittingCode });
				}

				parts[fittingCode].Quantity += 1;
			}
			else
			{
				if (pipeCode == outletCode)
				{
					fittingCode = string.Format("FI{0}SSTTEE", pipeCode);
				}
				else
				{
					fittingCode = string.Format("FI{0}X{1}SSTTEE", pipeCode, outletCode);
				}

				if (!parts.ContainsKey(fittingCode))
				{
					parts.Add(fittingCode, new Part() { Code = fittingCode });
				}

				parts[fittingCode].Quantity += 1;
			}

			#endregion

			#region Funny Pipe/Riser

			if (!isRiser)
			{
				string inletFPECode = string.Format("FPSBE{0}", inletCode);
				string inletMarlexCode = string.Format("FIM{0}STR90", inletCode);

				parts["FPSBE050"].Quantity += 1;

				if (!parts.ContainsKey(inletFPECode))
				{
					parts.Add(inletFPECode, new Part() { Code = inletFPECode });
				}

				parts[inletFPECode].Quantity += 1;
				parts[inletMarlexCode].Quantity += 1;
				parts["FUNNYPIPE"].Quantity += 2;
			}

			#endregion
		}

		#endregion
	}
}
