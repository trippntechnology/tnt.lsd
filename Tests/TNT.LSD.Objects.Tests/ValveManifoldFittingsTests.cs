using System.Diagnostics.CodeAnalysis;
using TNT.LSD.Inventory;
using TNT.LSD.Objects;
using TNT.LSD.Settings;

namespace TNTObjectsTests
{
  [ExcludeFromCodeCoverage]
  public class ValveManifoldFittingsTests
  {
    private CodedParts parts;

    [SetUp]
    public void TestInitialize()
    {
      parts = new CodedParts();
    }

    [Test]
    public void AddInletParts_Manifold_FIPT_Valve_Same_Size()
    {
      var inletThreads = "FIPT";

      Valve.AddInletParts(parts, inletThreads, PartSize.SIZE_100, Manifold.SINGLE_100);
      Valve.AddInletParts(parts, inletThreads, PartSize.SIZE_150, Manifold.SINGLE_150);
      Valve.AddInletParts(parts, inletThreads, PartSize.SIZE_200, Manifold.SINGLE_200);

      Assert.That(parts.Count, Is.EqualTo(3));
      Assert.That(parts["AF18010"].Quantity, Is.EqualTo(1));
      Assert.That(parts["AF18010150"].Quantity, Is.EqualTo(1));
      Assert.That(parts["AF18010200"].Quantity, Is.EqualTo(1));
    }

    [Test]
    public void AddInletParts_Manifold_FIPT_Valve100_Manifold150()
    {
      var inletThreads = "FIPT";
      Valve.AddInletParts(parts, inletThreads, PartSize.SIZE_100, Manifold.SINGLE_150);

      Assert.That(parts.Count, Is.EqualTo(2));
      Assert.That(parts["NI100X4TOE"].Quantity, Is.EqualTo(1));
      Assert.That(parts["AF18012150"].Quantity, Is.EqualTo(1));
    }

    [Test]
    public void AddInletParts_Manifold_FIPT_Valve100_Manifold200()
    {
      var inletThreads = "FIPT";
      Valve.AddInletParts(parts, inletThreads, PartSize.SIZE_100, Manifold.SINGLE_200);

      Assert.That(parts.Count, Is.EqualTo(3));
      Assert.That(parts["NI100X4TOE"].Quantity, Is.EqualTo(1));
      Assert.That(parts["AF18012200"].Quantity, Is.EqualTo(1));
      Assert.That(parts["FI150X100SSRB"].Quantity, Is.EqualTo(1));
    }

    [Test]
    public void AddInletParts_Manifold_FIPT_Valve150_Manifold200()
    {
      var inletThreads = "FIPT";
      Valve.AddInletParts(parts, inletThreads, PartSize.SIZE_150, Manifold.SINGLE_200);

      Assert.That(parts.Count, Is.EqualTo(2));
      Assert.That(parts["NI150X4TOE"].Quantity, Is.EqualTo(1));
      Assert.That(parts["AF18012200"].Quantity, Is.EqualTo(1));
    }

    [Test]
    public void AddInletParts_Manifold_MIPT_Valve100_Manifold100()
    {
      var inletThreads = "MIPT";
      Valve.AddInletParts(parts, inletThreads, PartSize.SIZE_100, Manifold.SINGLE_100);

      Assert.That(parts.Count, Is.EqualTo(1));
      Assert.That(parts["AF18016"].Quantity, Is.EqualTo(1));
    }

    [Test]
    public void AddInletParts_Manifold_MIPT_NotImplemented()
    {
      var inletThreads = "MIPT";
      Assert.Throws<NotImplementedException>(() => Valve.AddInletParts(parts, inletThreads, PartSize.SIZE_150, Manifold.SINGLE_100));
    }

