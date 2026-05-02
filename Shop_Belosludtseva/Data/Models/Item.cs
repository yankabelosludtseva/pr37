namespace Shop_Belosludtseva.Data.Models
{
    public class Items
    {
        /// <summary> ID товара
        public int Id { get; set; }
        /// <summary> Наименование товара
        public string Name { get; set; }
        /// <summary> Описание товара
        public string Description { get; set; }
        /// <summary> Картинка товара
        public string Img { get; set; }
        /// <summary> Цена товара
        public int Price { get; set; }
        /// <summary> Категория товара
        public Categories Category { get; set; }
    }
}
