namespace LandscapeSprinklerDesigner.MenuEvents;

class PropertiesMenuEvent() : DockMenuEvent(Resource.menu_properties, Resource.menu_properties_tooltip), ILicensed
{
    public void OnLicensedChanged(bool isLicensed)
    {
        Checked = !isLicensed ? false : Checked;
        if (!Checked && DockContent != null)
        {
            DockContent.BeginInvoke(delegate { DockContent.Hide(); });
        }
    }
}