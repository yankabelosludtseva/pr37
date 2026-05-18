using MySql.Data.MySqlClient;

namespace Shop_Belosludtseva.Data.Common
{
    public class Connection
    {
        readonly static string ConnectionData = "server=127.0.0.1;database=Shop_Belosludtseva;uid=root;pwd=;";
        public static MySqlConnection MySqlOpen()
        {
            MySqlConnection conn = new MySqlConnection(ConnectionData);
            conn.Open();
            return conn;
        }
        public static MySqlDataReader MySqlQuery(string query, MySqlConnection connection)
        {
            MySqlCommand command = new MySqlCommand(query, connection);
            return command.ExecuteReader();
        }
        public static void MySqlClose(MySqlConnection connection)
        {
            connection.Close();
        }
    }
}
