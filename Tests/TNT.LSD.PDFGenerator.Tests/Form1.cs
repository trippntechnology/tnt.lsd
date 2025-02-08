using TNT.LSD.Inventory;
using TNT.LSD.Settings;

namespace TNT.LSD.PDFGenerator.Tests
{
  public partial class Form1 : Form
  {
    public Form1()
    {
      InitializeComponent();

      string fileName = "PDFGeneratorTest.pdf";
      PDFGenerator pdfGenerator = new GenericPDFGenerator();
      pdfGenerator = new SSCPDFGenerator() { Title = "Title", Author = "Author", Creator = "Creator", Subject = "Subject" };

      SSCSettings settings = new SSCSettings()
      {
        Comment = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nullam aliquet eleifend felis sit amet lacinia. Duis lacinia, risus sit amet aliquam ornare, dolor ipsum eleifend justo, a feugiat tellus tellus non turpis. Morbi congue, tellus ut fringilla vestibulum, nulla enim scelerisque justo, ut convallis nisl justo ut nisl. Ut viverra orci eget urna gravida quis venenatis nulla blandit. Aliquam metus metus, suscipit ac varius id, ultricies et est. Nunc elementum enim ac sem rhoncus mollis. Maecenas ultricies quam vel libero imperdiet aliquam. Duis imperdiet hendrerit ligula, interdum venenatis lorem consequat vel.\n\nVestibulum consectetur, erat nec pretium pretium, ligula tellus placerat dolor, vitae eleifend quam nisi ut arcu. Curabitur non urna urna, id facilisis purus. Proin tortor ante, dapibus sit amet dignissim in, luctus ut mauris. Nunc in velit quis sapien cursus auctor. Praesent nec magna at eros iaculis consectetur aliquet quis purus. Phasellus vitae dui ut leo bibendum fermentum eget sit amet dui. Fusce elit augue, ultrices at porta sed, dictum quis magna. Sed eros lectus, sollicitudin sed posuere eget, euismod nec nisl. Praesent fringilla imperdiet neque, at fringilla neque porta nec. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Donec dui lacus, dignissim non aliquet eget, iaculis vitae ante. Donec commodo felis eu metus rhoncus hendrerit.",
        CulinaryPSI = "< 40 PSI",
        CulinarySize = "3/4\"",
        CulinaryType = "Copper",
        DrawGrid = true,
        DrawUnits = true,
        EmailAddress = "address@domain.com",
        GridColor = Color.Blue,
        GridColorAlphaValue = 140,
        HeightInFeet = 150,
        IncludeCulinarySW = "Yes",
        IncludeSecondarySW = "No",
        LateralDrains = 2,
        MainlineDrains = 2,
        Name = "Owner Name",
        Number = "#41",
        RoundPipe = true,
        SecondaryPSI = "NA",
        SecondarySize = "NA",
        SecondaryType = "NA",
        ShowExternalCodes = true,
        TelephoneNumber = "808-888-8888",
        WidthInFeet = 150,
        StaticParts = new List<Part>()
      };

      Content content = new Content()
      {
        Comments = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nullam aliquet eleifend felis sit amet lacinia. Duis lacinia, risus sit amet aliquam ornare, dolor ipsum eleifend justo, a feugiat tellus tellus non turpis. Morbi congue, tellus ut fringilla vestibulum, nulla enim scelerisque justo, ut convallis nisl justo ut nisl. Ut viverra orci eget urna gravida quis venenatis nulla blandit. Aliquam metus metus, suscipit ac varius id, ultricies et est. Nunc elementum enim ac sem rhoncus mollis. Maecenas ultricies quam vel libero imperdiet aliquam. Duis imperdiet hendrerit ligula, interdum venenatis lorem consequat vel.\n\nVestibulum consectetur, erat nec pretium pretium, ligula tellus placerat dolor, vitae eleifend quam nisi ut arcu. Curabitur non urna urna, id facilisis purus. Proin tortor ante, dapibus sit amet dignissim in, luctus ut mauris. Nunc in velit quis sapien cursus auctor. Praesent nec magna at eros iaculis consectetur aliquet quis purus. Phasellus vitae dui ut leo bibendum fermentum eget sit amet dui. Fusce elit augue, ultrices at porta sed, dictum quis magna. Sed eros lectus, sollicitudin sed posuere eget, euismod nec nisl. Praesent fringilla imperdiet neque, at fringilla neque porta nec. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Donec dui lacus, dignissim non aliquet eget, iaculis vitae ante. Donec commodo felis eu metus rhoncus hendrerit.",
        DesignNumber = "DN123",
        OwnerName = "Design Owner",
        Parts = new List<Part>(),
        DynamicProperties = settings
      };

      for (int index = 0; index < 100; index++)
      {
        content.Parts.Add(new Part()
        {
          Code = string.Format("{0:D8}", index),
          Description = string.Format("Description for {0:D8}", index),
          Quantity = index
        });
      }

      content.Design = Image.FromFile("Test.jpg");

      pdfGenerator.Generate(fileName, content);

      Browser.Navigate(Path.Combine(AppContext.BaseDirectory, fileName));
    }
  }
}
