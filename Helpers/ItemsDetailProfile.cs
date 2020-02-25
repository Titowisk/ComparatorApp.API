using AutoMapper;
using ComparatorApp.API.Dtos.ItemsDetailDtos;
using ComparatorApp.API.Models;

namespace ComparatorApp.API.Helpers
{
    public class ItemsDetailProfile : Profile
    {
        public ItemsDetailProfile()
        {
            CreateMap<ItemDetail, ItemDetailForCreationDto>();
            CreateMap<ItemDetail, ItemsDetailForListDto>()
                .ForMember(dest => dest.ItemName,
                    opt => opt.MapFrom(src => src.Item.Name))
                .ForMember(dest => dest.BrandName,
                    opt => opt.MapFrom(src => src.Brand.Name))
                .ForMember(dest => dest.StoreName,
                    opt => opt.MapFrom(src => src.Store.Name))
                .ForMember(dest => dest.BaseUnitName,
                    opt => opt.MapFrom(src => src.BaseUnit.Name));
            CreateMap<ItemDetail, ItemDetailForGetDto>()
                .ForMember(dest => dest.ItemName,
                    opt => opt.MapFrom(src => src.Item.Name))
                .ForMember(dest => dest.BrandName,
                    opt => opt.MapFrom(src => src.Brand.Name))
                .ForMember(dest => dest.StoreName,
                    opt => opt.MapFrom(src => src.Store.Name))
                .ForMember(dest => dest.BaseUnitName,
                    opt => opt.MapFrom(src => src.BaseUnit.Name));
            CreateMap<ItemDetailForUpdatingDto, ItemDetail>();
        }
    }
}