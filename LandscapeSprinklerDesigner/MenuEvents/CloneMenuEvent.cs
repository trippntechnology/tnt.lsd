using LandscapeSprinklerDesigner.Properties;
using System;
using System.Linq;

namespace LandscapeSprinklerDesigner.MenuEvents
{
	class CloneMenuEvent : MenuEvent
	{
		public override string Text => Resources.menu_clone;

		public override string ToolTipText => Resources.menu_clone_tooltip;

		public CloneMenuEvent() : base(ResourceToImage("LandscapeSprinklerDesigner.Images.clone.png"))
		{

		}

		public override void OnApplicationIdle(object sender, EventArgs e)
		{
			base.OnApplicationIdle(sender, e);
			Enabled = (from o in CAD.SelectedObjects where o.CanClone select o).ToList().Count > 0;
		}

		public override void OnMouseClick(object sender, EventArgs e)
		{
			base.OnMouseClick(sender, e);
			CAD.Copy();
		}
	}
}
