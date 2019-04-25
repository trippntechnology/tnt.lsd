using System;
using LandscapeSprinklerDesigner.Properties;

namespace LandscapeSprinklerDesigner.MenuEvents
{
	class ShowLandscapeMenuEvent : MenuEvent
	{
		public override bool CheckOnClick => true;

		public override string Text => Resources.menu_show_landscape_image;

		public override string ToolTipText => Resources.menu_show_landscape_image_tooltip;

		public ShowLandscapeMenuEvent() : base(ResourceToImage("LandscapeSprinklerDesigner.Images.show_landscape.png"))
		{
		}

		public override void MouseClick(object sender, EventArgs e)
		{
			base.MouseClick(sender, e);
			CAD.DrawBackground = this.Checked;
		}
	}
}