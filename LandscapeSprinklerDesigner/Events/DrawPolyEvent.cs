using TNT.LSD.Components.DrawingModes;
using TNT.ToolStripItemManager.Extension;

namespace LandscapeSprinklerDesigner.Events;

class DrawPolyEvent() : DrawEvent(text: "Poly Line", toolTipText: "Use the Poly Line tool to draw a closed continuous line. Double click to end the line.", image: "LandscapeSprinklerDesigner.Images.polyline.png".ToImage())
{
    private PolyLineMode polyLineMode = new PolyLineMode() { Layer = 1 };
    public override DrawingMode DrawingMode => polyLineMode;
}
