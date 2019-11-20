using System.Windows.Forms;

namespace TNT.LSD.Background
{
	public abstract class BackgroundPlugin : TNT.Plugin.Manager.Plugin
	{
		public override string MenuStripName => "MenuStrip1";

		public override string ToolStripName => "pluginToolStrip";

		public override bool LicenseRequired => true;

		public override MenuStrip GetMenuStrip()
		{
			var existingMenu = base._Manager.FindToolStripMenuItem(MenuStripName, "Landscape");

			MenuStrip menuStrip = new MenuStrip();
			ToolStripMenuItem fileMenu = new ToolStripMenuItem("&File") { MergeAction = MergeAction.MatchOnly };
			ToolStripMenuItem landscapeMenu = null;

			if (existingMenu == null)
			{
				landscapeMenu = new ToolStripMenuItem("Landscape") { MergeAction = MergeAction.Insert, MergeIndex = 5 };
			}
			else
			{
				landscapeMenu = new ToolStripMenuItem("Landscape") { MergeAction = MergeAction.MatchOnly };
			}

			fileMenu.DropDownItems.Add(landscapeMenu);

			menuStrip.Items.Add(fileMenu);

			return menuStrip;
		}
	}
}
