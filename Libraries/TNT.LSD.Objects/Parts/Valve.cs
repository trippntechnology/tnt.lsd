using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Linq;
using System.Xml.Serialization;
using TNT.LSD.Inventory;
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
			Color? backgroundColor = this is Valve ? this.Color : Pipes.Count > 0 ? (Color?)Pipes[0].PipeColor : null;

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

		/// <summary>
		/// Sets parts for the valve
		/// </summary>
		public override void SetPartQuantity(CodedParts parts, SystemType systemType)
		{
			var latPipe = Pipes.Find(p => p is LateralPipe) as LateralPipe;
			var mainPipes = Pipes.FindAll(p => p is MainlinePipe).ConvertAll(p => p as MainlinePipe);
			var valveSize = PartSize.GetSize(this.SizeCode);

			// Add valve
			parts.Add(ModelCode, 1);

			// See if a manifold exists
			var manifold = AssociatedValveBox?.GetManifold();

			if (manifold != null)
			{
				// Add parts from manifold to valve
				AddInletParts(parts, InletThreads, valveSize, manifold);

				// Add manifold fitting out of valve to lateral
				if (latPipe != null)
				{
					AddOutletManifoldParts(parts, OutletThread, PartSize.GetSize(latPipe.PipeSizeIndex), valveSize, systemType);
				}
			}
			else if (mainPipes.Count > 0)
			{
				var mainSize = GetMaxPipeSize(typeof(MainlinePipe));

				AddInletParts(parts, InletThreads, mainSize, mainPipes.Count, valveSize, systemType);

				if (latPipe != null)
				{
					AddOutletParts(parts, OutletThread, valveSize, PartSize.GetSize(latPipe.PipeSizeIndex), systemType);
				}
			}
		}

		/// <summary>
		/// Gets the parts associated with the outlet of the valve in a non-manifold configuration
		/// </summary>
		public static void AddOutletParts(CodedParts parts, string outletThread, PartSize valveSize, PartSize pipeSize, SystemType systemType)
		{
			if (systemType == SystemType.PVC)
			{
				if (outletThread == "FIPT")
				{
					parts.Add($"NI{valveSize}X4TOE", 1);

					if (valveSize == pipeSize)
					{
						parts.Add($"FI{valveSize}SSCOUP", 1);
					}
					else if (valveSize < pipeSize)
					{
						parts.Add($"FI{pipeSize}SSCOUP", 1);
						parts.Add($"FI{pipeSize}X{valveSize}SSRB", 1);
					}
					else
					{
						parts.Add($"FI{valveSize}SSCOUP", 1);
						parts.Add($"FI{valveSize}X{pipeSize}SSRB", 1);
					}
				}
				else
				{
					if (valveSize > pipeSize)
					{
						parts.Add($"FI{valveSize}STFA", 1);
						parts.Add($"FI{valveSize}X{pipeSize}SSRB", 1);
					}
					else if (pipeSize > valveSize)
					{
						parts.Add($"FI{pipeSize}SSCOUP", 1);
						parts.Add($"FI{pipeSize}X{valveSize}STRB", 1);
					}
					else
					{
						parts.Add($"FI{valveSize}STFA", 1);
					}
				}
			}
			else // POLY
			{
				if (valveSize != PartSize.SIZE_100) throw new NotSupportedException("Only 1\" valves supported");
				if (pipeSize > PartSize.SIZE_125) throw new NotSupportedException("Pipe size larger than 125 not supported");

				if (outletThread == "FIPT")
				{
					parts.Add($"PF{valveSize}BTMA", 1);
				}
				else // MIPT
				{
					parts.Add($"PF{valveSize}BTFA", 1);
				}

				parts.AddHoseClamp(valveSize.Code, 1);

				if (valveSize != pipeSize)
				{
					if (valveSize > pipeSize)
					{
						parts.Add($"PF{valveSize}X{pipeSize}BBRB", 1);
					}
					else
					{
						parts.Add($"PF{pipeSize}X{valveSize}BBRB", 1);
					}
					parts.AddHoseClamp(valveSize.Code, 1);
					parts.AddHoseClamp(pipeSize.Code, 1);
				}
			}
		}

		/// <summary>
		/// Gets the inlet parts in a non-manifold configuration
		/// </summary>
		public static void AddInletParts(CodedParts parts, string inletThreads, PartSize mainSize, int pipeCount, PartSize valveSize, SystemType systemType)
		{
			if (mainSize == null) return;
			if (inletThreads == "MIPT") throw new NotSupportedException("Only female inlet valves are supported");

			if (systemType == SystemType.PVC)
			{
				if (mainSize == PartSize.SIZE_075)
				{
					parts.Add($"FI{valveSize}X{mainSize}TTRB", 1);
					parts.Add($"NI{mainSize}X4TOE", 1);
				}
				else
				{
					parts.Add($"NI{valveSize}X4TOE", 1);
				}

				if (pipeCount == 2)
				{
					parts.Add($"FI{mainSize}SSSTEE", 1);
				}
				else
				{
					parts.Add($"FI{mainSize}SS90", 1);
				}

				if (mainSize > valveSize) parts.Add($"FI{mainSize}X{valveSize}SSRB", 1);
			}
			else // Poly
			{
				if (valveSize != PartSize.SIZE_100) throw new NotSupportedException("Only 1\" valves supported with poly");
				if (mainSize> PartSize.SIZE_125) throw new NotSupportedException("Only pipe under 150 supported");

				if (mainSize == PartSize.SIZE_075)
				{
					parts.Add($"FI{valveSize}X{mainSize}TTRB", 1);
					parts.Add($"NI{mainSize}X4", 1);
				}
				else
				{
					parts.Add($"NI{valveSize}X4", 1);
				}

				if (pipeCount == 2)
				{
					parts.Add($"PF{mainSize}BBTTEE", 1);
					parts.AddHoseClamp(mainSize.Code, 2);
				}
				else
				{
					parts.Add($"PF{mainSize}BT90", 1);
					parts.AddHoseClamp(mainSize.Code, 1);
				}

				if (mainSize > valveSize) parts.Add($"FI{mainSize}X{valveSize}TTRB", 1);
			}
		}

		/// <summary>
		/// Gets the outlet parts of a valve in a manifold configuration
		/// </summary>
		public static void AddOutletManifoldParts(CodedParts parts, string outletThread, PartSize pipeSize, PartSize valveSize, SystemType systemType)
		{
			if (outletThread == "FIPT")
			{
				// Add male transition nipple out of valve 
				var transitionSize = valveSize == PartSize.SIZE_100 ? string.Empty : valveSize.Code;
				parts.Add($"AF18011{transitionSize}", 1);

				if (systemType == SystemType.PVC)
				{
					// Add female manifold to glue adapter
					var slipAdapterCode = valveSize == PartSize.SIZE_100 && pipeSize == PartSize.SIZE_075 ? 3 : 2;
					parts.Add($"AF1801{slipAdapterCode}{transitionSize}", 1);

					// Add coupler and bushing if needed
					if (valveSize == PartSize.SIZE_100)
					{
						if (pipeSize == PartSize.SIZE_125)
						{
							parts.Add($"FI{PartSize.SIZE_125.Code}SSCOUP", 1);
						}
					}
					else if (valveSize == PartSize.SIZE_150)
					{
						if (pipeSize < PartSize.SIZE_100)
						{
							parts.Add($"FI{PartSize.SIZE_100.Code}X{pipeSize.Code}SSRB", 1);
						}
						else if (pipeSize == PartSize.SIZE_125)
						{
							parts.Add($"FI{PartSize.SIZE_150.Code}SSCOUP", 1);
							parts.Add($"FI{PartSize.SIZE_150.Code}X{PartSize.SIZE_125.Code}SSRB", 1);
						}
						else if (pipeSize == PartSize.SIZE_150)
						{
							parts.Add($"FI{PartSize.SIZE_150.Code}SSCOUP", 1);
						}
					}
					else if (valveSize == PartSize.SIZE_200)
					{
						if (pipeSize < PartSize.SIZE_150)
						{
							parts.Add($"FI{PartSize.SIZE_150.Code}X{pipeSize.Code}SSRB", 1);
						}
						else if (pipeSize == PartSize.SIZE_200)
						{
							parts.Add($"FI{PartSize.SIZE_200.Code}SSCOUP", 1);
						}
					}
				}
				else // Poly
				{
					if (valveSize != PartSize.SIZE_100)
					{
						throw new NotSupportedException();
					}
					else
					{
						var insertCode = pipeSize == PartSize.SIZE_075 ? "15" : (pipeSize == PartSize.SIZE_100 ? "14" : "18");

						// Add insert adapter
						parts.Add($"AF180{insertCode}", 1);
						parts.AddHoseClamp(pipeSize.Code, 1);
					}
				}
			}
			else // MIPT
			{
				if (valveSize != PartSize.SIZE_100)
				{
					throw new NotSupportedException();
				}
				else
				{
					// Add female adapter to male manifold transition nipple out of valve
					parts.Add($"AF18017", 1);

					if (systemType == SystemType.PVC)
					{
						// Add slip adapter
						if (pipeSize == PartSize.SIZE_075)
						{
							parts.Add($"AF18013", 1);
						}
						else
						{
							parts.Add($"AF18012", 1);
						}

						if (pipeSize == PartSize.SIZE_125)
						{
							parts.Add($"FI{PartSize.SIZE_125.Code}SSCOUP", 1);
						}
					}
					else // Poly
					{
						var insertCode = pipeSize == PartSize.SIZE_075 ? "15" : (pipeSize == PartSize.SIZE_100 ? "14" : "18");

						// Add insert adapter
						parts.Add($"AF180{insertCode}", 1);
						parts.AddHoseClamp(pipeSize.Code, 1);
					}
				}
			}
		}

		/// <summary>
		/// Gets the inlet parts of a valve for a manifold configuration
		/// </summary>
		public static void AddInletParts(CodedParts parts, string inletThreads, PartSize valveSize, Manifold manifold)
		{
			var manifoldSize = manifold.Size;

			if (inletThreads == "FIPT")
			{
				if (manifoldSize == valveSize)
				{
					var size = manifoldSize == PartSize.SIZE_100 ? "" : manifoldSize.Code;

					// Female manifold to male adapter
					parts.Add($"AF18010{size}", 1);
				}
				else if (manifoldSize > valveSize)
				{
					// Add toe nipple out of valve
					parts.Add($"NI{valveSize.Code}X4TOE", 1);

					// Add slip adapter from manifold to toe nipple
					parts.Add($"AF18012{manifoldSize.Code}", 1);

					// Add coupler and bushing if needed
					if (manifoldSize == PartSize.SIZE_200)
					{
						if (valveSize == PartSize.SIZE_100)
						{
							parts.Add($"FI{PartSize.SIZE_150.Code}X{PartSize.SIZE_100.Code}SSRB", 1);
						}
					}
				}
			}
			else
			{
				if (valveSize == PartSize.SIZE_100 && manifoldSize == valveSize)
				{
					// Female inlet valve
					parts.Add("AF18016", 1);
				}
				else
				{
					throw new NotImplementedException();
				}
			}
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

			var pipe = Pipes.FindAll(p => p.GetType() == pipeType);

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
					upstreamPipe = Pipes.Find(p => p is MainlinePipe);
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
			return Pipes.FindAll(p => p.GetType() == pipeType)?.Count ?? 0;
		}

		#endregion
	}
}
