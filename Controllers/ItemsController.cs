using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ComparatorApp.API.Data;
using ComparatorApp.API.Dtos.ItemsDtos;
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

        // DELETE

        // UPDATE
    }
}