using LandscapeSprinklerDesigner.Properties;
using System;
using System.IO;
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

		public override void OnMouseClick(object sender, EventArgs e)
		{
			base.OnMouseClick(sender, e);
			var name = Path.GetFileNameWithoutExtension(CAD.CurrentFileName);
			double area = CAD.GetArea();
			double sqrFt = Math.Round(area, 2);
			double acre = Math.Round(area / 43560.1742405, 2);
			Clipboard.SetText($"{name}\t{DateTime.Now.ToShortDateString()}\t{acre}");
			MessageBox.Show(string.Format("{0} square feet. {1} acres", sqrFt, acre), "Selected Area");
		}
	}
}
