using TNT.LSD.Components;

namespace LandscapeSprinklerDesigner.MenuEvents;

class SelectAllMenuEvent() : MenuEvent(Resource.menu_select_all, Resource.menu_select_all_tooltip)
{
    public override void OnApplicationIdle(TNTCAD cad)
    {
        Enabled = cad.DrawingMode.GetType() == typeof(TNT.LSD.Components.DrawingModes.SelectMode);
    }

    public override void OnMouseClicked(Form owner, TNTCAD cad, LayoutSettingsForm layoutSettings)
    {
        if (cad.DrawingMode.GetType() == typeof(TNT.LSD.Components.DrawingModes.SelectMode)) cad.SelectAll();
    }
}
