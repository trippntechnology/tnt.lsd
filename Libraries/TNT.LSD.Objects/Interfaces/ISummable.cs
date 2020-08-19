namespace TNT.LSD.Objects.Interfaces
{
	/// <summary>
	/// Represents object that has flow that can be summed.
	/// </summary>
	public interface ISummable
	{
		/// <summary>
		/// Gets the GPM of the object represented by this interface
		/// </summary>
		double GPM { get; set; }
	}
}
