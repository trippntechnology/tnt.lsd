using TNT.LSD.Components.DrawingModes;
using TNT.ToolStripItemManager.Extension;

namespace LandscapeSprinklerDesigner.Events;

class DrawCircleEvent() : DrawEvent(text: "Circle", toolTipText: "Use the Circle tool to draw circles.", image: "LandscapeSprinklerDesigner.Images.circle.png".ToImage())
{
    private CircleMode circleMode = new CircleMode() { Layer = 1 };
    public override DrawingMode DrawingMode => circleMode;

}
