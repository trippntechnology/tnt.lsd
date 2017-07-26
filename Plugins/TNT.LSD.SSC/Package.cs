using LSDComponents;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using TNT.Plugin.Manager;
using WeifenLuo.WinFormsUI.Docking;

namespace TNT.LSD.SSC
{
	public class Package : Plugin
	{
		public override string MenuStripName => "MenuStrip1";

		public override string ToolStripName => "ToolStrip1";

		public override string Text => "SSC Package";

		public override string ToolTipText => "Create a package for SSC";

		public override string EmbeddedResource => "TNT.LSD.SSC.Images.package.png";

		public override void Execute(IWin32Window owner, ToolStripItem sender, IApplicationData content)
		{
			ApplicationData appData = content as ApplicationData;

			if (appData != null && appData.TNTCAD != null)
			{
				if (appData.TNTCAD.HasUnsavedChanges || string.IsNullOrEmpty(appData.TNTCAD.CurrentFileName))
				{
					if (!appData.TNTCAD.Save(false))
					{
						return;
					}
				}

				using (SaveFileDialog sfd = new SaveFileDialog())
				{
					sfd.Title = this.Text;
					sfd.Filter = "zip|*.zip";
					sfd.DefaultExt = "zip";
					sfd.FileName = $"{appData.TNTCAD.Settings.ToString()}.zip";

					if (sfd.ShowDialog(owner) == DialogResult.OK)
					{
						string path = Path.GetDirectoryName(sfd.FileName);
						string name = Path.GetFileNameWithoutExtension(sfd.FileName);
						string pdfFileName = Path.Combine(path, $"{name}.pdf");
						string jpgFileName = Path.Combine(path, $"{name}.jpg");

						// Save image
						appData.TNTCAD.Design.Save(jpgFileName, ImageFormat.Jpeg);

						PDFForm pdfForm = new PDFForm();
						pdfForm.Show(jpgFileName, appData.DockPanel, DockState.Document);

						// Generate PDF
						GeneratePDF(appData, appData.TNTCAD, pdfFileName);

						File.Delete(sfd.FileName);

						// Zip up files
						using (Ionic.Zip.ZipFile zipFile = new Ionic.Zip.ZipFile(sfd.FileName))
						{
							zipFile.AddFiles(new string[] { appData.TNTCAD.CurrentFileName, pdfFileName, jpgFileName }, string.Empty);
							zipFile.Save();
						}
					}
				}
			}
		}

		public override MenuStrip GetMenuStrip()
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

		public override ToolStrip GetToolStrip()
		{
			ToolStrip toolStrip = new ToolStrip();

			ToolStripButton toolStripButton = (ToolStripButton)CreateToolStripItem<ToolStripButton>();
			toolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
			toolStrip.Items.Add(toolStripButton);

			return toolStrip;
		}
	}
}
