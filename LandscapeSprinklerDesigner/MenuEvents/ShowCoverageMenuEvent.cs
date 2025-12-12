using TNT.LSD.Components;


namespace LandscapeSprinklerDesigner.MenuEvents;

class ShowCoverageMenuEvent() : MenuEvent(Resource.menu_show_coverage, Resource.menu_show_coverage_tooltip, checkOnClick: true)
{
    public override void OnMouseClicked(Form owner, TNTCAD cad, LayoutSettingsForm layoutSettings)
    {
        cad.DrawingOptions.ShowCoverage = Checked;
        cad.Repaint();
    }
}
