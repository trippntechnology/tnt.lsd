using System.Drawing;
using TNT.LSD.Components;
using TNT.LSD.PDFGenerator;
using TNT.LSD.Settings;
using WeifenLuo.WinFormsUI.Docking;

namespace TNT.LSD.SSC;

abstract public class Plugin : TNT.Plugin.Manager.Plugin
{
  public override string MenuStripName => "MenuStrip1";

  public override string ToolStripName => "pluginToolStrip";

  public override bool LicenseRequired => true;

  protected void GeneratePDF(ApplicationData appData, TNTCAD _cad, string fileName)
  {
    // Create a copy so that it doesn't effect the actual layout
    var cad = new TNTCAD(_cad);

    // Unselect all objects so that they are included in the drawn image for the PDF
    cad.UnselectAll();
    cad.Repaint();

    cad.DisplayScale = 100;
    cad.DrawingOptions.ShowCoverage = false;

    cad.Repaint();
    var design = cad.Design.Clone() as Image;

    cad.DrawingOptions.ShowCoverage = true;
    cad.Repaint();

    var coverage = cad.Design.Clone() as Image;

    Content pdfContent = new Content()
    {
      Design = design,
      Coverage = coverage,
      DynamicProperties = cad.Settings,
      Parts = cad.GetPartsList()
    };

    SSCSettings sscSettings = cad.Settings as SSCSettings;

    if (sscSettings != null)
    {
      pdfContent.Comments = sscSettings.Comment;
      pdfContent.DesignNumber = sscSettings.Number;
      pdfContent.OwnerName = sscSettings.Name;
    }

    (new PDFGenerator.SSCPDFGenerator()).Generate(fileName, pdfContent);

    PDFForm pdfForm = new PDFForm();
    pdfForm.Show(fileName, appData.DockPanel, DockState.Document);
  }
}
