using System.Diagnostics.CodeAnalysis;
using TNT.LSD.Inventory;
using TNT.LSD.Objects;
using TNT.LSD.Settings;

namespace TNTObjectsTests
{
  [ExcludeFromCodeCoverage]
  public class FilterTests
  {
    private CodedParts parts = new CodedParts();
    private List<PartSize> filterSizes = new List<PartSize>()
      {
        PartSize.SIZE_100,
        PartSize.SIZE_150,
        PartSize.SIZE_200
      };
    private List<PartSize> mainSizes = new List<PartSize>()
      {
        PartSize.SIZE_075,
        PartSize.SIZE_100,
        PartSize.SIZE_125,
        PartSize.SIZE_150,
        PartSize.SIZE_200
      };


    [Test]
    public void SetFilterBoxBallValveTest()
    {
      filterSizes.ForEach(size =>
      {
        parts.Clear();
        Filter.SetFilterBoxBallValve(parts, size, "300 MICRON");
        Assert.That(parts[$"{size}FILTER300M"].Quantity, Is.EqualTo(1));
        Assert.That(parts[$"VBJUMBO"].Quantity, Is.EqualTo(1));
        Assert.That(parts[$"BV{size}FT"].Quantity, Is.EqualTo(1));
      });
    }

    [Test]
    public void AddFittings_PVC()
    {
      filterSizes.ForEach(filterSize =>
      {
        mainSizes.ForEach(mainSize =>
        {
          parts.Clear();
          Filter.AddFittings(parts, SystemType.PVC, mainSize, filterSize);

          if (filterSize == PartSize.SIZE_100)
          {
            Assert.That(parts["AF18017"].Quantity, Is.EqualTo(1));
            Assert.That(parts["AF18011"].Quantity, Is.EqualTo(1));

            if (mainSize == PartSize.SIZE_075)
            {
              Assert.That(parts["AF18013"].Quantity, Is.EqualTo(2));
            }
            else if (mainSize == PartSize.SIZE_100)
            {
              Assert.That(parts["AF18012"].Quantity, Is.EqualTo(2));
            }
            else if (mainSize == PartSize.SIZE_125)
            {
              Assert.That(parts["AF18012"].Quantity, Is.EqualTo(2));
              Assert.That(parts["FI125SSCOUP"].Quantity, Is.EqualTo(2));
            }
          }
          else
          {
            parts.Add($"FI{filterSize}STFA", 1);
            parts.Add($"FI{filterSize}TSMA", 1);
          }
        });
      });
    }

    [Test]
    public void AddFittings_POLY()
    {
      filterSizes.ForEach(filterSize =>
      {
        mainSizes.ForEach(mainSize =>
        {
          parts.Clear();
          Filter.AddFittings(parts, SystemType.POLY, mainSize, filterSize);

          if (filterSize == PartSize.SIZE_100)
          {
            Assert.That(parts["AF18017"].Quantity, Is.EqualTo(1));
            Assert.That(parts["AF18011"].Quantity, Is.EqualTo(1));

            if (mainSize == PartSize.SIZE_075)
            {
              Assert.That(parts["AF18015"].Quantity, Is.EqualTo(2));
            }
            else if (mainSize == PartSize.SIZE_100)
            {
              Assert.That(parts["AF18014"].Quantity, Is.EqualTo(2));
            }
            else if (mainSize == PartSize.SIZE_125)
            {
              Assert.That(parts["AF18018"].Quantity, Is.EqualTo(2));
            }

            var hcCode = mainSize == PartSize.SIZE_125 ? "HC150" : $"HC{mainSize}";
            Assert.That(parts[hcCode].Quantity, Is.EqualTo(2));
          }
        });
      });
    }
  }
}
