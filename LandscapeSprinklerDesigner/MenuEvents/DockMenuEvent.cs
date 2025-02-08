using System.Diagnostics;
using TNT.ToolStripItemManager;
using WeifenLuo.WinFormsUI.Docking;

namespace LandscapeSprinklerDesigner.MenuEvents;

abstract class DockMenuEvent : ToolStripItemGroup
{
  public override bool CheckOnClick => true;
  protected Tuple<DockContent, DockPanel>? tuple => base.ExternalObject as Tuple<DockContent, DockPanel>;
  protected DockContent? dockContent => tuple?.Item1 as DockContent;
  protected DockPanel? dockPanel => tuple?.Item2 as DockPanel;

  public DockMenuEvent(Image? image = null) : base(image)
  {
  }

  public override void OnApplicationIdle(object? sender, EventArgs e)
  {
    base.OnApplicationIdle(sender, e);
    base.Checked = dockContent?.IsHidden == false;
  }

  public override void OnLicenseChanged(bool isLicensed)
  {
    Debug.WriteLine($"DockMenuEvent::OnLicensedChanged({isLicensed})");
    base.OnLicenseChanged(isLicensed);
    if (!isLicensed)
    {
      try
      {
        base.Checked = false;
        if (dockContent != null)
        {
          dockContent.BeginInvoke(delegate
          {
            dockContent.IsHidden = true;
            dockContent.Hide();
          });
        }
      }
      catch { }
    }
  }

  public override void OnMouseClick(object? sender, EventArgs e)
  {
    base.OnMouseClick(sender, e);

    if (base.Checked)
    {
      dockContent?.Show(dockPanel);
    }
    else
    {
      dockContent?.Hide();
    }
  }
}
