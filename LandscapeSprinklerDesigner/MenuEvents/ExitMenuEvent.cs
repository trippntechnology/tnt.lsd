namespace LandscapeSprinklerDesigner.MenuEvents;

class ExitMenuEvent : MenuEvent
{
  public override string Text => Resource.menu_exit;

  public override string ToolTipText => Resource.menu_exit;

  public override void OnMouseClick(object sender, EventArgs e)
  {
    base.OnMouseClick(sender, e);
    Owner.Close();
  }
}
