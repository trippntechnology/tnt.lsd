using PalletDesigner;

namespace PaletteDesigner.Events;

public abstract class NodeToolStripItemGroup(string text, string? toolTipText = null, bool checkOnClick = false) : AppToolStripItemGroup(text, toolTipText, checkOnClick)
{
    public virtual void OnApplicationIdle(Main main)
    {
        Enabled = main.Pallet.SelectedNode != null;
    }
}
