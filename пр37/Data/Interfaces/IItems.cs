using пр37.Data.Models;
namespace пр37.Data.Interfaces
{
    public interface IItems
    {
        public IEnumerable<IItems> AllItems {  get; }
    }
}
