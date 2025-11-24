using TNT.LSD.Components;
using TNT.ToolStripItemManager.Extension;

namespace LandscapeSprinklerDesigner.MenuEvents;

class ShowCoverageMenuEvent() : MenuEvent(Resource.menu_show_coverage, Resource.menu_show_coverage_tooltip, checkOnClick: true, image: "LandscapeSprinklerDesigner.Images.show_coverage.png".ToImage())
{
    public override void OnMouseClicked(Form owner, TNTCAD cad, LayoutSettingsForm layoutSettings)
    {
        cad.DrawingOptions.ShowCoverage = Checked;
        cad.Repaint();
    }
}
