using LandscapeSprinklerDesigner.Properties;
using System;

namespace LandscapeSprinklerDesigner.MenuEvents
{
	class SaveAsMenuEvent : MenuEvent
	{
		public override string Text => Resources.menu_save_as;

		public override string ToolTipText => Resources.menu_save_as_tooltip;

		public override void OnMouseClick(object sender, EventArgs e)
		{
			base.OnMouseClick(sender, e);
			CAD.Save(true);
		}
	}
}
