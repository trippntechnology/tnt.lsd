using System.Collections;

namespace LandscapeSprinklerDesigner.Comparer;

public class ListViewColumnSorter : IComparer
{
  public int Column { get; set; }
  public SortOrder Order { get; set; }
  public CaseInsensitiveComparer Comparer { get; set; }

  public ListViewColumnSorter()
  {
    Column = 0;
    Order = SortOrder.None;
    Comparer = new CaseInsensitiveComparer();
  }

  public int Compare(object x, object y)
  {
    int compareResult;
    ListViewItem listviewX, listviewY;

    // Cast the objects to be compared to ListViewItem objects
    listviewX = (ListViewItem)x;
    listviewY = (ListViewItem)y;

    var subItemsX = listviewX.SubItems;
    var subItemsY = listviewY.SubItems;

    // Compare the two items
    if (Column == 2)
    {
      compareResult = Convert.ToInt32(listviewX.SubItems[Column].Text) - Convert.ToInt32(listviewY.SubItems[Column].Text);
    }
    else if (Column < subItemsX.Count && Column < subItemsY.Count)
    {
      compareResult = Comparer.Compare(listviewX.SubItems[Column].Text, listviewY.SubItems[Column].Text);
    }
    else
    {
      compareResult = 1;
    }

    // Calculate correct return value based on object comparison
    if (Order == SortOrder.Ascending)
    {
      // Ascending sort is selected, return normal result of compare operation
      return compareResult;
    }
    else if (Order == SortOrder.Descending)
    {
      // Descending sort is selected, return negative result of compare operation
      return (-compareResult);
    }
    else
    {
      // Return '0' to indicate they are equal
      return 0;
    }
  }
}
