using TNT.LSD.Objects;

namespace TNT.LSD.Components.DrawingModes;

public class MainlinePipeMode : PipeMode<MainlinePipe>
{
  public MainlinePipeMode()
    : base()
  {
  }

  public MainlinePipeMode(MainlinePipeMode obj)
    : base(obj)
  {
  }

  public override DrawingMode Clone()
  {
    return new MainlinePipeMode(this);
  }
}
