using TNT.LSD.Components.DrawingModes;

namespace LandscapeSprinklerDesigner.Events;

class DrawLineEvent : DrawEvent
{
  private LineMode lineMode = new LineMode() { Layer = 1 };

  public override DrawingMode DrawingMode => lineMode;

  public override string Text => "Line";

  public override string ToolTipText => "Use the Line tool to draw a continuous line. Double click to end the line.";

  public DrawLineEvent()
    : base(ResourceToImage("LandscapeSprinklerDesigner.Images.line.png"))
  {

  }
}
