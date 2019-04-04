using LandscapeSprinklerDesigner.Properties;
using System;
using System.Windows.Forms;

namespace LandscapeSprinklerDesigner.MenuEvents
{
	class CalculateDistanceMenuEvent : MenuEvent
	{
		public override string Text => Resources.menu_calculate_length;

		public override string ToolTipText => Resources.menu_calculate_length_tooltip;

		public CalculateDistanceMenuEvent() : base(ResourceToImage("LandscapeSprinklerDesigner.Images.calculate_distance.png"))
		{
		}

		public override void MouseClick(object sender, EventArgs e)
		{
			double length = Math.Round(CAD.GetLength(), 2);
			MessageBox.Show(string.Format("{0} feet.", length), "Selected Length");
		}

		public override void OnApplicationIdle(object sender, EventArgs e)
		{
			this.Enabled = CAD.LengthAvailable;
		}
	}
}
