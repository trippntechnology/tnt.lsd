using TNT.LSD.Components;

namespace LandscapeSprinklerDesigner.MenuEvents;

class CalculateDistanceMenuEvent() : MenuEvent(Resource.menu_calculate_length, Resource.menu_calculate_length_tooltip), ILicensed
{
    public override void OnMouseClicked(Form owner, TNTCAD cad, LayoutSettingsForm layoutSettings)
    {
        double length = Math.Round(cad.GetLength(), 2);
        MessageBox.Show(string.Format("{0} feet.", length), "Selected Length");
    }

    public override void OnApplicationIdle(TNTCAD cad)
    {
        Enabled = cad.LengthAvailable;
    }

    public void OnLicensedChanged(bool isLicensed)
    {
        Checked = !isLicensed ? false : Checked;
    }
}
