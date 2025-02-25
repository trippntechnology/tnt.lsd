using iText.IO.Image;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using System.Drawing.Imaging;
using System.Reflection;
using TNT.Commons;
using TNT.LSD.Inventory;
using iTextBorder = iText.Layout.Borders.Border;
using iTextColors = iText.Kernel.Colors;
using iTextFontConstants = iText.IO.Font.Constants;
using iTextImage = iText.Layout.Element.Image;

namespace TNT.LSD.PDFGenerator;

/// <summary>
/// The abstract class that represents a PDF generators
/// </summary>
public abstract class PDFGenerator
{
  #region public constants

  public const int POINTS_PER_INCH = 72;
  public const int SECTION_FONT_SIZE = 14;
  public const int ROW_FONT_SIZE = SECTION_FONT_SIZE - 5;

  /// <summary>
  /// Footer font size
  /// </summary>
  protected const float FOOTER_FONT_SIZE = ROW_FONT_SIZE; // -1 uses default size

  #endregion

  #region Properties

  /// <summary>
  /// Title of the PDF
  /// </summary>
  public string Title { get; set; } = string.Empty;

  /// <summary>
  /// Subject associated with the PDF
  /// </summary>
  public string Subject { get; set; } = string.Empty;

  /// <summary>
  /// Creator of the PDF
  /// </summary>
  public string Creator { get; set; } = string.Empty;

  /// <summary>
  /// Author of the PDF
  /// </summary>
  public string Author { get; set; } = string.Empty;

  protected virtual PdfFont BoldFont => PdfFontFactory.CreateFont(iTextFontConstants.StandardFonts.HELVETICA_BOLD);

  protected virtual PdfFont DefaultFont => PdfFontFactory.CreateFont(iTextFontConstants.StandardFonts.HELVETICA);

  #endregion

  /// <summary>
  /// Creates a PDF file using the content
  /// </summary>
  /// <param name="fileName">Location of file</param>
  /// <param name="content">Content to place in the PDF</param>
  public abstract void Generate(string fileName, Content content);

  /// <summary>
  /// This should be called by the subclass within <see cref="Generate(string, Content)"/>
  /// </summary>
  /// <param name="fileName">Location of file</param>
  /// <param name="onGenerate">Lambda providing <see cref="PdfDocument"/> and <see cref="Document"/> used to generate PDF</param>
  public virtual void Generate(string filename, Action<PdfDocument, Document> onGenerate)
  {
    using PdfWriter pdfWriter = new PdfWriter(filename);
    using PdfDocument pdfDocument = new PdfDocument(pdfWriter);
    using Document document = new Document(pdfDocument);

    pdfDocument.SetDefaultPageSize(iText.Kernel.Geom.PageSize.LETTER);
    pdfDocument.GetDocumentInfo()
      .SetAuthor(Author)
      .AddCreationDate()
      .SetCreator(Creator)
      .SetSubject(Subject)
      .SetTitle(Title);

    // Display one page at a time rather than continuous scrolling.
    pdfDocument.GetCatalog().SetPageLayout(PdfName.SinglePage);

    document.SetMargins(InchesToPoints(.625), InchesToPoints(.5), InchesToPoints(.5), InchesToPoints(.5));

    onGenerate(pdfDocument, document);
  }

  /// <summary>
  /// Converts inches to points
  /// </summary>
  /// <param name="inches">Inches to convert</param>
  /// <returns>Points that represent the inches</returns>
  public static float InchesToPoints(double inches) => (float)(inches * POINTS_PER_INCH);

