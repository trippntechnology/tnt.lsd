using TNT.LSD.Components;
using TNT.ToolStripItemManager.Extension;

namespace LandscapeSprinklerDesigner.MenuEvents;

class ShowPartsMenuEvent() : MenuEvent(Resource.menu_show_parts, Resource.menu_show_parts_tooltip, checkOnClick: true, image: "LandscapeSprinklerDesigner.Images.show_parts.png".ToImage())
{
    public override void OnMouseClicked(Form owner, TNTCAD cad, LayoutSettingsForm layoutSettings)
    {
        cad.ShowPartsToolTip = Checked;
    }
}