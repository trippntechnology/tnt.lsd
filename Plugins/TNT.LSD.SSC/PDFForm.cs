using Microsoft.Web.WebView2.Core;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace TNT.LSD.SSC;

public partial class PDFForm : DockContent
{
    public PDFForm()
    {
        InitializeComponent();
    }

    public async void Show(string fileName, DockPanel dockPanel, DockState dockState)
    {
        if (!IsWebView2Installed())
        {
            MessageBox.Show("Microsoft Edge WebView2 Runtime is not installed. Please install it to view PDF files.", "WebView2 Not Installed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        Text = fileName;
        await Browser.EnsureCoreWebView2Async();
        Browser.Source = new Uri(fileName);
        Show(dockPanel, dockState);
    }

    public bool IsWebView2Installed()
    {
        try
        {
            // This returns the version string (e.g., "121.0.2277.83") if found.
            // It throws an exception if the runtime is missing.
            string version = CoreWebView2Environment.GetAvailableBrowserVersionString();
            return !string.IsNullOrEmpty(version);
        }
        catch (WebView2RuntimeNotFoundException)
        {
            return false;
        }
        catch (Exception)
        {
            return false;
        }
    }
}
