
namespace TNT.LSD.Objects
{
	/// <summary>
	/// Represents main line pipe
	/// </summary>
	public class MainlinePipe : Pipe
	{
		#region Constructors

		/// <summary>
		/// Constructor
		/// </summary>
		public MainlinePipe()
			: base()
		{
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="part1">First part of the pipe segment</param>
		/// <param name="part2">Second part of the pipe segment</param>
		public MainlinePipe(TNTPart part1, TNTPart part2)
			: base(part1, part2)
		{
		}

		/// <summary>
		/// Copy constructor
		/// </summary>
		/// <param name="obj">Object to copy</param>
		public MainlinePipe(MainlinePipe obj)
			: base(obj)
		{
		}

		public override TNTObject Clone()
		{
			return new MainlinePipe(this);
		}

		#endregion

		/// <summary>
		/// Checks if this pipe segement, beginning at origPart, leads to a source. This
		/// was neccessary so that pipe sizing from physical disconnects only followed paths to valves.
		/// </summary>
		/// <param name="origPart">Origination part</param>
		/// <returns>True if path leads to source, false otherwise.</returns>
		virtual public bool LeadsToSource(TNTPart origPart)
		{
			bool leadsToSource = false;
			TNTPart destPart = GetOtherPart(origPart);

			if (destPart.GetType() == typeof(TNTSource))
			{
				// Source was reached return true
				leadsToSource = true;
			}
			else
			{
				// Get pipe segments that leave origPart excluding this
				var pipes = destPart.Pipes.FindAll(p => p != this && p is MainlinePipe);

				foreach (MainlinePipe pipe in pipes)
				{
					leadsToSource = pipe.LeadsToSource(destPart);

					if (leadsToSource)
					{
						break;
					}
				}
			}

			return leadsToSource;
		}
	}
}
