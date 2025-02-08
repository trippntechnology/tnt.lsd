namespace PaletteDesigner.Events;

class DemoteNode : NodeEvents
{
  public override string Text => "Demote Node";

  public override string ToolTipText => "Demote the node";

  public override void OnApplicationIdle(object sender, EventArgs e)
  {
    base.OnApplicationIdle(sender, e);
    Enabled = PalletNodeTreeView?.SelectedNode?.Parent != null;
  }
  public override void OnMouseClick(object sender, EventArgs e)
  {
    base.OnMouseClick(sender, e);
    PalletNodeTreeView.DemoteSelectedNode();
  }
}
