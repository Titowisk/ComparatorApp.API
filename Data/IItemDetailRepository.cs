using System.Collections.Generic;
using System.Threading.Tasks;
using ComparatorApp.API.Models;

namespace ComparatorApp.API.Data
{
    public interface IItemDetailRepository
    {
        void Add(ItemDetail itemDetail);
        void Delete(ItemDetail itemDetail);
        Task<bool> SaveAll();
        Task<List<ItemDetail>> GetItemsDetail();
        Task<bool> ItemDetailExists(ItemDetail itemDetail);
    }
}