using LandscapeSprinklerDesigner.Utils;
using TNT.LSD.Components;

namespace LandscapeSprinklerDesigner.MenuEvents;

class CheckForUpdateMenuItem() : MenuEvent(Resource.menu_check_for_update, Resource.menu_check_for_update_tooltip)
{
    public override void OnApplicationIdle(TNTCAD cad)
    {
        Enabled = FileUtil.HasValidLicense();
    }

    public override void OnMouseClicked(Form owner, TNTCAD cad)
    {
        Global.CheckForUpdate(owner, false);
    }
}
