using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ComparatorApp.API.Data;
using Microsoft.AspNetCore.Mvc;
using ComparatorApp.API.Dtos.BrandsDtos;
using ComparatorApp.API.Models;

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

        [HttpPost("create")]
        public async Task<IActionResult> CreateBrand(BrandForCreationDto brandForCreationDto)
        {
            brandForCreationDto.Name = brandForCreationDto.Name.ToLower();
            var brand = new Brand { Name = brandForCreationDto.Name };

            if (await _repo.BrandExists(brand))
                return BadRequest(string.Format("There is already a brand with name: {0}", brand.Name.ToLower()));

            _repo.Add(brand);
            await _repo.SaveAll();

            return StatusCode(201);
        }

    }
}