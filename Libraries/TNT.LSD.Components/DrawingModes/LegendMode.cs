using System.Drawing;
using System.Windows.Forms;
using TNT.LSD.Objects;
using TNT.LSD.Objects.Extensions;

namespace LSDComponents.DrawingModes
{
	public class LegendMode : DrawingMode
	{
		public LegendMode()
			: base()
		{
			DefaultObject = new Legend(new Point(TNTConstants.PIXELS_PER_FOOT, TNTConstants.PIXELS_PER_FOOT));
			DefaultObjectType = DefaultObject.GetType().ToString();
		}

		public LegendMode(LegendMode obj)
			: base(obj)
		{
		}

		public override DrawingMode Clone()
		{
			return new LegendMode(this);
		}

		public override void OnMouseMove(TNTCAD cad, MouseEventArgs e, Keys modifierKeys)
		{
			var g = cad.CreateGraphics();
			var adjPos = new Point(e.X, e.Y).ToWorldCoordinateSpace(g, cad.SnapToGrid);
			cad.Refresh();
			DefaultObject.MoveTo(adjPos.X, adjPos.Y, true);
			DefaultObject.Draw(g, cad.DrawingOptions);
			DrawPointerPoint(g, adjPos, cad.DrawingOptions);
		}

		public override void OnMouseClick(TNTCAD cad, MouseEventArgs e, Keys modifierKeys)
		{
			cad.AddObject(DefaultObject.Clone());
		}
	}
}
