using Newtonsoft.Json;
using System.Drawing.Drawing2D;
using TNT.LSD.Objects.Extensions;

namespace TNT.LSD.Objects.ControlPoints
{
  /// <summary>
  /// Method signature for a moved event
  /// </summary>
  /// <param name="sender"></param>
  public delegate void MovedEvent(TNTControlPoint sender);

  /// <summary>
  /// Represents a control point
  /// </summary>
  public class TNTControlPoint : TNTObject
  {
    /// <summary>
    /// Position member
    /// </summary>
    protected Point m_Position;

    /// <summary>
    /// Image member
    /// </summary>
    protected Image m_Image = null;

    #region Properties

    /// <summary>
    /// OnMoved event
    /// </summary>
    [JsonIgnore]
    public MovedEvent OnMoved { get; set; }

    /// <summary>
    /// Parent
    /// </summary>
    [JsonIgnore]
    public TNTObject Parent { get; set; }

    /// <summary>
    /// X position 
    /// </summary>
    public int XPos { get { return m_Position.X; } set { m_Position.X = value; } }

    /// <summary>
    /// Y position
    /// </summary>
    public int YPos { get { return m_Position.Y; } set { m_Position.Y = value; } }

    /// <summary>
    /// Position of point
    /// </summary>
    [JsonIgnore]
    public Point Position { get { return m_Position; } set { m_Position = value; } }

    /// <summary>
    /// Specifies whether point is hidden
    /// </summary>
    public bool Hidden { get; set; }

    /// <summary>
    /// Indicates the point is selected
    /// </summary>
    public override bool Selected { get { return Parent != null ? Parent.Selected : false; } }

    #endregion

    #region Constructors

    /// <summary>
    /// Default constructor
    /// </summary>
    public TNTControlPoint()
    {
    }

    /// <summary>
    /// Initializes control point
    /// </summary>
    /// <param name="parent">Parent</param>
    /// <param name="xOffset">X position</param>
    /// <param name="yOffset">Y position</param>
    public TNTControlPoint(TNTObject parent, int xOffset, int yOffset)
    {
      Parent = parent;
      m_Position.X = xOffset;
      m_Position.Y = yOffset;
      Hidden = false;
    }

    /// <summary>
    /// Initializes control point
    /// </summary>
    /// <param name="parent">Parent</param>
    /// <param name="xOffset">X position</param>
    /// <param name="yOffset">Y position</param>
    /// <param name="hidden">Indicates whether the point is hidden</param>
    public TNTControlPoint(TNTObject parent, int xOffset, int yOffset, bool hidden)
    {
      Parent = parent;
      m_Position.X = xOffset;
      m_Position.Y = yOffset;
      Hidden = hidden;
    }

    /// <summary>
    /// Initiallizes control point
    /// </summary>
    /// <param name="parent">Parent</param>
    /// <param name="position">Position</param>
    public TNTControlPoint(TNTObject parent, Point position)
    {
      Parent = parent;
      m_Position = position;
      Hidden = false;
    }

    /// <summary>
    /// Initializes control point
    /// </summary>
    /// <param name="parent">Parent</param>
    /// <param name="position">Position</param>
    /// <param name="hidden">Indicates whether the point is hidden</param>
    public TNTControlPoint(TNTObject parent, Point position, bool hidden)
    {
      Parent = parent;
      m_Position = position;
      Hidden = hidden;
    }

    /// <summary>
    /// Copy constructor
    /// </summary>
    /// <param name="parent">Parent</param>
    /// <param name="obj">Object to copy</param>
    public TNTControlPoint(TNTObject parent, TNTControlPoint obj)
      : base(obj)
    {
      Parent = parent;
      Position = obj.Position;
      Hidden = obj.Hidden;
    }

    /// <summary>
    /// Returns the image that represents the control point
    /// </summary>
    virtual protected Image Image
    {
      get
      {
        if (m_Image == null)
        {
          Stream s = this.GetType().Assembly.GetManifestResourceStream("TNT.LSD.Objects.Images.CornerControlPoint.png");
          m_Image = new Bitmap(s);
        }

        return m_Image;
      }
    }

    #endregion

    /// <summary>
    /// Draws the control point
    /// </summary>
    /// <param name="graphics">Graphics</param>
    /// <param name="drawingOptions">Drawing options</param>
    public override void Draw(Graphics graphics, DrawingOptions drawingOptions)
    {
      if (!Hidden)
      {
        graphics.DrawImage(this.Image, m_Position.X - (this.Image.Width / 2), m_Position.Y - (this.Image.Height / 2));
      }
    }

