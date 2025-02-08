namespace LandscapeSprinklerDesigner.MenuEvents;

class LayoutSettingsEvent : DockMenuEvent
{

  public override string Text => Resource.menu_layout_settings;

  public override string ToolTipText => Resource.menu_layout_settings_tooltip;

  public LayoutSettingsEvent() : base(ResourceToImage("LandscapeSprinklerDesigner.Images.layout_settings.png"))
  {
  }
}
