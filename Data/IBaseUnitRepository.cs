using System.Collections.Generic;
using System.Threading.Tasks;
using ComparatorApp.API.Models;

namespace ComparatorApp.API.Data
{
    public interface IBaseUnitRepository
    {
        void Add(BaseUnit baseUnit);
        void Delete(BaseUnit baseUnit);
        Task<bool> SaveAll();
        Task<List<BaseUnit>> GetBaseUnits();
        Task<bool> BaseUnitExists(BaseUnit baseUnit);
    }
}