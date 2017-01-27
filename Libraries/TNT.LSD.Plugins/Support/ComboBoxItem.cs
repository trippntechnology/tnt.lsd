using TNT.LSD.Inventory;

namespace TNT.LSD.Plugins
{
	// Represents an item in a ComboBox
	public class ComboBoxItem
	{
		/// <summary>
		/// Part associated with the item
		/// </summary>
		public Part Part { get; protected set; }

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="part">Part associated with the item</param>
		public ComboBoxItem(Part part)
		{
			Part = part;
		}
	}
}
