using TNT.LSD.Components;
using TNT.ToolStripItemManager.Extension;

namespace LandscapeSprinklerDesigner.MenuEvents;

class ExitMenuEvent() : MenuEvent(Resource.menu_exit, Resource.menu_exit, image: "LandscapeSprinklerDesigner.Images.exit.png".ToImage())
{
    public override void OnMouseClicked(Form owner, TNTCAD cad, LayoutSettingsForm layoutSettings)
    {
        owner.Close();
    }
}
