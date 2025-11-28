using System.Diagnostics;
using TNT.ToolStripItemManager;
using WeifenLuo.WinFormsUI.Docking;

namespace LandscapeSprinklerDesigner.MenuEvents;

abstract class DockMenuEvent(string text, string? toolTipText = null, Image? image = null) : ToolStripItemGroup(text, toolTipText, true, image)
{
    public DockContent? DockContent { get; set; }
  protected Tuple<DockContent, DockPanel>? tuple => base.ExternalObject as Tuple<DockContent, DockPanel>;
  protected DockContent? dockContent => tuple?.Item1 as DockContent;
  protected DockPanel? dockPanel => tuple?.Item2 as DockPanel;

    virtual public void OnApplicationIdle()
  {
        base.Checked = DockContent?.IsHidden == false;
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

    virtual public void OnMouseClicked(DockPanel dockPanel)
  {
    base.OnMouseClick(sender, e);

    if (base.Checked)
    {
            DockContent?.Show(dockPanel);
    }
    else
    {
            DockContent?.Hide();
    }
  }
}
