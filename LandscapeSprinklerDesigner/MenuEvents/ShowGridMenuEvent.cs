using TNT.LSD.Components;


namespace LandscapeSprinklerDesigner.MenuEvents;

class ShowGridMenuEvent() : MenuEvent(Resource.menu_show_grid, Resource.menu_show_grip_tooltip, checkOnClick: true)
{
    public override void OnMouseClicked(Form owner, TNTCAD cad, LayoutSettingsForm layoutSettings)
    {
        cad.Settings.DrawGrid = Checked;
    }
}
