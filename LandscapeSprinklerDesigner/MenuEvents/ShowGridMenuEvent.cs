using System;

namespace LandscapeSprinklerDesigner.MenuEvents
{
	class ShowGridMenuEvent : MenuEvent
	{
		public override bool CheckOnClick => true;

		public override string Text => "Show Grid";

		public override string ToolTipText => "Toggle Grid Visibility";

		public ShowGridMenuEvent()
			: base(ResourceToImage("LandscapeSprinklerDesigner.Images.show_grid.png"))
		{

		}

		public override void MouseClick(object sender, EventArgs e)
		{
			base.MouseClick(sender, e);
			CAD.Settings.DrawGrid = this.Checked;
		}
	}
}
