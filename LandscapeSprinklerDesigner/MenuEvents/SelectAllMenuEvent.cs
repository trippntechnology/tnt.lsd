using LandscapeSprinklerDesigner.Properties;
using System;

namespace LandscapeSprinklerDesigner.MenuEvents
{
	class SelectAllMenuEvent : MenuEvent
	{
		public override string Text => Resources.menu_select_all;

		public override string ToolTipText => Resources.menu_select_all_tooltip;

		public override void OnApplicationIdle(object sender, EventArgs e)
		{
			base.OnApplicationIdle(sender, e);
			Enabled = CAD.DrawingMode.GetType() == typeof(LSDComponents.DrawingModes.SelectMode);
		}

		public override void MouseClick(object sender, EventArgs e)
		{
			base.MouseClick(sender, e);
			if (CAD.DrawingMode.GetType() == typeof(LSDComponents.DrawingModes.SelectMode))
			{
				CAD.SelectAll();
			}
		}
	}
}
