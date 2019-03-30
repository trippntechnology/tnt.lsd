using LandscapeSprinklerDesigner.Properties;
using System;
using TNT.Utilities;

namespace LandscapeSprinklerDesigner.MenuEvents
{
	class LabelHeadsMenuEvent : PersistedMenuEvent
	{
		private const string REGISTRY_KEY = "LabelHeads";

		public override bool CheckOnClick => true;

		public override string Text => Resources.menu_label_heads;

		public override string ToolTipText => Resources.menu_label_heads_tooltip;

		public LabelHeadsMenuEvent() : base(ResourceToImage("LandscapeSprinklerDesigner.Images.label_heads.png"))
		{

		}

		public override void RestoreState(ApplicationRegistry applicationRegistry)
		{
			this.Checked = applicationRegistry.ReadBoolean(REGISTRY_KEY, true);
			MouseClick(null, null);
		}

		public override void SaveState(ApplicationRegistry applicationRegistry)
		{
			applicationRegistry.WriteBoolean(REGISTRY_KEY, this.Checked);
		}

		public override void MouseClick(object sender, EventArgs e)
		{
			CAD.DrawingOptions.LabelHeads = this.Checked;
			CAD.Repaint();
		}
	}
}