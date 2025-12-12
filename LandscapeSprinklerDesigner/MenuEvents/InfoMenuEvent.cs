using TNT.LSD.Components;
using TNT.LSD.Objects.Interfaces;


namespace LandscapeSprinklerDesigner.MenuEvents;

class InfoMenuEvent() : MenuEvent(Resource.menu_info, Resource.menu_info_tooltip)
{
    public override void OnApplicationIdle(TNTCAD cad)
    {
        Enabled = cad.SelectedObjects.OfType<ISummable>().Any();
    }

    public override void OnMouseClicked(Form owner, TNTCAD cad, LayoutSettingsForm layoutSettings)
    {
        var summableParts = cad.SelectedObjects.OfType<ISummable>().ToList();
        double gpm = summableParts.Sum(p => p.GPM);
        MessageBox.Show(owner, $"Part Count: {summableParts.Count}\nGPM: {gpm}", "Selected GPM");
    }
}
