using TNT.ToolStripItemManager.Extension;

namespace LandscapeSprinklerDesigner.MenuEvents;

class RotateRightMenuEvent() : RotateMenuEvent(Resource.menu_rotate_right, Resource.menu_rotate_right_tooltip, "LandscapeSprinklerDesigner.Images.rotate_right.png".ToImage())
{
    public override int Angle => 90;
}
