using PalletDesigner;
using TNT.ToolStripItemManager;

namespace PaletteDesigner.Events;

public abstract class AppToolStripItemGroup(string text, string? toolTipText = null, bool checkOnClick = false) : ToolStripItemGroup(text, toolTipText, checkOnClick)
{
    public virtual void OnMouseClick(Main main) { }
}
