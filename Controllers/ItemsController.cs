using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ComparatorApp.API.Data;
using ComparatorApp.API.Dtos.ItemsDtos;
using ComparatorApp.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ComparatorApp.API.Controllers
{
    [Route("comparatorproject/[Controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IItemRepository _repo;
        private readonly IMapper _mapper;

        public ItemsController(IItemRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        // READ
        // Get comparatorproject/items
        [HttpGet]
        public async Task<IActionResult> GetItems()
        {
            var items = await _repo.GetItems();
            var itemsDto = _mapper.Map<List<ItemsForListDto>>(items);

            return Ok(itemsDto);
        }

        // CREATE
        [HttpPost("create")]
        public async Task<IActionResult> CreateItem(ItemForCreatingDto itemForCreatingDto)
        {
            itemForCreatingDto.Name = itemForCreatingDto.Name.ToLower();

            var item = new Item { Name = itemForCreatingDto.Name };
            if (await _repo.ItemExists(item))
                return BadRequest(
                    string.Format("There is already a item with name: {0}", item.Name.ToLower()));

            _repo.Add(item);
            await _repo.SaveAll(); // TODO: is this really here??

            return StatusCode(201);
        }

        // DELETE

        // UPDATE
    }
}