using System.Drawing;
using System.Windows.Forms;
using TNT.LSD.Objects;
using TNT.LSD.Objects.Extensions;

namespace LSDComponents.DrawingModes
{
	public class CircleMode : DrawingMode
	{
		#region Constants

		private const string START_TEXT = "Click on start location";
		private const string END_TEXT = "Click on end location. Right click to remove last point. Try CTRL/SHIFT combinations.";

		#endregion

		private TNTCircle m_Circle = null;
		private Point m_InitialPoint = Point.Empty;

		public CircleMode()
			: base()
		{
			DefaultObject = new TNTCircle();
			DefaultObjectType = DefaultObject.GetType().ToString();
		}

		public CircleMode(CircleMode obj)
			:base(obj)
		{
		}

		#region Overridden methods

		public override DrawingMode Clone()
		{
			return new CircleMode(this);
		}

		public override void OnMouseMove(TNTCAD cad, MouseEventArgs e, Keys modifierKeys)
		{
			cad.Cursor = TNTCursors.CircleTool;
			Graphics g = cad.CreateGraphics();
			Point mousePos = new Point(e.X, e.Y).ToWorldCoordinateSpace(g, cad.SnapToGrid);

			cad.Refresh();

			if (m_Circle != null)
			{
				if ((modifierKeys & Keys.Control) == Keys.Control)
				{
					// Adjust points to maintain circle
					Point deltaPos = mousePos.Subtract(m_InitialPoint);// m_Circle.ControlPoints[0].Position);
					mousePos = m_InitialPoint.Add(deltaPos.Square());
				}

				if ((modifierKeys & Keys.Shift) == Keys.Shift)
				{
					// Adjust to create circle around InitialPoint
					Point deltaPos = mousePos.Subtract(m_InitialPoint);
					Point newPoint = m_InitialPoint.Subtract(deltaPos);
					m_Circle.ControlPoints[0].MoveTo(newPoint.X, newPoint.Y, true);
				}
				else
				{
					m_Circle.ControlPoints[0].MoveTo(m_InitialPoint.X, m_InitialPoint.Y, true);
				}

				m_Circle.ControlPoints[4].MoveTo(mousePos.X, mousePos.Y, true);
				m_Circle.Draw(g, cad.DrawingOptions);

				cad.Text = END_TEXT;
			}
			else
			{
				cad.Text = START_TEXT;
			}

			DrawPointerPoint(g, mousePos, cad.DrawingOptions);
		}

		public override void OnMouseClick(TNTCAD cad, MouseEventArgs e, Keys modifierKeys)
		{
			if (e.Button == MouseButtons.Left)
			{
				if (m_Circle == null)
				{
					TNTCircle defaults = DefaultObject as TNTCircle;
					m_InitialPoint = new Point(e.X, e.Y).ToWorldCoordinateSpace(cad.CreateGraphics(), cad.SnapToGrid);

					m_Circle = new TNTCircle(new Rectangle(new Point(m_InitialPoint.X, m_InitialPoint.Y), new Size(1, 1)), Color.Black);

					if (defaults != null)
					{
						m_Circle.LineColor = defaults.LineColor;
						m_Circle.LineStyle = defaults.LineStyle;
						m_Circle.LineWidth = defaults.LineWidth;
						m_Circle.FillColor = defaults.FillColor;
						m_Circle.FillOpacity = defaults.FillOpacity;
					}

					m_Circle.Selected = true;
				}
				else if (m_Circle != null)
				{
					m_Circle.Selected = false;

					cad.AddObject(m_Circle);
					m_Circle = null;
				}
			}
			else if (e.Button == MouseButtons.Right)
			{
				if (m_Circle != null)
				{
					m_Circle = null;
				}
			}

			if (m_Circle != null)
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
			m_Circle = null;
		}

		#endregion
	}
}
