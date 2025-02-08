using TNT.ToolStripItemManager;

namespace PaletteDesigner.Events;

class Exit : ToolStripItemGroup
{
  public override string Text => "Exit";

  public override string ToolTipText => "Exit";

  public Exit()
    : base()
  {
  }

  public override void OnMouseClick(object sender, EventArgs e)
  {
    base.OnMouseClick(sender, e);
    Application.Exit();
  }
}
