using LandscapeSprinklerDesigner.Properties;
using System;

namespace LandscapeSprinklerDesigner.MenuEvents
{
	class ShowCoverageMenuEvent : MenuEvent
	{
		public override bool CheckOnClick => true;

		public override string Text => Resources.menu_show_coverage;

		public override string ToolTipText => Resources.menu_show_coverage_tooltip;

		public ShowCoverageMenuEvent() : base(ResourceToImage("LandscapeSprinklerDesigner.Images.show_coverage.png"))
		{
		}

		public override void OnMouseClick(object sender, EventArgs e)
		{
			CAD.DrawingOptions.ShowCoverage = this.Checked;
			CAD.Repaint();
		}
	}
}
