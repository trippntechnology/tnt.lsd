using System.ComponentModel;
using TNT.LSD.Inventory;
using TNT.LSD.Settings;

namespace TNT.LSD.Objects
{
	/// <summary>
	/// Stub type
	/// </summary>
	public enum StubType { PVC, FunnyPipe }

	/// <summary>
	/// Represents a drip adapter
	/// </summary>
	public class DripAdapter : LateralPart
	{
		/// <summary>
		/// Type of pipe stubbed to the surface
		/// </summary>
		[Description("Type of pipe stubbed to the surface")]
		public StubType Stub { get; set; }

		#region Constructors

		/// <summary>
		/// Copy constructor
		/// </summary>
		/// <param name="obj">Object to copy</param>
		public DripAdapter(DripAdapter obj)
			: base(obj)
		{
			Stub = obj.Stub;
		}

		/// <summary>
		/// Default constructor
		/// </summary>
		public DripAdapter()
			: base()
		{
		}

		#endregion

		#region Overrides

		/// <summary>
		/// Clones this <see cref="DripAdapter"/>
		/// </summary>
		/// <returns></returns>
		public override TNTObject Clone()
		{
			return new DripAdapter(this);
		}

		/// <summary>
		/// Adds the parts needed to stub off the pipe
		/// </summary>
		public override void SetPartQuantity(CodedParts parts, SystemType systemType)
		{
			base.SetPartQuantity(parts, systemType);

			var maxPipeSize = GetMaxPipeSize(typeof(LateralPipe));
			var minPipeSize = m_Pipes.Count > 1 ? GetMinPipeSize(typeof(LateralPipe)) : null;

			// Only add parts if there is a lateral line connected
			if (maxPipeSize == null) return;

			if (Stub == StubType.FunnyPipe)
			{
				if (minPipeSize == null)// && maxPipeSize <= PartSize.SIZE_100)
				{
					var elbowCode = $"FI{maxPipeSize.Code}X{PartSize.SIZE_050.Code}ST90";

					if (!parts.ContainsKey(elbowCode))
					{
						parts.Add(elbowCode, new Part() { Code = elbowCode });
					}

					parts[elbowCode].Quantity += 1;
				}
				else
				{
					parts[$"FI{maxPipeSize.Code}X{PartSize.SIZE_050.Code}SSTTEE"].Quantity += 1;
				}

				parts["FPSBE050"].Quantity += 1;
				parts["FUNNYPIPE"].Quantity += 1;
				parts["HRFIG8"].Quantity += 1;
			}
			else if (Stub == StubType.PVC)
			{
				if (minPipeSize == null)
				{
					parts[$"FI{maxPipeSize.Code}SS90"].Quantity += 1;
				}
				else
				{
					parts[$"FI{maxPipeSize.Code}SSSTEE"].Quantity += 1;

				}

				if (maxPipeSize > PartSize.SIZE_075)
				{
					parts[$"FI{maxPipeSize.Code}X{PartSize.SIZE_075.Code}SSRB"].Quantity += 1;
				}

				parts[$"PI{PartSize.SIZE_075.Code}"].Quantity += 1;
				parts[$"FI{PartSize.SIZE_075.Code}SCAP"].Quantity += 1;
			}
		}

		#endregion
	}
}
