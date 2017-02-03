using LSDComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TNT.LSD.Inventory;
using TNT.LSD.Plugins.Support;

namespace TNT.LSD.Plugins
{
	public class DGVPartsEditorPlugin : Plugin.Plugin
	{
		protected DGVPartsEditor _Editor = new DGVPartsEditor();

		public override string Text { get { return this._Editor.Text; } }

		public override string ToolTipText { get { return "Edit the static parts listing"; } }

		public override System.Drawing.Image Image { get { return this._Editor.Icon.ToBitmap(); } }

		protected override object Execute(System.Windows.Forms.IWin32Window owner, object parameter)
		{
			TNTCAD cad = parameter as TNTCAD;

			if (cad != null)
			{
				new DGVPartsEditor().ShowDialog(owner, cad.State.StaticParts);
			}

			return null;
		}
	}
}
