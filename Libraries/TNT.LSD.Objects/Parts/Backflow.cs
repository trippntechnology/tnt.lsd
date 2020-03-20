using System.ComponentModel;
using TNT.LSD.Inventory;
using TNT.LSD.Settings;

namespace TNT.LSD.Objects
{
	/// <summary>
	/// Represents a backflow valve
	/// </summary>
	public class Backflow : MainlinePart
	{
		#region Properties

		/// <summary>
		/// Generic (without size code) code
		/// </summary>
#if !PALETTE_PROPERTIES
		[Browsable(false)]
#endif
		public string Code { get; set; }

		#endregion

		#region Constructors

		/// <summary>
		/// Default constructor
		/// </summary>
		public Backflow()
			: base()
		{
		}

		/// <summary>
		/// Copy constructor
		/// </summary>
		public Backflow(Backflow obj)
			: base(obj)
		{
			Code = obj.Code;
		}

		#endregion

		#region Overrides

		/// <summary>
		/// Clones this <see cref="Backflow"/>
		/// </summary>
		public override TNTObject Clone() => new Backflow(this);

		private void AddAdapterToPVC(CodedParts parts, PartSize partSize, PartSize pipeSize)
		{
			parts.Add($"NI{partSize}X4TOE", 1);

			if (pipeSize == partSize)
			{
				parts.Add($"FI{pipeSize}SSCOUP", 1);
			}
			else if (partSize > pipeSize)
			{
				parts.Add($"FI{partSize}SSCOUP", 1);
				parts.Add($"FI{partSize}X{pipeSize}SSRB", 1);
			}
			else
			{
				parts.Add($"FI{pipeSize}SSCOUP", 1);
				parts.Add($"FI{pipeSize}X{partSize}SSRB", 1);
			}
		}

		private void AddAdapterToPoly(CodedParts parts, PartSize partSize, PartSize pipeSize)
		{
			var hcCode = pipeSize == PartSize.SIZE_125 ? PartSize.SIZE_150 : pipeSize;

			if (partSize == pipeSize)
			{
				parts.Add($"PF{pipeSize}BTMA", 1);
			}
			else if (partSize < pipeSize)
			{
				parts.Add($"NI{partSize}X4", 1);
				parts.Add($"FI{pipeSize}X{partSize}TTRB", 1);
				parts.Add($"PF{pipeSize}BTFA", 1);
			}
			else
			{
				parts.Add($"FI{partSize}X{pipeSize}TTRB", 1);
				parts.Add($"PF{pipeSize}BTMA", 1);
			}

			parts.AddHoseClamp(hcCode.Code, 1);
		}

		/// <summary>
		/// Gets the backflow parts
		/// </summary>
		public override void SetPartQuantity(CodedParts parts, SystemType systemType)
		{
			if (Pipes == null || Pipes.Count < 2) return;

			var pipeSize1 = PartSize.GetSize(Pipes[0].PipeSizeIndex);
			var pipeSize2 = PartSize.GetSize(Pipes[1].PipeSizeIndex);
			var maxPipeSize = pipeSize1 > pipeSize2 ? pipeSize1 : pipeSize2;

			// This condition is in place so that a 1" backflow is figured for 1-1/4" mainline
			var partSize = maxPipeSize == PartSize.SIZE_125 ? PartSize.SIZE_100 : maxPipeSize;

			// Add BF
			parts.Add(string.Format(Code, partSize), 1);

			// Add galvanized fittings
			parts.Add($"GN{partSize}STR90", 2);
			parts.Add($"GN{partSize}X4", 2);
			parts.Add($"GN{partSize}UNION", 2);
			parts.Add($"GN{partSize}X18", 2);
			parts.Add($"GN{partSize}90", 2);

			if (systemType == SystemType.POLY)
			{
				AddAdapterToPoly(parts, partSize, pipeSize1);
				AddAdapterToPoly(parts, partSize, pipeSize2);
			}
			else
			{
				AddAdapterToPVC(parts, partSize, pipeSize1);
				AddAdapterToPVC(parts, partSize, pipeSize2);
			}
		}

		#endregion
	}
}
