namespace LandscapeSprinklerDesigner.MenuEvents;

class RotateRightMenuEvent() : RotateMenuEvent(Resource.menu_rotate_right, Resource.menu_rotate_right_tooltip)
{
    public override int Angle => 90;
}
