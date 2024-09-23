namespace LandscapeSprinklerDesigner.MenuEvents;

class AlignToGridMenuEvent : MenuEvent
{
  public override string Text => Resource.menu_align_to_grid;

  public override string ToolTipText => Resource.menu_align_to_grid_tooltip;

  public AlignToGridMenuEvent() : base(ResourceToImage("LandscapeSprinklerDesigner.Images.align_to_grid.png"))
  {
  }

  public override void OnApplicationIdle(object sender, EventArgs e)
  {
    this.Enabled = CAD.SelectedObjects.Count > 0;
  }

  public override void OnMouseClick(object sender, EventArgs e)
  {
    CAD.AlignToGrid();
  }
}
