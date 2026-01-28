using PalletDesigner;

namespace PaletteDesigner.Events;

class Exit() : AppToolStripItemGroup("Exit", "Exit")
{
    public override void OnMouseClick(Main main)
    {
        Application.Exit();
    }
}
