using LandscapeSprinklerDesigner.Properties;
using System;
using System.Linq;
using System.Windows.Forms;
using TNT.LSD.Objects;
using TNT.LSD.Objects.Interfaces;

namespace LandscapeSprinklerDesigner.MenuEvents
{
	class SumGPMMenuEvent : MenuEvent
	{
		public override string Text => Resources.menu_sum_gpm;

		public override string ToolTipText => Resources.menu_sum_gpm_tooltip;

		public SumGPMMenuEvent() : base(ResourceToImage("LandscapeSprinklerDesigner.Images.sum_gpm.png"))
		{

		}

		public override void OnApplicationIdle(object sender, EventArgs e)
		{
			Enabled = (from o in CAD.SelectedObjects where o is ISummable select o as ISummable).ToList().Count > 0;
		}

		public override void OnMouseClick(object sender, EventArgs e)
		{
			var summableParts = (from o in CAD.SelectedObjects where o is ISummable select o as ISummable).ToList();
			double gpm = summableParts.Sum(p => p.GPM);
			MessageBox.Show(this.Owner, string.Format("GPM of selected parts: {0}", gpm), "Selected GPM");
		}
	}
}
