using System;

namespace PalletDesigner.Events
{
	class DeleteNode : NodeEvents
	{
		public override string Text => "Delete Node";

		public override string ToolTipText => "Delete a node";

		public override void OnMouseClick(object sender, EventArgs e)
		{
			base.OnMouseClick(sender, e);

			this.PalletNodeTreeView.DeleteSelectedNode();
			this.PropertyGrid.SelectedObject = null;
		}
	}
}
