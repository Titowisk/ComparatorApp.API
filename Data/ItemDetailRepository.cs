using System.Collections.Generic;
using System.Threading.Tasks;
using ComparatorApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace ComparatorApp.API.Data
{
    public class ItemDetailRepository : IItemDetailRepository
    {
        private readonly DataContext _context;

        public ItemDetailRepository(DataContext context)
        {
            _context = context;
        }

        public void Add(ItemDetail itemDetail)
        {
            _context.Add(itemDetail);
        }

        public void Delete(ItemDetail itemDetail)
        {
            _context.Remove(itemDetail);
        }

        public void Update(ItemDetail itemDetail)
        {
            _context.Update(itemDetail);
        }

        public async Task<List<ItemDetail>> GetItemsDetail()
        {
            // eager
            return await _context.ItemsDetail
            .Include(itemDetail => itemDetail.Item)
            .Include(itemDetail => itemDetail.Store)
            .Include(itemDetail => itemDetail.Brand)
            .Include(itemDetail => itemDetail.BaseUnit)
            .ToListAsync();
        }

        public async Task<ItemDetail> GetItemDetail(int id)
        {
            return await _context.ItemsDetail
            .Include(ide => ide.Item)
            .Include(ide => ide.Store)
            .Include(ide => ide.Brand)
            .Include(ide => ide.BaseUnit)
            .SingleOrDefaultAsync(ide => ide.Id == id);
        }

        public async Task<bool> ItemDetailExists(ItemDetail itemDetail)
        {
            bool ItemDetailExists = await _context.ItemsDetail.AnyAsync(i =>
                i.Price == itemDetail.Price &&
                i.Quantity == itemDetail.Quantity &&
                i.ItemId == itemDetail.ItemId &&
                i.BrandId == itemDetail.BrandId &&
                i.StoreId == itemDetail.StoreId);

            return ItemDetailExists;
        }

        public async Task<bool> ItemDetailExists(int id)
        {
            bool ItemDetailExists = await _context.ItemsDetail.AnyAsync(i => i.Id == id);

            return ItemDetailExists;
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}