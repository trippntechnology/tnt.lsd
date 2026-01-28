using PalletDesigner;
using TNT.LSD.Components;

namespace PaletteDesigner.Events;

class AddChildNode() : NodeToolStripItemGroup("Add Child Node", "Add a sibling node")
{
    public override void OnMouseClick(Main main)
    {
        PaletteNode newNode = main.Pallet.AddChildNode();
        main.PropertyEditor.SelectedObject = newNode.Properties;
    }
}
