using System.Diagnostics.CodeAnalysis;
using TNT.LSD.Inventory;
using TNT.LSD.Objects;
using TNT.LSD.Settings;

namespace TNTObjectsTests
{
  [ExcludeFromCodeCoverage]
  public class ValveFittingsTests
  {
    private CodedParts parts;

    [SetUp]
    public void TestInitialize()
    {
      parts = new CodedParts();
    }

    [Test]
    public void AddInletParts_PVC_FIPT_075_100()
    {
      var threads = "FIPT";
      Valve.AddInletParts(parts, threads, PartSize.SIZE_075, 1, PartSize.SIZE_100, SystemType.PVC);

      Assert.That(parts.Count, Is.EqualTo(3));
      Assert.That(parts["FI100X075TTRB"].Quantity, Is.EqualTo(1));
      Assert.That(parts["NI075X4TOE"].Quantity, Is.EqualTo(1));
      Assert.That(parts["FI075SS90"].Quantity, Is.EqualTo(1));

      parts.Clear();
      Valve.AddInletParts(parts, threads, PartSize.SIZE_075, 2, PartSize.SIZE_100, SystemType.PVC);

      Assert.That(parts.Count, Is.EqualTo(3));
      Assert.That(parts["FI100X075TTRB"].Quantity, Is.EqualTo(1));
      Assert.That(parts["NI075X4TOE"].Quantity, Is.EqualTo(1));
      Assert.That(parts["FI075SSSTEE"].Quantity, Is.EqualTo(1));
    }

    [Test]
    public void AddInletParts_PVC_FIPT_075_150()
    {
      var threads = "FIPT";
      Valve.AddInletParts(parts, threads, PartSize.SIZE_075, 1, PartSize.SIZE_150, SystemType.PVC);

      Assert.That(parts.Count, Is.EqualTo(3));
      Assert.That(parts["FI150X075TTRB"].Quantity, Is.EqualTo(1));
      Assert.That(parts["NI075X4TOE"].Quantity, Is.EqualTo(1));
      Assert.That(parts["FI075SS90"].Quantity, Is.EqualTo(1));
    }

    [Test]
    public void AddInletParts_PVC_FIPT_100_100()
    {
      var threads = "FIPT";
      Valve.AddInletParts(parts, threads, PartSize.SIZE_100, 1, PartSize.SIZE_100, SystemType.PVC);

      Assert.That(parts.Count, Is.EqualTo(2));
      Assert.That(parts["NI100X4TOE"].Quantity, Is.EqualTo(1));
      Assert.That(parts["FI100SS90"].Quantity, Is.EqualTo(1));

      parts.Clear();
      Valve.AddInletParts(parts, threads, PartSize.SIZE_100, 2, PartSize.SIZE_100, SystemType.PVC);

      Assert.That(parts.Count, Is.EqualTo(2));
      Assert.That(parts["NI100X4TOE"].Quantity, Is.EqualTo(1));
      Assert.That(parts["FI100SSSTEE"].Quantity, Is.EqualTo(1));
    }

    [Test]
    public void AddInletParts_PVC_FIPT_125_100()
    {
      var threads = "FIPT";
      Valve.AddInletParts(parts, threads, PartSize.SIZE_125, 1, PartSize.SIZE_100, SystemType.PVC);

      Assert.That(parts.Count, Is.EqualTo(3));
      Assert.That(parts["NI100X4TOE"].Quantity, Is.EqualTo(1));
      Assert.That(parts["FI125SS90"].Quantity, Is.EqualTo(1));
      Assert.That(parts["FI125X100SSRB"].Quantity, Is.EqualTo(1));

      parts.Clear();
      Valve.AddInletParts(parts, threads, PartSize.SIZE_125, 2, PartSize.SIZE_100, SystemType.PVC);

      Assert.That(parts.Count, Is.EqualTo(3));
      Assert.That(parts["NI100X4TOE"].Quantity, Is.EqualTo(1));
      Assert.That(parts["FI125SSSTEE"].Quantity, Is.EqualTo(1));
      Assert.That(parts["FI125X100SSRB"].Quantity, Is.EqualTo(1));
    }

