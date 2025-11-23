using TNT.LSD.Components;
using TNT.ToolStripItemManager.Extension;
using TNT.Utilities;

namespace LandscapeSprinklerDesigner.MenuEvents;

class AutoSizeMenuEvent() : PersistedMenuEvent(Resource.menu_auto_size, Resource.menu_auto_size_tooltip, checkOnClick: true, image: "LandscapeSprinklerDesigner.Images.auto_size.png".ToImage())
{
    private const string REGISTRY_KEY = "AutoPipeSize";

    public override void RestoreState(ApplicationRegistry applicationRegistry, TNTCAD cad)
    {
        Checked = applicationRegistry.ReadBoolean(REGISTRY_KEY, true);
        cad.DrawingOptions.AutoSizePipes = Checked;
    }

    public override void SaveState(ApplicationRegistry applicationRegistry)
    {
        applicationRegistry.WriteBoolean(REGISTRY_KEY, this.Checked);
    }

    public override void OnMouseClicked(Form owner, TNTCAD cad)
    {
        cad.DrawingOptions.AutoSizePipes = Checked;
    }
}
