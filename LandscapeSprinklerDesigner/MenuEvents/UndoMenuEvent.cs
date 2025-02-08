namespace LandscapeSprinklerDesigner.MenuEvents;

class UndoMenuEvent : MenuEvent
{
  public override string Text => Resource.menu_undo;

  public override string ToolTipText => Resource.menu_undo_tooltip;

  public UndoMenuEvent()
    : base(ResourceToImage("LandscapeSprinklerDesigner.Images.undo.png"))
  {

  }

  public override void OnApplicationIdle(object sender, EventArgs e)
  {
    base.OnApplicationIdle(sender, e);
    Enabled = CAD.HasUnsavedChanges && CAD.DrawingMode.UndoEnabled;
  }

  public override void OnMouseClick(object sender, EventArgs e)
  {
    base.OnMouseClick(sender, e);
    CAD.Undo();
  }
}
