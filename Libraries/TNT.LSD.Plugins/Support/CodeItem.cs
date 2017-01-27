using TNT.LSD.Inventory;

namespace TNT.LSD.Plugins
{
	/// <summary>
	/// Used to obtain the Code value from the part item when displayed within
	/// a ComboBox
	/// </summary>
	public class CodeItem: ComboBoxItem
	{
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="part">Part assocated with the code</param>
		public CodeItem(Part part)
			: base(part)
		{
		}

		/// <summary>
		/// Returns the part's code
		/// </summary>
		/// <returns>Part's code</returns>
		public override string ToString()
		{
			return Part.Code;
		}
	}
}
