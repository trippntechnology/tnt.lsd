using WeifenLuo.WinFormsUI.Docking;

namespace TNT.LSD.SSC;

public partial class PDFForm : DockContent
{
  public PDFForm()
  {
    InitializeComponent();
  }

  public void Show(string fileName, DockPanel dockPanel, DockState dockState)
  {
    Text = fileName;
    Browser.Navigate(string.Concat(@"file:\", fileName));
    Show(dockPanel, dockState);
  }
}
