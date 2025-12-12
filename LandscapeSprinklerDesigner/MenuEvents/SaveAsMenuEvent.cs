using TNT.LSD.Components;

namespace LandscapeSprinklerDesigner.MenuEvents;

class SaveAsMenuEvent() : MenuEvent(Resource.menu_save_as, Resource.menu_save_as_tooltip)
{
    public override void OnMouseClicked(Form owner, TNTCAD cad, LayoutSettingsForm layoutSettings)
    {
        cad.Save(true);
    }
}
