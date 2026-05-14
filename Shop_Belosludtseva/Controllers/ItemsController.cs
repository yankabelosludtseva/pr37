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
        private readonly IItems _allItems;
        private readonly ICategorys _allCategories;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public ItemsController(IItems allItems, ICategorys allCategories, IWebHostEnvironment environment)
        {
            _allItems = allItems;
            _allCategories = allCategories;
            _hostingEnvironment = environment;
        }

        public ViewResult List(int id = 0)
        {
            ViewBag.Title = "Страница с предметами";

            var viewModel = new VMItems
            {
                Items = _allItems.AllItems,
                Categories = _allCategories.AllCategories,
                SelectCategory = id
            };

            return View(viewModel);
        }

        public ViewResult Add()
        {
            IEnumerable<Categories> categories = _allCategories.AllCategories;
            return View(categories);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(string name, string description, IFormFile files, int price, int idCategory)
        {
            string fileName = null;

            if (files != null && files.Length > 0)
            {
                fileName = Guid.NewGuid().ToString() + Path.GetExtension(files.FileName);
                var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "img");

                if (!Directory.Exists(uploads))
                    Directory.CreateDirectory(uploads);

                var filePath = Path.Combine(uploads, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    files.CopyTo(stream);
                }
            }

            var newItem = new Items
            {
                Name = name,
                Description = description,
                Img = fileName,  // ✅ Безопасно: будет null, если файл не загружен
                Price = price,
                Category = new Categories { Id = idCategory }
            };

            int id = _allItems.Add(newItem);
            return RedirectToAction(nameof(Update), new { id = id });
        }

        // ✅ ИЗМЕНЕНО: ViewResult → IActionResult
        public IActionResult Update(int id)
        {
            var item = _allItems.GetItem(id);
            if (item == null)
                return NotFound();  // ✅ Теперь работает, т.к. IActionResult поддерживает NotFoundResult

            ViewBag.Categories = _allCategories.AllCategories;
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Items item, IFormFile files)
        {
            if (ModelState.IsValid)
            {
                if (files != null && files.Length > 0)
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(files.FileName);
                    var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "img");

                    if (!Directory.Exists(uploads))
                        Directory.CreateDirectory(uploads);

                    var filePath = Path.Combine(uploads, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        files.CopyTo(stream);
                    }

                    item.Img = fileName;
                }
                else
                {
                    var existingItem = _allItems.GetItem(item.Id);
                    if (existingItem != null)
                    {
                        item.Img = existingItem.Img;
                    }
                }

                _allItems.Update(item);
                return RedirectToAction(nameof(List));
            }

            ViewBag.Categories = _allCategories.AllCategories;
            return View(item);
        }

        // ✅ ИЗМЕНЕНО: ViewResult → IActionResult
        public IActionResult Delete(int id)
        {
            var item = _allItems.GetItem(id);
            if (item == null)
                return NotFound();  // ✅ Теперь работает

            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var item = _allItems.GetItem(id);
            if (item != null)
            {
                if (!string.IsNullOrEmpty(item.Img))
                {
                    var filePath = Path.Combine(_hostingEnvironment.WebRootPath, "img", item.Img);
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }
                }

                _allItems.Delete(id);
            }

            return RedirectToAction(nameof(List));
        }
    }
}