using TNT.LSD.Components;


namespace LandscapeSprinklerDesigner.MenuEvents;

class MoveToBackMenuEvent() : MenuEvent(Resource.menu_move_to_back, Resource.menu_move_to_back_tooltip)
{
    public override void OnApplicationIdle(TNTCAD cad)
    {
        Enabled = cad.SelectedObjects.Count > 0;
    }

    public override void OnMouseClicked(Form owner, TNTCAD cad, LayoutSettingsForm layoutSettings)
    {
        cad.SendToBack();
    }
}
