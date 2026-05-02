using MySql.Data.MySqlClient;

namespace Shop_Belosludtseva.Data.Common
{
    public class Connection
    {
        // <summary> Прописываем настройки для подключения сервера
        readonly static string ConnectionData = "server=127.0.0.1;port=3307;database=Shop;uid=root;pwd=";

        // <summary> Открываем соединение с базой данных MySQL
        public static MySqlConnection MySqlOpen()
        {
            MySqlConnection NewMySqlConnection = new MySqlConnection(ConnectionData);
            NewMySqlConnection.Open();
            return NewMySqlConnection;
        }

        // <summary> Выполнение запроса
        public static MySqlDataReader MySqlQuery(string Query, MySqlConnection Connection)
        {
            MySqlCommand NewMySqlCommand = new MySqlCommand(Query, Connection);
            return NewMySqlCommand.ExecuteReader();
        }

        // <summary> Закрываем соединение с базой данных MySQL
        public static void MySqlClose(MySqlConnection connection)
        {
            connection.Close();
        }
    }
}
