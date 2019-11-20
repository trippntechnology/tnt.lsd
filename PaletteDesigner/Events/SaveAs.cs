using System;
using System.Windows.Forms;
using TNT.ToolStripItemManager;

namespace PalletDesigner.Events
{
	class SaveAs : ToolStripItemGroup
	{
		public override string Text => "Save &As";

		public override string ToolTipText => "Save palette as";

		public SaveAs()
			: base()
		{
		}

		public override void OnMouseClick(object sender, EventArgs e)
		{
			base.OnMouseClick(sender, e);

			if (base.ExternalObject is Main main)
			{
				using (SaveFileDialog SaveDialog = new SaveFileDialog())
				{
					if (SaveDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
					{
						main.Pallet.Save(SaveDialog.FileName);
						main.CurrentFileName = SaveDialog.FileName;
					}
				}
			}
		}
	}
}
