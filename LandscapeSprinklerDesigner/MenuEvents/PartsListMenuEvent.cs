namespace LandscapeSprinklerDesigner.MenuEvents;

class PartsListMenuEvent : DockMenuEvent
{
  public override string Text => Resource.menu_parts_listing;

  public override string ToolTipText => Resource.menu_parts_listing_tooltip;

  public PartsListMenuEvent() : base(ResourceToImage("LandscapeSprinklerDesigner.Images.parts_list.png"))
  {
  }
}
