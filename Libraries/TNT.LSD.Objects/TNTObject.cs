using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Serialization;
using TNT.LSD.Objects.ControlPoints;
using TNT.LSD.Objects.Extensions;
using TNT.Math;

namespace TNT.LSD.Objects
{
	/// <summary>
	/// Base object for all TNT.LSD.Objects
	/// </summary>
	public abstract class TNTObject
	{
		#region Members

		/// <summary>
		/// Indicates whether this object is selected
		/// </summary>
		protected bool m_Selected = false;

		/// <summary>
		/// Specifies the selected control point
		/// </summary>
		protected TNTControlPoint m_SelectedControlPoint = null;

		/// <summary>
		/// Specifies the active control point
		/// </summary>
		protected TNTControlPoint m_ActiveControlPoint = null;

		#endregion

		#region Properties

		/// <summary>
		/// Returns the value of m_Selected. When set to false, nulls out m_SelectedControlPoint
		/// </summary>
		[Browsable(false)]
		[XmlIgnore()]
		public virtual bool Selected { get { return m_Selected; } set { m_Selected = value; if (!m_Selected) { m_SelectedControlPoint = null; } } }

		/// <summary>
		/// Indicates if a control point is selected
		/// </summary>
		[Browsable(false)]
		virtual public bool OverControlPoint
		{
			get { return m_SelectedControlPoint != null; }
		}

		/// <summary>
		/// Contains the control points for this object
		/// </summary>
		[Browsable(false)]
		[XmlElement(typeof(TNTBezierControlPoint))]
		[XmlElement(typeof(TNTControlPoint))]
		public List<TNTControlPoint> ControlPoints { get; set; }

		/// <summary>
		/// The unique ID associated with the object
		/// </summary>
		[Browsable(false)]
		public Guid ID { get; set; }

		/// <summary>
		/// Indicates whether the object can be cloned
		/// </summary>
		[Browsable(false)]
		public virtual bool CanClone { get { return true; } }

		#endregion // Properties

		#region Protected Methods

		/// <summary>
		/// Converts the ControlPoints into a list of Points
		/// </summary>
		/// <returns>List of Points representing the ControlPoints</returns>
		virtual protected List<Point> GetGDIPoints()
		{
			List<Point> points = new List<Point>();
			points = (from p in ControlPoints select p.Position).ToList();
			return points;
		}

