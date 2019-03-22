using System;
using System.Drawing;
using TNT.ToolStripItemManager;
using WeifenLuo.WinFormsUI.Docking;

namespace LandscapeSprinklerDesigner.MenuEvents
{
	abstract class CheckableMenuEvent : ToolStripItemGroup
	{
		public override bool CheckOnClick => true;
		protected Tuple<DockContent, DockPanel> tuple => base.ExternalObject as Tuple<DockContent, DockPanel>;
		protected DockContent dockContent => tuple.Item1 as DockContent;
		protected DockPanel dockPanel => tuple.Item2 as DockPanel;

		public CheckableMenuEvent(Image image = null) : base(image)
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