    /// <summary>
    /// Indicates the mouse is over the control point
    /// </summary>
    /// <param name="mousePosition">Mouse position</param>
    /// <param name="modifierKeys">Not used</param>
    /// <returns>The object if the mouse is over the object</returns>
    public override TNTObject MouseOver(Point mousePosition, Keys modifierKeys)
    {
      TNTObject obj = null;

      if (!Hidden)
      {
        int radius = this.Image.Width;
        GraphicsPath path = new GraphicsPath();
        path.AddEllipse(-radius, -radius, this.Image.Width * 2, this.Image.Height * 2);
        Region region = new Region(path);
        region.Translate(m_Position.X, m_Position.Y);

        obj = region.IsVisible(mousePosition) ? this : null;
      }

      return obj;
    }

    /// <summary>
    /// Moves the control point by the delta values
    /// </summary>
    /// <param name="deltaX">Change in the x position</param>
    /// <param name="deltaY">Change in the y position</param>
    /// <param name="alignPoints">Indicates whether the position is aligned to the grid intersections</param>
    public override void MoveBy(int deltaX, int deltaY, bool alignPoints)
    {
      m_Position.X += deltaX;
      m_Position.Y += deltaY;

      if (alignPoints && OnMoved != null)
      {
        OnMoved(this);
      }
    }

    /// <summary>
    /// Moves object to (x,y)
    /// </summary>
    /// <param name="x">X position</param>
    /// <param name="y">Y position</param>
    /// <param name="alignPoints">If true aligns points (default: false)</param>
    public override void MoveTo(int x, int y, bool alignPoints)
    {
      m_Position.X = x;
      m_Position.Y = y;

      if (alignPoints && OnMoved != null)
      {
        OnMoved(this);
      }
    }

    /// <summary>
    /// Creates a copy of the object (This is not implemented)
    /// </summary>
    /// <returns>Copy of the object</returns>
    public override TNTObject Clone()
    {
      // Should never be called.
      throw new NotImplementedException();
    }

    /// <summary>
    /// Creates a copy of the object
    /// </summary>
    /// <param name="parent">New parent</param>
    /// <returns>Copy of the object</returns>
    virtual public TNTObject Clone(TNTObject parent)
    {
      return new TNTControlPoint(parent, this);
    }

    /// <summary>
    /// Calculates the distance between this and the point
    /// </summary>
    /// <param name="point">Point</param>
    /// <returns>Distance between this and the point</returns>
    virtual public double DistanceFrom(TNTControlPoint point)
    {
      double distance = System.Math.Sqrt(System.Math.Pow((double)point.XPos - (double)this.XPos, 2.0) + System.Math.Pow((double)point.YPos - (double)this.YPos, 2.0)) / (double)TNTConstants.PIXELS_PER_FOOT;
      return distance;
    }

    /// <summary>
    /// Gets the midpoint between this and the point
    /// </summary>
    /// <param name="point">Point</param>
    /// <returns>Midpoint between this and the point</returns>
    virtual public Point GetMidpoint(TNTControlPoint point)
    {
      int minX = System.Math.Min(XPos, point.XPos);
      int maxX = System.Math.Max(XPos, point.XPos);
      int minY = System.Math.Min(YPos, point.YPos);
      int maxY = System.Math.Max(YPos, point.YPos);

      return new Point((maxX - minX) / 2 + minX, (maxY - minY) / 2 + minY);
    }

    /// <summary>
    /// Indiates whether the point is in the rectangle
    /// </summary>
    /// <param name="rectangle">Rectangle</param>
    /// <returns>Not implemented</returns>
    public override bool InRectangle(Rectangle rectangle)
    {
      throw new NotImplementedException();
    }

    /// <summary>
    /// Copies selected properties of the object to this
    /// </summary>
    /// <param name="obj">Object to copy</param>
    public override void Assign(TNTObject obj)
    {
      base.Assign(obj);

      TNTControlPoint cp = obj as TNTControlPoint;

      if (cp != null)
      {
        Position = cp.Position;
        Hidden = cp.Hidden;
      }
    }

    /// <summary>
    /// Aligns the point to the nearest grid intersection
    /// </summary>
    public override void AlignToGrid()
    {
      Position = Position.SnapToGrid();
    }
  }
}
