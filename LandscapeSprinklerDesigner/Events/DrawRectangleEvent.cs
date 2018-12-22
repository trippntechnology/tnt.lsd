using LSDComponents.DrawingModes;

namespace LandscapeSprinklerDesigner.Events
{
	class DrawRectangleEvent : DrawEvent
	{
		public override DrawingMode DrawingMode => new RectangleMode() { Layer = 1 };

		public override string Text => "Rectangle";

		public override string ToolTipText => "Use the Rectangle tool to draw rectangles.";

		public DrawRectangleEvent()
			: base(ResourceToImage("LandscapeSprinklerDesigner.Images.rectangle.png"))
		{

		}
	}
}
