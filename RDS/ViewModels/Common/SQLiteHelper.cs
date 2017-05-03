using System.Data;
using System.Data.SQLite;

namespace RDS.ViewModels.Common
{
	public class SQLiteHelper
	{
		private string connectionString = string.Empty;

		public SQLiteHelper(string connectionString )
		{
			this.connectionString = connectionString;//$"Data Source=C:\\RDS_Database\\Rds.db;Pooling=true;FailIfMissing=false";
		}

		public DataTable GetResultTable(string cmdString)
		{
			DataTable resultDataTable = new DataTable();
			DataRow dataRow;
			using (SQLiteConnection connection = new SQLiteConnection(this.connectionString))
			{
				connection.Open();
				using (SQLiteCommand command = connection.CreateCommand())
				{
					command.CommandText = cmdString;
					using (SQLiteDataReader sqliteDataReader = command.ExecuteReader())
					{
						int size = sqliteDataReader.FieldCount;
						for (int i = 0; i < size; i++)
						{
							DataColumn dataColumn;
							dataColumn = new DataColumn(sqliteDataReader.GetName(i), sqliteDataReader.GetName(i).GetType());
							resultDataTable.Columns.Add(dataColumn);
						}

						while (sqliteDataReader.Read())
						{
							dataRow = resultDataTable.NewRow();
							for (int i = 0; i < size; i++)
							{
								dataRow[sqliteDataReader.GetName(i)] = sqliteDataReader[sqliteDataReader.GetName(i)].ToString();
							}
							resultDataTable.Rows.Add(dataRow);
						}
					}
				}
			}
			return resultDataTable;
		}

		public DataTable GetSchema()
		{
			using (SQLiteConnection connection = new SQLiteConnection(connectionString))
			{
				connection.Open();
				DataTable data = connection.GetSchema("TABLES");
				connection.Close();
				return data;
			}
		}

	}
}

