using LSDComponents;
using LSDComponents.Settings;
using System.IO;
using System.Windows.Forms;
using TNT.LSD.PDFGenerator;
using TNT.Plugin.Manager;
using WeifenLuo.WinFormsUI.Docking;

namespace TNT.LSD.SSC
{
	public class PDF : TNT.Plugin.Manager.Plugin
	{
		public override string MenuStripName => "MenuStrip1";

		public override string ToolStripName => "ToolStrip1";

		public override string Text => "Export SSC PDF";

		public override string ToolTipText => "Generate PDF for SSC";

		public override string EmbeddedResource => "TNT.LSD.SSC.Images.pdf-ssc-icon.png";

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
					// Unselect all objects so that they are included in the drawn image for the PDF
					cad.UnselectAll();
					cad.Repaint();

					int previousScale = cad.DisplayScale;
					cad.DisplayScale = 100;

					Content pdfContent = new Content()
					{
						Design = cad.Design,
						DynamicProperties = cad.Settings,
						Parts = cad.GetPartsList()
					};

					SSCSettings sscSettings = cad.Settings as SSCSettings;

					if (sscSettings != null)
					{
						pdfContent.Comments = sscSettings.Comment;
						pdfContent.DesignNumber = sscSettings.Number;
						pdfContent.OwnerName = sscSettings.Name;
					}

					(new PDFGenerator.SSCPDFGenerator()).Generate(sfd.FileName, pdfContent);

					cad.DisplayScale = previousScale;

					PDFForm pdfForm = new PDFForm();
					pdfForm.Show(sfd.FileName, appData.DockPanel, DockState.Document);
				}
			}
		}

		public override System.Windows.Forms.MenuStrip GetMenuStrip()
		{
			MenuStrip menuStrip = new MenuStrip();
			ToolStripMenuItem fileMenu = new ToolStripMenuItem("&File");

			// Causes the Menu item in this menu strip to match the merging menu strip
			fileMenu.MergeAction = MergeAction.MatchOnly;

			ToolStripMenuItem exportMenu = new ToolStripMenuItem("Export");
			exportMenu.MergeAction = MergeAction.MatchOnly;

			fileMenu.DropDownItems.Add(exportMenu);
			//menu.DropDownItems.Add(new ToolStripSeparator());

			ToolStripMenuItem tsmi = (ToolStripMenuItem)CreateToolStripItem<ToolStripMenuItem>();
			tsmi.MergeAction = MergeAction.Insert;
			tsmi.MergeIndex = 6;

			exportMenu.DropDownItems.Add(tsmi);
			//var appMS = (ToolStrip)_Controls.Find(plugin.MenuStripName, true).FirstOrDefault();



			//fileMenu.DropDownItems.Add(tsmi);
			menuStrip.Items.Add(fileMenu);

			return menuStrip;
		}

		public override System.Windows.Forms.ToolStrip GetToolStrip()
		{
			ToolStrip toolStrip = new ToolStrip();

			ToolStripButton toolStripButton = (ToolStripButton)CreateToolStripItem<ToolStripButton>();
			toolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
			toolStrip.Items.Add(toolStripButton);

			return toolStrip;
		}
	}
}
