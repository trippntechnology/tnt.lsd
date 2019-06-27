using LandscapeSprinklerDesigner.Properties;
using LSDComponents;
using System;
using System.Collections.Generic;
using TNT.LSD.Objects;

namespace LandscapeSprinklerDesigner.MenuEvents
{
	class NewMenuEvent : MenuEvent
	{
		public override string Text => Resources.menu_new;

		public override string ToolTipText => Resources.menu_new_tooltip;

		public NewMenuEvent()
			: base(ResourceToImage("LandscapeSprinklerDesigner.Images.new.png"))
		{

		}

		public override void OnMouseClick(object sender, EventArgs e)
		{
			base.OnMouseClick(sender, e);
			if (HandleUnsavedChanges())
			{
				NewLayoutDialog nld = new NewLayoutDialog();
				TNTCADState state = new TNTCADState(CAD);

				for (int index = 0; index < CAD.State.ObjectLayers.Count; index++)
				{
					state.ObjectLayers.Add(new List<TNTObject>());
				}

				if (nld.ShowDialog(Owner, state.Settings) == System.Windows.Forms.DialogResult.OK)
				{
					CAD.State = state;
					CAD.CurrentFileName = string.Empty;
				}

				CAD.Settings.DrawGrid = this.ToolStripItemGroupManager["Show Grid"]?.Checked ?? false;
				LayoutSettings.Settings = CAD.Settings;
				CAD.Refresh();
			}
		}
	}
}
