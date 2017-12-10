using System;
using System.Drawing;
using System.Windows.Forms;
using TNT.ToolStripItemManager;

namespace PalletDesigner.Events
{
	class Exit : ToolStripItemGroup
	{
		public override string Text => "Exit";

		public override string ToolTipText => "Exit";

		public Exit()
			: base()
		{
		}

		public override void MouseClick(object sender, EventArgs e)
		{
			base.MouseClick(sender, e);
			Application.Exit();
		}
	}
}
