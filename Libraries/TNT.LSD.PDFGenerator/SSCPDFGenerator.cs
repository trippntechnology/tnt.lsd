using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TNT.LSD.PDFGenerator.Properties;
using TNT.Utilities;
using iText = iTextSharp.text;

namespace TNT.LSD.PDFGenerator
{
	/// <summary>
	/// Generates a PDF for Sprinkler Supply Co
	/// </summary>
	public class SSCPDFGenerator : PDFGenerator
	{
		/// <summary>
		/// Creates a PDF file for Sprinkle Supply Co using the content
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
					PdfPCell cell = new PdfPCell(new Phrase(content.DesignNumber));
					cell.Phrase.Font.Size = HEADER_FONT_SIZE;
					cell.MinimumHeight = 18;
					cell.Border = PdfPCell.BOTTOM_BORDER;
					cell.BorderWidth = 2f;
					cell.BorderColor = BaseColor.RED;
					cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
					headerTbl.AddCell(cell);

					// Write the owner's name in the second column
					cell.Phrase = new Phrase(string.Format("{0}{1}", content.OwnerName, string.Concat(" (", DateTime.Now.ToShortDateString(), ")")));
					cell.Phrase.Font.Size = HEADER_FONT_SIZE;
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

			CreateCoverPage(document, content);

			document.NewPage();

			CreatePartsListing(document, content.Parts);

			System.Drawing.Font font = new System.Drawing.Font("Arial", 8);
			Graphics graphics = Graphics.FromImage(content.Design);

			string disclaimer = Resources.Disclaimer;
			SizeF size = graphics.MeasureString(disclaimer, font, content.Design.Width);

			System.Drawing.Image image = new Bitmap(content.Design.Width, content.Design.Height + (int)size.Height);

			graphics = Graphics.FromImage(image);
			graphics.FillRectangle(new SolidBrush(Color.White), 0, 0, image.Width, image.Height);
			graphics.DrawImage(content.Design, new Point(0, 0));

			graphics.DrawString(disclaimer, font, new SolidBrush(Color.Black), new RectangleF(new PointF(0, content.Design.Height), size));

			CreateImagePage(document, image);

			if (content.Coverage != null)
			{
				image = new Bitmap(content.Coverage.Width, content.Coverage.Height + (int)size.Height);

				graphics = Graphics.FromImage(image);
				graphics.FillRectangle(new SolidBrush(Color.White), 0, 0, image.Width, image.Height);
				graphics.DrawImage(content.Coverage, new Point(0, 0));

				graphics.DrawString(disclaimer, font, new SolidBrush(Color.Black), new RectangleF(new PointF(0, content.Coverage.Height), size));

				CreateImagePage(document, image);
			}

			document.Close();
		}

		/// <summary>
		/// Creates the cover page
		/// </summary>
		/// <param name="document">Document</param>
		/// <param name="content">Content that should be used</param>
		protected void CreateCoverPage(Document document, Content content)
		{
			Reflector<object> reflector = new Reflector<object>(content.DynamicProperties);
			var categories = (from p in reflector.Properties orderby p.Category select p.Category).Distinct();
			List<PropertyReflector> propReflectors = (from p in reflector.Properties orderby p.Category, p.DisplayName select p).ToList();

			document.Add(new Paragraph(" "));
			iText.Image image = iText.Image.GetInstance(Resources.ssc, System.Drawing.Imaging.ImageFormat.Png);
			image.ScalePercent(30F);
			//image.ScaleAbsoluteHeight(100f);
      image.Alignment = iText.Image.ALIGN_MIDDLE;
			document.Add(image);

			document.Add(new Paragraph(" "));

			PdfPTable table = new PdfPTable(2);
			table.WidthPercentage = 100;
			table.DefaultCell.Border = PdfPCell.NO_BORDER;

			var cats = reflector.GetCategoriesByPriority();

			foreach(var category in cats)
			{
				List<PropertyReflector> props = (from p in reflector.Properties where p.Category == category orderby p.Priority select p).ToList();
				AddTableSection(table, category, props);
			}

			AddTableRow(table, new string[] { string.Empty, string.Empty }, RowFont, BaseColor.WHITE);

			AddTableRow(table, new string[] { string.Empty, string.Empty }, RowFont, BaseColor.WHITE);
			AddTableSection(table, "Comments");
			AddTableRow(table, new string[] { string.Empty, string.Empty }, RowFont, BaseColor.WHITE);

			PdfPCell cell = new PdfPCell(new Phrase(content.Comments, RowFont));
			cell.Border = PdfPCell.NO_BORDER;
			cell.Colspan = 2;
			table.AddCell(cell);

			document.Add(table);
		}

		private void AddTableSection(PdfPTable table, string category, List<PropertyReflector> props)
		{
			AddTableRow(table, new string[] { string.Empty, string.Empty }, RowFont, BaseColor.WHITE);

			if (!string.IsNullOrEmpty(category))
			{
				AddTableSection(table, category);
			}

			AddTableRow(table, new string[] { string.Empty, string.Empty }, RowFont, BaseColor.WHITE);

			List<string[]> rows = new List<string[]>();

			foreach (PropertyReflector pr in props)
			{
				rows.Add(new string[] { pr.DisplayName, pr.Value == null ? string.Empty: pr.Value.ToString() });
			}

			WriteTableData(table, rows.ToArray());
		}

		/// <summary>
		/// Adds row that spans both columns with the section label
		/// </summary>
		/// <param name="table"></param>
		/// <param name="value"></param>
		protected void AddTableSection(PdfPTable table, string value)
		{
			PdfPCell cell = new PdfPCell(new Phrase(value, SectionFont));
			cell.Border = PdfPCell.TOP_BORDER;
			cell.BorderWidth = 2f;
			cell.BorderColor = BaseColor.BLUE;
			cell.Colspan = 2;
			table.AddCell(cell);
		}

		/// <summary>
		/// Writes data to the table
		/// </summary>
		/// <param name="table">Table where data should be written</param>
		/// <param name="data">Data</param>
		protected void WriteTableData(PdfPTable table, string[][] data)
		{
			BaseColor shadedRowColor = new BaseColor(Color.LightGray);

			for (int index = 0; index < data.Length; index++)
			{
				AddRowCell(table, data[index][0], BoldRowFont, index % 2 == 0 ? shadedRowColor : BaseColor.WHITE);
				AddRowCell(table, data[index][1], RowFont, index % 2 == 0 ? shadedRowColor : BaseColor.WHITE);
			}
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
