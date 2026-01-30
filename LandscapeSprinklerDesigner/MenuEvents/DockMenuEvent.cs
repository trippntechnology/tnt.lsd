using TNT.Commons;
using TNT.ToolStripItemManager;
using WeifenLuo.WinFormsUI.Docking;

namespace LandscapeSprinklerDesigner.MenuEvents;

abstract class DockMenuEvent(string text, string? toolTipText = null) : ToolStripItemGroup(text, toolTipText, true), ILicensed
{
    private DockContent? _dockContent;
    public DockContent? DockContent
    {
        get { return _dockContent; }
        set
        {
            _dockContent = value;
            base.Checked = _dockContent?.IsHidden == false;
        }
    }

    virtual public void OnApplicationIdle()
    {
    }

    public void OnLicensedChanged(bool isLicensed)
    {
        Checked = !isLicensed ? false : Checked;

        if (!Checked && DockContent != null)
        {
            try
            {
                DockContent.BeginInvoke(delegate { DockContent.Hide(); });
            }
            catch (Exception)
            {
                Logger.Info("Failed to hide parts list dock content.");
            }
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
