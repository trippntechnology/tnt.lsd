using Newtonsoft.Json;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using TNT.LSD.Objects.ControlPoints;
using TNT.LSD.Objects.Extensions;
using TNT.LSD.Objects.Interfaces;

namespace TNT.LSD.Objects
{
  public class TNTLine : TNTObject, IMeasurable
  {
    virtual protected internal void ControlPointMoved(TNTControlPoint Sender)
    {
      TNTBezierControlPoint bcp = null;

      // Find the bezier control point that has the sender as endpoint
      foreach (TNTControlPoint cp in ControlPoints)
      {
        bcp = cp as TNTBezierControlPoint;

        // Check if match
        if (bcp != null && ReferenceEquals(bcp.EndPoint, Sender) && bcp.IsLocked)
        {
          // Move bezier control point to end point
          bcp.MoveTo(Sender.XPos, Sender.YPos);
        }
      }
    }

    #region Properties

    [Description("Indicates the line color.")]
    [DisplayName("Line Color")]
    [DefaultValue(typeof(Color), "Black")]
    [JsonIgnore]
    public Color LineColor { get; set; }

    [Browsable(false)]
    public int _LineColor
    {
      get { return LineColor.ToArgb(); }
      set { LineColor = Color.FromArgb(value); }
    }

    [Description("Indicates the line width.")]
    [DisplayName("Line Width")]
    [DefaultValue(1)]
    public int LineWidth { get; set; }

    [Description("Indicates the line style.")]
    [DisplayName("Line Style")]
    [DefaultValue(DashStyle.Solid)]
    public DashStyle LineStyle { get; set; }

    #endregion

    #region Constructors

    public TNTLine()
      : base()
    {
      LineColor = Color.Black;
      LineWidth = 1;
      LineStyle = DashStyle.Solid;
    }

    public TNTLine(List<Point> points, Color color)
      : base()
    {
      LineColor = color;
      LineWidth = 1;
      CreateControlPoints(points);
    }

    // Copy constructor
    public TNTLine(TNTLine obj)
      : base()
    {
      // Create the control points based on the obj
      List<Point> points = (from p in obj.ControlPoints where p.GetType() == typeof(TNTControlPoint) select p.Position).ToList();

      CreateControlPoints(points);

      // Now we need to adjust the location of the bezier points to match the bezier points in the object
      List<TNTBezierControlPoint> myBcps = (from bcp in ControlPoints where bcp is TNTBezierControlPoint select bcp as TNTBezierControlPoint).ToList();
      List<TNTBezierControlPoint> objBcps = (from bcp in obj.ControlPoints where bcp is TNTBezierControlPoint select bcp as TNTBezierControlPoint).ToList();

      for (int index = 0; index < objBcps.Count; index++)
      {
        myBcps[index].Hidden = objBcps[index].Hidden;
        myBcps[index].IsLocked = objBcps[index].IsLocked;
        myBcps[index].XPos = objBcps[index].XPos;
        myBcps[index].YPos = objBcps[index].YPos;
      }

      ControlPoints.ForEach(cp => cp.OnMoved = ControlPointMoved);

      LineColor = obj.LineColor;
      LineWidth = obj.LineWidth;
      LineStyle = obj.LineStyle;
    }

    #endregion

    public override void Draw(Graphics graphics, DrawingOptions drawingOptions)
    {
      Pen pen = new Pen(LineColor, LineWidth);
      pen.DashStyle = LineStyle;
      List<Point> points = GetGDIPoints();

      graphics.DrawBeziers(pen, points.ToArray());

      base.Draw(graphics, drawingOptions);
    }

    public override void DrawDistances(Graphics graphics)
    {
      for (int index = 3; index < ControlPoints.Count; index += 3)
      {
        DrawDistance(graphics, ControlPoints[index - 3], ControlPoints[index], CalculateDistance(ControlPoints[index - 3].Position, ControlPoints[index - 2].Position, ControlPoints[index - 1].Position, ControlPoints[index].Position));
      }
    }

