using System;
using TNT.ToolStripItemManager;

namespace PalletDesigner.Events
{
	class Save : ToolStripItemGroup
	{
		public override string Text => "&Save";

		public override string ToolTipText => "Save a palette file";

		public Save()
			:base(ResourceToImage("PalletDesigner.Images.disk.png"))
		{
		}

		public override void MouseClick(object sender, EventArgs e)
		{
			base.MouseClick(sender, e);

			if (base.ExternalObject is Main main)
			{
				if (!string.IsNullOrEmpty(main.CurrentFileName))
				{
					main.Pallet.Save(main.CurrentFileName);
				}
				else
				{
					base.ToolStripItemGroupManager["Save &As"]?.MouseClick(sender, e);
				}
			}
		}
	}
}
