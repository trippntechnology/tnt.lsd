using TNT.LSD.Inventory;

namespace TNT.LSD.Plugins
{
	/// <summary>
	/// Used to obtain the Description value from the part item when displayed within
	/// a ComboBox
	/// </summary>
	public class DescriptionItem : ComboBoxItem
	{
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="part">Part associated with the item</param>
		public DescriptionItem(Part part)
			: base(part)
		{
		}

		/// <summary>
		/// Returns the Part's description
		/// </summary>
		/// <returns>Part's description</returns>
		public override string ToString()
		{
			return Part.Description;
		}
	}
}
