using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using TNT.Configuration;
using TNT.Utilities;
using TNT.Web;
using TNT.Web.LSD.Models;

namespace LandscapeSprinklerDesigner
{
	public partial class RegistrationForm : Form
	{
		public RegistrationForm()
		{
			InitializeComponent();

			System.Windows.Forms.Application.Idle += new EventHandler(Application_Idle);
		}

		public new DialogResult ShowDialog(IWin32Window owner)
		{
			DialogResult result = base.ShowDialog(owner);

			if (result == System.Windows.Forms.DialogResult.OK)
			{
				try
				{
					RESTClient restClient = XmlSection<RESTClient>.Deserialize("TNT.Web");
					GuidAttribute attr = Utilities.GetAssemblyAttribute<GuidAttribute>(Assembly.GetExecutingAssembly());
					string volSerialNumber = Registration.GetVolumeSerialNumber();
					RegistrationKey regKey = new RegistrationKey();
					regKey.License = LicenseText.Text;

					AuthorizationKeyRequest akr = new AuthorizationKeyRequest() { ApplicationID = new Guid(attr.Value), HardwareID = volSerialNumber, LicenseKey = regKey.License };
					Response<string> keyResponse = restClient.Post<Response<string>>("AuthorizationKey", akr);

					if (!keyResponse.Success)
					{
						throw new Exception(keyResponse.Message);
					}

					regKey.Authorization = keyResponse.Payload;
					string hash = Registration.GenerateSHA1Hash(string.Concat(volSerialNumber, attr.Value, LicenseText.Text));

					if (hash == regKey.Authorization)
					{
						string path = Assembly.GetExecutingAssembly().Location;
						path = Path.GetDirectoryName(path);

						Registration.SetRegistrationKey(regKey, Path.Combine(path, "license.txt"));
						MessageBox.Show(owner, "Registration complete.", "Registration Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show(owner, ex.Message, "Registration Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}

			return DialogResult;
		}

		protected void Application_Idle(object sender, EventArgs e)
		{
			RegisterButton.Enabled = Regex.IsMatch(LicenseText.Text, "^(([^-]{4})-){4}[^-]{4}$");
		}
	}



}
