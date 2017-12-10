using System;
using System.Windows.Forms;
using TNT.ToolStripItemManager;

namespace PalletDesigner.Events
{
	public class Open : ToolStripItemGroup
	{
		public override string Text => "&Open";

		public override string ToolTipText => "Open a palette file";

		public Open()
			: base(ResourceToImage("PalletDesigner.Images.folder.png"))
		{
		}

		public override void MouseClick(object sender, EventArgs e)
		{
			base.MouseClick(sender, e);

			if (base.ExternalObject is Main main)
			{
				OpenFileDialog ofd = main.OpenDialog;

				if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					main.Pallet.Load(ofd.FileName);
					main.CurrentFileName = ofd.FileName;
				}
			}
		}
	}
}
