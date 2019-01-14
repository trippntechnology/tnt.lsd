using LSDComponents.DrawingModes;

namespace LandscapeSprinklerDesigner.Events
{
	class DrawLegendEvent : DrawEvent
	{
		private LegendMode legendMode = new LegendMode() { Layer = 1 };

		public override DrawingMode DrawingMode => legendMode;

		public override string Text => "Draw legend";

		public override string ToolTipText => "Draw a legend";

		public DrawLegendEvent()
			: base(ResourceToImage("LandscapeSprinklerDesigner.Images.legend.png"))
		{

		}
	}
}
