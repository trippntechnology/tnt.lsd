using System.Collections;

namespace LandscapeSprinklerDesigner.Comparer;

/// <summary>
/// This class is an implementation of the 'IComparer' interface and provides
/// sorting functionality for ListView columns.
/// </summary>
public class ListViewColumnSorter : IComparer
{
  /// <summary>
  /// Specifies the column to be sorted
  /// </summary>
  private int ColumnToSort;

  /// <summary>
  /// Specifies the order in which to sort (i.e. 'Ascending' or 'Descending')
  /// </summary>
  private SortOrder OrderOfSort;

  /// <summary>
  /// Case insensitive comparer object
  /// </summary>
  private readonly CaseInsensitiveComparer ObjectCompare;

  /// <summary>
  /// Class constructor. Initializes various elements
  /// </summary>
  public ListViewColumnSorter()
  {
    // Initialize the column to '0'
    ColumnToSort = 0;

    // Initialize the sort order to 'none'
    OrderOfSort = SortOrder.None;

    // Initialize the CaseInsensitiveComparer object
    ObjectCompare = new CaseInsensitiveComparer();
  }

  /// <summary>
  /// This method is inherited from the IComparer interface. It compares the two objects passed using a case insensitive comparison.
  /// </summary>
  /// <param name="x">First object to be compared</param>
  /// <param name="y">Second object to be compared</param>
  /// <returns>The result of the comparison. "0" if equal, negative if 'x' is less than 'y' and positive if 'x' is greater than 'y'</returns>
  public int Compare(object? x, object? y)
  {
    if (x == null || y == null)
      return 0;

    // Cast the objects to be compared to ListViewItem objects
    ListViewItem listviewX = (ListViewItem)x;
    ListViewItem listviewY = (ListViewItem)y;

    // Get the sub-item values for the column being sorted
    string stringX = listviewX.SubItems.Count > ColumnToSort
        ? listviewX.SubItems[ColumnToSort].Text
        : string.Empty;
    string stringY = listviewY.SubItems.Count > ColumnToSort
        ? listviewY.SubItems[ColumnToSort].Text
        : string.Empty;

    // Determine if the column contains numeric or version data
    if (ColumnToSort == 1) // Version column
    {
      // Try to parse as version
      if (Version.TryParse(stringX, out var versionX) && Version.TryParse(stringY, out var versionY))
        return OrderOfSort == SortOrder.Ascending ? versionX.CompareTo(versionY) : versionY.CompareTo(versionX);
    }

    // Compare the two items
    int compareResult = ObjectCompare.Compare(stringX, stringY);

    // Calculate correct return value based on object comparison
    if (OrderOfSort == SortOrder.Ascending)
    {
      // Ascending sort is selected, return normal result of compare operation
      return compareResult;
    }
    else if (OrderOfSort == SortOrder.Descending)
    {
      // Descending sort is selected, return negative result of compare operation
      return -compareResult;
    }
    else
    {
      // Return '0' to indicate they are equal
      return 0;
    }
  }

  /// <summary>
  /// Gets or sets the number of the column to which to apply the sorting operation (Defaults to '0').
  /// </summary>
  public int SortColumn
  {
    set => ColumnToSort = value;
    get => ColumnToSort;
  }

  /// <summary>
  /// Gets or sets the order of sorting to apply (for example, 'Ascending' or 'Descending').
  /// </summary>
  public SortOrder Order
  {
    set => OrderOfSort = value;
    get => OrderOfSort;
  }
}
