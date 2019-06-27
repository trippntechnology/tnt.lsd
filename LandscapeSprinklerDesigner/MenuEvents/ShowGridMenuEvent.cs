using LandscapeSprinklerDesigner.Properties;
using System;

namespace LandscapeSprinklerDesigner.MenuEvents
{
	class ShowGridMenuEvent : MenuEvent
	{
		public override bool CheckOnClick => true;

		public override string Text => Resources.menu_show_grid;

		public override string ToolTipText => Resources.menu_show_grip_tooltip;

		public ShowGridMenuEvent()
			: base(ResourceToImage("LandscapeSprinklerDesigner.Images.show_grid.png"))
		{

		}

		public override void OnMouseClick(object sender, EventArgs e)
		{
			base.OnMouseClick(sender, e);
			CAD.Settings.DrawGrid = this.Checked;
		}
	}
}
