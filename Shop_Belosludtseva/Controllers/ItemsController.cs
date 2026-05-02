using Microsoft.AspNetCore.Mvc;
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

        public ItemsController(IItems IAllItems, ICategorys IAllCategories)
        {
            this.IAllItems = IAllItems;
            this.IAllCategories = IAllCategories;
        }


        public ViewResult List(int id=0)
        {
            ViewBag.Title = "Страница с предметами";

            VMItems.Items = IAllItems.AllItems;

            VMItems.Categories = IAllCategories.AllCategories;

            VMItems.SelectCategory = id;

            return View(VMItems);
        }
    }
}