using PalletDesigner;

namespace PaletteDesigner.Events;

class Save() : AppToolStripItemGroup("&Save", "Save a palette file")
{
    public override void OnMouseClick(Main main)
    {
        if (!string.IsNullOrEmpty(main.CurrentFileName))
        {
            main.Pallet.Save(main.CurrentFileName);
        }
        else
        {
            //ToolStripItemGroupManager["Save &As"]?.OnMouseClick(sender, e);
        }
    }
}
