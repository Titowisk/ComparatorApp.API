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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBrand(int id)
        {
            var brand = await _repo.GetBrand(id);

            if (brand == null)
                return NotFound("This brand doesn't exist.");

            return Ok(brand);
        }

        [HttpGet]
        public async Task<IActionResult> GetBrands()
        {
            var brands = await _repo.GetBrands();
            var brandsDto = _mapper.Map<List<BrandsForListDto>>(brands);

            return Ok(brandsDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBrand(BrandForCreationDto brandForCreationDto)
        {
            brandForCreationDto.Name = brandForCreationDto.Name.ToLower();
            var brand = new Brand { Name = brandForCreationDto.Name };

            if (await _repo.BrandExists(brand))
                return BadRequest(string.Format("There is already a brand with name: {0}", brand.Name.ToLower()));

            _repo.Add(brand);
            await _repo.SaveAll();

            return Ok(brand);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveBrand(int id)
        {
            var brand = await _repo.GetBrand(id);

            if (brand == null)
                return NotFound("This brand doesn't exist.");

            _repo.Delete(brand);

            if (await _repo.SaveAll())
                return NoContent();

            return BadRequest("It was not possible to remove the item.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBrand(int id, BrandForUpdatingDto brandForUpdatingDto)
        {
            if (!await _repo.BrandExists(id))
                return NotFound("This brand doesn't exist.");

            if (brandForUpdatingDto == null)
                return BadRequest("There was no new information to update");


            var brand = _mapper.Map<Brand>(brandForUpdatingDto);

            _repo.Update(brand);

            if (await _repo.SaveAll())
                return Ok(brand);

            return BadRequest("It was not possible to update the current brand.");
        }

    }
}