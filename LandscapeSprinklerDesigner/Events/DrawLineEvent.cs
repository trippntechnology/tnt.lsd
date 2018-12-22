using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LSDComponents.DrawingModes;

namespace LandscapeSprinklerDesigner.Events
{
	class DrawLineEvent : DrawEvent
	{
		public override DrawingMode DrawingMode => new LineMode() { Layer = 1 };

		public override string Text => "Line";

		public override string ToolTipText => "Use the Line tool to draw a continuous line. Double click to end the line.";

		public DrawLineEvent()
			: base(ResourceToImage("LandscapeSprinklerDesigner.Images.line.png"))
		{

		}
	}
}
