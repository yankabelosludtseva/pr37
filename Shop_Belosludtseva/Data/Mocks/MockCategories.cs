using Shop_Belosludtseva.Data.Interfaces;
using Shop_Belosludtseva.Data.Models;
using System.Collections.Generic;

namespace Shop_Belosludtseva.Data.Mocks
{
    public class MockCategories : ICategorys
    {
        public IEnumerable<Categories> AllCategories
        {
            get
            {
                return new List<Categories>()
                {
                    new Categories { Id = 0, Name = "Микроволновые печи", Description = "Бытовая техника" },
                    new Categories { Id = 1, Name = "Холодильники", Description = "Бытовая техника" },
                    new Categories { Id = 2, Name = "Электрочайники", Description = "Мелкая бытовая техника" }
                };
            }
        }
    }
}