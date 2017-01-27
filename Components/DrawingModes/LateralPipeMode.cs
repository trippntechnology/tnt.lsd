using TNT.LSD.Objects;

namespace LSDComponents.DrawingModes
{
	public class LateralPipeMode : PipeMode<LateralPipe>
	{
		public LateralPipeMode()
			: base()
		{
		}

		public LateralPipeMode(LateralPipeMode obj)
			: base(obj)
		{
		}

		public override DrawingMode Clone()
		{
			return new LateralPipeMode(this);
		}
	}
}
