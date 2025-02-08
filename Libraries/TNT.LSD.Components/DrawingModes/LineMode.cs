using System.Drawing;
using System.Windows.Forms;
using TNT.LSD.Objects;
using TNT.LSD.Objects.ControlPoints;
using TNT.LSD.Objects.Extensions;
using TNT.Math;

namespace TNT.LSD.Components.DrawingModes;

public class LineMode : DrawingMode
{
  #region Constants

  protected const string START_TEXT = "Click on start location";
  protected const string ADD_POINT_TEXT = "Click next location. Right click to remove last point. Double click to complete line";

  #endregion

  protected TNTLine m_ConstructedObject = null;

  public LineMode()
    : base()
  {
    DefaultObject = new TNTLine();
    DefaultObjectType = DefaultObject.GetType().ToString();
  }

  public LineMode(LineMode obj)
    : base(obj)
  {
  }

  #region Public Methods

  public override DrawingMode Clone()
  {
    return new LineMode(this);
  }

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

    cad.Cursor = TNTCursors.LineTool;
    Point adjPos = new Point(e.X, e.Y).ToWorldCoordinateSpace(cad.CreateGraphics(), cad.SnapToGrid);

    if (m_ConstructedObject != null && m_ConstructedObject.ControlPoints.Count > 3 && modifierKeys == Keys.Control)
    {
      adjPos = AdjustToNearestAngleModulus(m_ConstructedObject.ControlPoints[m_ConstructedObject.ControlPoints.Count - 4].Position, adjPos, TNTConstants.ANGLE_MODULUS, cad.SnapToGrid);
    }

    DrawLine(cad, adjPos);
  }

  public override void OnMouseClick(TNTCAD cad, MouseEventArgs e, Keys modifierKeys)
  {
    Point adjPos = new Point(e.X, e.Y).ToWorldCoordinateSpace(cad.CreateGraphics(), cad.SnapToGrid);

    if (m_ConstructedObject != null && m_ConstructedObject.ControlPoints.Count > 3 && modifierKeys == Keys.Control)
    {
      adjPos = AdjustToNearestAngleModulus(m_ConstructedObject.ControlPoints[m_ConstructedObject.ControlPoints.Count - 4].Position, adjPos, TNTConstants.ANGLE_MODULUS, cad.SnapToGrid);
    }

    if (e.Button == MouseButtons.Left)
    {
      if (m_ConstructedObject == null)
      {
        TNTLine defaults = DefaultObject as TNTLine;

        List<Point> points = new List<Point>();
        points.Add(adjPos);
        points.Add(adjPos);
        m_ConstructedObject = new TNTLine(points, Color.Black);

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
      TNTLine line = m_ConstructedObject;

      if (line != null && line.ControlPoints.Count > 4)
      {
        // Remove the last point drawn
        line.RemoveControlPoint(line.ControlPoints.Last(), false);
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
    if (e.Button == MouseButtons.Left && m_ConstructedObject != null && m_ConstructedObject.ControlPoints.Count > 4)
    {
      // Since a OnMouseClick preceeds this event and adds an additional point, that point can be removed.
      m_ConstructedObject.RemoveControlPoint(m_ConstructedObject.ControlPoints.Last());

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

  public override void Reset(TNTCAD cad)
  {
    base.Reset(cad);

    if (m_ConstructedObject != null)
    {
      cad.ActiveObjects.Remove(m_ConstructedObject);
      m_ConstructedObject = null;
    }
  }

  #endregion

  #region Protected Methods

  protected virtual void DrawLine(TNTCAD cad, Point currentPos)
  {
    TNTLine line = m_ConstructedObject;

    Graphics g = cad.CreateGraphics();

    if (line != null)
    {
      TNTControlPoint cp = line.ControlPoints.Last();
      cp.MoveTo(currentPos.X, currentPos.Y, true);
    }

    cad.Refresh();

    DrawPointerPoint(g, currentPos, cad.DrawingOptions);
  }

  /// <summary>
  /// Adjusts current point to the nearest angleModulus
  /// </summary>
  /// <param name="previous">Previous point</param>
  /// <param name="current">Current point</param>
  /// <param name="angleModulus">Angle to adjust to</param>
  /// <param name="snapToGrid">Indicates if the new point should be snapped to the grid</param>
  /// <returns>New point adjusted to the nearest angle modulus</returns>
  protected virtual Point AdjustToNearestAngleModulus(Point previous, Point current, double angleModulus, bool snapToGrid)
  {
    Point adjPoint = current;

    if (previous != null)
    {
      Vector currentVector = new Vector(previous, current);
      Angle currentAngle = currentVector.Angle(new Vector(0, -1));

      double newAngle = System.Math.Sign(currentVector.X) == -1 ? 360 - currentAngle.InDegrees : currentAngle.InDegrees;
      newAngle = ((int)(newAngle / angleModulus) + (newAngle % angleModulus > angleModulus / 2 ? 1 : 0)) * angleModulus;
      Vector newVector = new Vector(new Angle(newAngle, true), 1);
      PointF pf = previous + newVector.Unit * currentVector.Magnitude;
      adjPoint = new Point((int)pf.X, (int)pf.Y).SnapToGrid(snapToGrid);
    }

    return adjPoint;
  }

  #endregion
}
