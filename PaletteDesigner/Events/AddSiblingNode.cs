using LSDComponents;
using System;

namespace PalletDesigner.Events
{
	class AddSiblingNode : NodeEvents
	{
		public override string Text => "Add Sibling Node";

		public override string ToolTipText => "Add a sibling node";

		public override void OnMouseClick(object sender, EventArgs e)
		{
			base.OnMouseClick(sender, e);

			PaletteNode newNode = this.PalletNodeTreeView.AddSiblingNode();
			this.PropertyGrid.SelectedObject = newNode.Properties;
		}
	}
}
