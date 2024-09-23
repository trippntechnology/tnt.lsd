namespace LandscapeSprinklerDesigner.MenuEvents;

class DeleteMenuEvent : MenuEvent
{
  public override string Text => Resource.menu_delete;

  public override string ToolTipText => Resource.menu_delete_tooltip;

  public DeleteMenuEvent() : base(ResourceToImage("LandscapeSprinklerDesigner.Images.delete.png"))
  {

  }

  public override void OnMouseClick(object sender, EventArgs e)
  {
    base.OnMouseClick(sender, e);
    CAD.Delete();
  }

  public override void OnApplicationIdle(object sender, EventArgs e)
  {
    base.OnApplicationIdle(sender, e);
    Enabled = base.HasSelectedObjects;
  }
}
