using System.Windows.Forms;

namespace TNT.LSD.Background;

static class Extensions
{
  public static bool IsChecked(this ToolStripItem item)
  {
    var isChecked = false;

    if (item is ToolStripMenuItem)
    {
      isChecked = (item as ToolStripMenuItem).Checked;
    }
    else if (item is ToolStripButton)
    {
      isChecked = (item as ToolStripButton).Checked;
    }

    return isChecked;
  }

  public static void SetCheck(this ToolStripItem item, bool isChecked)
  {
    if (item is ToolStripMenuItem)
    {
      (item as ToolStripMenuItem).Checked = isChecked;
    }
    else if (item is ToolStripButton)
    {
      (item as ToolStripButton).Checked = isChecked;
    }
  }
}
