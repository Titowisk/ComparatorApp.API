using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ComparatorApp.API.Data;
using Microsoft.AspNetCore.Mvc;
using ComparatorApp.API.Dtos.BrandsDtos;

namespace ComparatorApp.API.Controllers
{
    [Route("comparatorproject/[Controller]")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        private readonly IBrandRepository _repo;
        private readonly IMapper _mapper;

        public BrandsController(IBrandRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetBrands()
        {
            var brands = await _repo.GetBrands();
            var brandsDto = _mapper.Map<List<BrandsForListDto>>(brands);

            return Ok(brandsDto);
        }


    }
}