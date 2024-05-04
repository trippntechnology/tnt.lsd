using TNT.LSD.Components;
using TNT.ToolStripItemManager;

namespace PaletteDesigner.Events;

abstract class NodeEvents : ToolStripItemGroup
{
  protected PalletNodeTreeView PalletNodeTreeView => (ExternalObject as Tuple<object, object>).Item1 as PalletNodeTreeView;
  protected PropertyGrid PropertyGrid => (ExternalObject as Tuple<object, object>).Item2 as PropertyGrid;

  public override void OnApplicationIdle(object sender, EventArgs e)
  {
    base.OnApplicationIdle(sender, e);
    Enabled = PalletNodeTreeView?.SelectedNode != null;
  }
}
