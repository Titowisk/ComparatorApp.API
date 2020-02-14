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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetItem(int id)
        {
            var item = await _repo.GetItem(id);

            if (item == null)
                return NotFound("This item doesn't exist.");

            return Ok(item);
        }

        // CREATE
        [HttpPost]
        public async Task<IActionResult> CreateItem(ItemForCreatingDto itemForCreatingDto)
        {
            itemForCreatingDto.Name = itemForCreatingDto.Name.ToLower();

            var item = new Item { Name = itemForCreatingDto.Name };
            if (await _repo.ItemExists(item))
                return BadRequest(
                    string.Format("There is already a item with name: {0}", item.Name.ToLower()));

            _repo.Add(item);
            await _repo.SaveAll();

            return Ok(item);
        }

        // DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveItem(int id)
        {
            // check if id exists
            if (!await _repo.ItemExists(id))
                return NotFound("The item was already removed.");

            var item = await _repo.GetItem(id);

            // remove
            _repo.Delete(item);

            if (await _repo.SaveAll())
                return NoContent();

            return BadRequest("It was not possible to remove the item.");
        }

        // UPDATE
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItem(int id, ItemForUpdatingDto itemForUpdatingDto)
        {
            if (!await _repo.ItemExists(id))
                return NotFound("This item doesn't exist.");

            if (itemForUpdatingDto == null)
                return BadRequest("There was no new information to update");


            var item = _mapper.Map<Item>(itemForUpdatingDto);

            _repo.Update(item);

            if (await _repo.SaveAll())
                return Ok(item);

            return BadRequest("It was not possible to update the current item.");
        }
    }
}