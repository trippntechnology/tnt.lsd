using LandscapeSprinklerDesigner.Properties;
using System;

namespace LandscapeSprinklerDesigner.MenuEvents
{
	class MoveToBackMenuEvent : MenuEvent
	{
		public override string Text => Resources.menu_move_to_back;

		public override string ToolTipText => Resources.menu_move_to_back_tooltip;

		public MoveToBackMenuEvent() : base(ResourceToImage("LandscapeSprinklerDesigner.Images.move_to_back.png"))
		{
		}

		public override void OnApplicationIdle(object sender, EventArgs e)
		{
			this.Enabled = CAD.SelectedObjects.Count > 0;
		}

		public override void MouseClick(object sender, EventArgs e)
		{
			CAD.SendToBack();
		}
	}
}
