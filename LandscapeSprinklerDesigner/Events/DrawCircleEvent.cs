using LSDComponents.DrawingModes;

namespace LandscapeSprinklerDesigner.Events
{
	class DrawCircleEvent : DrawEvent
	{
		private CircleMode circleMode = new CircleMode() { Layer = 1 };

		public override DrawingMode DrawingMode => circleMode;

		public override string Text => "Circle";

		public override string ToolTipText => "Use the Circle tool to draw circles.";

		public DrawCircleEvent()
			: base(ResourceToImage("LandscapeSprinklerDesigner.Images.circle.png"))
		{

		}
	}
}
