using TNT.LSD.Components.DrawingModes;

namespace LandscapeSprinklerDesigner.Events;

class DrawLegendEvent() : DrawEvent(text: "Draw legend", toolTipText: "Draw a legend")
{
    private LegendMode legendMode = new LegendMode() { Layer = 1 };
    public override DrawingMode DrawingMode => legendMode;
}