    [Test]
    public void AddOnletParts_PVC_FIPT_075_100()
    {
      var outletThreads = "FIPT";
      Valve.AddOutletManifoldParts(parts, outletThreads, PartSize.SIZE_075, PartSize.SIZE_100, SystemType.PVC);

      Assert.That(parts.Count, Is.EqualTo(2));
      Assert.That(parts["AF18011"].Quantity, Is.EqualTo(1));
      Assert.That(parts["AF18013"].Quantity, Is.EqualTo(1));
    }

    [Test]
    public void AddOnletParts_PVC_FIPT_075_150()
    {
      var outletThreads = "FIPT";
      Valve.AddOutletManifoldParts(parts, outletThreads, PartSize.SIZE_075, PartSize.SIZE_150, SystemType.PVC);

      Assert.That(parts.Count, Is.EqualTo(3));
      Assert.That(parts["AF18011150"].Quantity, Is.EqualTo(1));
      Assert.That(parts["AF18012150"].Quantity, Is.EqualTo(1));
      Assert.That(parts["FI100X075SSRB"].Quantity, Is.EqualTo(1));
    }

    [Test]
    public void AddOnletParts_PVC_FIPT_075_200()
    {
      var outletThreads = "FIPT";
      Valve.AddOutletManifoldParts(parts, outletThreads, PartSize.SIZE_075, PartSize.SIZE_200, SystemType.PVC);

      Assert.That(parts.Count, Is.EqualTo(3));
      Assert.That(parts["AF18011200"].Quantity, Is.EqualTo(1));
      Assert.That(parts["AF18012200"].Quantity, Is.EqualTo(1));
      Assert.That(parts["FI150X075SSRB"].Quantity, Is.EqualTo(1));
    }

    [Test]
    public void AddOnletParts_PVC_FIPT_100_100()
    {
      var outletThreads = "FIPT";
      Valve.AddOutletManifoldParts(parts, outletThreads, PartSize.SIZE_100, PartSize.SIZE_100, SystemType.PVC);

      Assert.That(parts.Count, Is.EqualTo(2));
      Assert.That(parts["AF18011"].Quantity, Is.EqualTo(1));
      Assert.That(parts["AF18012"].Quantity, Is.EqualTo(1));
    }

    [Test]
    public void AddOnletParts_PVC_FIPT_100_150()
    {
      var outletThreads = "FIPT";
      Valve.AddOutletManifoldParts(parts, outletThreads, PartSize.SIZE_100, PartSize.SIZE_150, SystemType.PVC);

      Assert.That(parts.Count, Is.EqualTo(2));
      Assert.That(parts["AF18011150"].Quantity, Is.EqualTo(1));
      Assert.That(parts["AF18012150"].Quantity, Is.EqualTo(1));
    }

    [Test]
    public void AddOnletParts_PVC_FIPT_100_200()
    {
      var outletThreads = "FIPT";
      Valve.AddOutletManifoldParts(parts, outletThreads, PartSize.SIZE_100, PartSize.SIZE_200, SystemType.PVC);

      Assert.That(parts.Count, Is.EqualTo(3));
      Assert.That(parts["AF18011200"].Quantity, Is.EqualTo(1));
      Assert.That(parts["AF18012200"].Quantity, Is.EqualTo(1));
      Assert.That(parts["FI150X100SSRB"].Quantity, Is.EqualTo(1));
    }

    [Test]
    public void AddOnletParts_PVC_FIPT_125_100()
    {
      var outletThreads = "FIPT";
      Valve.AddOutletManifoldParts(parts, outletThreads, PartSize.SIZE_125, PartSize.SIZE_100, SystemType.PVC);

      Assert.That(parts.Count, Is.EqualTo(3));
      Assert.That(parts["AF18011"].Quantity, Is.EqualTo(1));
      Assert.That(parts["AF18012"].Quantity, Is.EqualTo(1));
      Assert.That(parts["FI125SSCOUP"].Quantity, Is.EqualTo(1));
    }

