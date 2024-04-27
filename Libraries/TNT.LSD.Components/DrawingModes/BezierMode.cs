using System.Drawing;
using TNT.LSD.Objects;
using TNT.LSD.Objects.ControlPoints;
using TNT.LSD.Objects.Extensions;

namespace TNT.LSD.Components.DrawingModes;

public class BezierMode : LineMode
{
  #region Constants

  protected const string END_TEXT = "Click on end location. Right click to remove last point";
  protected const string FIRST_BEZIER = "Move bezier point. Right click to remove last point";
  protected const string SECOND_BEZIER = "Move bezier point. Right click to remove last point. Click to complete curve";

  #endregion

  private TNTControlPoint bcp1 = null;
  private TNTControlPoint bcp2 = null;

  #region Public Methods

  public override void OnMouseMove(TNTCAD cad, System.Windows.Forms.MouseEventArgs e, System.Windows.Forms.Keys modifierKeys)
  {
    cad.Cursor = TNTCursors.CurveTool;
    Point adjPos = new Point(e.X, e.Y).ToWorldCoordinateSpace(cad.CreateGraphics(), cad.SnapToGrid);

    if (bcp1 != null && bcp2 != null)
    {
      // Adjust the bezier points
      bcp1.MoveTo(adjPos.X, adjPos.Y);
      bcp2.MoveTo(adjPos.X, adjPos.Y);
    }
    else if (bcp2 != null)
    {
      // Just move the 2nd point
      bcp2.MoveTo(adjPos.X, adjPos.Y);
    }
    else if (m_ConstructedObject == null)
    {
      cad.Text = START_TEXT;
    }

    DrawLine(cad, adjPos);
  }

  public override void OnMouseClick(TNTCAD cad, System.Windows.Forms.MouseEventArgs e, System.Windows.Forms.Keys modifierKeys)
  {
    Point adjPos = new Point(e.X, e.Y).ToWorldCoordinateSpace(cad.CreateGraphics(), cad.SnapToGrid);

    if (e.Button == System.Windows.Forms.MouseButtons.Left)
    {
      if (m_ConstructedObject == null)
      {
        TNTLine defaults = DefaultObject as TNTLine;

        // Create the first point
        List<Point> points = new List<Point>();
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
        UndoEnabled = false;
      }
      else if (m_ConstructedObject.ControlPoints.Count == 1)
      {
        // Only the first point has been added so far this would add the end point
        m_ConstructedObject.AddControlPoint(adjPos);

        // Set the bezier points so that they can now be positioned
        bcp1 = m_ConstructedObject.ControlPoints[1];
        bcp2 = m_ConstructedObject.ControlPoints[2];
      }
      else if (bcp1 != null)
      {
        // Null the first so that only the second will be positioned
        bcp1 = null;
      }
      else
      {
        // The second bezier point has been moved. Persist the line.
        m_ConstructedObject.Selected = false;
        m_ConstructedObject = null;
        bcp1 = null;
        bcp2 = null;

        UndoEnabled = true;

        cad.Repaint();
      }
    }
    else if (e.Button == System.Windows.Forms.MouseButtons.Right)
    {
      TNTLine line = m_ConstructedObject;

      if (bcp1 == null && bcp2 != null)
      {
        // Occurs when the 2nd point is being moved. Set bcp1 so that it will be moved now and set it's current location
        bcp1 = line.ControlPoints[1];
        bcp1.MoveTo(bcp2.Position.X, bcp2.Position.Y);
      }
      else if (line != null && line.ControlPoints.Count > 1)
      {
        // Remove the last point drawn
        line.RemoveControlPoint(line.ControlPoints.Last(), false);
      }
      else
      {
        // Remove the line completely
        cad.ActiveObjects.Remove(m_ConstructedObject);
        m_ConstructedObject = null;

        UndoEnabled = true;
      }
    }

    if (m_ConstructedObject == null)
    {
      cad.Text = START_TEXT;
    }
    else if (m_ConstructedObject.ControlPoints.Count == 1)
    {
      cad.Text = END_TEXT;
    }
    else if (bcp1 != null && bcp2 != null)
    {
      cad.Text = FIRST_BEZIER;
    }
    else if (bcp1 == null)
    {
      cad.Text = SECOND_BEZIER;
    }

    DrawLine(cad, adjPos);
  }

  #endregion

  #region Private Methods

  protected override void DrawLine(TNTCAD cad, Point currentPos)
  {
    TNTLine line = m_ConstructedObject;

    Graphics g = cad.CreateGraphics();

    cad.Refresh();

    if (line != null && m_ConstructedObject.ControlPoints.Count == 1)
    {
      // Only the first point in line has been drawn. This draws a line to the next possible point
      TNTControlPoint cp = line.ControlPoints.Last();
      TNTLine defaults = DefaultObject as TNTLine;
      Pen pen = new Pen(Color.Black);

      if (defaults != null)
      {
        pen.Width = defaults.LineWidth;
        pen.DashStyle = defaults.LineStyle;
        pen.Color = defaults.LineColor;
      }

      g.DrawLine(pen, cp.Position, currentPos);
      cp.DrawDistance(g, cp, new TNTControlPoint(null, currentPos));
      DrawPointerPoint(g, currentPos, cad.DrawingOptions);
    }
    else if (line != null)
    {
      line.Draw(g, cad.DrawingOptions);
    }
    else
    {
      DrawPointerPoint(g, currentPos, cad.DrawingOptions);
    }
  }

  #endregion
}
