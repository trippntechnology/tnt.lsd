using TNT.LSD.Components;


namespace LandscapeSprinklerDesigner.MenuEvents;

class SaveMenuEvent() : MenuEvent(Resource.menu_save, Resource.menu_save_tooltip)
{
    public override void OnMouseClicked(Form owner, TNTCAD cad, LayoutSettingsForm layoutSettings)
    {
        cad.Save(false);
    }
}
