using TNT.LSD.Components.DrawingModes;
using TNT.ToolStripItemManager.Extension;

namespace LandscapeSprinklerDesigner.Events;

class DrawTextEvent() : DrawEvent(text: "Text", toolTipText: "Use the Text tool to place notes on the layout.", image: "LandscapeSprinklerDesigner.Images.text.png".ToImage())
{
    private TextMode textMode = new TextMode() { Layer = 1 };
    public override DrawingMode DrawingMode => textMode;
}
