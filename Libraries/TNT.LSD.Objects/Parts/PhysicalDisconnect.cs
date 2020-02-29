using System;
using TNT.LSD.Inventory;
using TNT.LSD.Settings;

namespace TNT.LSD.Objects
{
	/// <summary>
	/// Represents a physical disconnect
	/// </summary>
	public class PhysicalDisconnect : MainlinePart
	{
		#region Constructors

		/// <summary>
		/// Copy constructor
		/// </summary>
		public PhysicalDisconnect(PhysicalDisconnect obj)
			: base(obj)
		{
		}

		/// <summary>
		/// Default constructor
		/// </summary>
		public PhysicalDisconnect()
			: base()
		{
		}

		#endregion

		#region Overrides

		/// <summary>
		/// Clones this <see cref="PhysicalDisconnect"/>
		/// </summary>
		public override TNTObject Clone() => new PhysicalDisconnect(this);

		/// <summary>
		/// Indicates whether a pipce can be connected to this <see cref="PhysicalDisconnect"/>
		/// </summary>
		public override bool CanAddPipe(Type pipeType, out string reason)
		{
			if (Pipes.Count > 2)
			{
				reason = "Only three pipe connections allowed";
				return false;
			}

			return base.CanAddPipe(pipeType, out reason);
		}

		/// <summary>
		/// Sets parts associated with the physical disconnect
		/// </summary>
		/// <param name="parts"></param>
		/// <param name="systemType"></param>
		public override void SetPartQuantity(CodedParts parts, SystemType systemType)
		{
			parts.Add("PD2", 1);
			parts.Add("VBJUMBO", 1);
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
				Pipes.ForEach(p =>
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
