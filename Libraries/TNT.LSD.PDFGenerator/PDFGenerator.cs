using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using iTextSharp.text;
using iTextSharp.text.pdf;
using TNT.LSD.Inventory;
using iText = iTextSharp.text;

namespace TNT.LSD.PDFGenerator
{
	/// <summary>
	/// The abstract class that represents all PDF generators
	/// </summary>
	public abstract class PDFGenerator
	{
		#region Constants

		const int POINTS_PER_INCH = 72;
		const int SECTION_FONT_SIZE = 14;
		const int ROW_FONT_SIZE = SECTION_FONT_SIZE - 5;
		const int TABLE_ROW_HEIGHT = 14;

		/// <summary>
		/// Footer font size
		/// </summary>
		protected const float FOOTER_FONT_SIZE = ROW_FONT_SIZE; // -1 uses default size

		/// <summary>
		/// Header font size
		/// </summary>
		protected const float HEADER_FONT_SIZE = -1; // -1 uses default size

		#endregion

		#region Members

		/// <summary>
		/// Font to use for the sections
		/// </summary>
		protected iText.Font m_SectionFont = null;

		/// <summary>
		/// Font to use for the header row in a table
		/// </summary>
		protected iText.Font m_HeaderRowFont = null;

		/// <summary>
		/// Font to use for other table rows
		/// </summary>
		protected iText.Font m_RowFont = null;

		/// <summary>
		/// Font to use for bolded table rows
		/// </summary>
		protected iText.Font m_BoldRowFont = null;

		#endregion

		#region Properties

		/// <summary>
		/// Title of the PDF
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		/// Subject associated with the PDF
		/// </summary>
		public string Subject { get; set; }

		/// <summary>
		/// Creator of the PDF
		/// </summary>
		public string Creator { get; set; }

		/// <summary>
		/// Author of the PDF
		/// </summary>
		public string Author { get; set; }

		/// <summary>
		/// Writer associated with the document
		/// </summary>
		protected PdfWriter Writer { get; set; }

		/// <summary>
		/// Section font
		/// </summary>
		virtual protected iText.Font SectionFont
		{
			get
			{
				if (m_SectionFont == null)
				{
					m_SectionFont = new iText.Font(iText.Font.FontFamily.HELVETICA, SECTION_FONT_SIZE, iText.Font.BOLD);
				}

				return m_SectionFont;
			}
		}

		/// <summary>
		/// Header row font
		/// </summary>
		virtual protected iText.Font HeaderRowFont
		{
			get
			{
				if (m_HeaderRowFont == null)
				{
					m_HeaderRowFont = new iText.Font(iText.Font.FontFamily.HELVETICA, ROW_FONT_SIZE, iText.Font.BOLD);
				}

				return m_HeaderRowFont;
			}
		}

		/// <summary>
		/// Row font
		/// </summary>
		virtual protected iText.Font RowFont
		{
			get
			{
				if (m_RowFont == null)
				{
					m_RowFont = new iText.Font(iText.Font.FontFamily.HELVETICA, ROW_FONT_SIZE);
				}

				return m_RowFont;
			}
		}

		/// <summary>
		/// Bolded row font
		/// </summary>
		virtual protected iText.Font BoldRowFont
		{
			get
			{
				if (m_BoldRowFont == null)
				{
					m_BoldRowFont = new iText.Font(iText.Font.FontFamily.HELVETICA, ROW_FONT_SIZE, iText.Font.BOLD);
				}

				return m_BoldRowFont;
			}

		}

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes all properties to string.Empty.
		/// </summary>
		public PDFGenerator()
		{
			Title = string.Empty;
			Subject = string.Empty;
			Creator = string.Empty;
			Author = string.Empty;
		}

		#endregion

		/// <summary>
		/// Creates a PDF file using the content
		/// </summary>
		/// <param name="fileName">Location of file</param>
		/// <param name="content">Content to place in the PDF</param>
		public abstract void Generate(string fileName, Content content);

		/// <summary>
		/// Converts inches to points
		/// </summary>
		/// <param name="inches">Inches to convert</param>
		/// <returns>Points that represent the inches</returns>
		virtual protected float InchesToPoints(double inches)
		{
			return (float)(inches * POINTS_PER_INCH);
		}

		/// <summary>
		/// Creates a document with the metadata at the location specified by fileName
		/// </summary>
		/// <param name="fileName">Location to create document</param>
		/// <returns>Open document (caller must close document)</returns>
		virtual protected Document CreateDocument(string fileName)
		{
			return CreateDocument(fileName, null);
		}

		/// <summary>
		/// Creates a document with the metadata at the location specified by fileName
		/// </summary>
		/// <param name="fileName">Location to create document</param>
		/// <param name="pageEventHelper">Page event helper</param>
		/// <returns>Open document (caller must close document)</returns>
		virtual protected Document CreateDocument(string fileName, PdfPageEventHelper pageEventHelper)
		{
			Document document = new Document(PageSize.LETTER);

			Writer = PdfWriter.GetInstance(document, new FileStream(fileName, FileMode.Create));
			Writer.ViewerPreferences = PdfWriter.PageLayoutSinglePage;
			Writer.PageEvent = pageEventHelper;

			document.AddAuthor(Author);
			document.AddCreationDate();
			document.AddCreator(Creator);
			document.AddSubject(Subject);
			document.AddTitle(Title);

			document.SetMargins(InchesToPoints(.5), InchesToPoints(.5), InchesToPoints(.75), InchesToPoints(.75));

			document.Open();

			return document;
		}

