using LSDComponents;
using LSDComponents.Settings;
using TNT.LSD.PDFGenerator;
using WeifenLuo.WinFormsUI.Docking;

namespace TNT.LSD.SSC
{
	abstract public class Plugin : TNT.Plugin.Manager.Plugin
	{
		protected void GeneratePDF(ApplicationData appData, TNTCAD cad, string fileName)
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

			(new PDFGenerator.SSCPDFGenerator()).Generate(fileName, pdfContent);

			cad.DisplayScale = previousScale;

			PDFForm pdfForm = new PDFForm();
			pdfForm.Show(fileName, appData.DockPanel, DockState.Document);
		}
	}
}
