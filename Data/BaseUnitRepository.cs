using System.Collections.Generic;
using System.Threading.Tasks;
using ComparatorApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace ComparatorApp.API.Data
{
    public class BaseUnitRepository : IBaseUnitRepository
    {
        private readonly DataContext _context;

        public BaseUnitRepository(DataContext context)
        {
            _context = context;
        }

        public void Add(BaseUnit baseUnit)
        {
            _context.Add(baseUnit);
        }

        public async Task<bool> BaseUnitExists(BaseUnit baseUnit)
        {
            return await _context.BaseUnits.AnyAsync(bu =>
            (bu.Name == baseUnit.Name && bu.Symbol == baseUnit.Symbol));
        }

        public void Delete(BaseUnit baseUnit)
        {
            _context.Remove(baseUnit);
        }

        public async Task<List<BaseUnit>> GetBaseUnits()
        {
            var baseUnits = await _context.BaseUnits.ToListAsync();

            return baseUnits;
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}