using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PickUpApi.Data;
using PickUpApi.Models.Relationship;

namespace PickUpApi.Controllers
{
    [Produces("application/json")]
    [Route("api/GamePlayers")]
    public class GamePlayersController : Controller
    {
        private readonly PickupContext _context;

        public GamePlayersController(PickupContext context)
        {
            _context = context;
        }

        // GET: api/GamePlayers
        [HttpGet]
        public IEnumerable<GamePlayer> GetGamePlayers()
        {
            return _context.GamePlayers;
        }

        // GET: api/GamePlayers/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGamePlayer([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var gamePlayer = _context.GamePlayers.Where(m => m.GameId == id).ToList();

            if (gamePlayer == null)
            {
                return NotFound();
            }

            return Ok(gamePlayer);
        }

        // PUT: api/GamePlayers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGamePlayer([FromRoute] long id, [FromBody] GamePlayer gamePlayer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != gamePlayer.GameId)
            {
                return BadRequest();
            }

            _context.Entry(gamePlayer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GamePlayerExists(id))
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

        // POST: api/GamePlayers
        [HttpPost]
        public async Task<IActionResult> PostGamePlayer([FromBody] GamePlayer gamePlayer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.GamePlayers.Add(gamePlayer);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (GamePlayerExists(gamePlayer.GameId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetGamePlayer", new { id = gamePlayer.GameId }, gamePlayer);
        }

        // DELETE: api/GamePlayers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGamePlayer([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var gamePlayer = await _context.GamePlayers.SingleOrDefaultAsync(m => m.GameId == id);
            if (gamePlayer == null)
            {
                return NotFound();
            }

            _context.GamePlayers.Remove(gamePlayer);
            await _context.SaveChangesAsync();

            return Ok(gamePlayer);
        }

        private bool GamePlayerExists(long id)
        {
            return _context.GamePlayers.Any(e => e.GameId == id);
        }
    }
}