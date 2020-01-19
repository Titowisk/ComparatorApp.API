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

        [HttpPost("create")]
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
    }
}