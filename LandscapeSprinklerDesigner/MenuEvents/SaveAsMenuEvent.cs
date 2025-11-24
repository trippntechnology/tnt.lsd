using TNT.LSD.Components;
using TNT.ToolStripItemManager.Extension;

namespace LandscapeSprinklerDesigner.MenuEvents;

class SaveAsMenuEvent() : MenuEvent(Resource.menu_save_as, Resource.menu_save_as_tooltip, image: "LandscapeSprinklerDesigner.Images.save_as.png".ToImage())
{
    public override void OnMouseClicked(Form owner, TNTCAD cad, LayoutSettingsForm layoutSettings)
    {
        cad.Save(true);
    }
}
