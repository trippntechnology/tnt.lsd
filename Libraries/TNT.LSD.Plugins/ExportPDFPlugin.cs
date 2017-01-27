using System.Drawing;
using System.IO;
using System.Windows.Forms;
using LSDComponents;
using LSDComponents.Settings;
using TNT.LSD.PDFGenerator;
using TNT.Plugin;

namespace TNT.LSD.Plugins
{
	/// <summary>
	/// Creates a SSC PDF
	/// </summary>
	public class ExportPDFPlugin : Plugin.Plugin
	{
		/// <summary>
		/// Presents a save dialog to specify a file name and save the file as PDF
		/// </summary>
		/// <param name="owner">Top-level form that owns the plugin</param>
		/// <param name="parameter">TNTCAD object</param>
		/// <returns>PDFPluginResult</returns>
		protected override object Execute(IWin32Window owner, object parameter)
		{
			TNTCAD cad = parameter as TNTCAD;

			if (cad != null)
			{
				if (cad.HasUnsavedChanges || string.IsNullOrEmpty(cad.CurrentFileName))
				{
					if (!cad.Save(false))
					{
						return null;
					}
				}

				SaveFileDialog sfd = new SaveFileDialog();
				sfd.Title = "Export as PDF";
				sfd.Filter = "PDF|*.pdf";
				sfd.DefaultExt = "pdf";
				sfd.FileName = Path.GetFileNameWithoutExtension(cad.CurrentFileName);

				if (sfd.ShowDialog() == DialogResult.OK)
				{
					// Unselect all objects so that they are included in the drawn image for the PDF
					cad.UnselectAll();
					cad.Repaint();

					int previousScale = cad.DisplayScale;
					cad.DisplayScale = 100;

					Content content = new Content()
					{
						Design = cad.Design,
						DynamicProperties = cad.Settings,
						Parts = cad.GetPartsList()
					};

					SSCSettings sscSettings = cad.Settings as SSCSettings;

					if (sscSettings != null)
					{
						content.Comments = sscSettings.Comment;
						content.DesignNumber = sscSettings.Number;
						content.OwnerName = sscSettings.Name;
					}

					GeneratePDF(sfd.FileName, content);

					cad.DisplayScale = previousScale;

					return new PDFPluginResult(true, sfd.FileName);
				}
			}

			return null;
		}

		/// <summary>
		/// Generates the PDF using the specific PDF generator
		/// </summary>
		/// <param name="fileName">Name of file that should be assigned to PDF</param>
		/// <param name="content">Content of the PDF</param>
		protected virtual void GeneratePDF(string fileName, Content content)
		{
			(new PDFGenerator.GenericPDFGenerator()).Generate(fileName, content);
		}

		/// <summary>
		/// Image used by the plugin
		/// </summary>
		public override Image Image { get { return new Bitmap(this.GetType().Assembly.GetManifestResourceStream("TNT.LSD.Plugins.Images.pdf.png")); } }

		/// <summary>
		/// Name of the plugin
		/// </summary>
		public override string Text { get { return "Export PDF"; } }

		/// <summary>
		/// Plugin's description used to display as tool tip hint
		/// </summary>
		public override string ToolTipText { get { return "Creates a PDF with the layout and parts listing"; } }
	}
}
