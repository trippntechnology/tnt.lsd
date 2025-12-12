using TNT.LSD.Components;


namespace LandscapeSprinklerDesigner.MenuEvents;

class ShowPartsMenuEvent() : MenuEvent(Resource.menu_show_parts, Resource.menu_show_parts_tooltip, checkOnClick: true), ILicensed
{
    public void OnLicensedChanged(bool isLicensed)
    {
        Checked = !isLicensed ? false : Checked;
    }

    public override void OnMouseClicked(Form owner, TNTCAD cad, LayoutSettingsForm layoutSettings)
    {
        cad.ShowPartsToolTip = Checked;
    }
}