using LSDComponents;
using System.Drawing;
using System.Windows.Forms;

namespace TNT.LSD.Plugins
{
	public class SaveBackgroundPlugin : Plugin.Plugin
	{
		protected override object Execute(IWin32Window owner, object parameter)
		{
			TNTCAD cad = parameter as TNTCAD;

			using (SaveFileDialog sfd = new SaveFileDialog())
			{
				if (sfd.ShowDialog(owner) == DialogResult.OK)
				{
					cad.State.BackgroundImage.Save(sfd.FileName);
				}
			}

			return null;
		}

		public override Image Image
		{
			get { return new Bitmap(16,16); }
		}

		public override string Text
		{
			get { return "Save Background Image"; }
		}

		public override string ToolTipText
		{
			get { return "Saves background image"; }
		}
	}
}
