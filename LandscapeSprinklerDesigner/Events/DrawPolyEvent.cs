using LSDComponents.DrawingModes;

namespace LandscapeSprinklerDesigner.Events
{
	class DrawPolyEvent : DrawEvent
	{
		private PolyLineMode polyLineMode = new PolyLineMode() { Layer = 1 };

		public override DrawingMode DrawingMode => polyLineMode;

		public override string Text => "Poly Line";

		public override string ToolTipText => "Use the Poly Line tool to draw a closed continuous line. Double click to end the line.";

		public DrawPolyEvent()
			: base(ResourceToImage("LandscapeSprinklerDesigner.Images.polyline.png"))
		{

		}
	}
}
