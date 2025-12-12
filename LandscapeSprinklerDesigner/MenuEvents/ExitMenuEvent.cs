using TNT.LSD.Components;

namespace LandscapeSprinklerDesigner.MenuEvents;

class ExitMenuEvent() : MenuEvent(Resource.menu_exit, Resource.menu_exit)
{
    public override void OnMouseClicked(Form owner, TNTCAD cad, LayoutSettingsForm layoutSettings)
    {
        owner.Close();
    }
}
