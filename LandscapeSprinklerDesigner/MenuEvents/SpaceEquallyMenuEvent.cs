using LandscapeSprinklerDesigner.Properties;
using System;
using System.Linq;
using TNT.LSD.Objects;

namespace LandscapeSprinklerDesigner.MenuEvents
{
	class SpaceEquallyMenuEvent : MenuEvent
	{
		public override string Text => Resources.menu_space_equally;

		public override string ToolTipText => Resources.menu_space_equally_tooltip;

		public SpaceEquallyMenuEvent() : base(ResourceToImage("LandscapeSprinklerDesigner.Images.space_equally.png"))
		{
		}

		public override void OnApplicationIdle(object sender, EventArgs e)
		{
			this.Enabled = (from o in CAD.SelectedObjects where o is TNTPart select o).ToList().Count > 2;
		}

		public override void OnMouseClick(object sender, EventArgs e)
		{
			CAD.SpaceSelectedEqually();
		}
	}
}
