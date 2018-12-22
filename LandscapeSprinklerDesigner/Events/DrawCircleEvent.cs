using LSDComponents.DrawingModes;

namespace LandscapeSprinklerDesigner.Events
{
	class DrawCircleEvent : DrawEvent
	{
		public override DrawingMode DrawingMode => new CircleMode() { Layer = 1 };

		public override string Text => "Circle";

		public override string ToolTipText => "Use the Circle tool to draw circles.";

		public DrawCircleEvent()
			: base(ResourceToImage("LandscapeSprinklerDesigner.Images.circle.png"))
		{

		}
	}
}
