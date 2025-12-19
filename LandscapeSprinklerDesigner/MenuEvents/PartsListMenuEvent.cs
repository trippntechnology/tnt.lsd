namespace LandscapeSprinklerDesigner.MenuEvents;

class PartsListMenuEvent() : DockMenuEvent(Resource.menu_parts_listing, Resource.menu_parts_listing_tooltip), ILicensed
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
