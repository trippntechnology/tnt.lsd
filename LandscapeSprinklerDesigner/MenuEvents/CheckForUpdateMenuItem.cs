using LandscapeSprinklerDesigner.Properties;
using System;
using System.Windows.Forms;

namespace LandscapeSprinklerDesigner.MenuEvents
{
	class CheckForUpdateMenuItem : MenuEvent
	{
		public override string Text => Resources.menu_check_for_update;

		public override string ToolTipText => Resources.menu_check_for_update_tooltip;

		public override void OnApplicationIdle(object sender, EventArgs e)
		{
			this.Enabled = false;
		}

		public override void OnMouseClick(object sender, EventArgs e)
		{
			try
			{
				MessageBox.Show("Not implemented");
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex.Message);
				MessageBox.Show(this.Owner, "The update server is unavailable. Please verify you're connected to the internet and try again.", "Update Server Unavailable", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}
	}
}
