using System.Diagnostics.CodeAnalysis;
using TNT.LSD.Inventory;
using TNT.LSD.Objects;

namespace TNTObjectsTests
{
  [ExcludeFromCodeCoverage]
  public class DripAdapterTests
  {
    [Test]
    public void AddPVCFittings_Single_Pipe()
    {
      var parts = new CodedParts();
      var sizes = new List<PartSize>() { PartSize.SIZE_075, PartSize.SIZE_100, PartSize.SIZE_125 };

      foreach (var pipeSize in sizes)
      {
        parts.Clear();
        DripAdapter.AddPVCFittings(parts, pipeSize, null);

        Assert.That(parts.Count, Is.EqualTo(4));
        Assert.That(parts[$"FI{pipeSize}X050ST90"].Quantity, Is.EqualTo(1));
        Assert.That(parts[$"FPSBE050"].Quantity, Is.EqualTo(1));
        Assert.That(parts[$"FUNNYPIPE"].Quantity, Is.EqualTo(1));
        Assert.That(parts[$"HRFIG8"].Quantity, Is.EqualTo(1));
      }
    }

    [Test]
    public void AddPVCFittings_Double_Pipe()
    {
      var parts = new CodedParts();
      var sizes = new List<PartSize>() { PartSize.SIZE_075, PartSize.SIZE_100, PartSize.SIZE_125, PartSize.SIZE_150, PartSize.SIZE_200 };

      foreach (var pipeSize in sizes)
      {
        parts.Clear();
        DripAdapter.AddPVCFittings(parts, pipeSize, pipeSize);

        Assert.That(parts.Count, Is.EqualTo(4));
        Assert.That(parts[$"FI{pipeSize}X050SSTTEE"].Quantity, Is.EqualTo(1));
        Assert.That(parts[$"FPSBE050"].Quantity, Is.EqualTo(1));
        Assert.That(parts[$"FUNNYPIPE"].Quantity, Is.EqualTo(1));
        Assert.That(parts[$"HRFIG8"].Quantity, Is.EqualTo(1));
      }
    }

    [Test]
    public void AddPOLYFittings_Single_Pipe()
    {
      var parts = new CodedParts();
      var sizes = new List<PartSize>() { PartSize.SIZE_075, PartSize.SIZE_100, PartSize.SIZE_125 };

      foreach (var pipeSize in sizes)
      {
        var hc = pipeSize == PartSize.SIZE_125 ? PartSize.SIZE_150 : pipeSize;

        parts.Clear();
        DripAdapter.AddPOLYFittings(parts, pipeSize, null);

        Assert.That(parts.Count, Is.EqualTo(5));
        Assert.That(parts[$"PF{pipeSize}X050BT90"].Quantity, Is.EqualTo(1));
        Assert.That(parts[$"FPSBE050"].Quantity, Is.EqualTo(1));
        Assert.That(parts[$"FUNNYPIPE"].Quantity, Is.EqualTo(1));
        Assert.That(parts[$"HRFIG8"].Quantity, Is.EqualTo(1));
        Assert.That(parts[$"HC{hc}"].Quantity, Is.EqualTo(1));
      }
    }

    [Test]
    public void AddPOLYFittings_Double_Pipe()
    {
      var parts = new CodedParts();
      var sizes = new List<PartSize>() { PartSize.SIZE_075, PartSize.SIZE_100, PartSize.SIZE_125 };

      foreach (var pipeSize in sizes)
      {
        parts.Clear();
        DripAdapter.AddPOLYFittings(parts, pipeSize, pipeSize);

        Assert.That(parts.Count, Is.EqualTo(4));
        Assert.That(parts[$"PF{pipeSize}HDSADDLE"].Quantity, Is.EqualTo(1));
        Assert.That(parts[$"FPSBE050"].Quantity, Is.EqualTo(1));
        Assert.That(parts[$"FUNNYPIPE"].Quantity, Is.EqualTo(1));
        Assert.That(parts[$"HRFIG8"].Quantity, Is.EqualTo(1));
      }
    }
  }
}
