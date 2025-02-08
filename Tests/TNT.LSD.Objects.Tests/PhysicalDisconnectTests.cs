using System.Diagnostics.CodeAnalysis;
using TNT.LSD.Inventory;
using TNT.LSD.Objects;

namespace TNTObjectsTests
{
  [ExcludeFromCodeCoverage]
  public class PhysicalDisconnectTests
  {
    [Test]
    public void SetPVCPartQuantities_075_125()
    {
      var parts = new CodedParts();
      var sizes = new List<PartSize>() { PartSize.SIZE_075, PartSize.SIZE_100, PartSize.SIZE_125 };

      sizes.ForEach(pipeSize =>
      {
        parts.Clear();
        PhysicalDisconnect.SetPVCPartQuantities(parts, pipeSize);
        Assert.That(parts["PD2"].Quantity, Is.EqualTo(1));
        if (pipeSize != PartSize.SIZE_125) Assert.That(parts["FI100TSMA"].Quantity, Is.EqualTo(2));
        Assert.That(parts["FI100STFA"].Quantity, Is.EqualTo(1));
        Assert.That(parts["BV100FT"].Quantity, Is.EqualTo(2));
        Assert.That(parts["PD100CTADAPTER"].Quantity, Is.EqualTo(2));
        Assert.That(parts["PD100CAMCAP"].Quantity, Is.EqualTo(1));
        Assert.That(parts["VBJUMBO"].Quantity, Is.EqualTo(1));

        if (pipeSize == PartSize.SIZE_075) Assert.That(parts["FI100X075SSRB"].Quantity, Is.EqualTo(3));

        if (pipeSize == PartSize.SIZE_125)
        {
          Assert.That(parts["FI125SSCOUP"].Quantity, Is.EqualTo(3));
          Assert.That(parts["FI125X100SSRB"].Quantity, Is.EqualTo(3));
          Assert.That(parts["NI100X4TOE"].Quantity, Is.EqualTo(2));
        }
      });
    }
    [Test]
    public void SetPVCPartQuantities_150_200()
    {
      var parts = new CodedParts();
      var sizes = new List<PartSize>() { PartSize.SIZE_150, PartSize.SIZE_200 };

      sizes.ForEach(pipeSize =>
      {
        parts.Clear();
        PhysicalDisconnect.SetPVCPartQuantities(parts, pipeSize);
        Assert.That(parts[$"PD{pipeSize}HOSE"].Quantity, Is.EqualTo(2));
        Assert.That(parts[$"PD{pipeSize}IFCAMLOCK"].Quantity, Is.EqualTo(1));
        Assert.That(parts[$"PD{pipeSize}MMCAMLOCK"].Quantity, Is.EqualTo(2));
        Assert.That(parts[$"PD{pipeSize}CAMCAP"].Quantity, Is.EqualTo(1));
        Assert.That(parts[$"BV{pipeSize}BRONZE"].Quantity, Is.EqualTo(2));
        Assert.That(parts[$"NI{pipeSize}X4TOE"].Quantity, Is.EqualTo(2));
        Assert.That(parts[$"FI{pipeSize}SSCOUP"].Quantity, Is.EqualTo(2));

        if (pipeSize == PartSize.SIZE_150)
        {
          Assert.That(parts[$"FI{pipeSize}TSMA"].Quantity, Is.EqualTo(1));
          Assert.That(parts["VBJUMBO"].Quantity, Is.EqualTo(1));
        }
        else
        {
          Assert.That(parts["PF200BTMA"].Quantity, Is.EqualTo(1));
          Assert.That(parts[$"FI{pipeSize}STFA"].Quantity, Is.EqualTo(1));
          Assert.That(parts["VBGIANT"].Quantity, Is.EqualTo(1));
        }
      });
    }

    [Test]
    public void SetPOLYPartQuantities_075()
    {
      var parts = new CodedParts();

      PhysicalDisconnect.SetPOLYPartQuantities(parts, PartSize.SIZE_075);
      Assert.That(parts["PD2"].Quantity, Is.EqualTo(1));
      Assert.That(parts["BV100FT"].Quantity, Is.EqualTo(2));
      Assert.That(parts["PD100CTADAPTER"].Quantity, Is.EqualTo(2));
      Assert.That(parts["PD100CAMCAP"].Quantity, Is.EqualTo(1));
      Assert.That(parts["VBJUMBO"].Quantity, Is.EqualTo(1));

      Assert.That(parts["FI100TTCOUP"].Quantity, Is.EqualTo(1));
      Assert.That(parts["FI100X075TTRB"].Quantity, Is.EqualTo(3));
      Assert.That(parts["PF075BTMA"].Quantity, Is.EqualTo(3));
      Assert.That(parts["HC075"].Quantity, Is.EqualTo(3));
    }

    [Test]
    public void SetPOLYPartQuantities_100()
    {
      var parts = new CodedParts();

      PhysicalDisconnect.SetPOLYPartQuantities(parts, PartSize.SIZE_100);
      Assert.That(parts["PD2"].Quantity, Is.EqualTo(1));
      Assert.That(parts["BV100FT"].Quantity, Is.EqualTo(2));
      Assert.That(parts["PD100CTADAPTER"].Quantity, Is.EqualTo(2));
      Assert.That(parts["PD100CAMCAP"].Quantity, Is.EqualTo(1));
      Assert.That(parts["VBJUMBO"].Quantity, Is.EqualTo(1));

      Assert.That(parts["PF100BTMA"].Quantity, Is.EqualTo(2));
      Assert.That(parts["PF100BTFA"].Quantity, Is.EqualTo(1));
      Assert.That(parts["HC100"].Quantity, Is.EqualTo(3));
    }

    [Test]
    public void SetPOLYPartQuantities_125()
    {
      var parts = new CodedParts();

      PhysicalDisconnect.SetPOLYPartQuantities(parts, PartSize.SIZE_125);
      Assert.That(parts["PD2"].Quantity, Is.EqualTo(1));
      Assert.That(parts["BV100FT"].Quantity, Is.EqualTo(2));
      Assert.That(parts["PD100CTADAPTER"].Quantity, Is.EqualTo(2));
      Assert.That(parts["PD100CAMCAP"].Quantity, Is.EqualTo(1));
      Assert.That(parts["VBJUMBO"].Quantity, Is.EqualTo(1));

      Assert.That(parts["NI100X2"].Quantity, Is.EqualTo(2));
      Assert.That(parts["FI125X100TTRB"].Quantity, Is.EqualTo(3));
      Assert.That(parts["PF125BTFA"].Quantity, Is.EqualTo(3));
      Assert.That(parts["HC150"].Quantity, Is.EqualTo(3));
    }
  }
}
