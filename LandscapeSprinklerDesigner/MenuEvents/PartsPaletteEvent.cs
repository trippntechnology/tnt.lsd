using LandscapeSprinklerDesigner.Properties;
using System;

namespace LandscapeSprinklerDesigner.MenuEvents
{
	class PartsPaletteEvent : DockMenuEvent
	{
		public override string Text => Resources.menu_parts_palette;

		public override string ToolTipText => Resources.menu_parts_palette_tooltip;

		public PartsPaletteEvent() : base(ResourceToImage("LandscapeSprinklerDesigner.Images.parts_palette.png"))
		{
		}
	}
}
