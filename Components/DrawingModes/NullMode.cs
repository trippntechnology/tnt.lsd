
namespace LSDComponents.DrawingModes
{
	// This mode is assigned to nodes that do not perform any action such as root nodes in the Pallet Tree.
	public class NullMode : DrawingMode
	{
		public NullMode()
			: base()
		{
		}

		public NullMode(NullMode obj)
			: base(obj)
		{
		}

		public override DrawingMode Clone()
		{
			return new NullMode(this);
		}
	}
}
