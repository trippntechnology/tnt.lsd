namespace LandscapeSprinklerDesigner.MenuEvents;

class PartsPaletteEvent : DockMenuEvent
{
  public override string Text => Resource.menu_parts_palette;

  public override string ToolTipText => Resource.menu_parts_palette_tooltip;

  public PartsPaletteEvent() : base(ResourceToImage("LandscapeSprinklerDesigner.Images.parts_palette.png"))
  {
  }
}
