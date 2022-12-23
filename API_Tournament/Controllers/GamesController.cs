using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_Tournament.Models;
using Microsoft.AspNetCore.Authorization;

namespace API_Tournament.Controllers
{
    [Route("api/tournament/{tournamentId}/games")]
    [ApiController]
    [Authorize]
    public class GamesController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public GamesController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet()]
        public async Task<ActionResult<List<Game>>> GetGames(int tournamentId)
        {
            var game = await _context.Games.Where(x=>x.FkTournamentId == tournamentId).ToListAsync();

            if (game == null)
            {
                return NotFound();
            }

            return game;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Game>> GetGame(int tournamentId, int id)
        {
            var tournament = await _context.Tournaments.FindAsync(tournamentId);
            if(tournament == null)
                return NotFound();

            var game = tournament.Games.Where(x=>x.PkGameId == id).FirstOrDefault();

            if (game == null)
            {
                return NotFound();
            }

            return game;
        }

        // PUT: api/Games/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGame(int tournamentId, int id, [FromBody]   Game game)
        {
            if (id != game.PkGameId)    
            {
                return BadRequest();
            }
            var tournament = await _context.Tournaments.FindAsync(tournamentId);
            if (tournament == null)
                return NotFound();

            _context.Entry(game).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GameExists(tournamentId,id))
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

        // POST: api/Games
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost()]
        public async Task<ActionResult<Game>> PostGame([FromBody] Game game, int tournamentId)
        {
            var tournament = await _context.Tournaments.FindAsync(tournamentId);
            if (tournament == null)
                return NotFound();
            tournament.Games.Add(game);

            await _context.SaveChangesAsync();

            return Ok(game);
        }

        // DELETE: api/Games/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGame(int tournamentId,int id)
        {
            var tournament = await _context.Tournaments.FindAsync(tournamentId);
            if (tournament == null)
                return NotFound();
            
            var game = tournament.Games.FirstOrDefault(x=>x.PkGameId == id);
            if (game == null)
            {
                return NotFound();
            }

            _context.Games.Remove(game);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GameExists(int tournamentId, int id)
        {
            return _context.Games.Any(e => e.PkGameId == id && e.FkTournamentId == tournamentId);
        }
    }
}
