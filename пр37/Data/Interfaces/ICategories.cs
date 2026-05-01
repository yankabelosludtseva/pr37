using пр37.Data.Models;
namespace пр37.Data.Interfaces
{
    public interface ICategories
    {
        IEnumerable<Categories> AllCategories { get; }
    }
}
