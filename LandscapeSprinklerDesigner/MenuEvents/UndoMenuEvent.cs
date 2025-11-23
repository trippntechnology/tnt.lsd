using TNT.LSD.Components;
using TNT.ToolStripItemManager.Extension;

namespace LandscapeSprinklerDesigner.MenuEvents;

class UndoMenuEvent() : MenuEvent(Resource.menu_undo, Resource.menu_undo_tooltip, image: "LandscapeSprinklerDesigner.Images.undo.png".ToImage())
{
    public override void OnApplicationIdle(TNTCAD cad)
    {
        Enabled = cad.HasUnsavedChanges && cad.DrawingMode.UndoEnabled;
    }

    public override void OnMouseClicked(Form owner, TNTCAD cad)
    {
        cad.Undo();
    }
}
