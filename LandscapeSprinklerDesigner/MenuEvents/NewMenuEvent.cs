using TNT.LSD.Components;
using TNT.LSD.Objects;


namespace LandscapeSprinklerDesigner.MenuEvents;

class NewMenuEvent() : MenuEvent(Resource.menu_new, Resource.menu_new_tooltip)
{
    public override void OnMouseClicked(Form owner, TNTCAD cad, LayoutSettingsForm layoutSettings)
    {
        if (HandleUnsavedChanges(cad))
        {
            NewLayoutDialog nld = new NewLayoutDialog();
            TNTCADState state = new TNTCADState(cad);

            for (int index = 0; index < cad.State.ObjectLayers.Count; index++)
            {
                state.ObjectLayers.Add(new List<TNTObject>());
            }

            if (state.Settings != null && nld.ShowDialog(owner, state.Settings) == DialogResult.OK)
            {
                cad.State = state;
                cad.CurrentFileName = string.Empty;
            }

            cad.Settings.DrawGrid = Manager?.Find(m => m is ShowGridMenuEvent)?.Checked ?? false;
            layoutSettings.Settings = cad.Settings;
            cad.Refresh();
        }
    }
}
