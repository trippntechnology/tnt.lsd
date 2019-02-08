using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;

namespace TNT.LSD.Objects
{
	public class Legend : TNTShape
	{
		#region Constructors

		public Legend(Legend obj)
			: base(obj)
		{
		}

		public Legend()
			: base()
		{
		}

		public Legend(Point point)
			: base(new Rectangle(point, new Size(100, 100)), Color.Black)
		{
			base.FillColor = Color.White;
			base.LineWidth = 2;
		}

		#endregion

		public override TNTObject Clone()
		{
			return new Legend(this);
		}

		public override TNTObject MouseOver(Point mousePosition, System.Windows.Forms.Keys modifierKeys)
		{
			return base.MouseOver(mousePosition, modifierKeys);
		}

		public override bool InRectangle(Rectangle rectangle)
		{
			return base.InRectangle(rectangle);
		}

		public override void AlignToGrid()
		{
			base.AlignToGrid();
		}

		public override void MoveTo(int x, int y, bool alignPoints)
		{
			ControlPoints[0].MoveTo(x, y, alignPoints);
		}

		public override void Draw(Graphics graphics, DrawingOptions drawingOptions)
		{
			Font textFont = new Font("Arial", 10);
			int legendHeight = 20;

			List<LegendEntry> legendEntries = new List<LegendEntry>();
			int maxTextWidth = 0;
			int maxTextHeight = 0;
			int minWidth = 100;
			int minHeight = 100;

			var distinctLegendText = (from o in drawingOptions.PartsLayer where o is PalettePart && !(o as PalettePart).ExcludeFromLegend orderby (o as PalettePart).LegendText select (o as PalettePart).LegendText).Distinct();

			foreach (string legendText in distinctLegendText)
			{
				PalettePart palettePart = drawingOptions.PartsLayer.Find(o => { return (o is PalettePart) && (o as PalettePart).LegendText == legendText; }) as PalettePart;
				legendEntries.Add(new LegendEntry(palettePart.LegendImage, palettePart.LegendText));
			}

			#region Add pipe sizes to legend

			var distinctPipe = (from p in drawingOptions.PartsLayer where p is Pipe select p as Pipe).ToList().Distinct(new PipeComparer());
			LateralPipe legendPipe = new LateralPipe(new TNTPart(new Point(0, 8)), new TNTPart(new Point(16, 8)));

			//foreach (string size in distinctPipesSizes)
			foreach (Pipe pipe in distinctPipe)
			{
				legendPipe.PipeSize = pipe.PipeSize;
				legendPipe.LineStyle = pipe.LineStyle;
				Bitmap bm = new Bitmap(16, 16);
				Graphics gp = Graphics.FromImage(bm);
				legendPipe.Draw(gp, drawingOptions);
				legendEntries.Add(new LegendEntry(bm, string.Format("{0} {1}", legendPipe.PipeSize, pipe is LateralPipe ? "Lateral" : "Main")));
			}

			#endregion

			foreach (LegendEntry le in legendEntries)
			{
				maxTextHeight = System.Math.Max(maxTextHeight, le.Height(graphics, textFont) + 2);
				maxTextWidth = System.Math.Max(maxTextWidth, le.Width(graphics, textFont));
			}

			int width = System.Math.Max(minWidth, maxTextWidth);
			int height = System.Math.Max(minHeight, maxTextHeight * (legendEntries.Count + 1) + legendHeight);

			ControlPoints[4].MoveTo(ControlPoints[0].XPos + width, ControlPoints[0].YPos + height, true);

			GraphicsPath path = CreateRoundedRectangle(ControlPoints[0].Position, ControlPoints[4].Position, 20);

			using (Pen pen = new Pen(LineColor))
			using (Brush brush = new SolidBrush(Color.FromArgb(FillOpacity, FillColor)))
			using (Brush textBrush = new SolidBrush(Color.Black))
			using (StringFormat stringFormat = new StringFormat())
			using (Font font = new Font("Arial", 10, FontStyle.Bold))
			{
				stringFormat.Alignment = StringAlignment.Center;
				stringFormat.LineAlignment = StringAlignment.Center;

				pen.Width = LineWidth;
				pen.DashStyle = LineStyle;

				graphics.DrawPath(pen, path);
				graphics.FillPath(brush, path);

				graphics.DrawString("Legend", font, textBrush, new Rectangle(ControlPoints[0].Position, new Size(width, legendHeight)), stringFormat);

				int currentYPos = legendHeight;

				foreach (LegendEntry le in legendEntries)
				{
					le.Draw(graphics, ControlPoints[0].XPos, ControlPoints[0].YPos + currentYPos, textFont, textBrush);
					currentYPos += maxTextHeight;
				}
			}

			base.Draw(graphics, drawingOptions);
		}

		public override void DrawDistances(Graphics graphics)
		{
			//base.DrawDistances(graphics);
		}

		protected GraphicsPath CreateRoundedRectangle(Point topLeft, Point bottomRight, int radius)
		{
			int x = topLeft.X;
			int y = topLeft.Y;
			int xw = bottomRight.X;
			int yh = bottomRight.Y;
			int xwr = xw - radius;
			int yhr = yh - radius;
			int xr = x + radius;
			int yr = y + radius;
			int r2 = radius * 2;
			int xwr2 = xw - r2;
			int yhr2 = yh - r2;

			GraphicsPath p = new GraphicsPath();
			p.StartFigure();

			//Top Left Corner
			p.AddArc(x, y, r2, r2, 180, 90);

			//Top Edge
			p.AddLine(xr, y, xwr, y);

			//Top Right Corner
			p.AddArc(xwr2, y, r2, r2, 270, 90);

			//Right Edge
			p.AddLine(xw, yr, xw, yhr);

			//Bottom Right Corner
			p.AddArc(xwr2, yhr2, r2, r2, 0, 90);

			//Bottom Edge
			p.AddLine(xwr, yh, xr, yh);

			//Bottom Left Corner
			p.AddArc(x, yhr2, r2, r2, 90, 90);

			//Left Edge
			p.AddLine(x, yhr, x, yr);

			p.CloseFigure();
			return p;
		}
	}

	public class LegendEntry
	{
		private const int MARGIN = 3;
		public Image Image { get; set; }
		public string Description { get; set; }

		public LegendEntry(Image image, string description)
		{
			Image = image;
			Description = description;
		}

		public void Draw(Graphics graphics, int xPos, int yPos, Font font, Brush brush)
		{
			int imageXPos = xPos + MARGIN;
			int descXPos = imageXPos + Image.Width + MARGIN;

			graphics.DrawImage(Image, imageXPos, yPos, Image.Width, Image.Height);
			graphics.DrawString(Description, font, brush, descXPos, yPos);
		}

		public int Height(Graphics graphics, Font font)
		{
			return System.Math.Max(Image.Height, graphics.MeasureString(Description, font).ToSize().Height);
		}

		public int Width(Graphics graphics, Font font)
		{
			return MARGIN + Image.Width + MARGIN + graphics.MeasureString(Description, font).ToSize().Width + MARGIN;
		}
	}

	public class PipeComparer : IEqualityComparer<Pipe>
	{
		public bool Equals(Pipe x, Pipe y)
		{
			return x.LineStyle == y.LineStyle && x.PipeSize == y.PipeSize;
		}

		public int GetHashCode(Pipe obj)
		{
			return 0;
			throw new System.NotImplementedException();
		}
	}
}