  /// <summary>
  /// Creates the parts listing
  /// </summary>
  /// <param name="document">Current document</param>
  /// <param name="parts">List of parts</param>
  protected virtual void CreatePartsListing(Document document, List<Part>? parts)
  {
    document.Add(new Paragraph("Parts List").SetFont(BoldFont).SetFontSize(SECTION_FONT_SIZE));
    document.Add(new Paragraph());

    Table table = new Table(UnitValue.CreatePercentArray(new float[] { .4f, 1, .2f })).UseAllAvailableWidth();
    List<string> columnNames = new List<string> { "Code", "Description", "Quantity" };

    columnNames.ForEach(name =>
    {
      var cell = new Cell()
        .Add(new Paragraph(name)
          .SetFont(BoldFont))
        .SetFontSize(ROW_FONT_SIZE)
        .SetBorder(iTextBorder.NO_BORDER)
        .SetBackgroundColor(iTextColors.ColorConstants.GRAY);

      table.AddHeaderCell(cell);
    });

    if (parts != null)
    {
      List<Part> orderedParts = parts.OrderBy(p => p.Code).ToList();
      for (int row = 0; row < orderedParts.Count; row++)
      {
        Part part = orderedParts[row];
        AddTableRow(table, new List<string>() { part.Code ?? string.Empty, part.Description ?? string.Empty, part.Quantity.ToString() }, row % 2 == 0 ? iTextColors.ColorConstants.LIGHT_GRAY : iTextColors.ColorConstants.WHITE);
      }
    }

    document.Add(table);
  }

  /// <summary>
  /// Creates a page with the design image
  /// </summary>
  /// <param name="document">Current document</param>
  /// <param name="design">Design image</param>
  protected virtual void CreateImagePage(Document document, System.Drawing.Image? design)
  {
    if (design == null) return;

    // Convert System.Drawing.Image to byte array
    byte[] imageBytes;
    using (MemoryStream ms = new MemoryStream())
    {
      design.Save(ms, ImageFormat.Jpeg); // Use the appropriate format
      imageBytes = ms.ToArray();
    }

    // Create ImageData object from byte array
    ImageData imageData = ImageDataFactory.Create(imageBytes);

    // Create iText Image element
    iTextImage image = new iTextImage(imageData);

    if (image.GetImageWidth() > image.GetImageHeight())
    {
      image.SetRotationAngle(Math.PI / 2);
    }

    document.Add(image.SetAutoScale(true));
  }

  protected void AddTableRow(Table table, List<string> values, int fontSize = ROW_FONT_SIZE) => AddTableRow(table, values, iTextColors.ColorConstants.WHITE, fontSize);

  protected void AddTableRow(Table table, List<string> values, iTextColors.Color backgroundColor, int fontSize = ROW_FONT_SIZE) => AddTableRow(table, values, backgroundColor, DefaultFont, fontSize);

  protected void AddTableRow(Table table, List<string> values, iTextColors.Color backgroundColor, PdfFont font, int fontSize = ROW_FONT_SIZE)
  {
    values.ForEach(value =>
    {
      var cell = new Cell()
        .Add(new Paragraph(value))
        .SetFont(font)
        .SetFontSize(fontSize)
        .SetBorder(iTextBorder.NO_BORDER)
        .SetBackgroundColor(backgroundColor);
      table.AddCell(cell);
    });
  }


  /// <summary>
  /// Gets the application's name
  /// </summary>
  /// <returns>The application's description</returns>
  protected string GetApplicationName()
  {
    Assembly? asm = Assembly.GetEntryAssembly();
    if (asm == null) return string.Empty;
    AssemblyDescriptionAttribute? ada = (AssemblyDescriptionAttribute)asm.GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false)[0];
    var title = ada.Description;
    var version = getVersion();
    return version != null ? $"{title} ({version})" : string.Empty;
  }

  private string getVersion()
  {
    return Assembly.GetEntryAssembly()?.GetName().Version?.let(version =>
    {
      var values = version.ToString().Split('.').Take(3);
      return String.Join(".", values);
    }) ?? "0.0.0";
  }

  /// <summary>
  /// Gets the application's copyright info
  /// </summary>
  /// <returns>Application's copyright info</returns>
  protected string GetCopyright()
  {
    Assembly? asm = Assembly.GetEntryAssembly();
    if (asm == null) return string.Empty;
    AssemblyCopyrightAttribute acra = (AssemblyCopyrightAttribute)asm.GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false)[0];

    return acra.Copyright;
  }
}
