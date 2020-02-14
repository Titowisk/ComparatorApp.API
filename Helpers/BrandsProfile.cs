using AutoMapper;
using ComparatorApp.API.Dtos.BrandsDtos;
using ComparatorApp.API.Models;

namespace ComparatorApp.API.Helpers
{
    public class BrandsProfile : Profile
    {
        public BrandsProfile()
        {
            CreateMap<Brand, BrandsForListDto>();
            CreateMap<Brand, BrandForCreationDto>();
            CreateMap<BrandForUpdatingDto, Brand>();
        }
    }
}