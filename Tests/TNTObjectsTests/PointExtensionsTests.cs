using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using TNT.LSD.Objects;
using TNT.LSD.Objects.Extensions;

namespace TNTObjectsTests
{
	[ExcludeFromCodeCoverage]
	[TestClass]
	public class PointExtensionsTests
	{
		[TestMethod]
		public void SnapToGridTest1()
		{
			int midPoint = TNTConstants.PIXELS_PER_FOOT * 2 - (TNTConstants.PIXELS_PER_FOOT / 2);
			Assert.AreEqual(8, TNTConstants.PIXELS_PER_FOOT);
			Assert.AreEqual(12, midPoint);

			for (int i = TNTConstants.PIXELS_PER_FOOT; i < TNTConstants.PIXELS_PER_FOOT * 2 + 1; i++)
			{
				for (int j = TNTConstants.PIXELS_PER_FOOT; j < TNTConstants.PIXELS_PER_FOOT * 2 + 1; j++)
				{
					Point p = new Point(i, j);
					Point pSnapped = p.SnapToGrid();

					if (i < midPoint && j < midPoint)
					{
						Assert.AreEqual(new Point(TNTConstants.PIXELS_PER_FOOT, TNTConstants.PIXELS_PER_FOOT), pSnapped);
					}
					else if (i >= midPoint && j < midPoint)
					{
						Assert.AreEqual(new Point(TNTConstants.PIXELS_PER_FOOT * 2, TNTConstants.PIXELS_PER_FOOT), pSnapped);
					}
					else if (i >= midPoint && j >= midPoint)
					{
						Assert.AreEqual(new Point(TNTConstants.PIXELS_PER_FOOT * 2, TNTConstants.PIXELS_PER_FOOT * 2), pSnapped);
					}
					else
					{
						Assert.AreEqual(new Point(TNTConstants.PIXELS_PER_FOOT, TNTConstants.PIXELS_PER_FOOT * 2), pSnapped);
					}
				}
			}
		}

		[TestMethod]
		public void SnapToGridTest2()
		{
			Assert.AreEqual(8, TNTConstants.PIXELS_PER_FOOT);

			for (int i = TNTConstants.PIXELS_PER_FOOT; i < TNTConstants.PIXELS_PER_FOOT * 2 + 1; i++)
			{
				for (int j = TNTConstants.PIXELS_PER_FOOT; j < TNTConstants.PIXELS_PER_FOOT * 2 + 1; j++)
				{
					Point p = new Point(i, j);
					Point pSnapped = p.SnapToGrid(false);

					Assert.AreEqual(p, pSnapped);
				}
			}
		}

		[TestMethod]
		public void GetMidPointTest()
		{
			Point startPoint = new Point(13, 17);

			Assert.AreEqual(13, startPoint.X);
			Assert.AreEqual(17, startPoint.Y);

			Point endPoint = new Point(71, 67);

			Assert.AreEqual(71, endPoint.X);
			Assert.AreEqual(67, endPoint.Y);

			PointF midPoint = startPoint.GetMidPoint(endPoint);
			Assert.AreEqual(new PointF(42, 42), midPoint);

			midPoint = endPoint.GetMidPoint(startPoint);
			Assert.AreEqual(new PointF(42, 42), midPoint);
		}

		[TestMethod]
		public void ToWorldCoordinateSpaceTest()
		{
			Bitmap b = new Bitmap(100, 100);
			Graphics g = Graphics.FromImage(b);

			g.ScaleTransform((float)2, (float)2);

			Point origPoint = new Point(30, 42);
			Point newPoint = origPoint.ToWorldCoordinateSpace(g, false);

			Assert.AreEqual(new Point(15, 21), newPoint);

			newPoint = origPoint.ToWorldCoordinateSpace(g);
			Assert.AreEqual(new Point(16, 24), newPoint);

			g.ScaleTransform((float)3, (float)3);
			newPoint = origPoint.ToWorldCoordinateSpace(g, false);

			Assert.AreEqual(new Point(5, 7), newPoint);
		}

