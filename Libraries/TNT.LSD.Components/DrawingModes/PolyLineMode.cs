using System.Drawing;
using System.Windows.Forms;
using TNT.LSD.Objects;
using TNT.LSD.Objects.ControlPoints;
using TNT.LSD.Objects.Extensions;

namespace TNT.LSD.Components.DrawingModes;

public class PolyLineMode : LineMode
{
  public PolyLineMode()
    : base()
  {
    DefaultObject = new TNTPolyLine();
    DefaultObjectType = DefaultObject.GetType().ToString();
  }

  #region Overridden Methods

  public override void OnMouseMove(TNTCAD cad, MouseEventArgs e, Keys modifierKeys)
  {
    if (m_ConstructedObject == null)
    {
      cad.Text = START_TEXT;
    }
    else
    {
      cad.Text = ADD_POINT_TEXT;
    }

    cad.Cursor = TNTCursors.PolyLineTool;
    Point adjPos = new Point(e.X, e.Y).ToWorldCoordinateSpace(cad.CreateGraphics(), cad.SnapToGrid);

    if (m_ConstructedObject != null && m_ConstructedObject.ControlPoints.Count > 6 && modifierKeys == Keys.Control)
    {
      adjPos = AdjustToNearestAngleModulus(m_ConstructedObject.ControlPoints[m_ConstructedObject.ControlPoints.Count - 7].Position, adjPos, TNTConstants.ANGLE_MODULUS, cad.SnapToGrid);
    }

    DrawLine(cad, adjPos);
  }

  public override void OnMouseClick(TNTCAD cad, MouseEventArgs e, Keys modifierKeys)
  {
    Point adjPos = new Point(e.X, e.Y).ToWorldCoordinateSpace(cad.CreateGraphics(), cad.SnapToGrid);

    if (m_ConstructedObject != null && m_ConstructedObject.ControlPoints.Count > 6 && modifierKeys == Keys.Control)
    {
      adjPos = AdjustToNearestAngleModulus(m_ConstructedObject.ControlPoints[m_ConstructedObject.ControlPoints.Count - 7].Position, adjPos, TNTConstants.ANGLE_MODULUS, cad.SnapToGrid);
    }

    if (e.Button == MouseButtons.Left)
    {
      if (m_ConstructedObject == null)
      {
        TNTPolyLine defaults = DefaultObject as TNTPolyLine;

        List<Point> points = new List<Point>();
        points.Add(adjPos);
        points.Add(adjPos);
        m_ConstructedObject = new TNTPolyLine(points, Color.Black);

        if (defaults != null)
        {
          m_ConstructedObject.LineColor = defaults.LineColor;
          m_ConstructedObject.LineStyle = defaults.LineStyle;
          m_ConstructedObject.LineWidth = defaults.LineWidth;
        }

        m_ConstructedObject.Selected = true;
        cad.AddObject(m_ConstructedObject);

        cad.Text = ADD_POINT_TEXT;

        UndoEnabled = false;
      }
      else
      {
        m_ConstructedObject.AddControlPoint(adjPos);
      }
    }
    else if (e.Button == MouseButtons.Right)
    {
      TNTPolyLine line = m_ConstructedObject as TNTPolyLine;

      if (line != null && line.ControlPoints.Count > 7)
      {
        // Remove the second to the last point
        line.RemoveControlPoint(line.ControlPoints[line.ControlPoints.Count - 4], false);
      }
      else
      {
        // Remove the line completely
        cad.ActiveObjects.Remove(m_ConstructedObject);
        m_ConstructedObject = null;

        cad.Text = START_TEXT;

        UndoEnabled = true;
      }
    }

    DrawLine(cad, adjPos);
  }

  public override void OnMouseDoubleClick(TNTCAD cad, MouseEventArgs e)
  {
    if (e.Button == MouseButtons.Left && m_ConstructedObject != null && m_ConstructedObject.ControlPoints.Count > 7)
    {
      // Since a OnMouseClick preceeds this event and addes an additional point, that point can be removed.
      m_ConstructedObject.RemoveControlPoint(m_ConstructedObject.ControlPoints[m_ConstructedObject.ControlPoints.Count - 4]);

      m_ConstructedObject.Selected = false;
      m_ConstructedObject = null;

      cad.Text = START_TEXT;

      UndoEnabled = true;

      cad.Repaint();
    }
    else if (e.Button == MouseButtons.Left)
    {
      m_ConstructedObject = null;

      cad.Text = START_TEXT;
      cad.UnselectAll();
    }
  }

  #endregion

  #region Protected Methods

  protected override void DrawLine(TNTCAD cad, Point currentPos)
  {
    TNTPolyLine line = m_ConstructedObject as TNTPolyLine;

    Graphics g = cad.CreateGraphics();

    if (line != null)
    {
      // Get the second to last point
      TNTControlPoint cp = line.ControlPoints[line.ControlPoints.Count - 4];
      cp.MoveTo(currentPos.X, currentPos.Y, true);
    }

    cad.Refresh();

    DrawPointerPoint(g, currentPos, cad.DrawingOptions);
  }

  #endregion
}
