using LSDComponents;
using System;
using TNT.ToolStripItemManager;

namespace PalletDesigner.Events
{
	class Copy : ToolStripItemGroup
	{
		private PalletNodeTreeView PalletNodeTreeView => base.ExternalObject as PalletNodeTreeView;

		public override string Text => "Copy";

		public override string ToolTipText => "Copy Node";

		public override void OnApplicationIdle(object sender, EventArgs e)
		{
			base.OnApplicationIdle(sender, e);
			Enabled = this.PalletNodeTreeView.SelectedNode != null;
		}

		public override void MouseClick(object sender, EventArgs e)
		{
			base.MouseClick(sender, e);
			this.PalletNodeTreeView.Copy();
		}
	}
}