    [Test]
    public void AddInletParts_PVC_FIPT_150_100()
    {
      var threads = "FIPT";
      Valve.AddInletParts(parts, threads, PartSize.SIZE_150, 1, PartSize.SIZE_100, SystemType.PVC);

      Assert.That(parts.Count, Is.EqualTo(3));
      Assert.That(parts["NI100X4TOE"].Quantity, Is.EqualTo(1));
      Assert.That(parts["FI150SS90"].Quantity, Is.EqualTo(1));
      Assert.That(parts["FI150X100SSRB"].Quantity, Is.EqualTo(1));

      parts.Clear();
      Valve.AddInletParts(parts, threads, PartSize.SIZE_150, 2, PartSize.SIZE_100, SystemType.PVC);

      Assert.That(parts.Count, Is.EqualTo(3));
      Assert.That(parts["NI100X4TOE"].Quantity, Is.EqualTo(1));
      Assert.That(parts["FI150SSSTEE"].Quantity, Is.EqualTo(1));
      Assert.That(parts["FI150X100SSRB"].Quantity, Is.EqualTo(1));
    }

    [Test]
    public void AddInletParts_PVC_FIPT_150_150()
    {
      var threads = "FIPT";
      Valve.AddInletParts(parts, threads, PartSize.SIZE_150, 1, PartSize.SIZE_150, SystemType.PVC);

      Assert.That(parts.Count, Is.EqualTo(2));
      Assert.That(parts["NI150X4TOE"].Quantity, Is.EqualTo(1));
      Assert.That(parts["FI150SS90"].Quantity, Is.EqualTo(1));

      parts.Clear();
      Valve.AddInletParts(parts, threads, PartSize.SIZE_150, 2, PartSize.SIZE_150, SystemType.PVC);

      Assert.That(parts.Count, Is.EqualTo(2));
      Assert.That(parts["NI150X4TOE"].Quantity, Is.EqualTo(1));
      Assert.That(parts["FI150SSSTEE"].Quantity, Is.EqualTo(1));
    }

    [Test]
    public void AddInletParts_PVC_FIPT_200_150()
    {
      var threads = "FIPT";
      Valve.AddInletParts(parts, threads, PartSize.SIZE_200, 1, PartSize.SIZE_150, SystemType.PVC);

      Assert.That(parts.Count, Is.EqualTo(3));
      Assert.That(parts["NI150X4TOE"].Quantity, Is.EqualTo(1));
      Assert.That(parts["FI200SS90"].Quantity, Is.EqualTo(1));
      Assert.That(parts["FI200X150SSRB"].Quantity, Is.EqualTo(1));

      parts.Clear();
      Valve.AddInletParts(parts, threads, PartSize.SIZE_200, 2, PartSize.SIZE_150, SystemType.PVC);

      Assert.That(parts.Count, Is.EqualTo(3));
      Assert.That(parts["NI150X4TOE"].Quantity, Is.EqualTo(1));
      Assert.That(parts["FI200SSSTEE"].Quantity, Is.EqualTo(1));
      Assert.That(parts["FI200X150SSRB"].Quantity, Is.EqualTo(1));
    }

    [Test]
    public void AddInletParts_PVC_FIPT_200_200()
    {
      var threads = "FIPT";
      Valve.AddInletParts(parts, threads, PartSize.SIZE_200, 1, PartSize.SIZE_200, SystemType.PVC);

      Assert.That(parts.Count, Is.EqualTo(2));
      Assert.That(parts["NI200X4TOE"].Quantity, Is.EqualTo(1));
      Assert.That(parts["FI200SS90"].Quantity, Is.EqualTo(1));

      parts.Clear();
      Valve.AddInletParts(parts, threads, PartSize.SIZE_200, 2, PartSize.SIZE_200, SystemType.PVC);

      Assert.That(parts.Count, Is.EqualTo(2));
      Assert.That(parts["NI200X4TOE"].Quantity, Is.EqualTo(1));
      Assert.That(parts["FI200SSSTEE"].Quantity, Is.EqualTo(1));
    }

    [Test]
    public void AddInletParts_NULL_MAINSIZE()
    {
      var threads = "FIPT";
      Valve.AddInletParts(parts, threads, null, 1, PartSize.SIZE_200, SystemType.PVC);
      Assert.That(parts.Count, Is.EqualTo(0));
    }

    [Test]
    public void AddInletParts_PVC_MIPT_NOT_100_VALVE()
    {
      var threads = "MIPT";
      Assert.Throws<NotSupportedException>(() => Valve.AddInletParts(parts, threads, PartSize.SIZE_100, 1, PartSize.SIZE_150, SystemType.PVC));
    }

