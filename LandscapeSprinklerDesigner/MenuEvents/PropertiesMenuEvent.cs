using LandscapeSprinklerDesigner.Properties;
using System;
using TNT.ToolStripItemManager;
using WeifenLuo.WinFormsUI.Docking;

namespace LandscapeSprinklerDesigner.MenuEvents
{
	class PropertiesMenuEvent : ToolStripItemGroup
	{
		public override string Text => Resources.menu_properties;

		public override string ToolTipText => Resources.menu_properties_tooltip;

		public override bool CheckOnClick => true;

		protected Tuple<DockContent, DockPanel> tuple => base.ExternalObject as Tuple<DockContent, DockPanel>;

		protected DockContent dockContent => tuple.Item1 as DockContent;
		protected DockPanel dockPanel => tuple.Item2 as DockPanel;

		public PropertiesMenuEvent() : base(ResourceToImage("LandscapeSprinklerDesigner.Images.properties.png"))
		{

		}

		public override void OnApplicationIdle(object sender, EventArgs e)
		{
			base.OnApplicationIdle(sender, e);
			base.Checked = !dockContent.IsHidden;

		}

		public override void MouseClick(object sender, EventArgs e)
		{
			base.MouseClick(sender, e);

			if (base.Checked)
			{
				dockContent.Show(dockPanel);
			}
			else
			{
				dockContent.Hide();
			}
		}
	}
}
