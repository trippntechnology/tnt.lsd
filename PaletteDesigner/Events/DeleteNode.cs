namespace PaletteDesigner.Events;

class DeleteNode : NodeEvents
{
  public override string Text => "Delete Node";

  public override string ToolTipText => "Delete a node";

  public override void OnMouseClick(object sender, EventArgs e)
  {
    base.OnMouseClick(sender, e);

    PalletNodeTreeView.DeleteSelectedNode();
    PropertyGrid.SelectedObject = null;
  }
}
