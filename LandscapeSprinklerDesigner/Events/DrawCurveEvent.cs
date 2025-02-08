
using TNT.LSD.Components.DrawingModes;

namespace LandscapeSprinklerDesigner.Events;

class DrawCurveEvent : DrawEvent
{
  private BezierMode bezierMode = new BezierMode() { Layer = 1 };

  public override DrawingMode DrawingMode => bezierMode;

  public override string Text => "Curve";

  public override string ToolTipText => "Use the Curve tool to draw curves.";

  public DrawCurveEvent()
    : base(ResourceToImage("LandscapeSprinklerDesigner.Images.curve.png"))
  {

  }
}
