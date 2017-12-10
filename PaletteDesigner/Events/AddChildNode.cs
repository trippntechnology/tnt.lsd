using LSDComponents;
using System;

namespace PalletDesigner.Events
{
	class AddChildNode : NodeEvents
	{
		public override string Text => "Add Child Node";

		public override string ToolTipText => "Add a sibling node";

		public override void MouseClick(object sender, EventArgs e)
		{
			base.MouseClick(sender, e);

			PaletteNode newNode = this.PalletNodeTreeView.AddChildNode();
			this.PropertyGrid.SelectedObject = newNode.Properties;
		}
	}
}
