using TNT.LSD.Objects;

namespace LSDComponents.DrawingModes
{
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
}
