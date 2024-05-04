using TNT.LSD.Components;
using TNT.ToolStripItemManager;

namespace PaletteDesigner.Events;

class Paste : ToolStripItemGroup
{
  private PalletNodeTreeView PalletNodeTreeView => ExternalObject as PalletNodeTreeView;

  public override string Text => "Paste";

  public override string ToolTipText => "Paste Node";

  public override void OnApplicationIdle(object sender, EventArgs e)
  {
    base.OnApplicationIdle(sender, e);
    Enabled = PalletNodeTreeView.SelectedNode != null;
  }

  public override void OnMouseClick(object sender, EventArgs e)
  {
    base.OnMouseClick(sender, e);
    PalletNodeTreeView.Paste();
  }
}
