using LSDComponents.DrawingModes;

namespace LandscapeSprinklerDesigner.Events
{
	class DrawPolyEvent : DrawEvent
	{
		public override DrawingMode DrawingMode => new PolyLineMode() { Layer = 1 };

		public override string Text => "Poly Line";

		public override string ToolTipText => "Use the Poly Line tool to draw a closed continuous line. Double click to end the line.";

		public DrawPolyEvent() 
			: base(ResourceToImage("LandscapeSprinklerDesigner.Images.polyline.png"))
		{

		}
	}
}
