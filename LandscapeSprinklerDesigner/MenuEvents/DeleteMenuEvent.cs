using System;
using LandscapeSprinklerDesigner.Properties;

namespace LandscapeSprinklerDesigner.MenuEvents
{
	class DeleteMenuEvent : MenuEvent
	{
		public override string Text => Resources.menu_delete;

		public override string ToolTipText => Resources.menu_delete_tooltip;

		public DeleteMenuEvent() : base(ResourceToImage("LandscapeSprinklerDesigner.Images.delete.png"))
		{

		}

		public override void MouseClick(object sender, EventArgs e)
		{
			base.MouseClick(sender, e);
			CAD.Delete();
		}

		public override void OnApplicationIdle(object sender, EventArgs e)
		{
			base.OnApplicationIdle(sender, e);
			Enabled = base.HasSelectedObjects;
		}
	}
}
