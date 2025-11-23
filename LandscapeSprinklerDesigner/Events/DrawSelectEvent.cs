using TNT.LSD.Components.DrawingModes;
using TNT.ToolStripItemManager.Extension;

namespace LandscapeSprinklerDesigner.Events;

class DrawSelectEvent() : DrawEvent(text: "Select", toolTipText: "Use the Select tool to select Landscape objects so that they can be manipulated.", image: "LandscapeSprinklerDesigner.Images.hand.png".ToImage())
{
    private TNT.LSD.Components.DrawingModes.SelectMode selectMode = new TNT.LSD.Components.DrawingModes.SelectMode() { Layer = 1 };
    public override DrawingMode DrawingMode => selectMode;
}
