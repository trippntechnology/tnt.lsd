using PalletDesigner;
using TNT.ToolStripItemManager;

namespace PaletteDesigner.Events;

public class Open : ToolStripItemGroup
{
  public override string Text => "&Open";

  public override string ToolTipText => "Open a palette file";

  public Open()
    : base(ResourceToImage("PaletteDesigner.Images.folder.png"))
  {
  }

  public override void OnMouseClick(object sender, EventArgs e)
  {
    base.OnMouseClick(sender, e);

    if (ExternalObject is Main main)
    {
      OpenFileDialog ofd = main.OpenDialog;

      if (ofd.ShowDialog() == DialogResult.OK)
      {
        main.Pallet.Load(ofd.FileName);
        main.CurrentFileName = ofd.FileName;
      }
    }
  }
}
