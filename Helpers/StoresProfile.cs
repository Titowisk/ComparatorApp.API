using AutoMapper;
using ComparatorApp.API.Dtos.StoresDtos;
using ComparatorApp.API.Models;

namespace ComparatorApp.API.Helpers
{
    /// <summary>
    /// AutoMapper for Store entity
    /// </summary>
    public class StoresProfile : Profile
    {
        /// <summary>
        /// Maps the store entity for each kind of store-dto
        /// </summary>
        public StoresProfile()
        {
            CreateMap<Store, StoresForListDto>();
        }
    }
}