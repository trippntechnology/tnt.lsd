using TNT.LSD.Components.DrawingModes;

namespace LandscapeSprinklerDesigner.Events;

class DrawSelectEvent() : DrawEvent(text: "Select", toolTipText: "Use the Select tool to select Landscape objects so that they can be manipulated.")
{
    private TNT.LSD.Components.DrawingModes.SelectMode selectMode = new TNT.LSD.Components.DrawingModes.SelectMode() { Layer = 1 };
    public override DrawingMode DrawingMode => selectMode;
}
