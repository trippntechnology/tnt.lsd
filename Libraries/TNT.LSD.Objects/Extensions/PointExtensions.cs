using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using TNT.Math;

namespace TNT.LSD.Objects.Extensions
{
	/// <summary>
	/// Contains Point and PointF extension methods
	/// </summary>
	public static class PointExtensions
	{
		/// <summary>
		/// Adjusts the point's location to the nearest grid intersection
		/// </summary>
		/// <param name="thisPoint">This point</param>
		/// <returns>Point moved to the nearest grid intersection</returns>
		public static Point SnapToGrid(this Point thisPoint)
		{
			return SnapToGrid(thisPoint, true);
		}

		/// <summary>
		/// Adjusts the point's location to the nearest grid intersection
		/// </summary>
		/// <param name="thisPoint">This point</param>
		/// <param name="snap">Specifies whether the point should be snapped to the nearest intersection (default: true)</param>
		/// <returns>Point moved to the nearest grid intersection</returns>
		public static Point SnapToGrid(this Point thisPoint, bool snap)
		{
			if (snap)
			{
				int xMod = thisPoint.X % TNTConstants.PIXELS_PER_FOOT;
				int yMod = thisPoint.Y % TNTConstants.PIXELS_PER_FOOT;

				// Set point back to prior grid point
				thisPoint.X = (thisPoint.X / TNTConstants.PIXELS_PER_FOOT) * TNTConstants.PIXELS_PER_FOOT;
				thisPoint.Y = (thisPoint.Y / TNTConstants.PIXELS_PER_FOOT) * TNTConstants.PIXELS_PER_FOOT;

				if (xMod >= (TNTConstants.PIXELS_PER_FOOT / 2.0))
				{
					// Move X location forward
					thisPoint.X += TNTConstants.PIXELS_PER_FOOT;
				}

				if (yMod >= (TNTConstants.PIXELS_PER_FOOT / 2.0))
				{
					// Move Y location forward
					thisPoint.Y += TNTConstants.PIXELS_PER_FOOT;
				}
			}

			return thisPoint;
		}

		/// <summary>
		/// Returns the point between this point and the endPoint
		/// </summary>
		/// <param name="thisPoint">This point</param>
		/// <param name="endPoint">Indicates the location of the endPoint</param>
		/// <returns>Point between this point the the endPoint</returns>
		public static PointF GetMidPoint(this Point thisPoint, Point endPoint)
		{
			PointF midPoint = new PointF();
			
			midPoint.X = (float)(System.Math.Min(thisPoint.X, endPoint.X) + System.Math.Abs(thisPoint.X - endPoint.X) / 2.0);
			midPoint.Y = (float)(System.Math.Min(thisPoint.Y, endPoint.Y) + System.Math.Abs(thisPoint.Y - endPoint.Y) / 2.0);

			return midPoint;
		}

		/// <summary>
		/// Transforms this point to the world coordinate space
		/// </summary>
		/// <param name="thisPoint">This point</param>
		/// <param name="graphic">Graphic to use when performing the transform</param>
		/// <returns>Transformed point</returns>
		public static Point ToWorldCoordinateSpace(this Point thisPoint, Graphics graphic)
		{
			return ToWorldCoordinateSpace(thisPoint, graphic, true);
		}

		/// <summary>
		/// Transforms this point to the world coordinate space
		/// </summary>
		/// <param name="thisPoint">This point</param>
		/// <param name="graphic">Graphic to use when performing the transform</param>
		/// <param name="snapToGrid">Specifies whether the transformed point should be snapped to the nearest
		/// grid intersection (default: true)</param>
		/// <returns>Transformed point</returns>
		public static Point ToWorldCoordinateSpace(this Point thisPoint, Graphics graphic, bool snapToGrid)
		{
			Point[] pointArray = { thisPoint };

			graphic.TransformPoints(CoordinateSpace.World, CoordinateSpace.Page, pointArray);

			return snapToGrid ? pointArray[0].SnapToGrid() : pointArray[0];
		}

		/// <summary>
		/// Transforms this point to the page coordinate space
		/// </summary>
		/// <param name="thisPoint">This point</param>
		/// <param name="graphics">Graphics</param>
		/// <param name="snapToGrid">Specifies whether the transformed point should be snapped to the nearest</param>
		/// <returns>Transformed point</returns>
		public static Point ToPageCoordinateSpace(this Point thisPoint, Graphics graphics, bool snapToGrid)
		{
			Point[] pointArray = { thisPoint };

			graphics.TransformPoints(CoordinateSpace.Page, CoordinateSpace.World, pointArray);

			return snapToGrid ? pointArray[0].SnapToGrid() : pointArray[0];
		}

		#region Operators

		/// <summary>
		/// Subtracts point from this point
		/// </summary>
		/// <param name="thisPoint">This point</param>
		/// <param name="point">Point subtracted from this</param>
		/// <returns>New point offset by point</returns>
		public static Point Subtract(this Point thisPoint, Point point)
		{
			return new Point(thisPoint.X - point.X, thisPoint.Y - point.Y);
		}

		/// <summary>
		/// Returns a new <see cref="PointF"/> object representing the difference 
		/// between the two points
		/// </summary>
		/// <param name="thisPoint">First point</param>
		/// <param name="point">Second point</param>
		/// <returns>New <see cref="PointF"/> representing the difference between the 
		/// two points</returns>
		public static PointF Subtract(this PointF thisPoint, PointF point)
		{
			return new PointF(thisPoint.X - point.X, thisPoint.Y - point.Y);
		}

