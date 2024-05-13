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

    Assert.AreEqual(0, DAL.DALPart.GetPartsFromCodes(null).Count);
    Assert.AreEqual(0, DAL.DALPart.GetPartsFromCodes(new List<string>()).Count);

    List<Part> parts = DAL.DALPart.GetPartsFromCodes(codes);

    Assert.AreEqual(5, parts.Count);

    Part part = parts.Find(p => p.Code == "AF18004");

    Assert.IsNotNull(part);
    Assert.AreEqual("AF18004", part.Code);
    Assert.AreEqual("1\" QUAD. MANIFOLD", part.Description);
    Assert.AreEqual("AF-18004", part.ExternalPart.Code);
    Assert.AreEqual("QUAD. MANIFOLD", part.ExternalPart.Description);
  }

  [Test]
  public void GetPartsTests()
  {
    int currentRecordCount = GetPartsCount();
    Dictionary<string, Part> parts = DAL.DALPart.GetParts();

    Assert.AreEqual(currentRecordCount, parts.Count);

    foreach (string key in parts.Keys)
    {
      Assert.AreEqual(key, parts[key].Code);
    }
  }

  [Test]
  public void GetDescriptionsTest()
  {
    string[] codes = { "NI050X12;HUPROS00", "NI050X24;HUPROS00", "RA1804" };

    List<string> descriptions = DAL.DALPart.GetDescriptions(new List<string>(codes));

    Assert.AreEqual("1/2\" X 12\" SCHEDULE 80 NIPPLE;HUNTER PRO-SPRAY SHRUB ADAPTER", descriptions[0]);
    Assert.AreEqual("1/2\" X 24\" SCHEDULE 80 NIPPLE;HUNTER PRO-SPRAY SHRUB ADAPTER", descriptions[1]);
    Assert.AreEqual("4\" RAINBIRD 1800 POP-UP", descriptions[2]);
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
