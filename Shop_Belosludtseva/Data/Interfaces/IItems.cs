using Shop_Belosludtseva.Data.Models;
using System.Collections.Generic;

namespace Shop_Belosludtseva.Data.Interfaces
{
    public interface IItems
    {
        IEnumerable<Items> AllItems { get; }
        Items GetItem(int itemId);
        int Add(Items item);
        void Update(Items item);
        void Delete(int itemId);
    }
}