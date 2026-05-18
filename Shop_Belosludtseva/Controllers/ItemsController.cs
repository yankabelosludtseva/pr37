using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Shop_Belosludtseva.Data.Interfaces;
using Shop_Belosludtseva.Data.Models;
using Shop_Belosludtseva.Data.ViewModell;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Shop_Belosludtseva.Controllers
{
    public class ItemsController : Controller
    {
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment;
        private IItems IAllItems;
        private ICategorys IAllCategories;
        VMItems VMItems = new VMItems();

        public ItemsController(IItems IAllItems, ICategorys IAllCategories, Microsoft.AspNetCore.Hosting.IHostingEnvironment environment)
        {
            this.IAllItems = IAllItems;
            this.IAllCategories = IAllCategories;
            this.hostingEnvironment = environment;
        }

        public ViewResult List(int id = 0)
        {
            ViewBag.Title = "Страница с предметами";

            VMItems.Items = IAllItems.AllItems
                .Where(i => id == 0 || i.Category.Id == id)
                .ToList();
            VMItems.Categories = IAllCategories.AllCategories;
            VMItems.SelectCategory = id;
            return View(VMItems);
        }
        [HttpGet]
        public ViewResult Add()
        {
            IEnumerable<Categories> Categories = IAllCategories.AllCategories;
            return View(Categories);
        }
        [HttpPost]
        public RedirectResult Add(string name, string description, IFormFile files, float price, int idCategory)
        {
            string fileName = null;

            if (files != null)
            {
                var uploads = Path.Combine(hostingEnvironment.WebRootPath, "img");

                fileName = Guid.NewGuid().ToString() + Path.GetExtension(files.FileName);

                var filePath = Path.Combine(uploads, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    files.CopyTo(stream);
                }
            }

            Items newItems = new Items();
            newItems.Name = name;
            newItems.Description = description;
            newItems.Img = "/img/" + fileName;
            newItems.Price = Convert.ToInt32(price);
            newItems.Category = new Categories() { Id = idCategory };

            int id = IAllItems.Add(newItems);
            return Redirect("/Items/List");
        }
        [HttpGet]
        public ViewResult Update(int id)
        {
            ViewBag.Title = "Обновление товара";
            var item = IAllItems.GetItem(id);
            ViewBag.Categories = IAllCategories.AllCategories;
            return View(item);
        }

        [HttpPost]
        public IActionResult Update(Items item, IFormFile? newImage)
        {
            if (newImage != null)
            {
                var oldItem = IAllItems.GetItem(item.Id);
                if (oldItem != null && !string.IsNullOrEmpty(oldItem.Img))
                {
                    var oldFilePath = Path.Combine(hostingEnvironment.WebRootPath, oldItem.Img.TrimStart('/'));
                    if (System.IO.File.Exists(oldFilePath))
                    {
                        System.IO.File.Delete(oldFilePath);
                    }
                }
                var uploads = Path.Combine(hostingEnvironment.WebRootPath, "img");
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(newImage.FileName);
                var filePath = Path.Combine(uploads, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    newImage.CopyTo(stream);
                }

                item.Img = "/img/" + fileName;
            }
            else
            {
                var oldItem = IAllItems.GetItem(item.Id);
                item.Img = oldItem?.Img;
            }

            IAllItems.Update(item);
            return Redirect("/Items/List");
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            IAllItems.Delete(id);
            return Redirect("/Items/List");
        }
    }
}