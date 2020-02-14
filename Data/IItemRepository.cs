using System.Collections.Generic;
using System.Threading.Tasks;
using ComparatorApp.API.Models;

namespace ComparatorApp.API.Data
{
    public interface IItemRepository
    {
        void Add(Item item);
        void Delete(Item item);
        Item Update(Item item);
        Task<bool> SaveAll();
        Task<List<Item>> GetItems();
        Task<Item> GetItem(int id);
        Task<bool> ItemExists(Item item);

        Task<bool> ItemExists(int id);
    }
}