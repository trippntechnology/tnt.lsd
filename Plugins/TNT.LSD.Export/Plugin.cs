using LSDComponents;
using System.Drawing.Imaging;
using System.Windows.Forms;
using TNT.Plugin.Manager;

namespace TNT.LSD.Export
{
	public class Plugin : TNT.Plugin.Manager.Plugin
	{
		public override string MenuStripName => "MenuStrip1";

		public override string ToolStripName => string.Empty;

		public override string Text => "Export";

		public override string ToolTipText => string.Empty;

		public override string EmbeddedResource => string.Empty;

		public override void Execute(IWin32Window owner, ToolStripItem sender, IApplicationData content)
		{
			ApplicationData appData = content as ApplicationData;

			if (appData!= null && appData.TNTCAD != null)
			{
				if (sender.Text == "Export as JPG")
				{
					using (SaveFileDialog sfd = new SaveFileDialog())
					{
						sfd.Title = "Export as JPG";
						sfd.Filter = "JPEG|*.jpg";
						sfd.DefaultExt = "jpg";

						if (sfd.ShowDialog(owner) == DialogResult.OK)
						{
							appData.TNTCAD.Design.Save(sfd.FileName, ImageFormat.Jpeg);
						}
					}
				}
				else if (sender.Text == "Export as PNG")
				{
					using (SaveFileDialog sfd = new SaveFileDialog())
					{
						sfd.Title = "Export as PNG";
						sfd.Filter = "PNG|*.png";
						sfd.DefaultExt = "png";

						if (sfd.ShowDialog(owner) == DialogResult.OK)
						{
							appData.TNTCAD.Design.Save(sfd.FileName, ImageFormat.Png);
						}
					}
				}
			}
		}

		public override MenuStrip GetMenuStrip()
		{
			MenuStrip menuStrip = new MenuStrip();
			ToolStripMenuItem fileMenu = new ToolStripMenuItem("&File");

			// Causes the Menu item in this menu strip to match the merging menu strip
			fileMenu.MergeAction = MergeAction.MatchOnly;

			ToolStripMenuItem exportMenu = (ToolStripMenuItem)CreateToolStripItem<ToolStripMenuItem>();
			fileMenu.DropDownItems.Add(exportMenu);

			ToolStripItem item = new ToolStripMenuItem("Export as JPG", GetImage("TNT.LSD.Export.Images.jpg.png"));
			item.ToolTipText = "Export design as JPG image";
			item.Tag = this;
			item.MouseEnter += Item_MouseEnter;
			item.MouseLeave += Item_MouseLeave;

			_ToolStripItems.Add(item);
			exportMenu.DropDownItems.Add(item);

			item = new ToolStripMenuItem("Export as PNG", GetImage("TNT.LSD.Export.Images.png.png"));
			item.ToolTipText = "Export design as PNG image";
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

		public override ToolStrip GetToolStrip()
		{
			return null;
		}
	}
}
