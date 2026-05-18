using MySql.Data.MySqlClient;
using Shop_Belosludtseva.Data.Common;
using Shop_Belosludtseva.Data.Interfaces;
using Shop_Belosludtseva.Data.Models;
using System.Collections.Generic;
using System.Linq;

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
                MySqlConnection conn = Common.Connection.MySqlOpen();
                MySqlDataReader ItemsData = Common.Connection.MySqlQuery("SELECT * FROM Items ORDER BY `Name`;", conn);
                while (ItemsData.Read())
                {
                    items.Add(new Items()
                    {
                        Id = ItemsData.IsDBNull(0) ? -1 : ItemsData.GetInt32(0),
                        Name = ItemsData.IsDBNull(1) ? "" : ItemsData.GetString(1),
                        Description = ItemsData.IsDBNull(2) ? "" : ItemsData.GetString(2),
                        Img = ItemsData.IsDBNull(3) ? "" : ItemsData.GetString(3),
                        Price = ItemsData.IsDBNull(4) ? -1 : ItemsData.GetInt32(4),
                        Category = ItemsData.IsDBNull(5) ? null : Categories.Where(x => x.Id == ItemsData.GetInt32(5)).First()
                    });
                }
                return items;
            }
        }
        public Items GetItem(int id)
        {
            Items item = null;
            MySqlConnection conn = Common.Connection.MySqlOpen();

            string query = "SELECT * FROM Items WHERE Id = @Id;";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Id", id);

            MySqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                item = new Items()
                {
                    Id = reader.IsDBNull(0) ? -1 : reader.GetInt32(0),
                    Name = reader.IsDBNull(1) ? "" : reader.GetString(1),
                    Description = reader.IsDBNull(2) ? "" : reader.GetString(2),
                    Img = reader.IsDBNull(3) ? "" : reader.GetString(3),
                    Price = reader.IsDBNull(4) ? -1 : reader.GetInt32(4),
                    Category = reader.IsDBNull(5) ? null : Categories.Where(x => x.Id == reader.GetInt32(5)).First()
                };
            }

            conn.Close();
            return item;
        }
        public int Add(Items item)
        {
            MySqlConnection connection = Common.Connection.MySqlOpen();
            Common.Connection.MySqlQuery($"INSERT INTO `items`(`Name`, `Description`, `Img`, `Price`, `IdCategory`) VALUES ('{item.Name}', '{item.Description}', '{item.Img}', {item.Price}, {item.Category.Id})", connection);
            connection.Close();

            int IdItem = -1;
            connection = Common.Connection.MySqlOpen();
            MySqlDataReader reader = Common.Connection.MySqlQuery(
                $"SELECT Id FROM items WHERE Name = '{item.Name}' AND Description = '{item.Description}' AND Price = {item.Price} AND IdCategory = {item.Category.Id}", connection);

            if (reader.HasRows)
            {
                reader.Read();
                IdItem = reader.GetInt32(0);
            }
            connection.Close();
            return IdItem;
        }
        public void Update(Items item)
        {
            MySqlConnection connection = Common.Connection.MySqlOpen();

            string query = @"UPDATE `items` 
                           SET `Name` = @Name, 
                               `Description` = @Description, 
                               `Img` = @Img, 
                               `Price` = @Price, 
                               `IdCategory` = @IdCategory 
                           WHERE `Id` = @Id;";

            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Id", item.Id);
            cmd.Parameters.AddWithValue("@Name", item.Name);
            cmd.Parameters.AddWithValue("@Description", item.Description);
            cmd.Parameters.AddWithValue("@Img", item.Img);
            cmd.Parameters.AddWithValue("@Price", item.Price);
            cmd.Parameters.AddWithValue("@IdCategory", item.Category.Id);

            cmd.ExecuteNonQuery();
            connection.Close();
        }
        public void Delete(int id)
        {
            Items item = GetItem(id);

            MySqlConnection connection = Common.Connection.MySqlOpen();

            string query = "DELETE FROM `items` WHERE `Id` = @Id;";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Id", id);

            cmd.ExecuteNonQuery();
            connection.Close();
            if (item != null && !string.IsNullOrEmpty(item.Img))
            {
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", item.Img.TrimStart('/'));
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }
        }
    }
}