using TNT.LSD.Components;
using TNT.ToolStripItemManager.Extension;
using TNT.Utilities;

namespace LandscapeSprinklerDesigner.MenuEvents;

class LabelHeadsMenuEvent() : PersistedMenuEvent(Resource.menu_label_heads, Resource.menu_label_heads_tooltip, checkOnClick: true, image: "LandscapeSprinklerDesigner.Images.label_heads.png".ToImage())
{
    private const string REGISTRY_KEY = "LabelHeads";

    public override void RestoreState(ApplicationRegistry applicationRegistry, TNTCAD cad)
    {
        this.Checked = applicationRegistry.ReadBoolean(REGISTRY_KEY, true);
        cad.DrawingOptions.LabelHeads = Checked;
        cad.Repaint();
    }

    public override void SaveState(ApplicationRegistry applicationRegistry)
    {
        applicationRegistry.WriteBoolean(REGISTRY_KEY, this.Checked);
    }

    public override void OnMouseClicked(Form owner, TNTCAD cad, LayoutSettingsForm layoutSettings)
    {
        cad.DrawingOptions.LabelHeads = Checked;
        cad.Repaint();
    }
}