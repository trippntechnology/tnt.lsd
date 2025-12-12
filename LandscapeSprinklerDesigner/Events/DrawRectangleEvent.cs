using TNT.LSD.Components.DrawingModes;

namespace LandscapeSprinklerDesigner.Events;

class DrawRectangleEvent() : DrawEvent(text: "Rectangle", toolTipText: "Use the Rectangle tool to draw rectangles.")
{
    private RectangleMode rectangleMode = new RectangleMode() { Layer = 1 };
    public override DrawingMode DrawingMode => rectangleMode;
}
