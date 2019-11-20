using LandscapeSprinklerDesigner.Properties;
using System;
using TNT.Utilities;

namespace LandscapeSprinklerDesigner.MenuEvents
{
	class ShowDistanceMenuEvent : PersistedMenuEvent
	{
		private const string REGISTRY_KEY = "AlwaysShowDistances";

		public override bool CheckOnClick => true;

		public override string Text => Resources.menu_show_distances;

		public override string ToolTipText => Resources.menu_show_distance_tooltip;

		public ShowDistanceMenuEvent() : base(ResourceToImage("LandscapeSprinklerDesigner.Images.show_distance.png"))
		{
		}

		public override void RestoreState(ApplicationRegistry applicationRegistry)
		{
			this.Checked = applicationRegistry.ReadBoolean(REGISTRY_KEY, false);
			OnMouseClick(null, null);
		}

		public override void SaveState(ApplicationRegistry applicationRegistry)
		{
			applicationRegistry.WriteBoolean(REGISTRY_KEY, this.Checked);
		}

		public override void OnMouseClick(object sender, EventArgs e)
		{
			CAD.DrawingOptions.AlwaysShowDistances = this.Checked;
			CAD.Repaint();
		}
	}
}
