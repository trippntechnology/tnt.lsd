using iText.Kernel.Events;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;
using iText.Layout;
using iText.Layout.Element;

namespace TNT.LSD.PDFGenerator;

public abstract class BaseEventHandler(Document document) : IEventHandler
{
  protected readonly Document document = document;

  public virtual void HandleEvent(Event @event)
  {
    PdfDocumentEvent docEvent = (PdfDocumentEvent)@event;
    HandleEvent(docEvent, docEvent.GetPage(), docEvent.GetDocument());
  }

  protected abstract void HandleEvent(PdfDocumentEvent pdfDocumentEvent, PdfPage pdfPage, PdfDocument pdfDocument);

  protected iText.Kernel.Geom.Rectangle getHeaderRect()
  {
    PageSize pageSize = document.GetPdfDocument().GetDefaultPageSize();
    float left = document.GetLeftMargin();
    float bottom = pageSize.GetHeight() - document.GetTopMargin();
    float width = pageSize.GetWidth() - document.GetLeftMargin() - document.GetRightMargin();
    float height = document.GetTopMargin();
    return new iText.Kernel.Geom.Rectangle(left, bottom, width, height);
  }

  protected iText.Kernel.Geom.Rectangle getFooterRect()
  {
    PageSize pageSize = document.GetPdfDocument().GetDefaultPageSize();
    float x = document.GetLeftMargin();
    float y = 0;
    float width = pageSize.GetWidth() - document.GetLeftMargin() - document.GetRightMargin();
    float height = document.GetBottomMargin();
    return new iText.Kernel.Geom.Rectangle(x, y, width, height);
  }

  protected Canvas getCanvas(PdfPage pdfPage, iText.Kernel.Geom.Rectangle rect)
  {
    var pdfDocument = document.GetPdfDocument();
    PdfCanvas canvas = new PdfCanvas(pdfPage.NewContentStreamBefore(), pdfPage.GetResources(), pdfDocument);
    return new Canvas(canvas, rect);
  }

  protected float getTableHeight(Table table)
  {
    // Create a temporary PDF document to measure the table height
    using (MemoryStream ms = new MemoryStream())
    {
      PdfWriter writer = new PdfWriter(ms);
      PdfDocument pdfDoc = new PdfDocument(writer);
      Document doc = new Document(pdfDoc);

      // Create a new page and add the table to the temporary document
      pdfDoc.AddNewPage();
      doc.Add(table);

      // Get the table height
      float height = table.GetHeight().GetValue();

      // Close the temporary document
      doc.Close();

      return height;
    }
  }
}
