using LSDComponents;

namespace PaletteDesigner.Events;

class AddChildNode : NodeEvents
{
  public override string Text => "Add Child Node";

  public override string ToolTipText => "Add a sibling node";

  public override void OnMouseClick(object sender, EventArgs e)
  {
    base.OnMouseClick(sender, e);

    PaletteNode newNode = PalletNodeTreeView.AddChildNode();
    PropertyGrid.SelectedObject = newNode.Properties;
  }
}
