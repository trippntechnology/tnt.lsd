using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Xml.Serialization;
using TNT.LSD.Inventory;
using TNT.LSD.Settings;

namespace TNT.LSD.Objects
{
	public class ValveBox : DryPart
	{
		[Browsable(false)]
		[XmlIgnore()]
		public List<Valve> AssociatedValves { get; set; }

		[Description("Use manifold fittings for the valves located within this valve box")]
		[DisplayName("Use Manifold")]
		[DefaultValue(true)]
		public bool UseManifold { get; set; }

		[Description("Indicates the maximum number of valves that can fit in this valve box")]
		[DisplayName("Maximum Valve Quantity")]
#if !PALETTE_PROPERTIES
		[ReadOnly(true)]
#endif
		public int MaximumValveQuantity { get; set; }

		[Description("Indicates the number of bags of gravel that should be included with valve box")]
		[DisplayName("Bags of Gravel")]
#if !PALETTE_PROPERTIES
		[ReadOnly(true)]
#endif
		public float BagsOfGravel { get; set; }

		public int ManifoldCount
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


		// Copy constructor
		public ValveBox(ValveBox obj)
			: base(obj)
		{
			UseManifold = obj.UseManifold;
			MaximumValveQuantity = obj.MaximumValveQuantity;
			BagsOfGravel = obj.BagsOfGravel;
		}

		public ValveBox()
			: base()
		{
			UseManifold = true;
			BagsOfGravel = 1F;
		}

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

		private void AddMainLineAdapter(Dictionary<string, Part> parts, PartSize mainlineSize, Manifold manifold)
		{
			// Glue adapter from manifold inlet to main
			if (manifold.Size == PartSize.SIZE_100 && mainlineSize == PartSize.SIZE_075)
			{
				parts[$"AF18013"].Quantity += 1;
			}
			else if (manifold.Size == PartSize.SIZE_100)
			{
				parts[$"AF18012"].Quantity += 1;
				parts[$"FI{mainlineSize.Code}SSCOUP"].Quantity += mainlineSize == PartSize.SIZE_125 ? 1 : 0;
			}
			else if (manifold.Size == PartSize.SIZE_150)
			{
				parts[$"AF18012150"].Quantity += 1;
				parts[$"FI150SSCOUP"].Quantity += mainlineSize >= PartSize.SIZE_125 ? 1 : 0;
				parts[$"FI100x075SSRB"].Quantity += mainlineSize == PartSize.SIZE_075 ? 1 : 0;
				parts[$"FI150x125SSRB"].Quantity += mainlineSize == PartSize.SIZE_125 ? 1 : 0;
			}
			else if (manifold.Size == PartSize.SIZE_200)
			{
				parts[$"AF18012{PartSize.SIZE_200}"].Quantity += 1;
				parts[$"FI200SSCOUP"].Quantity += mainlineSize == PartSize.SIZE_200 ? 1 : 0;
				//parts[$"FI100x075SSRB"].Quantity += mainlineSize == PartSize.SIZE_075 ? 1 : 0;
				parts[$"FI150x{mainlineSize.Code}SSRB"].Quantity += mainlineSize <= PartSize.SIZE_125 ? 1 : 0;
			}
		}

		private void AddManifoldCap(Dictionary<string, Part> parts, PartSize mainlineSize, Manifold manifold)
		{
			if (manifold.Size == PartSize.SIZE_100)
			{
				// Manifold cap
				parts[$"AF18000"].Quantity += 1;
			}
			else
			{
				// Adapter with cap
				parts[$"AF18012{manifold.Size.Code}"].Quantity += 1;
				parts[$"FI{mainlineSize.Code}SCAP"].Quantity += 1;
			}
		}

		public override void SetPartQuantity(Dictionary<string, Part> parts, SystemType systemType)
		{
			base.SetPartQuantity(parts, systemType);

			parts["GRAVEL"].Quantity += BagsOfGravel;

			var manifold = GetManifold();

			if (manifold != null)
			{
				var mainlineMaxSize = GetMainlineMaxSize();
				var mainlineMinSize = GetMainlineMinSize();
				var code = manifold.Size == PartSize.SIZE_100 ? string.Empty : manifold.Size.Code;

				Debug.WriteLine($"mainlineMaxSize: {mainlineMaxSize}");
				Debug.WriteLine($"mainlineMinSize: {mainlineMinSize}");

				// Add manifold
				parts[$"AF1800{manifold.Count}{code}"].Quantity += 1;

				//Inlet
				AddMainLineAdapter(parts, mainlineMaxSize, manifold);

				if (Terminates())
				{
					AddManifoldCap(parts, mainlineMaxSize, manifold);
				}
				else
				{
					// Outlet
					AddMainLineAdapter(parts, mainlineMaxSize, manifold);
				}
			}
		}

		/// <summary>
		/// Indicates whether the main line terminates here
		/// </summary>
		/// <returns>True if terminates, false otherwise</returns>
		private bool Terminates()
		{
			return AssociatedValves.Find(v => v.GetPipeCount(typeof(MainlinePipe)) == 1) != null;
		}

		/// <summary>
		/// Gets a <see cref="Manifold"/> that is sized to the mainline size
		/// </summary>
		/// <returns><see cref="Manifold"/> that is sized to the mainline size</returns>
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
		/// <returns><see cref="PartSize"/> of the manifold</returns>
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
		/// Gets the <see cref="PartSize"/> of the mainline
		/// </summary>
		/// <returns><see cref="PartSize"/> of the mainline</returns>
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
