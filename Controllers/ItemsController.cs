using System.Threading.Tasks;
using ComparatorApp.API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ComparatorApp.API.Controllers
{
    [Route("comparatorproject/[Controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly DataContext _context;
        public ItemsController(DataContext context)
        {
            _context = context;
        }

        // READ
        // Get comparatorproject/items
        [HttpGet]
        public async Task<IActionResult> GetItems()
        {
            var items = await _context.Items.ToListAsync();

            return Ok(items);
        }

        // Get comparatorproject/item/3
        [HttpGet("{id}")]
        public async Task<IActionResult> GetItem(int id)
        {
            var item = await _context.Items.FirstOrDefaultAsync(i => i.Id == id);

            return Ok(item);
        }

        // CREATE

        // DELETE

        // UPDATE
    }
}