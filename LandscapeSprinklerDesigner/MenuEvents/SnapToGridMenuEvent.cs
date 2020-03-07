using LandscapeSprinklerDesigner.Properties;
using System;
using TNT.Utilities;

namespace LandscapeSprinklerDesigner.MenuEvents
{
	class SnapToGridMenuEvent : PersistedMenuEvent
	{
		private const string REGISTRY_KEY = "SnapToGrid";

		public override bool CheckOnClick => true;

		public override string Text => Resources.menu_snap_to_grid;

		public override string ToolTipText => Resources.menu_snap_to_grid_tooltip;

		public SnapToGridMenuEvent() : base(ResourceToImage("LandscapeSprinklerDesigner.Images.snap_to_grid.png"))
		{

		}

		public override void RestoreState(ApplicationRegistry applicationRegistry)
		{
			this.Checked = applicationRegistry.ReadBoolean(REGISTRY_KEY, true);
			OnMouseClick(null, null);
		}

		public override void SaveState(ApplicationRegistry applicationRegistry)
		{
			applicationRegistry.WriteBoolean(REGISTRY_KEY, this.Checked);
		}

		public override void OnMouseClick(object sender, EventArgs e)
		{
			CAD.SnapToGrid = this.Checked;
		}
	}
}
