using System.Drawing;
using System.Drawing.Drawing2D;
using TNT.LSD.Objects.Interfaces;

namespace TNT.LSD.Objects
{
	public class TNTRectangle : TNTShape, IShape, IMeasurable
	{
		#region Constructors

		public TNTRectangle()
			: base()
		{
		}

		public TNTRectangle(TNTRectangle obj)
			: base(obj)
		{
		}

		public TNTRectangle(Rectangle rect, Color fillColor)
			: base(rect, fillColor)
		{
		}

		#endregion

		public override TNTObject Clone()
		{
			return new TNTRectangle(this);
		}

		public override void Draw(Graphics graphics, DrawingOptions drawingOptions)
		{
			Rectangle rect = GetRectangle();
			int dx = GetMidPoint(rect.Left, rect.Right);
			int dy = GetMidPoint(rect.Top, rect.Bottom);

			//graphics.RotateTransform(RotationAngle);
			//graphics.TranslateTransform(dx, dy);

			// Draw the inner rectangle
			graphics.FillRectangle(new SolidBrush(Color.FromArgb(FillOpacity, FillColor)), rect);

			// Draw the outer rectangle
			Pen p = new Pen(LineColor);
			p.Width = LineWidth;
			p.DashStyle = LineStyle;
			graphics.DrawRectangle(p, rect);

			//graphics.TranslateTransform(-dx, -dy);
			//graphics.RotateTransform(-RotationAngle);

			base.Draw(graphics, drawingOptions);
		}

		public double GetArea()
		{
			GraphicsPath gp = new GraphicsPath();
			gp.AddPolygon(GetGDIPoints().ToArray());
			Region region = new Region(gp);

			var rects = region.GetRegionScans(new Matrix());
			double area = 0;

			foreach (var rc in rects)
			{
				area += rc.Width * rc.Height;
			}

			return area / System.Math.Pow(TNTConstants.PIXELS_PER_FOOT, 2);
		}

		public double GetLength()
		{
			Rectangle rect = GetRectangle();
			return 2 * (rect.Width / TNTConstants.PIXELS_PER_FOOT + rect.Height / TNTConstants.PIXELS_PER_FOOT);
		}
	}
}
