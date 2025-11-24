using TNT.LSD.Components;
using TNT.ToolStripItemManager.Extension;

namespace LandscapeSprinklerDesigner.MenuEvents;

class ShowLandscapeMenuEvent() : MenuEvent(Resource.menu_show_landscape_image, Resource.menu_show_landscape_image_tooltip, checkOnClick: true, image: "LandscapeSprinklerDesigner.Images.show_landscape.png".ToImage())
{
    public override void OnMouseClicked(Form owner, TNTCAD cad, LayoutSettingsForm layoutSettings)
    {
        cad.DrawBackground = Checked;
    }
}