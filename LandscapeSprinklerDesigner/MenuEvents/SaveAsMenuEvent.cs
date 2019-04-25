using System;
using LandscapeSprinklerDesigner.Properties;

namespace LandscapeSprinklerDesigner.MenuEvents
{
	class SaveAsMenuEvent : MenuEvent
	{
		public override string Text => Resources.menu_save_as;

		public override string ToolTipText => Resources.menu_save_as_tooltip;

		public override void MouseClick(object sender, EventArgs e)
		{
			base.MouseClick(sender, e);
			CAD.Save(true);
		}
	}
}
