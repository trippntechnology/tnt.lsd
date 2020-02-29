using System.Drawing;
using System.Drawing.Drawing2D;
using TNT.LSD.Inventory;
using TNT.LSD.Settings;

namespace TNT.LSD.Objects
{
	/// <summary>
	/// Represents a fitting
	/// </summary>
	public class Fitting : TNTPart
	{
		private const int FOOTPRINT = 8;

		/// <summary>
		/// Indicates if this <see cref="TNTPart"/> can be cloned. Aways returns false
		/// </summary>
		public override bool CanClone { get { return false; } }

		/// <summary>
		/// Default constructor
		/// </summary>
		public Fitting()
			: base()
		{
			Color = Color.Black;
		}

		/// <summary>
		/// Constructor that positions the fitting at <paramref name="position"/>
		/// </summary>
		public Fitting(Point position)
			: base(position)
		{
			Color = Color.Black;
		}

		/// <summary>
		/// Copy constructor
		/// </summary>
		/// <param name="obj"></param>
		public Fitting(Fitting obj)
			: base(obj)
		{
			Color = Color.Black;
		}

		#region Overrides

		/// <summary>
		/// Draws a circle that represents the fitting
		/// </summary>
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

		/// <summary>
		/// Indicates if the maouse is over the fitting
		/// </summary>
		/// <returns>True if over, false otherwise</returns>
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

		/// <summary>
		/// Adds a slip cap or plug if this fitting only has one pipe connection.
		/// </summary>
		public override void SetPartQuantity(CodedParts parts, SystemType systemType)
		{
			base.SetPartQuantity(parts, systemType);

			if (Pipes.Count != 1)
			{
				return;
			}

			var pipeSize = PartSize.GetSize(Pipes[0].PipeSizeIndex);
			if (systemType == SystemType.PVC)
			{
				parts.Add($"FI{pipeSize.Code}SCAP", 1);
			}
			else
			{
				var hcCode = pipeSize == PartSize.SIZE_125 ? PartSize.SIZE_150.Code : pipeSize.Code;
				parts.Add($"PF{pipeSize.Code}PLUG", 1);
				parts.AddHoseClamp(hcCode, 1);
			}
		}

		/// <summary>
		/// Sizes the pipe
		/// </summary>
		/// <returns>The required flow at this point</returns>
		public override double SizePipe(Pipe upstreamPipe)
		{
			RequiredFlow = 0;
			return base.SizePipe(upstreamPipe);
		}

		/// <summary>
		/// Sets the selected control point to null
		/// </summary>
		public override void MouseDown(Point mousePosition, System.Windows.Forms.Keys modifierKeys)
		{
			// This was neccessary to keep other objects selected when click on an already selected fitting to
			// begin moving the selected objects.
			m_SelectedControlPoint = null;
		}

		#endregion
	}
}
