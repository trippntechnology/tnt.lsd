using TNT.LSD.Components;
using TNT.ToolStripItemManager.Extension;
using TNT.Utilities;

namespace LandscapeSprinklerDesigner.MenuEvents;

class SnapToGridMenuEvent() : PersistedMenuEvent(Resource.menu_snap_to_grid, Resource.menu_snap_to_grid_tooltip, checkOnClick: true, image: "LandscapeSprinklerDesigner.Images.snap_to_grid.png".ToImage())
{
    private const string REGISTRY_KEY = "SnapToGrid";

    public override void RestoreState(ApplicationRegistry applicationRegistry, TNTCAD cad)
    {
        this.Checked = applicationRegistry.ReadBoolean(REGISTRY_KEY, true);
        cad.SnapToGrid = Checked;
    }

    public override void SaveState(ApplicationRegistry applicationRegistry)
    {
        applicationRegistry.WriteBoolean(REGISTRY_KEY, this.Checked);
    }

    public override void OnMouseClicked(Form owner, TNTCAD cad)
    {
        cad.SnapToGrid = Checked;
    }
}
