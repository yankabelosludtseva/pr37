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
        private readonly ICategorys _categories;

        public DBItems(ICategorys categories)
        {
            _categories = categories;
        }

        private IEnumerable<Categories> Categories => _categories.AllCategories;

        public IEnumerable<Items> AllItems
        {
            get
            {
                var items = new List<Items>();

                using (var connection = Connection.MySqlOpen())
                {
                    string query = "SELECT Id, Name, Description, Img, Price, IdCategory FROM Shop.items ORDER BY `Name`";

                    using (var command = new MySqlCommand(query, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            items.Add(new Items
                            {
                                Id = reader.IsDBNull(0) ? -1 : reader.GetInt32(0),
                                Name = reader.IsDBNull(1) ? "" : reader.GetString(1),
                                Description = reader.IsDBNull(2) ? "" : reader.GetString(2),
                                Img = reader.IsDBNull(3) ? "" : reader.GetString(3),
                                Price = reader.IsDBNull(4) ? 0 : reader.GetInt32(4),  // ✅ GetInt32 вместо GetDecimal
                                Category = reader.IsDBNull(5)
                                    ? null
                                    : Categories.FirstOrDefault(x => x.Id == reader.GetInt32(5))
                            });
                        }
                    }
                }

                return items;
            }
        }

        public Items GetItem(int itemId)
        {
            using (var connection = Connection.MySqlOpen())
            {
                string query = "SELECT Id, Name, Description, Img, Price, IdCategory FROM Shop.items WHERE Id = @Id";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", itemId);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Items
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Description = reader.GetString(2),
                                Img = reader.GetString(3),
                                Price = reader.GetInt32(4),  // ✅ GetInt32
                                Category = Categories.FirstOrDefault(x => x.Id == reader.GetInt32(5))
                            };
                        }
                    }
                }
            }
            return null;
        }

        public int Add(Items item)
        {
            string query = "INSERT INTO `items` (`Name`, `Description`, `Img`, `Price`, `IdCategory`) " +
                          "VALUES (@Name, @Description, @Img, @Price, @IdCategory); " +
                          "SELECT LAST_INSERT_ID();";

            using (var connection = Connection.MySqlOpen())
            using (var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Name", item.Name ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Description", item.Description ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Img", item.Img ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Price", item.Price);  // ✅ int передаётся как есть
                command.Parameters.AddWithValue("@IdCategory", item.Category?.Id ?? (object)DBNull.Value);

                return Convert.ToInt32(command.ExecuteScalar());
            }
        }

        public void Update(Items item)
        {
            string query = "UPDATE `items` SET `Name` = @Name, `Description` = @Description, " +
                          "`Img` = @Img, `Price` = @Price, `IdCategory` = @IdCategory WHERE `Id` = @Id";

            using (var connection = Connection.MySqlOpen())
            using (var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", item.Id);
                command.Parameters.AddWithValue("@Name", item.Name ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Description", item.Description ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Img", item.Img ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Price", item.Price);  // ✅ int
                command.Parameters.AddWithValue("@IdCategory", item.Category?.Id ?? (object)DBNull.Value);

                command.ExecuteNonQuery();
            }
        }

        public void Delete(int itemId)
        {
            string query = "DELETE FROM `items` WHERE `Id` = @Id";

            using (var connection = Connection.MySqlOpen())
            using (var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", itemId);
                command.ExecuteNonQuery();
            }
        }
    }
}