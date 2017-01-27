using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace TNT.LSD.Objects.ControlPoints
{
	/// <summary>
	/// Represents a Bezier control point
	/// </summary>
	public class TNTBezierControlPoint : TNTControlPoint
	{
		#region Properties

		/// <summary>
		/// Specifies whether the point is locked
		/// </summary>
		public bool IsLocked { get; set; }

		/// <summary>
		/// ID of the associated control point
		/// </summary>
		public Guid EndPointID
		{
			get { return EndPoint.ID; }
			set
			{
				if (EndPoint == null)
				{
					EndPoint = new TNTControlPoint();
				}

				EndPoint.ID = value;
			}
		}

		/// <summary>
		/// Associated control point where point would be locked.
		/// </summary>
		[XmlIgnore()]
		public TNTControlPoint EndPoint { get; set; }

		/// <summary>
		/// Returns the image that represents the object.
		/// </summary>
		protected override Image Image
		{
			get
			{
				if (m_Image == null)
				{
					Stream s = this.GetType().Assembly.GetManifestResourceStream("TNT.LSD.Objects.Images.BezierControlPoint.png");
					m_Image = new Bitmap(s);
				}

				return m_Image;
			}
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Default contructor
		/// </summary>
		public TNTBezierControlPoint()
			: base()
		{
		}

		/// <summary>
		/// Initialization constructor
		/// </summary>
		/// <param name="parent">Parent</param>
		/// <param name="endPoint">Endpoint</param>
		/// <param name="xPos">X position</param>
		/// <param name="yPos">Y position</param>
		public TNTBezierControlPoint(TNTObject parent, TNTControlPoint endPoint, int xPos, int yPos)
			: base(parent, xPos, yPos)
		{
			IsLocked = true;
			EndPoint = endPoint;
		}

		/// <summary>
		/// Initialization constructor
		/// </summary>
		/// <param name="parent">Parent</param>
		/// <param name="endPoint">Endpoint</param>
		/// <param name="position">Position</param>
		public TNTBezierControlPoint(TNTObject parent, TNTControlPoint endPoint, Point position)
			: base(parent, position)
		{
			IsLocked = true;
			EndPoint = endPoint;
		}

		/// <summary>
		/// Copy constructor
		/// </summary>
		/// <param name="parent">Parent</param>
		/// <param name="obj">Object to copy</param>
		public TNTBezierControlPoint(TNTObject parent, TNTBezierControlPoint obj)
			: base(parent, obj)
		{
			IsLocked = obj.IsLocked;
			EndPoint = obj.EndPoint;
		}

		#endregion

		/// <summary>
		/// Copies this object with the new parent
		/// </summary>
		/// <param name="parent">Parent</param>
		/// <returns>Copy of this object with the new parent</returns>
		public override TNTObject Clone(TNTObject parent)
		{
			return new TNTBezierControlPoint(parent, this);
		}

		/// <summary>
		/// Moves the object to the x and y position
		/// </summary>
		/// <param name="x">X position</param>
		/// <param name="y">Y position</param>
		/// <param name="alignPoints">Indicates whether the location should align to the grid intersect</param>
		public override void MoveTo(int x, int y, bool alignPoints)
		{
			base.MoveTo(x, y, alignPoints);
			IsLocked = EndPoint.Position.X == Position.X && EndPoint.Position.Y == Position.Y;
		}

		/// <summary>
		/// Resolves the external references after being loaded from a file
		/// </summary>
		/// <param name="objects">List of external objects</param>
		public override void ResolveReferences(List<TNTObject> objects)
		{
			base.ResolveReferences(objects);

			EndPoint = (TNTControlPoint)(from o in objects where (o is TNTControlPoint) && o.ID == EndPoint.ID select o).Single();
		}

		/// <summary>
		/// Draws the object
		/// </summary>
		/// <param name="graphics">Graphics</param>
		/// <param name="drawingOptions">Drawing options</param>
		public override void Draw(Graphics graphics, DrawingOptions drawingOptions)
		{
			if (!IsLocked)
			{
				// Draw a line from the bezier point to the corresponding control point.
				Pen pen = new Pen(Color.FromArgb(100, Color.Black), 1);
				pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
				graphics.DrawLine(pen, this.Position, this.EndPoint.Position);
				base.Draw(graphics, drawingOptions);
			}
		}

		/// <summary>
		/// Gets the mouse and hint for the object
		/// </summary>
		/// <param name="mousePosition">Not used</param>
		/// <param name="modifierKeys">Not used</param>
		/// <returns></returns>
		public override StateInfo GetStateInfo(Point mousePosition, System.Windows.Forms.Keys modifierKeys)
		{
			return new StateInfo(TNTCursors.CurvePoint, "Click and drag to curve point");
		}
	}
}
