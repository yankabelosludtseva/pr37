using пр37.Data.Interfaces;
using пр37.Data.Models;
namespace пр37.Data.Mocks
{
    public class MockItems : IItems
    {
        /// <summary> Интерфейс категорий</summary>
        public ICategories _category = new MockCategories();

        /// <summary> Имитируем хранимые данные, через реализацию IEnumerable</summary>
        public IEnumerable<Items> AllItems
        {
            // метод возвращения
            get
            {
                // возвращаем список вещей
                return new List<Items>()
                {
                    new Items() {
                        Id = 0,
                        Name = "DEXP MS-70",
                        Description = "Благодаря черному корпусу с лаконичным дизайном микроволновая печь отлично впишется в интерьер кухни",
                        Img = "https://c.dns-shop.ru/thumb/st4/fit/wm/0/0/d3136d0800646b0b8a38e5c4d5e5f5a5.jpg",
                        Price = 3699,
                        Category = _category.AllCategories.Where(x => x.Id == 0).First()
                    },
                    new Items() {
                        Id = 1,
                        Name = "BBK 20MWC",
                        Description = "Компактная микроволновая печь с механическим управлением и объемом камеры 20 литров",
                        Img = "https://c.dns-shop.ru/thumb/st4/fit/wm/0/0/bbk20mwc.jpg",
                        Price = 4299,
                        Category = _category.AllCategories.Where(x => x.Id == 0).First()
                    },
                    new Items() {
                        Id = 2,
                        Name = "Redmond RMC-M90",
                        Description = "Мультиварка с объемом чаши 5 литров, 10 автоматических программ и отложенным стартом",
                        Img = "https://c.dns-shop.ru/thumb/st4/fit/wm/0/0/redmond_rmc_m90.jpg",
                        Price = 5999,
                        Category = _category.AllCategories.Where(x => x.Id == 1).First()
                    },
                    new Items() {
                        Id = 3,
                        Name = "Polaris PMC 5053AD",
                        Description = "Мультиварка-скороварка с функцией отложенного старта и поддержанием температуры",
                        Img = "https://c.dns-shop.ru/thumb/st4/fit/wm/0/0/polaris_pmc_5053ad.jpg",
                        Price = 4599,
                        Category = _category.AllCategories.Where(x => x.Id == 1).First()
                    },
                    new Items() {
                        Id = 4,
                        Name = "Samsung MC28H5013AK",
                        Description = "Микроволновая печь с грилем и конвекцией, объем камеры 28 литров",
                        Img = "https://c.dns-shop.ru/thumb/st4/fit/wm/0/0/samsung_mc28h5013ak.jpg",
                        Price = 8999,
                        Category = _category.AllCategories.Where(x => x.Id == 0).First()
                    }
                };
            }
        }
    }
}