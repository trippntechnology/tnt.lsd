using System;
using System.Drawing;
using System.Linq;
using TNT.LSD.Objects;

namespace LandscapeSprinklerDesigner.MenuEvents
{
	abstract class RotateMenuEvent : MenuEvent
	{
		public abstract int Angle { get; }

		public RotateMenuEvent(Image image = null) : base(image)
		{
		}

		public override void OnApplicationIdle(object sender, EventArgs e)
		{
			var paletteParts = (from s in CAD.SelectedObjects where s is PalettePart select s as PalettePart).ToList();
			this.Enabled = CAD.SelectedObjects.Count != 0 && CAD.SelectedObjects.Count == paletteParts.Count;
		}

		public override void MouseClick(object sender, EventArgs e)
		{
			CAD.SelectedObjects.ForEach(o =>
			{
				PalettePart p = o as PalettePart;
				p.RotationAngle += this.Angle;
			});

			CAD.ShowPropertyChanges();
		}
	}
}
