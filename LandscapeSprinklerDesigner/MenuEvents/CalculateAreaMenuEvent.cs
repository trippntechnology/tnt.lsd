using LandscapeSprinklerDesigner.Properties;
using System;
using System.Windows.Forms;

namespace LandscapeSprinklerDesigner.MenuEvents
{
	class CalculateAreaMenuEvent : MenuEvent
	{
		private const double ACRE_FEET = 43560.1742405;

		public override string Text => Resources.menu_calculate_area;

		public override string ToolTipText => Resources.menu_calculate_area_tooltip;

		public CalculateAreaMenuEvent() : base(ResourceToImage("LandscapeSprinklerDesigner.Images.calculate_area.png"))
		{
		}

		public override void OnApplicationIdle(object sender, EventArgs e)
		{
			this.Enabled = Enabled = CAD.AreaAvailable;
		}

		public override void MouseClick(object sender, EventArgs e)
		{
			double area = CAD.GetArea();
			double sqrFt = Math.Round(area, 2);
			double acre = Math.Round(area / ACRE_FEET, 2);
			MessageBox.Show(string.Format("{0} square feet. {1} acres", sqrFt, acre), "Selected Area");
		}
	}
}
