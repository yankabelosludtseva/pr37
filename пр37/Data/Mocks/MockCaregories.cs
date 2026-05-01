using пр37.Data.Interfaces;
using пр37.Data.Models;

namespace пр37.Data.Mocks
{
    public class MockCategories : ICategories
    {
        /// <summary> Имитируем хранимые данные, через реализацию IEnumerable</summary>
        public IEnumerable<Categories> AllCategorys
        {
            // метод возвращения
            get
            {
                // возвращаем список категорий
                return new List<Categories>
                {
                    new Categories() {
                        Id = 0,
                        Name = "Микроволновые печи",
                        Description = "Микроволновая печь — электроприбор, предназначенный для быстрого приготовления или подогрева пищи"
                    },
                    new Categories() {
                        Id = 1,
                        Name = "Мультиварки",
                        Description = "Мультиварка — многофункциональный бытовой электроприбор, предназначенный для приготовления пищи"
                    },
                    new Categories() {
                        Id = 2,
                        Name = "Холодильники",
                        Description = "Холодильник — устройство для хранения продуктов при низкой температуре"
                    },
                    new Categories() {
                        Id = 3,
                        Name = "Плиты",
                        Description = "Кухонная плита для приготовления пищи"
                    }
                };
            }
        }
    }
}
