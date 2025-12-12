using TNT.LSD.Components;


namespace LandscapeSprinklerDesigner.MenuEvents;

class MoveToFrontMenuEvent() : MenuEvent(Resource.menu_move_to_front, Resource.menu_move_to_front_tooltip)
{
    public override void OnApplicationIdle(TNTCAD cad)
    {
        Enabled = cad.SelectedObjects.Count > 0;
    }

    public override void OnMouseClicked(Form owner, TNTCAD cad, LayoutSettingsForm layoutSettings)
    {
        cad.BringToFront();
    }
}