    /// <summary>
    /// Calculates the distance of a bezier line
    /// </summary>
    /// <param name="p1">First point</param>
    /// <param name="p2">First control point</param>
    /// <param name="p3">Second control point</param>
    /// <param name="p4">Second point</param>
    /// <returns>Distance of a bezier lne</returns>
    virtual public double CalculateDistance(Point p1, Point p2, Point p3, Point p4)
    {
      double pixelLength = 0;
      float step = 0.01F;

      PointF a = GetBezierPoint(p1, p2, p3, p4, 0);
      PointF b;

      for (float t = step; t <= 1; t += step)
      {
        b = GetBezierPoint(p1, p2, p3, p4, t);
        pixelLength += a.Distance(b);

        a = b;
      }

      return pixelLength / (double)TNTConstants.PIXELS_PER_FOOT;
    }

    /// <summary>
    /// Returns a <see cref="PointF"/> on a bezier curve for location <paramref name="t"/> where <paramref name="t"/>
    /// is a floating point between 0 and 1.
    /// </summary>
    /// <remarks>Algorithm obtained from http://www.cubic.org/docs/bezier.htm </remarks>
    /// <param name="p1">First point in bezier curve</param>
    /// <param name="p2">First control point in bezier curve</param>
    /// <param name="p3">Second control point in bezier curve</param>
    /// <param name="p4">Second point in bezier curve</param>
    /// <param name="t">Represents the location within the bezier curve where there are infinite locations that are represented
    /// by a floating point number between 0 and 1 inclusively</param>
    /// <returns><see cref="PointF"/> repsenting the point correspoinding the <paramref name="t"/></returns>
    protected PointF GetBezierPoint(Point p1, Point p2, Point p3, Point p4, float t)
    {
      PointF p1p2 = GetBezierPoint(p1, p2, t);
      PointF p2p3 = GetBezierPoint(p2, p3, t);
      PointF p3p4 = GetBezierPoint(p3, p4, t);
      PointF p1p3 = GetBezierPoint(p1p2, p2p3, t);
      PointF p2p4 = GetBezierPoint(p2p3, p3p4, t);
      return GetBezierPoint(p1p3, p2p4, t);
    }

    protected PointF GetBezierPoint(PointF p1, PointF p2, float t)
    {
      PointF point = PointF.Empty;
      point.X = p1.X + (p2.X - p1.X) * t;
      point.Y = p1.Y + (p2.Y - p1.Y) * t;
      return point;
    }

    public override TNTObject MouseOver(Point mousePosition, Keys modifierKeys)
    {
      TNTObject isOver = null;

      try
      {
        Pen p = new Pen(Color.Black, 10);
        GraphicsPath path = new GraphicsPath();
        List<Point> points = GetGDIPoints();

        if (Selected)
        {
          isOver = GetControlPoint(mousePosition, modifierKeys);
        }

        if (isOver != null)
        {
          // Over point belonging to this
          isOver = this;
        }
        else if (points.Count > 1)
        {
          path.AddBeziers(points.ToArray());
          path.Widen(p);

          isOver = path.IsVisible(mousePosition) ? this : null;
        }

      }
      catch
      {
      }

      return isOver;
    }

    public override void InsertControlPoint(Point position)
    {
      int index;

      // Move to the end point of the first bezier curve
      for (index = 3; index < ControlPoints.Count; index += 3)
      {
        Pen p = new Pen(Color.Black, 10);
        GraphicsPath path = new GraphicsPath();

        path.AddBezier(ControlPoints[index - 3].Position, ControlPoints[index - 2].Position, ControlPoints[index - 1].Position, ControlPoints[index].Position);
        path.Widen(p);

        if (path.IsVisible(position))
        {
          // Over this segment
          index -= 1;
          break;
        }
      }

      if (index < ControlPoints.Count)
      {
        // Mouse is currently on a segment of the line where a point could be added

        // Insert new point
        TNTControlPoint newCP = new TNTControlPoint(this, position);
        newCP.OnMoved = ControlPointMoved;

        ControlPoints.Insert(index, new TNTBezierControlPoint(this, newCP, position));
        ControlPoints.Insert(index + 1, newCP);
        ControlPoints.Insert(index + 2, new TNTBezierControlPoint(this, newCP, position));
      }
    }

