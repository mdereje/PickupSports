using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PickUpApi.Data;
using PickUpApi.Models;

namespace PickUpApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Sports")]
    public class SportsController : Controller
    {
        private readonly PickupContext _context;

        public SportsController(PickupContext context)
        {
            _context = context;
        }

        // GET: api/Sports
        [HttpGet]
        public IEnumerable<Sport> GetSport()
        {
            return _context.Sports.ToList();
        }

        // GET: api/Sports/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSport([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var sport = await _context.Sports.SingleOrDefaultAsync(m => m.SportId == id);

            if (sport == null)
            {
                return NotFound();
            }

            return Ok(sport);
        }

        // PUT: api/Sports/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSport([FromRoute] long id, [FromBody] Sport sport)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != sport.SportId)
            {
                return BadRequest();
            }
            _context.Entry(sport).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SportExists(id))
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

        // POST: api/Sports
        [HttpPost]
        public async Task<IActionResult> PostSport([FromBody] Sport sport)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Sports.Add(sport);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSport", new { id = sport.SportId }, sport);
        }

        // DELETE: api/Sports/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSport([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var sport = await _context.Sports.SingleOrDefaultAsync(m => m.SportId == id);
            if (sport == null)
            {
                return NotFound();
            }

            _context.Sports.Remove(sport);
            await _context.SaveChangesAsync();

            return Ok(sport);
        }

        private bool SportExists(long id)
        {
            return _context.Sports.Any(e => e.SportId == id);
        }
    }
}