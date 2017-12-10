using System;

namespace PalletDesigner.Events
{
	class DeleteNode : NodeEvents
	{
		public override string Text => "Delete Node";

		public override string ToolTipText => "Delete a node";

		public override void MouseClick(object sender, EventArgs e)
		{
			base.MouseClick(sender, e);

			this.PalletNodeTreeView.DeleteSelectedNode();
			this.PropertyGrid.SelectedObject = null;
		}
	}
}
