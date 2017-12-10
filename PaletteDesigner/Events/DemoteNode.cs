using System;

namespace PalletDesigner.Events
{
	class DemoteNode : NodeEvents
	{
		public override string Text => "Demote Node";

		public override string ToolTipText => "Demote the node";

		public override void OnApplicationIdle(object sender, EventArgs e)
		{
			base.OnApplicationIdle(sender, e);
			Enabled = PalletNodeTreeView?.SelectedNode?.Parent != null;
		}
		public override void MouseClick(object sender, EventArgs e)
		{
			base.MouseClick(sender, e);
			PalletNodeTreeView.DemoteSelectedNode();
		}
	}
}
