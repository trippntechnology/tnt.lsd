namespace LandscapeSprinklerDesigner.MenuEvents;

class PropertiesMenuEvent : DockMenuEvent
{
  public override string Text => Resource.menu_properties;

  public override string ToolTipText => Resource.menu_parts_palette_tooltip;

  public PropertiesMenuEvent() : base(ResourceToImage("LandscapeSprinklerDesigner.Images.properties.png"))
  {
  }
}
