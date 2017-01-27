using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using TNT.LSD.Objects.Interfaces;

namespace TNT.LSD.Objects
{
	public class TNTCircle : TNTShape, IShape, IMeasurable
	{
		#region Constructors

		public TNTCircle()
			: base()
		{
		}

		public TNTCircle(Rectangle rect, Color fillColor)
			: base(rect, fillColor)
		{
		}

		// Copy Constructor
		public TNTCircle(TNTCircle obj)
			: base(obj)
		{
		}

		#endregion

		public override void Draw(Graphics graphics, DrawingOptions drawingOptions)
		{
			Rectangle rect = GetRectangle();
			SolidBrush sBrush = new SolidBrush(Color.FromArgb(FillOpacity, FillColor));

			graphics.FillEllipse(sBrush, rect);

			Pen p = new Pen(LineColor);
			p.Width = LineWidth;
			p.DashStyle = LineStyle;
			graphics.DrawEllipse(p, rect);

			base.Draw(graphics, drawingOptions);
		}

		public override TNTObject MouseOver(Point mousePosition, Keys modifierKeys)
		{
			TNTObject isOver = null;
			GraphicsPath path = new GraphicsPath();
			Rectangle rect = GetRectangle();

			path.AddEllipse(rect);

			Region region = new Region(path);
			isOver = region.IsVisible(mousePosition) ? this : null;

			if (isOver == null && Selected)
			{
				isOver = GetControlPoint(mousePosition, modifierKeys);
			}

			return isOver;
		}

		public override TNTObject Clone()
		{
			return new TNTCircle(this);
		}

		public double GetArea()
		{
			Rectangle rect = GetRectangle();
			return System.Math.PI * rect.Width / TNTConstants.PIXELS_PER_FOOT / 2 * rect.Height / TNTConstants.PIXELS_PER_FOOT / 2;
		}

		public double GetLength()
		{
			Rectangle rect = GetRectangle();
			float a = rect.Width / 2 / TNTConstants.PIXELS_PER_FOOT;
			float b = rect.Height / 2 / TNTConstants.PIXELS_PER_FOOT;

			return 2 * System.Math.PI * System.Math.Sqrt((System.Math.Pow(a, 2) + System.Math.Pow(b, 2)) / 2);
		}
	}
}
