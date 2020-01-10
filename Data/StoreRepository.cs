using System.Collections.Generic;
using System.Threading.Tasks;
using ComparatorApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace ComparatorApp.API.Data
{
    public class StoreRepository : IStoreRepository
    {
        private readonly DataContext _context;

        public StoreRepository(DataContext context)
        {
            _context = context;
        }
        public void Add(Store store)
        {
            _context.Add(store);
        }

        public void Delete(Store store)
        {
            _context.Remove(store);
        }

        public async Task<List<Store>> GetStores()
        {
            var stores = await _context.Stores.ToListAsync();

            return stores;
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}