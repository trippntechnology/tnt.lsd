namespace LandscapeSprinklerDesigner.MenuEvents;

class SelectAllMenuEvent : MenuEvent
{
  public override string Text => Resource.menu_select_all;

  public override string ToolTipText => Resource.menu_select_all_tooltip;

  public override void OnApplicationIdle(object sender, EventArgs e)
  {
    base.OnApplicationIdle(sender, e);
    Enabled = CAD.DrawingMode.GetType() == typeof(TNT.LSD.Components.DrawingModes.SelectMode);
  }

  public override void OnMouseClick(object sender, EventArgs e)
  {
    base.OnMouseClick(sender, e);
    if (CAD.DrawingMode.GetType() == typeof(TNT.LSD.Components.DrawingModes.SelectMode))
    {
      CAD.SelectAll();
    }
  }
}