    [Test]
    public void AddOnletParts_PVC_FIPT_125_150()
    {
      var outletThreads = "FIPT";
      Valve.AddOutletManifoldParts(parts, outletThreads, PartSize.SIZE_125, PartSize.SIZE_150, SystemType.PVC);

      Assert.That(parts.Count, Is.EqualTo(4));
      Assert.That(parts["AF18011150"].Quantity, Is.EqualTo(1));
      Assert.That(parts["AF18012150"].Quantity, Is.EqualTo(1));
      Assert.That(parts["FI150SSCOUP"].Quantity, Is.EqualTo(1));
      Assert.That(parts["FI150X125SSRB"].Quantity, Is.EqualTo(1));
    }

    [Test]
    public void AddOnletParts_PVC_FIPT_125_200()
    {
      var outletThreads = "FIPT";
      Valve.AddOutletManifoldParts(parts, outletThreads, PartSize.SIZE_125, PartSize.SIZE_200, SystemType.PVC);

      Assert.That(parts.Count, Is.EqualTo(3));
      Assert.That(parts["AF18011200"].Quantity, Is.EqualTo(1));
      Assert.That(parts["AF18012200"].Quantity, Is.EqualTo(1));
      Assert.That(parts["FI150X125SSRB"].Quantity, Is.EqualTo(1));
    }

    [Test]
    public void AddOnletParts_PVC_FIPT_150_150()
    {
      var outletThreads = "FIPT";
      Valve.AddOutletManifoldParts(parts, outletThreads, PartSize.SIZE_150, PartSize.SIZE_150, SystemType.PVC);

      Assert.That(parts.Count, Is.EqualTo(3));
      Assert.That(parts["AF18011150"].Quantity, Is.EqualTo(1));
      Assert.That(parts["AF18012150"].Quantity, Is.EqualTo(1));
      Assert.That(parts["FI150SSCOUP"].Quantity, Is.EqualTo(1));
    }

    [Test]
    public void AddOnletParts_PVC_FIPT_150_200()
    {
      var outletThreads = "FIPT";
      Valve.AddOutletManifoldParts(parts, outletThreads, PartSize.SIZE_150, PartSize.SIZE_200, SystemType.PVC);

      Assert.That(parts.Count, Is.EqualTo(2));
      Assert.That(parts["AF18011200"].Quantity, Is.EqualTo(1));
      Assert.That(parts["AF18012200"].Quantity, Is.EqualTo(1));
    }

    [Test]
    public void AddOnletParts_PVC_FIPT_200_200()
    {
      var outletThreads = "FIPT";
      Valve.AddOutletManifoldParts(parts, outletThreads, PartSize.SIZE_200, PartSize.SIZE_200, SystemType.PVC);

      Assert.That(parts.Count, Is.EqualTo(3));
      Assert.That(parts["AF18011200"].Quantity, Is.EqualTo(1));
      Assert.That(parts["AF18012200"].Quantity, Is.EqualTo(1));
      Assert.That(parts["FI200SSCOUP"].Quantity, Is.EqualTo(1));
    }

    [Test]
    public void AddOnletParts_PVC_MIPT_075_100()
    {
      var outletThreads = "MIPT";
      Valve.AddOutletManifoldParts(parts, outletThreads, PartSize.SIZE_075, PartSize.SIZE_100, SystemType.PVC);

      Assert.That(parts.Count, Is.EqualTo(2));
      Assert.That(parts["AF18017"].Quantity, Is.EqualTo(1));
      Assert.That(parts["AF18013"].Quantity, Is.EqualTo(1));
    }

    [Test]
    public void AddOnletParts_PVC_MIPT_100_100()
    {
      var outletThreads = "MIPT";
      Valve.AddOutletManifoldParts(parts, outletThreads, PartSize.SIZE_100, PartSize.SIZE_100, SystemType.PVC);

      Assert.That(parts.Count, Is.EqualTo(2));
      Assert.That(parts["AF18017"].Quantity, Is.EqualTo(1));
      Assert.That(parts["AF18012"].Quantity, Is.EqualTo(1));
    }

