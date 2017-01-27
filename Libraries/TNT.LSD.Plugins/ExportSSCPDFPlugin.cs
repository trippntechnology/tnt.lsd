using TNT.LSD.PDFGenerator;

namespace TNT.LSD.Plugins
{
	/// <summary>
	/// Creates a SSC PDF
	/// </summary>
	public class ExportSSCPDFPlugin : ExportPDFPlugin
	{
		/// <summary>
		/// Generates the PDF using the specific PDF generator
		/// </summary>
		/// <param name="fileName">Name of file that should be assigned to PDF</param>
		/// <param name="content">Content of the PDF</param>
		protected override void GeneratePDF(string fileName, Content content)
		{
			(new PDFGenerator.SSCPDFGenerator()).Generate(fileName, content);
		}
	}
}
