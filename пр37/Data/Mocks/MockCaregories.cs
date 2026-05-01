using пр37.Data.Interfaces;
using пр37.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace пр37.Data.Mocks
{
    public class MockCategories : ICategories
    {
        public IEnumerable<Categories> AllCategories
        {
            get
            {
                return new List<Categories>
                {
                    new Categories
                    {
                        Id = 0,
                        Name = "Микроволновые печи",
                        Description = "Микроволновая печь для быстрого приготовления пищи"
                    },
                    new Categories
                    {
                        Id = 1,
                        Name = "Мультиварки",
                        Description = "Мультиварка для автоматического приготовления блюд"
                    },
                    new Categories
                    {
                        Id = 2,
                        Name = "Холодильники",
                        Description = "Холодильник для хранения продуктов"
                    }
                };
            }
        }
    }
}