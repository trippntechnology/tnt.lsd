namespace LandscapeSprinklerDesigner.MenuEvents;

class ShowPartsMenuEvent : MenuEvent
{
  public override bool CheckOnClick => true;

  public override string Text => Resource.menu_show_parts;

  public override string ToolTipText => Resource.menu_show_parts_tooltip;

  public ShowPartsMenuEvent() : base(ResourceToImage("LandscapeSprinklerDesigner.Images.show_parts.png"))
  {
  }

  public override void OnMouseClick(object? sender, EventArgs e)
  {
    CAD.ShowPartsToolTip = this.Checked;
  }
}