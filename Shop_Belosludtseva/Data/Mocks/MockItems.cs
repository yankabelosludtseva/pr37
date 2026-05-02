using Shop_Belosludtseva.Data.Interfaces;
using Shop_Belosludtseva.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace Shop_Belosludtseva.Data.Mocks
{
    public class MockItems : IItems
    {
        public ICategorys _category = new MockCaregories();

        public IEnumerable<Items> AllItems
        {
            get
            {
                return new List<Items>()
                {
                    new Items()
                    {
                        Id = 0,
                        Name = "DEXP MS-70",
                        Description= "Благодаря черном корпусу блаблбалбалбла",
                        Img = "https://www.dns-shop.ru/product/f0fd24cc14403332/mikrovolnovaa-pec-dexp-ms-70-cernyj/?utm_medium=organic&utm_source=google&utm_referrer=https%3A%2F%2Fwww.google.com%2F",
                        Price = 3699,
                        Category = _category.AllCategories.Where(x => x.Id == 0).First()
                    },

                    new Items()
                    {
                        Id = 1,
                        Name = "DEXP MS-71",
                        Description= "Благодаря черном корпусу блаблбалбалбла",
                        Img = "https://www.dns-shop.ru/product/f0fd24cc14403332/mikrovolnovaa-pec-dexp-ms-70-cernyj/?utm_medium=organic&utm_source=google&utm_referrer=https%3A%2F%2Fwww.google.com%2F",
                        Price = 3799,
                        Category = _category.AllCategories.Where(x => x.Id == 1).First()
                    },

                    new Items()
                    {
                        Id = 2,
                        Name = "DEXP MS-72",
                        Description= "Благодаря черном корпусу блаблбалбалбла",
                        Img = "https://www.dns-shop.ru/product/f0fd24cc14403332/mikrovolnovaa-pec-dexp-ms-70-cernyj/?utm_medium=organic&utm_source=google&utm_referrer=https%3A%2F%2Fwww.google.com%2F",
                        Price = 3899,
                        Category = _category.AllCategories.Where(x => x.Id == 2).First()
                    },

                };
            }
        }
    }
}