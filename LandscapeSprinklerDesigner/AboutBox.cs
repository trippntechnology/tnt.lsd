using LandscapeSprinklerDesigner.Comparer;
using System.Reflection;
using TNT.Commons;

namespace LandscapeSprinklerDesigner;

partial class AboutBox : Form
{
  // ListView column sorter
  private readonly ListViewColumnSorter lvwColumnSorter;

  public AboutBox()
  {
    InitializeComponent();

    // Create an instance of a ListView column sorter and assign it to the ListView control
    lvwColumnSorter = new ListViewColumnSorter();
    listView1.ListViewItemSorter = lvwColumnSorter;

    this.Text = String.Format("About {0}", AssemblyTitle);
    this.VersionLabel.Text = String.Format("Version {0}", AssemblyVersion);
    this.CopyrightLabel.Text = AssemblyCopyright;
  }

  #region Assembly Attribute Accessors

  private readonly Assembly _executingAssembly = Assembly.GetExecutingAssembly();

  public string AssemblyTitle => _executingAssembly.GetCustomAttribute<AssemblyTitleAttribute>()?.Title ?? string.Empty;
  public string AssemblyVersion => Global.getVersion();
  public string AssemblyDescription => _executingAssembly.GetCustomAttribute<AssemblyDescriptionAttribute>()?.Description ?? string.Empty;
  public string AssemblyProduct => _executingAssembly.GetCustomAttribute<AssemblyProductAttribute>()?.Product ?? string.Empty;
  public string AssemblyCopyright => _executingAssembly.GetCustomAttribute<AssemblyCopyrightAttribute>()?.Copyright ?? string.Empty;
  public string AssemblyCompany => _executingAssembly.GetCustomAttribute<AssemblyCompanyAttribute>()?.Company ?? string.Empty;

  #endregion

  private void AboutBox_Load(object sender, EventArgs e)
  {
    string? executablePath = Path.GetDirectoryName(Application.ExecutablePath);
    List<string> files = Path.GetDirectoryName(Application.ExecutablePath)?.Let(path =>
    {
      var list = Directory.GetFiles(path, "*.dll", SearchOption.AllDirectories).Distinct().Order().ToList();
      return list.DistinctBy(x => Path.GetFileName(x)).ToList();
    }) ?? new List<string>();

    foreach (string file in files)
    {
      try
      {
        Assembly asm = Assembly.LoadFile(file);
        ListViewItem item = listView1.Items.Add(Path.GetFileName(file)); item.SubItems.Add(asm.GetName().Version?.ToString() ?? string.Empty);

        AssemblyCopyrightAttribute? assCopyAttr = asm.GetCustomAttribute<AssemblyCopyrightAttribute>();
        item.SubItems.Add(assCopyAttr?.Copyright ?? string.Empty);
      }
      catch { }
    }
  }

  /// <summary>
  /// Handles the ColumnClick event of the ListView control.
  /// </summary>
  /// <param name="sender">The source of the event.</param>
  /// <param name="e">The event data.</param>
  private void listView1_ColumnClick(object sender, ColumnClickEventArgs e)
  {
    // Determine if the clicked column is already the column that is being sorted
    if (e.Column == lvwColumnSorter.SortColumn)
    {
      // Reverse the current sort direction for this column
      lvwColumnSorter.Order = lvwColumnSorter.Order == SortOrder.Ascending
          ? SortOrder.Descending
          : SortOrder.Ascending;
    }
    else
    {
      // Set the column number that is to be sorted; default to ascending
      lvwColumnSorter.SortColumn = e.Column;
      lvwColumnSorter.Order = SortOrder.Ascending;
    }

    // Perform the sort with these new sort options
    listView1.Sort();
  }
}
