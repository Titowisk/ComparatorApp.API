using System.Collections.Generic;
using System.Threading.Tasks;
using ComparatorApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace ComparatorApp.API.Data
{
    public class ItemRepository : IItemRepository
    {
        private readonly DataContext _context;

        public ItemRepository(DataContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Only saves in memory
        /// </summary>
        /// <param name="item"></param>
        public void Add(Item item)
        {
            _context.Add(item);
        }

        public void Delete(Item item)
        {
            _context.Remove(item);
        }

        public async Task<List<Item>> GetItems()
        {
            var items = await _context.Items.ToListAsync();

            return items;
        }

        public async Task<Item> GetItem(int id)
        {
            var item = await _context.Items.SingleOrDefaultAsync(i => i.Id == id);

            return item;
        }

        public async Task<bool> ItemExists(Item item)
        {
            // all items names are saved lower case
            return await _context.Items.AnyAsync(i => i.Name == item.Name.ToLower());
        }

        public async Task<bool> ItemExists(int id)
        {
            return await _context.Items.AnyAsync(i => i.Id == id);
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public Item Update(Item item)
        {
            _context.Update(item);

            return item;
        }
    }
}