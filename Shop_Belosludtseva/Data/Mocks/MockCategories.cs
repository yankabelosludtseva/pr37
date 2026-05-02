using Shop_Belosludtseva.Data.Interfaces;
using Shop_Belosludtseva.Data.Models; // 1. Добавили ссылку на модели
using System.Collections.Generic;

namespace Shop_Belosludtseva.Data.Mocks
{
    public class MockCaregories : ICategorys
    {
        public IEnumerable<Categories> AllCategories
        {
            get
            {
                return new List<Categories>
                {
                    new Categories()
                    {
                        Id = 0,
                        Name = "Микроволновые печи",
                        Description = "Микроволновая печь - электроприбор для нагрева еды"
                    },

                    new Categories()
                    {
                        Id = 1,
                        Name = "Мультиварка",
                        Description = "Мультиварка готовит еду"
                    }
                };
            }
        }
    }
}