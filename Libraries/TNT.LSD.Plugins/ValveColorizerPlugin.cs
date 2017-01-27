using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using LSDComponents;
using TNT.LSD.Objects;
using TNT.Plugin;

namespace TNT.LSD.Plugins
{
	/// <summary>
	/// Colorizes the valves using predefined colors.
	/// </summary>
	public class ValveColorizerPlugin : Plugin.Plugin
	{
		protected ValveColorizer m_Form = new ValveColorizer();

		/// <summary>
		/// Executes the colorizer
		/// </summary>
		/// <param name="owner">Top-level form that owns the plugin</param>
		/// <param name="parameter">TNTCAD object</param>
		/// <returns>null</returns>
		protected override object Execute(IWin32Window owner, object parameter)
		{
			TNTCAD cad = parameter as TNTCAD;

			if (cad != null)
			{
				List<Valve> valves = (from v in cad.State.ObjectLayers.Last() where v is Valve select v as Valve).ToList();
				m_Form.ShowDialog(owner, valves);
			}

			return null;
		}

		/// <summary>
		/// Image associated with the plugin (form's icon)
		/// </summary>
		public override Image Image { get { return m_Form.Icon.ToBitmap(); } }

		/// <summary>
		/// Name of the plugin (form's caption)
		/// </summary>
		public override string Text { get { return m_Form.Text; } }

		/// <summary>
		/// Plugin's description used to display as tool tip hint
		/// </summary>
		public override string ToolTipText { get { return "Colorizes all the valves using a defined color palette"; } }
	}
}
