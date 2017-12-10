using LSDComponents;
using System;
using System.Windows.Forms;
using TNT.ToolStripItemManager;

namespace PalletDesigner.Events
{
	abstract class NodeEvents : ToolStripItemGroup
	{
		protected PalletNodeTreeView PalletNodeTreeView => (ExternalObject as Tuple<object, object>).Item1 as PalletNodeTreeView;
		protected PropertyGrid PropertyGrid => (ExternalObject as Tuple<object, object, object>).Item2 as PropertyGrid;

		public override void OnApplicationIdle(object sender, EventArgs e)
		{
			base.OnApplicationIdle(sender, e);
			Enabled = this.PalletNodeTreeView?.SelectedNode != null;
		}
	}
}
