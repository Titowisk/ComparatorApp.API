using AutoMapper;
using ComparatorApp.API.Dtos.ItemsDtos;
using ComparatorApp.API.Models;

namespace ComparatorApp.API.Helpers
{
    /// <summary>
    /// AutoMapper for Item Entity
    /// </summary>
    public class ItemsProfile : Profile
    {
        public ItemsProfile()
        {
            CreateMap<Item, ItemsForListDto>();
        }
    }
}