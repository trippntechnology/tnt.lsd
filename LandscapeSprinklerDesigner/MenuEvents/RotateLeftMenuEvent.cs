using TNT.ToolStripItemManager.Extension;

namespace LandscapeSprinklerDesigner.MenuEvents;

class RotateLeftMenuEvent() : RotateMenuEvent(Resource.menu_rotate_left, Resource.menu_rotate_left_tooltip, "LandscapeSprinklerDesigner.Images.rotate_left.png".ToImage())
{
    public override int Angle => -90;
}
