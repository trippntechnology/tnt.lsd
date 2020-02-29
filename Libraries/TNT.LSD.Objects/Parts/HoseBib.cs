using System;
using System.Collections.Generic;
using TNT.LSD.Inventory;

namespace TNT.LSD.Objects
{
	public class HoseBib : IndirectMainlinePart
	{
		#region Constructors

		public HoseBib(HoseBib obj)
			: base(obj)
		{
		}

		public HoseBib()
			: base()
		{
			// To maintain backward compatibility for hose bibs in previous layouts, set the fitting thread size to 3/4"
			FittingThreadSize = "3/4\"";
		}

		#endregion

		#region Overrides
		public override bool CanAddPipe(Type pipeType, out string reason)
		{
			Pipe pipe = Pipes.Find(p => p.GetType() != pipeType);

			if (pipe != null)
			{
				reason = "Part must be connected to same pipe types";
				return false;
			}

			if (Pipes.Count > 1)
			{
				reason = "Only two pipe connections allowed";
				return false;
			}

			reason = string.Empty;
			return true;
		}

		public override TNTObject Clone()
		{
			return new HoseBib(this);
		}

		#endregion
	}
}
