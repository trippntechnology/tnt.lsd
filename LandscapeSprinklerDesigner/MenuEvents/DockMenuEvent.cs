using System;
using System.Drawing;
using TNT.ToolStripItemManager;
using WeifenLuo.WinFormsUI.Docking;

namespace LandscapeSprinklerDesigner.MenuEvents
{
	abstract class DockMenuEvent : ToolStripItemGroup
	{
		public override bool CheckOnClick => true;
		protected Tuple<DockContent, DockPanel> tuple => base.ExternalObject as Tuple<DockContent, DockPanel>;
		protected DockContent dockContent => tuple.Item1 as DockContent;
		protected DockPanel dockPanel => tuple.Item2 as DockPanel;

		public DockMenuEvent(Image image = null) : base(image)
		{
		}

		public override void OnApplicationIdle(object sender, EventArgs e)
		{
			base.OnApplicationIdle(sender, e);
			base.Checked = !dockContent.IsHidden;
		}

		public override void OnLicenseChanged(bool isLicensed)
		{
			base.OnLicenseChanged(isLicensed);
			if (!isLicensed)
			{
				base.Checked = false;
				dockContent.IsHidden = true;
			}
		}

  public override void OnMouseClick(object? sender, EventArgs e)
		{
			base.OnMouseClick(sender, e);

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