    public override void AddControlPoint(Point position)
    {
      // Get the last point
      TNTControlPoint lastPoint = ControlPoints.Last();

      // Add new point
      TNTControlPoint newCP = new TNTControlPoint(this, position);
      newCP.OnMoved = ControlPointMoved;

      ControlPoints.Add(new TNTBezierControlPoint(this, lastPoint, lastPoint.Position));
      ControlPoints.Add(new TNTBezierControlPoint(this, newCP, position));
      ControlPoints.Add(newCP);
    }

    public override void RemoveControlPoint(TNTControlPoint controlPoint)
    {
      RemoveControlPoint(controlPoint, true);
    }

    virtual public void RemoveControlPoint(TNTControlPoint controlPoint, bool keepLine)
    {
      foreach (TNTControlPoint cp in ControlPoints)
      {
        if (cp == controlPoint)
        {
          TNTBezierControlPoint bcp = cp as TNTBezierControlPoint;

          if (bcp != null)
          {
            // Set to locked
            bcp.IsLocked = true;
            bcp.MoveTo(bcp.EndPoint.XPos, bcp.EndPoint.YPos, false);
          }
          else if (!keepLine || ControlPoints.Count > 4)
          {
            if (cp == ControlPoints.First())
            {
              // This is the first point so the first three control points need to be removed.
              for (int counter = 0; counter < 3; counter++)
              {
                ControlPoints.RemoveAt(0);
              }
            }
            else if (cp == ControlPoints.Last())
            {
              // Last point so remove last three control points.
              for (int counter = 0; counter < 3; counter++)
              {
                ControlPoints.RemoveAt(ControlPoints.Count - 1);
              }
            }
            else
            {
              ControlPoints.RemoveRange(ControlPoints.IndexOf(cp) - 1, 3);
            }
          }

          break;
        }
      }
    }

    public override void MouseDown(Point mousePosition, Keys modifierKeys)
    {
      base.MouseDown(mousePosition, modifierKeys);

      if (modifierKeys == (Keys.Control | Keys.Shift))
      {
        if (m_SelectedControlPoint != null)
        {
          // We are over an existing control point so delete it
          RemoveControlPoint(m_SelectedControlPoint);
        }
        else
        {
          // We are not over a control point so add a new one.
          InsertControlPoint(mousePosition);
        }
      }
    }

    public override TNTObject Clone()
    {
      return new TNTLine(this);
    }

    protected void CreateControlPoints(List<Point> points)
    {
      // Create control points
      for (int index = 0; index < points.Count; index++)
      {
        TNTControlPoint newCP = new TNTControlPoint(this, points[index].X, points[index].Y);
        newCP.OnMoved = ControlPointMoved;

        if (index != 0)
        {
          // Add bezier point
          ControlPoints.Add(new TNTBezierControlPoint(this, newCP, points[index].X, points[index].Y));
        }

        ControlPoints.Add(newCP);

        if ((index + 1) < points.Count)
        {
          // Another point exists so add bezier points
          ControlPoints.Add(new TNTBezierControlPoint(this, newCP, points[index].X, points[index].Y));
        }
      }
    }

    public override bool InRectangle(Rectangle rectangle)
    {
      bool inRect = true;
      TNTControlPoint cp;

      for (int index = 0; index < ControlPoints.Count && inRect; index++)
      {
        cp = ControlPoints[index] as TNTControlPoint;

        if (cp.GetType() == typeof(TNTControlPoint))
        {
          inRect = rectangle.Contains(cp.Position);
        }
      }

      return inRect;
    }

    /// <summary>
    /// Creates a copy of the object for an undo action
    /// </summary>
    /// <returns>Copy of object for undo action</returns>
    public override TNTObject CreateUndoCopy()
    {
      TNTLine newLine = base.CreateUndoCopy() as TNTLine;
      var cps = newLine.ControlPoints;

      for (int index = 0; index < cps.Count; index++)
      {
        cps[index].OnMoved = ControlPointMoved;
        TNTBezierControlPoint bp = cps[index] as TNTBezierControlPoint;

        if (bp != null)
        {
          if (cps[index + 1] is TNTBezierControlPoint)
          {
            bp.EndPoint = cps[index - 1];
          }
          else
          {
            bp.EndPoint = cps[index + 1];
          }
        }
      }

      newLine.LineColor = LineColor;
      newLine.LineWidth = LineWidth;
      newLine.LineStyle = LineStyle;

      return newLine;
    }

