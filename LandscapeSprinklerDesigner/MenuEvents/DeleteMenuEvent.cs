using TNT.LSD.Components;

namespace LandscapeSprinklerDesigner.MenuEvents;

class DeleteMenuEvent() : MenuEvent(Resource.menu_delete, Resource.menu_delete_tooltip)
{
    public override void OnMouseClicked(Form owner, TNTCAD cad, LayoutSettingsForm layoutSettings)
    {
        cad.Delete();
    }

    public override void OnApplicationIdle(TNTCAD cad)
    {
        Enabled = cad.SelectedObjects.Count > 0;
    }
}
