using TNT.LSD.Components.DrawingModes;
using TNT.ToolStripItemManager.Extension;

namespace LandscapeSprinklerDesigner.Events;

class DrawLineEvent() : DrawEvent(text: "Line", toolTipText: "Use the Line tool to draw a continuous line. Double click to end the line.", image: "LandscapeSprinklerDesigner.Images.line.png".ToImage())
{
    private LineMode lineMode = new LineMode() { Layer = 1 };
    public override DrawingMode DrawingMode => lineMode;
}
