using LandscapeSprinklerDesigner.Properties;
using System;

namespace LandscapeSprinklerDesigner.MenuEvents
{
	class ShowPartsMenuEvent : MenuEvent
	{
		public override bool CheckOnClick => true;

		public override string Text => Resources.menu_show_parts;

		public override string ToolTipText => Resources.menu_show_parts_tooltip;

		public ShowPartsMenuEvent() : base(ResourceToImage("LandscapeSprinklerDesigner.Images.show_parts.png"))
		{
		}

		public override void MouseClick(object sender, EventArgs e)
		{
			CAD.ShowPartsToolTip = this.Checked;
		}
	}
}