using AutoMapper;
using ComparatorApp.API.Dtos.BaseUnitsDtos;
using ComparatorApp.API.Models;

namespace ComparatorApp.API.Helpers
{
    public class BaseUnitsProfile : Profile
    {
        public BaseUnitsProfile()
        {
            CreateMap<BaseUnit, BaseUnitsForListDto>();
        }
    }
}