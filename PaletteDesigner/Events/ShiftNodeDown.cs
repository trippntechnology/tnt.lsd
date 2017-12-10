using System;

namespace PalletDesigner.Events
{
	class ShiftNodeDown : NodeEvents
	{
		public override string Text => "Shift Node Down";

		public override string ToolTipText => "Shift the node down";

		public override void OnApplicationIdle(object sender, EventArgs e)
		{
			base.OnApplicationIdle(sender, e);
			Enabled = PalletNodeTreeView.SelectedNode?.NextNode != null;
		}
		public override void MouseClick(object sender, EventArgs e)
		{
			base.MouseClick(sender, e);
			PalletNodeTreeView.ShiftSelectedNodeDown();
		}
	}
}
