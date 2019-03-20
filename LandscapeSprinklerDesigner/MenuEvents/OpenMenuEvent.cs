using LandscapeSprinklerDesigner.Properties;
using Microsoft.Win32;
using System;
using System.IO;
using System.Windows.Forms;
using TNT.Utilities;

namespace LandscapeSprinklerDesigner.MenuEvents
{
	public delegate void LoadLayoutDelegate(string fileName);

	class OpenMenuEvent : MenuEvent
	{
		private OpenFileDialog openFileDialog;
		private ApplicationRegistry applicationRegistry = new ApplicationRegistry(Registry.CurrentUser, "Tripp'n Technology", "LandscapeSprinklerDesigner");

		public override string Text => Resources.menu_open;

		public override string ToolTipText => Resources.menu_open_tooltip;

		public LoadLayoutDelegate LoadLayout { get; set; }

		public OpenMenuEvent() : base(ResourceToImage("LandscapeSprinklerDesigner.Images.open.png"))
		{
			openFileDialog = new OpenFileDialog();
			openFileDialog.DefaultExt = "lsd";
			openFileDialog.Filter = "Landscape Sprinkler Design files|*.lsd;*.lsdx";
			openFileDialog.RestoreDirectory = true;
			openFileDialog.Title = Resources.menu_open;
		}

		public override void MouseClick(object sender, EventArgs e)
		{
			base.MouseClick(sender, e);
			openFileDialog.InitialDirectory = applicationRegistry.ReadString("InitialDirectory", string.Empty);

			if (HandleUnsavedChanges() && openFileDialog.ShowDialog() == DialogResult.OK)
			{
				LoadLayout?.Invoke(openFileDialog.FileName);
				applicationRegistry.WriteString("InitialDirectory", Path.GetDirectoryName(openFileDialog.FileName));
			}
		}
	}
}
