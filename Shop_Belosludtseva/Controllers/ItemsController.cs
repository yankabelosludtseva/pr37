using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting.Internal;
using Shop_Belosludtseva.Data.Interfaces;
using Shop_Belosludtseva.Data.Models;
using Shop_Belosludtseva.Data.ViewModell;

namespace Shop_Belosludtseva.Controllers
{
    public class ItemsController : Controller
    {
        private IItems IAllItems;
        private ICategorys IAllCategories;
        VMItems VMItems = new VMItems();
        private readonly IWebHostEnvironment hostingEnvironment;

        public ItemsController(IItems IAllItems, ICategorys IAllCategories, IWebHostEnvironment environment)
        {
            this.IAllItems = IAllItems;
            this.IAllCategories = IAllCategories;
            this.hostingEnvironment = environment;
        }


        public ViewResult List(int id=0)
        {
            ViewBag.Title = "Страница с предметами";
            VMItems.Items = IAllItems.AllItems;
            VMItems.Categories = IAllCategories.AllCategories;
            VMItems.SelectCategory = id;
            return View(VMItems);
        }

        public ViewResult Add()
        {
            IEnumerable<Categories> Categories = IAllCategories.AllCategories;

            return View(Categories);
        }

        /// <summary>
        /// Метод добавления предмета
        /// </summary>
        /// <param name="name">Наименование предмета</param>
        /// <param name="description">Описание предмета</param>
        /// <param name="files">Изображение</param>
        /// <param name="price">Цена</param>
        /// <param name="idCategory">Код категории</param>
        /// <returns></returns>
        [HttpPost]
        public RedirectToResult Add(string name, string description, IFormFile files, float price, int idCategory)
        {
            // если присутствует файл
            if (files != null)
            {
                // Получаем путь к папке
                var uploads = Path.Combine(hostingEnvironment.WebRootPath, "img");
                // Получаем путь к файлу
                var filePath = Path.Combine(uploads, files.FileName);
                // Копируем файл
                files.CopyTo(new FileStream(filePath, FileMode.Create));
            }
            // Создаём новый предмет, заполняем данные
            Items newItems = new Items();
            newItems.Name = name;
            newItems.Description = description;
            newItems.Img = files.FileName;
            newItems.Price = Convert.ToInt32(price);
            newItems.Category = new Categories() { Id = idCategory };
            // Вызываем метод добавления
            int id = IAllItems.Add(newItems);
            // Перенаправляем пользователя на страницу изменения
            return Redirect("/Items/Update?id=" + id);
        }
    }
}