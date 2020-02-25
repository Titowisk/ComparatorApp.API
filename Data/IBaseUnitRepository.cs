using System.Collections.Generic;
using System.Threading.Tasks;
using ComparatorApp.API.Models;

namespace ComparatorApp.API.Data
{
    public interface IBaseUnitRepository
    {
        void Add(BaseUnit baseUnit);
        void Delete(BaseUnit baseUnit);
        BaseUnit Update(BaseUnit baseUnit);
        Task<bool> SaveAll();
        Task<List<BaseUnit>> GetBaseUnits();
        Task<BaseUnit> GetBaseUnit(int id);
        Task<bool> BaseUnitExists(BaseUnit baseUnit);
        Task<bool> BaseUnitExists(int id);
    }
}