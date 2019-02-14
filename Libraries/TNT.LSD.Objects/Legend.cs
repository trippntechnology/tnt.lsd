using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;

namespace TNT.LSD.Objects
{
	public class Legend : TNTShape
	{
		const int MAX_TEXT_WIDTH = 0;
		const int MAX_TEXT_HEIGHT = 0;
		const int MIN_WIDTH = 100;
		const int MIN_HEIGHT = 100;

		#region Constructors

		public Legend(Legend obj)
			: base(obj)
		{
		}

		/// <summary>
		/// Needed for deserialization
		/// </summary>
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

			var distinctLegendText = (from o in drawingOptions.PartsLayer where o is PalettePart && !(o as PalettePart).ExcludeFromLegend orderby (o as PalettePart).LegendText select (o as PalettePart).LegendText).Distinct();

			foreach (string legendText in distinctLegendText)
			{
				PalettePart palettePart = drawingOptions.PartsLayer.Find(o => { return (o is PalettePart) && (o as PalettePart).LegendText == legendText; }) as PalettePart;
				legendEntries.Add(new LegendEntry(palettePart.LegendImage, palettePart.LegendText));
			}

			#region Add pipe sizes to legend

			var distinctPipe = (from p in drawingOptions.PartsLayer where p is Pipe select p as Pipe).ToList().Distinct(new PipeComparer());

			foreach (Pipe pipe in distinctPipe)
			{
				var legendPipe = new LateralPipe(new TNTPart(new Point(-20, 8)), new TNTPart(new Point(36, 8)));
				legendPipe.PipeSize = pipe.PipeSize;
				legendPipe.LineStyle = pipe.LineStyle;
				legendEntries.Add(new PipeLegendEntry(legendPipe, string.Format("{0} {1}", legendPipe.PipeSize, pipe is LateralPipe ? "Lateral" : "Main")));
			}

			#endregion

			int textWidth = MAX_TEXT_WIDTH;
			int textHeight = MAX_TEXT_HEIGHT;

			foreach (LegendEntry le in legendEntries)
			{
				textHeight = System.Math.Max(textHeight, le.Height(graphics, textFont) + 2);
				textWidth = System.Math.Max(textWidth, le.Width(graphics, textFont));
			}

			int width = System.Math.Max(MIN_WIDTH, textWidth);
			int height = System.Math.Max(MIN_HEIGHT, textHeight * (legendEntries.Count + 1) + legendHeight);

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
					currentYPos += textHeight;
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

	public class PipeLegendEntry : LegendEntry
	{
		protected const int PIPE_OFFSET = 8;

		public Pipe Pipe { get; set; }

		public PipeLegendEntry(Pipe pipe, string description)
			: base(null, description)
		{
			this.Pipe = pipe;
		}

		public override void Draw(Graphics graphics, int xPos, int yPos, Font font, Brush brush)
		{
			int pipeXPos = xPos + MARGIN;
			int pipeYpos = yPos + PIPE_OFFSET;
			int descXPos = pipeXPos + 16 + MARGIN;

			this.Pipe.Part1.MoveTo(pipeXPos, pipeYpos);
			this.Pipe.Part2.MoveTo(pipeXPos + 16, pipeYpos);
			this.Pipe.Draw(graphics, new DrawingOptions());
			graphics.DrawString(Description, font, brush, descXPos, yPos);
		}
	}

	public class LegendEntry
	{
		protected const int MARGIN = 3;
		public Image Image { get; set; }
		public string Description { get; set; }

		public LegendEntry(Image image, string description)
		{
			Image = image;
			Description = description;
		}

		virtual public void Draw(Graphics graphics, int xPos, int yPos, Font font, Brush brush)
		{
			int imageXPos = xPos + MARGIN;
			int descXPos = imageXPos + Image.Width + MARGIN;

			graphics.DrawImage(Image, imageXPos, yPos, Image.Width, Image.Height);
			graphics.DrawString(Description, font, brush, descXPos, yPos);
		}

		public int Height(Graphics graphics, Font font)
		{
			return System.Math.Max(16, graphics.MeasureString(Description, font).ToSize().Height);
		}

		public int Width(Graphics graphics, Font font)
		{
			return MARGIN + 16 + MARGIN + graphics.MeasureString(Description, font).ToSize().Width + MARGIN;
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