    [Test]
    public void AddInletParts_PVC_FIPT_075_MAIN()
    {
      var threads = "FIPT";
      Valve.AddInletParts(parts, threads, PartSize.SIZE_075, 1, PartSize.SIZE_100, SystemType.PVC);

      Assert.That(parts.Count, Is.EqualTo(3));
      Assert.That(parts["FI100X075TTRB"].Quantity, Is.EqualTo(1));
      Assert.That(parts["NI075X4TOE"].Quantity, Is.EqualTo(1));
      Assert.That(parts["FI075SS90"].Quantity, Is.EqualTo(1));

      parts.Clear();
      Valve.AddInletParts(parts, threads, PartSize.SIZE_075, 2, PartSize.SIZE_100, SystemType.PVC);

      Assert.That(parts.Count, Is.EqualTo(3));
      Assert.That(parts["FI100X075TTRB"].Quantity, Is.EqualTo(1));
      Assert.That(parts["NI075X4TOE"].Quantity, Is.EqualTo(1));
      Assert.That(parts["FI075SSSTEE"].Quantity, Is.EqualTo(1));
    }

    [Test]
    public void AddInletParts_PVC_FIPT_100_MAIN()
    {
      var threads = "FIPT";
      Valve.AddInletParts(parts, threads, PartSize.SIZE_100, 1, PartSize.SIZE_100, SystemType.PVC);

      Assert.That(parts.Count, Is.EqualTo(2));
      Assert.That(parts["NI100X4TOE"].Quantity, Is.EqualTo(1));
      Assert.That(parts["FI100SS90"].Quantity, Is.EqualTo(1));

      parts.Clear();
      Valve.AddInletParts(parts, threads, PartSize.SIZE_100, 2, PartSize.SIZE_100, SystemType.PVC);

      Assert.That(parts.Count, Is.EqualTo(2));
      Assert.That(parts["NI100X4TOE"].Quantity, Is.EqualTo(1));
      Assert.That(parts["FI100SSSTEE"].Quantity, Is.EqualTo(1));
    }

    [Test]
    public void AddInletParts_PVC_FIPT_GT_100_MAIN()
    {
      var threads = "FIPT";
      var mainSizes = new List<PartSize>() { PartSize.SIZE_125, PartSize.SIZE_150, PartSize.SIZE_200 };

      mainSizes.ForEach(ps =>
      {
        parts.Clear();
        Valve.AddInletParts(parts, threads, ps, 1, PartSize.SIZE_100, SystemType.PVC);

        Assert.That(parts.Count, Is.EqualTo(3));
        Assert.That(parts["NI100X4TOE"].Quantity, Is.EqualTo(1));
        Assert.That(parts[$"FI{ps}SS90"].Quantity, Is.EqualTo(1));
        Assert.That(parts[$"FI{ps}X{100}SSRB"].Quantity, Is.EqualTo(1));

        parts.Clear();
        Valve.AddInletParts(parts, threads, ps, 2, PartSize.SIZE_100, SystemType.PVC);

        Assert.That(parts.Count, Is.EqualTo(3));
        Assert.That(parts["NI100X4TOE"].Quantity, Is.EqualTo(1));
        Assert.That(parts[$"FI{ps}SSSTEE"].Quantity, Is.EqualTo(1));
        Assert.That(parts[$"FI{ps}X{100}SSRB"].Quantity, Is.EqualTo(1));
      });
    }

    [Test]
    public void AddOutletParts_PVC_FIPT()
    {
      var sizes = new Dictionary<PartSize, List<PartSize>>() {
        { PartSize.SIZE_100, new List<PartSize>(){PartSize.SIZE_075, PartSize.SIZE_100, PartSize.SIZE_125 } },
        { PartSize.SIZE_150, new List<PartSize>() { PartSize.SIZE_075, PartSize.SIZE_100, PartSize.SIZE_125, PartSize.SIZE_150 } },
        { PartSize.SIZE_200, new List<PartSize>() { PartSize.SIZE_075, PartSize.SIZE_100, PartSize.SIZE_125, PartSize.SIZE_150, PartSize.SIZE_200 }},
      };
      var threads = "FIPT";

      foreach (var pair in sizes)
      {
        var valveSize = pair.Key;
        pair.Value.ForEach(pipeSize =>
        {
          parts.Clear();
          Valve.AddOutletParts(parts, threads, valveSize, pipeSize, SystemType.PVC);

          Assert.That(parts.Count, Is.EqualTo(valveSize == pipeSize ? 2 : 3));
          Assert.That(parts[$"NI{valveSize}X4TOE"].Quantity, Is.EqualTo(1));

          if (valveSize > pipeSize)
          {
            Assert.That(parts[$"FI{valveSize}SSCOUP"].Quantity, Is.EqualTo(1));
            Assert.That(parts[$"FI{valveSize}X{pipeSize}SSRB"].Quantity, Is.EqualTo(1));
          }
          else if (valveSize < pipeSize)
          {
            Assert.That(parts[$"FI{pipeSize}SSCOUP"].Quantity, Is.EqualTo(1));
            Assert.That(parts[$"FI{pipeSize}X{valveSize}SSRB"].Quantity, Is.EqualTo(1));
          }
          else
          {
            Assert.That(parts[$"FI{valveSize}SSCOUP"].Quantity, Is.EqualTo(1));
          }
        });
      }
    }

