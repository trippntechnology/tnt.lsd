using TNT.LSD.Components;


namespace LandscapeSprinklerDesigner.MenuEvents;

class ShowLandscapeMenuEvent() : MenuEvent(Resource.menu_show_landscape_image, Resource.menu_show_landscape_image_tooltip, checkOnClick: true)
{
    public override void OnMouseClicked(Form owner, TNTCAD cad, LayoutSettingsForm layoutSettings)
    {
        cad.DrawBackground = Checked;
    }
}