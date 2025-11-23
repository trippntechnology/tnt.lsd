using TNT.LSD.Components.DrawingModes;
using TNT.ToolStripItemManager.Extension;

namespace LandscapeSprinklerDesigner.Events;

class DrawCurveEvent() : DrawEvent(text: "Curve", toolTipText: "Use the Curve tool to draw curves.", image: "LandscapeSprinklerDesigner.Images.curve.png".ToImage())
{
    private BezierMode bezierMode = new BezierMode() { Layer = 1 };
    public override DrawingMode DrawingMode => bezierMode;
}
