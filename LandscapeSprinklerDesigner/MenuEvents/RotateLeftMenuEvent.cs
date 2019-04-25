using LandscapeSprinklerDesigner.Properties;

namespace LandscapeSprinklerDesigner.MenuEvents
{
	class RotateLeftMenuEvent : RotateMenuEvent
	{
		public override string Text => Resources.menu_rotate_left;

		public override string ToolTipText => Resources.menu_rotate_left_tooltip;

		public override int Angle => -90;

		public RotateLeftMenuEvent() : base(ResourceToImage("LandscapeSprinklerDesigner.Images.rotate_left.png"))
		{
		}
	}
}
