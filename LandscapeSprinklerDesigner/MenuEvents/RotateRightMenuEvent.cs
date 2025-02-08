namespace LandscapeSprinklerDesigner.MenuEvents;

class RotateRightMenuEvent : RotateMenuEvent
{
  public override string Text => Resource.menu_rotate_right;

  public override string ToolTipText => Resource.menu_rotate_right_tooltip;

  public override int Angle => 90;

  public RotateRightMenuEvent() : base(ResourceToImage("LandscapeSprinklerDesigner.Images.rotate_right.png"))
  {
  }
}
