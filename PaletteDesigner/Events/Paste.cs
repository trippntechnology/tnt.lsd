using LSDComponents;
using System;
using TNT.ToolStripItemManager;

namespace PalletDesigner.Events
{
	class Paste : ToolStripItemGroup
	{
		private PalletNodeTreeView PalletNodeTreeView => base.ExternalObject as PalletNodeTreeView;

		public override string Text => "Paste";

		public override string ToolTipText => "Paste Node";

		public override void OnApplicationIdle(object sender, EventArgs e)
		{
			base.OnApplicationIdle(sender, e);
			Enabled = this.PalletNodeTreeView.SelectedNode != null;
		}

		public override void OnMouseClick(object sender, EventArgs e)
		{
			base.OnMouseClick(sender, e);
			this.PalletNodeTreeView.Paste();
		}
	}
}
