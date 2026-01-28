using PalletDesigner;

namespace PaletteDesigner.Events;

class DeleteNode() : AppToolStripItemGroup("Delete Node", "Delete a node")
{
    public override void OnMouseClick(Main main)
    {
        main.Pallet.DeleteSelectedNode();
        main.PropertyEditor.SelectedObject = null;
    }
}
