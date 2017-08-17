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
    [Route("api/Game")]
    public class GamePlayersController : Controller
    {
        private readonly PickupContext _context;

        public GamePlayersController(PickupContext context)
        {
            _context = context;
        }

        // GET: api/games/{id}/player/{playerId}
        [HttpGet("{id}/player/{playerId}")]
        public async Task<IActionResult> GetPlayer([FromRoute] long id, [FromRoute] long playerId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var player = await _context.Players.Include(p => p.Name).SingleOrDefaultAsync(p => p.GameId == id && p.PlayerId == playerId);

            if (player == null)
            {
                return NotFound();
            }

            return Ok(player);
        }

        // PUT: api/games/{id}/player/{playerId}
        [HttpPut("{id}/player/{playerId}")]
        public async Task<IActionResult> PutPlayer([FromRoute] long id, [FromRoute] long playerId, [FromBody] Player player)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != player.PlayerId)
            {
                return BadRequest();
            }

            _context.Entry(player).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlayerExists(id))
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

        // POST: api/games/{id}/player/{playerId}
        [HttpPost("{id}/player/{playerId}")]
        public async Task<IActionResult> PostPlayer([FromRoute] long id, [FromRoute] long playerId, [FromBody] Player player)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Players.Add(player);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPlayer", new { id = player.PlayerId }, player);
        }

        // DELETE: api/games/{id}/player/{playerId}
        [HttpDelete("{id}/player/{playerId}")]
        public async Task<IActionResult> DeletePlayer([FromRoute] long id, [FromRoute] long playerId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var player = await _context.Players.SingleOrDefaultAsync(m => m.PlayerId == id);
            if (player == null)
            {
                return NotFound();
            }

            _context.Players.Remove(player);
            await _context.SaveChangesAsync();

            return Ok(player);
        }

        private bool PlayerExists(long id)
        {
            return _context.Players.Any(e => e.PlayerId == id);
        }
    }
}