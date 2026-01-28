using PalletDesigner;

namespace PaletteDesigner.Events;

class AddImage() : AppToolStripItemGroup("Add Image", "Add image to node")
{
    public void OnMouseClick(Main main)
    {
        using (OpenFileDialog ofd = new OpenFileDialog())
        {
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                main.Pallet.AddImage(ofd.FileName);
            }
        }
    }
}
