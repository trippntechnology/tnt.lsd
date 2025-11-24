using TNT.LSD.Components;
using TNT.ToolStripItemManager.Extension;

namespace LandscapeSprinklerDesigner.MenuEvents;

class ShowGridMenuEvent() : MenuEvent(Resource.menu_show_grid, Resource.menu_show_grip_tooltip, checkOnClick: true, image: "LandscapeSprinklerDesigner.Images.show_grid.png".ToImage())
{
    public override void OnMouseClicked(Form owner, TNTCAD cad, LayoutSettingsForm layoutSettings)
    {
        cad.Settings.DrawGrid = Checked;
    }
}
