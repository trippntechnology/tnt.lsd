namespace LandscapeSprinklerDesigner.MenuEvents;

class SaveAsMenuEvent : MenuEvent
{
  public override string Text => Resource.menu_save_as;

  public override string ToolTipText => Resource.menu_save_as_tooltip;

  public override void OnMouseClick(object sender, EventArgs e)
  {
    base.OnMouseClick(sender, e);
    CAD.Save(true);
  }
}
