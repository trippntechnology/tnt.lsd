

namespace LandscapeSprinklerDesigner.MenuEvents;

class Rotate180MenuEvent() : RotateMenuEvent(Resource.menu_rotate_180, Resource.menu_rotate_180_tooltip)
{
    public override int Angle => 180;
}
