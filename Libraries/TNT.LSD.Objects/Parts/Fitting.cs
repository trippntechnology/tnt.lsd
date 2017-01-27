using System.Drawing;
using System.Drawing.Drawing2D;

namespace TNT.LSD.Objects
{
	public class Fitting : TNTPart
	{
		private const int FOOTPRINT = 8;
		private const int SIZE = 5;

		#region Static

		private static string[] SLIP_CAPS = {"FI050SCAP", "FI075SCAP", "FI100SCAP", "FI125SCAP", "FI150SCAP", "FI200SCAP" };

		#endregion

		public override bool CanClone { get { return false; } }

		public Fitting()
			: base()
		{
			Color = Color.Black;
		}

		public Fitting(Point position)
			: base(position)
		{
			Color = Color.Black;
		}

		public Fitting(Fitting obj)
			: base(obj)
		{
			Color = Color.Black;
		}

		#region Overrides

		public override void Draw(Graphics graphics, DrawingOptions drawingOptions)
		{
			base.Draw(graphics, drawingOptions);

			using (Brush b = new SolidBrush(Color.FromArgb(Selected ? 100 : 255, Color)))
			{
				graphics.TranslateTransform(ControlPoints[0].XPos, ControlPoints[0].YPos);
				graphics.FillEllipse(b, -TNTConstants.FITTING_SIZE / 2, -TNTConstants.FITTING_SIZE / 2, TNTConstants.FITTING_SIZE, TNTConstants.FITTING_SIZE);
				graphics.TranslateTransform(-ControlPoints[0].XPos, -ControlPoints[0].YPos);
			}
		}

		public override TNTObject MouseOver(Point mousePosition, System.Windows.Forms.Keys modifierKeys)
		{
			TNTObject isOver = null;

			GraphicsPath path = new GraphicsPath();
			Rectangle rect = new Rectangle(ControlPoints[0].XPos - FOOTPRINT / 2, ControlPoints[0].YPos - FOOTPRINT / 2, FOOTPRINT, FOOTPRINT);

			path.AddRectangle(rect);

			Region region = new Region(path);
			isOver = region.IsVisible(mousePosition) ? this : null;

			return isOver;
		}

		public override void SetPartQuantity(System.Collections.Generic.Dictionary<string, TNT.LSD.Inventory.Part> parts)
		{
			base.SetPartQuantity(parts);

			if (m_Pipes.Count != 1)
			{
				return;
			}

			int maxPipeSizeIndex = GetMaxPipeSizeIndex();

			parts[SLIP_CAPS[maxPipeSizeIndex]].Quantity += 1;
		}

		public override double SizePipe(Pipe upstreamPipe)
		{
			RequiredFlow = 0;
			return base.SizePipe(upstreamPipe);
		}

		public override void MouseDown(Point mousePosition, System.Windows.Forms.Keys modifierKeys)
		{
			// This was neccessary to keep other objects selected when click on an already selected fitting to
			// begin moving the selected objects.
			m_SelectedControlPoint = null; 
		}

		#endregion
	}
}
