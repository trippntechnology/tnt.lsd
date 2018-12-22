using LSDComponents.DrawingModes;

namespace LandscapeSprinklerDesigner.Events
{
	class DrawTextEvent : DrawEvent
	{
		public override DrawingMode DrawingMode => new TextMode() { Layer = 1 };

		public override string Text => "Text";

		public override string ToolTipText => "Use the Text tool to place notes on the layout.";

		public DrawTextEvent()
			: base(ResourceToImage("LandscapeSprinklerDesigner.Images.text.png"))
		{

		}
	}
}
