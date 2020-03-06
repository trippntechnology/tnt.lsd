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
			var pipeSize = GetMaxPipeSize();

			if (systemType == SystemType.PVC)
			{
				SetPVCPartQuantities(parts, pipeSize);
			}
			else
			{
				SetPOLYPartQuantities(parts, pipeSize);
			}
		}

		private void SetPOLYPartQuantities(CodedParts parts, PartSize pipeSize)
		{
			if (pipeSize == PartSize.SIZE_125)
			{
				parts.Add("PD2", 1);
				parts.Add("FI100STFA", 1);
				parts.Add("BV100FT", 2);
				parts.Add("PD100CTADAPTER", 2);
				parts.Add("PD100CAMCAP", 1);
				parts.Add("FI125SSCOUP", 3);
				parts.Add("FI125X100SSRB", 3);
				parts.Add("NI100X4TOE", 2);
				parts.Add("VBJUMBO", 1);
			}
			else // 075 & 100
			{
				parts.Add("PD2", 1);
				parts.Add("FI100TSMA", 2);
				parts.Add("FI100STFA", 1);
				parts.Add("BV100FT", 2);
				parts.Add("PD100CTADAPTER", 2);
				parts.Add("PD100CAMCAP", 1);
				parts.Add("VBJUMBO", 1);

				if (pipeSize == PartSize.SIZE_075) parts.Add("FI100X075SSRB", 1);
			}
		}

		private void SetPVCPartQuantities(CodedParts parts, PartSize pipeSize)
		{
			if (pipeSize == PartSize.SIZE_125)
			{
				parts.Add("PD2", 1);
				parts.Add("FI100STFA", 1);
				parts.Add("BV100FT", 2);
				parts.Add("PD100CTADAPTER", 2);
				parts.Add("PD100CAMCAP", 1);
				parts.Add("FI125SSCOUP", 3);
				parts.Add("FI125X100SSRB", 3);
				parts.Add("NI100X4TOE", 2);
				parts.Add("VBJUMBO", 1);
			}
			else if (pipeSize == PartSize.SIZE_150)
			{
				parts.Add("PD150HOSE", 2);
				parts.Add("FI150TSMA", 1);
				parts.Add("PD150IFCAMLOCK", 1);
				parts.Add("PD150MMCAMLOCK", 2);
				parts.Add("PD150CAMCAP", 1);
				parts.Add("BV150BRONZE", 2);
				parts.Add("NI150X4TOE", 2);
				parts.Add("FI150SSCOUP", 2);
				parts.Add("FI150STFASCH80", 1);
				parts.Add("VBJUMBO", 1);
			}
			else if (pipeSize == PartSize.SIZE_200)
			{
				parts.Add("PD200HOSE", 2);
				parts.Add("PF200BTMA", 1);
				parts.Add("PD200IFCAMLOCK", 1);
				parts.Add("PD200MMCAMLOCK", 2);
				parts.Add("PD200CAMCAP", 1);
				parts.Add("BV200BRONZE", 2);
				parts.Add("NI200X4TOE", 2);
				parts.Add("FI200SSCOUP", 2);
				parts.Add("FI200STFA", 1);
				parts.Add("VBGIANT", 1);
			}
			else // 075 & 100
			{
				parts.Add("PD2", 1);
				parts.Add("FI100TSMA", 2);
				parts.Add("FI100STFA", 1);
				parts.Add("BV100FT", 2);
				parts.Add("PD100CTADAPTER", 2);
				parts.Add("PD100CAMCAP", 1);
				parts.Add("VBJUMBO", 1);

				if (pipeSize == PartSize.SIZE_075) parts.Add("FI100X075SSRB", 3);
			}
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
