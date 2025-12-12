using TNT.LSD.Components.DrawingModes;

namespace LandscapeSprinklerDesigner.Events;

class DrawCircleEvent() : DrawEvent(text: "Circle", toolTipText: "Use the Circle tool to draw circles.")
{
    private CircleMode circleMode = new CircleMode() { Layer = 1 };
    public override DrawingMode DrawingMode => circleMode;
}
