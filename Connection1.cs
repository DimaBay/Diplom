using MySql.Data.MySqlClient;

namespace inventory
{
    public class Connection1
    {
        public static int ExecuteScalar(string query, MySqlParameter[] parameters)
        {
            string connectionString = "server=127.0.0.1;port=3302;database=DataBase;uid=root;pwd=;";

            using MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            using MySqlCommand command = new MySqlCommand(query, connection);
            if (parameters != null)
            {
                command.Parameters.AddRange(parameters);
            }

            return Convert.ToInt32(command.ExecuteScalar());
        }

        public static void MySqlClose(MySqlConnection connection)
        {
            connection.Close();
        }
    }
}
