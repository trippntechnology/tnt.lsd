using LandscapeSprinklerDesigner.Properties;
using System;

namespace LandscapeSprinklerDesigner.MenuEvents
{
	class RegisterMenuEvent : MenuEvent
	{
		public override string Text => Resources.menu_register;

		public override string ToolTipText => Resources.menu_register_tooltip;

		public RegisterMenuEvent() : base(ResourceToImage("LandscapeSprinklerDesigner.Images.application_key.png"))
		{
		}

		public override void OnMouseClick(object sender, EventArgs e)
		{
			using (RegistrationForm form = new RegistrationForm())
			{
				form.ShowDialog(this.Owner);
			}
		}
	}
}
