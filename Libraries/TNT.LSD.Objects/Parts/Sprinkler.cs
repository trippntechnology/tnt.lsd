using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using TNT.LSD.Inventory;
using TNT.LSD.Objects.ControlPoints;
using TNT.LSD.Settings;

namespace TNT.LSD.Objects
{
	/// <summary>
	/// Represents a sprinkler part
	/// </summary>
	public class Sprinkler : LateralPart
	{
		const string DEFAULT_INLET_SIZE = "1/2\"";

		#region Static Constants

		static readonly Point[] HEXAGON = {new Point(-4,-8), new Point(3,-8),
																			 new Point(7, -4), new Point(7,3),
																			 new Point(3, 7), new Point(-4, 7),
																			 new Point(-8, 3), new Point(-8, -4)};

		#endregion

		#region Properties

		#region Body

		/// <summary>
		/// <see cref="Body"/> backing field
		/// </summary>
		protected string m_Body;

		/// <summary>
		/// Type of body to use
		/// </summary>
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

					if (descriptionIndex > -1 && BodyCodes.Count > descriptionIndex)
						BodyCode = BodyCodes[descriptionIndex];

					if (descriptionIndex > -1 && descriptionIndex < InletSizes.Count)
					{
						InletSize = InletSizes[descriptionIndex];
					}
					else
					{
						InletSize = DEFAULT_INLET_SIZE;
					}
				}
			}
		}

		/// <summary>
		/// Descriptions of body
		/// </summary>
		[XmlIgnore()]
		[Browsable(false)]
		public List<string> BodyDescriptions { get; set; }

		/// <summary>
		/// Body code
		/// </summary>
		[Description("The part code associated with the body portion of the sprinkler")]
		[DisplayName("Body Code")]
		[ReadOnly(true)]
		public string BodyCode { get; set; }

		/// <summary>
		/// Available body codes
		/// </summary>
		[Editor(@"System.Windows.Forms.Design.StringCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
#if !PALETTE_PROPERTIES
		[Browsable(false)]
#endif
		public List<string> BodyCodes { get; set; } = new List<string>();

		#endregion

		#region Radius

		/// <summary>
		/// <see cref="Radius"/> backing field
		/// </summary>
		protected string m_Radius = string.Empty;

		/// <summary>
		/// Distance water can cover
		/// </summary>
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

		/// <summary>
		/// List of available radaii
		/// </summary>
		[Editor(@"System.Windows.Forms.Design.StringCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
#if !PALETTE_PROPERTIES
		[Browsable(false)]
#endif
		public List<string> Radii { get; set; } = new List<string>();

		#endregion

		#region Nozzle

		/// <summary>
		/// Nozzle descriptions
		/// </summary>
		[XmlIgnore()]
		[Browsable(false)]
		public List<string> NozzleDescriptions { get; set; }

		/// <summary>
		/// Nozzle codes
		/// </summary>
		[Description("The nozzle code associated with the nozzle of the sprinkler")]
		[DisplayName("Nozzle Code")]
		[ReadOnly(true)]
		public string NozzleCode { get; set; }

		/// <summary>
		/// Available nozzle codes
		/// </summary>
		[Editor(@"System.Windows.Forms.Design.StringCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
#if !PALETTE_PROPERTIES
		[Browsable(false)]
#endif
		public List<string> NozzleCodes { get; set; } = new List<string>();

		#endregion

		#region Inlet

		/// <summary>
		/// Inlet size
		/// </summary>
		[Description("Specifies the inlet size of the sprinkler")]
		[DisplayName("Inlet Size")]
		[ReadOnly(true)]
		public string InletSize { get; set; }

		/// <summary>
		/// Available inlet sizes
		/// </summary>
		[Editor(@"System.Windows.Forms.Design.StringCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
#if !PALETTE_PROPERTIES
		[Browsable(false)]
#endif
		public List<string> InletSizes { get; set; } = new List<String>();

		#endregion

		#region XOffset

		/// <summary>
		/// X offset
		/// </summary>
		[Description("Specifies the X offset")]
		[DisplayName("X Offset")]
		[DefaultValue(0.0)]
#if !PALETTE_PROPERTIES
		[Browsable(false)]
#endif
		public double XOffset { get; set; }

		#endregion

		#region YOffset

		/// <summary>
		/// Y offset
		/// </summary>
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

		/// <summary>
		/// Default constructor
		/// </summary>
		public Sprinkler()
			: base()
		{
			InletSize = "1/2\"";
		}

		/// <summary>
		/// Copy constructor
		/// </summary>
		/// <param name="obj"></param>
		public Sprinkler(Sprinkler obj)
			: base(obj)
		{
			BodyDescriptions = obj.BodyDescriptions != null ? new List<string>(obj.BodyDescriptions) : null;
			BodyCode = obj.BodyCode;
			BodyCodes = obj.BodyCodes != null ? new List<string>(obj.BodyCodes) : null;
			NozzleCodes = obj.NozzleCodes != null ? new List<string>(obj.NozzleCodes) : null;
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

		/// <summary>
		/// Clones this object
		/// </summary>
		public override TNTObject Clone() => new Sprinkler(this);

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

			if (obj is Sprinkler sprinkler)
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

		/// <summary>
		/// Draws a <see cref="Sprinkler"/>
		/// </summary>
		public override void Draw(Graphics graphics, DrawingOptions drawingOptions)
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

					int rectX = (-(x * TNTConstants.PIXELS_PER_FOOT) / 2) + (int)(XOffset * TNTConstants.PIXELS_PER_FOOT);
					int rectY = -(y * TNTConstants.PIXELS_PER_FOOT) + (int)(YOffset * TNTConstants.PIXELS_PER_FOOT);
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
				StringFormat format = new StringFormat
				{
					Alignment = StringAlignment.Center,
					LineAlignment = StringAlignment.Center
				};

				graphics.TranslateTransform(0, 15);
				graphics.DrawString(Radius.ToString(), myFont, sBrush, textRect, format);
				graphics.TranslateTransform(0, -15);

				graphics.TranslateTransform(-ControlPoints[0].XPos, -ControlPoints[0].YPos);
			}

			#endregion
		}

		/// <summary>
		/// Sets part quantities
		/// </summary>
		public override void SetPartQuantity(CodedParts parts, SystemType systemType)
		{
			base.SetPartQuantity(parts, systemType);

			SetBodyAndNozzle(parts);

			if (Pipes == null || Pipes.Count < 1) return;

			bool isRiser = Regex.IsMatch(BodyCode, "NI[0-9]{3}X[0-9]*");
			var pipeSize = GetMaxPipeSize();
			var inletSize = PartSize.GetSizeByReadable(InletSize);
			var outletSize = isRiser ? inletSize : PartSize.SIZE_050;

			string pipeCode = pipeSize.Code;
			string inletCode = inletSize.Code;
			string outletCode = outletSize.Code;

			if (systemType == SystemType.PVC)
			{
				SetPVCFittings(parts, pipeCode, outletCode);
			}
			else
			{
				SetPOLYFittings(parts, Pipes.Count, pipeSize, outletSize);
			}

			SetFunnyPipeAndRiser(parts, isRiser, inletCode);
		}

		/// <summary>
		/// Sets poly fittings
		/// </summary>
		public static void SetPOLYFittings(CodedParts parts, int pipesCount, PartSize pipeSize, PartSize outletSize)
		{
			if (pipesCount == 1)
			{
				if (pipeSize == outletSize)
				{
					parts.Add($"PF{pipeSize}BT90", 1);
				}
				else
				{
					parts.Add($"PF{pipeSize}X{outletSize}BT90", 1);
				}

				parts.AddHoseClamp(pipeSize.Code, 1);
			}
			else
			{
				if (outletSize == PartSize.SIZE_050)
				{
					parts.Add($"PF{pipeSize}HDSADDLE", 1);
				}
				else
				{
					parts.Add($"PF{pipeSize}X{outletSize}BBTTEE", 1);
					parts.AddHoseClamp(pipeSize.Code, 2);
				}
			}
		}

		/// <summary>
		/// Sets funny pipe and riser
		/// </summary>
		private static void SetFunnyPipeAndRiser(CodedParts parts, bool isRiser, string inletCode)
		{
			if (!isRiser)
			{
				string inletFPECode = string.Format("FPSBE{0}", inletCode);
				string inletMarlexCode = string.Format("FIM{0}STR90", inletCode);

				parts.Add("FPSBE050", 1);
				parts.Add(inletFPECode, 1);
				parts.Add(inletMarlexCode, 1);
				parts.Add("FUNNYPIPE", 2);
			}
		}

		/// <summary>
		/// Sets the fittings
		/// </summary>
		private void SetPVCFittings(CodedParts parts, string pipeCode, string outletCode)
		{
			string fittingCode;

			if (Pipes.Count == 1)
			{
				if (pipeCode == outletCode)
				{
					fittingCode = string.Format("FI{0}ST90", pipeCode);
				}
				else
				{
					fittingCode = string.Format("FI{0}X{1}ST90", pipeCode, outletCode);
				}

				parts.Add(fittingCode, 1);
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

				parts.Add(fittingCode, 1);
			}
		}

		/// <summary>
		/// Sets the body and nozzle
		/// </summary>
		private void SetBodyAndNozzle(CodedParts parts)
		{
			string[] bodyCodes = BodyCode.Split(';');

			foreach (string bodyCode in bodyCodes)
			{
				parts.Add(bodyCode, 1);
			}

			if (!string.IsNullOrEmpty(NozzleCode))
			{
				parts.Add(NozzleCode, 1);
			}
		}

		#endregion
	}
}
