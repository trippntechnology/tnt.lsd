using System;
using System.Drawing;

namespace TNT.LSD.Objects
{
	/// <summary>
	/// Represents a part that can only be on the main line
	/// </summary>
	public abstract class MainlinePart : PalettePart
	{
		#region Constructors

		// Copy constructor
		public MainlinePart(MainlinePart obj)
			: base(obj)
		{
		}

		public MainlinePart()
			: base()
		{
		}

		#endregion

		#region Overrides

		/// <summary>
		/// Specifies if the pipe type can be added to this part
		/// </summary>
		/// <param name="pipeType">Pipe's type</param>
		/// <param name="reason">When false, reason pipe can not be added</param>
		/// <returns>True if pipe type can be added, false otherwise</returns>
		public override bool CanAddPipe(Type pipeType, out string reason)
		{
			if (pipeType != typeof(MainlinePipe))
			{
				reason = "Part can only be connected using mainline pipe";
				return false;
			}
			else if (!(this is PhysicalDisconnect) && Pipes.Count > 1)
			{
				reason = "Only two pipe connections allowed";
				return false;
			}

			return base.CanAddPipe(pipeType, out reason);
		}

		public override double SizePipe(Type pipeType, Pipe upstreamPipe)
		{
			RequiredFlow = 0;
			RequiredFlow = base.SizePipe(pipeType, upstreamPipe);
			return RequiredFlow;
		}

		public override bool TerminatePipe()
		{
			if (Pipes.Count > 1)
			{
				return true;
			}

			return base.TerminatePipe();
		}

		#endregion
	}
}
