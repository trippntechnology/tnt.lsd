using System.Diagnostics;
using TNT.ToolStripItemManager;
using WeifenLuo.WinFormsUI.Docking;

namespace LandscapeSprinklerDesigner.MenuEvents;

abstract class DockMenuEvent(string text, string? toolTipText = null) : ToolStripItemGroup(text, toolTipText, true)
{
    public DockContent? DockContent { get; set; }

    virtual public void OnApplicationIdle()
    {
        //base.Checked = DockContent?.IsHidden == false;
    }

    virtual public void OnLicenseChanged(bool isLicensed)
    {
        Debug.WriteLine($"DockMenuEvent::OnLicensedChanged({isLicensed})");
        if (!isLicensed)
        {
            try
            {
                base.Checked = false;
                if (DockContent != null)
                {
                    DockContent.BeginInvoke(delegate
                    {
                        DockContent.IsHidden = true;
                        DockContent.Hide();
                    });
                }
            }
            catch { }
        }
    }

    virtual public void OnMouseClicked(DockPanel dockPanel)
    {
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
