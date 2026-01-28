using PalletDesigner;

namespace PaletteDesigner.Events;

class SaveAs() : AppToolStripItemGroup("Save &As", "Save palette as")
{
    public override void OnMouseClick(Main main)
    {
        SaveFileDialog SaveDialog = main.SaveDialog;
        if (SaveDialog.ShowDialog() == DialogResult.OK)
        {
            main.Pallet.Save(SaveDialog.FileName);
            main.CurrentFileName = SaveDialog.FileName;
        }
    }
}
