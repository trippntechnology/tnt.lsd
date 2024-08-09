using Newtonsoft.Json;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using TNT.LSD.Objects.ControlPoints;
using TNT.LSD.Objects.Interfaces;

namespace TNT.LSD.Objects
{
  public class TNTPolyLine : TNTLine, IShape
  {
    #region Members

    protected Color m_FillColor;

    #endregion

    #region Properties

    [DisplayName("Fill Color")]
    [Description("Indicates the fill color.")]
    [DefaultValue(typeof(Color), "Transparent")]
    [JsonIgnore]
    public Color FillColor
    {
      get { return m_FillColor; }
      set
      {
        m_FillColor = value;
        FillOpacity = FillColor == Color.Transparent || FillColor == Color.FromArgb(0, 255, 255, 255) ? 0 : FillOpacity == 0 ? 255 : FillOpacity;
      }
    }

    [Browsable(false)]
    public int _FillColor
    {
      get { return FillColor.ToArgb(); }
      set { FillColor = Color.FromArgb(value); }
    }

    [DisplayName("Fill Opacity")]
    [Description("Specifies the opacity of the fill color.")]
    [DefaultValue(0)]
    public int FillOpacity { get; set; }

    #endregion

    protected internal override void ControlPointMoved(TNTControlPoint Sender)
    {
      TNTControlPoint otherPoint = null;

      if (ReferenceEquals(Sender, ControlPoints[0]))
      {
        otherPoint = ControlPoints.Last();
      }
      else if (ReferenceEquals(Sender, ControlPoints.Last()))
      {
        otherPoint = ControlPoints[0];
      }

      if (otherPoint != null && otherPoint.Position != Sender.Position)
      {
        otherPoint.MoveTo(Sender.XPos, Sender.YPos, true);
      }

      base.ControlPointMoved(Sender);
    }

    #region Constructors

    public TNTPolyLine()
      : base()
    {
      this.FillColor = Color.Transparent;
      this.FillOpacity = 0;
    }

    public TNTPolyLine(List<Point> points, Color color)
      : this()
    {
      // Add additional point that is the same as the first
      points.Add(new Point(points[0].X, points[0].Y));

      LineColor = color;
      CreateControlPoints(points);
    }

    // Copy constructor
    public TNTPolyLine(TNTPolyLine obj)
      : base(obj)
    {
      this.FillColor = obj.FillColor;
      this.FillOpacity = obj.FillOpacity;
    }

    #endregion

    #region Overrides

    public override void AddControlPoint(Point position)
    {
      // Get the last point
      TNTControlPoint lastPoint = ControlPoints.Last();

      // Add new point
      TNTControlPoint newCP = new TNTControlPoint(this, position);
      newCP.OnMoved = ControlPointMoved;

      ControlPoints.Insert(ControlPoints.Count - 2, new TNTBezierControlPoint(this, newCP, position));
      ControlPoints.Insert(ControlPoints.Count - 2, newCP);
      ControlPoints.Insert(ControlPoints.Count - 2, new TNTBezierControlPoint(this, newCP, position));
    }

    public override void RemoveControlPoint(TNTControlPoint controlPoint, bool keepLine)
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
          else if (!keepLine || ControlPoints.Count > 10)
          {
            if (cp == ControlPoints.First() || cp == ControlPoints.Last())
            {
              // Remove the first point and move the last to the position of the next point
              ControlPoints.RemoveRange(0, 3);
              ControlPoints.Last().MoveTo(ControlPoints.First().XPos, ControlPoints.First().YPos, true);
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

    public override TNTObject Clone()
    {
      return new TNTPolyLine(this);
    }

    public override StateInfo GetStateInfo(Point mousePosition, Keys modifierKeys)
    {
      StateInfo si = new StateInfo();

      TNTObject objUnderMouse = null;

      if (Selected)
      {
        objUnderMouse = GetControlPoint(mousePosition, modifierKeys);
      }

      if (objUnderMouse == null)
      {
        GraphicsPath outline = new GraphicsPath();
        outline.AddBeziers(GetGDIPoints().ToArray());
        outline.Widen(new Pen(Color.Black, 10));

        GraphicsPath enclosed = new GraphicsPath();
        enclosed.AddBeziers(GetGDIPoints().ToArray());

        if (outline.IsVisible(mousePosition))
        {
          // Mouse is over outline (lines)

          if (Selected)
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
        else if (enclosed.IsVisible(mousePosition))
        {
          // Mouse is over enclosed area by lines

          si.ToolTipText = !Selected ? "Click to select" : "Click and drag to move";
          si.Cursor = TNTCursors.SelectObject;
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
        else if (modifierKeys == (Keys.Control | Keys.Shift) && ControlPoints.Count > 10)
        {
          si.ToolTipText = "Click to remove point";
          si.Cursor = TNTCursors.RemovePoint;
        }
        else if (ControlPoints.Count > 10)
        {
          si.ToolTipText = "Ctrl+Shift to remove point";
          si.Cursor = TNTCursors.MovePoint;
        }
        else
        {
          si.Cursor = TNTCursors.SelectObject;
        }
      }

      return si;
    }

    public override TNTObject MouseOver(Point mousePosition, Keys modifierKeys)
    {
      TNTObject isOver = base.MouseOver(mousePosition, modifierKeys);

      if (isOver == null)
      {
        GraphicsPath path = new GraphicsPath();
        path.AddBeziers(GetGDIPoints().ToArray());

        if (path.IsVisible(mousePosition))
        {
          isOver = this;
        }
      }

      return isOver;
    }

    public override void Draw(Graphics graphics, DrawingOptions drawingOptions)
    {
      SolidBrush solidBrush = new SolidBrush(Color.FromArgb(FillOpacity, FillColor));
      GraphicsPath graphicsPath = new GraphicsPath();

      graphicsPath.AddBeziers(GetGDIPoints().ToArray());
      Region fillRegion = new Region(graphicsPath);
      graphics.FillRegion(solidBrush, fillRegion);
      base.Draw(graphics, drawingOptions);
    }

    #endregion

    public double GetArea()
    {
      GraphicsPath gp = new GraphicsPath();
      gp.AddBeziers(GetGDIPoints().ToArray());
      Region region = new Region(gp);

      var rects = region.GetRegionScans(new Matrix());
      double area = 0;

      foreach (var rc in rects)
      {
        area += rc.Width * rc.Height;
      }

      return area / System.Math.Pow(TNTConstants.PIXELS_PER_FOOT, 2);
    }
  }
}
