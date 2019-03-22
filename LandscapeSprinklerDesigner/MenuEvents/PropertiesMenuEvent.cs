using LandscapeSprinklerDesigner.Properties;

namespace LandscapeSprinklerDesigner.MenuEvents
{
	class PropertiesMenuEvent : CheckableMenuEvent
	{
		public override string Text => Resources.menu_properties;

		public override string ToolTipText => Resources.menu_properties_tooltip;

		public PropertiesMenuEvent() : base(ResourceToImage("LandscapeSprinklerDesigner.Images.properties.png"))
		{
		}
	}
}
