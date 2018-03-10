using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using TNT.LSD.Settings.TypeConverters;

namespace TNT.LSD.Objects
{
	public class Valve : GenericPart
	{
		#region Properties

		[Description("Color associated with the zone")]
		[DisplayName("Zone Color")]
		[DefaultValue(typeof(Color), "Black")]
		[XmlIgnore()]
		[Browsable(true)]
		public override Color Color { get { return base.Color; } set { base.Color = value; } }

		[TypeConverter(typeof(TypeConverters.ThreadGenderList))]
		[Description("Gender if inlet threads")]
		[DisplayName("Inlet Threads")]
		[DefaultValue("FIPT")]
#if !PALETTE_PROPERTIES
		[ReadOnly(true)]
#endif
		public string InletThreads { get; set; }

		[TypeConverter(typeof(TypeConverters.ThreadGenderList))]
		[Description("Gender if outlet threads")]
		[DisplayName("Outlet Threads")]
		[DefaultValue("FIPT")]
#if !PALETTE_PROPERTIES
		[ReadOnly(true)]
#endif
		public string OutletThread { get; set; }

		[Browsable(false)]
		[XmlIgnore()]
		public ValveBox AssociatedValveBox { get; set; }

		#endregion

		#region Constructors

		public Valve()
			: base()
		{
			InletThreads = "FIPT";
			OutletThread = "FIPT";
		}

		public Valve(Valve obj)
			: base(obj)
		{
			InletThreads = obj.InletThreads;
			OutletThread = obj.OutletThread;
		}

		#endregion

		/// <summary>
		/// Sets the Color throughout the zone
		/// </summary>
		public virtual void ColorizeZone()
		{
			ColorizeZone(this, null);
		}

		protected virtual void ColorizeZone(TNTPart currentPart, TNTPart lastPart)
		{
			// Get the lateral pipes
			var latPipes = (from p in currentPart.Pipes where p is LateralPipe select p).ToList();

			// Set the color on the pipe and the other part
			latPipes.ForEach(p =>
				{
					TNTPart other = p.GetOtherPart(currentPart);

					p.PipeColor = currentPart.Color;

					if (lastPart == null || lastPart != other)
					{
						// All parts are initially set to black before calling ColorizeZone, therefore if we encounter
						// a part that isn't set to black we have already visited it and should ignore it
						if (other.Color == Color.Black)
						{
							other.Color = currentPart.Color;

							// Color parts down stream
							ColorizeZone(other, currentPart);
						}
					}
				});
		}

		#region Overrides

		public override TNTObject Clone()
		{
			return new Valve(this);
		}

		public override void SetPartQuantity(Dictionary<string, TNT.LSD.Inventory.Part> parts)
		{
			base.SetPartQuantity(parts);

			LateralPipe latPipe = m_Pipes.Find(p => p is LateralPipe) as LateralPipe;
			MainlinePipe mainPipe = m_Pipes.Find(p => p is MainlinePipe) as MainlinePipe;

			Match match = Regex.Match(Model, @"^(?<size>[^ ]*)");

			if (!match.Success)
			{
				return;
			}

			PipeSizeList pipeSizeList = new PipeSizeList();
			string valveSize = match.Groups["size"].ToString();
			int valveSizeIndex = pipeSizeList.IndexOf(valveSize);

			if (latPipe != null)
			{
				int latPipeSizeIndex = pipeSizeList.IndexOf(latPipe.PipeSize);

				if (AssociatedValveBox != null && AssociatedValveBox.UseManifold)
				{
					if (OutletThread == "FIPT")
					{
						// Add outlet transition nipple
						parts["AF18011"].Quantity += 1;
					}
					else
					{
						// Add outlet FA
						parts["AF18017"].Quantity += 1;
					}

					// Add adapter to lateral pipe
					switch (latPipeSizeIndex)
					{
						case 1: // 3/4"
							parts["AF18013"].Quantity += 1;
							break;
						case 2: // 1"
							parts["AF18012"].Quantity += 1;
							break;
						case 3: // 1-1/4"
							parts["AF18012"].Quantity += 1;
							parts["FI125SSCOUP"].Quantity += 1;
							break;
						case 4: // 1-1/2"
							parts["AF18012200"].Quantity += 1;
							break;
						case 5: // 2"
							parts["AF18012200"].Quantity += 1;
							parts["FI200SSCOUP"].Quantity += 1;
							break;
					}
				}
				else
				{
					string code;

					if (OutletThread == "MIPT")
					{
						// Add FA
						code = valveSizeIndex == latPipeSizeIndex ? string.Format("FI{0}STFA", SIZE_CODE[valveSizeIndex]) : string.Format("FI{0}X{1}STFA", SIZE_CODE[latPipeSizeIndex], SIZE_CODE[valveSizeIndex]);
					}
					else
					{
						// Add MA
						code = valveSizeIndex == latPipeSizeIndex ? string.Format("FI{0}TSMA", SIZE_CODE[valveSizeIndex]) : string.Format("FI{0}X{1}TSMA", SIZE_CODE[valveSizeIndex], SIZE_CODE[latPipeSizeIndex]);
					}

					if (!parts.ContainsKey(code))
					{
						parts.Add(code, new TNT.LSD.Inventory.Part() { Code = code });
					}

					parts[code].Quantity += 1;
				}
			}

			if (mainPipe != null)
			{
				if (AssociatedValveBox != null && AssociatedValveBox.UseManifold)
				{
					// Add inlet MA
					switch (valveSizeIndex)
					{
						case 2: // 1"
							parts["AF18010"].Quantity += 1;
							break;
						case 4: // 1-1/2"
							parts["AF18010150"].Quantity += 1;
							break;
						case 5: // 2"
							parts["AF18010200"].Quantity += 1;
							break;
					}
				}
				else
				{
					int mainPipeSizeIndex = m_PipeSizeList.IndexOf(mainPipe.PipeSize);
					string toeCode = string.Format("NI{0}X4TOE", SIZE_CODE[valveSizeIndex]);

					if (!parts.ContainsKey(toeCode))
					{
						parts.Add(toeCode, new TNT.LSD.Inventory.Part() { Code = toeCode });
					}

					parts[toeCode].Quantity += 1;
				}
			}
		}

		public override bool CanAddPipe(System.Type pipeType, out string reason)
		{
			base.CanAddPipe(pipeType, out reason);

			Pipe pipe = m_Pipes.Find(p => p.GetType() == pipeType);

			if (pipe != null)
			{
				reason = "Only one connection for each type of pipe permitted";
				return false;
			}

			return true;
		}

		public override bool TerminatePipe()
		{
			return true;
		}

		public override double SizePipe(Pipe upstreamPipe)
		{
			if (!Sized)
			{
				// Only size downstream pipe from valve. Find mainline pipe if exists
				if (upstreamPipe == null)
				{
					upstreamPipe = m_Pipes.Find(p => p is MainlinePipe);
				}

				RequiredFlow = 0;
				RequiredFlow = base.SizePipe(upstreamPipe);
			}

			Sized = true;

			return RequiredFlow;
		}

		#endregion
	}
}
