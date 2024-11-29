using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessService.Data;
using DataAccessService.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DriversController : Controller
    {
        private readonly AppDbContext _context;
        private readonly RabbitMQService _rabbitMQService;

        public DriversController(AppDbContext context, RabbitMQService rabbitMQService)
        {
            _context = context;
            _rabbitMQService = rabbitMQService;
        }
        // GET: api/Drivers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Driver>>> GetDrivers()
        {
            return await _context.Drivers.ToListAsync();
        }

        // GET: api/Drivers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Driver>> GetDriver(int id)
        {
            var route = await _context.Drivers.FindAsync(id);

            if (route == null)
            {
                return NotFound();
            }

            return route;
        }

        // POST: api/Drivers
        [HttpPost]
        public async Task<ActionResult<Driver>> PostBus(Driver route)
        {
            _context.Drivers.Add(route);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDriver), new { id = route.Id }, route);
        }

        // PUT: api/Drivers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDriver(int id, Driver route)
        {
            if (id != route.Id)
            {
                return BadRequest();
            }

            _context.Entry(route).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DriverExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Drivers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDriver(int id)
        {
            var route = await _context.Drivers.FindAsync(id);
            if (route == null)
            {
                return NotFound();
            }

            _context.Drivers.Remove(route);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DriverExists(int id)
        {
            return _context.Drivers.Any(e => e.Id == id);
        }
    }
}
