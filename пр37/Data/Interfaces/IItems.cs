using пр37.Data.Models;
namespace пр37.Data.Interfaces
{
    public interface IItems
    {
        IEnumerable<Items> AllItems { get; }
    }
}
