using System.Diagnostics.CodeAnalysis;
using TNT.LSD.Inventory;
using TNT.LSD.Settings;

namespace TNT.LSD.Components.Tests;

[ExcludeFromCodeCoverage]
public class TNTCADTests
{
  private const string PI075 = "PI075";
  private const string PI100 = "PI100";
  private const string POLY075 = "POLY075";
  private const string POLY100 = "POLY100";

  [Test]
  public void AddDrains_NoPipe()
  {
    var sut = new CodedParts();

    TNTCAD.AddDrains(sut, 7, SystemType.PVC);
    Assert.That(sut.Count, Is.EqualTo(0));
  }

  [Test]
  public void AddDrains_34Pipe()
  {
    var sut = new CodedParts();
    sut.Add(PI075, 100);
    sut.Add(POLY075, 0);
    TNTCAD.AddDrains(sut, 6, SystemType.PVC);
    Assert.That(sut.Count, Is.EqualTo(4));
    Assert.That(sut["FI075X050SSTTEE"].Quantity, Is.EqualTo(6));
    Assert.That(sut["KD22"].Quantity, Is.EqualTo(6));
  }

  [Test]
  public void AddDrains_34and1Pipe()
  {
    var sut = new CodedParts();
    sut.Add(PI075, 100);
    sut.Add(PI100, 100);
    TNTCAD.AddDrains(sut, 6, SystemType.PVC);
    Assert.That(sut.Count, Is.EqualTo(5));
    Assert.That(sut["FI075X050SSTTEE"].Quantity, Is.EqualTo(3));
    Assert.That(sut["FI100X050SSTTEE"].Quantity, Is.EqualTo(3));
    Assert.That(sut["KD22"].Quantity, Is.EqualTo(6));
  }

  [Test]
  public void AddDrains_34Poly()
  {
    var sut = new CodedParts();
    sut.Add(PI075, 0);
    sut.Add(POLY075, 100);
    TNTCAD.AddDrains(sut, 6, SystemType.POLY);
    Assert.That(sut.Count, Is.EqualTo(5));
    Assert.That(sut["PF075X050BBTTEE"].Quantity, Is.EqualTo(6));
    Assert.That(sut["HC075"].Quantity, Is.EqualTo(12));
    Assert.That(sut["KD22"].Quantity, Is.EqualTo(6));
  }

  [Test]
  public void AddDrains_34and1Poly()
  {
    var sut = new CodedParts();
    sut.Add(POLY075, 100);
    sut.Add(POLY100, 100);
    TNTCAD.AddDrains(sut, 6, SystemType.POLY);
    Assert.That(sut.Count, Is.EqualTo(7));
    Assert.That(sut["PF075X050BBTTEE"].Quantity, Is.EqualTo(3));
    Assert.That(sut["PF100X050BBTTEE"].Quantity, Is.EqualTo(3));
    Assert.That(sut["HC075"].Quantity, Is.EqualTo(6));
    Assert.That(sut["HC100"].Quantity, Is.EqualTo(6));
    Assert.That(sut["KD22"].Quantity, Is.EqualTo(6));
  }
}