		[TestMethod]
		public void AddTest()
		{
			Point p1 = new Point(7, 13);
			Point result = p1.Add(new Point(23, 47));

			Assert.AreEqual(new Point(30, 60), result);
		}

		[TestMethod]
		public void SubtractTest()
		{
			Point p1 = new Point(7, 13);
			Point result = p1.Subtract(new Point(23, 47));

			Assert.AreEqual(new Point(7 - 23, 13 - 47), result);
		}

		[TestMethod]
		public void MultiplyTest()
		{
			Point p1 = new Point(7, 13);
			Point result = p1.Multiply(4);

			Assert.AreEqual(new Point(7 * 4, 13 * 4), result);

			PointF pf1 = new PointF((float)7.3, (float)13.7);
			result = pf1.Multiply(3);

			Assert.AreEqual(new Point(21, 41), result);
		}

		[TestMethod]
		public void SquareTest()
		{
			for (int x = -10; x < 10; x++)
			{
				for (int y = -10; y < 10; y++)
				{
					Point p = new Point(x, y);
					p = p.Square();

					if (Math.Sign(x) == Math.Sign(y))
					{
						if (Math.Sign(x) == 1)
						{
							if (x > y)
							{
								Assert.AreEqual(new Point(x, x), p);
							}
							else
							{
								Assert.AreEqual(new Point(y, y), p);
							}
						}
						else
						{
							if (x < y)
							{
								Assert.AreEqual(new Point(x, x), p);
							}
							else
							{
								Assert.AreEqual(new Point(y, y), p);
							}
						}
					}
					else if (Math.Sign(x) < Math.Sign(y))
					{
						if (Math.Abs(x) > y)
						{
							Assert.AreEqual(new Point(x, Math.Abs(x)), p);
						}
						else
						{
							Assert.AreEqual(new Point(-y, y), p);
						}
					}
					else
					{
						if (x > Math.Abs(y))
						{
							Assert.AreEqual(new Point(x, -x), p);
						}
						else
						{
							Assert.AreEqual(new Point(Math.Abs(y), y), p);
						}
					}
				}
			}
		}

		[TestMethod]
		public void FindExtremePointsTest()
		{
			Point p = new Point(2, 2);
			Point[] ps = null;
			double distance;

			try
			{
				p.FindExtremePoints(ps, out distance);
			}
			catch (ArgumentException ae)
			{
				Assert.IsTrue(ae is ArgumentException);
				Assert.AreEqual("Points array must contain at least one point.", ae.Message);
			}

			ps = new Point[] { new Point(50, 2), new Point(100, 2), new Point(150, 2), new Point(75, 2), new Point(125, 2) };

			p = p.FindExtremePoints(ps, out distance);

			Assert.AreEqual(new Point(150, 2), p);
			Assert.AreEqual(148, distance);
		}

		[TestMethod]
		public void DistancePointTest()
		{
			Point p1 = new Point(10, 10);
			Point p2 = new Point(20, 10);
			Point p3 = new Point(10, 20);
			Point p4 = new Point(20, 20);

			Assert.AreEqual(10, p1.Distance(p2));
			Assert.AreEqual(10, p1.Distance(p3));
			Assert.AreEqual(Math.Sqrt(200), p1.Distance(p4));
		}

		[TestMethod]
		public void DistancePointFTest()
		{
			PointF p1 = new PointF(10.5F, 10.5F);
			PointF p2 = new PointF(20.5F, 10.5F);
			PointF p3 = new PointF(10.5F, 20.5F);
			PointF p4 = new PointF(20.5F, 20.5F);

			Assert.AreEqual(10, p1.Distance(p2));
			Assert.AreEqual(10, p1.Distance(p3));
			Assert.AreEqual(Math.Sqrt(200), p1.Distance(p4));
		}
	}
}
