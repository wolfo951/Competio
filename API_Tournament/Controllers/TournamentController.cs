using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_Tournament.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TournamentController : ControllerBase
    {
        private readonly DatabaseContext _context;
        public TournamentController(DatabaseContext context)
        {
            _context = context;
        }
        // GET: api/<TournamentController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tournament>>> Get()
        {
            bool displayPrivate = QueryActive("private");
            bool displayPublic = QueryActive("public");
            var userIdQuery = this.Request.Query.Where(query => query.Key == "userId");
            int userId = userIdQuery.Any() ? int.Parse(userIdQuery.First().Value) : -1;
            var publicTournamentDisplay = this.Request.Query.Where(x => x.Key == "public");
            return _context.Tournaments.Where(tournament => ((tournament.IsPrivate.Value && displayPrivate) || (!tournament.IsPrivate.Value && displayPublic)) && (userId == -1 || tournament.Players.Any(player=>player.FkUserId == userId))).ToList();
        }

        private bool QueryActive(string key)
        {
            var foundValue = this.Request.Query.Where(x => x.Key == key);
            return foundValue.Any() ? bool.Parse(foundValue.First().Value) : false; 
        }

        // GET api/<TournamentController>/5
        [HttpGet("{PkTournamentId}")]
        public async Task<ActionResult<Tournament>> Get(int PkTournamentId)
        {
            var tournament = await _context.Tournaments.Include(tournament => tournament.Players)
                                                       .Include(x => x.Games)
                                                       .Where(x => x.PkTournamentId == PkTournamentId)
                                                       .FirstOrDefaultAsync();

            if (tournament == null)
                return NotFound();
            
            return Ok(tournament);
        }

        // POST api/<TournamentController>
        [HttpPost]
        [Authorize(Roles = "administrator,moderator")]
        public async Task<ActionResult<Tournament>> Post([FromBody] Tournament newTournament)
        {
            if(_context.Tournaments.Where(x=>x.Title == newTournament.Title).Any())
            {
                return BadRequest("Title already exists");
            }
            var tournament = _context.Tournaments.Add(newTournament);

            await _context.SaveChangesAsync();
            
            return Ok(tournament.Entity);
        }

        // PUT api/<TournamentController>/5
        [HttpPut("{PkTournamentId}")]
        public async Task<ActionResult<Tournament>> Put(int PkTournamentId, [FromBody] Tournament value)
        {
            value.PkTournamentId = PkTournamentId;
            _context.Tournaments.Update(value);
            await _context.SaveChangesAsync();
            return value;
        
        }


        [HttpPost("games")]
        [Authorize(Roles = "administrator,moderator")]
        public async Task<ActionResult> AddGamesToTournament([FromBody] IEnumerable<Game> games)
        {
            await _context.Games.AddRangeAsync(games);

            
            await _context.SaveChangesAsync();
            return Ok(await _context.Tournaments.Where(x=>x.PkTournamentId == games.First().FkTournamentId).Include(x=>x.Games).Include(x=>x.Players).FirstAsync());
        }

        [HttpPost("{tournamentId}/players")]
        [Authorize(Roles = "administrator,moderator,user")]
        public async Task<ActionResult> AddPlayersToTournament([FromBody] IEnumerable<int> users, int tournamentId)
        {
            var tournament = _context.Tournaments.Where(x => x.PkTournamentId == tournamentId).FirstOrDefault();
            if (tournament == null)
                return NotFound();

            if(tournament.Players.Where(x=>users.Contains(x.FkUserId)).Any())
            {
                return BadRequest("Already user exists");
            }

            await _context.Players.AddRangeAsync(users.Select(x => new Player() { FkTournamentId = tournamentId, FkUserId = x }));

            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{tournamentId}/players/{playerId}")]
        [Authorize(Roles = "administrator,moderator,user")]
        public async Task<ActionResult> RemovePlayersToTournament(int tournamentId, string playerId)
        {
            var tournament = _context.Tournaments.Where(x => x.PkTournamentId == tournamentId).FirstOrDefault();
            if (tournament == null)
                return NotFound();

            int userID = int.Parse(playerId);

            var player = tournament.Players.Where(x=>x.FkUserId == userID).First();

            _context.Players.Remove(player);

            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
