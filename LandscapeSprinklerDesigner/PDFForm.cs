using WeifenLuo.WinFormsUI.Docking;

namespace LandscapeSprinklerDesigner
{
	public partial class PDFForm : DockableForm
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
}
