namespace LandscapeSprinklerDesigner.MenuEvents;

class CheckForUpdateMenuItem : MenuEvent
{
  public override string Text => Resource.menu_check_for_update;

  public override string ToolTipText => Resource.menu_check_for_update_tooltip;

  public override void OnApplicationIdle(object sender, EventArgs e)
  {
    this.Enabled = Global.GetLicense() != null;
  }

  public override void OnMouseClick(object sender, EventArgs e)
  {
    Global.CheckForUpdate(base.Owner, false);
  }
}
