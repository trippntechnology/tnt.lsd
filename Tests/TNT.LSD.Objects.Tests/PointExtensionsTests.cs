using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using TNT.LSD.Objects;
using TNT.LSD.Objects.Extensions;

namespace TNTObjectsTests
{
  [ExcludeFromCodeCoverage]
  public class PointExtensionsTests
  {
    [Test]
    public void SnapToGridTest1()
    {
      int midPoint = TNTConstants.PIXELS_PER_FOOT * 2 - (TNTConstants.PIXELS_PER_FOOT / 2);
      Assert.That(TNTConstants.PIXELS_PER_FOOT, Is.EqualTo(8));
      Assert.That(midPoint, Is.EqualTo(12));

      for (int i = TNTConstants.PIXELS_PER_FOOT; i < TNTConstants.PIXELS_PER_FOOT * 2 + 1; i++)
      {
        for (int j = TNTConstants.PIXELS_PER_FOOT; j < TNTConstants.PIXELS_PER_FOOT * 2 + 1; j++)
        {
          Point p = new Point(i, j);
          Point pSnapped = p.SnapToGrid();

          if (i < midPoint && j < midPoint)
          {
            Assert.That(pSnapped, Is.EqualTo(new Point(TNTConstants.PIXELS_PER_FOOT, TNTConstants.PIXELS_PER_FOOT)));
          }
          else if (i >= midPoint && j < midPoint)
          {
            Assert.That(pSnapped, Is.EqualTo(new Point(TNTConstants.PIXELS_PER_FOOT * 2, TNTConstants.PIXELS_PER_FOOT)));
          }
          else if (i >= midPoint && j >= midPoint)
          {
            Assert.That(pSnapped, Is.EqualTo(new Point(TNTConstants.PIXELS_PER_FOOT * 2, TNTConstants.PIXELS_PER_FOOT * 2)));
          }
          else
          {
            Assert.That(pSnapped, Is.EqualTo(new Point(TNTConstants.PIXELS_PER_FOOT, TNTConstants.PIXELS_PER_FOOT * 2)));
          }
        }
      }
    }

    [Test]
    public void SnapToGridTest2()
    {
      Assert.That(TNTConstants.PIXELS_PER_FOOT, Is.EqualTo(8));

      for (int i = TNTConstants.PIXELS_PER_FOOT; i < TNTConstants.PIXELS_PER_FOOT * 2 + 1; i++)
      {
        for (int j = TNTConstants.PIXELS_PER_FOOT; j < TNTConstants.PIXELS_PER_FOOT * 2 + 1; j++)
        {
          Point p = new Point(i, j);
          Point pSnapped = p.SnapToGrid(false);

          Assert.That(pSnapped, Is.EqualTo(p));
        }
      }
    }

    [Test]
    public void GetMidPointTest()
    {
      Point startPoint = new Point(13, 17);

      Assert.That(startPoint.X, Is.EqualTo(13));
      Assert.That(startPoint.Y, Is.EqualTo(17));

      Point endPoint = new Point(71, 67);

      Assert.That(endPoint.X, Is.EqualTo(71));
      Assert.That(endPoint.Y, Is.EqualTo(67));

      PointF midPoint = startPoint.GetMidPoint(endPoint);
      Assert.That(midPoint, Is.EqualTo(new PointF(42, 42)));

      midPoint = endPoint.GetMidPoint(startPoint);
      Assert.That(midPoint, Is.EqualTo(new PointF(42, 42)));
    }

    [Test]
    public void ToWorldCoordinateSpaceTest()
    {
      Bitmap b = new Bitmap(100, 100);
      Graphics g = Graphics.FromImage(b);

      g.ScaleTransform(2, 2);

      Point origPoint = new Point(30, 42);
      Point newPoint = origPoint.ToWorldCoordinateSpace(g, false);

      Assert.That(newPoint, Is.EqualTo(new Point(15, 21)));

      newPoint = origPoint.ToWorldCoordinateSpace(g);
      Assert.That(newPoint, Is.EqualTo(new Point(16, 24)));

      g.ScaleTransform(3, 3);
      newPoint = origPoint.ToWorldCoordinateSpace(g, false);

      Assert.That(newPoint, Is.EqualTo(new Point(5, 7)));
    }

    [Test]
    public void AddTest()
    {
      Point p1 = new Point(7, 13);
      Point result = p1.Add(new Point(23, 47));

      Assert.That(result, Is.EqualTo(new Point(30, 60)));
    }

    [Test]
    public void SubtractTest()
    {
      Point p1 = new Point(7, 13);
      Point result = p1.Subtract(new Point(23, 47));

      Assert.That(result, Is.EqualTo(new Point(7 - 23, 13 - 47)));
    }

    [Test]
    public void MultiplyTest()
    {
      Point p1 = new Point(7, 13);
      Point result = p1.Multiply(4);

      Assert.That(result, Is.EqualTo(new Point(7 * 4, 13 * 4)));

      PointF pf1 = new PointF((float)7.3, (float)13.7);
      result = pf1.Multiply(3);

      Assert.That(result, Is.EqualTo(new Point(21, 41)));
    }

    [Test]
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
                Assert.That(p, Is.EqualTo(new Point(x, x)));
              }
              else
              {
                Assert.That(p, Is.EqualTo(new Point(y, y)));
              }
            }
            else
            {
              if (x < y)
              {
                Assert.That(p, Is.EqualTo(new Point(x, x)));
              }
              else
              {
                Assert.That(p, Is.EqualTo(new Point(y, y)));
              }
            }
          }
          else if (Math.Sign(x) < Math.Sign(y))
          {
            if (Math.Abs(x) > y)
            {
              Assert.That(p, Is.EqualTo(new Point(x, Math.Abs(x))));
            }
            else
            {
              Assert.That(p, Is.EqualTo(new Point(-y, y)));
            }
          }
          else
          {
            if (x > Math.Abs(y))
            {
              Assert.That(p, Is.EqualTo(new Point(x, -x)));
            }
            else
            {
              Assert.That(p, Is.EqualTo(new Point(Math.Abs(y), y)));
            }
          }
        }
      }
    }

    [Test]
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
        Assert.That(ae.Message, Is.EqualTo("Points array must contain at least one point."));
      }

      ps = new Point[] { new Point(50, 2), new Point(100, 2), new Point(150, 2), new Point(75, 2), new Point(125, 2) };

      p = p.FindExtremePoints(ps, out distance);

      Assert.That(p, Is.EqualTo(new Point(150, 2)));
      Assert.That(distance, Is.EqualTo(148));
    }

    [Test]
    public void DistancePointTest()
    {
      Point p1 = new Point(10, 10);
      Point p2 = new Point(20, 10);
      Point p3 = new Point(10, 20);
      Point p4 = new Point(20, 20);

      Assert.That(p1.Distance(p2), Is.EqualTo(10));
      Assert.That(p1.Distance(p3), Is.EqualTo(10));
      Assert.That(p1.Distance(p4), Is.EqualTo(Math.Sqrt(200)));
    }

    [Test]
    public void DistancePointFTest()
    {
      PointF p1 = new PointF(10.5F, 10.5F);
      PointF p2 = new PointF(20.5F, 10.5F);
      PointF p3 = new PointF(10.5F, 20.5F);
      PointF p4 = new PointF(20.5F, 20.5F);

      Assert.That(p1.Distance(p2), Is.EqualTo(10));
      Assert.That(p1.Distance(p3), Is.EqualTo(10));
      Assert.That(p1.Distance(p4), Is.EqualTo(Math.Sqrt(200)));
    }
  }
}