		/// <summary>
		/// Gets the control point that is under the mouse position if exists
		/// </summary>
		/// <param name="mousePosition">Position of mouse</param>
		/// <param name="modifierKeys">Modifier keys</param>
		/// <returns>Control point that is under the mouse position if exists</returns>
		virtual protected TNTControlPoint GetControlPoint(Point mousePosition, Keys modifierKeys)
		{
			TNTControlPoint cp = null;

			foreach (TNTControlPoint tmpCP in ControlPoints)
			{
				// Check if we're over a the control point
				if (tmpCP.MouseOver(mousePosition, modifierKeys) != null)
				{
					cp = tmpCP;

					if (modifierKeys == Keys.Shift && (cp as TNTBezierControlPoint) != null)
					{
						// Bezier point was found and since shift is down that is what we want
						break;
					}
					else if ((modifierKeys != Keys.Shift || modifierKeys == Keys.Control) && (cp as TNTBezierControlPoint) == null)
					{
						// Regular control point found. That all that is wanted so we're done.
						break;
					}
				}
			}

			return cp;
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Default constructor
		/// </summary>
		public TNTObject()
		{
			ControlPoints = new List<TNTControlPoint>();
			ID = Guid.NewGuid();
		}

		/// <summary>
		/// Copy constructor
		/// </summary>
		/// <param name="obj">Object to copy</param>
		public TNTObject(TNTObject obj)
		{
			ControlPoints = new List<TNTControlPoint>();

			foreach (TNTControlPoint cp in obj.ControlPoints)
			{
				ControlPoints.Add(cp.Clone(this) as TNTControlPoint);
			}

			ID = Guid.NewGuid();
		}

		#endregion

		/// <summary>
		/// Draws each of the control points and the distance between them
		/// </summary>
		/// <param name="graphics">Graphics</param>
		/// <param name="drawingOptions">Drawing optoins</param>
		virtual public void Draw(Graphics graphics, DrawingOptions drawingOptions)
		{
			if (Selected)
			{
				foreach (TNTControlPoint cp in ControlPoints)
				{
					cp.Draw(graphics, drawingOptions);
				}
			}

			if (Selected || drawingOptions.AlwaysShowDistances)
			{
				DrawDistances(graphics);
			}
		}

		/// <summary>
		/// Draws the distances between the control points
		/// </summary>
		/// <param name="graphics">Graphics</param>
		virtual public void DrawDistances(Graphics graphics)
		{

			for (int index = 1; index < ControlPoints.Count; index++)
			{
				DrawDistance(graphics, ControlPoints[index - 1], ControlPoints[index]);
			}
		}

		/// <summary>
		/// Calculates and draws the distance between two <see cref="TNTControlPoint"/> objects
		/// </summary>
		/// <param name="graphics">Graphics</param>
		/// <param name="p1">First point</param>
		/// <param name="p2">Second point</param>
		virtual public void DrawDistance(Graphics graphics, TNTControlPoint p1, TNTControlPoint p2)
		{
			DrawDistance(graphics, p1, p2, p1.DistanceFrom(p2));
		}

		/// <summary>
		/// Draws <paramref name="distance"/> between two <see cref="TNTControlPoint"/> objects
		/// </summary>
		/// <param name="graphics">Graphics</param>
		/// <param name="p1">First point</param>
		/// <param name="p2">Second point</param>
		/// <param name="distance">Value to draw</param>
		virtual public void DrawDistance(Graphics graphics, TNTControlPoint p1, TNTControlPoint p2, double distance)
		{
			Point midPoint = p1.GetMidpoint(p2);

			Vector v1 = new Vector(p1.Position, p2.Position);
			Angle a1 = v1.Angle(new Vector(0, -1));
			float angle = (float)(System.Math.Sign(v1.X) < 0 ? 360 - a1.InDegrees : a1.InDegrees);// p1.Position.Angle(p2.Position);
			string fomattedDistance = string.Format("{0:0.00}'", distance);

			SolidBrush sBrush = new SolidBrush(Color.Black);
			Font myFont = new Font("Microsoft Sans Serif", (float)8.25);
			RectangleF textRect = new RectangleF(-100, -10, 200, 0);
			StringFormat format = new StringFormat();
			format.Alignment = StringAlignment.Center;
			format.LineAlignment = StringAlignment.Center;
			graphics.TranslateTransform(midPoint.X, midPoint.Y);

			// Adjust the angle so that the text is never below the line upside down.
			if (angle == 0 || angle > 180)
			{
				angle += 90;
			}
			else
			{
				angle -= 90;
			}

			graphics.RotateTransform(angle);

			graphics.DrawString(fomattedDistance, myFont, sBrush, textRect, format);

			graphics.RotateTransform(-angle);
			graphics.TranslateTransform(-midPoint.X, -midPoint.Y);
		}

		/// <summary>
		/// Moves each control point by deltaX and deltaY values, aligning with grid intersection
		/// if indicated
		/// </summary>
		/// <param name="deltaX">Change in x position</param>
		/// <param name="deltaY">Change in y position</param>
		/// <param name="alignPoints">Indicates whether new location should align with grid intersection</param>
		virtual public void MoveBy(int deltaX, int deltaY, bool alignPoints)
		{
			foreach (TNTControlPoint cp in ControlPoints)
			{
				cp.MoveBy(deltaX, deltaY, alignPoints);
			}
		}

		/// <summary>
		/// Moves object to new position specified by x and y
		/// </summary>
		/// <param name="x">X position</param>
		/// <param name="y">Y position</param>
		virtual public void MoveTo(int x, int y)
		{
			MoveTo(x, y, false);
		}

		/// <summary>
		/// Implement to move the object to the location specified
		/// </summary>
		/// <param name="x">X position</param>
		/// <param name="y">Y position</param>
		/// <param name="alignPoints">If true aligns points (default: false)</param>
		virtual public void MoveTo(int x, int y, bool alignPoints)
		{
			// This likely is never called.
		}

		/// <summary>
		/// Moves the control point to the exact location specified by <paramref name="currentPos"/> if control point
		/// is selected, otherwise move the object by the delta values
		/// </summary>
		/// <param name="currentPos">Current position</param>
		/// <param name="lastPosition">Last position</param>
		/// <param name="alignToGrid">Indicates positions should be aligned to the grid</param>
		/// <param name="alignPoints">Indicates if points should be aligned</param>
		/// <param name="modifierKeys">Modifier keys</param>
		public void Move(Point currentPos, Point lastPosition, bool alignToGrid, bool alignPoints, Keys modifierKeys)
		{
			var adjCurrent = alignToGrid ? currentPos.SnapToGrid() : currentPos;
			var adjLast = alignToGrid ? lastPosition.SnapToGrid() : lastPosition;
			var deltaX = adjCurrent.X - adjLast.X;
			var deltaY = adjCurrent.Y - adjLast.Y;

			if (m_SelectedControlPoint != null)
			{
				TNTBezierControlPoint bezierPoint = m_SelectedControlPoint as TNTBezierControlPoint;

				if (bezierPoint != null && modifierKeys == Keys.Shift)
				{
					// Adjust the other bezier point in the opposite direction
					Point adjPoint = bezierPoint.EndPoint.Position.Subtract(currentPos).Add(bezierPoint.EndPoint.Position);
					var controlPoints = (from p in this.ControlPoints where (p is TNTBezierControlPoint) && (p as TNTBezierControlPoint).EndPointID == bezierPoint.EndPointID && p.ID != bezierPoint.ID select p).ToList();
					controlPoints.ForEach(p => p.MoveTo(adjPoint.X, adjPoint.Y, true));
					m_SelectedControlPoint.MoveTo(currentPos.X, currentPos.Y, true);
				}
				else
				{
					if (m_SelectedControlPoint is TNTBezierControlPoint)
					{
						// Don't align this move to grid
						m_SelectedControlPoint.MoveTo(currentPos.X, currentPos.Y, true);
					}
					else
					{
						// We're over control point. Move control point
						m_SelectedControlPoint.MoveTo(adjCurrent.X, adjCurrent.Y, true);
					}
				}
			}
			else
			{
				this.MoveBy(deltaX, deltaY, alignPoints);
			}
		}

		/// <summary>
		/// Sets the selected control point if the mouse position is over the point
		/// </summary>
		/// <param name="mousePosition">Mouse position</param>
		/// <param name="modifierKeys">Modifier keys</param>
		virtual public void MouseDown(Point mousePosition, Keys modifierKeys)
		{
			m_SelectedControlPoint = GetControlPoint(mousePosition, modifierKeys);
		}

		/// <summary>
		/// Sets selected control point to null
		/// </summary>
		/// <param name="mousePosition">Mouse position</param>
		/// <param name="ctrlDown">Specifies whether control key is down</param>
		/// <param name="shiftDown">Specifies whether shift key is down</param>
		virtual public void MouseUp(Point mousePosition, bool ctrlDown, bool shiftDown)
		{
			m_SelectedControlPoint = null;
		}

		/// <summary>
		/// Implement to inserts a control point with the position specified
		/// </summary>
		/// <param name="position">Position</param>
		virtual public void InsertControlPoint(Point position)
		{
		}

		/// <summary>
		/// Implement to add a control point with the position specified
		/// </summary>
		/// <param name="position">Position</param>
		virtual public void AddControlPoint(Point position)
		{
		}

		/// <summary>
		/// Implement to remove the control point specified
		/// </summary>
		/// <param name="controlPoint">Control point to remove</param>
		virtual public void RemoveControlPoint(TNTControlPoint controlPoint)
		{

		}

		/// <summary>
		/// Assigns obj properties on this object
		/// </summary>
		/// <param name="obj">Object whos properties are being assigned</param>
		virtual public void Assign(TNTObject obj)
		{
			if (obj != null)
			{
				ControlPoints = obj.ControlPoints;
			}
		}

		/// <summary>
		/// Creates a copy of the object with only those properties that might change
		/// </summary>
		/// <returns>An undo copy</returns>
		virtual public TNTObject CreateUndoCopy()
		{
			TNTObject newTNTObject = Activator.CreateInstance(GetType()) as TNTObject;

			foreach (TNTControlPoint cp in ControlPoints)
			{
				newTNTObject.ControlPoints.Add(cp.Clone(this) as TNTControlPoint);
			}

			return newTNTObject;
		}

		/// <summary>
		/// Implement to resolve reference when object is loaded from disk that are not saved
		/// </summary>
		/// <param name="objects">List of other objects where reference may exist</param>
		virtual public void ResolveReferences(List<TNTObject> objects)
		{
		}

		/// <summary>
		/// Gets the state information for this object
		/// </summary>
		/// <param name="mousePosition">Mouse position</param>
		/// <param name="modifierKeys">Modifier keys</param>
		/// <returns></returns>
		virtual public StateInfo GetStateInfo(Point mousePosition, Keys modifierKeys)
		{
			if (Selected)
			{
				return new StateInfo(TNTCursors.SelectObject, "Click and drag to move");
			}
			else
			{
				return new StateInfo(TNTCursors.SelectObject, "Click to select");
			}
		}

		/// <summary>
		/// Implement to get object under mouse position
		/// </summary>
		/// <param name="mousePosition">Mouse position</param>
		/// <param name="modifierKeys">Modifier keys</param>
		/// <returns>Object under mouse if exists, null otherwise</returns>
		abstract public TNTObject MouseOver(Point mousePosition, Keys modifierKeys);
		abstract public TNTObject Clone();
		/// <summary>
		/// Imlement to check whether object is with rectangle
		/// </summary>
		/// <param name="rectangle">Rectangle</param>
		/// <returns>True if object is within rectangle, false otherwise</returns>
		abstract public bool InRectangle(Rectangle rectangle);

		/// <summary>
		/// Implement to align the object to the nearest grid intersection
		/// </summary>
		abstract public void AlignToGrid();
	}
}
