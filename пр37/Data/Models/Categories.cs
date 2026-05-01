namespace пр37.Data.Models
{
    public class Categories
    {
        /// <summary> ID категории</summary>
        public int Id { get; set; }

        /// <summary> Наименование</summary>
        public string Name { get; set; }

        /// <summary> Описание категории</summary>
        public string Description { get; set; }

        /// <summary> Товары которые относятся к категории</summary>
        public List<Items> Items { get; set; }
    }
}
