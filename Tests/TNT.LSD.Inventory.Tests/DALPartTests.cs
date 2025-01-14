using Microsoft.Data.Sqlite;
using System.Diagnostics.CodeAnalysis;

namespace TNT.LSD.Inventory.Tests;

[ExcludeFromCodeCoverage]
public class DALPartTests
{
  [Test]
  public void GetPartsFromCodesTests()
  {
    List<string> codes = new List<string>();

    for (int i = 4; i > -1; i--)
    {
      codes.Add(string.Concat("AF1800", i));
    }

    codes.Add("bogus");

    Assert.That(DAL.DALPart.GetPartsFromCodes(null).Count, Is.EqualTo(0));
    Assert.That(DAL.DALPart.GetPartsFromCodes(new List<string>()).Count, Is.EqualTo(0));

    List<Part> parts = DAL.DALPart.GetPartsFromCodes(codes);

    Assert.That(parts.Count, Is.EqualTo(5));

    Part part = parts.Find(p => p.Code == "AF18004");

    Assert.That(part, Is.Not.Null);
    Assert.That(part.Code, Is.EqualTo("AF18004"));
    Assert.That(part.Description, Is.EqualTo("1\" QUAD. MANIFOLD"));
    Assert.That(part.ExternalPart.Code, Is.EqualTo("AF-18004"));
    Assert.That(part.ExternalPart.Description, Is.EqualTo("QUAD. MANIFOLD"));
  }

  [Test]
  public void GetPartsTests()
  {
    int currentRecordCount = GetPartsCount();
    Dictionary<string, Part> parts = DAL.DALPart.GetParts();

    Assert.That(parts.Count, Is.EqualTo(currentRecordCount));

    foreach (string key in parts.Keys)
    {
      Assert.That(parts[key].Code, Is.EqualTo(key));
    }
  }

  [Test]
  public void GetDescriptionsTest()
  {
    string[] codes = { "NI050X12;HUPROS00", "NI050X24;HUPROS00", "RA1804" };

    List<string> descriptions = DAL.DALPart.GetDescriptions(new List<string>(codes));

    Assert.That(descriptions[0], Is.EqualTo("1/2\" X 12\" SCHEDULE 80 NIPPLE;HUNTER PRO-SPRAY SHRUB ADAPTER"));
    Assert.That(descriptions[1], Is.EqualTo("1/2\" X 24\" SCHEDULE 80 NIPPLE;HUNTER PRO-SPRAY SHRUB ADAPTER"));
    Assert.That(descriptions[2], Is.EqualTo("4\" RAINBIRD 1800 POP-UP"));
  }

  #region Private

  private int GetPartsCount()
  {
    var connectionString = DAL.DALBase.ConnectionString;
    if (connectionString == null) return -1;

    using (SqliteConnection conn = new SqliteConnection(DAL.DALBase.ConnectionString))
    using (SqliteCommand cmd = conn.CreateCommand())
    {
      conn.Open();
      cmd.CommandText = "select count(*) from InternalInventory";

      int count = Convert.ToInt32(cmd.ExecuteScalar());

      return count;
    }
  }
  #endregion
}
