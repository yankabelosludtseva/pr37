namespace пр37.Data.Models
{
    public class Items
    {
        /// <summary> ID товара</summary>
        public int Id { get; set; }

        /// <summary> Наименование товара</summary>
        public string Name { get; set; }

        /// <summary> Короткое описание</summary>
        public string Description { get; set; }

        /// <summary> Изображение товара</summary>
        public string Img { get; set; }

        /// <summary> Цена товара</summary>
        public int Price { get; set; }

        /// <summary> Категория товара</summary>
        public Categories Category { get; set; }
    }
}
