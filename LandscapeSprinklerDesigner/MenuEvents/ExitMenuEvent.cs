using LandscapeSprinklerDesigner.Properties;
using System;

namespace LandscapeSprinklerDesigner.MenuEvents
{
	class ExitMenuEvent : MenuEvent
	{
		public override string Text => Resources.menu_exit;

		public override string ToolTipText => Resources.menu_exit_tooltip;

		public override void OnMouseClick(object sender, EventArgs e)
		{
			base.OnMouseClick(sender, e);
			Owner.Close();
		}
	}
}
