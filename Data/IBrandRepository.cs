using System.Collections.Generic;
using System.Threading.Tasks;
using ComparatorApp.API.Models;

namespace ComparatorApp.API.Data
{
    public interface IBrandRepository
    {
        void Add(Brand brand);
        void Delete(Brand brand);
        Brand Update(Brand brand);
        Task<bool> SaveAll();
        Task<List<Brand>> GetBrands();
        Task<Brand> GetBrand(int id);
        Task<bool> BrandExists(Brand brand);
        Task<bool> BrandExists(int id);
    }
}