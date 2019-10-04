using System;
using System.Linq;
using System.Windows.Forms;
using TNT.Cryptography;

namespace LandscapeSprinklerDesigner
{
	public partial class RegistrationForm : Form
	{
		private const string BEGIN = "--- BEGIN ---";
		private const string END = "--- END ---";

		public RegistrationForm()
		{
			InitializeComponent();
			System.Windows.Forms.Application.Idle += new EventHandler(Application_Idle);
			this.ActiveControl = label1;
		}

		public new DialogResult ShowDialog(IWin32Window owner)
		{
			DialogResult result = base.ShowDialog(owner);

			if (result == System.Windows.Forms.DialogResult.OK)
			{
				try
				{
					var license = Global.SetLicense(LicenseText.Lines.ToList());
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
			var firstLine = LicenseText.Lines?.FirstOrDefault() ?? string.Empty;
			var lastLine = LicenseText.Lines?.LastOrDefault() ?? string.Empty;

			RegisterButton.Enabled = firstLine == BEGIN && lastLine == END;
		}
	}
}
