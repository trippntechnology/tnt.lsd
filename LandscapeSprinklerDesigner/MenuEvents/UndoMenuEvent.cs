using TNT.LSD.Components;


namespace LandscapeSprinklerDesigner.MenuEvents;

class UndoMenuEvent() : MenuEvent(Resource.menu_undo, Resource.menu_undo_tooltip)
{
    public override void OnApplicationIdle(TNTCAD cad)
    {
        Enabled = cad.HasUnsavedChanges && cad.DrawingMode.UndoEnabled;
    }

    public override void OnMouseClicked(Form owner, TNTCAD cad, LayoutSettingsForm layoutSettings)
    {
        cad.Undo();
    }
}
