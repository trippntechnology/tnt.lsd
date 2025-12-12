using TNT.LSD.Components.DrawingModes;
using TNT.ToolStripItemManager;

namespace LandscapeSprinklerDesigner.Events;

abstract class DrawEvent(string text, string toolTipText) : ToolStripItemRadioGroup(text, toolTipText)
{
    public abstract DrawingMode DrawingMode { get; }
}
