using LSDComponents;
using System.Windows.Forms;
using TNT.Plugin.Manager;
using WeifenLuo.WinFormsUI.Docking;

namespace TNT.LSD.Checker
{
	class Plugin : TNT.Plugin.Manager.Plugin
	{
		private const string imageName = "TNT.LSD.Checker.Images.eye.png";

		public override string MenuStripName => "MenuStrip1";

		public override string ToolStripName => "ToolStrip3";

		public override string Text => "Check Design";

		public override string ToolTipText => "Check design for issues";

		public override System.Drawing.Image Image => base.GetImage(imageName);

		public override void Execute(System.Windows.Forms.IWin32Window owner, System.Windows.Forms.ToolStripItem sender, IApplicationData content)
		{
			var applicationData = content as ApplicationData;

			var outputForm = new OutputForm();
			outputForm.Show(applicationData, DockState.Document);
		}

		public override System.Windows.Forms.MenuStrip GetMenuStrip()
		{
			return null;
		}

		public override System.Windows.Forms.ToolStrip GetToolStrip()
		{
			ToolStrip toolStrip = new ToolStrip();

			ToolStripButton toolStripButton = (ToolStripButton)CreateToolStripItem<ToolStripButton>(this.Text, base.GetImage(imageName), this.ToolTipText);
			toolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
			toolStrip.Items.Add(toolStripButton);

			return toolStrip;
		}
	}
}
