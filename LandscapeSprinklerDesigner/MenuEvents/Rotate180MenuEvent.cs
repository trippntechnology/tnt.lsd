namespace LandscapeSprinklerDesigner.MenuEvents;

class Rotate180MenuEvent : RotateMenuEvent
{
  public override string Text => Resource.menu_rotate_180;

  public override string ToolTipText => Resource.menu_rotate_180_tooltip;

  public override int Angle => 180;

  public Rotate180MenuEvent() : base(ResourceToImage("LandscapeSprinklerDesigner.Images.rotate_180.png"))
  {
  }
}
