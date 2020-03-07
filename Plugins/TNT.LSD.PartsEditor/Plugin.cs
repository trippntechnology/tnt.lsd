using LSDComponents;
using System.Drawing;
using System.Windows.Forms;
using TNT.Plugin.Manager;

namespace TNT.LSD.PartsEditor
{
	public class Plugin : TNT.Plugin.Manager.Plugin
	{
		public override string MenuStripName => "MenuStrip1";

		public override string ToolStripName => "ToolStrip2";

		public override string Text => "Parts Editor";

		public override string ToolTipText => "Edit static parts listing";

		public override bool LicenseRequired => true;

		public override Image Image
		{
			get
			{
				return (new PartsEditor()).Icon.ToBitmap();
			}
		}

		public override void Execute(IWin32Window owner, ToolStripItem sender, IApplicationData content)
		{
			ApplicationData appData = content as ApplicationData;
			(new PartsEditor()).ShowDialog(owner, appData.TNTCAD.State.StaticParts);
		}

		public override MenuStrip GetMenuStrip()
		{
			return null;
		}

		public override ToolStrip GetToolStrip()
		{
			ToolStrip toolStrip = new ToolStrip();

			ToolStripButton toolStripButton = (ToolStripButton)CreateToolStripItem<ToolStripButton>();
			toolStripButton.Image = this.Image;
			toolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
			toolStrip.Items.Add(toolStripButton);

			return toolStrip;
		}
	}
}
