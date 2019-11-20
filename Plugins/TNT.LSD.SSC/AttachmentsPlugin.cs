using LSDComponents;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using TNT.Plugin.Manager;

namespace TNT.LSD.SSC
{
	public class AttachmentsPlugin : Plugin
	{
		public override string Text => "SSC Attachments";

		public override string ToolTipText => "Generate SSC Attachments";

		public override Image Image => base.GetImage("TNT.LSD.SSC.Images.email_attach.png");

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

				//using (FolderBrowserDialog fbd = new FolderBrowserDialog())
				//{
				//    fbd.SelectedPath = Path.GetDirectoryName(appData.TNTCAD.CurrentFileName);
				//    //fbd.SelectedPath = appData.TNTCAD.Settings.ToString();
				//    //fbd.Title = this.Text;
				//    //fbd.Filter = "zip|*.zip";
				//    //fbd.DefaultExt = "zip";
				//    //fbd.FileName = $"{appData.TNTCAD.Settings.ToString()}.zip";

				//    if (fbd.ShowDialog(owner) == DialogResult.OK)
				//    {
				string path = Path.GetDirectoryName(appData.TNTCAD.CurrentFileName);
				string name = Path.GetFileNameWithoutExtension(appData.TNTCAD.CurrentFileName);
				string pdfFileName = Path.Combine(path, $"{name}.pdf");
				string jpgFileName = Path.Combine(path, $"{name}.jpg");

				// Save image
				appData.TNTCAD.Design.Save(jpgFileName, ImageFormat.Jpeg);

				//PDFForm pdfForm = new PDFForm();
				//pdfForm.Show(jpgFileName, appData.DockPanel, DockState.Document);

				// Generate PDF
				GeneratePDF(appData, appData.TNTCAD, pdfFileName);

				//var thread = new Thread(() =>
				//{
				//									//var toast = new Toast();
				//									using (var toast = new Toast())
				//	{
				//		toast.Show(owner, $"{name} added to clipboard");
				//		Thread.Sleep(3000);
				//	}
				//});

				//File.Delete(fbd.FileName);

				//// Zip up files
				//using (Ionic.Zip.ZipFile zipFile = new Ionic.Zip.ZipFile(fbd.FileName))
				//{
				//    zipFile.AddFiles(new string[] { appData.TNTCAD.CurrentFileName, pdfFileName, jpgFileName }, string.Empty);
				//    zipFile.Save();
				//}
				//    }
				//}
			}
		}

		public override MenuStrip GetMenuStrip()
		{
			return null;
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
