using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Event;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;
using iTextColors = iText.Kernel.Colors;

namespace TNT.LSD.PDFGenerator
{
  /// <summary>
  /// Represents a generic document
  /// </summary>
  public class GenericPDFGenerator : PDFGenerator
  {
    class StartEventHandler(Document document, Content content) : BaseEventHandler(document)
    {
      protected override void HandleEvent(PdfDocumentEvent pdfDocumentEvent, PdfPage? pdfPage, PdfDocument pdfDocument)
      {
        if (pdfPage == null) return;

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
        var headerRect = getHeaderRect();
        headerTbl.SetFixedPosition(headerRect.GetLeft(), headerRect.GetBottom(), headerRect.GetWidth());
        getCanvas(pdfPage, headerRect).Add(headerTbl);
      }

      protected override void OnAcceptedEvent(AbstractPdfDocumentEvent @event)
      {
      }
    }

    class EndEventHandler(Document document, string applicationName, string copyright) : BaseEventHandler(document)
    {
      protected override void HandleEvent(PdfDocumentEvent pdfDocumentEvent, PdfPage? pdfPage, PdfDocument pdfDocument)
      {
        if (pdfPage == null) return;

        PageSize pageSize = pdfDocument.GetDefaultPageSize();
        Table footerTbl = new Table(new float[] { 6f, 1, 6f });
        var topBorder = new SolidBorder(iTextColors.ColorConstants.BLUE, 2f);

        Cell appNameCell = new Cell().Add(new Paragraph(applicationName).SetFontSize(FOOTER_FONT_SIZE))
          .SetBorder(Border.NO_BORDER)
          .SetBorderTop(topBorder);
        footerTbl.AddCell(appNameCell);

        Cell pageNumberCell = new Cell().Add(new Paragraph("<page number here>").SetFontSize(FOOTER_FONT_SIZE))
          .SetBorder(Border.NO_BORDER)
          .SetBorderTop(topBorder)
          .SetTextAlignment(TextAlignment.CENTER);
        footerTbl.AddCell(pageNumberCell);

        Cell copyrightCell = new Cell().Add(new Paragraph(copyright).SetFontSize(FOOTER_FONT_SIZE))
           .SetBorder(Border.NO_BORDER)
           .SetBorderTop(topBorder)
           .SetTextAlignment(TextAlignment.RIGHT);
        footerTbl.AddCell(copyrightCell);

        // Add the table to the page
        var footerRect = getFooterRect();
        footerTbl.SetFixedPosition(footerRect.GetLeft(), footerRect.GetTop() - getTableHeight(footerTbl, pageSize), footerRect.GetWidth());
        getCanvas(pdfPage, footerRect).Add(footerTbl);
      }

      protected override void OnAcceptedEvent(AbstractPdfDocumentEvent @event)
      {
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
