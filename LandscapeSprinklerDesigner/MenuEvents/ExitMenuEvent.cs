using LandscapeSprinklerDesigner.Properties;
using TNT.ToolStripItemManager;

namespace LandscapeSprinklerDesigner.MenuEvents
{
	class ExitMenuEvent : ToolStripItemGroup
	{
		public override string Text => Resources.menu_exit;

		public override string ToolTipText => Resources.menu_exit_tooltip;
	}
}
