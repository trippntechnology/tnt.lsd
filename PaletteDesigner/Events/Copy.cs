using PalletDesigner;

namespace PaletteDesigner.Events;

class Copy() : AppToolStripItemGroup("Copy", "Copy Node")
{
    public override void OnMouseClick(Main main)
    {
        main.Pallet.Copy();
    }
}
