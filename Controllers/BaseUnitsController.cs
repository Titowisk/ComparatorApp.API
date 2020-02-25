using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ComparatorApp.API.Data;
using ComparatorApp.API.Dtos.BaseUnitsDtos;
using ComparatorApp.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace ComparatorApp.API.Controllers
{
    [Route("comparatorproject/[Controller]")]
    [ApiController]
    public class BaseUnitsController : ControllerBase
    {
        private readonly IBaseUnitRepository _repo;
        private readonly IMapper _mapper;

        public BaseUnitsController(IBaseUnitRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetBaseUnits()
        {
            var baseUnits = await _repo.GetBaseUnits();

            var baseUnitsDto = _mapper.Map<List<BaseUnitsForListDto>>(baseUnits);

            return Ok(baseUnitsDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBaseUnit(int id)
        {
            var baseUnit = await _repo.GetBaseUnit(id);

            if (baseUnit == null)
                return NotFound("This base unit doesn't exist");

            return Ok(baseUnit);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBaseUnit(BaseUnitForCreationDto baseUnitForCreationDto)
        {
            var baseUnit = new BaseUnit
            {
                Name = baseUnitForCreationDto.Name.ToLower(),
                Symbol = baseUnitForCreationDto.Symbol.ToLower()
            };

            if (await _repo.BaseUnitExists(baseUnit))
                return BadRequest(
                    string.Format("There is already a base unit with name: {0} and symbol: {1}",
                    baseUnit.Name, baseUnit.Symbol));
            _repo.Add(baseUnit);
            await _repo.SaveAll();

            return StatusCode(201);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveBaseUnit(int id)
        {
            // check if id exists
            if (!await _repo.BaseUnitExists(id))
                return NotFound("The base unit was already removed or doesn't exist.");

            var baseUnit = await _repo.GetBaseUnit(id);

            // remove
            _repo.Delete(baseUnit);

            if (await _repo.SaveAll())
                return NoContent();

            return BadRequest("It was not possible to remove the baseUnit.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItem(int id, BaseUnitForUpdatingDto baseUnitForUpdatingDto)
        {
            if (!await _repo.BaseUnitExists(id))
                return NotFound("This base unit doesn't exist.");

            if (baseUnitForUpdatingDto == null)
                return BadRequest("There was no new information to update");

            baseUnitForUpdatingDto.Id = id;

            var baseUnit = _mapper.Map<BaseUnit>(baseUnitForUpdatingDto);

            _repo.Update(baseUnit);

            if (await _repo.SaveAll())
                return Ok(baseUnit);

            return BadRequest("It was not possible to update the current base unit.");
        }
    }
}