using LSDComponents;
using System.Drawing.Imaging;
using System.Windows.Forms;
using TNT.Plugin.Manager;
using System;
using System.Drawing;

namespace TNT.LSD.Export
{
	public class Plugin : TNT.Plugin.Manager.Plugin
	{
		private string JPGMenuText = "Export as JPG";
		private string JPGToolTipText = "Export design as JPG image";
		private string JPGImageResource = "TNT.LSD.Export.Images.jpg.png";
		private string PNGMenuText = "Export as PNG";
		private string PNGToolTipText = "Export design as PNG image";
		private string PNGImageResource = "TNT.LSD.Export.Images.png.png";

		public override string MenuStripName => "MenuStrip1";

		public override string ToolStripName => "ToolStrip1";

		public override string Text => "Export";

		public override string ToolTipText => string.Empty;

		public override Image Image => null;

		public override void Execute(IWin32Window owner, ToolStripItem sender, IApplicationData content)
		{
			ApplicationData appData = content as ApplicationData;

			if (appData != null && appData.TNTCAD != null)
			{
				if (sender.Text == JPGMenuText)
				{
					using (SaveFileDialog sfd = new SaveFileDialog())
					{
						sfd.Title = JPGMenuText;
						sfd.Filter = "JPEG|*.jpg";
						sfd.DefaultExt = "jpg";
						sfd.FileName = $"{appData.TNTCAD.Settings.ToString()}.jpg";

						if (sfd.ShowDialog(owner) == DialogResult.OK)
						{
							appData.TNTCAD.Design.Save(sfd.FileName, ImageFormat.Jpeg);
						}
					}
				}
				else if (sender.Text == PNGMenuText)
				{
					using (SaveFileDialog sfd = new SaveFileDialog())
					{
						sfd.Title = PNGMenuText;
						sfd.Filter = "PNG|*.png";
						sfd.DefaultExt = "png";
						sfd.FileName = $"{appData.TNTCAD.Settings.ToString()}.png";

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

			ToolStripItem item = new ToolStripMenuItem(JPGMenuText, GetImage(JPGImageResource));
			item.ToolTipText = JPGToolTipText;
			item.Tag = this;
			item.MouseEnter += Item_MouseEnter;
			item.MouseLeave += Item_MouseLeave;

			_ToolStripItems.Add(item);
			exportMenu.DropDownItems.Add(item);

			item = new ToolStripMenuItem(PNGMenuText, GetImage(PNGImageResource));
			item.ToolTipText = PNGToolTipText;
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
			ToolStrip toolStrip = new ToolStrip();

			ToolStripButton toolStripButton = (ToolStripButton)CreateToolStripItem<ToolStripButton>(JPGMenuText, GetImage(JPGImageResource), JPGToolTipText);
			toolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
			toolStrip.Items.Add(toolStripButton);

			return toolStrip;
		}
	}
}
