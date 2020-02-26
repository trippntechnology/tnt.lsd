using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Linq;
using System.Xml.Serialization;
using TNT.LSD.Settings;

namespace TNT.LSD.Objects
{
	/// <summary>
	/// Represents a valve
	/// </summary>
	public class Valve : GenericPart
	{
		/// <summary>
		/// Default size of the circle drawn with the valve color
		/// </summary>
		protected const int DEFAULT_COLOR_RADIUS = 12;

		#region Properties

		/// <summary>
		/// Radius of color circle
		/// </summary>
		[Description("Radius of the background color")]
		[DisplayName("Color Radius")]
		[DefaultValue(DEFAULT_COLOR_RADIUS)]
#if !PALETTE_PROPERTIES
		[Browsable(false)]
#endif
		public int ColorRadius { get; set; }

		/// <summary>
		/// Color associated with zone
		/// </summary>
		[Description("Color associated with the zone")]
		[DisplayName("Zone Color")]
		[DefaultValue(typeof(Color), "Black")]
		[XmlIgnore()]
		[Browsable(true)]
		public override Color Color { get { return base.Color; } set { base.Color = value; } }

		/// <summary>
		/// Gender of inlet threads
		/// </summary>
		[TypeConverter(typeof(TypeConverters.ThreadGenderList))]
		[Description("Gender of inlet threads")]
		[DisplayName("Inlet Threads")]
		[DefaultValue("FIPT")]
#if !PALETTE_PROPERTIES
		[ReadOnly(true)]
#endif
		public string InletThreads { get; set; }

		/// <summary>
		/// Gender of outlet threads
		/// </summary>
		[TypeConverter(typeof(TypeConverters.ThreadGenderList))]
		[Description("Gender of outlet threads")]
		[DisplayName("Outlet Threads")]
		[DefaultValue("FIPT")]
#if !PALETTE_PROPERTIES
		[ReadOnly(true)]
#endif
		public string OutletThread { get; set; }

