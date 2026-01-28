using PalletDesigner;

namespace PaletteDesigner.Events;

class ShiftNodeUp() : NodeToolStripItemGroup("Shift Node Up", "Shift the node up")
{
    public override void OnApplicationIdle(Main main)
    {
        base.OnApplicationIdle(main);
        Enabled = main.Pallet.SelectedNode?.PrevNode != null;
    }

    public override void OnMouseClick(Main main)
    {
        base.OnMouseClick(main);
        main.Pallet.ShiftSelectedNodeUp();
    }
}
