using TNT.LSD.Components;
using TNT.ToolStripItemManager.Extension;

namespace LandscapeSprinklerDesigner.MenuEvents;

class MoveToFrontMenuEvent() : MenuEvent(Resource.menu_move_to_front, Resource.menu_move_to_front_tooltip, image: "LandscapeSprinklerDesigner.Images.move_to_front.png".ToImage())
{
    public override void OnApplicationIdle(TNTCAD cad)
    {
        Enabled = cad.SelectedObjects.Count > 0;
    }

    public override void OnMouseClicked(Form owner, TNTCAD cad, LayoutSettingsForm layoutSettings)
    {
        cad.BringToFront();
    }
}
