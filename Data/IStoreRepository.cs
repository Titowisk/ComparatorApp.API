using System.Collections.Generic;
using System.Threading.Tasks;
using ComparatorApp.API.Models;

namespace ComparatorApp.API.Data
{
    public interface IStoreRepository
    {
        void Add(Store store);
        void Delete(Store store);
        Store Update(Store store);
        Task<bool> SaveAll();
        Task<List<Store>> GetStores();
        Task<Store> GetStore(int id);
        Task<bool> StoreExists(Store store);
        Task<bool> StoreExists(int id);
    }
}