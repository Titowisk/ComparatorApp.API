using System.Collections.Generic;
using System.Threading.Tasks;
using ComparatorApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace ComparatorApp.API.Data
{
    public class BrandRepository : IBrandRepository
    {
        private readonly DataContext _context;

        public BrandRepository(DataContext context)
        {
            _context = context;
        }
        public void Add(Brand brand)
        {
            _context.Add(brand);
        }

        public async Task<bool> BrandExists(Brand brand)
        {
            return await _context.Brands.AnyAsync(b => b.Name == brand.Name.ToLower());
        }

        public async Task<bool> BrandExists(int id)
        {
            return await _context.Brands.AnyAsync(b => b.Id == id);
        }

        public void Delete(Brand brand)
        {
            _context.Remove(brand);
        }

        public async Task<Brand> GetBrand(int id)
        {
            return await _context.Brands.SingleOrDefaultAsync(b => b.Id == id);
        }

        public async Task<List<Brand>> GetBrands()
        {
            return await _context.Brands.ToListAsync();
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public Brand Update(Brand brand)
        {
            _context.Update(brand);

            return brand;
        }
    }
}