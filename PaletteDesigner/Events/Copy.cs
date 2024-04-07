using LSDComponents;
using TNT.ToolStripItemManager;

namespace PaletteDesigner.Events;

class Copy : ToolStripItemGroup
{
  private PalletNodeTreeView PalletNodeTreeView => ExternalObject as PalletNodeTreeView;

  public override string Text => "Copy";

  public override string ToolTipText => "Copy Node";

  public override void OnApplicationIdle(object sender, EventArgs e)
  {
    base.OnApplicationIdle(sender, e);
    Enabled = PalletNodeTreeView.SelectedNode != null;
  }

  public override void OnMouseClick(object sender, EventArgs e)
  {
    base.OnMouseClick(sender, e);
    PalletNodeTreeView.Copy();
  }
}