		/// <summary>
		/// List of size codes, i.e. 100, 125, etc that can be associate with this valve
		/// </summary>
		[Editor(@"System.Windows.Forms.Design.StringCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
#if !PALETTE_PROPERTIES
		[Browsable(false)]
#endif
		virtual public List<string> SizeCodes { get; set; }

		/// <summary>
		/// Size code of the valve
		/// </summary>
		[TypeConverter(typeof(TypeConverters.SizeList))]
		[Description("Valve Size")]
		[DefaultValue("100")]
		[ReadOnly(true)]
		public string SizeCode { get; set; } = "100";

		/// <summary>
		/// <see cref="ValveBox"/> that encloses this valve
		/// </summary>
		[Browsable(false)]
		[XmlIgnore()]
		public ValveBox AssociatedValveBox { get; set; }

		#endregion

		#region Constructors

		/// <summary>
		/// Default constructor
		/// </summary>
		public Valve()
			: base()
		{
			InletThreads = "FIPT";
			OutletThread = "FIPT";
			ColorRadius = DEFAULT_COLOR_RADIUS;
		}

		/// <summary>
		/// Copy constructor
		/// </summary>
		public Valve(Valve obj)
			: base(obj)
		{
			InletThreads = obj.InletThreads;
			OutletThread = obj.OutletThread;
			ColorRadius = obj.ColorRadius;
			SizeCodes = obj.SizeCodes;
		}

		#endregion

		/// <summary>
		/// Called when the <see cref="GenericPart.Model"/> changes
		/// </summary>
		/// <param name="index">Index of the model</param>
		protected override void onModelIndexChanged(int index)
		{
			if (SizeCodes.Count > index)
			{
				this.SizeCode = SizeCodes[index];
			}
		}

		/// <summary>
		/// Sets the Color throughout the zone
		/// </summary>
		public virtual void ColorizeZone() => ColorizeZone(this, null);

		/// <summary>
		/// Applies the color associated with the valve to the lateral lines and parts.
		/// </summary>
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

		/// <summary>
		/// Draws the color associated with the zone behind the valve image
		/// </summary>
		protected override void DrawBackground(Graphics graphics)
		{
			Color? backgroundColor = this is Valve ? this.Color : m_Pipes.Count > 0 ? (Color?)m_Pipes[0].PipeColor : null;

			if (backgroundColor != null && backgroundColor?.ToArgb() != Color.Black.ToArgb())
			{
				using (SolidBrush sb = new SolidBrush((Color)backgroundColor))
				{
					graphics.FillEllipse(sb, new Rectangle(base.Position.X - (this.ColorRadius / 2), base.Position.Y - (this.ColorRadius / 2), this.ColorRadius, this.ColorRadius));
				}
			}
		}

		/// <summary>
		/// Creates a copy of this <see cref="Valve"/>
		/// </summary>
		/// <returns></returns>
		public override TNTObject Clone() => new Valve(this);


		public override void SetPartQuantity(Dictionary<string, TNT.LSD.Inventory.Part> parts, SystemType systemType)
		{
			base.SetPartQuantity(parts, systemType);

			var latPipe = m_Pipes.Find(p => p is LateralPipe) as LateralPipe;
			var mainPipes = m_Pipes.FindAll(p => p is MainlinePipe).ConvertAll(p => p as MainlinePipe);
			var valveSize = PartSize.GetSize(this.SizeCode);

			System.Diagnostics.Debug.WriteLine($"AssociatedValveBox: {AssociatedValveBox}");
			System.Diagnostics.Debug.WriteLine($"valveSize: {valveSize}");
			System.Diagnostics.Debug.WriteLine($"size: {valveSize}");

			// See if a manifold exists
			var manifold = AssociatedValveBox?.GetManifold();

			if (manifold != null)
			{
				if (InletThreads == "FIPT")
				{
					// Add parts from manifold to valve
					if (manifold.Size == PartSize.SIZE_100 && valveSize == PartSize.SIZE_100)
					{
						// Male adapter
						parts[$"AF18010"].Quantity += 1;
					}
					else
					{
						// Add slip adapter from manifold to toe nipple
						parts[$"AF18012{manifold.Size.Code}"].Quantity += 1;

						// Add toe nipple out of valve
						parts[$"NI{valveSize.Code}X4TOE"].Quantity += 1;
						
						
						// Add coupler and bushing if needed
						if (manifold.Size == PartSize.SIZE_150)
						{
							if (valveSize == PartSize.SIZE_100)
							{
								// We're good
							}
							else if (valveSize == PartSize.SIZE_150)
							{
								parts[$"FI{manifold.Size.Code}SSCOUP"].Quantity += 1;
							}
						}
						else if (manifold.Size == PartSize.SIZE_200)
						{
							if (valveSize == PartSize.SIZE_100)
							{
								parts[$"FI{PartSize.SIZE_150.Code}X{PartSize.SIZE_100.Code}SSRB"].Quantity += 1;
							}
							else if (valveSize == PartSize.SIZE_150)
							{
								// We're good
							}
							else if (valveSize == PartSize.SIZE_200)
							{
								parts[$"FI{manifold.Size.Code}SSCOUP"].Quantity += 1;
							}
						}
					}

					// Add manifold fitting out of valve to lateral
					if (latPipe != null)
					{
						var latPipeSize = PartSize.GetSize(latPipe.PipeSizeIndex);

						if (OutletThread == "FIPT")
						{
							// Add transition nipple out of valve and slip adapter
							var transitionSize = valveSize == PartSize.SIZE_100 ? string.Empty : valveSize.Code;
							var slipAdapterCode = valveSize == PartSize.SIZE_100 && latPipeSize == PartSize.SIZE_075 ? 3 : 2;
							parts[$"AF18011{transitionSize}"].Quantity += 1;
							parts[$"AF1801{slipAdapterCode}{transitionSize}"].Quantity += 1;

							// Add coupler and bushing if needed
							if (valveSize == PartSize.SIZE_100)
							{
								if (latPipeSize == PartSize.SIZE_125)
								{
									parts[$"FI{PartSize.SIZE_125.Code}SSCOUP"].Quantity += 1;
								}
							}
							else if (valveSize == PartSize.SIZE_150)
							{
								if (latPipeSize < PartSize.SIZE_100)
								{
									parts[$"FI{PartSize.SIZE_100.Code}X{latPipeSize.Code}SSRB"].Quantity += 1;
								}
								else if (latPipeSize == PartSize.SIZE_125)
								{
									parts[$"FI{PartSize.SIZE_150.Code}SSCOUP"].Quantity += 1;
									parts[$"FI{PartSize.SIZE_150.Code}X{PartSize.SIZE_125.Code}SSRB"].Quantity += 1;
								}
								else if (latPipeSize == PartSize.SIZE_150)
								{
									parts[$"FI{PartSize.SIZE_150.Code}SSCOUP"].Quantity += 1;
								}
							}
							else if (valveSize == PartSize.SIZE_200)
							{
								if (latPipeSize < PartSize.SIZE_150)
								{
									parts[$"FI{PartSize.SIZE_150.Code}X{latPipeSize.Code}SSRB"].Quantity += 1;
								}
								else if (latPipeSize == PartSize.SIZE_200)
								{
									parts[$"FI{PartSize.SIZE_200.Code}SSCOUP"].Quantity += 1;
								}
							}
						}
						else // MIPT
						{
							// Add transition nipple out of valve and slip adapter
							var transitionSize = valveSize == PartSize.SIZE_100 ? string.Empty : valveSize.Code;
							parts[$"AF18017{transitionSize}"].Quantity += 1;
							parts[$"AF18012{transitionSize}"].Quantity += 1;

							if (valveSize == PartSize.SIZE_100)
							{
								if (latPipeSize < PartSize.SIZE_100)
								{
									parts[$"FI{valveSize.Code}X{latPipeSize.Code}SSRB"].Quantity += 1;
								}
								else if (latPipeSize == PartSize.SIZE_125)
								{
									parts[$"FI{PartSize.SIZE_125.Code}SSCOUP"].Quantity += 1;
								}
							}
						}
					}
				}
				else
				{
					throw new NotImplementedException();
				}
			}

			//if (AssociatedValveBox != null && AssociatedValveBox.UseManifold)
			//{
			//	// Add inlet MA
			//	switch (valveSizeIndex)
			//	{
			//		case 2: // 1"
			//			parts["AF18010"].Quantity += 1;
			//			break;
			//		case 4: // 1-1/2"
			//			parts["AF18010150"].Quantity += 1;
			//			break;
			//		case 5: // 2"
			//			parts["AF18010200"].Quantity += 1;
			//			break;
			//	}
			//}
			else
			{
				//int mainPipeSizeIndex = m_PipeSizeList.IndexOf(mainPipe.PipeSize);
				string toeCode = string.Format("NI{0}X4TOE", valveSize.Code);

				if (!parts.ContainsKey(toeCode))
				{
					parts.Add(toeCode, new TNT.LSD.Inventory.Part() { Code = toeCode });
				}

				parts[toeCode].Quantity += 1;
			}

			return;
			//if (latPipe != null)
			//{
			//	if (AssociatedValveBox != null && AssociatedValveBox.UseManifold)
			//	{
			//		if (OutletThread == "FIPT")
			//		{
			//			// Add outlet transition nipple
			//			parts["AF18011"].Quantity += 1;
			//		}
			//		else
			//		{
			//			// Add outlet FA
			//			parts["AF18017"].Quantity += 1;
			//		}

			//		// Add adapter to lateral pipe
			//		switch (latPipe.PipeSizeIndex)
			//		{
			//			case 1: // 3/4"
			//				parts["AF18013"].Quantity += 1;
			//				break;
			//			case 2: // 1"
			//				parts["AF18012"].Quantity += 1;
			//				break;
			//			case 3: // 1-1/4"
			//				parts["AF18012"].Quantity += 1;
			//				parts["FI125SSCOUP"].Quantity += 1;
			//				break;
			//			case 4: // 1-1/2"
			//				parts["AF18012200"].Quantity += 1;
			//				break;
			//			case 5: // 2"
			//				parts["AF18012200"].Quantity += 1;
			//				parts["FI200SSCOUP"].Quantity += 1;
			//				break;
			//		}
			//	}
			//	else
			//	{
			//		string code;

			//		if (OutletThread == "MIPT")
			//		{
			//			// Add FA
			//			code = valveSizeIndex == latPipeSizeIndex ? string.Format("FI{0}STFA", SIZE_CODE[valveSizeIndex]) : string.Format("FI{0}X{1}STFA", SIZE_CODE[latPipeSizeIndex], SIZE_CODE[valveSizeIndex]);
			//		}
			//		else
			//		{
			//			// Add MA
			//			code = valveSizeIndex == latPipeSizeIndex ? string.Format("FI{0}TSMA", SIZE_CODE[valveSizeIndex]) : string.Format("FI{0}X{1}TSMA", SIZE_CODE[valveSizeIndex], SIZE_CODE[latPipeSizeIndex]);
			//		}

			//		if (!parts.ContainsKey(code))
			//		{
			//			parts.Add(code, new TNT.LSD.Inventory.Part() { Code = code });
			//		}

			//		parts[code].Quantity += 1;
			//	}
			//}

			//if (mainPipe != null)
			//{
			//	if (AssociatedValveBox != null && AssociatedValveBox.UseManifold)
			//	{
			//		// Add inlet MA
			//		switch (valveSize.Index)
			//		{
			//			case 2: // 1"
			//				parts["AF18010"].Quantity += 1;
			//				break;
			//			case 4: // 1-1/2"
			//				parts["AF18010150"].Quantity += 1;
			//				break;
			//			case 5: // 2"
			//				parts["AF18010200"].Quantity += 1;
			//				break;
			//		}
			//	}
			//	else
			//	{
			//		int mainPipeSizeIndex = m_PipeSizeList.IndexOf(mainPipe.PipeSize);
			//		string toeCode = string.Format("NI{0}X4TOE", valveSize.Code);

			//		if (!parts.ContainsKey(toeCode))
			//		{
			//			parts.Add(toeCode, new TNT.LSD.Inventory.Part() { Code = toeCode });
			//		}

			//		parts[toeCode].Quantity += 1;
			//	}
			//}
		}

		/// <summary>
		/// Indicates if a pipe can be connected to this <see cref="Valve"/>
		/// </summary>
		/// <param name="pipeType">Type of <see cref="Pipe"/></param>
		/// <param name="reason">Reason that can be returned indicating why the pipe could not be added</param>
		/// <returns>True if <see cref="Pipe"/> of type <paramref name="pipeType"/> can be added, false otherwise</returns>
		public override bool CanAddPipe(System.Type pipeType, out string reason)
		{
			base.CanAddPipe(pipeType, out reason);

			var pipe = m_Pipes.FindAll(p => p.GetType() == pipeType);

			if (pipeType == typeof(MainlinePipe) && pipe?.Count > 1)
			{
				reason = "Only two main line connections permitted";
				return false;
			}
			else if (pipeType == typeof(LateralPipe) && pipe?.Count > 0)
			{
				reason = "Only one lateral line connection permitted";
				return false;
			}

			return true;
		}

		/// <summary>
		/// Sizes the lateral line pipe
		/// </summary>
		/// <param name="upstreamPipe"></param>
		/// <returns>The required flow from this point on downstream</returns>
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

		/// <summary>
		/// Gets the count of the <see cref="Pipe"/> connections of type <paramref name="pipeType"/>
		/// </summary>
		/// <returns>Count of the <see cref="Pipe"/> connections of type <paramref name="pipeType"/></returns>
		public int GetPipeCount(System.Type pipeType)
		{
			return m_Pipes.FindAll(p => p.GetType() == pipeType)?.Count ?? 0;
		}

		#endregion
	}
}
