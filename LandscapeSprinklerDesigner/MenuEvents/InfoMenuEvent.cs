using TNT.LSD.Objects.Interfaces;

namespace LandscapeSprinklerDesigner.MenuEvents;

class InfoMenuEvent : MenuEvent
{
  public override string Text => Resource.menu_info;

  public override string ToolTipText => Resource.menu_info_tooltip;

  public InfoMenuEvent() : base(ResourceToImage("LandscapeSprinklerDesigner.Images.info.png"))
  {

  }

  public override void OnApplicationIdle(object? sender, EventArgs e)
  {
    Enabled = (from o in CAD.SelectedObjects where o is ISummable select o as ISummable).ToList().Count > 0;
  }

  public override void OnMouseClick(object? sender, EventArgs e)
  {
    var summableParts = (from o in CAD.SelectedObjects where o is ISummable select o as ISummable).ToList();
    double gpm = summableParts.Sum(p => p.GPM);
    MessageBox.Show(this.Owner, $"Part Count: {summableParts.Count}\nGPM: {gpm}", "Selected GPM");
  }
}
