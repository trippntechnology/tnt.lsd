using TNT.LSD.Components;

namespace LandscapeSprinklerDesigner.MenuEvents;

class AlignToGridMenuEvent() : MenuEvent(Resource.menu_align_to_grid, Resource.menu_align_to_grid_tooltip)
{
    public override void OnApplicationIdle(TNTCAD cad)
    {
        this.Enabled = cad.SelectedObjects.Count > 0;
    }

    public override void OnMouseClicked(Form owner, TNTCAD cad, LayoutSettingsForm layoutSettings)
    {
        cad.AlignToGrid();
    }
}
