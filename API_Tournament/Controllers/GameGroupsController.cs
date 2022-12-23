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
    [Route("/api/tournament/{tournamentId}/game/{gameId}/GameGroups")]
    [ApiController]
    [Authorize]
    public class GameGroupsController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public GameGroupsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/GameGroups
        [HttpGet()]
        public async Task<ActionResult<IEnumerable<GameGroup>>> GetGameGroups(int tournamentId, int gameId)
        {
            var tournament = _context.Tournaments.Include(x => x.Games).FirstOrDefault(x => x.PkTournamentId == tournamentId);
            if (tournament == null)
                return NotFound("Tournament not found");

            var game = tournament.Games.FirstOrDefault(x => x.PkGameId == gameId);
            if (game == null)
                return NotFound("Game not found");
            var group = _context.GameGroups.Where(group => game.PkGameId == group.FkGameId);

            return Ok(group);
        }

        // GET: api/GameGroups/5
        [HttpGet("{groupId}")]
        public async Task<ActionResult<GameGroup>> GetGameGroup(int tournamentId, int gameId, int groupId)
        {
            var tournament = _context.Tournaments.Include(x=>x.Games).FirstOrDefault(x => x.PkTournamentId == tournamentId);
            if (tournament == null)
                return NotFound("Tournament not found");

            var game = tournament.Games.FirstOrDefault(x => x.PkGameId == gameId);
            if (game == null)
                return NotFound("Game not found");
            var group = _context.GameGroups.FirstOrDefault(group => group.PkGroupId == groupId && game.PkGameId == group.FkGameId);
            if (group == null)
                return NotFound("Group not found");
            return Ok(group);
        }

        // PUT: api/GameGroups/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{groupId}")]
        public async Task<IActionResult> PutGameGroup(int tournamentId, int gameId, int groupId, [FromBody]GameGroup gameGroup)
        {
            if (groupId != gameGroup.PkGroupId)
            {
                return BadRequest();
            }

            var tournament = await _context.Tournaments.FirstOrDefaultAsync(x=>x.PkTournamentId == tournamentId);
            if (tournament == null)
                return NotFound("Tournament not found");

            var game = tournament.Games.FirstOrDefault(x => x.PkGameId == gameId);
            if (game == null)
                return NotFound("Game not found");

            _context.Entry(gameGroup).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GameGroupExists(groupId))
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

        // POST: api/GameGroups
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<GameGroup>> PostGameGroup(int tournamentId, int gameId, int groupId, [FromBody] GameGroup gameGroup)
        {
            var tournament = _context.Tournaments.Include(x=>x.Games).FirstOrDefault(x=>x.PkTournamentId == tournamentId);
            if(tournament == null)
                return NotFound("Tournament not found");

            var game = tournament.Games.FirstOrDefault(x=>x.PkGameId==gameId);
            if (game == null)
                return NotFound("Game not found");
            gameGroup.FkGameId = gameId;
            _context.GameGroups.Add(gameGroup);
            await _context.SaveChangesAsync();

            return Ok(gameGroup);
        }

        private Game? GetGame(int tournamentId, int gameId)
        {
            var game = _context.Tournaments.Where(x => x.PkTournamentId == tournamentId).SelectMany(x => x.Games).Where(x=>x.PkGameId == gameId).FirstOrDefault();
            return game;
        }
        private bool GameGroupExists(int id)
        {
            return _context.GameGroups.Any(e => e.PkGroupId == id);
        }
    }
}
