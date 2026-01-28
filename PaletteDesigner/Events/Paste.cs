using PalletDesigner;

namespace PaletteDesigner.Events;

class Paste() : AppToolStripItemGroup("Paste", "Paste Node")
{
    public void OnApplicationIdle(Main main)
    {
        Enabled = main.Pallet.SelectedNode != null;
    }

    public override void OnMouseClick(Main main)
    {
        main.Pallet.Paste();
    }
}