		/// <summary>
		/// Creates the parts listing
		/// </summary>
		/// <param name="document">Current document</param>
		/// <param name="parts">List of parts</param>
		virtual protected void CreatePartsListing(Document document, List<Part> parts)
		{
			document.Add(new Paragraph("Parts List", SectionFont));
			document.Add(new Paragraph(" "));

			PdfPTable table = new PdfPTable(new float[] { .2f, 1, .2f });
			table.WidthPercentage = 100;
			table.HeaderRows = 1;
			AddTableRow(table, new string[] { "Code", "Description", "Quantity" }, HeaderRowFont, new BaseColor(Color.Gray));

			BaseColor oddColor = new BaseColor(Color.LightGray);
			BaseColor evenColor = BaseColor.WHITE;

			if (parts != null)
			{
				for (int index = 0; index < parts.Count; index++)
				{
					Part part = parts[index];
					AddTableRow(table, new string[] { part.Code, part.Description, part.Quantity.ToString() }, RowFont, index % 2 == 0 ? evenColor : oddColor);
				}
			}

			document.Add(table);
		}

		/// <summary>
		/// Creates a page with the design image
		/// </summary>
		/// <param name="document">Current document</param>
		/// <param name="design">Design image</param>
		virtual protected void CreateImagePage(Document document, System.Drawing.Image design)
		{
			if (design.Width > design.Height)
			{
				design.RotateFlip(RotateFlipType.Rotate90FlipNone);
			}

			iText.Rectangle newPageSize;
			float adjWidth = design.Width + document.LeftMargin + document.RightMargin;
			float adjHeight = design.Height + document.TopMargin + document.BottomMargin + 10;

			if (adjWidth / adjHeight > 8.5 / 11)
			{
				// This means the width is what decides the height
				adjHeight = (float)(adjWidth / (8.5 / 11));
				newPageSize = new iText.Rectangle(adjWidth, (float)(adjWidth / (8.5 / 11)));
			}
			else
			{
				// Height decides width
				adjWidth = (float)(adjHeight * (8.5 / 11));
				newPageSize = new iText.Rectangle((float)(adjHeight * (8.5 / 11)), adjHeight);
			}

			iText.Image image = iText.Image.GetInstance(design, System.Drawing.Imaging.ImageFormat.Jpeg);
			image.Alignment = iText.Image.ALIGN_MIDDLE;
			document.SetPageSize(newPageSize);
			document.Add(image);
		}

		/// <summary>
		/// Adds a row to table using the font and backgroundColor
		/// </summary>
		/// <param name="table">Table where row should be added</param>
		/// <param name="values">Array of strings that are to be placed in each column of the row</param>
		/// <param name="font">Font to apply</param>
		/// <param name="backgroundColor">Background color to use</param>
		protected void AddTableRow(PdfPTable table, string[] values, iText.Font font, BaseColor backgroundColor)
		{
			for (int col = 0; col < table.NumberOfColumns; col++)
			{
				if (col < values.Length)
				{
					AddRowCell(table, values[col], font, backgroundColor);
				}
				else
				{
					AddRowCell(table, string.Empty, font, backgroundColor);
				}
			}
		}

		/// <summary>
		/// Adds a row cell to the table
		/// </summary>
		/// <param name="table">Table</param>
		/// <param name="value">Value to place in the cell</param>
		/// <param name="font">Font to use</param>
		/// <param name="backgroundColor">Background color to use</param>
		/// <returns>The pdfPCell that was created</returns>
		protected PdfPCell AddRowCell(PdfPTable table, string value, iText.Font font, BaseColor backgroundColor)
		{
			PdfPCell cell = new PdfPCell(new Phrase(value, font));
			cell.FixedHeight = TABLE_ROW_HEIGHT;
			cell.Border = PdfPCell.NO_BORDER;
			cell.BackgroundColor = backgroundColor;
			table.AddCell(cell);

			return cell;
		}

		/// <summary>
		/// Gets the application's name
		/// </summary>
		/// <returns>The application's description</returns>
		protected string GetApplicationName()
		{
			Assembly asm = Assembly.GetEntryAssembly();
			AssemblyDescriptionAttribute ada = ((AssemblyDescriptionAttribute)asm.GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false)[0]);

			return string.Concat(ada.Description, " (", asm.GetName().Version.ToString(), ")");
		}

		/// <summary>
		/// Gets the application's copyright info
		/// </summary>
		/// <returns>Application's copyright info</returns>
		protected string GetCopyright()
		{
			Assembly asm = Assembly.GetEntryAssembly();
			AssemblyCopyrightAttribute acra = ((AssemblyCopyrightAttribute)asm.GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false)[0]);

			return acra.Copyright;
		}
	}
}
