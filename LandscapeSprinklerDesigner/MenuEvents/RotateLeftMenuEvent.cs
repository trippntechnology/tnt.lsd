

namespace LandscapeSprinklerDesigner.MenuEvents;

class RotateLeftMenuEvent() : RotateMenuEvent(Resource.menu_rotate_left, Resource.menu_rotate_left_tooltip)
{
    public override int Angle => -90;
}
