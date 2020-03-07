using System;

namespace TNT.LSD.Objects
{
	/// <summary>
	/// Represents a hose connection
	/// </summary>
	public class HoseBib : IndirectMainlinePart
	{
		#region Constructors

		/// <summary>
		/// Copy constructor
		/// </summary>
		/// <param name="obj"></param>
		public HoseBib(HoseBib obj)
			: base(obj)
		{
		}

		/// <summary>
		/// Default constructor
		/// </summary>
		public HoseBib()
			: base()
		{
		}

		#endregion

		/// <summary>
		/// Indicates if a pipe connection can be added
		/// </summary>
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

		/// <summary>
		/// Clones this <see cref="HoseBib"/>
		/// </summary>
		/// <returns></returns>
		public override TNTObject Clone() => new HoseBib(this);

		#endregion
	}
}