    [Test]
    public void AddOnletParts_PVC_MIPT_125_100()
    {
      var outletThreads = "MIPT";
      Valve.AddOutletManifoldParts(parts, outletThreads, PartSize.SIZE_125, PartSize.SIZE_100, SystemType.PVC);

      Assert.That(parts.Count, Is.EqualTo(3));
      Assert.That(parts["AF18017"].Quantity, Is.EqualTo(1));
      Assert.That(parts["AF18012"].Quantity, Is.EqualTo(1));
      Assert.That(parts["FI125SSCOUP"].Quantity, Is.EqualTo(1));
    }

    [Test]
    public void AddOnletParts_PVC_MIPT_075_150()
    {
      var outletThreads = "MIPT";
      Assert.Throws<NotSupportedException>(() => Valve.AddOutletManifoldParts(parts, outletThreads, PartSize.SIZE_075, PartSize.SIZE_150, SystemType.PVC));
    }

    [Test]
    public void AddOnletParts_POLY_FIPT_100()
    {
      var sizes = new Dictionary<PartSize, List<PartSize>>()
      {
        {PartSize.SIZE_100, new List<PartSize> { PartSize.SIZE_075,  PartSize.SIZE_100, PartSize.SIZE_125 } }
      };
      var insertCodes = new List<string> { "15", "14", "18" };
      var outletThreads = "FIPT";

      foreach (var pair in sizes)
      {
        var valveSize = pair.Key;
        pair.Value.ForEach(pipeSize =>
        {
          parts.Clear();
          Valve.AddOutletManifoldParts(parts, outletThreads, pipeSize, valveSize, SystemType.POLY);

          var insertCode = insertCodes[pair.Value.IndexOf(pipeSize)];
          var hcCode = pipeSize == PartSize.SIZE_125 ? PartSize.SIZE_150 : pipeSize;

          Assert.That(parts.Count, Is.EqualTo(3));
          Assert.That(parts["AF18011"].Quantity, Is.EqualTo(1));
          Assert.That(parts[$"AF180{insertCode}"].Quantity, Is.EqualTo(1));
          Assert.That(parts[$"HC{hcCode}"].Quantity, Is.EqualTo(1));
        });
      }
    }

    [Test]
    public void AddOnletParts_POLY_MIPT_100()
    {
      var sizes = new Dictionary<PartSize, List<PartSize>>()
      {
        {PartSize.SIZE_100, new List<PartSize> { PartSize.SIZE_075,  PartSize.SIZE_100, PartSize.SIZE_125 } }
      };
      var insertCodes = new List<string> { "15", "14", "18" };
      var outletThreads = "MIPT";

      foreach (var pair in sizes)
      {
        var valveSize = pair.Key;
        pair.Value.ForEach(pipeSize =>
        {
          parts.Clear();
          Valve.AddOutletManifoldParts(parts, outletThreads, pipeSize, valveSize, SystemType.POLY);

          var insertCode = insertCodes[pair.Value.IndexOf(pipeSize)];
          var hcCode = pipeSize == PartSize.SIZE_125 ? PartSize.SIZE_150 : pipeSize;

          Assert.That(parts.Count, Is.EqualTo(3));
          Assert.That(parts["AF18017"].Quantity, Is.EqualTo(1));
          Assert.That(parts[$"AF180{insertCode}"].Quantity, Is.EqualTo(1));
          Assert.That(parts[$"HC{hcCode}"].Quantity, Is.EqualTo(1));
        });
      }
    }

    [Test]
    public void AddOnletParts_POLY_FIPT_150()
    {
      Assert.Throws<NotSupportedException>(() => Valve.AddOutletManifoldParts(parts, "FIPT", PartSize.SIZE_100, PartSize.SIZE_150, SystemType.POLY));
    }
  }
}
