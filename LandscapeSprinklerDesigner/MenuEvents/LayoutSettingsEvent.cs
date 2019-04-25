using LandscapeSprinklerDesigner.Properties;

namespace LandscapeSprinklerDesigner.MenuEvents
{
	class LayoutSettingsEvent : DockMenuEvent
	{

		public override string Text => Resources.menu_layout_settings;

		public override string ToolTipText => Resources.menu_layout_settings_tooltip;

		public LayoutSettingsEvent() : base(ResourceToImage("LandscapeSprinklerDesigner.Images.layout_settings.png"))
		{
		}
	}
}
