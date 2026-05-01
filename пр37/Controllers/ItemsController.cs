using Microsoft.AspNetCore.Mvc;
using пр37.Data.Interfaces;

namespace пр37.Controllers
{
    public class ItemsController : Controller
    {
        /// <summary> Интерфейс объектов</summary>
        private readonly IItems _IAllItems;

        /// <summary> Интерфейс категорий</summary>
        private readonly ICategories _IAllCategories;

        /// <summary> Конструктор принимающий параметры</summary>
        public ItemsController(IItems IAllItems, ICategories IAllCategories)
        {
            // запоминаем интерфейс вещей
            _IAllItems = IAllItems;
            // запоминаем интерфейс категорий
            _IAllCategories = IAllCategories;
        }

        /// <summary> Метод реализующий отображение данных</summary>
        public ViewResult List()
        {
            // Название нашей страницы
            ViewBag.Title = "Страница с предметами";

            // получаем все вещи
            var cars = _IAllItems.AllItems;

            // передаём на страницу
            return View(cars);
        }
    }
}
