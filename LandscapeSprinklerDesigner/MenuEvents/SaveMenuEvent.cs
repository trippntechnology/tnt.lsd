namespace LandscapeSprinklerDesigner.MenuEvents;

class SaveMenuEvent : MenuEvent
{
  public override string Text => Resource.menu_save;

  public override string ToolTipText => Resource.menu_save_tooltip;

  public SaveMenuEvent() : base(ResourceToImage("LandscapeSprinklerDesigner.Images.save.png"))
  {
  }

  public override void OnMouseClick(object sender, EventArgs e)
  {
    base.OnMouseClick(sender, e);
    CAD.Save(false);
  }
}
