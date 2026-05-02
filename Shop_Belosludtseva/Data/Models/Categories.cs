using Shop_Belosludtseva.Data.Interfaces;

namespace Shop_Belosludtseva.Data.Models
{
    public class Categories
    {
        /// <summary> ID товара
        public int Id { get; set; }
        /// <summary> Наименование товара
        public string Name { get; set; }
        /// <summary> Описание товара
        public string Description { get; set; }
        public List<Items> Items { get; set; }
    }
}