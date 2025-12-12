using TNT.LSD.Components;


namespace LandscapeSprinklerDesigner.MenuEvents;

class RegisterMenuEvent() : MenuEvent(Resource.menu_register, Resource.menu_register_tooltip)
{
    public override void OnMouseClicked(Form owner, TNTCAD cad, LayoutSettingsForm layoutSettings)
    {
        using RegistrationForm form = new RegistrationForm();
        form.ShowDialog(owner);
    }
}
