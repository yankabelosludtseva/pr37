using MySql.Data.MySqlClient;
using Shop_Belosludtseva.Data.Common;
using Shop_Belosludtseva.Data.Interfaces;
using Shop_Belosludtseva.Data.Models;

namespace Shop_Belosludtseva.Data.DataBase
{
    public class DBItems : IItems
    {
        public IEnumerable<Categories> Categories = new DBCategory().AllCategories;

        public IEnumerable<Items> AllItems
        {
            get
            {
                List<Items> items = new List<Items>();
                MySqlConnection MySqlConnection = Connection.MySqlOpen();
                MySqlDataReader ItemsData = Connection.MySqlQuery("SELECT * FROM Shop.items ORDER BY `Name`;", MySqlConnection);

                while (ItemsData.Read())
                {
                    items.Add(new Items()  // исправлено: new Items)
                    {
                        Id = ItemsData.IsDBNull(0) ? -1 : ItemsData.GetInt32(0),
                        Name = ItemsData.IsDBNull(1) ? "" : ItemsData.GetString(1),
                        Description = ItemsData.IsDBNull(2) ? "" : ItemsData.GetString(2),
                        Img = ItemsData.IsDBNull(3) ? "" : ItemsData.GetString(3),
                        Price = ItemsData.IsDBNull(4) ? -1 : ItemsData.GetInt32(4),
                        Category = ItemsData.IsDBNull(5) ? null : Categories.FirstOrDefault(x => x.Id == ItemsData.GetInt32(5))
                    });
                }

                Connection.MySqlClose(MySqlConnection);
                return items;
            }
        }
        public int Add(Items Item)
        {
            // открываем подключение к базе данных
            MySqlConnection MySqlConnection = Connection.MySqlOpen();
            // вставляем запись
            Connection.MySqlQuery(
                $"INSERT INTO `items` (`Name`, `Description`, `Img`, `Price`, `IdCategory`) VALUES ('{Item.Name}', '{Item.Description}', '{Item.Img}', {Item.Price}, {Item.Category.Id});",
                MySqlConnection);
            MySqlConnection.Close();

            int IdItem = -1;
            // выполняем второй запрос, для того чтобы получить ID
            MySqlConnection = Connection.MySqlOpen();
            // получаем Id записи обратно
            MySqlDataReader MySqlDataReaderItem = Connection.MySqlQuery(
                $"SELECT `Id` FROM `items` WHERE `Name` = '{Item.Name}' AND `Description` = '{Item.Description}' AND `Price` = {Item.Price} AND `IdCategory` = {Item.Category.Id};",
                MySqlConnection);
            // если есть что читать
            if (MySqlDataReaderItem.HasRows)
            {
                // читаем
                MySqlDataReaderItem.Read();
                IdItem = MySqlDataReaderItem.GetInt32(0);
            }
            // закрываем соединение
            MySqlConnection.Close();
            // возвращаем результат
            return IdItem;
        }
    }
}
