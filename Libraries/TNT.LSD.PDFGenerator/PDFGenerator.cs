using iText.IO.Image;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using System.Drawing.Imaging;
using System.Reflection;
using TNT.LSD.Inventory;
using iTextBorder = iText.Layout.Borders.Border;
using iTextColors = iText.Kernel.Colors;
using iTextFontConstants = iText.IO.Font.Constants;
using iTextImage = iText.Layout.Element.Image;

namespace TNT.LSD.PDFGenerator
{
  /// <summary>
  /// The abstract class that represents all PDF generators
  /// </summary>
  public abstract class PDFGenerator
  {
    #region public constants

    public const int POINTS_PER_INCH = 72;
    public const int SECTION_FONT_SIZE = 14;
    public const int ROW_FONT_SIZE = SECTION_FONT_SIZE - 5;
    public const int TABLE_ROW_HEIGHT = 14;

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
    protected PdfFont? m_SectionFont = null;

    /// <summary>
    /// Font to use for the header row in a table
    /// </summary>
    protected PdfFont? m_HeaderRowFont = null;

    /// <summary>
    /// Font to use for other table rows
    /// </summary>
    protected PdfFont? m_RowFont = null;

    /// <summary>
    /// Font to use for bolded table rows
    /// </summary>
    protected PdfFont? m_BoldRowFont = null;

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
    protected PdfWriter? pdfWriter { get; set; }


    public virtual PdfFont BoldFont => PdfFontFactory.CreateFont(iTextFontConstants.StandardFonts.HELVETICA_BOLD);
    public virtual PdfFont DefaultFont => PdfFontFactory.CreateFont(iTextFontConstants.StandardFonts.HELVETICA);

    /// <summary>
    /// Section font
    /// </summary>
    //protected virtual iText.Font SectionFont
    //{
    //  get
    //  {
    //    if (m_SectionFont == null)
    //    {
    //      m_SectionFont = new iText.Font(iText.Font.FontFamily.HELVETICA, SECTION_FONT_SIZE, iText.Font.BOLD);
    //    }

    //    return m_SectionFont;
    //  }
    //}

    /// <summary>
    /// Header row font
    /// </summary>
    //protected virtual iText.Font HeaderRowFont
    //{
    //  get
    //  {
    //    if (m_HeaderRowFont == null)
    //    {
    //      m_HeaderRowFont = new iText.Font(iText.Font.FontFamily.HELVETICA, ROW_FONT_SIZE, iText.Font.BOLD);
    //    }

    //    return m_HeaderRowFont;
    //  }
    //}

    /// <summary>
    /// Row font
    /// </summary>
    //protected virtual iText.Font RowFont
    //{
    //  get
    //  {
    //    if (m_RowFont == null)
    //    {
    //      m_RowFont = new iText.Font(iText.Font.FontFamily.HELVETICA, ROW_FONT_SIZE);
    //    }

    //    return m_RowFont;
    //  }
    //}

    /// <summary>
    /// Bolded row font
    /// </summary>
    //protected virtual iText.Font BoldRowFont
    //{
    //  get
    //  {
    //    if (m_BoldRowFont == null)
    //    {
    //      m_BoldRowFont = new iText.Font(iText.Font.FontFamily.HELVETICA, ROW_FONT_SIZE, iText.Font.BOLD);
    //    }

    //    return m_BoldRowFont;
    //  }

    //}

    #endregion

    #region public constructors

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

    public virtual void Generate(string filename, Action<PdfDocument, Document> onGenerate)
    {
      using (PdfWriter pdfWriter = new PdfWriter(filename))
      {
        using (PdfDocument pdfDocument = new PdfDocument(pdfWriter))
        {
          pdfDocument.SetDefaultPageSize(iText.Kernel.Geom.PageSize.LETTER);

          using (Document document = new Document(pdfDocument))
          {

            pdfDocument.GetDocumentInfo()
              .SetAuthor(Author)
              .AddCreationDate()
              .SetCreator(Creator)
              .SetSubject(Subject)
              .SetTitle(Title);

            document.SetMargins(InchesToPoints(.5), InchesToPoints(.5), InchesToPoints(.5), InchesToPoints(.5));

            onGenerate(pdfDocument, document);
          }
        }
      }
    }

