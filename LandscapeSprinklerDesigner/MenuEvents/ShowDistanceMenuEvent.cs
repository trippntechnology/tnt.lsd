using TNT.LSD.Components;

using TNT.Utilities;

namespace LandscapeSprinklerDesigner.MenuEvents;

class ShowDistanceMenuEvent() : PersistedMenuEvent(Resource.menu_show_distances, Resource.menu_show_distance_tooltip, checkOnClick: true)
{
    private const string REGISTRY_KEY = "AlwaysShowDistances";

    public override void RestoreState(ApplicationRegistry applicationRegistry, TNTCAD cad)
    {
        this.Checked = applicationRegistry.ReadBoolean(REGISTRY_KEY, false);
        cad.DrawingOptions.AlwaysShowDistances = Checked;
        cad.Repaint();
    }

    public override void SaveState(ApplicationRegistry applicationRegistry)
    {
        applicationRegistry.WriteBoolean(REGISTRY_KEY, this.Checked);
    }

    public override void OnMouseClicked(Form owner, TNTCAD cad, LayoutSettingsForm layoutSettings)
    {
        cad.DrawingOptions.AlwaysShowDistances = Checked;
        cad.Repaint();
    }
}