		/// <summary>
		/// Adds point to this point
		/// </summary>
		/// <param name="thisPoint">This point</param>
		/// <param name="point">Point added to this</param>
		/// <returns>New point offset by point</returns>
		public static Point Add(this Point thisPoint, Point point)
		{
			return new Point(thisPoint.X + point.X, thisPoint.Y + point.Y);
		}

		/// <summary>
		/// Multiplies the X and Y position by multiplier
		/// </summary>
		/// <param name="thisPoint">This point</param>
		/// <param name="multiplier">Multiplier</param>
		/// <returns>New point where each position was multiplied by multiplier</returns>
		public static Point Multiply(this Point thisPoint, double multiplier)
		{
			return new Point((int)(thisPoint.X * multiplier), (int)(thisPoint.Y * multiplier));
		}

		/// <summary>
		/// Multiplies the X and Y position by multiplier
		/// </summary>
		/// <param name="thisPoint">This point</param>
		/// <param name="multiplier">Multiplier</param>
		/// <returns>New point where each position was multiplied by multiplier</returns>
		public static Point Multiply(this PointF thisPoint, double multiplier)
		{
			return new Point((int)(thisPoint.X * multiplier), (int)(thisPoint.Y * multiplier));
		}

		#endregion

		/// <summary>
		/// Adjusted point so that the X and Y location are the same.
		/// </summary>
		/// <param name="thisPoint">This point</param>
		/// <returns>This point with adjusted values</returns>
		public static Point Square(this Point thisPoint)
		{
			if (System.Math.Sign(thisPoint.X) == System.Math.Sign(thisPoint.Y))
			{
				if (System.Math.Sign(thisPoint.X) == 1)
				{
					if (thisPoint.X > thisPoint.Y)
					{
						thisPoint.Y = thisPoint.X;
					}
					else
					{
						thisPoint.X = thisPoint.Y;
					}
				}
				else
				{
					if (thisPoint.X < thisPoint.Y)
					{
						thisPoint.Y = thisPoint.X;
					}
					else
					{
						thisPoint.X = thisPoint.Y;
					}
				}
			}
			else if (System.Math.Sign(thisPoint.X) < System.Math.Sign(thisPoint.Y))
			{
				if (System.Math.Abs(thisPoint.X) > thisPoint.Y)
				{
					thisPoint.Y = System.Math.Abs(thisPoint.X);
				}
				else
				{
					thisPoint.X = -thisPoint.Y;
				}
			}
			else
			{
				if (thisPoint.X > System.Math.Abs(thisPoint.Y))
				{
					thisPoint.Y = -thisPoint.X;
				}
				else
				{
					thisPoint.X = System.Math.Abs(thisPoint.Y);
				}
			}

			return thisPoint;
		}

		/// <summary>
		/// Move this point to a location where it creates angle between a line from originPoint to this point and a line
		/// verticle line from originPoint
		/// </summary>
		/// <param name="thisPoint">This point</param>
		/// <param name="originPoint">Point representing the origin</param>
		/// <param name="angle">Angle that this point should be moved to</param>
		/// <returns>Adjustment to this point given the angle</returns>
		public static Point AdjustToAngle(this Point thisPoint, Point originPoint, int angle)
		{
			Vector currentVector = new Vector(thisPoint, originPoint);

			// Get a vector with the current angle
			Vector angledVector = new Vector(new Angle(angle, true), 1);

			PointF pf = (angledVector.Unit * currentVector.Magnitude) + originPoint;

			return new Point((int)pf.X, (int)pf.Y);
		}

		/// <summary>
		/// Returns the point that is furthest from this point
		/// </summary>
		/// <param name="thisPoint">This point</param>
		/// <param name="points">Array of points</param>
		/// <param name="distance">Specifies the distance from the furthest point</param>
		/// <returns>The point in points that is furthest from this point</returns>
		/// <exception cref="ArgumentException"></exception>
		public static Point FindExtremePoints(this Point thisPoint, Point[] points, out double distance)
		{
			if (points == null || points.Length < 1)
			{
				throw new ArgumentException("Points array must contain at least one point.");
			}

			Point extremePoint = Point.Empty;
			distance = 0;

			for (int index = 0; index < points.Length; index++)
			{
				double thisDistance = new Vector(thisPoint, points[index]).Magnitude;

				if (thisDistance > distance)
				{
					extremePoint = points[index];
					distance = thisDistance;
				}
			}

			return extremePoint;
		}

		/// <summary>
		/// Calculates the distance in pixels between two <see cref="Point"/> objects
		/// </summary>
		/// <param name="thisPoint">One of the points</param>
		/// <param name="point">The other point</param>
		/// <returns>Distance in pixels between two <see cref="Point"/></returns>
		public static double Distance(this Point thisPoint, Point point)
		{
			Point result = thisPoint.Subtract(point);
			return System.Math.Sqrt(System.Math.Pow(result.X, 2) + System.Math.Pow(result.Y, 2));
		}

		/// <summary>
		/// Calculates the distance in pixels between two <see cref="PointF"/> objects
		/// </summary>
		/// <param name="thisPoint">One of the points</param>
		/// <param name="point">The other point</param>
		/// <returns>Distance in pixels between two <see cref="PointF"/></returns>
		public static double Distance(this PointF thisPoint, PointF point)
		{
			PointF result = thisPoint.Subtract(point);
			return System.Math.Sqrt(System.Math.Pow(result.X, 2) + System.Math.Pow(result.Y, 2));
		}
	}
}
