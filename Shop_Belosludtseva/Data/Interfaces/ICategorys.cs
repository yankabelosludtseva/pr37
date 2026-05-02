using Shop_Belosludtseva.Data.Models;
namespace Shop_Belosludtseva.Data.Interfaces
{
    public interface ICategorys
    {
        public IEnumerable<Categories> AllCategories { get; }
    }
}
