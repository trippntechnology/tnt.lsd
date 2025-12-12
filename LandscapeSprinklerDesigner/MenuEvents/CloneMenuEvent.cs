using TNT.LSD.Components;

namespace LandscapeSprinklerDesigner.MenuEvents;

class CloneMenuEvent() : MenuEvent(Resource.menu_clone, Resource.menu_clone_tooltip)
{
    public override void OnApplicationIdle(TNTCAD cad)
    {
        Enabled = (from o in cad.SelectedObjects where o.CanClone select o).ToList().Count > 0;
    }

    public override void OnMouseClicked(Form owner, TNTCAD cad, LayoutSettingsForm layoutSettings)
    {
        cad.Copy();
    }
}
