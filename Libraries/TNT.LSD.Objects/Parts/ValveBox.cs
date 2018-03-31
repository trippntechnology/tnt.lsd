using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using TNT.LSD.Inventory;

namespace TNT.LSD.Objects
{
	public class ValveBox : DryPart
	{
		#region Properties

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

		#endregion

		#region Constructors

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

		#endregion

		#region Overrides

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

		public override void SetPartQuantity(Dictionary<string, Part> parts)
		{
			base.SetPartQuantity(parts);

			if (UseManifold && AssociatedValves != null && AssociatedValves.Count > 0)
			{
				// Add manifold
				parts[string.Format("AF1800{0}", System.Math.Min(AssociatedValves.Count, MaximumValveQuantity))].Quantity += 1;

				// Get the largest index of the inlet pipe
				int pipeSizeIndex = -1;

				foreach (Valve v in AssociatedValves)
				{
					Pipe main = v.Pipes.Find(p => p is MainlinePipe);

					if (main != null)
					{
						pipeSizeIndex = System.Math.Max(pipeSizeIndex, m_PipeSizeList.IndexOf(main.PipeSize));
					}
				}

				switch (pipeSizeIndex)
				{
					case 1: // 3/4"
						parts["AF18013"].Quantity += 2;
						parts["AF18000"].Quantity += 1;
						break;
					case 2: // 1"
						parts["AF18012"].Quantity += 2;
						parts["AF18000"].Quantity += 1;
						break;
					case 3: // 1-1/4"
						parts["AF18012"].Quantity += 2;
						parts["FI125SSCOUP"].Quantity += 2;
						parts["AF18000"].Quantity += 1;
						break;
					case 4: // 1-1/2"
						parts["AF18012150"].Quantity += 2;
						parts["FI150SSCOUP"].Quantity += 2;
						parts["AF18000150"].Quantity += 1;
						break;
					case 5: // 2"
						parts["AF18012200"].Quantity += 2;
						parts["FI200SSCOUP"].Quantity += 2;
						parts["AF18000200"].Quantity += 1;
						break;
				}
			}

			parts["GRAVEL"].Quantity += BagsOfGravel;
		}

		#endregion
	}
}
