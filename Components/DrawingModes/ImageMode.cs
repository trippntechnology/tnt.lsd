using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using TNT.LSD.Objects;
using TNT.LSD.Objects.Extensions;
using TNT.Utilities;

namespace LSDComponents.DrawingModes
{
	public class ImageMode : DrawingMode
	{
		public ImageMode()
			: base()
		{
		}

		public ImageMode(ImageMode obj)
			: base(obj)
		{
		}

		#region Overrides

		public override DrawingMode Clone()
		{
			return new ImageMode(this);
		}

		public override void OnMouseMove(TNTCAD cad, System.Windows.Forms.MouseEventArgs e, System.Windows.Forms.Keys modifierKeys)
		{
			cad.Cursor = Cursors.Default;

			cad.Refresh();

			Graphics g = cad.CreateGraphics();
			Point adjPos = new Point(e.X, e.Y).ToWorldCoordinateSpace(g, cad.SnapToGrid);
			PalettePart palettePart = DefaultObject as PalettePart;
			palettePart.MoveTo(adjPos.X, adjPos.Y);

			ColorMatrix matrix = new ColorMatrix();
			matrix.Matrix33 = 0.5f; //opacity 0 = completely transparent, 1 = completely opaque

			ImageAttributes attributes = new ImageAttributes();
			attributes.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

			palettePart.Draw(g, cad.DrawingOptions, attributes);
		}

		public override void OnMouseClick(TNTCAD cad, MouseEventArgs e, Keys modifierKeys)
		{
			if (e.Button == System.Windows.Forms.MouseButtons.Left)
			{
				Point adjPos = new Point(e.X, e.Y).ToWorldCoordinateSpace(cad.CreateGraphics(), cad.SnapToGrid);

				cad.UnselectAll();
				TNTPart image = (TNTPart)DefaultObject.Clone();
				image.MoveTo(adjPos.X, adjPos.Y);
				image.Selected = true;
				cad.AddObject(image);
				cad.TriggerOnObjectsSelected();
			}
		}

		public override void OnKeyDown(TNTCAD cad, KeyEventArgs e)
		{
			base.OnKeyDown(cad, e);

			PalettePart palettePart = DefaultObject as PalettePart;

			if (palettePart != null && e.KeyCode == Keys.R)
			{
				palettePart.RotationAngle = (palettePart.RotationAngle + 90) % 360;

				cad.Refresh();

				Graphics g = cad.CreateGraphics();
				palettePart.Draw(g, cad.DrawingOptions);
			}
		}

		public override string[] DefaultObjects()
		{
			return Utilities.GetNameSpaceClasses("TNT.LSD.Objects", "TNT.LSD.Objects.dll", typeof(PalettePart)).ToArray();
		}

		#endregion
	}
}
