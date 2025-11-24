using TNT.LSD.Components;
using TNT.ToolStripItemManager.Extension;

namespace LandscapeSprinklerDesigner.MenuEvents;

class DeleteMenuEvent() : MenuEvent(Resource.menu_delete, Resource.menu_delete_tooltip, image: "LandscapeSprinklerDesigner.Images.delete.png".ToImage())
{
    public override void OnMouseClicked(Form owner, TNTCAD cad, LayoutSettingsForm layoutSettings)
    {
        cad.Delete();
    }

    public override void OnApplicationIdle(TNTCAD cad)
    {
        Enabled = cad.SelectedObjects.Count > 0;
    }
}
