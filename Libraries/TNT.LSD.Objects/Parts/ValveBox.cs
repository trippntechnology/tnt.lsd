using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;
using TNT.LSD.Inventory;
using TNT.LSD.Settings;

namespace TNT.LSD.Objects
{
	/// <summary>
	/// Represents a valve box
	/// </summary>
	public class ValveBox : DryPart
	{
		/// <summary>
		/// Valves encompassed by this valve box
		/// </summary>
		[Browsable(false)]
		[XmlIgnore()]
		public List<Valve> AssociatedValves { get; set; }

		/// <summary>
		/// Indicates whether or not to use a manifold
		/// </summary>
		[Description("Use manifold fittings for the valves located within this valve box")]
		[DisplayName("Use Manifold")]
		[DefaultValue(true)]
		public bool UseManifold { get; set; }

		/// <summary>
		/// Maximum number of 1" valves that can fit in this valve box
		/// </summary>
		[Description("Indicates the maximum number of valves that can fit in this valve box")]
		[DisplayName("Maximum Valve Quantity")]
#if !PALETTE_PROPERTIES
		[ReadOnly(true)]
#endif
		public int MaximumValveQuantity { get; set; }

		/// <summary>
		/// Number of bags of gravel to include with the valve box
		/// </summary>
		[Description("Indicates the number of bags of gravel that should be included with valve box")]
		[DisplayName("Bags of Gravel")]
#if !PALETTE_PROPERTIES
		[ReadOnly(true)]
#endif
		public float BagsOfGravel { get; set; }

		private int ManifoldCount
		{
			get
			{
				var size = 0;
				if (UseManifold && AssociatedValves != null)
				{
					size = System.Math.Min(AssociatedValves.Count, MaximumValveQuantity);
				}

				return size;
			}
		}


		/// <summary>
		/// Copy constructor
		/// </summary>
		/// <param name="obj"></param>
		public ValveBox(ValveBox obj)
			: base(obj)
		{
			UseManifold = obj.UseManifold;
			MaximumValveQuantity = obj.MaximumValveQuantity;
			BagsOfGravel = obj.BagsOfGravel;
		}

		/// <summary>
		/// Default constructor
		/// </summary>
		public ValveBox()
			: base()
		{
			UseManifold = true;
			BagsOfGravel = 1F;
		}

		/// <summary>
		/// Clones this <see cref="ValveBox"/>
		/// </summary>
		/// <returns>Copy of this <see cref="ValveBox"/></returns>
		public override TNTObject Clone()
		{
			return new ValveBox(this);
		}

		/// <summary>
		/// Creates undo copy
		/// </summary>
		/// <returns>Undo copy</returns>
		public override TNTObject CreateUndoCopy()
		{
			ValveBox newObj = base.CreateUndoCopy() as ValveBox;

			newObj.UseManifold = UseManifold;
			newObj.MaximumValveQuantity = MaximumValveQuantity;
			newObj.BagsOfGravel = BagsOfGravel;

			return newObj;
		}

		/// <summary>
		/// Assigned obj's properties to this object
		/// </summary>
		/// <param name="obj">Source object</param>
		public override void Assign(TNTObject obj)
		{
			base.Assign(obj);

			ValveBox vb = obj as ValveBox;

			if (vb != null)
			{
				UseManifold = vb.UseManifold;
				MaximumValveQuantity = vb.MaximumValveQuantity;
				BagsOfGravel = vb.BagsOfGravel;
			}
		}

		/// <summary>
		/// Adds PVC manifold adapter to the main line
		/// </summary>
		public static void AddPVCMainLineAdapter(CodedParts parts, PartSize mainlineSize, Manifold manifold)
		{
			// Glue adapter from manifold inlet to main
			if (manifold.Size == PartSize.SIZE_100 && mainlineSize == PartSize.SIZE_075)
			{
				parts.Add($"AF18013", 1);
			}
			else if (manifold.Size == PartSize.SIZE_100)
			{
				parts.Add($"AF18012", 1);
				parts.Add($"FI{mainlineSize}SSCOUP", mainlineSize == PartSize.SIZE_125 ? 1 : 0);
			}
			else if (manifold.Size == PartSize.SIZE_150)
			{
				parts.Add($"AF18012150", 1);
				parts.Add($"FI150SSCOUP", mainlineSize >= PartSize.SIZE_125 ? 1 : 0);
				parts.Add($"FI100X075SSRB", mainlineSize == PartSize.SIZE_075 ? 1 : 0);
				parts.Add($"FI150X125SSRB", mainlineSize == PartSize.SIZE_125 ? 1 : 0);
			}
			else if (manifold.Size == PartSize.SIZE_200)
			{
				parts.Add($"AF18012{PartSize.SIZE_200}", 1);
				parts.Add($"FI200SSCOUP", mainlineSize == PartSize.SIZE_200 ? 1 : 0);
				parts.Add($"FI150X{mainlineSize.Code}SSRB", mainlineSize < PartSize.SIZE_150 ? 1 : 0);
			}
		}


