namespace LandscapeSprinklerDesigner.MenuEvents;

class PartsPaletteEvent() : DockMenuEvent(Resource.menu_parts_palette, Resource.menu_parts_palette_tooltip), ILicensed
{
    public void OnLicensedChanged(bool isLicensed)
    {
        Checked = !isLicensed ? false : Checked;
    }
}