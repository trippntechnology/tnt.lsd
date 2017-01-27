using System.Configuration;
using System.Data.SQLite;

namespace TNT.LSD.Inventory.DAL
{
	/// <summary>
	/// Base class for all DAL classes. Implements methods for accessing SQLite database
	/// </summary>
	public class DALBase
	{
		/// <summary>
		/// Connection string
		/// </summary>
		protected static string m_ConnectionString = string.Empty;

		/// <summary>
		/// Initializes the connection string if empty and returns it
		/// </summary>
		public static string ConnectionString 
		{
			get
			{
				if (string.IsNullOrEmpty(m_ConnectionString))
				{
					m_ConnectionString = ConfigurationManager.ConnectionStrings["SQLite"].ConnectionString;
				}

				return m_ConnectionString;
			}
		}

		/// <summary>
		/// Returns a SQLite Connection associated with the connection string
		/// </summary>
		public static SQLiteConnection Connection
		{
			get
			{
				SQLiteConnection conn = new SQLiteConnection(ConnectionString);
				conn.Open();

				return conn;
			}
		}
	}
}
