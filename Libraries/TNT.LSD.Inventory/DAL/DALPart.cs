using Microsoft.Data.Sqlite;
using System.ComponentModel;
using System.Text;
using TNT.Commons;

namespace TNT.LSD.Inventory.DAL;

/// <summary>
/// DAL methods for getting parts
/// </summary>
public class DALPart : DALBase
{
  private static CodedParts? Parts { get; set; }

  /// <summary>
  /// Gets the descriptions associated with those listed in codes
  /// </summary>
  /// <param name="codes">List of codes. Codes that exist together may be listed in the same string
  /// separated by a semicolon</param>
  /// <returns>List of description associated to the codes</returns>
  public static List<string> GetDescriptions(List<string> codes)
  {
    List<string> descriptions = new List<string>();

    if (codes == null || codes.Count == 0)
    {
      return descriptions;
    }

    foreach (string code in codes)
    {
      string[] indCodes = code.Split(';');
      StringBuilder description = new StringBuilder();

      foreach (string indCode in indCodes)
      {
        if (indCode != indCodes.First())
        {
          description.Append(";");
        }

        description.Append(GetDescription(indCode));

      }

      descriptions.Add(description.ToString());
    }

    return descriptions;
  }

  /// <summary>
  /// Gets a description associated with a part code
  /// </summary>
  /// <param name="code">Part code</param>
  /// <returns>Description associated with a part code</returns>
  public static string GetDescription(string code)
  {
    Dictionary<string, Part> parts = GetParts();

    if (parts.ContainsKey(code))
    {
      return parts[code].Description ?? string.Empty;
    }

    return string.Empty;
  }

  /// <summary>
  /// Get a listing of descriptions associated with the codes provided
  /// </summary>
  /// <param name="codes">List of codes</param>
  /// <returns>List of descriptions associated with the codes</returns>
  public static List<Part> GetPartsFromCodes(List<string> codes)
  {
    List<Part> parts = new List<Part>();

    if (codes == null || codes.Count == 0)
    {
      return parts;
    }

    Dictionary<string, Part> allParts = GetParts();

    parts = (from prop in allParts join c in codes on prop.Key equals c select prop.Value).ToList();

    return parts;
  }

  /// <summary>
  /// Gets a listing of all parts
  /// </summary>
  /// <returns>Dictionary of of all parts with the key being the part code</returns>
  public static CodedParts GetParts()
  {
    if (Parts == null && Connection != null)
    {
      Parts = new CodedParts();
      Part? part = null;

      using (SqliteConnection conn = Connection)
      using (SqliteCommand cmd = conn.CreateCommand())
      {
        StringBuilder sql = new StringBuilder();

        sql.AppendLine("SELECT ii.InternalID Code, ii.Description, ii.Glueable, ei.ExternalID ExternalCode, ei.Description ExternalDescription");
        sql.AppendLine("  FROM InternalInventory ii");
        sql.AppendLine("    LEFT JOIN ExternalInventory ei ON ii.InternalID = ei.InternalID");
        sql.AppendLine("    Order by ii.InternalID");

        cmd.CommandText = sql.ToString();

        using (SqliteDataReader dr = cmd.ExecuteReader())
        {
          while (dr.Read())
          {
            part = FillPart(dr);

            if (part?.Code != null)
            {
              Parts.Add(part.Code, part);
            }
          }
        }
      }
    }

    // Return a copy
    CodedParts copyOfParts = new CodedParts();

    if (Parts != null)
    {
      foreach (string key in Parts.Keys)
      {
        copyOfParts.Add(key, new Part(Parts[key]));
      }
    }

    return copyOfParts;
  }

  public static BindingList<Part> GetPartBindingList(string? orderby = null)
  {
    BindingList<Part> parts = new BindingList<Part>();
    if (Connection == null) return parts;

    using (SqliteConnection conn = Connection)
    using (SqliteCommand cmd = conn.CreateCommand())
    {
      StringBuilder sql = new StringBuilder();

      sql.AppendLine("SELECT ii.InternalID Code, ii.Description, ei.ExternalID ExternalCode, ei.Description ExternalDescription");
      sql.AppendLine("  FROM InternalInventory ii");
      sql.AppendLine("    LEFT JOIN ExternalInventory ei ON ii.InternalID = ei.InternalID");

      if (orderby == "code")
      {
        sql.AppendLine("    order by ii.InternalID");
      }
      else if (orderby == "description")
      {
        sql.AppendLine("    order by ii.Description");
      }

      cmd.CommandText = sql.ToString();

      using (SqliteDataReader dr = cmd.ExecuteReader())
      {
        while (dr.Read())
        {
          FillPart(dr)?.also(it => parts.Add(it));
        }
      }
    }

    return parts;
  }

  /// <summary>
  /// Fills a Part with the contents of the data reader
  /// </summary>
  /// <param name="dr">Data reader</param>
  /// <returns>Part containing the contents of the data reader</returns>
  protected static Part? FillPart(SqliteDataReader dr)
  {
    if (dr.IsDBNull(0))
    {
      return null;
    }

    return new Part()
    {
      Code = dr["Code"]?.Let(it => it.ToString()) ?? string.Empty,
      Description = dr["Description"]?.Let(it => it.ToString()) ?? string.Empty,
      Glueable = dr.GetBoolean(2),
      ExternalPart = new Part()
      {
        Code = dr["ExternalCode"]?.Let(it => it.ToString()) ?? string.Empty,
        Description = dr["ExternalDescription"]?.Let(it => it.ToString()) ?? string.Empty,
      }
    };
  }
}
