using System.Collections.Generic;
using System.Threading.Tasks;
using ComparatorApp.API.Models;

namespace ComparatorApp.API.Data
{
    public interface IBrandRepository
    {
        void Add(Brand brand);
        void Delete(Brand brand);
        Task<bool> SaveAll();
        Task<List<Brand>> GetBrands();
        Task<bool> BrandExists(Brand brand);
    }
}