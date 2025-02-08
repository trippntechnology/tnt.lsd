namespace LandscapeSprinklerDesigner.MenuEvents;

class MoveToBackMenuEvent : MenuEvent
{
  public override string Text => Resource.menu_move_to_back;

  public override string ToolTipText => Resource.menu_move_to_back_tooltip;

  public MoveToBackMenuEvent() : base(ResourceToImage("LandscapeSprinklerDesigner.Images.move_to_back.png"))
  {
  }

  public override void OnApplicationIdle(object sender, EventArgs e)
  {
    this.Enabled = CAD.SelectedObjects.Count > 0;
  }

  public override void OnMouseClick(object sender, EventArgs e)
  {
    CAD.SendToBack();
  }
}