    /// <summary>
    /// Converts inches to points
    /// </summary>
    /// <param name="inches">Inches to convert</param>
    /// <returns>Points that represent the inches</returns>
    protected virtual float InchesToPoints(double inches)
    {
      return (float)(inches * POINTS_PER_INCH);
    }

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
      List<string> columnNames = new List<String> { "Code", "Description", "Quantity" };

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
        for (int row = 0; row < parts.Count; row++)
        {
          Part part = parts[row];
          AddTableRow(table, new List<string>() { part.Code, part.Description, part.Quantity.ToString() }, row % 2 == 0 ? iTextColors.ColorConstants.LIGHT_GRAY : iTextColors.ColorConstants.WHITE);
        }
      }

      document.Add(table);
    }

    /// <summary>
    /// Creates a page with the design image
    /// </summary>
    /// <param name="document">Current document</param>
    /// <param name="design">Design image</param>
    protected virtual void CreateImagePage(Document document, System.Drawing.Image design)
    {
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

      document.Add(new Paragraph().Add(image));
    }

    protected virtual void CreateImagePage(Document document, string imagePath)
    {
      // Load image
      ImageData imageData = ImageDataFactory.Create(imagePath);
      iTextImage image = new iTextImage(imageData);

      //image.SetWidth(200 * image.GetImageWidth() / image.GetImageHeight());
      //image.SetHeight(200);

      //// Create a paragraph and add text and image inline
      //Paragraph paragraph = new Paragraph()
      //    .Add(beforeImageText)
      //    .Add(image)
      //    .Add(afterImageText);

      //// Add paragraph to the document
      //document.Add(paragraph);

      if (image.GetImageWidth() > image.GetImageHeight())
      {
        image.SetRotationAngle(Math.PI / 2);
      }

      document.Add(new AreaBreak(AreaBreakType.NEXT_PAGE));

      //iTextSharp.text.Rectangle newPageSize;
      //float adjWidth = design.Width + document.LeftMargin + document.RightMargin;
      //float adjHeight = design.Height + document.TopMargin + document.BottomMargin + 10;

      //if (adjWidth / adjHeight > 8.5 / 11)
      //{
      //  // This means the width is what decides the height
      //  adjHeight = (float)(adjWidth / (8.5 / 11));
      //  newPageSize = new iTextSharp.text.Rectangle(adjWidth, (float)(adjWidth / (8.5 / 11)));
      //}
      //else
      //{
      //  // Height decides width
      //  adjWidth = (float)(adjHeight * (8.5 / 11));
      //  newPageSize = new iTextSharp.text.Rectangle((float)(adjHeight * (8.5 / 11)), adjHeight);
      //}

      //iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(design, System.Drawing.Imaging.ImageFormat.Jpeg);
      //image.Alignment = iTextSharp.text.Image.ALIGN_MIDDLE;
      //document.SetPageSize(newPageSize);
      document.Add(new Paragraph().Add(image));
    }

    /// <summary>
    /// Adds a row to table using the font and backgroundColor
    /// </summary>
    /// <param name="table">Table where row should be added</param>
    /// <param name="values">Array of strings that are to be placed in each column of the row</param>
    /// <param name="font">Font to apply</param>
    /// <param name="backgroundColor">Background color to use</param>
    //protected void AddTableRow(Table table, Part part, iTextColors.Color backgroundColor)
    //{
    //  new List<string>() { part.Code, part.Description, part.Quantity.ToString() }.ForEach(value =>
    //  {
    //    var cell = new Cell()
    //      .Add(new Paragraph(value))
    //      .SetFontSize(ROW_FONT_SIZE)
    //      .SetBorder(iTextBorder.NO_BORDER)
    //      .SetBackgroundColor(backgroundColor);
    //    table.AddCell(cell);
    //  });
    //}

    protected void AddTableRow(Table table, List<string> values, int fontSize = ROW_FONT_SIZE)
    {
      AddTableRow(table, values, iTextColors.ColorConstants.WHITE, fontSize);
    }

    protected void AddTableRow(Table table, List<string> values, iTextColors.Color backgroundColor, int fontSize = ROW_FONT_SIZE)
    {
      AddTableRow(table, values, backgroundColor, DefaultFont, fontSize);
    }

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
      AssemblyDescriptionAttribute? ada = ((AssemblyDescriptionAttribute)asm.GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false)[0]);
      var title = ada.Description;
      var version = asm.GetName().Version;
      return version != null ? $"{title} ({version.ToString()})" : String.Empty;
    }

    /// <summary>
    /// Gets the application's copyright info
    /// </summary>
    /// <returns>Application's copyright info</returns>
    protected string GetCopyright()
    {
      Assembly? asm = Assembly.GetEntryAssembly();
      if (asm == null) return string.Empty;
      AssemblyCopyrightAttribute acra = ((AssemblyCopyrightAttribute)asm.GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false)[0]);

      return acra.Copyright;
    }
  }
}
