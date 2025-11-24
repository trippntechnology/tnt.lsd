using TNT.LSD.Components;
using TNT.ToolStripItemManager.Extension;

namespace LandscapeSprinklerDesigner.MenuEvents;

class SaveMenuEvent() : MenuEvent(Resource.menu_save, Resource.menu_save_tooltip, image: "LandscapeSprinklerDesigner.Images.save.png".ToImage())
{
    public override void OnMouseClicked(Form owner, TNTCAD cad, LayoutSettingsForm layoutSettings)
    {
        cad.Save(false);
    }
}
