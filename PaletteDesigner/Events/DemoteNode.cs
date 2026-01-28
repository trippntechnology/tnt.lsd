using PalletDesigner;

namespace PaletteDesigner.Events;

class DemoteNode() : NodeToolStripItemGroup("Demote Node", "Demote the node")
{
    public override void OnApplicationIdle(Main main)
    {
        base.OnApplicationIdle(main);
        Enabled = main.Pallet.SelectedNode?.Parent != null;
    }
    public override void OnMouseClick(Main main)
    {
        base.OnMouseClick(main);
        main.Pallet.DemoteSelectedNode();
    }
}
