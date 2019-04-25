using LandscapeSprinklerDesigner.Properties;
using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using TNT.Configuration;
using TNT.Utilities;
using TNT.Web;

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

		public override void MouseClick(object sender, EventArgs e)
		{
			try
			{
				CheckVersion((curVer, appInfo) =>
				{
					if (appInfo != null)
					{
						Version latestVer = new Version(appInfo.Version);

						if (latestVer > curVer)
						{
							new UpdateInformation().ShowDialog(this.Owner, curVer.ToString(), latestVer.ToString(), appInfo.URL.ToString());
						}
						else
						{
							MessageBox.Show(this.Owner, "The latest version is installed", "Version Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
						}
					}
				});
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex.Message);
				MessageBox.Show(this.Owner, "The update server is unavailable. Please verify you're connected to the internet and try again.", "Update Server Unavailable", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}

		private void CheckVersion(Action<Version, TNT.Web.LSD.Models.Application> action)
		{
			RESTClient restClient = XmlSection<RESTClient>.Deserialize("TNT.Web");
			GuidAttribute attr = Utilities.GetAssemblyAttribute<GuidAttribute>(Assembly.GetExecutingAssembly());
			AssemblyFileVersionAttribute verAttr = Utilities.GetAssemblyAttribute<AssemblyFileVersionAttribute>(Assembly.GetExecutingAssembly());
			TNT.Web.LSD.Models.Response<TNT.Web.LSD.Models.Application> appResponse = restClient.Get<TNT.Web.LSD.Models.Response<TNT.Web.LSD.Models.Application>>(string.Format("Application/{0}", attr.Value));

			if (action != null && appResponse.Success)
			{
				action(new Version(verAttr.Version), appResponse.Payload);
			}
		}
	}
}
