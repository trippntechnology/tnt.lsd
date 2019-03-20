using LandscapeSprinklerDesigner.Properties;
using System;

namespace LandscapeSprinklerDesigner.MenuEvents
{
	class SaveMenuEvent : MenuEvent
	{
		public override string Text => Resources.menu_save;

		public override string ToolTipText => Resources.menu_save_tooltip;

		public SaveMenuEvent() : base(ResourceToImage("LandscapeSprinklerDesigner.Images.save.png"))
		{
		}

		public override void MouseClick(object sender, EventArgs e)
		{
			base.MouseClick(sender, e);
			CAD.Save(false);
		}
	}
}
