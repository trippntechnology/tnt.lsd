using System;
using TNT.LSD.Settings;

namespace TNT.LSD.Objects
{
	public class PhysicalDisconnect : MainlinePart
	{
		#region Constructors

		public PhysicalDisconnect(PhysicalDisconnect obj)
			: base(obj)
		{
		}

		public PhysicalDisconnect()
			: base()
		{
		}

		#endregion

		#region Overrides

		public override TNTObject Clone()
		{
			return new PhysicalDisconnect(this);
		}

		public override bool CanAddPipe(Type pipeType, out string reason)
		{
			if (Pipes.Count > 2)
			{
				reason = "Only three pipe connections allowed";
				return false;
			}

			return base.CanAddPipe(pipeType, out reason);
		}

		public override void SetPartQuantity(System.Collections.Generic.Dictionary<string, TNT.LSD.Inventory.Part> parts, SystemType systemType)
		{
			GetPart(parts, "PD2").Quantity += 1;
			GetPart(parts, "VBJUMBO").Quantity += 1;

			base.SetPartQuantity(parts, systemType);
		}

		/// <summary>
		/// This has to be handled differently so that sizing doesn't occur from the PD
		/// to another source.
		/// </summary>
		/// <param name="upstreamPipe">Origination pipe segment</param>
		/// <returns>GPM required for this pipe onward</returns>
		public override double SizePipe(Pipe upstreamPipe)
		{
			if (!Sized)
			{
				m_Pipes.ForEach(p =>
				{
					if (upstreamPipe != null && upstreamPipe == p)
					{
						// Ignore this pipe
						return;
					}

					// Since this is a mainline part only mainline pipes can exist
					MainlinePipe mainPipe = p as MainlinePipe;

					// Check if this pipe segment leads to another source
					if (mainPipe.LeadsToSource(this))
					{
						// Ignore this pipe
						return;
					}

					// Get the downstream part for this pipe segment
					TNTPart other = p.GetOtherPart(this);

					RequiredFlow = System.Math.Max(other.SizePipe(p), RequiredFlow);
				});
			}

			Sized = true;

			// Size the upstream pipe to handle the required flow
			if (upstreamPipe != null)
			{
				upstreamPipe.SizeFor(RequiredFlow);
			}

			return RequiredFlow;
		}

		#endregion
	}
}
