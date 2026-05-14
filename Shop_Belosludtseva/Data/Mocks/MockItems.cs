using Shop_Belosludtseva.Data.Interfaces;
using Shop_Belosludtseva.Data.Models;

public class MockItems : IItems
{
    private readonly ICategorys _categories;
    private static int _nextId = 3;
    private readonly List<Items> _items;

    public MockItems(ICategorys categories)
    {
        _categories = categories;
        _items = new List<Items>
        {
            new Items() { Id = 0, Name = "DEXP MS-70", Description = "Микроволновка 1", Img = "img1.jpg", Price = 3699, Category = _categories.AllCategories.FirstOrDefault(x => x.Id == 0) },
            new Items() { Id = 1, Name = "DEXP MS-71", Description = "Микроволновка 2", Img = "img2.jpg", Price = 3799, Category = _categories.AllCategories.FirstOrDefault(x => x.Id == 1) },
            new Items() { Id = 2, Name = "DEXP MS-72", Description = "Микроволновка 3", Img = "img3.jpg", Price = 3899, Category = _categories.AllCategories.FirstOrDefault(x => x.Id == 2) }
        };
    }

    public IEnumerable<Items> AllItems => _items;

    public Items GetItem(int itemId) => _items.FirstOrDefault(i => i.Id == itemId);

    public int Add(Items item)
    {
        item.Id = _nextId++;
        _items.Add(item);
        return item.Id;
    }

    // ✅ Изменение предмета
    public void Update(Items item)
    {
        var existingItem = _items.FirstOrDefault(i => i.Id == item.Id);
        if (existingItem != null)
        {
            existingItem.Name = item.Name;
            existingItem.Description = item.Description;
            existingItem.Img = item.Img;
            existingItem.Price = item.Price;
            existingItem.Category = item.Category;
        }
    }

    // ✅ Удаление предмета
    public void Delete(int itemId)
    {
        var item = _items.FirstOrDefault(i => i.Id == itemId);
        if (item != null)
        {
            _items.Remove(item);
        }
    }
}