using TNT.LSD.Components;
using TNT.ToolStripItemManager.Extension;

namespace LandscapeSprinklerDesigner.MenuEvents;

class RegisterMenuEvent() : MenuEvent(Resource.menu_register, Resource.menu_register_tooltip, image: "LandscapeSprinklerDesigner.Images.application_key.png".ToImage())
{
    public override void OnMouseClicked(Form owner, TNTCAD cad)
    {
        using RegistrationForm form = new RegistrationForm();
        form.ShowDialog(owner);
    }
}
