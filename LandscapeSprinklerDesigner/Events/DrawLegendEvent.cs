using TNT.LSD.Components.DrawingModes;
using TNT.ToolStripItemManager.Extension;

namespace LandscapeSprinklerDesigner.Events;

class DrawLegendEvent() : DrawEvent(text: "Draw legend", toolTipText: "Draw a legend", image: "LandscapeSprinklerDesigner.Images.legend.png".ToImage())
{
    private LegendMode legendMode = new LegendMode() { Layer = 1 };
    public override DrawingMode DrawingMode => legendMode;
}
