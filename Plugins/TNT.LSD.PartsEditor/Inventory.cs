using System.ComponentModel;
using TNT.LSD.Inventory;

namespace TNT.LSD.PartsEditor
{
	public class Inventory:BindingList<TNT.LSD.Inventory.Part>
	{
		public Inventory(CodedParts codedParts)
		{
			foreach(var item in codedParts)
			{
				this.Add(item.Value);
			}
		}
	}
}