    [Test]
    public void AddOutletParts_PVC_MIPT()
    {
      var sizes = new Dictionary<PartSize, List<PartSize>>() {
        { PartSize.SIZE_100, new List<PartSize>(){PartSize.SIZE_075, PartSize.SIZE_100, PartSize.SIZE_125 } },
      };
      var threads = "MIPT";

      foreach (var pair in sizes)
      {
        var valveSize = pair.Key;
        pair.Value.ForEach(pipeSize =>
        {
          parts.Clear();
          Valve.AddOutletParts(parts, threads, valveSize, pipeSize, SystemType.PVC);

          Assert.That(parts.Count, Is.EqualTo(valveSize == pipeSize ? 1 : 2));

          if (valveSize > pipeSize)
          {
            Assert.That(parts[$"FI{valveSize}STFA"].Quantity, Is.EqualTo(1));
            Assert.That(parts[$"FI{valveSize}X{pipeSize}SSRB"].Quantity, Is.EqualTo(1));
          }
          else if (valveSize < pipeSize)
          {
            Assert.That(parts[$"FI{pipeSize}SSCOUP"].Quantity, Is.EqualTo(1));
            Assert.That(parts[$"FI{pipeSize}X{valveSize}STRB"].Quantity, Is.EqualTo(1));
          }
          else
          {
            Assert.That(parts[$"FI{valveSize}STFA"].Quantity, Is.EqualTo(1));
          }
        });
      }
    }

    [Test]
    public void AddInletParts_POLY_Invalid_Valve_Size()
    {
      Assert.Throws<NotSupportedException>(() => Valve.AddInletParts(parts, "FIPT", PartSize.SIZE_100, 1, PartSize.SIZE_150, SystemType.POLY));
    }

    [Test]
    public void AddInletParts_POLY_Invalid_Pipe_Size()
    {
      Assert.Throws<NotSupportedException>(() => Valve.AddInletParts(parts, "FIPT", PartSize.SIZE_150, 1, PartSize.SIZE_100, SystemType.POLY));
    }

    [Test]
    public void AddInletParts_POLY_FIPT_100()
    {
      var threads = "FIPT";
      var valveSize = PartSize.SIZE_100;
      var mainSizes = new List<PartSize>() { PartSize.SIZE_075, PartSize.SIZE_100, PartSize.SIZE_125 };

      mainSizes.ForEach(mainSize =>
      {
        var smallestSize = valveSize < mainSize ? valveSize : mainSize;
        var largestSize = valveSize > mainSize ? valveSize : mainSize;
        var hcCode = mainSize == PartSize.SIZE_125 ? PartSize.SIZE_150 : mainSize;

        parts.Clear();
        Valve.AddInletParts(parts, threads, mainSize, 1, PartSize.SIZE_100, SystemType.POLY);

        Assert.That(parts.Count, Is.EqualTo(smallestSize != largestSize ? 4 : 3));
        Assert.That(parts[$"NI{smallestSize}X4"].Quantity, Is.EqualTo(1));
        Assert.That(parts[$"PF{mainSize}BT90"].Quantity, Is.EqualTo(1));
        if (smallestSize != largestSize)
        {
          Assert.That(parts[$"FI{largestSize}X{smallestSize}TTRB"].Quantity, Is.EqualTo(1));
        }
        Assert.That(parts[$"HC{hcCode}"].Quantity, Is.EqualTo(1));

        parts.Clear();
        Valve.AddInletParts(parts, threads, mainSize, 2, PartSize.SIZE_100, SystemType.POLY);

        Assert.That(parts.Count, Is.EqualTo(smallestSize != largestSize ? 4 : 3));
        Assert.That(parts[$"NI{smallestSize}X4"].Quantity, Is.EqualTo(1));
        Assert.That(parts[$"PF{mainSize}BBTTEE"].Quantity, Is.EqualTo(1));
        if (smallestSize != largestSize)
        {
          Assert.That(parts[$"FI{largestSize}X{smallestSize}TTRB"].Quantity, Is.EqualTo(1));
        }
        Assert.That(parts[$"HC{hcCode}"].Quantity, Is.EqualTo(2));
      });
    }

