using TNT.LSD.Objects;

namespace LandscapeSprinklerDesigner.MenuEvents;

class SpaceEquallyMenuEvent : MenuEvent
{
  public override string Text => Resource.menu_space_equally;

  public override string ToolTipText => Resource.menu_space_equally_tooltip;

  public SpaceEquallyMenuEvent() : base(ResourceToImage("LandscapeSprinklerDesigner.Images.space_equally.png"))
  {
  }

  public override void OnApplicationIdle(object sender, EventArgs e)
  {
    this.Enabled = (from o in CAD.SelectedObjects where o is TNTPart select o).ToList().Count > 2;
  }

  public override void OnMouseClick(object sender, EventArgs e)
  {
    CAD.SpaceSelectedEqually();
  }
}
