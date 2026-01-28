using PalletDesigner;

namespace PaletteDesigner.Events;

class ExportImage() : AppToolStripItemGroup("Export Image", "Export image associated with node")
{
    public override void OnMouseClick(Main main)
    {
        using (var sfd = new SaveFileDialog())
        {
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                main.Pallet.ExportImage(sfd.FileName);
            }
        }
    }
}
