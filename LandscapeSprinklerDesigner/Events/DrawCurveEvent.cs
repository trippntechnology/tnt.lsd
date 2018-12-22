using LSDComponents.DrawingModes;

namespace LandscapeSprinklerDesigner.Events
{
	class DrawCurveEvent : DrawEvent
	{
		public override DrawingMode DrawingMode => new BezierMode() { Layer = 1 };

		public override string Text => "Curve";

		public override string ToolTipText => "Use the Curve tool to draw curves.";

		public DrawCurveEvent()
			: base(ResourceToImage("LandscapeSprinklerDesigner.Images.curve.png"))
		{

		}
	}
}
