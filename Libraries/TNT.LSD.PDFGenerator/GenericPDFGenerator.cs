using System;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace TNT.LSD.PDFGenerator
{
	/// <summary>
	/// Represents a generic document
	/// </summary>
	public class GenericPDFGenerator : PDFGenerator
	{
		/// <summary>
		/// Creates a PDF file using the content
		/// </summary>
		/// <param name="fileName">Location of file</param>
		/// <param name="content">Content to place in the PDF</param>
		public override void Generate(string fileName, Content content)
		{
			PageEventHelper peh = new PageEventHelper((headerTbl, footerTbl) =>
			{
				// Create a table with two columns
				headerTbl.ResetColumnCount(2);

				// Write the design number in two columns
				PdfPCell cell = new PdfPCell(new Phrase(content.OwnerName));
				cell.Phrase.Font.Size = HEADER_FONT_SIZE;
				cell.MinimumHeight = 18;
				cell.Border = PdfPCell.BOTTOM_BORDER;
				cell.BorderWidth = 2f;
				cell.BorderColor = BaseColor.RED;
				cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
				headerTbl.AddCell(cell);

				// Write the owner's name in the second column
				cell.Phrase = new Phrase(DateTime.Now.ToShortDateString());
				cell.Phrase.Font.Size = HEADER_FONT_SIZE;
				cell.MinimumHeight = 18;
				cell.Border = PdfPCell.BOTTOM_BORDER;
				cell.BorderWidth = 2f;
				cell.BorderColor = BaseColor.RED;
				cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
				headerTbl.AddCell(cell);

				// Create a table with three columns
				footerTbl.ResetColumnCount(3);
				footerTbl.SetTotalWidth(new float[] { 6f, 1, 6f });

				// Put the application name in the first column
				cell = new PdfPCell(new Phrase(GetApplicationName()));
				cell.Phrase.Font.Size = FOOTER_FONT_SIZE;
				cell.Border = PdfPCell.TOP_BORDER;
				cell.BorderWidth = 2f;
				cell.BorderColor = BaseColor.BLUE;
				footerTbl.AddCell(cell);

				// Add a cell for the page number
				cell.Phrase = new Phrase("");
				cell.Phrase.Font.Size = FOOTER_FONT_SIZE;
				cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
				cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
				footerTbl.AddCell(cell);

				// Add copyright info
				cell.Phrase = new Phrase(GetCopyright());
				cell.Phrase.Font.Size = FOOTER_FONT_SIZE;
				cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
				footerTbl.AddCell(cell);
			});

			peh.OnBeforeWriteFooter = BeforeWriteFooter;

			Document document = CreateDocument(fileName, peh);

			CreatePartsListing(document, content.Parts);

			CreateImagePage(document, content.Design);

			document.Close();
		}

		/// <summary>
		/// Event to set the page number in the second column of the footer
		/// </summary>
		/// <param name="footer">Footer</param>
		/// <param name="document">Document</param>
		protected void BeforeWriteFooter(PdfPTable footer, Document document)
		{
			Phrase phrase = new Phrase(document.PageNumber.ToString());
			phrase.Font.Size = FOOTER_FONT_SIZE;
			footer.Rows[0].GetCells()[1].Phrase = phrase;
		}
	}
}
