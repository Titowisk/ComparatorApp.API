using Microsoft.AspNetCore.Mvc;
using ComparatorApp.API.Data;
using AutoMapper;
using ComparatorApp.API.Models;
using ComparatorApp.API.Dtos.ItemsDetailDtos;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ComparatorApp.API.Controllers
{
    [Route("comparatorproject/[Controller]")]
    [ApiController]
    public class ItemsDetailController : ControllerBase
    {
        private readonly IItemDetailRepository _repo;
        private readonly IMapper _mapper;

        public ItemsDetailController(IItemDetailRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetItemsDetail()
        {
            var itemsDetail = await _repo.GetItemsDetail();
            var itemsDetailDtos = _mapper.Map<List<ItemsDetailForListDto>>(itemsDetail);
            return Ok(itemsDetailDtos);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateItemDetail(ItemDetailForCreationDto itemDetailForCreationDto)
        {
            var itemDetail = new ItemDetail
            {
                ItemId = itemDetailForCreationDto.ItemId,
                BrandId = itemDetailForCreationDto.BrandId,
                StoreId = itemDetailForCreationDto.StoreId,
                BaseUnitId = itemDetailForCreationDto.BaseUnitId,
                Price = itemDetailForCreationDto.Price,
                Quantity = itemDetailForCreationDto.Quantity,
                Created = DateTime.Now
            };

            if (await _repo.ItemDetailExists(itemDetail))
                return BadRequest("There is already a item detail with the exaclty information of the one you are trying to create.");

            _repo.Add(itemDetail);
            await _repo.SaveAll();

            return StatusCode(201);
        }
    }
}