using LandscapeSprinklerDesigner.Properties;
using System;
using System.Linq;
using System.Windows.Forms;
using TNT.LSD.Objects;

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
			this.Enabled = (from o in CAD.SelectedObjects where o is LateralPart select o as LateralPart).ToList().Count > 0;
		}

		public override void MouseClick(object sender, EventArgs e)
		{
			var lateralParts = (from o in CAD.SelectedObjects where o is LateralPart select o as LateralPart).ToList();
			double gpm = 0;

			lateralParts.ForEach(p => gpm += Convert.ToDouble(p.GPM));

			MessageBox.Show(this.Owner, string.Format("GPM of selected parts: {0}", gpm), "Selected GPM");
		}
	}
}
