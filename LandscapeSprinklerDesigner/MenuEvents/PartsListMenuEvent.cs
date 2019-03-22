using LandscapeSprinklerDesigner.Properties;

namespace LandscapeSprinklerDesigner.MenuEvents
{
	class PartsListMenuEvent : CheckableMenuEvent
	{
		public override string Text => Resources.menu_parts_listing;

		public override string ToolTipText => Resources.menu_parts_listing_tooltip;

		public PartsListMenuEvent() : base(ResourceToImage("LandscapeSprinklerDesigner.Images.parts_list.png"))
		{
		}
	}
}
