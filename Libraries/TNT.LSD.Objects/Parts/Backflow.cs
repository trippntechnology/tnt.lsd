using System.Collections.Generic;
using System.ComponentModel;
using TNT.LSD.Settings;

namespace TNT.LSD.Objects
{
	public class Backflow : MainlinePart
	{
		#region Properties

#if !PALETTE_PROPERTIES
		[Browsable(false)]
#endif
		public string Code { get; set; }

		#endregion

		#region Constructors

		public Backflow()
			: base()
		{
		}

		public Backflow(Backflow obj)
			: base(obj)
		{
			Code = obj.Code;
		}

		#endregion

		#region Overrides

		public override TNTObject Clone()
		{
			return new Backflow(this);
		}

		public override void SetPartQuantity(Dictionary<string, TNT.LSD.Inventory.Part> parts, SystemType systemType)
		{
			int pipeSizeIndex = Pipes != null && Pipes.Count > 0 ? m_PipeSizeList.IndexOf(Pipes[0].PipeSize) : 2;

			// This condition is in place so the an 1" backflow is figured for 1-1/4" mainline
			string pipeSizeCode = SIZE_CODE[pipeSizeIndex == 3 ? pipeSizeIndex - 1 : pipeSizeIndex];

			GetPart(parts, string.Format(Code, pipeSizeCode)).Quantity += 1;
			GetPart(parts, string.Format("FI{0}SSCOUP", SIZE_CODE[pipeSizeIndex])).Quantity += 2;

			if (pipeSizeIndex == 3)
			{
				GetPart(parts, string.Format("FI{0}X{1}SSRB", SIZE_CODE[pipeSizeIndex], pipeSizeCode)).Quantity += 2;
			}

			GetPart(parts, string.Format("NI{0}X4TOE", pipeSizeCode)).Quantity += 2;
			GetPart(parts, string.Format("GN{0}90", pipeSizeCode)).Quantity += 2;
			GetPart(parts, string.Format("GN{0}X18", pipeSizeCode)).Quantity += 2;
			GetPart(parts, string.Format("GN{0}UNION", pipeSizeCode)).Quantity += 2;
			GetPart(parts, string.Format("GN{0}X4", pipeSizeCode)).Quantity += 2;
			GetPart(parts, string.Format("GN{0}STR90", pipeSizeCode)).Quantity += 2;

			base.SetPartQuantity(parts, systemType);
		}

		#endregion
	}
}
