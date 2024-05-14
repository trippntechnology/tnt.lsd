using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using TNT.LSD.Inventory;
using TNT.LSD.Objects;

namespace TNTObjectsTests
{
  [ExcludeFromCodeCoverage]
  public class ValveBoxTests
  {
    [Test]
    public void AddPVCMainlineAdapter()
    {
      var parts = new CodedParts();
      var pipeSizes = new List<PartSize>() { PartSize.SIZE_075, PartSize.SIZE_100, PartSize.SIZE_125, PartSize.SIZE_150, PartSize.SIZE_200 };
      var manifolds = new List<Manifold>() { Manifold.DOUBLE_100, Manifold.DOUBLE_150, Manifold.DOUBLE_200 };

      foreach (var manifold in manifolds)
      {
        var manifoldSize = manifold.Size;

        foreach (var pipeSize in pipeSizes)
        {
          if (pipeSize > manifoldSize && pipeSize != PartSize.SIZE_125) break;

          Debug.WriteLine($"{manifoldSize}:{pipeSize}");

          parts.Clear();
          ValveBox.AddPVCMainLineAdapter(parts, pipeSize, manifold);

          if (manifoldSize == PartSize.SIZE_100 && pipeSize == PartSize.SIZE_075)
          {
            Assert.That(parts[$"AF18013"].Quantity, Is.EqualTo(1));
          }
          else if (manifoldSize == PartSize.SIZE_100)
          {
            Assert.That(parts[$"AF18012"].Quantity, Is.EqualTo(1));

            if (pipeSize == PartSize.SIZE_125)
            {
              Assert.That(parts[$"FI125SSCOUP"].Quantity, Is.EqualTo(1));
            }
          }
          else
          {
            Assert.That(parts[$"AF18012{manifoldSize}"].Quantity, Is.EqualTo(1));

            if (manifoldSize == pipeSize)
            {
              Assert.That(parts[$"FI{manifoldSize}SSCOUP"].Quantity, Is.EqualTo(1));
            }
          }
        }
      }
    }

    [Test]
    public void AddPOLYMainlineAdapter_NotSupported_Manifold()
    {
      Assert.Throws<NotSupportedException>(() => ValveBox.AddPOLYMainLineAdapter(new CodedParts(), PartSize.SIZE_100, Manifold.DOUBLE_150));
    }

    [Test]
    public void AddPOLYMainlineAdapter_NotSupported_PipSize()
    {
      Assert.Throws<NotSupportedException>(() => ValveBox.AddPOLYMainLineAdapter(new CodedParts(), PartSize.SIZE_150, Manifold.DOUBLE_100));
    }

    [Test]
    public void AddPOLYMainlineAdapter_PVC()
    {
      var parts = new CodedParts();
      var pipeSizes = new List<PartSize>() { PartSize.SIZE_075, PartSize.SIZE_100, PartSize.SIZE_125 };

      var manifold = Manifold.DOUBLE_100;
      var manifoldSize = manifold.Size;

      foreach (var pipeSize in pipeSizes)
      {
        var clamp = pipeSize == PartSize.SIZE_125 ? PartSize.SIZE_150 : pipeSize;

        parts.Clear();
        ValveBox.AddPOLYMainLineAdapter(parts, pipeSize, manifold);

        if (pipeSize == PartSize.SIZE_075)
        {
          Assert.That(parts[$"AF18015"].Quantity, Is.EqualTo(1));
        }
        else if (pipeSize == PartSize.SIZE_100)
        {
          Assert.That(parts[$"AF18014"].Quantity, Is.EqualTo(1));
        }
        else
        {
          Assert.That(parts[$"AF18018"].Quantity, Is.EqualTo(1));
        }

        Assert.That(parts[$"HC{clamp}"].Quantity, Is.EqualTo(1));
      }
    }

    [Test]
    public void AddManifoldCap()
    {
      var parts = new CodedParts();
      var pipeSizes = new List<PartSize>() { PartSize.SIZE_075, PartSize.SIZE_100, PartSize.SIZE_125, PartSize.SIZE_150, PartSize.SIZE_200 };
      var manifolds = new List<Manifold>() { Manifold.DOUBLE_100, Manifold.DOUBLE_150, Manifold.DOUBLE_200 };

      manifolds.ForEach(manSize =>
      {
        pipeSizes.ForEach(pipeSize =>
        {
          parts.Clear();
          ValveBox.AddManifoldCap(parts, pipeSize, manSize);

          if (manSize.Size == PartSize.SIZE_100)
          {
            Assert.That(parts["AF18000"].Quantity, Is.EqualTo(1));
          }
          else
          {
            Assert.That(parts[$"AF18012{manSize.Size}"].Quantity, Is.EqualTo(1));
            Assert.That(parts[$"FI{pipeSize.Code}SCAP"].Quantity, Is.EqualTo(1));
          }
        });
      });
    }
  }
}
