using System.Windows.Forms;

namespace TNT.LSD.Components;

public class TNTPanel : Panel
{
  protected override System.Drawing.Point ScrollToControl(Control activeControl)
  {
    return DisplayRectangle.Location;
  }
}
