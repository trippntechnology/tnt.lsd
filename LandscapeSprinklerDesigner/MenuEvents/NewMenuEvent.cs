using LSDComponents;
using System;
using System.Collections.Generic;
using TNT.LSD.Objects;

namespace LandscapeSprinklerDesigner.MenuEvents
{
	class NewMenuEvent : MenuEvent
	{
		public override string Text => "&New";

		public override string ToolTipText => "Create new layout";

		public NewMenuEvent()
			: base(ResourceToImage("LandscapeSprinklerDesigner.Images.new.png"))
		{

		}

		public override void MouseClick(object sender, EventArgs e)
		{
			base.MouseClick(sender, e);
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
