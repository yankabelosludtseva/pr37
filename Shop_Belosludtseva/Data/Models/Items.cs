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

        public Items(Items item = null)
        {
            if (item != null)
            {
                this.Id = item.Id;
                this.Name = item.Name;
                this.Description = item.Description;
                this.Img = item.Img;
                this.Price = item.Price;
                this.Category = item.Category;
            }
        }
    }
}
