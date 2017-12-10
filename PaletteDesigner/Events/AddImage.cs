using LSDComponents;
using System;
using System.Windows.Forms;

namespace PalletDesigner.Events
{
	class AddImage : NodeEvents
	{
		//protected new Tuple<PalletNodeTreeView, PropertyGrid, OpenFileDialog> Tuple => ExternalObject as Tuple<PalletNodeTreeView, PropertyGrid, OpenFileDialog>;

		public OpenFileDialog OpenFileDialog { get { return (ExternalObject as Tuple<PalletNodeTreeView, PropertyGrid, OpenFileDialog>).Item3; } }

		public override string Text => "Add Image";

		public override string ToolTipText => "Add image to node";

		public override void MouseClick(object sender, EventArgs e)
		{
			base.MouseClick(sender, e);
			using (OpenFileDialog ofd = new OpenFileDialog())
			{
				if (this.OpenFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					PalletNodeTreeView.AddImage(this.OpenFileDialog.FileName);
				}
			}
		}
	}
}
