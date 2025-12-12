using Microsoft.Win32;
using TNT.Commons;
using TNT.LSD.Components;

using TNT.Utilities;

namespace LandscapeSprinklerDesigner.MenuEvents;

public delegate void LoadLayoutDelegate(string fileName);

class OpenMenuEvent() : MenuEvent(Resource.menu_open, Resource.menu_open_tooltip)
{
    private readonly OpenFileDialog openFileDialog = InitializeOpenFileDialog();
    private readonly ApplicationRegistry applicationRegistry = new ApplicationRegistry(Registry.CurrentUser, "Tripp'n Technology", "LandscapeSprinklerDesigner");

    public LoadLayoutDelegate? LoadLayout { get; set; }

    private static OpenFileDialog InitializeOpenFileDialog()
    {
        var dialog = new OpenFileDialog();
        dialog.DefaultExt = "lsd";
        dialog.Filter = "Landscape Sprinkler Design files|*.lsd;*.lsdx";
        dialog.RestoreDirectory = true;
        dialog.Title = Resource.menu_open_tooltip;
        return dialog;
    }

    public override void OnMouseClicked(Form owner, TNTCAD cad, LayoutSettingsForm layoutSettings)
    {
        openFileDialog.InitialDirectory = applicationRegistry.ReadString("InitialDirectory", string.Empty);

        if (HandleUnsavedChanges(cad) && openFileDialog.ShowDialog() == DialogResult.OK)
        {
            LoadLayout?.Invoke(openFileDialog.FileName);
            Path.GetDirectoryName(openFileDialog.FileName)?.Also(directory => applicationRegistry.WriteString("InitialDirectory", directory));
        }
    }
}
