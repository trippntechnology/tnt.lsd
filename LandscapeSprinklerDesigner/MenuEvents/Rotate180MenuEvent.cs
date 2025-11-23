using TNT.ToolStripItemManager.Extension;

namespace LandscapeSprinklerDesigner.MenuEvents;

class Rotate180MenuEvent() : RotateMenuEvent(Resource.menu_rotate_180, Resource.menu_rotate_180_tooltip, "LandscapeSprinklerDesigner.Images.rotate_180.png".ToImage())
{
    public override int Angle => 180;
}
