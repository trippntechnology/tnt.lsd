using LSDComponents.DrawingModes;

namespace LandscapeSprinklerDesigner.Events
{
	class DrawSelectEvent : DrawEvent
	{
		public override DrawingMode DrawingMode => new SelectMode() { Layer = 1 };

		public override string Text => "Select";

		public override string ToolTipText => "Use the Select tool to select Landscape objects so that they can be manipulated.";

		public DrawSelectEvent()
			: base(ResourceToImage("LandscapeSprinklerDesigner.Images.hand.png"))
		{
		}
	}
}
