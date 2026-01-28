using PalletDesigner;

namespace PaletteDesigner.Events;

class ShiftNodeDown() : NodeToolStripItemGroup("Shift Node Down", "Shift the node down")
{
    public override void OnApplicationIdle(Main main)
    {
        base.OnApplicationIdle(main);
        Enabled = main.Pallet.SelectedNode?.NextNode != null;
    }

    public override void OnMouseClick(Main main)
    {
        base.OnMouseClick(main);
        main.Pallet.ShiftSelectedNodeDown();
    }
}
