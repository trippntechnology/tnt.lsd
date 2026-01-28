using PalletDesigner;

namespace PaletteDesigner.Events;

public class Open() : AppToolStripItemGroup("&Open", "Open a palette file")
{
    public override void OnMouseClick(Main main)
    {
        OpenFileDialog ofd = main.OpenDialog;

        if (ofd.ShowDialog() == DialogResult.OK)
        {
            main.Pallet.Load(ofd.FileName);
            main.CurrentFileName = ofd.FileName;
        }
    }
}
