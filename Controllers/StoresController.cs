using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ComparatorApp.API.Data;
using ComparatorApp.API.Dtos.StoresDtos;
using ComparatorApp.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace ComparatorApp.API.Controllers
{
    [Route("comparatorproject/[controller]")]
    [ApiController]
    public class StoresController : ControllerBase
    {
        private readonly IStoreRepository _repo;
        private IMapper _mapper;

        public StoresController(IStoreRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        // Get Stores
        [HttpGet]
        public async Task<IActionResult> GetStores()
        {
            var stores = await _repo.GetStores();
            var storesDto = _mapper.Map<List<StoresForListDto>>(stores);

            return Ok(storesDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStore(int id)
        {
            var store = await _repo.GetStore(id);

            if (store == null)
                return NotFound("This store doesn't exist.");

            return Ok(store);
        }

        [HttpPost]
        public async Task<IActionResult> CreateStore(StoreForCreatingDto storeForCreatingDto)
        {
            storeForCreatingDto.Name = storeForCreatingDto.Name.ToLower();
            var store = new Store { Name = storeForCreatingDto.Name };

            if (await _repo.StoreExists(store))
                return BadRequest(string.Format("There is already a store with the given information."));

            _repo.Add(store);
            await _repo.SaveAll();

            return Ok(store);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveStore(int id)
        {
            if (!await _repo.StoreExists(id))
                return NotFound("This store was already removed or doesn't exist.");

            var store = await _repo.GetStore(id);

            _repo.Delete(store);

            if (await _repo.SaveAll())
                return NoContent();

            return BadRequest("It was not possible to remove the Store");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItem(int id, StoreForUpdatingDto storeForUpdatingDto)
        {
            if (!await _repo.StoreExists(id))
                return NotFound("This store doesn't exist.");

            if (storeForUpdatingDto == null)
                return BadRequest("There was no new information to update");


            var store = _mapper.Map<Store>(storeForUpdatingDto);

            _repo.Update(store);

            if (await _repo.SaveAll())
                return Ok(store);

            return BadRequest("It was not possible to update the current store.");
        }
    }
}