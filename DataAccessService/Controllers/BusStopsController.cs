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
    public class BusStopsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BusStopsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/BusStops
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BusStop>>> GetBusStops()
        {
            return await _context.BusStops.ToListAsync();
        }

        // GET: api/BusStops/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BusStop>> GetBusStops(int id)
        {
            var busStop = await _context.BusStops.FindAsync(id);

            if (busStop == null)
            {
                return NotFound();
            }

            return busStop;
        }

        // POST: api/BusStops
        [HttpPost]
        public async Task<ActionResult<BusStop>> PostBusStop(BusStop busStop)
        {
            _context.BusStops.Add(busStop);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBusStops), new { id = busStop.Id }, busStop);
        }

        // PUT: api/BusStops/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBusStop(int id, BusStop busStop)
        {
            if (id != busStop.Id)
            {
                return BadRequest();
            }

            _context.Entry(busStop).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BusStopExists(id))
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

        // DELETE: api/BusStops/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBusStop(int id)
        {
            var busStop = await _context.BusStops.FindAsync(id);
            if (busStop == null)
            {
                return NotFound();
            }

            _context.BusStops.Remove(busStop);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BusStopExists(int id)
        {
            return _context.BusStops.Any(e => e.Id == id);
        }
    }
}