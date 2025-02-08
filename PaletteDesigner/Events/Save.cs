using PalletDesigner;
using TNT.ToolStripItemManager;

namespace PaletteDesigner.Events;

class Save : ToolStripItemGroup
{
  public override string Text => "&Save";

  public override string ToolTipText => "Save a palette file";

  public Save()
    : base(ResourceToImage("PaletteDesigner.Images.disk.png"))
  {
  }

  public override void OnMouseClick(object sender, EventArgs e)
  {
    base.OnMouseClick(sender, e);

    if (ExternalObject is Main main)
    {
      if (!string.IsNullOrEmpty(main.CurrentFileName))
      {
        main.Pallet.Save(main.CurrentFileName);
      }
      else
      {
        ToolStripItemGroupManager["Save &As"]?.OnMouseClick(sender, e);
      }
    }
  }
}
