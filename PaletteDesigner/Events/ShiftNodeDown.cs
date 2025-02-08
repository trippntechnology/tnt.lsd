namespace PaletteDesigner.Events;

class ShiftNodeDown : NodeEvents
{
  public override string Text => "Shift Node Down";

  public override string ToolTipText => "Shift the node down";

  public override void OnApplicationIdle(object sender, EventArgs e)
  {
    base.OnApplicationIdle(sender, e);
    Enabled = PalletNodeTreeView.SelectedNode?.NextNode != null;
  }
  public override void OnMouseClick(object sender, EventArgs e)
  {
    base.OnMouseClick(sender, e);
    PalletNodeTreeView.ShiftSelectedNodeDown();
  }
}
