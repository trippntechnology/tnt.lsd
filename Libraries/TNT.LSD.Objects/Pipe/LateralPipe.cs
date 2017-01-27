
namespace TNT.LSD.Objects
{
	/// <summary>
	/// Represents pipe downstream from the valve
	/// </summary>
	public class LateralPipe : Pipe
	{
		#region Constructors

		/// <summary>
		/// Constructor
		/// </summary>
		public LateralPipe()
			: base()
		{
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="part1">First part of the pipe segment</param>
		/// <param name="part2">Second part of the pipe segment</param>
		public LateralPipe(TNTPart part1, TNTPart part2)
			: base(part1, part2)
		{
		}

		/// <summary>
		/// Copy constructor
		/// </summary>
		/// <param name="obj">Object to copy</param>
		public LateralPipe(LateralPipe obj)
			: base(obj)
		{
		}

		public override TNTObject Clone()
		{
			return new LateralPipe(this);
		}

		#endregion
	}
}
