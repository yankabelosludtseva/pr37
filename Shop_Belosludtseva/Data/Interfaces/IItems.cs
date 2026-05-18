using Shop_Belosludtseva.Data.Models;
using System.Collections.Generic;

namespace Shop_Belosludtseva.Data.Interfaces
{
    public interface IItems
    {
        public IEnumerable<Items> AllItems { get; }
        Items GetItem(int id);
        public int Add(Items item);
        void Update(Items item);
        void Delete(int id);
    }
}