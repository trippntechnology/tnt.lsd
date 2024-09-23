using TNT.Utilities;

namespace LandscapeSprinklerDesigner.MenuEvents;

class AutoSizeMenuEvent : PersistedMenuEvent
{
  private const string REGISTRY_KEY = "AutoPipeSize";

  public override bool CheckOnClick => true;

  public override string Text => Resource.menu_auto_size;

  public override string ToolTipText => Resource.menu_auto_size_tooltip;

  public AutoSizeMenuEvent() : base(ResourceToImage("LandscapeSprinklerDesigner.Images.auto_size.png"))
  {
  }

  public override void RestoreState(ApplicationRegistry applicationRegistry)
  {
    this.Checked = applicationRegistry.ReadBoolean(REGISTRY_KEY, true);
    OnMouseClick(null, null);
  }

  public override void SaveState(ApplicationRegistry applicationRegistry)
  {
    applicationRegistry.WriteBoolean(REGISTRY_KEY, this.Checked);
  }

  public override void OnMouseClick(object sender, EventArgs e)
  {
    CAD.DrawingOptions.AutoSizePipes = this.Checked;
  }
}
