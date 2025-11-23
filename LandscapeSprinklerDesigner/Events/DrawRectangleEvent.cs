using TNT.LSD.Components.DrawingModes;
using TNT.ToolStripItemManager.Extension;

namespace LandscapeSprinklerDesigner.Events;

class DrawRectangleEvent() : DrawEvent(text: "Rectangle", toolTipText: "Use the Rectangle tool to draw rectangles.", image: "LandscapeSprinklerDesigner.Images.rectangle.png".ToImage())
{
    private RectangleMode rectangleMode = new RectangleMode() { Layer = 1 };
    public override DrawingMode DrawingMode => rectangleMode;
}
