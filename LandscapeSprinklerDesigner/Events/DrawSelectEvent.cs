using TNT.LSD.Components.DrawingModes;

namespace LandscapeSprinklerDesigner.Events;

class DrawSelectEvent : DrawEvent
{
  private TNT.LSD.Components.DrawingModes.SelectMode selectMode = new TNT.LSD.Components.DrawingModes.SelectMode() { Layer = 1 };

  public override DrawingMode DrawingMode => selectMode;

  public override string Text => "Select";

  public override string ToolTipText => "Use the Select tool to select Landscape objects so that they can be manipulated.";

  public DrawSelectEvent()
    : base(ResourceToImage("LandscapeSprinklerDesigner.Images.hand.png"))
  {
  }
}
