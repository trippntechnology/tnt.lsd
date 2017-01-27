using System.Drawing;
using System.Windows.Forms;
using LSDComponents;
using TNT.Plugin;

namespace TNT.LSD.Plugins
{
	public class BackgroundImporterPlugin : Plugin.Plugin
	{
		/// <summary>
		/// Shows the BackgroundImporter dialog
		/// </summary>
		/// <param name="owner">Top-level form that owns the plugin</param>
		/// <param name="parameter">TNTCAD object</param>
		/// <returns>Null</returns>
		protected override object Execute(IWin32Window owner, object parameter)
		{
			TNTCAD cad = parameter as TNTCAD;

			if (cad != null)
			{
				using (BackgroundImporter bi = new BackgroundImporter())
				{
					bi.ShowDialog(owner, cad.State);
				}
			}

			return null;
		}

		/// <summary>
		/// Overridden to eliminate need for license
		/// </summary>
		/// <param name="owner">Top-level form that owns the plugin</param>
		/// <param name="parameter">TNTCAD object</param>
		/// <param name="isLicensed">Ignored</param>
		/// <returns>Execute(owner, parameter)</returns>
		public override object Execute(IWin32Window owner, object parameter, bool isLicensed)
		{
			return Execute(owner, parameter);
		}

		/// <summary>
		/// Image associated with the plugin (form's icon)
		/// </summary>
		public override Image Image { get { return (new BackgroundImporter()).Icon.ToBitmap(); } }

		/// <summary>
		/// Name of the plugin (form's caption)
		/// </summary>
		public override string Text { get { return (new BackgroundImporter()).Text; } }

		/// <summary>
		/// Plugin's description used to display as tool tip hint
		/// </summary>
		public override string ToolTipText { get { return "Imports background image behind the grid layer"; } }
	}
}
