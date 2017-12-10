using System;

namespace PalletDesigner.Events
{
	class ShiftNodeUp : NodeEvents
	{
		public override string Text => "Shift Node Up";

		public override string ToolTipText => "Shift the node up";

		public override void OnApplicationIdle(object sender, EventArgs e)
		{
			base.OnApplicationIdle(sender, e);
			Enabled = PalletNodeTreeView.SelectedNode?.PrevNode != null;
		}
		public override void MouseClick(object sender, EventArgs e)
		{
			base.MouseClick(sender, e);
			base.PalletNodeTreeView.ShiftSelectedNodeUp();
		}
	}
}
