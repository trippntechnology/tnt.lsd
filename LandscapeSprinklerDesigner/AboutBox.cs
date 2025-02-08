using System.Reflection;
using TNT.Commons;
using TNT.Utilities;

namespace LandscapeSprinklerDesigner
{
  partial class AboutBox : Form
  {
    public AboutBox()
    {
      InitializeComponent();
      this.Text = String.Format("About {0}", AssemblyTitle);
      this.VersionLabel.Text = String.Format("Version {0}", AssemblyVersion);
      this.CopyrightLabel.Text = AssemblyCopyright;
    }

    #region Assembly Attribute Accessors

    public string AssemblyTitle { get { return Utilities.GetAssemblyAttribute<AssemblyTitleAttribute>(Assembly.GetExecutingAssembly())?.Title ?? string.Empty; } }
    public string AssemblyVersion => Global.getVersion();
    public string AssemblyDescription { get { return Utilities.GetAssemblyAttribute<AssemblyDescriptionAttribute>(Assembly.GetExecutingAssembly())?.Description ?? string.Empty; } }
    public string AssemblyProduct { get { return Utilities.GetAssemblyAttribute<AssemblyProductAttribute>(Assembly.GetExecutingAssembly())?.Product ?? string.Empty; } }
    public string AssemblyCopyright { get { return Utilities.GetAssemblyAttribute<AssemblyCopyrightAttribute>(Assembly.GetExecutingAssembly())?.Copyright ?? string.Empty; } }
    public string AssemblyCompany { get { return Utilities.GetAssemblyAttribute<AssemblyCompanyAttribute>(Assembly.GetExecutingAssembly())?.Company ?? string.Empty; } }

    #endregion

    private void AboutBox_Load(object sender, EventArgs e)
    {
      string? executablePath = Path.GetDirectoryName(Application.ExecutablePath);
      List<string> files = Path.GetDirectoryName(Application.ExecutablePath)?.let(path =>
      {
        return Directory.GetFiles(path, "*.dll", SearchOption.AllDirectories).Order().ToList();
      }) ?? new List<string>();

      foreach (string file in files)
      {
        try
        {
          Assembly asm = Assembly.LoadFile(file);
          ListViewItem item = listView1.Items.Add(Path.GetFileName(file));

          item.SubItems.Add(asm.GetName().Version?.ToString() ?? string.Empty);

          AssemblyCopyrightAttribute? assCopyAttr = Utilities.GetAssemblyAttribute<AssemblyCopyrightAttribute>(asm);
          if (assCopyAttr != null)
          {
            item.SubItems.Add(assCopyAttr.Copyright);
          }
          else
          {
            item.SubItems.Add(string.Empty);
          }
        }
        catch { }
      }
    }
  }
}
