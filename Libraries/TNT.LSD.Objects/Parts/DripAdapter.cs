using System;
using System.ComponentModel;
using System.Drawing;
using TNT.LSD.Inventory;

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
		[Description("Type of pipe stubbed to the surface")]
		public StubType Stub { get; set; }

		#region Constructors

		public DripAdapter(Point position, String fileName)
			: base(position, fileName)
		{
		}

		// Copy constructor
		public DripAdapter(DripAdapter obj)
			: base(obj)
		{
		}

		public DripAdapter()
			: base()
		{
		}

		#endregion

		#region Overrides

		public override TNTObject Clone()
		{
			return new DripAdapter(this);
		}

		public override void SetPartQuantity(System.Collections.Generic.Dictionary<string, TNT.LSD.Inventory.Part> parts)
		{
			base.SetPartQuantity(parts);

			if (m_Pipes == null || m_Pipes.Count < 1)
			{
				return;
			}

			string pipeCode = SIZE_CODE[GetMaxPipeSizeIndex()];
			string outletCode = SIZE_CODE[0];

			#region Fitting

			string fittingCode = string.Empty;

			if (m_Pipes.Count == 1)
			{
				// Need to add elbow
				if (this.Stub == StubType.PVC)
				{
					fittingCode = string.Format("FI{0}SS90", pipeCode);
				}
				else if (this.Stub == StubType.FunnyPipe)
				{
					if (pipeCode == outletCode)
					{
						fittingCode = string.Format("FI{0}ST90", pipeCode);
					}
					else
					{
						fittingCode = string.Format("FI{0}X{1}ST90", pipeCode, outletCode);
					}
				}
			}
			else
			{
				// Need to add tee
				if (this.Stub == StubType.PVC)
				{
					fittingCode = string.Format("FI{0}SSSTEE", pipeCode);
				}
				else if (this.Stub == StubType.FunnyPipe)
				{
					if (pipeCode == outletCode)
					{
						fittingCode = string.Format("FI{0}SSTTEE", pipeCode);
					}
					else
					{
						fittingCode = string.Format("FI{0}X{1}SSTTEE", pipeCode, outletCode);
					}
				}
			}

			if (!parts.ContainsKey(fittingCode))
			{
				parts.Add(fittingCode, new Part() { Code = fittingCode });
			}

			parts[fittingCode].Quantity += 1;

			#endregion

			#region Funny Pipe/pvc

			if (this.Stub == StubType.PVC)
			{
				parts[$"PI{pipeCode}"].Quantity += 1;
				parts[$"FI{pipeCode}SCAP"].Quantity += 1;
			}
			else if (this.Stub == StubType.FunnyPipe)
			{
				parts["FPSBE050"].Quantity += 1;
				parts["FUNNYPIPE"].Quantity += 1;
			}

			#endregion
		}

		#endregion
	}
}
