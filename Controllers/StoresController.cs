using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ComparatorApp.API.Data;
using ComparatorApp.API.Dtos.StoresDtos;
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
        public async Task<IActionResult> GetStores()
        {
            var stores = await _repo.GetStores();
            var storesDto = _mapper.Map<List<StoresForListDto>>(stores);

            return Ok(storesDto);
        }
    }
}