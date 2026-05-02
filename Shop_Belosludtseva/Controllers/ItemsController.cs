using Microsoft.AspNetCore.Mvc;
using Shop_Belosludtseva.Data.Interfaces;
using Shop_Belosludtseva.Data.Models;

namespace Shop_Belosludtseva.Controllers
{
    public class ItemsController : Controller
    {
        private IItems IAllItems;
        private ICategorys IAllCategories;

        public ItemsController(IItems IAllItems, ICategorys IAllCategories)
        {
            this.IAllItems = IAllItems;
            this.IAllCategories = IAllCategories;
        }

        public ViewResult List()
        {
            ViewBag.Title = "Страница с предметами";

            var cars = IAllItems.AllItems;
            return View(cars);
        }
    }
}