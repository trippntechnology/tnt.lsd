using TNT.LSD.Components.DrawingModes;

namespace LandscapeSprinklerDesigner.Events;

class DrawLineEvent() : DrawEvent(text: "Line", toolTipText: "Use the Line tool to draw a continuous line. Double click to end the line.")
{
    private LineMode lineMode = new LineMode() { Layer = 1 };
    public override DrawingMode DrawingMode => lineMode;
}
