using Shop_Belosludtseva.Data.Models;
namespace Shop_Belosludtseva.Data.Interfaces
{
    public interface IItems
    {
        public IEnumerable<Items> AllItems { get; }
    }
}
