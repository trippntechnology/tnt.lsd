using TNT.LSD.Components.DrawingModes;

namespace LandscapeSprinklerDesigner.Events;

class DrawTextEvent : DrawEvent
{
  private TextMode textMode = new TextMode() { Layer = 1 };

  public override DrawingMode DrawingMode => textMode;

  public override string Text => "Text";

  public override string ToolTipText => "Use the Text tool to place notes on the layout.";

  public DrawTextEvent()
    : base(ResourceToImage("LandscapeSprinklerDesigner.Images.text.png"))
  {

  }
}
