using System.Collections.Generic;
using System.Threading.Tasks;
using ComparatorApp.API.Models;

namespace ComparatorApp.API.Data
{
    public interface IItemDetailRepository
    {
        void Add(ItemDetail itemDetail);
        void Delete(ItemDetail itemDetail);
        void Update(ItemDetail itemDetail);
        Task<bool> SaveAll();
        Task<List<ItemDetail>> GetItemsDetail();
        Task<ItemDetail> GetItemDetail(int id);
        Task<bool> ItemDetailExists(ItemDetail itemDetail);
        Task<bool> ItemDetailExists(int id);
    }
}