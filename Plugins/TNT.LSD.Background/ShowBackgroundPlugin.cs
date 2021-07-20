using LSDComponents;
using System.Drawing;
using System.Windows.Forms;
using TNT.Plugin.Manager;

namespace TNT.LSD.Background
{
	class ShowBackgroundPlugin : TNT.Plugin.Manager.Plugin
	{
		private ToolStripButton toolStripButton = null;

		public override string MenuStripName => "MenuStrip1";

		public override string ToolStripName => "pluginToolStrip";

		public override string Text => "Landscape Image";

		public override string ToolTipText => "Hide/Show Landscape Image";

		public override bool LicenseRequired => true;

		public override Image Image => base.GetImage("TNT.LSD.Background.Images.show_landscape.png");

		public override void Execute(IWin32Window owner, ToolStripItem sender, IApplicationData content)
		{
			System.Diagnostics.Debug.WriteLine("Execute called");
			ApplicationData appData = content as ApplicationData;
			if (appData == null || appData.TNTCAD == null) return;
			var isChecked = sender.IsChecked();

			_ToolStripItems.ForEach(t => { t.SetCheck(isChecked); });
			appData.TNTCAD.DrawBackground = isChecked;
		}

		public void SetChecked(IWin32Window owner, IApplicationData content)
		{
			toolStripButton.Checked = true;
			Execute(owner, toolStripButton, content);
		}

		public override MenuStrip GetMenuStrip()
		{
			MenuStrip menuStrip = new MenuStrip();
			ToolStripMenuItem viewMenu = new ToolStripMenuItem("&View");

			// Causes the Menu item in this menu strip to match the merging menu strip
			viewMenu.MergeAction = MergeAction.MatchOnly;

			ToolStripMenuItem showLandscapeMenu = (ToolStripMenuItem)CreateToolStripItem<ToolStripMenuItem>();
			showLandscapeMenu.ShortcutKeys = Keys.Control | Keys.L;
			showLandscapeMenu.CheckOnClick = true;
			viewMenu.DropDownItems.Add(showLandscapeMenu);
			menuStrip.Items.Add(viewMenu);

			return menuStrip;
		}

		public override ToolStrip GetToolStrip()
		{
			ToolStrip toolStrip = new ToolStrip();

			toolStripButton = (ToolStripButton)CreateToolStripItem<ToolStripButton>();
			toolStripButton.CheckOnClick = true;
			toolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
			toolStrip.Items.Add(toolStripButton);
			toolStrip.Items.Add(new ToolStripSeparator());

			return toolStrip;
		}
	}
}
