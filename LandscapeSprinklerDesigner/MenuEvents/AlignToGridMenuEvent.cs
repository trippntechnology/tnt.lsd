using LandscapeSprinklerDesigner.Properties;
using System;

namespace LandscapeSprinklerDesigner.MenuEvents
{
	class AlignToGridMenuEvent : MenuEvent
	{
		public override string Text => Resources.menu_align_to_grid;

		public override string ToolTipText => Resources.menu_align_to_grid_tooltip;

		public AlignToGridMenuEvent() : base(ResourceToImage("LandscapeSprinklerDesigner.Images.align_to_grid.png"))
		{
		}

		public override void OnApplicationIdle(object sender, EventArgs e)
		{
			this.Enabled = CAD.SelectedObjects.Count > 0;
		}

		public override void MouseClick(object sender, EventArgs e)
		{
			CAD.AlignToGrid();
		}
	}
}
