using iText.Kernel.Events;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;
using iTextColors = iText.Kernel.Colors;
using iTextProperties = iText.Layout.Properties;

namespace TNT.LSD.PDFGenerator
{
  /// <summary>
  /// Represents a generic document
  /// </summary>
  public class GenericPDFGenerator : PDFGenerator
  {
    class StartEventHandler(Document document, Content content) : BaseEventHandler(document)
    {
      protected override void HandleEvent(PdfDocumentEvent pdfDocumentEvent, PdfPage pdfPage, PdfDocument pdfDocument)
      {
        PageSize pageSize = pdfDocument.GetDefaultPageSize();
        Table headerTbl = new Table(2);
        var bottomBorder = new SolidBorder(iTextColors.ColorConstants.RED, 2f);

        Cell ownerNameCell = new Cell().Add(new Paragraph(content.OwnerName))
          .SetBorder(Border.NO_BORDER)
          .SetBorderBottom(bottomBorder)
          .SetTextAlignment(TextAlignment.LEFT);
        headerTbl.AddCell(ownerNameCell);

        Cell dateTimeCell = new Cell().Add(new Paragraph(DateTime.Now.ToShortDateString()))
           .SetBorder(Border.NO_BORDER)
           .SetBorderBottom(bottomBorder)
           .SetTextAlignment(TextAlignment.RIGHT);
        headerTbl.AddCell(dateTimeCell);

        // Add the table to the page
        var rect = getHeaderRect();

        System.Diagnostics.Debug.WriteLine(rect.ToString());
        headerTbl.SetFixedPosition(rect.GetLeft(), rect.GetBottom(), rect.GetWidth());

        getCanvas(pdfPage, rect).Add(headerTbl);
      }
    }

    class EndEventHandler(Document document, string applicationName, string copyright) : BaseEventHandler(document)
    {
      protected override void HandleEvent(PdfDocumentEvent pdfDocumentEvent, PdfPage pdfPage, PdfDocument pdfDocument)
      {
        PageSize pageSize = pdfDocument.GetDefaultPageSize();
        Table footerTbl = new Table(new float[] { 6f, 1, 6f });
        var topBorder = new SolidBorder(iTextColors.ColorConstants.BLUE, 2f);

        Cell cell = new Cell().Add(new Paragraph(applicationName).SetFontSize(FOOTER_FONT_SIZE))
          .SetBorder(Border.NO_BORDER)
          .SetBorderTop(topBorder);
        footerTbl.AddCell(cell);

        cell.Add(new Paragraph("").SetFontSize(FOOTER_FONT_SIZE))
          .SetBorder(Border.NO_BORDER)
          .SetBorderTop(topBorder)
          .SetHorizontalAlignment(iTextProperties.HorizontalAlignment.CENTER)
          .SetVerticalAlignment(VerticalAlignment.MIDDLE);
        footerTbl.AddCell(cell);

        cell.Add(new Paragraph(copyright).SetFontSize(FOOTER_FONT_SIZE))
          .SetBorder(Border.NO_BORDER)
          .SetBorderTop(topBorder)
          .SetHorizontalAlignment(iTextProperties.HorizontalAlignment.RIGHT);
        footerTbl.AddCell(cell);

        // Add the table to the page
        float x = document.GetLeftMargin();
        float y = document.GetBottomMargin();
        footerTbl.SetFixedPosition(x, y, pageSize.GetWidth() - document.GetLeftMargin() - document.GetRightMargin());

        PdfCanvas canvas = new PdfCanvas(pdfPage.NewContentStreamBefore(), pdfPage.GetResources(), pdfDocument);
        new Canvas(canvas, new iText.Kernel.Geom.Rectangle(0, 0, x, y)).Add(footerTbl);
      }
    }




    /// <summary>
    /// Creates a PDF file using the content
    /// </summary>
    /// <param name="fileName">Location of file</param>
    /// <param name="content">Content to place in the PDF</param>
    public override void Generate(string fileName, Content content)
    {
      Generate(fileName, (pdfDocument, document) =>
      {
        pdfDocument.AddEventHandler(PdfDocumentEvent.START_PAGE, new StartEventHandler(document, content));
        pdfDocument.AddEventHandler(PdfDocumentEvent.END_PAGE, new EndEventHandler(document, GetApplicationName(), GetCopyright()));

        CreatePartsListing(document, content.Parts);
        document.Add(new AreaBreak(AreaBreakType.NEXT_PAGE));
        CreateImagePage(document, content.Design);
      });
    }
  }
}
