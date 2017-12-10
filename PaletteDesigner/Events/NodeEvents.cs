using LSDComponents;
using System;
using System.Windows.Forms;
using TNT.ToolStripItemManager;

namespace PalletDesigner.Events
{
	abstract class NodeEvents : ToolStripItemGroup
	{
		//protected virtual Tuple<PalletNodeTreeView, PropertyGrid> Tuple => base.ExternalObject as Tuple<PalletNodeTreeView, PropertyGrid>;
		protected PalletNodeTreeView PalletNodeTreeView => (ExternalObject as Tuple<PalletNodeTreeView, PropertyGrid>).Item1 as PalletNodeTreeView;
		protected PropertyGrid PropertyGrid => (ExternalObject as Tuple<PalletNodeTreeView, PropertyGrid>).Item2 as PropertyGrid;
		
		public override void OnApplicationIdle(object sender, EventArgs e)
		{
			base.OnApplicationIdle(sender, e);
			Enabled = this.PalletNodeTreeView.SelectedNode != null;
		}
	}
}