		/// <summary>
		/// Adds PVC manifold adapter to the main line
		/// </summary>
		public static void AddPOLYMainLineAdapter(CodedParts parts, PartSize mainlineSize, Manifold manifold)
		{
			if (manifold.Size != PartSize.SIZE_100) throw new NotSupportedException("Poly only supports 100 manifold");
			if (mainlineSize > PartSize.SIZE_125) throw new NotSupportedException("Poly only supporst pipe sizes less than 150");

			var part = mainlineSize == PartSize.SIZE_075 ? "AF18015" : (mainlineSize == PartSize.SIZE_100 ? "AF18014" : "AF18018");

			parts.Add(part, 1);
			parts.AddHoseClamp(mainlineSize.Code, 1); ;
		}

		/// <summary>
		/// Adds manifold cap
		/// </summary>
		public static void AddManifoldCap(CodedParts parts, PartSize mainlineSize, Manifold manifold)
		{
			if (manifold.Size == PartSize.SIZE_100)
			{
				// Manifold cap
				parts.Add($"AF18000", 1);
			}
			else
			{
				// Adapter with cap
				parts.Add($"AF18012{manifold.Size.Code}", 1);
				parts.Add($"FI{mainlineSize.Code}SCAP", 1);
			}
		}

		/// <summary>
		/// Updates the <paramref name="parts"/> with the quantities associated with this valve box
		/// </summary>
		public override void SetPartQuantity(CodedParts parts, SystemType systemType)
		{
			base.SetPartQuantity(parts, systemType);

			parts.Add("GRAVEL", BagsOfGravel);

			var manifold = GetManifold();

			if (manifold != null)
			{
				var mainlineMaxSize = GetMainlineMaxSize();
				var mainlineMinSize = GetMainlineMinSize();
				var code = manifold.Size == PartSize.SIZE_100 ? string.Empty : manifold.Size.Code;

				Debug.WriteLine($"mainlineMaxSize: {mainlineMaxSize}");
				Debug.WriteLine($"mainlineMinSize: {mainlineMinSize}");

				// Add manifold
				parts.Add($"AF1800{manifold.Count}{code}", 1);

				//Inlet
				if (systemType == SystemType.PVC)
				{
					AddPVCMainLineAdapter(parts, mainlineMaxSize, manifold);
				}
				else
				{
					AddPOLYMainLineAdapter(parts, mainlineMaxSize, manifold);
				}

				if (Terminates())
				{
					AddManifoldCap(parts, mainlineMaxSize, manifold);
				}
				else
				{
					// Outlet
					if (systemType == SystemType.PVC)
					{
						AddPVCMainLineAdapter(parts, mainlineMinSize, manifold);
					}
					else
					{
						AddPOLYMainLineAdapter(parts, mainlineMinSize, manifold);
					}
				}
			}
		}

		/// <summary>
		/// Indicates whether the main line terminates here
		/// </summary>
		private bool Terminates() => AssociatedValves.Find(v => v.GetPipeCount(typeof(MainlinePipe)) == 1) != null;

		/// <summary>
		/// Gets a <see cref="Manifold"/> that is sized to the mainline size
		/// </summary>
		public Manifold GetManifold()
		{
			Manifold manifold = null;

			if (UseManifold && AssociatedValves != null && AssociatedValves.Count > 0)
			{
				PartSize manifoldSize = null;
				try
				{
					manifoldSize = GetManifoldSize();
				}
				catch (Exception ex)
				{
					System.Diagnostics.Debug.WriteLine(ex.Message);
					return null;
				}

				if (ManifoldCount != 0)
				{
					manifold = Manifold.GetManifold(ManifoldCount, manifoldSize);
				}
			}

			return manifold;
		}

		/// <summary>
		/// Gets the <see cref="PartSize"/> of the manifold. It will match the mainline size.
		/// </summary>
		private PartSize GetManifoldSize()
		{
			// Get the largest index of the mainline pipe
			var mainlinePipeSize = GetMainlineMaxSize();
			var manifoldSize = PartSize.SIZE_100;

			if (ManifoldCount > 2 && mainlinePipeSize.Index > 3)
			{
				throw new Exception("Manifold does not exist for this configuration");
			}
			else
			{
				switch (mainlinePipeSize.Index)
				{
					case 4:
						manifoldSize = PartSize.SIZE_150;
						break;
					case 5:
						manifoldSize = PartSize.SIZE_200;
						break;
				}
			}
			return manifoldSize;
		}

		/// <summary>
		/// Gets the <see cref="PartSize"/> that represents the maximum size of the main line within this valve box
		/// </summary>
		private PartSize GetMainlineMaxSize()
		{
			// Get the largest index of the inlet pipe
			int index = PartSize.SIZE_050.Index - 1;
			foreach (var valve in AssociatedValves)
			{
				valve.Pipes.FindAll(p => p is MainlinePipe).ForEach(pipe =>
				{
					index = System.Math.Max(index, pipe.PipeSizeIndex);
				});
			}

			return PartSize.GetSize(index);
		}

		/// <summary>
		/// Gets the <see cref="PartSize"/> that represents the minimum size of the main line within this valve box
		/// </summary>
		private PartSize GetMainlineMinSize()
		{
			int index = PartSize.SIZE_200.Index + 1;
			foreach (var valve in AssociatedValves)
			{
				valve.Pipes.FindAll(p => p is MainlinePipe).ForEach(pipe =>
				{
					index = System.Math.Min(index, pipe.PipeSizeIndex);
				});
			}

			return PartSize.GetSize(index);
		}
	}
}