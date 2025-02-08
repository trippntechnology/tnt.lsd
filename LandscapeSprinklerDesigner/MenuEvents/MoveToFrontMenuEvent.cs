namespace LandscapeSprinklerDesigner.MenuEvents;

class MoveToFrontMenuEvent : MenuEvent
{
  public override string Text => Resource.menu_move_to_front;

  public override string ToolTipText => Resource.menu_move_to_front_tooltip;

  public MoveToFrontMenuEvent() : base(ResourceToImage("LandscapeSprinklerDesigner.Images.move_to_front.png"))
  {
  }

  public override void OnApplicationIdle(object sender, EventArgs e)
  {
    this.Enabled = CAD.SelectedObjects.Count > 0;
  }

  public override void OnMouseClick(object sender, EventArgs e)
  {
    CAD.BringToFront();
  }
}
