using Microsoft.Data.Sqlite;

namespace TNT.LSD.Inventory.DAL;

/// <summary>
/// Base class for all DAL classes. Implements methods for accessing SQLite database
/// </summary>
public class DALBase(string connectionString)
{
  protected string connectionString = connectionString;

  /// <summary>
  /// Returns a SQLite Connection associated with the connection string
  /// </summary>
  public SqliteConnection GetConnection()
  {
    SqliteConnection connection = new SqliteConnection(connectionString);
    connection.Open();
    return connection;
  }
}
