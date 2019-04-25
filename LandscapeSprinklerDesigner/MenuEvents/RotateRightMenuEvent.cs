using LandscapeSprinklerDesigner.Properties;

namespace LandscapeSprinklerDesigner.MenuEvents
{
	class RotateRightMenuEvent : RotateMenuEvent
	{
		public override string Text => Resources.menu_rotate_right;

		public override string ToolTipText => Resources.menu_rotate_right_tooltip;

		public override int Angle => 90;

		public RotateRightMenuEvent() : base(ResourceToImage("LandscapeSprinklerDesigner.Images.rotate_right.png"))
		{
		}
	}
}
