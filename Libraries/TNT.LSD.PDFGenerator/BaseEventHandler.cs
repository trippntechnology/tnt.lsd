using iText.Commons.Actions;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;
using iText.Kernel.Pdf.Event;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Layout;
using iText.Layout.Renderer;

namespace TNT.LSD.PDFGenerator;

public abstract class BaseEventHandler(Document document) : AbstractPdfDocumentEventHandler
{
  protected readonly Document document = document;


  public void onEvent(IEvent @event)
  {
    PdfDocumentEvent docEvent = (PdfDocumentEvent)@event;
    HandleEvent(docEvent, docEvent.GetPage(), docEvent.GetDocument());
  }

  protected abstract void HandleEvent(PdfDocumentEvent pdfDocumentEvent, PdfPage pdfPage, PdfDocument pdfDocument);

  protected iText.Kernel.Geom.Rectangle getHeaderRect()
  {
    PageSize pageSize = document.GetPdfDocument().GetDefaultPageSize();
    float left = document.GetLeftMargin();
    float bottom = pageSize.GetHeight() - document.GetTopMargin() + PDFGenerator.InchesToPoints(.125);
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

  protected float getTableHeight(Table table, PageSize pageSize)
  {
    // Create a temporary PDF document to measure the table height
    using (MemoryStream ms = new MemoryStream())
    {
      using PdfWriter writer = new PdfWriter(ms);
      using PdfDocument pdfDoc = new PdfDocument(writer);
      using Document doc = new Document(pdfDoc, pageSize);

      // Create a renderer for the table
      TableRenderer tableRenderer = (TableRenderer)table.CreateRendererSubTree()
          .SetParent(new DocumentRenderer(doc));

      // Layout the table and get the occupied area
      LayoutResult result = tableRenderer.Layout(new LayoutContext(
          new LayoutArea(1, new iText.Kernel.Geom.Rectangle(0, 0, pageSize.GetWidth(), pageSize.GetHeight()))
      ));

      // Get the height of the table
      float height = result.GetOccupiedArea().GetBBox().GetHeight();

      // Close the temporary document
      doc.Close();

      return height;
    }
  }
}
