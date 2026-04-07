namespace LandscapeSprinklerDesigner.MenuEvents;

class LayoutSettingsEvent() : DockMenuEvent(Resource.menu_layout_settings, Resource.menu_layout_settings_tooltip), ILicensed
{
    public void OnLicensedChanged(bool isLicensed)
    {
        Checked = !isLicensed ? false : Checked;
        if (!Checked && DockContent != null)
        {
            if (DockContent.IsHandleCreated)
                DockContent.BeginInvoke(delegate { DockContent.Hide(); });
            else
                DockContent.Hide();
        }
    }
}
