using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using TNT.Commons;

namespace TNT.LSD.Inventory.DAL;

/// <summary>
/// Base class for all DAL classes. Implements methods for accessing SQLite database
/// </summary>
public class DALBase
{
  /// <summary>
  /// Connection string
  /// </summary>
  protected static string? m_ConnectionString;

  /// <summary>
  /// Initializes the connection string if empty and returns it
  /// </summary>
  public static string? ConnectionString
  {
    get
    {
      if (string.IsNullOrEmpty(m_ConnectionString))
      {
        // Build a config object, using env vars and JSON providers.

        IConfigurationRoot config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        // Get values from the config given their key and their target type.
        ConnectionStrings? connectionStrings = config.GetRequiredSection("ConnectionStrings").Get<ConnectionStrings>();
        m_ConnectionString = connectionStrings?.Inventory;
      }

      return m_ConnectionString;
    }
  }

  /// <summary>
  /// Returns a SQLite Connection associated with the connection string
  /// </summary>
  public static SqliteConnection? Connection
  {
    get
    {
      SqliteConnection conn = new SqliteConnection(ConnectionString);
      conn.Open();

      return ConnectionString?.let(it =>
      {
        SqliteConnection conn = new SqliteConnection(ConnectionString);
        conn.Open();
        return conn;
      });
    }
  }
}
