using LandscapeSprinklerDesigner.Properties;
using System;

namespace LandscapeSprinklerDesigner.MenuEvents
{
	class RegisterMenuEvent : MenuEvent
	{
		public override string Text => Resources.menu_register;

		public override string ToolTipText => Resources.menu_register_tooltip;

		public override void OnApplicationIdle(object sender, EventArgs e)
		{
			this.Enabled = false;
		}

		public override void MouseClick(object sender, EventArgs e)
		{
			using (RegistrationForm form = new RegistrationForm())
			{
				if (form.ShowDialog(this.Owner) == System.Windows.Forms.DialogResult.OK)
				{
					//m_IsAuthorized = null;
					//c.Visible = !IsAuthorized;
					//m_CommandManager["PartsList"].Visible = IsAuthorized;

					//if (IsAuthorized)
					//{
					//	(m_CommandManager["PartsList"].Tag as DockContent).Show();
					//}
				}
			}
		}
	}
}
