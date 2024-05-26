using iText.IO.Image;
using iText.Kernel.Events;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;
using System.Drawing.Imaging;
using TNT.Utilities;
using iTextColors = iText.Kernel.Colors;
using iTextImage = iText.Layout.Element.Image;

namespace TNT.LSD.PDFGenerator
{
  /// <summary>
  /// Generates a PDF for Sprinkler Supply Co
  /// </summary>
  public class SSCPDFGenerator : PDFGenerator
  {
    class StartEventHandler(Document document, Content content) : BaseEventHandler(document)
    {
      protected override void HandleEvent(PdfDocumentEvent pdfDocumentEvent, PdfPage pdfPage, PdfDocument pdfDocument)
      {
        // Create a table with two columns
        PageSize pageSize = pdfDocument.GetDefaultPageSize();
        Table headerTbl = new Table(2);
        var bottomBorder = new SolidBorder(iTextColors.ColorConstants.RED, 2f);

        // Write the design number in two columns
        Cell designNumberCell = new Cell().Add(new Paragraph(content.DesignNumber))
          .SetBorder(Border.NO_BORDER)
          .SetBorderBottom(bottomBorder)
          .SetTextAlignment(TextAlignment.LEFT);
        headerTbl.AddCell(designNumberCell);

        // Write the owner's name in the second column
        Cell ownersNameCell = new Cell().Add(new Paragraph(string.Format("{0}{1}", content.OwnerName, string.Concat(" (", DateTime.Now.ToShortDateString(), ")"))))
           .SetBorder(Border.NO_BORDER)
           .SetBorderBottom(bottomBorder)
           .SetTextAlignment(TextAlignment.RIGHT);
        headerTbl.AddCell(ownersNameCell);

        // Add the table to the page
        var rect = getHeaderRect();

        System.Diagnostics.Debug.WriteLine(rect.ToString());
        headerTbl.SetFixedPosition(rect.GetLeft(), rect.GetBottom(), rect.GetWidth());

        getCanvas(pdfPage, rect).Add(headerTbl);
      }
    }

    class EndEventHandler(Document document, string appName, string copyright) : BaseEventHandler(document)
    {
      protected override void HandleEvent(PdfDocumentEvent pdfDocumentEvent, PdfPage pdfPage, PdfDocument pdfDocument)
      {
        // Create a table with three columns
        PageSize pageSize = pdfDocument.GetDefaultPageSize();
        Table footerTbl = new Table(new float[] { 6f, 1, 6f });
        var topBorder = new SolidBorder(iTextColors.ColorConstants.BLUE, 2f);
        int pageNumber = pdfDocument.GetPageNumber(pdfPage);

        // Put the application name in the first column
        Cell appNameCell = new Cell().Add(new Paragraph(appName).SetFontSize(FOOTER_FONT_SIZE))
          .SetBorder(Border.NO_BORDER)
          .SetBorderTop(topBorder)
          .SetTextAlignment(TextAlignment.LEFT);
        footerTbl.AddCell(appNameCell);

        // Add a cell for the page number
        Cell pageNumberCell = new Cell().Add(new Paragraph(pageNumber.ToString()).SetFontSize(FOOTER_FONT_SIZE))
           .SetBorder(Border.NO_BORDER)
           .SetBorderTop(topBorder)
           .SetTextAlignment(TextAlignment.CENTER);
        footerTbl.AddCell(pageNumberCell);

        // Add copyright info
        Cell copyrightCell = new Cell().Add(new Paragraph(copyright).SetFontSize(FOOTER_FONT_SIZE))
          .SetBorder(Border.NO_BORDER)
           .SetBorderTop(topBorder)
           .SetTextAlignment(TextAlignment.RIGHT);
        footerTbl.AddCell(copyrightCell);

        // Add the table to the page
        var rect = getFooterRect();
        float x = document.GetLeftMargin();
        float y = document.GetBottomMargin();
        footerTbl.SetFixedPosition(rect.GetLeft(), rect.GetBottom()/* - getTableHeight(footerTbl)*/, rect.GetWidth());

        getCanvas(pdfPage, rect).Add(footerTbl);
      }
    }

