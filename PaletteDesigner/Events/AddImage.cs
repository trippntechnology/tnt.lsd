using LSDComponents;
using System;
using System.Windows.Forms;

namespace PalletDesigner.Events
{
	class AddImage : NodeEvents
	{
		public OpenFileDialog OpenFileDialog { get { return (ExternalObject as Tuple<object, object>).Item2 as OpenFileDialog; } }

		public override string Text => "Add Image";

		public override string ToolTipText => "Add image to node";

		public override void OnMouseClick(object sender, EventArgs e)
		{
			base.OnMouseClick(sender, e);
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
