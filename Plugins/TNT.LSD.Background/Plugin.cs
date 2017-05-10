using LSDComponents;
using System.Windows.Forms;
using TNT.Plugin.Manager;

namespace TNT.LSD.Background
{
	public class Plugin : TNT.Plugin.Manager.Plugin
	{
		private string ImportMenuText = "Import Image";
		private string ImportMenuToolTip = "Import background image";
		private string ImportResourceImage = "TNT.LSD.Background.Images.import_background_image.png";

		private string ExportMenuText = "Export Image";
		private string ExportMenuToolTip = "Saves background image";

		public override string MenuStripName => "MenuStrip1";

		public override string ToolStripName => "ToolStrip1";

		public override string Text => "Background";

		public override string ToolTipText => string.Empty;

		public override string EmbeddedResource => string.Empty;

		public override void Execute(System.Windows.Forms.IWin32Window owner, System.Windows.Forms.ToolStripItem sender, IApplicationData content)
		{
			ApplicationData appData = content as ApplicationData;

			if (appData != null && appData.TNTCAD != null)
			{
				if (sender.Text == ImportMenuText)
				{
					using (BackgroundImporter bi = new BackgroundImporter())
					{
						bi.ShowDialog(owner, appData.TNTCAD.State);
					}
				}
				else if (sender.Text == ExportMenuText)
				{
					using (SaveFileDialog sfd = new SaveFileDialog())
					{
						if (sfd.ShowDialog(owner) == DialogResult.OK)
						{
							appData.TNTCAD.State.BackgroundImage.Save(sfd.FileName);
						}
					}
				}
			}
		}

		public override System.Windows.Forms.MenuStrip GetMenuStrip()
		{
			MenuStrip menuStrip = new MenuStrip();
			ToolStripMenuItem fileMenu = new ToolStripMenuItem("&File");

			// Causes the Menu item in this menu strip to match the merging menu strip
			fileMenu.MergeAction = MergeAction.MatchOnly;

			ToolStripMenuItem exportMenu = (ToolStripMenuItem)CreateToolStripItem<ToolStripMenuItem>();
			fileMenu.DropDownItems.Add(exportMenu);

			ToolStripItem item = new ToolStripMenuItem(ImportMenuText, GetImage(ImportResourceImage));
			item.ToolTipText = ImportMenuToolTip;
			item.Tag = this;
			item.MouseEnter += Item_MouseEnter;
			item.MouseLeave += Item_MouseLeave;

			_ToolStripItems.Add(item);
			exportMenu.DropDownItems.Add(item);

			item = new ToolStripMenuItem(ExportMenuText);
			item.ToolTipText = ExportMenuToolTip;
			item.Tag = this;
			item.MouseEnter += Item_MouseEnter;
			item.MouseLeave += Item_MouseLeave;

			_ToolStripItems.Add(item);
			exportMenu.DropDownItems.Add(item);

			exportMenu.MergeAction = MergeAction.Insert;
			exportMenu.MergeIndex = 5;

			menuStrip.Items.Add(fileMenu);

			return menuStrip;
		}

		public override System.Windows.Forms.ToolStrip GetToolStrip()
		{
			ToolStrip toolStrip = new ToolStrip();

			ToolStripButton toolStripButton = (ToolStripButton)CreateToolStripItem<ToolStripButton>(ImportMenuText, ImportResourceImage, ImportMenuToolTip);
			toolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
			toolStrip.Items.Add(toolStripButton);

			return toolStrip;
		}
	}
}