    /// <summary>
    /// Creates a PDF file for Sprinkle Supply Co using the content
    /// </summary>
    /// <param name="fileName">Location of file</param>
    /// <param name="content">Content to place in the PDF</param>
    public override void Generate(string fileName, Content content)
    {
      Generate(fileName, (pdfDocument, document) =>
      {
        pdfDocument.AddEventHandler(PdfDocumentEvent.START_PAGE, new StartEventHandler(document, content));
        pdfDocument.AddEventHandler(PdfDocumentEvent.END_PAGE, new EndEventHandler(document, GetApplicationName(), GetCopyright()));

        CreateCoverPage(document, content);

        document.Add(new AreaBreak(AreaBreakType.NEXT_PAGE));

        CreatePartsListing(document, content.Parts);

        System.Drawing.Font font = new System.Drawing.Font("Arial", 8);
        Graphics graphics = Graphics.FromImage(content.Design);

        string disclaimer = Resources.Disclaimer;
        SizeF size = graphics.MeasureString(disclaimer, font, content.Design.Width);

        System.Drawing.Image image = new Bitmap(content.Design.Width, content.Design.Height + (int)size.Height);

        graphics = Graphics.FromImage(image);
        graphics.FillRectangle(new SolidBrush(Color.White), 0, 0, image.Width, image.Height);
        graphics.DrawImage(content.Design, new System.Drawing.Point(0, 0));

        graphics.DrawString(disclaimer, font, new SolidBrush(Color.Black), new RectangleF(new PointF(0, content.Design.Height), size));

        CreateImagePage(document, image);

        if (content.Coverage != null)
        {
          image = new Bitmap(content.Coverage.Width, content.Coverage.Height + (int)size.Height);

          graphics = Graphics.FromImage(image);
          graphics.FillRectangle(new SolidBrush(Color.White), 0, 0, image.Width, image.Height);
          graphics.DrawImage(content.Coverage, new System.Drawing.Point(0, 0));

          graphics.DrawString(disclaimer, font, new SolidBrush(Color.Black), new RectangleF(new PointF(0, content.Coverage.Height), size));

          CreateImagePage(document, image);
        }
      });
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

      // Convert the System.Drawing.Bitmap to a byte array
      byte[] imageBytes;
      using (MemoryStream ms = new MemoryStream())
      {
        Resources.ssc.Save(ms, ImageFormat.Png); // Use the appropriate format
        imageBytes = ms.ToArray();
      }

      // Create an iText ImageData object from the byte array
      ImageData imageData = ImageDataFactory.Create(imageBytes);

      // Create an iText Image element
      iTextImage pdfImage = new iTextImage(imageData).Scale(.3f, .3f).SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.CENTER);
      document.Add(pdfImage);

      document.Add(new Paragraph(" "));

      Table table = new Table(2).UseAllAvailableWidth();
      //table.DefaultCell.Border = PdfPCell.NO_BORDER;

      var cats = reflector.GetCategoriesByPriority();

      foreach (var category in cats)
      {
        List<PropertyReflector> props = (from p in reflector.Properties where p.Category == category orderby p.Priority select p).ToList();
        AddTableSection(table, category, props);
      }

      AddTableRow(table, [string.Empty, string.Empty]);

      AddTableRow(table, [string.Empty, string.Empty]);
      AddTableSection(table, "Comments");
      AddTableRow(table, [string.Empty, string.Empty]);

      Cell cell = new Cell(1, 2).Add(new Paragraph(content.Comments).SetFontSize(ROW_FONT_SIZE))
        .SetBorder(Border.NO_BORDER);
      table.AddCell(cell);

      document.Add(table);
    }

    private void AddTableSection(Table table, string category, List<PropertyReflector> props)
    {
      AddTableRow(table, [string.Empty, string.Empty]);

      if (!string.IsNullOrEmpty(category))
      {
        AddTableSection(table, category);
      }

      AddTableRow(table, [string.Empty, string.Empty]);

      List<List<string>> rows = [];

      foreach (PropertyReflector pr in props)
      {
        rows.Add(new List<string> { pr.DisplayName, pr.Value?.ToString() ?? string.Empty });
      }

      WriteTableData(table, rows);
    }

    /// <summary>
    /// Adds row that spans both columns with the section label
    /// </summary>
    /// <param name="table"></param>
    /// <param name="value"></param>
    protected void AddTableSection(Table table, string value)
    {
      Cell cell = new Cell(1, 2).Add(new Paragraph(value).SetFont(BoldFont).SetFontSize(SECTION_FONT_SIZE))
        .SetBorder(Border.NO_BORDER)
        .SetBorderTop(new SolidBorder(iTextColors.ColorConstants.BLUE, 2));
      table.AddCell(cell);
    }

    /// <summary>
    /// Writes data to the table
    /// </summary>
    /// <param name="table">Table where data should be written</param>
    /// <param name="data">Data</param>
    protected void WriteTableData(Table table, List<List<string>> data)
    {
      for (int row = 0; row < data.Count; row++)
      {
        AddTableRow(table, data[row], row % 2 == 0 ? iTextColors.ColorConstants.LIGHT_GRAY : iTextColors.ColorConstants.WHITE);
      }
    }

    /// <summary>
    /// Event to set the page number in the second column of the footer
    /// </summary>
    /// <param name="footer">Footer</param>
    /// <param name="document">Document</param>
    //protected void BeforeWriteFooter(PdfPTable footer, Document document)
    //{
    //  Phrase phrase = new Phrase(document.PageNumber.ToString());
    //  phrase.Font.Size = FOOTER_FONT_SIZE;
    //  footer.Rows[0].GetCells()[1].Phrase = phrase;
    //}
  }
}
