using System.Diagnostics;
using System.Windows.Forms;

namespace LandscapeSprinklerDesigner
{
	public partial class UpdateInformation : Form
	{
		public UpdateInformation()
		{
			InitializeComponent();
		}

		public DialogResult ShowDialog(IWin32Window owner, string thisVersion, string latestVersion, string url)
		{
			YourVersionLabel.Text = thisVersion;
			LatestVersionLabel.Text = latestVersion;
			Link.Click += new System.EventHandler((sender, e) =>
			{
				Process.Start(url);
				Close();
			});

			return base.ShowDialog(owner);
		}
	}
}
