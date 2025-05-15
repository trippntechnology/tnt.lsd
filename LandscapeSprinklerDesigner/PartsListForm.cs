using TNT.Commons;
using TNT.LSD.Inventory;

namespace LandscapeSprinklerDesigner;

public partial class PartsListForm : DockableForm
{
  private Debouncer debouncer = new Debouncer();

  public PartsListForm()
  {
    InitializeComponent();

    Parts_ListView.ListViewItemSorter = new Comparer.ListViewColumnSorter() { Order = SortOrder.Ascending };
  }

  /// <summary>
  /// Lists the parts in the parts list view
  /// </summary>
  /// <param name="parts">List of parts</param>
  /// and description</param>
  public void SetParts(List<Part> parts)
  {
    IProgress<List<Part>> doWork = new Progress<List<Part>>(listOfParts =>
    {
      Parts_ListView.BeginUpdate();
      Parts_ListView.Items.Clear();

      foreach (Part p in parts)
      {
        ListViewItem lvi = null;

        lvi = Parts_ListView.Items.Add(p.Code);
        lvi.SubItems.Add(p.Description);
        lvi.SubItems.Add(p.Quantity.ToString());
      }

      Parts_ListView.Sort();
      Parts_ListView.EndUpdate();
    });

    var task = debouncer.DebounceAsync(token =>
    {
      doWork.Report(parts);
    });
  }

  /// <summary>
  /// Adjusts the Description column when the Parts list view is resized
  /// </summary>
  private void PartsListForm_Resize(object sender, EventArgs e)
  {
    ListView.ColumnHeaderCollection columns = Parts_ListView.Columns;
    columns[1].Width = Width - columns[0].Width - columns[2].Width - 4;
  }

  private void Parts_ListView_ColumnClick(object sender, ColumnClickEventArgs e)
  {
    Comparer.ListViewColumnSorter? sorter = Parts_ListView.ListViewItemSorter as Comparer.ListViewColumnSorter;

    // Determine if clicked column is already the column that is being sorted.
    if (e.Column == sorter?.SortColumn)
    {
      // Reverse the current sort direction for this column.
      if (sorter.Order == SortOrder.Ascending)
      {
        sorter.Order = SortOrder.Descending;
      }
      else
      {
        sorter.Order = SortOrder.Ascending;
      }
    }
    else
    {
      // Set the column number that is to be sorted; default to ascending.
      sorter?.Also(s =>
      {
        s.SortColumn = e.Column;
        s.Order = SortOrder.Ascending;
      });
    }

    // Perform the sort with these new sort options.
    Parts_ListView.Sort();
  }
}
