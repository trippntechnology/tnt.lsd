using PalletDesigner;
using TNT.ToolStripItemManager;

namespace PaletteDesigner.Events;

class SaveAs : ToolStripItemGroup
{
  public override string Text => "Save &As";

  public override string ToolTipText => "Save palette as";

  public SaveAs()
    : base()
  {
  }

  public override void OnMouseClick(object sender, EventArgs e)
  {
    base.OnMouseClick(sender, e);

    if (ExternalObject is Main main)
    {
      using (SaveFileDialog SaveDialog = new SaveFileDialog())
      {
        if (SaveDialog.ShowDialog() == DialogResult.OK)
        {
          main.Pallet.Save(SaveDialog.FileName);
          main.CurrentFileName = SaveDialog.FileName;
        }
      }
    }
  }
}
