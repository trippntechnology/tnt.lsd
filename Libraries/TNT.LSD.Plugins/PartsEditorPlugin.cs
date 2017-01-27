using System.Windows.Forms;
using LSDComponents;

namespace TNT.LSD.Plugins
{
	/// <summary>
	/// PartsEditorPlugin
	/// </summary>
	public class PartsEditorPlugin : Plugin.Plugin
	{
		protected PartsEditor m_PartsEditor = new PartsEditor();

		/// <summary>
		/// Shows the PartsEditor dialog
		/// </summary>
		/// <param name="owner">Top-level form that owns the plugin</param>
		/// <param name="parameter">TNTCAD object</param>
		/// <returns>Null</returns>
		protected override object Execute(IWin32Window owner, object parameter)
		{
			TNTCAD cad = parameter as TNTCAD;

			if (cad != null)
			{
				new PartsEditor().ShowDialog(owner, cad.State.StaticParts);
			}

			return null;
		}

		/// <summary>
		/// Image associated with the plugin (form's icon)
		/// </summary>
		public override System.Drawing.Image Image { get { return this.m_PartsEditor.Icon.ToBitmap(); } }

		/// <summary>
		/// Name of the plugin (form's caption)
		/// </summary>
		public override string Text { get { return this.m_PartsEditor.Text; } }

		/// <summary>
		/// Plugin's description used to display as tool tip hint
		/// </summary>
		public override string ToolTipText { get { return "Edit the static parts listing"; } }
	}
}
