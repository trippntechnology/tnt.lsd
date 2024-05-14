using System.Diagnostics.CodeAnalysis;
using TNT.LSD.Inventory;
using TNT.LSD.Objects;

namespace TNTObjectsTests
{
  [ExcludeFromCodeCoverage]
  public class SprinklerTests
  {
    [Test]
    public void SetPOLYFittings_Single_Pipe()
    {
      var parts = new CodedParts();
      var pipeSizes = new List<PartSize>() { PartSize.SIZE_075, PartSize.SIZE_100, PartSize.SIZE_125 };
      var outletSizes = new List<PartSize>() { PartSize.SIZE_050, PartSize.SIZE_075, PartSize.SIZE_100, PartSize.SIZE_125 };

      foreach (var pipeSize in pipeSizes)
      {
        var hc = pipeSize == PartSize.SIZE_125 ? PartSize.SIZE_150 : pipeSize;

        foreach (var outletSize in outletSizes)
        {
          if (outletSize > pipeSize) continue;

          parts.Clear();
          Sprinkler.SetPOLYFittings(parts, 1, pipeSize, outletSize);

          if (pipeSize == outletSize)
          {
            Assert.That(parts[$"PF{pipeSize}BT90"].Quantity, Is.EqualTo(1));
          }
          else
          {
            Assert.That(parts[$"PF{pipeSize}X{outletSize}BT90"].Quantity, Is.EqualTo(1));
          }

          Assert.That(parts[$"HC{hc}"].Quantity, Is.EqualTo(1));
        }
      }
    }

    [Test]
    public void SetPOLYFittings_Double_Pipe()
    {
      var parts = new CodedParts();
      var pipeSizes = new List<PartSize>() { PartSize.SIZE_075, PartSize.SIZE_100, PartSize.SIZE_125 };
      var outletSizes = new List<PartSize>() { PartSize.SIZE_050, PartSize.SIZE_075, PartSize.SIZE_100, PartSize.SIZE_125 };

      foreach (var pipeSize in pipeSizes)
      {
        var hc = pipeSize == PartSize.SIZE_125 ? PartSize.SIZE_150 : pipeSize;

        foreach (var outletSize in outletSizes)
        {
          if (outletSize > pipeSize) continue;

          parts.Clear();
          Sprinkler.SetPOLYFittings(parts, 2, pipeSize, outletSize);

          if (outletSize == PartSize.SIZE_050)
          {
            Assert.That(parts[$"PF{pipeSize}HDSADDLE"].Quantity, Is.EqualTo(1));
          }
          else
          {
            Assert.That(parts[$"PF{pipeSize}X{outletSize}BBTTEE"].Quantity, Is.EqualTo(1));
            Assert.That(parts[$"HC{hc}"].Quantity, Is.EqualTo(2));
          }
        }
      }
    }
  }
}
