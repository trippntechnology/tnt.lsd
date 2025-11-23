using TNT.LSD.Components;
using TNT.ToolStripItemManager.Extension;

namespace LandscapeSprinklerDesigner.MenuEvents;

class AlignToGridMenuEvent() : MenuEvent(Resource.menu_align_to_grid, Resource.menu_align_to_grid_tooltip, image: "LandscapeSprinklerDesigner.Images.align_to_grid.png".ToImage())
{
    public override void OnApplicationIdle(TNTCAD cad)
    {
        this.Enabled = cad.SelectedObjects.Count > 0;
    }

    public override void OnMouseClicked(Form owner, TNTCAD cad)
    {
        cad.AlignToGrid();
    }
}
