using LSDComponents;
using System.Drawing;
using System.Windows.Forms;
using TNT.Plugin.Manager;

namespace TNT.LSD.Background
{
	public class ExportBackgroundPlugin : BackgroundPlugin
	{
		public override string Text => "Export";

		public override string ToolTipText => "Export landscape image";

		public override Image Image => null;

		public override void Execute(IWin32Window owner, ToolStripItem sender, IApplicationData content)
		{
			ApplicationData appData = content as ApplicationData;

			if (appData != null && appData.TNTCAD != null)
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

		public override MenuStrip GetMenuStrip()
		{
			var menuStrip = base.GetMenuStrip();
			var landscapeMenu = menuStrip.Items.FindItem("Landscape");
			var item = (ToolStripMenuItem)CreateToolStripItem<ToolStripMenuItem>();
			landscapeMenu.DropDownItems.Add(item);

			return menuStrip;
		}

		public override ToolStrip GetToolStrip() => null;
	}
}
