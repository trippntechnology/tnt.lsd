using TNT.LSD.Components.DrawingModes;

namespace LandscapeSprinklerDesigner.Events;

class DrawTextEvent() : DrawEvent(text: "Text", toolTipText: "Use the Text tool to place notes on the layout.")
{
    private TextMode textMode = new TextMode() { Layer = 1 };
    public override DrawingMode DrawingMode => textMode;
}
