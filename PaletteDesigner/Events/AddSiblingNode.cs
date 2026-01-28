using PalletDesigner;
using TNT.LSD.Components;

namespace PaletteDesigner.Events;

class AddSiblingNode() : AppToolStripItemGroup("Add Sibling Node", "Add a sibling node")
{
    public override void OnMouseClick(Main main)
    {
        PaletteNode newNode = main.Pallet.AddSiblingNode();
        main.PropertyEditor.SelectedObject = newNode.Properties;
    }
}
