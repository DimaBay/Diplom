using MySql.Data.MySqlClient;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace inventory
{
	public class Connection
	{
		public static MySqlDataReader SqlConnection(string query, List<MySqlParameter> parameters = null)
        {
            string connectString = "server=127.0.0.1;port=3302;database=DataBase;uid=root;pwd=;";
            MySqlConnection mySqlConnection = new MySqlConnection(connectString);
			
			 mySqlConnection.Open(); 
            MySqlCommand mySqlCommand = new MySqlCommand(query, mySqlConnection);
            
			if (parameters != null)
			{
				mySqlCommand.Parameters.AddRange(parameters.ToArray());
			}
			return mySqlCommand.ExecuteReader();
		}


		public static void MySqlClose(MySqlConnection connection)
		{
			connection.Close();

		}
	}
}
