using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using LSDComponents;
using TNT.Plugin;

namespace TNT.LSD.Plugins
{
	public class ExportJPGPlugin : Plugin.Plugin
	{
		/// <summary>
		/// Presents a save dialog to specify a file name and save the file as JPG
		/// </summary>
		/// <param name="owner">Top-level form that owns the plugin</param>
		/// <param name="parameter">TNTCAD object</param>
		/// <returns>Null</returns>
		protected override object Execute(IWin32Window owner, object parameter)
		{
			TNTCAD cad = parameter as TNTCAD;

			if (cad != null)
			{
				SaveFileDialog sfd = new SaveFileDialog();
				sfd.Title = "Export as JPG";
				sfd.Filter = "JPEG|*.jpg";
				sfd.DefaultExt = "jpg";

				if (sfd.ShowDialog(owner) == DialogResult.OK)
				{
					cad.Design.Save(sfd.FileName, ImageFormat.Jpeg);
				}
			}

			return null;
		}

		/// <summary>
		/// Image used by the plugin
		/// </summary>
		public override Image Image { get { return new Bitmap(this.GetType().Assembly.GetManifestResourceStream("TNT.LSD.Plugins.Images.jpg.png")); } }

		/// <summary>
		/// Name of the plugin
		/// </summary>		
		public override string Text { get { return "Export JPG"; } }

		/// <summary>
		/// Plugin's description used to display as tool tip hint
		/// </summary>
		public override string ToolTipText { get { return "Export as JPG"; } }
	}
}
