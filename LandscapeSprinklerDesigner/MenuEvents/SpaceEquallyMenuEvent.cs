using TNT.LSD.Components;
using TNT.LSD.Objects;
using TNT.ToolStripItemManager.Extension;

namespace LandscapeSprinklerDesigner.MenuEvents;

class SpaceEquallyMenuEvent() : MenuEvent(Resource.menu_space_equally, Resource.menu_space_equally_tooltip, image: "LandscapeSprinklerDesigner.Images.space_equally.png".ToImage())
{
    public override void OnApplicationIdle(TNTCAD cad)
    {
        Enabled = cad.SelectedObjects.OfType<TNTPart>().ToList().Count > 2;
    }

    public override void OnMouseClicked(Form owner, TNTCAD cad)
    {
        cad.SpaceSelectedEqually();
    }
}