    [Test]
    public void AddOutletParts_POLY_FIPT_100()
    {
      var threads = "FIPT";
      var valveSize = PartSize.SIZE_100;
      var mainSizes = new List<PartSize>() { PartSize.SIZE_075, PartSize.SIZE_100, PartSize.SIZE_125 };
      var partCounts = new List<int>() { 4, 2, 4 };

      mainSizes.ForEach(pipeSize =>
      {
        var smallestSize = valveSize < pipeSize ? valveSize : pipeSize;
        var largestSize = valveSize > pipeSize ? valveSize : pipeSize;

        parts.Clear();
        Valve.AddOutletParts(parts, threads, valveSize, pipeSize, SystemType.POLY);

        Assert.That(parts.Count, Is.EqualTo(partCounts[mainSizes.IndexOf(pipeSize)]));
        Assert.That(parts[$"PF{valveSize}BTMA"].Quantity, Is.EqualTo(1));
        if (smallestSize != largestSize)
        {
          Assert.That(parts[$"PF{largestSize}X{smallestSize}BBRB"].Quantity, Is.EqualTo(1));
        }

        if (pipeSize == PartSize.SIZE_075)
        {
          Assert.That(parts[$"HC{SizeToHoseClamp(valveSize).Code}"].Quantity, Is.EqualTo(2));
          Assert.That(parts[$"HC{SizeToHoseClamp(PartSize.SIZE_075).Code}"].Quantity, Is.EqualTo(1));
        }
        else if (pipeSize == PartSize.SIZE_125)
        {
          Assert.That(parts[$"HC{SizeToHoseClamp(valveSize).Code}"].Quantity, Is.EqualTo(2));
          Assert.That(parts[$"HC{SizeToHoseClamp(PartSize.SIZE_125).Code}"].Quantity, Is.EqualTo(1));
        }
        else
        {
          Assert.That(parts[$"HC{SizeToHoseClamp(largestSize).Code}"].Quantity, Is.EqualTo(1));
        }
      });
    }

    [Test]
    public void AddOutletParts_POLY_Unsupported_Valve()
    {
      Assert.Throws<NotSupportedException>(() => Valve.AddOutletParts(parts, "FIPT", PartSize.SIZE_150, PartSize.SIZE_100, SystemType.POLY));
    }


    [Test]
    public void AddOutletParts_POLY_Unsupported_Pipe_Size()
    {
      Assert.Throws<NotSupportedException>(() => Valve.AddOutletParts(parts, "FIPT", PartSize.SIZE_100, PartSize.SIZE_150, SystemType.POLY));
    }


    [Test]
    public void AddOutletParts_POLY_MIPT_100()
    {
      var threads = "MIPT";
      var valveSize = PartSize.SIZE_100;
      var mainSizes = new List<PartSize>() { PartSize.SIZE_075, PartSize.SIZE_100, PartSize.SIZE_125 };
      var partCounts = new List<int>() { 4, 2, 4 };

      mainSizes.ForEach(pipeSize =>
      {
        var smallestSize = valveSize < pipeSize ? valveSize : pipeSize;
        var largestSize = valveSize > pipeSize ? valveSize : pipeSize;

        parts.Clear();
        Valve.AddOutletParts(parts, threads, valveSize, pipeSize, SystemType.POLY);

        Assert.That(parts.Count, Is.EqualTo(partCounts[mainSizes.IndexOf(pipeSize)]));
        Assert.That(parts[$"PF{valveSize}BTFA"].Quantity, Is.EqualTo(1));
        if (smallestSize != largestSize)
        {
          Assert.That(parts[$"PF{largestSize}X{smallestSize}BBRB"].Quantity, Is.EqualTo(1));
        }

        if (pipeSize == PartSize.SIZE_075)
        {
          Assert.That(parts[$"HC{SizeToHoseClamp(valveSize).Code}"].Quantity, Is.EqualTo(2));
          Assert.That(parts[$"HC{SizeToHoseClamp(PartSize.SIZE_075).Code}"].Quantity, Is.EqualTo(1));
        }
        else if (pipeSize == PartSize.SIZE_125)
        {
          Assert.That(parts[$"HC{SizeToHoseClamp(valveSize).Code}"].Quantity, Is.EqualTo(2));
          Assert.That(parts[$"HC{SizeToHoseClamp(PartSize.SIZE_125).Code}"].Quantity, Is.EqualTo(1));
        }
        else
        {
          Assert.That(parts[$"HC{SizeToHoseClamp(largestSize).Code}"].Quantity, Is.EqualTo(1));
        }
      });
    }

    private PartSize SizeToHoseClamp(PartSize partSize) => partSize == PartSize.SIZE_125 ? PartSize.SIZE_150 : partSize;
  }
}