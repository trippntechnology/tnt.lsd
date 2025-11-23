using TNT.LSD.Components.DrawingModes;
using TNT.ToolStripItemManager;

namespace LandscapeSprinklerDesigner.Events;

abstract class DrawEvent(string text, string toolTipText, Image? image = null) : ToolStripItemRadioGroup(text, toolTipText, image: image)
{
    public abstract DrawingMode DrawingMode { get; }
}
