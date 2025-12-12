using TNT.LSD.Components.DrawingModes;

namespace LandscapeSprinklerDesigner.Events;

class DrawCurveEvent() : DrawEvent(text: "Curve", toolTipText: "Use the Curve tool to draw curves.")
{
    private BezierMode bezierMode = new BezierMode() { Layer = 1 };
    public override DrawingMode DrawingMode => bezierMode;
}