    public override void Assign(TNTObject obj)
    {
      base.Assign(obj);

      TNTLine l = obj as TNTLine;

      if (l != null)
      {
        LineColor = l.LineColor;
        LineWidth = l.LineWidth;
        LineStyle = l.LineStyle;
      }
    }

    public override void ResolveReferences(List<TNTObject> objects)
    {
      base.ResolveReferences(objects);

      List<TNTObject> objList = ControlPoints.ConvertAll<TNTObject>(delegate (TNTControlPoint c) { return (TNTObject)c; });

      // Assign ControlPointMoved to all TNTControl points
      foreach (TNTControlPoint cp in ControlPoints)
      {
        cp.ResolveReferences(objList);
        cp.Parent = this;

        if (cp.GetType() == typeof(TNTControlPoint))
        {
          cp.OnMoved = ControlPointMoved;
        }
      }
    }

    public override StateInfo GetStateInfo(Point mousePosition, Keys modifierKeys)
    {
      StateInfo si = new StateInfo();

      TNTObject objUnderMouse = null;

      Pen p = new Pen(Color.Black, 10);
      GraphicsPath path = new GraphicsPath();
      List<Point> points = GetGDIPoints();

      if (Selected)
      {
        objUnderMouse = GetControlPoint(mousePosition, modifierKeys);
      }

      if (objUnderMouse == null && points.Count > 1)
      {
        path.AddBeziers(points.ToArray());
        path.Widen(p);

        objUnderMouse = path.IsVisible(mousePosition) ? this : null;

        if (objUnderMouse != null)
        {
          // Found this object. Set text and cursor
          if (objUnderMouse.Selected)
          {
            if (modifierKeys == (Keys.Control | Keys.Shift))
            {
              si.ToolTipText = "Click to insert point";
              si.Cursor = TNTCursors.AddPoint;
            }
            else
            {
              si.ToolTipText = "Ctrl+Shift to add point";
              si.Cursor = TNTCursors.SelectObject;
            }
          }
          else
          {
            si.ToolTipText = "Click to select";
            si.Cursor = TNTCursors.SelectObject;
          }
        }
      }
      else
      {
        // Control point was found
        if (modifierKeys == Keys.Shift && objUnderMouse is TNTBezierControlPoint)
        {
          si.ToolTipText = "Click and drag to curve point";
          si.Cursor = TNTCursors.CurvePoint;
        }
        else if (modifierKeys == (Keys.Control | Keys.Shift) && (ControlPoints.Count > 4 || objUnderMouse is TNTBezierControlPoint))
        {
          si.ToolTipText = "Click to remove point";
          si.Cursor = TNTCursors.RemovePoint;
        }
        else if (ControlPoints.Count > 4 || objUnderMouse is TNTBezierControlPoint)
        {
          si.ToolTipText = "Ctrl+Shift to remove point";
          si.Cursor = TNTCursors.MovePoint;
        }
        else
        {
          si.Cursor = TNTCursors.MovePoint;
        }
      }

      return si;
    }

    public override void AlignToGrid()
    {
      foreach (TNTControlPoint cp in ControlPoints.Where(p => !(p is TNTBezierControlPoint)))
      {
        // Find TNTBezierControlPoints with the same location and align
        ControlPoints.Where(p => p is TNTBezierControlPoint && p.Position == cp.Position).ToList().ForEach(p => p.AlignToGrid());
        cp.AlignToGrid();
      }
    }

    public double GetLength()
    {
      double length = 0;

      for (int index = 3; index < ControlPoints.Count; index += 3)
      {
        length += CalculateDistance(ControlPoints[index - 3].Position, ControlPoints[index - 2].Position, ControlPoints[index - 1].Position, ControlPoints[index].Position);
      }

      return length;
    }
  }
}
