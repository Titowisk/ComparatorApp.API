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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetItemDetail(int id)
        {
            var itemDetail = await _repo.GetItemDetail(id);

            if (itemDetail == null)
                return NotFound("Item not found");

            var itemDetailDtos = _mapper.Map<ItemDetailForGetDto>(itemDetail);
            return Ok(itemDetailDtos);
        }

        [HttpPost]
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

            return Ok(itemDetail);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveItemDetail(int id)
        {
            // check if id exists
            if (!await _repo.ItemDetailExists(id))
                return NotFound("The item detail was already removed or doesn't exist");

            var item = await _repo.GetItemDetail(id);

            // remove
            _repo.Delete(item);

            if (await _repo.SaveAll())
                return NoContent();

            return BadRequest("It was not possible to remove the item.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItem(int id, ItemDetailForUpdatingDto itemForUpdatingDto)
        {
            if (!await _repo.ItemDetailExists(id))
                return NotFound("This item detail doesn't exist.");

            if (itemForUpdatingDto == null)
                return BadRequest("There was no new information to update");

            var itemDetail = await _repo.GetItemDetail(id);

            itemForUpdatingDto.Id = id;
            itemForUpdatingDto.Modified = DateTime.Now;
            itemDetail = _mapper.Map<ItemDetailForUpdatingDto, ItemDetail>(itemForUpdatingDto, itemDetail);

            _repo.Update(itemDetail);

            if (await _repo.SaveAll())
                return Ok(itemDetail);

            return BadRequest("It was not possible to update the current item detail.");
        }
    }
}