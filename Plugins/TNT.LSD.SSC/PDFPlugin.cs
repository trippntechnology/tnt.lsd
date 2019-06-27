using LSDComponents;
using System.IO;
using System.Windows.Forms;
using TNT.Plugin.Manager;
using System;
using System.Drawing;

namespace TNT.LSD.SSC
{
	public class PDFPlugin : Plugin
	{
		public override string Text => "Export SSC PDF";

		public override string ToolTipText => "Generate PDF for SSC";

		public override Image Image => base.GetImage("TNT.LSD.SSC.Images.pdf-ssc-icon.png");

		public override void Execute(System.Windows.Forms.IWin32Window owner, System.Windows.Forms.ToolStripItem sender, IApplicationData content)
		{
			ApplicationData appData = content as ApplicationData;

			TNTCAD cad = appData.TNTCAD;

			if (cad != null)
			{
				if (cad.HasUnsavedChanges || string.IsNullOrEmpty(cad.CurrentFileName))
				{
					if (!cad.Save(false))
					{
						return;
					}
				}

				SaveFileDialog sfd = new SaveFileDialog();
				sfd.Title = "Export as PDF";
				sfd.Filter = "PDF|*.pdf";
				sfd.DefaultExt = "pdf";
				sfd.FileName = Path.GetFileNameWithoutExtension(appData.TNTCAD.CurrentFileName);

				if (sfd.ShowDialog() == DialogResult.OK)
				{
					GeneratePDF(appData, cad, sfd.FileName);
				}
			}
		}

		public override System.Windows.Forms.MenuStrip GetMenuStrip()
		{
			MenuStrip menuStrip = new MenuStrip();
			ToolStripMenuItem fileMenu = new ToolStripMenuItem("&File");
			fileMenu.MergeAction = MergeAction.MatchOnly;

			ToolStripMenuItem exportMenu = new ToolStripMenuItem("Export");
			exportMenu.MergeAction = MergeAction.MatchOnly;

			fileMenu.DropDownItems.Add(exportMenu);

			ToolStripMenuItem tsmi = (ToolStripMenuItem)CreateToolStripItem<ToolStripMenuItem>();

			exportMenu.DropDownItems.Add(tsmi);

			menuStrip.Items.Add(fileMenu);

			return menuStrip;
		}

		public override System.Windows.Forms.ToolStrip GetToolStrip() => null;
		//{
		//	ToolStrip toolStrip = new ToolStrip();

		//	ToolStripButton toolStripButton = (ToolStripButton)CreateToolStripItem<ToolStripButton>();
		//	toolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
		//	toolStrip.Items.Add(toolStripButton);

		//	return toolStrip;
		//}
	}
}
