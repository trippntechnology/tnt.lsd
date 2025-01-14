using System.Diagnostics.CodeAnalysis;

namespace TNT.LSD.Inventory.Tests;

[ExcludeFromCodeCoverage]
public class PartTests
{
  [Test]
  public void Constructor()
  {
    var part = new Part();
    Assert.That(part.Code, Is.Null);
    Assert.That(part.Description, Is.Null);
    Assert.That(part.Quantity, Is.EqualTo(0));
    Assert.That(part.Glueable, Is.False);
    Assert.That(part.ExternalPart, Is.Null);
  }

  [Test]
  public void Copy_Constructor()
  {
    var part = new Part()
    {
      Code = "code",
      Description = "description",
      Quantity = 7,
      Glueable = true,
      ExternalPart = new Part()
      {
        Code = "excode",
        Description = "exdescription",
      }
    };

    var sut = new Part(part);

    Assert.That(part.Code, Is.EqualTo("code"));
    Assert.That(part.Description, Is.EqualTo("description"));
    Assert.That(part.Quantity, Is.EqualTo(7));
    Assert.That(part.Glueable, Is.True);
    Assert.That(part.ExternalPart, Is.Not.Null);
    Assert.That(part.ExternalPart.Code, Is.EqualTo("excode"));
    Assert.That(part.ExternalPart.Description, Is.EqualTo("exdescription"));
  }

  [Test]
  public new void ToString()
  {
    var part = new Part()
    {
      Code = "code",
      Description = "description",
      Quantity = 7,
      ExternalPart = new Part()
      {
        Code = "excode",
        Description = "exdescription",
      }
    };

    Assert.That(part.ToString(), Is.EqualTo("(7) code: description"));
  }
}
