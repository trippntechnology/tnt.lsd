using System.Drawing;
using System.Windows.Forms;
using TNT.LSD.Objects;
using TNT.LSD.Objects.Extensions;

namespace LSDComponents.DrawingModes
{
	public class RectangleMode : DrawingMode
	{
		#region Constants

		private const string START_TEXT = "Click on start location";
		private const string END_TEXT = "Click on end location. Right click to remove last point";

		#endregion

		private TNTRectangle m_Rectangle = null;

		public RectangleMode()
			: base()
		{
			DefaultObject = new TNTRectangle();
			DefaultObjectType = DefaultObject.GetType().ToString();
		}

		public RectangleMode(RectangleMode obj)
			: base(obj)
		{
		}

		#region Overridden methods

		public override DrawingMode Clone()
		{
			return new RectangleMode(this);
		}

		public override void OnMouseMove(TNTCAD cad, MouseEventArgs e, Keys modifierKeys)
		{
			cad.Cursor = TNTCursors.RectangleTool;
			Graphics g = cad.CreateGraphics();

			Point adjPos = new Point(e.X, e.Y).ToWorldCoordinateSpace(g, cad.SnapToGrid);

			cad.Refresh();

			if (m_Rectangle != null)
			{
				if (modifierKeys == Keys.Control)
				{
					// Adjust points to maintain circle
					Point deltaPos = adjPos.Subtract(m_Rectangle.ControlPoints[0].Position);
					adjPos = m_Rectangle.ControlPoints[0].Position.Add(deltaPos.Square());
				}

				m_Rectangle.ControlPoints[4].MoveTo(adjPos.X, adjPos.Y, true);
				m_Rectangle.Draw(g, cad.DrawingOptions);

				cad.Text = END_TEXT;
			}
			else
			{
				cad.Text = START_TEXT;
			}

			DrawPointerPoint(g, adjPos, cad.DrawingOptions);
		}

		public override void OnMouseClick(TNTCAD cad, MouseEventArgs e, Keys modifierKeys)
		{
			if (e.Button == MouseButtons.Left)
			{
				if (m_Rectangle == null)
				{
					TNTRectangle defaults = DefaultObject as TNTRectangle;
					Point adjPos = new Point(e.X, e.Y).ToWorldCoordinateSpace(cad.CreateGraphics(), cad.SnapToGrid);

					m_Rectangle = new TNTRectangle(new Rectangle(new Point(adjPos.X, adjPos.Y), new Size(1, 1)), Color.Black);

					if (defaults != null)
					{
						m_Rectangle.LineColor = defaults.LineColor;
						m_Rectangle.LineStyle = defaults.LineStyle;
						m_Rectangle.LineWidth = defaults.LineWidth;
						m_Rectangle.FillColor = defaults.FillColor;
						m_Rectangle.FillOpacity = defaults.FillOpacity;
					}

					m_Rectangle.Selected = true;
				}
				else if (m_Rectangle != null)
				{
					m_Rectangle.Selected = false;

					cad.AddObject(m_Rectangle);
					m_Rectangle = null;
				}
			}
			else if (e.Button == MouseButtons.Right)
			{
				if (m_Rectangle != null)
				{
					m_Rectangle = null;
				}
			}

			if (m_Rectangle != null)
			{
				cad.Text = END_TEXT;
				UndoEnabled = false;
			}
			else
			{
				cad.Text = START_TEXT;
				UndoEnabled = true;
			}

			cad.Refresh();
		}

		public override void Reset(TNTCAD cad)
		{
			base.Reset(cad);
			m_Rectangle = null;
		}

		#endregion
	}
}
