using TNT.LSD.Components.DrawingModes;

namespace LandscapeSprinklerDesigner.Events;

class DrawRectangleEvent : DrawEvent
{
  private RectangleMode rectangleMode = new RectangleMode() { Layer = 1 };

  public override DrawingMode DrawingMode => rectangleMode;

  public override string Text => "Rectangle";

  public override string ToolTipText => "Use the Rectangle tool to draw rectangles.";

  public DrawRectangleEvent()
    : base(ResourceToImage("LandscapeSprinklerDesigner.Images.rectangle.png"))
  {

  }
}
